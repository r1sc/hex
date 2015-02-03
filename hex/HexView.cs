using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace hex {
    class HexView : UserControl {
        [DllImport("User32.dll")]
        static extern bool CreateCaret(IntPtr hWnd, int hBitmap, int nWidth, int nHeight);

        [DllImport("User32.dll")]
        static extern bool SetCaretPos(int x, int y);

        [DllImport("User32.dll")]
        static extern bool DestroyCaret();

        [DllImport("User32.dll")]
        static extern bool ShowCaret(IntPtr hWnd);

        [DllImport("User32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        public event EventHandler DataChanged;
        public event EventHandler SelectionChanged;
        public bool ShowASCII { get; set; }
        public int SelectionStart { get; set; }
        public int SelectionEnd { get; set; }

        public int NumberOfColumns {
            get {
                var width = GetWidth();
                return this.ClientRectangle.Width / width;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public byte[] Data { get; set; }

        public int Offset { get; set; }

        public HexView() {
            //this.DoubleBuffered = true;
            this.LostFocus += HexView_LostFocus;
        }

        private bool _lostCaret = true;
        void HexView_LostFocus(object sender, EventArgs e) {
            _lostCaret = true;
        }

        private void RecreateCaret() {
            Debug.WriteLine("Recreating caret");
            _lostCaret = false;
            DestroyCaret();
            var width = GetWidth();//ClientRectangle.Width / NumberOfColumns;
            CreateCaret(this.Handle, 0, width, 24);
        }

        private bool _selecting = false;
        protected override void OnMouseDown(MouseEventArgs e) {
            var width = GetWidth();//ClientRectangle.Width / NumberOfColumns;
            var col = e.X / width;
            var row = e.Y / 24 - 1;
            var idx = row * NumberOfColumns + col;
            ClearSelection();
            SetCursor(idx);
            _selecting = true;
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            _selecting = false;
            base.OnMouseUp(e);
        }

        private int _prevCol, _prevRow;
        protected override void OnMouseMove(MouseEventArgs e) {
            if (_selecting) {
                var width = GetWidth();//ClientRectangle.Width / NumberOfColumns;
                var col = e.X / width;
                var row = e.Y / 24 - 1;
                if (col == _prevCol && row == _prevRow)
                    return;
                _prevCol = col;
                _prevRow = row;

                var idx = row * NumberOfColumns + col;
                RepaintCells();
                SelectionEnd = idx;
                if (SelectionChanged != null)
                    SelectionChanged(this, null);
            }
            base.OnMouseMove(e);
        }


        private string _buffer = "";
        private Brush _highlightBrush = new SolidBrush(SystemColors.Highlight);

        protected override void OnKeyPress(KeyPressEventArgs e) {
            if (ShowASCII) {
                Data[Offset * NumberOfColumns + SelectionStart] = (byte)e.KeyChar;
                SetCursor(SelectionStart + 1);
                RepaintCells();
                if (DataChanged != null)
                    DataChanged(this, null);
            }
            else {
                if ((e.KeyChar >= 48 && e.KeyChar <= 57) ||
                    (e.KeyChar >= 65 && e.KeyChar <= 70) ||
                    (e.KeyChar >= 97 && e.KeyChar <= 102)) {
                    _buffer += e.KeyChar.ToString().ToUpper();
                    if (_buffer.Length == 2) {
                        byte val;
                        if (byte.TryParse(_buffer, NumberStyles.HexNumber, null, out val)) {
                            Data[Offset * NumberOfColumns + SelectionStart] = val;
                            SetCursor(SelectionStart + 1);
                            if (DataChanged != null)
                                DataChanged(this, null);
                        }
                        _buffer = string.Empty;
                    }
                    RepaintCells();
                }
            }
            base.OnKeyPress(e);
        }

        private bool HasKey(Keys keyData, Keys keyMask) {
            return (keyData & keyMask) == keyMask;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            if (Focused) {
                bool selecting = HasKey(keyData, Keys.Shift);
                if (HasKey(keyData, Keys.Right)) {
                    SetCursor(SelectionStart + 1);
                    return true;
                }
                else if (HasKey(keyData, Keys.Left)) {
                    SetCursor(SelectionStart - 1);
                    return true;
                }
                else if (HasKey(keyData, Keys.Up)) {
                    SetCursor(SelectionStart - NumberOfColumns);
                    return true;
                }
                else if (HasKey(keyData, Keys.Down)) {
                    SetCursor(SelectionStart + NumberOfColumns);
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void SetCursor(int idx) {
            if (idx > Data.Length - 1 || idx < 0)
                return;

            var row = idx / NumberOfColumns;
            var visibleRows = ClientRectangle.Height / 24 - 1;
            if (row >= visibleRows)
                Offset = (visibleRows - row);

            var col = idx % NumberOfColumns;
            var width = GetWidth();//ClientRectangle.Width / NumberOfColumns;
            SelectionStart = SelectionEnd = idx;

            if (_lostCaret)
                RecreateCaret();
            SetCaretPos(col * width, (row + 1) * 24);
            ShowCaret(this.Handle);
            _buffer = string.Empty;

            if (SelectionChanged != null)
                SelectionChanged(this, null);
        }

        private int GetWidth() {
            return (int)(ShowASCII ? Font.Size : Font.Size * 2);
        }

        private Rectangle GetSelectionRectangle() {
            var rowStart = Math.Min(SelectionStart, SelectionEnd) / NumberOfColumns;
            var rowEnd = Math.Max(SelectionStart, SelectionEnd) / NumberOfColumns + 1;
            var width = GetWidth();//ClientRectangle.Width / NumberOfColumns;
            return new Rectangle(0, rowStart * 24 + 24, ClientRectangle.Width, (rowEnd - rowStart) * 24 + 24);
        }

        public void RepaintCells() {
            Invalidate(GetSelectionRectangle());
        }

        public void ClearSelection() {
            var rect = GetSelectionRectangle();
            SelectionEnd = SelectionStart;
            Invalidate(rect);
        }

        protected override void OnPaint(PaintEventArgs e) {
            var width = GetWidth();//ClientRectangle.Width / NumberOfColumns;

            // Column headers
            if (e.ClipRectangle.Top == 0) {
                e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(0, 0, ClientRectangle.Width, 24));
                for (int i = 0; i < NumberOfColumns; i++) {
                    var rect = new Rectangle(i * width, 0, width, 24);
                    var num = ShowASCII ? ((i % 15) + 1) : (i + 1);
                    TextRenderer.DrawText(e.Graphics, num.ToString("X"), Font, rect, Color.Black, Color.Transparent, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    ControlPaint.DrawBorder3D(e.Graphics, rect, Border3DStyle.RaisedInner);
                }
            }
            if (Data == null)
                return;

            // Cells
            var firstRow = e.ClipRectangle.Top / 24;
            if (firstRow == 0)
                firstRow = 1;
            var bottomRow = Math.Min(Data.Length / NumberOfColumns, (e.ClipRectangle.Bottom / 24) - 1);
            var firstCol = e.ClipRectangle.Left / width;
            var lastCol = e.ClipRectangle.Right / width;
            var cel = new Rectangle(0, 0, width, 24);
            for (int row = firstRow; row <= bottomRow; row++) {
                for (int col = firstCol; col < lastCol; col++) {
                    var idx = ((Offset + row - 1) * NumberOfColumns + col);
                    if (idx >= Data.Length)
                        return;
                    cel.X = col * width;
                    cel.Y = row * 24;
                    bool selected = false;
                    if (!Focused && idx == SelectionStart) {
                        e.Graphics.FillRectangle(_highlightBrush, cel);
                    }
                    if (SelectionStart != SelectionEnd && idx >= Math.Min(SelectionStart, SelectionEnd) && idx <= Math.Max(SelectionStart, SelectionEnd)) {
                        e.Graphics.FillRectangle(_highlightBrush, cel);
                        selected = true;
                    }
                    if (_buffer != string.Empty && idx == (Offset * NumberOfColumns + SelectionStart)) {
                        e.Graphics.FillRectangle(new SolidBrush(Color.IndianRed), cel);
                        TextRenderer.DrawText(e.Graphics, _buffer, Font, cel, SystemColors.HighlightText, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    }
                    else {
                        string text;
                        if (ShowASCII) {
                            var chr = (char)Data[idx];
                            if (chr < 33 || chr > 126)
                                chr = '.';
                            text = chr.ToString();
                        }
                        else {
                            text = Data[idx].ToString("X2");
                        }
                        TextRenderer.DrawText(e.Graphics, text, Font, cel, selected ? SystemColors.HighlightText : ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    }
                }
            }
        }
    }
}
