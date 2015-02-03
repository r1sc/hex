using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hex {
    class Hex2 : UserControl {
        #region Win32
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
        #endregion

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public byte[] Data { get; set; }

        private VScrollBar _scrollBar;

        public Hex2() {
            Data = new byte[1280];
            Random r = new Random();
            r.NextBytes(Data);

            _scrollBar = new VScrollBar();
            _scrollBar.Dock = DockStyle.Right;
            _scrollBar.Scroll += _scrollBar_Scroll;
            Controls.Add(_scrollBar);
        }

        void _scrollBar_Scroll(object sender, ScrollEventArgs e) {
            Invalidate();
        }

        public int GetNumberOfColumns() {
            var width = Font.Size;
            var u = width * 2 + width;
            var c = ClientRectangle.Width / u;
            return (int)c;
        }

        private int _lastCaretType = 0;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            int charWidth = (int)(Font.Size*2);
            var leftViewWidth = GetNumberOfColumns()*charWidth;
            if (e.X < leftViewWidth)
            {
                if (_lastCaretType != 1)
                {
                    CreateCaret(Handle, 0, (int) (Font.Size*2), 24);
                    _lastCaretType = 1;
                }
                int row = e.Y/24;
                int col = e.X/(int)(Font.Size*2);
                SetCaretPos(col * (int)(Font.Size * 2), row * 24);
            }
            else
            {
                if (_lastCaretType != 2)
                {
                    CreateCaret(Handle, 0, (int) Font.Size, 24);
                    _lastCaretType = 2;
                }

                int row = e.Y / 24;
                int col = e.X / (int)(Font.Size);
                SetCaretPos(col * (int)Font.Size, row * 24);
            }
            ShowCaret(Handle);
            base.OnMouseDown(e);
        }

        private int _oldNumColumns;
        private int _oldVisibleRows;
        protected override void OnResize(EventArgs e)
        {
            var numColumns = GetNumberOfColumns();
            int visibleRows = ClientRectangle.Height / 24;
            if (numColumns != _oldNumColumns || visibleRows != _oldVisibleRows)
            {
                _oldNumColumns = numColumns;
                _oldVisibleRows = visibleRows;
                var numDataRows = Data.Length/GetNumberOfColumns();
                _scrollBar.Maximum = numDataRows - visibleRows + 2;
                _scrollBar.Minimum = 0;
                _scrollBar.LargeChange = 1;
                _scrollBar.SmallChange = 1;
                Invalidate();
            }

            base.OnResize(e);
        }

        protected override void OnPaint(PaintEventArgs e) {
            Debug.WriteLine("ClipRect: " + e.ClipRectangle);
            var numColumns = GetNumberOfColumns();
            var charWidth = (int) Font.Size;    
            var rect = new Rectangle(0, 0, charWidth * 2, 24);
            
            if (e.ClipRectangle.Top == 0) {
                e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(0, 0, ClientRectangle.Width, 24));

                for (int i = 0; i < numColumns; i++)
                {
                    var num = (i + 1);
                    TextRenderer.DrawText(e.Graphics, num.ToString("X"), Font, rect, Color.Black, Color.Transparent, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    ControlPaint.DrawBorder3D(e.Graphics, rect, Border3DStyle.RaisedInner);
                    rect.X += charWidth * 2;
                }
                rect.X += charWidth;
                
                rect.Width = charWidth;
                for (int i = 0; i < numColumns; i++) {
                    var num = ((i % 15) + 1);
                    TextRenderer.DrawText(e.Graphics, num.ToString("X"), Font, rect, Color.Black, Color.Transparent, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    rect.X += charWidth;
                }
            }

            var lineX = (charWidth*2)*numColumns;
            e.Graphics.DrawLine(Pens.Silver, lineX, 0, lineX, ClientRectangle.Bottom);

            int startRow = Math.Max(1, e.ClipRectangle.Top/24);
            int endRow = e.ClipRectangle.Bottom/24 + 1;
            int aidx, idx;
            aidx = idx = numColumns * (startRow + _scrollBar.Value - 1);
            for (int row = startRow; row < endRow; row++)
            {
                rect.Y = row * 24;
                rect.X = 0;
                rect.Width = charWidth * 2;
                for (int col = 0; col < numColumns; col++)
                {
                    if (idx >= Data.Length)
                        return;
                    TextRenderer.DrawText(e.Graphics, Data[idx].ToString("X2"), Font, rect, ForeColor);
                    idx ++;
                    rect.X += charWidth * 2;
                }
                rect.Width = charWidth;
                rect.X += charWidth;
                for (int col = 0; col < numColumns; col++)
                {
                    char c = (char)Data[aidx];
                    if (c <= 32 || c >= 127)
                        c = '.';
                    TextRenderer.DrawText(e.Graphics, c.ToString(), Font, rect, ForeColor);
                    aidx++;
                    rect.X += charWidth;
                }
            }
        }
    }
}
