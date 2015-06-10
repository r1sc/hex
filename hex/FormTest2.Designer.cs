namespace hex {
    partial class FormTest2 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTest2));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblByte = new System.Windows.Forms.Label();
            this.lblInt32 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblUint16 = new System.Windows.Forms.Label();
            this.lblInt16 = new System.Windows.Forms.Label();
            this.lblUint32 = new System.Windows.Forms.Label();
            this.lblFloat = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.alignedSearchCheckBox = new System.Windows.Forms.CheckBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.searchResultsListBox = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.searchTextBox = new System.Windows.Forms.RichTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatusSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusCol = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusRow = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusPosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.hex21 = new hex.Hex2();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.searchPanel.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.searchPanel);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(578, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(193, 501);
            this.panel1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.89474F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.10526F));
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblByte, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblInt32, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblUint16, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblInt16, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblUint32, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblFloat, 1, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 20);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(193, 130);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Float";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Int32";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "UInt32";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Int16";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "UInt16";
            // 
            // lblByte
            // 
            this.lblByte.BackColor = System.Drawing.SystemColors.Control;
            this.lblByte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblByte.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblByte.ForeColor = System.Drawing.Color.Black;
            this.lblByte.Location = new System.Drawing.Point(60, 3);
            this.lblByte.Name = "lblByte";
            this.lblByte.Size = new System.Drawing.Size(127, 18);
            this.lblByte.TabIndex = 2;
            this.lblByte.Text = "N/A";
            // 
            // lblInt32
            // 
            this.lblInt32.BackColor = System.Drawing.SystemColors.Control;
            this.lblInt32.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInt32.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInt32.ForeColor = System.Drawing.Color.Black;
            this.lblInt32.Location = new System.Drawing.Point(60, 87);
            this.lblInt32.Name = "lblInt32";
            this.lblInt32.Size = new System.Drawing.Size(127, 18);
            this.lblInt32.TabIndex = 9;
            this.lblInt32.Text = "N/A";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Byte";
            // 
            // lblUint16
            // 
            this.lblUint16.BackColor = System.Drawing.SystemColors.Control;
            this.lblUint16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUint16.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUint16.ForeColor = System.Drawing.Color.Black;
            this.lblUint16.Location = new System.Drawing.Point(60, 24);
            this.lblUint16.Name = "lblUint16";
            this.lblUint16.Size = new System.Drawing.Size(127, 18);
            this.lblUint16.TabIndex = 4;
            this.lblUint16.Text = "N/A";
            // 
            // lblInt16
            // 
            this.lblInt16.BackColor = System.Drawing.SystemColors.Control;
            this.lblInt16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInt16.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInt16.ForeColor = System.Drawing.Color.Black;
            this.lblInt16.Location = new System.Drawing.Point(60, 45);
            this.lblInt16.Name = "lblInt16";
            this.lblInt16.Size = new System.Drawing.Size(127, 18);
            this.lblInt16.TabIndex = 5;
            this.lblInt16.Text = "N/A";
            // 
            // lblUint32
            // 
            this.lblUint32.BackColor = System.Drawing.SystemColors.Control;
            this.lblUint32.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUint32.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUint32.ForeColor = System.Drawing.Color.Black;
            this.lblUint32.Location = new System.Drawing.Point(60, 66);
            this.lblUint32.Name = "lblUint32";
            this.lblUint32.Size = new System.Drawing.Size(127, 18);
            this.lblUint32.TabIndex = 7;
            this.lblUint32.Text = "N/A";
            // 
            // lblFloat
            // 
            this.lblFloat.BackColor = System.Drawing.SystemColors.Control;
            this.lblFloat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFloat.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFloat.ForeColor = System.Drawing.Color.Black;
            this.lblFloat.Location = new System.Drawing.Point(60, 108);
            this.lblFloat.Name = "lblFloat";
            this.lblFloat.Size = new System.Drawing.Size(127, 19);
            this.lblFloat.TabIndex = 11;
            this.lblFloat.Text = "N/A";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.LightGray;
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(193, 20);
            this.label7.TabIndex = 10;
            this.label7.Text = "Data inspector";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // searchPanel
            // 
            this.searchPanel.Controls.Add(this.alignedSearchCheckBox);
            this.searchPanel.Controls.Add(this.searchButton);
            this.searchPanel.Controls.Add(this.searchResultsListBox);
            this.searchPanel.Controls.Add(this.label9);
            this.searchPanel.Controls.Add(this.label8);
            this.searchPanel.Controls.Add(this.typeComboBox);
            this.searchPanel.Controls.Add(this.searchTextBox);
            this.searchPanel.Enabled = false;
            this.searchPanel.Location = new System.Drawing.Point(0, 150);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(193, 351);
            this.searchPanel.TabIndex = 11;
            // 
            // alignedSearchCheckBox
            // 
            this.alignedSearchCheckBox.AutoSize = true;
            this.alignedSearchCheckBox.Location = new System.Drawing.Point(124, 37);
            this.alignedSearchCheckBox.Name = "alignedSearchCheckBox";
            this.alignedSearchCheckBox.Size = new System.Drawing.Size(61, 17);
            this.alignedSearchCheckBox.TabIndex = 17;
            this.alignedSearchCheckBox.Text = "Aligned";
            this.alignedSearchCheckBox.UseVisualStyleBackColor = true;
            // 
            // searchButton
            // 
            this.searchButton.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchButton.Location = new System.Drawing.Point(63, 118);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(61, 23);
            this.searchButton.TabIndex = 16;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // searchResultsListBox
            // 
            this.searchResultsListBox.BackColor = System.Drawing.SystemColors.Control;
            this.searchResultsListBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchResultsListBox.FormattingEnabled = true;
            this.searchResultsListBox.ItemHeight = 14;
            this.searchResultsListBox.Location = new System.Drawing.Point(7, 148);
            this.searchResultsListBox.Name = "searchResultsListBox";
            this.searchResultsListBox.Size = new System.Drawing.Size(178, 200);
            this.searchResultsListBox.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(7, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 14);
            this.label9.TabIndex = 14;
            this.label9.Text = "Type";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.LightGray;
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(193, 20);
            this.label8.TabIndex = 13;
            this.label8.Text = "Search";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // typeComboBox
            // 
            this.typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeComboBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Items.AddRange(new object[] {
            "Byte",
            "UInt16",
            "Int16",
            "UInt32",
            "Int32",
            "Float"});
            this.typeComboBox.Location = new System.Drawing.Point(48, 32);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(70, 22);
            this.typeComboBox.TabIndex = 1;
            // 
            // searchTextBox
            // 
            this.searchTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchTextBox.Location = new System.Drawing.Point(7, 60);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(178, 52);
            this.searchTextBox.TabIndex = 0;
            this.searchTextBox.Text = "";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusSize,
            this.lblStatusCol,
            this.lblStatusRow,
            this.lblStatusPosition});
            this.statusStrip1.Location = new System.Drawing.Point(0, 501);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(771, 22);
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatusSize
            // 
            this.lblStatusSize.Name = "lblStatusSize";
            this.lblStatusSize.Size = new System.Drawing.Size(756, 17);
            this.lblStatusSize.Spring = true;
            this.lblStatusSize.Text = "Open with CTRL-O";
            this.lblStatusSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStatusCol
            // 
            this.lblStatusCol.Name = "lblStatusCol";
            this.lblStatusCol.Size = new System.Drawing.Size(0, 17);
            // 
            // lblStatusRow
            // 
            this.lblStatusRow.Name = "lblStatusRow";
            this.lblStatusRow.Size = new System.Drawing.Size(0, 17);
            // 
            // lblStatusPosition
            // 
            this.lblStatusPosition.Name = "lblStatusPosition";
            this.lblStatusPosition.Size = new System.Drawing.Size(0, 17);
            // 
            // hex21
            // 
            this.hex21.AsciiSelected = false;
            this.hex21.BackColor = System.Drawing.Color.White;
            this.hex21.Data = new byte[0];
            this.hex21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hex21.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hex21.Location = new System.Drawing.Point(0, 0);
            this.hex21.Name = "hex21";
            this.hex21.QueryHitAddresses = ((System.Collections.Generic.List<int>)(resources.GetObject("hex21.SearchHitAddresses")));
            this.hex21.Size = new System.Drawing.Size(578, 501);
            this.hex21.TabIndex = 0;
            // 
            // FormTest2
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 523);
            this.Controls.Add(this.hex21);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormTest2";
            this.Text = "Base16";
            this.Load += new System.EventHandler(this.FormTest2_Load);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Hex2 hex21;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblByte;
        private System.Windows.Forms.Label lblInt32;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblUint16;
        private System.Windows.Forms.Label lblInt16;
        private System.Windows.Forms.Label lblUint32;
        private System.Windows.Forms.Label lblFloat;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusSize;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusCol;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusRow;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusPosition;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.RichTextBox searchTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ListBox searchResultsListBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.CheckBox alignedSearchCheckBox;
    }
}