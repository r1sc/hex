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
        public int NumberOfColumns { get; set; }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public byte[] Data { get; set; }

        public HexView()
        {
            this.DoubleBuffered = true;
            NumberOfColumns = 15;
            this.LostFocus += HexView_LostFocus;
        }

        private bool _lostCaret = true;
        void HexView_LostFocus(object sender, EventArgs e)
        {
            _lostCaret = true;
        }

        private void RecreateCaret() {
            Debug.WriteLine("Recreating caret");
            _lostCaret = false;
            DestroyCaret();
            var width = ClientRectangle.Width / NumberOfColumns;
            CreateCaret(this.Handle, 0, width, 24);
        }

        protected override void OnResize(EventArgs e) {
            //RecreateCaret();
            base.OnResize(e);
        }

        private bool _selecting = false;
        protected override void OnMouseDown(MouseEventArgs e) {
            var width = ClientRectangle.Width / NumberOfColumns;
            var col = e.X / width;
            var row = e.Y / 24;
            if (row > 0)
            {
                var idx = row*NumberOfColumns + col;
                SetCursor(idx);
                _selecting = true;
                Invalidate();
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            _selecting = false;
            base.OnMouseUp(e);
        }

        private int _prevCol, _prevRow;
        protected override void OnMouseMove(MouseEventArgs e) {
            if (_selecting) {
                var width = ClientRectangle.Width / NumberOfColumns;
                var col = e.X / width;
                var row = e.Y / 24;
                if (col == _prevCol && row == _prevRow)
                    return;
                _prevCol = col;
                _prevRow = row;

                var idx = row * NumberOfColumns + col;
                SelectionEnd = idx;
                Invalidate();
                if (SelectionChanged != null)
                    SelectionChanged(this, null);
            }
            base.OnMouseMove(e);
        }

        private string _buffer = "";
        private Brush _highlightBrush = new SolidBrush(SystemColors.Highlight);

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (ShowASCII)
            {
                Data[SelectionStart] = (byte)e.KeyChar;
                SetCursor(SelectionStart + 1);
                if (DataChanged != null)
                    DataChanged(this, null);
            }
            else
            {
                if ((e.KeyChar >= 48 && e.KeyChar <= 57) ||
                    (e.KeyChar >= 65 && e.KeyChar <= 70) ||
                    (e.KeyChar >= 97 && e.KeyChar <= 102))
                {
                    _buffer += e.KeyChar.ToString().ToUpper();
                    if (_buffer.Length == 2)
                    {
                        byte val;
                        if (byte.TryParse(_buffer, NumberStyles.HexNumber, null, out val))
                        {
                            Data[SelectionStart] = val;
                            SetCursor(SelectionStart + 1);
                            if (DataChanged != null)
                                DataChanged(this, null);
                        }
                        _buffer = string.Empty;
                    }
                }
            }
            Invalidate();
            base.OnKeyPress(e);
        }

        private bool HasKey(Keys keyData, Keys keyMask)
        {
            return (keyData & keyMask) == keyMask;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (Focused)
            {
                bool selecting = HasKey(keyData, Keys.Shift);
                if (HasKey(keyData, Keys.Right))
                {
                    SetCursor(SelectionStart + 1);
                    return true;
                }
                else if (HasKey(keyData, Keys.Left))
                {
                    SetCursor(SelectionStart - 1);
                    return true;
                }
                else if (HasKey(keyData, Keys.Up))
                {
                    SetCursor(SelectionStart - NumberOfColumns);
                    return true;
                }
                else if (HasKey(keyData, Keys.Down))
                {
                    SetCursor(SelectionStart + NumberOfColumns);
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void SetCursor(int idx)
        {
            if (idx > Data.Length - 1
                || idx < NumberOfColumns)
                return;
            var row = idx/NumberOfColumns;
            var visibleRows = ClientRectangle.Height / 24 - 1;
            if (row >= visibleRows)
                return;
            var col = idx%NumberOfColumns;
            var width = ClientRectangle.Width / NumberOfColumns;
            SelectionStart = SelectionEnd = idx;
            if (_lostCaret)
                RecreateCaret();
            SetCaretPos(col * width, row * 24);
            ShowCaret(this.Handle);
            _buffer = string.Empty;
            //Invalidate();
            if(SelectionChanged != null)
                SelectionChanged(this, null);
        }

        protected override void OnPaint(PaintEventArgs e) {
            var width = ClientRectangle.Width / NumberOfColumns;
            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(0, 0, ClientRectangle.Width, 24));
            for (int i = 0; i < NumberOfColumns; i++) {
                var rect = new Rectangle(i * width, 0, width, 24);
                TextRenderer.DrawText(e.Graphics, (i+1).ToString("X"), Font, rect, ForeColor, Color.Transparent, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                ControlPaint.DrawBorder3D(e.Graphics, rect, Border3DStyle.Raised);
            }
            if (Data == null)
                return;
            var visibleRows = Math.Min(Data.Length / NumberOfColumns, (ClientRectangle.Height / 24) - 1);
            var col = 0;
            var row = 1;
            var cel = new Rectangle(0, 24, width, 24);
            var celPt = new Point();
            while (true) {
                cel.X = col * width;
                cel.Y = row * 24;
                var idx = (row * NumberOfColumns + col);
                if (idx >= Data.Length)
                    break;
                bool selected = false;
                if(idx == SelectionStart && !Focused)
                    e.Graphics.FillRectangle(Brushes.LightSalmon, cel);
                if (SelectionStart != SelectionEnd && idx >= Math.Min(SelectionStart, SelectionEnd) && idx <= Math.Max(SelectionStart, SelectionEnd)) {
                    e.Graphics.FillRectangle(_highlightBrush, cel);
                    selected = true;
                }
                if (_buffer != string.Empty && idx == SelectionStart)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.IndianRed), cel);
                    TextRenderer.DrawText(e.Graphics, _buffer, Font, cel, SystemColors.HighlightText, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }
                else
                {
                    TextRenderer.DrawText(e.Graphics, ShowASCII ? ((char)Data[idx]).ToString() : Data[idx].ToString("X2"), Font, cel, selected ? SystemColors.HighlightText : ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }
                ControlPaint.DrawBorder(e.Graphics, cel, Color.LightGray, ButtonBorderStyle.Solid);
                

                col++;
                if (col >= NumberOfColumns) {
                    col = 0;
                    row++;
                    if (row >= visibleRows)
                        break;
                }
            }
        }
    }
}
