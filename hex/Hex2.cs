﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections.Generic;
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

        public event EventHandler<int> PositionChanged;
        public bool AsciiSelected { get; set; }
        public int CurrentPos { get; private set; }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public byte[] Data { get; set; }
        public enum dataTypes {Byte, UInt16, Int16, UInt32, Int32, Float };
        public List<Int32> QueryHitAddresses { get; set; }
        
        private readonly VScrollBar _scrollBar;

        public Hex2() {
            /*Data = new byte[1280];
            Random r = new Random();
            r.NextBytes(Data);
            */
            Data = new byte[0];
            _scrollBar = new VScrollBar();
            _scrollBar.Dock = DockStyle.Right;
            _scrollBar.Scroll += _scrollBar_Scroll;
            Controls.Add(_scrollBar); 
            QueryHitAddresses = new List<Int32>();
        }

        void _scrollBar_Scroll(object sender, ScrollEventArgs e) {
            Invalidate();       
            UpdateCaret(CurrentPos);        
        }

        public int GetNumberOfColumns() {
            var width = Font.Size;
            var u = width * 2 + width;
            var c = ClientRectangle.Width / u;
            return (int)c - 1;
        }

        public void MakeSurePositionIsVisible(int position) {
            if (position < 0 || position >= Data.Length)
                return;
            var numColumns = GetNumberOfColumns();
            var row = position / numColumns + 1;
            var col = position % numColumns;

            var screenRow = row - _scrollBar.Value;
            var visibleRows = ClientRectangle.Height / 24 - 1;
            if (screenRow <= 0) {
                _scrollBar.Value = row - 1;
                Invalidate();
            }
            if (screenRow > visibleRows) {
                _scrollBar.Value = (row - visibleRows);
                Invalidate();
            }
        }

        public void UpdateCaret(int newPos) {
            if (newPos < 0 || newPos >= Data.Length)
                return;
            CurrentPos = newPos;
            var numColumns = GetNumberOfColumns();
            var row = CurrentPos / numColumns + 1;
            var col = CurrentPos % numColumns;

            var screenRow = row - _scrollBar.Value;
            var visibleRows = ClientRectangle.Height / 24 - 1;
            if (screenRow <= 0 || screenRow > visibleRows) {
                _lastCaretType = 0;
                DestroyCaret();
                return;
            }

            if (AsciiSelected) {
                if (_lastCaretType != 2) {
                    CreateCaret(Handle, 0, (int)Font.Size, 24);
                    _lastCaretType = 2;
                }
                SetCaretPos((numColumns * (int)(Font.Size * 2)) + (int)Font.Size + col * (int)Font.Size, screenRow * 24);
            }
            else {
                if (_lastCaretType != 1) {
                    CreateCaret(Handle, 0, (int)(Font.Size * 2), 24);
                    _lastCaretType = 1;
                }
                SetCaretPos(col * (int)(Font.Size * 2), screenRow * 24);
            }
            ShowCaret(Handle);
            
            if (PositionChanged != null)
                PositionChanged(this, CurrentPos);
        }

        public void PositionCursor(int newpos) {
            ClearBuffer();
            MakeSurePositionIsVisible(newpos);
            UpdateCaret(newpos);
        }        

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            if (keyData == Keys.Left) {
                PositionCursor(CurrentPos - 1);
                return true;
            }
            if (keyData == Keys.Right) {
                PositionCursor(CurrentPos + 1);
                return true;
            }
            if (keyData == Keys.Up) {
                PositionCursor(CurrentPos - GetNumberOfColumns());
                return true;
            }
            if (keyData == Keys.Down) {
                PositionCursor(CurrentPos + GetNumberOfColumns());
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private string _buffer = string.Empty;
        protected override void OnKeyPress(KeyPressEventArgs e) {
            var upper = e.KeyChar.ToString().ToUpper()[0];
            if (!AsciiSelected && upper >= 'A' && upper <= 'F' || char.IsDigit(upper)) {
                _buffer += upper;
                if (_buffer.Length == 2) {
                    var num = byte.Parse(_buffer, NumberStyles.HexNumber);
                    ClearBuffer();
                    Data[CurrentPos] = num;
                    RedrawCurrentByte();
                    PositionCursor(CurrentPos + 1);
                }
                e.Handled = true;
            }
            else if (AsciiSelected && Data.Length > 0) {
         //   else if (AsciiSelected)
            
                Data[CurrentPos] = (byte)e.KeyChar;
                RedrawCurrentByte();
                PositionCursor(CurrentPos + 1);
            }
            base.OnKeyPress(e);
        }

        public void ClearBuffer()  {
            _buffer = string.Empty;
        }

        private void RedrawCurrentByte()
        {
            var scrollBarPosition = _scrollBar.Value;
            var numColumns = GetNumberOfColumns();
            var row = CurrentPos / numColumns + 1;
            var col = CurrentPos % numColumns;
            Rectangle rect;
            var charWidth = (int)(Font.Size * 2);
            var asciiStart = charWidth * numColumns;
            rect = new Rectangle(col * charWidth, (row - scrollBarPosition) * 24, charWidth, 24);
            Invalidate(rect);

            charWidth = (int)(Font.Size);
            asciiStart += charWidth;
            rect = new Rectangle(asciiStart + col * charWidth, ((row - scrollBarPosition) * 24), charWidth, 24);
            Invalidate(rect);
        }

        private int _lastCaretType;
        protected override void OnMouseDown(MouseEventArgs e) {
            var numColumns = GetNumberOfColumns();
            var charWidth = (int)(Font.Size * 2);
            var leftViewWidth = numColumns * charWidth;
            int row = e.Y / 24 + _scrollBar.Value - 1;

            int col = e.X / (int)(Font.Size * 2);
            if (e.X >= leftViewWidth)
                col = (e.X - leftViewWidth) / (int)(Font.Size) - 1;

            var idx = row * numColumns + col;
            if (e.X < leftViewWidth) {
                AsciiSelected = false;
                UpdateCaret(idx);
            }
            else if (col >= 0 && col < numColumns) {
                AsciiSelected = true;
                UpdateCaret(idx);
            }

          
            base.OnMouseDown(e);
        }

        private int _oldNumColumns;
        private int _oldVisibleRows;
        protected override void OnResize(EventArgs e) {
            var numColumns = GetNumberOfColumns();
            int visibleRows = ClientRectangle.Height / 24;
            if (numColumns != _oldNumColumns || visibleRows != _oldVisibleRows) {
                _oldNumColumns = numColumns;
                _oldVisibleRows = visibleRows;
                UpdateScrollbar();
                PositionCursor(CurrentPos);
                Invalidate();
            }

            base.OnResize(e);
        }

        private void UpdateScrollbar() {
            int visibleRows = ClientRectangle.Height / 24;
            var numDataRows = Data.Length / GetNumberOfColumns();
            _scrollBar.Maximum = numDataRows - visibleRows + 2;
            _scrollBar.Minimum = 0;
            _scrollBar.LargeChange = 1;
            _scrollBar.SmallChange = 1;
            _scrollBar.Visible = (_scrollBar.Maximum != 0);
        }

        protected override void OnPaint(PaintEventArgs e) {
            //Debug.WriteLine("ClipRect: " + e.ClipRectangle);
            //e.Graphics.DrawRectangle(Pens.Red, e.ClipRectangle);

            var numColumns = GetNumberOfColumns();
            var charWidth = (int)Font.Size;
            var rect = new Rectangle(0, 0, charWidth * 2, 24);
            
            if (e.ClipRectangle.Top == 0) {
                e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(0, 0, ClientRectangle.Width, 24));

                for (int i = 0; i < numColumns; i++) {
                    var num = (i );
                    TextRenderer.DrawText(e.Graphics, num.ToString("X"), Font, rect, Color.Black, Color.Transparent, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    ControlPaint.DrawBorder3D(e.Graphics, rect, Border3DStyle.RaisedInner);
                    rect.X += charWidth * 2;
                }
                rect.X += charWidth;

                rect.Width = charWidth;
                for (int i = 0; i < numColumns; i++) {
                    var num = ((i % 16));
                    TextRenderer.DrawText(e.Graphics, num.ToString("X"), Font, rect, Color.Black, Color.Transparent, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    rect.X += charWidth;
                }
            }

            var lineX = (charWidth * 2) * numColumns;
            e.Graphics.DrawLine(Pens.Silver, lineX, 0, lineX, ClientRectangle.Bottom);

            int startRow = Math.Max(1, e.ClipRectangle.Top / 24);
            int endRow = e.ClipRectangle.Bottom / 24;
            int aidx, idx;
            aidx = idx = numColumns * (startRow + _scrollBar.Value - 1);
            for (int row = startRow; row < endRow; row++) {
                rect.Y = row * 24;
                rect.X = 0;
                rect.Width = charWidth * 2;
                for (int col = 0; col < numColumns; col++) {
                    if (idx >= Data.Length)
                        return;
                    TextRenderer.DrawText(e.Graphics, Data[idx].ToString("X2"), Font, rect, ForeColor);
                    idx++;
                    rect.X += charWidth * 2;
                }
                rect.Width = charWidth;
                rect.X += charWidth;
                for (int col = 0; col < numColumns; col++) {
                    char c = (char)Data[aidx];
                    if (c <= 32 || c >= 127)
                        c = '.';
                    TextRenderer.DrawText(e.Graphics, c.ToString(), Font, rect, ForeColor);
                    aidx++;
                    rect.X += charWidth;
                }
            }
        }



        public void SetData(byte[] data)  {
            _lastCaretType = 0;
            DestroyCaret();

            Data = data;
            UpdateScrollbar();
            _scrollBar.Value = 0;
            PositionCursor(0);
            Invalidate();
        }



        public void Search(String searchText, int dataType, bool aligned) {
            QueryHitAddresses.Clear();

            int totalBytes = Data.Length;
            byte[] needle = setNeedle(searchText, dataType);

            int jumpLength = GetAmountOfBytes(dataType);
            if (!aligned) jumpLength = 1;
                  
            for (Int32 address = 0; address < totalBytes; address += jumpLength)   {
                int offset = 0;
                while (offset < needle.Length && needle[offset] == Data[address + offset])
                    offset++;

                if (offset == needle.Length) {
                    QueryHitAddresses.Add(address);
                }           
            }
            if (QueryHitAddresses.Count > 0) {
                PositionCursor(QueryHitAddresses[0]);
                RefreshHexFocus();
            }
            else {
                MessageBox.Show("Not Found");
            }
      
        }

        //Determine the pattern to look for
        private byte[] setNeedle(String searchText, int dataType)  {
            if (dataType == (int)Hex2.dataTypes.Byte) return HexStringToBytes(searchText);
            else if (dataType == (int)Hex2.dataTypes.UInt16) return BitConverter.GetBytes(Convert.ToUInt16(searchText));
            else if (dataType == (int)Hex2.dataTypes.Int16) return BitConverter.GetBytes(Convert.ToInt16(searchText));
            else if (dataType == (int)Hex2.dataTypes.UInt32) return BitConverter.GetBytes(Convert.ToUInt32(searchText));
            else if (dataType == (int)Hex2.dataTypes.Int32) return BitConverter.GetBytes(Convert.ToInt32(searchText));
            else if (dataType == (int)Hex2.dataTypes.Float) return BitConverter.GetBytes(Convert.ToSingle(searchText));
            else return null;
        }

        private int GetHexCharValue(char c) {
            switch (c) {
                case '0': return 0;
                case '1': return 1;
                case '2': return 2;
                case '3': return 3;
                case '4': return 4;
                case '5': return 5;
                case '6': return 6;
                case '7': return 7;
                case '8': return 8;
                case '9': return 9;
                case 'A': return 10;
                case 'B': return 11;
                case 'C': return 12;
                case 'D': return 13;
                case 'E': return 14;
                case 'F': return 15;
                default : return -1;
            }
        }


        private int GetAmountOfBytes(int dataType) {
            switch (dataType) {
                case (int)dataTypes.Byte : return 1;
                case (int)dataTypes.UInt16: return 2;
                case (int)dataTypes.Int16: return 2;
                case (int)dataTypes.UInt32: return 4;
                case (int)dataTypes.Int32: return 4;
                case (int)dataTypes.Float: return 4;
                default: return -1;
            }
        }

        //this is nesessary to keep the selected position flashing
        //in the hex columns after the search textbox has taken focus
        public void RefreshHexFocus() {
            AsciiSelected = !AsciiSelected;
            PositionCursor(CurrentPos);
            AsciiSelected = !AsciiSelected;
            PositionCursor(CurrentPos);
        }

        //Takes string of hex characters and returns it as a byte array
        //Input can only contain hex characters
        private byte[] HexStringToBytes(string s) {
            byte[] byteArray = new byte[s.Length / 2];
            for (int index = 0; index < s.Length; index += 2) {
                int char1Value = GetHexCharValue(s[index]);
                int char2Value = GetHexCharValue(s[index + 1]);
                byteArray[index / 2] = Convert.ToByte(char1Value * 16 + char2Value);
            }
            return byteArray;
        }

        private void InitializeComponent() {
            this.SuspendLayout();
            // 
            // Hex2
            // 
            this.Name = "Hex2";
            this.ResumeLayout(false);
        }

    }
}
