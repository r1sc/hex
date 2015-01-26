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
        private void Form1_Load(object sender, EventArgs e)
        {
            var data = new byte[4096];
            var random = new Random(0);
            random.NextBytes(data);
            hexView1.Data = hexView2.Data = data;

            hexView1.SelectionChanged += hexView1_SelectionChanged;
            hexView2.SelectionChanged += hexView2_SelectionChanged;
            hexView1.DataChanged += hexView1_DataChanged;
            hexView2.DataChanged += hexView2_DataChanged;
            this.Resize += Form1_Resize;

            _dataInspector = new frmDataInspector();
            _dataInspector.Show();
        }

        void Form1_Resize(object sender, EventArgs e) {
            hexView1.Invalidate();
            hexView2.Invalidate();
        }

        void hexView2_SelectionChanged(object sender, EventArgs e)
        {
            //_dataInspector.DataView.Offset = hexView2.SelectionStart;
            //_dataInspector.UpdateView();
            hexView1.ClearSelection();
            hexView1.SelectionStart = hexView2.SelectionStart;
            hexView1.SelectionEnd = hexView2.SelectionEnd;
            hexView1.RepaintCells();
        }

        void hexView1_SelectionChanged(object sender, EventArgs e)
        {
            //_dataInspector.DataView.Offset = hexView1.SelectionStart;
            //_dataInspector.UpdateView();
            hexView2.ClearSelection();
            hexView2.SelectionStart = hexView1.SelectionStart;
            hexView2.SelectionEnd = hexView1.SelectionEnd;
            hexView2.RepaintCells();
        }

        private void hexView1_Load(object sender, EventArgs e) {

        }
        
        void hexView2_DataChanged(object sender, EventArgs e) {
            //hexView1.Invalidate();
        }

        void hexView1_DataChanged(object sender, EventArgs e) {
            //hexView2.Invalidate();
        }

        private void openToolStripButton_Click(object sender, EventArgs e) {
            if (_fileStream != null)
                _fileStream.Dispose();
            var ofd = new OpenFileDialog
            {
                Filter = "All Files (*.*)|*.*", 
                Title = "Open File"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    hexView1.Data = hexView2.Data = File.ReadAllBytes(ofd.FileName);
                    _dataInspector.DataView = new DataView(hexView1.Data);
                    lblSize.Text = hexView1.Data.Length + " byte(s)";
                    hexView1.Invalidate();
                    hexView2.Invalidate();
                    //ReadBlock(0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cannot open the file");
                }
            }
        }

        private void ReadBlock(int start)
        {
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
