using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hex {
    public partial class FormTest2 : Form {
        public FormTest2() {
            InitializeComponent();
        }

        private void FormTest2_Load(object sender, EventArgs e) {
            hex21.KeyDown += hex21_KeyDown;
            hex21.PositionChanged += hex21_PositionChanged;
        }

        void hex21_PositionChanged(object sender, int e) {
            lblByte.Text = hex21.Data[hex21.CurrentPos].ToString();
            if (hex21.CurrentPos + 1 < hex21.Data.Length)
                lblUint16.Text = BitConverter.ToUInt16(hex21.Data, hex21.CurrentPos).ToString();
            if (hex21.CurrentPos + 1 < hex21.Data.Length)
                lblInt16.Text = BitConverter.ToInt16(hex21.Data, hex21.CurrentPos).ToString();
            if (hex21.CurrentPos + 3 < hex21.Data.Length)
                lblUint32.Text = BitConverter.ToUInt32(hex21.Data, hex21.CurrentPos).ToString();
            if (hex21.CurrentPos + 3 < hex21.Data.Length)
                lblInt32.Text = BitConverter.ToInt32(hex21.Data, hex21.CurrentPos).ToString();
            if (hex21.CurrentPos + 3 < hex21.Data.Length)
                lblFloat.Text = BitConverter.ToSingle(hex21.Data, hex21.CurrentPos).ToString();
        }

        void hex21_KeyDown(object sender, KeyEventArgs e) {
            if (e.Control && e.KeyCode == Keys.O) {
                var ofd = new OpenFileDialog {
                    Filter = "All Files (*.*)|*.*"
                };
                if (ofd.ShowDialog() == DialogResult.OK) {
                    hex21.SetData(File.ReadAllBytes(ofd.FileName));
                }
            }
        }
    }
}
