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
        }

        void hex21_KeyDown(object sender, KeyEventArgs e) {
            if (e.Control && e.KeyCode == Keys.O) {
                var ofd = new OpenFileDialog {
                    Filter = "All Files (*.*)|*.*"
                };
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    hex21.SetData(File.ReadAllBytes(ofd.FileName));
                }
            }
        }
    }
}
