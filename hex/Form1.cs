using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hex {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private frmDataInspector _dataInspector;
        private FileStream _fileStream;
        private void Form1_Load(object sender, EventArgs e) {
            var data = new byte[4096];
            var random = new Random(0);
            random.NextBytes(data);

            vScrollBar1.Scroll += vScrollBar1_Scroll;

            vScrollBar1.Minimum = 0;
            vScrollBar1.Maximum = 1;
            vScrollBar1.SmallChange = 1;
            vScrollBar1.LargeChange = 1;
            _dataInspector = new frmDataInspector();
            _dataInspector.Show();
            _dataInspector.DataView = new DataView(data);

            hexView1.Data = hexView2.Data = data;

            hexView1.SelectionChanged += hexView1_SelectionChanged;
            hexView2.SelectionChanged += hexView2_SelectionChanged;
            this.Resize += Form1_Resize;
        }

        void vScrollBar1_Scroll(object sender, ScrollEventArgs e) {
            hexView1.Offset = hexView2.Offset = e.NewValue;
            hexView1.Invalidate();
            hexView2.Invalidate();
        }

        private int numColumns1, numColumns2;
        void Form1_Resize(object sender, EventArgs e) {
            UpdateScrollbar();
            if (hexView1.NumberOfColumns != numColumns1) {
                numColumns1 = hexView1.NumberOfColumns;
                if(vScrollBar1.Maximum > -1)
                    vScrollBar1.Value = 0;
                hexView1.Invalidate();
            }

            if (hexView2.NumberOfColumns != numColumns2) {
                numColumns2 = hexView2.NumberOfColumns;
                if (vScrollBar1.Maximum > -1)
                    vScrollBar1.Value = 0;
                hexView2.Invalidate();
            }
        }

        void hexView2_SelectionChanged(object sender, EventArgs e) {
            _dataInspector.DataView.Offset = hexView2.SelectionStart;
            _dataInspector.UpdateView();
            hexView1.ClearSelection();
            hexView1.SelectionStart = hexView2.SelectionStart;
            hexView1.SelectionEnd = hexView2.SelectionEnd;
            hexView1.RepaintCells();
        }

        void hexView1_SelectionChanged(object sender, EventArgs e) {
            _dataInspector.DataView.Offset = hexView1.SelectionStart;
            _dataInspector.UpdateView();
            hexView2.ClearSelection();
            hexView2.SelectionStart = hexView1.SelectionStart;
            hexView2.SelectionEnd = hexView1.SelectionEnd;
            hexView2.RepaintCells();
        }

        void UpdateScrollbar() {
            var sourceRows = hexView1.Data.Length / hexView1.NumberOfColumns;
            var numRows = (hexView1.ClientRectangle.Height / 24) - 1;
            vScrollBar1.Maximum = Math.Max(0, sourceRows - numRows + 1);
            hexView1.Offset = hexView2.Offset = vScrollBar1.Value = Math.Min(vScrollBar1.Value, vScrollBar1.Maximum);
        }

        private void openToolStripButton_Click(object sender, EventArgs e) {
            if (_fileStream != null)
                _fileStream.Dispose();
            var ofd = new OpenFileDialog {
                Filter = "All Files (*.*)|*.*",
                Title = "Open File"
            };
            if (ofd.ShowDialog() == DialogResult.OK) {
                try {
                    hexView1.Data = hexView2.Data = File.ReadAllBytes(ofd.FileName);
                    _dataInspector.DataView = new DataView(hexView1.Data);
                    lblSize.Text = hexView1.Data.Length + " byte(s)";
                    hexView1.Invalidate();
                    hexView2.Invalidate();
                    UpdateScrollbar();
                    //ReadBlock(0);
                }
                catch (Exception ex) {
                    MessageBox.Show("Cannot open the file");
                }
            }
        }

        private void ReadBlock(int start) {
            var buffer = new byte[4096];
            int readBytes = _fileStream.Read(buffer, start, 4096);
            var sizedBuffer = new byte[readBytes];
            Array.Copy(buffer, sizedBuffer, readBytes);
            hexView1.Data = hexView2.Data = sizedBuffer;
            hexView1.Invalidate();
            hexView2.Invalidate();
        }
    }
}
