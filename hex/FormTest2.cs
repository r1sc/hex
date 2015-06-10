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
            this.DragEnter += FormTest2_DragEnter;
            this.DragDrop += FormTest2_DragDrop;
            typeComboBox.SelectedIndex = 0;
       
        }


        void FormTest2_DragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        void FormTest2_DragDrop(object sender, DragEventArgs e) {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            LoadData(files.First());
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

            var numColumns = hex21.GetNumberOfColumns();
            var row = hex21.CurrentPos/numColumns;
            var col = hex21.CurrentPos%numColumns;
            lblStatusCol.Text = "Col " + col.ToString();
            lblStatusRow.Text = "Row " + row.ToString();
            lblStatusPosition.Text = "Pos " + (hex21.CurrentPos+1).ToString();
        }

        void hex21_KeyDown(object sender, KeyEventArgs e) {
            if (e.Control && e.KeyCode == Keys.O) {
                var ofd = new OpenFileDialog {
                    Filter = "All Files (*.*)|*.*"
                };
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    LoadData(ofd.FileName);
                }
            }
            else if (e.Control && e.KeyCode == Keys.S){
                var sfd = new SaveFileDialog
                {
                    Filter = "All Files (*.*)|*.*"
                };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    SaveData(sfd.FileName);
                }
            }
          
              
        }

        private void LoadData(string fileName)
        {
            hex21.SetData(File.ReadAllBytes(fileName));
            lblStatusSize.Text = hex21.Data.Length.ToString() + " bytes";
            panel1.Enabled = true;
            searchPanel.Enabled = true;
        }

        private void SaveData(string fileName)
        {
            File.WriteAllBytes(fileName,hex21.Data);
        }

        private void searchButton_Click(object sender, EventArgs e)
        {

            string searchText = searchTextBox.Text; 
            ///sanitize
            int dataType = typeComboBox.SelectedIndex;
            bool aligned = alignedSearchCheckBox.Checked;
           

            hex21.Search(searchText, dataType, aligned);

            updateResultsListBox();
          
        }
        private void updateResultsListBox()
        {
            foreach (Int32 i in hex21.QueryHitAddresses)
            {
                searchResultsListBox.Items.Clear();
                searchResultsListBox.Items.Add(i.ToString("X8"));
            }
        }

     

    }
}
