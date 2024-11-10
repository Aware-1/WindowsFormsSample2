namespace WindowsFormsApp1
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Button SearchBtn1;
            this.NameBox = new System.Windows.Forms.TextBox();
            this.nameLbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.AddBtn = new System.Windows.Forms.Button();
            this.کارکنان = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnSearchAdmin = new System.Windows.Forms.Button();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.SearchBtn = new System.Windows.Forms.Button();
            this.cityComboBox = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.CityBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Strip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Deletetem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.Strip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.limitingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            SearchBtn1 = new System.Windows.Forms.Button();
            this.کارکنان.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.Strip1.SuspendLayout();
            this.Strip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // SearchBtn1
            // 
            SearchBtn1.AllowDrop = true;
            SearchBtn1.AutoEllipsis = true;
            SearchBtn1.Location = new System.Drawing.Point(359, 12);
            SearchBtn1.Name = "SearchBtn1";
            SearchBtn1.Size = new System.Drawing.Size(51, 23);
            SearchBtn1.TabIndex = 10;
            SearchBtn1.TabStop = false;
            SearchBtn1.Text = "جستجو";
            SearchBtn1.UseVisualStyleBackColor = true;
            SearchBtn1.UseWaitCursor = true;
            SearchBtn1.Click += new System.EventHandler(this.SearchBtn1_Click);
            // 
            // NameBox
            // 
            this.NameBox.Location = new System.Drawing.Point(419, 16);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(100, 21);
            this.NameBox.TabIndex = 1;
            // 
            // nameLbl
            // 
            this.nameLbl.AutoSize = true;
            this.nameLbl.Location = new System.Drawing.Point(525, 20);
            this.nameLbl.Name = "nameLbl";
            this.nameLbl.Size = new System.Drawing.Size(20, 13);
            this.nameLbl.TabIndex = 3;
            this.nameLbl.Text = "نام";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(386, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "شهر";
            // 
            // AddBtn
            // 
            this.AddBtn.Location = new System.Drawing.Point(15, 14);
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Size = new System.Drawing.Size(75, 23);
            this.AddBtn.TabIndex = 6;
            this.AddBtn.Text = "افزودن";
            this.AddBtn.UseVisualStyleBackColor = true;
            this.AddBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // کارکنان
            // 
            this.کارکنان.AccessibleDescription = "کارکنان";
            this.کارکنان.AccessibleName = "کارکنان";
            this.کارکنان.AllowDrop = true;
            this.کارکنان.Controls.Add(this.tabPage3);
            this.کارکنان.Controls.Add(this.tabPage1);
            this.کارکنان.Controls.Add(this.tabPage2);
            this.کارکنان.Location = new System.Drawing.Point(12, 12);
            this.کارکنان.Name = "کارکنان";
            this.کارکنان.SelectedIndex = 0;
            this.کارکنان.Size = new System.Drawing.Size(566, 374);
            this.کارکنان.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.کارکنان.TabIndex = 7;
            this.کارکنان.Click += new System.EventHandler(this.کارکنان_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.textBox1);
            this.tabPage3.Controls.Add(this.btnReport);
            this.tabPage3.Controls.Add(this.btnSearchAdmin);
            this.tabPage3.Controls.Add(this.dataGridView3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(558, 348);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "مدیران";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(14, 22);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(173, 21);
            this.textBox1.TabIndex = 3;
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(14, 99);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(173, 23);
            this.btnReport.TabIndex = 2;
            this.btnReport.Text = "گزارش ";
            this.btnReport.UseVisualStyleBackColor = true;
            // 
            // btnSearchAdmin
            // 
            this.btnSearchAdmin.Location = new System.Drawing.Point(14, 49);
            this.btnSearchAdmin.Name = "btnSearchAdmin";
            this.btnSearchAdmin.Size = new System.Drawing.Size(173, 23);
            this.btnSearchAdmin.TabIndex = 1;
            this.btnSearchAdmin.Text = "search";
            this.btnSearchAdmin.UseVisualStyleBackColor = true;
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(211, 3);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.Size = new System.Drawing.Size(341, 342);
            this.dataGridView3.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.SearchBtn);
            this.tabPage1.Controls.Add(this.cityComboBox);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.AddBtn);
            this.tabPage1.Controls.Add(this.NameBox);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.nameLbl);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(558, 348);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "کارکنان";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // SearchBtn
            // 
            this.SearchBtn.Location = new System.Drawing.Point(202, 13);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(51, 23);
            this.SearchBtn.TabIndex = 9;
            this.SearchBtn.Text = "جستجو";
            this.SearchBtn.UseVisualStyleBackColor = true;
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click_1);
            // 
            // cityComboBox
            // 
            this.cityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cityComboBox.FormattingEnabled = true;
            this.cityComboBox.Location = new System.Drawing.Point(259, 15);
            this.cityComboBox.Name = "cityComboBox";
            this.cityComboBox.Size = new System.Drawing.Size(121, 21);
            this.cityComboBox.TabIndex = 8;
            this.cityComboBox.Click += new System.EventHandler(this.cityComboBox_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 43);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(559, 305);
            this.dataGridView1.TabIndex = 7;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(SearchBtn1);
            this.tabPage2.Controls.Add(this.CityBox2);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.dataGridView2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(558, 348);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "شهر";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // CityBox2
            // 
            this.CityBox2.Location = new System.Drawing.Point(416, 15);
            this.CityBox2.Name = "CityBox2";
            this.CityBox2.Size = new System.Drawing.Size(100, 21);
            this.CityBox2.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(522, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "شهر";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(0, 49);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(552, 300);
            this.dataGridView2.TabIndex = 0;
            // 
            // Strip1
            // 
            this.Strip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Deletetem1});
            this.Strip1.Name = "Strip1";
            this.Strip1.Size = new System.Drawing.Size(100, 26);
            // 
            // Deletetem1
            // 
            this.Deletetem1.Name = "Deletetem1";
            this.Deletetem1.Size = new System.Drawing.Size(99, 22);
            this.Deletetem1.Text = "حذف";
            this.Deletetem1.Click += new System.EventHandler(this.Deletetem1_Click);
            // 
            // Strip2
            // 
            this.Strip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.limitingToolStripMenuItem});
            this.Strip2.Name = "Strip1";
            this.Strip2.Size = new System.Drawing.Size(116, 48);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(115, 22);
            this.toolStripMenuItem1.Text = "حذف";
            // 
            // limitingToolStripMenuItem
            // 
            this.limitingToolStripMenuItem.Name = "limitingToolStripMenuItem";
            this.limitingToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.limitingToolStripMenuItem.Text = "limiting";
            this.limitingToolStripMenuItem.Click += new System.EventHandler(this.limitingToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 395);
            this.Controls.Add(this.کارکنان);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "مدیریت کارکنان";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.کارکنان.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.Strip1.ResumeLayout(false);
            this.Strip2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.Label nameLbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button AddBtn;
        private System.Windows.Forms.TabControl کارکنان;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ComboBox cityComboBox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox CityBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button SearchBtn;
        private System.Windows.Forms.ContextMenuStrip Strip1;
        private System.Windows.Forms.ToolStripMenuItem Deletetem1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.ContextMenuStrip Strip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem limitingToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnSearchAdmin;
    }
}

