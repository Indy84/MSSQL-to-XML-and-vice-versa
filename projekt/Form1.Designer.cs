namespace projekt
{
    partial class Form1
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
            this.DGV_OConfigPack = new System.Windows.Forms.DataGridView();
            this.DGV_ODesign = new System.Windows.Forms.DataGridView();
            this.button_FromDataBaseToApplication = new System.Windows.Forms.Button();
            this.textBoxHost = new System.Windows.Forms.TextBox();
            this.textBoxDBName = new System.Windows.Forms.TextBox();
            this.labelHost = new System.Windows.Forms.Label();
            this.labelDBName = new System.Windows.Forms.Label();
            this.labelOConfigPack = new System.Windows.Forms.Label();
            this.labelODesign = new System.Windows.Forms.Label();
            this.button_FromApplicationToXML = new System.Windows.Forms.Button();
            this.button_FromXMLToDataBase = new System.Windows.Forms.Button();
            this.saveFileDialog_XML = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog_XML = new System.Windows.Forms.OpenFileDialog();
            this.radioButton_FromDBtoXML = new System.Windows.Forms.RadioButton();
            this.radioButton_FromXMLtoDB = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxLogs = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_OConfigPack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_ODesign)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DGV_OConfigPack
            // 
            this.DGV_OConfigPack.AllowUserToAddRows = false;
            this.DGV_OConfigPack.AllowUserToDeleteRows = false;
            this.DGV_OConfigPack.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGV_OConfigPack.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_OConfigPack.Location = new System.Drawing.Point(19, 143);
            this.DGV_OConfigPack.MultiSelect = false;
            this.DGV_OConfigPack.Name = "DGV_OConfigPack";
            this.DGV_OConfigPack.ReadOnly = true;
            this.DGV_OConfigPack.RowHeadersVisible = false;
            this.DGV_OConfigPack.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGV_OConfigPack.Size = new System.Drawing.Size(409, 312);
            this.DGV_OConfigPack.TabIndex = 0;
            this.DGV_OConfigPack.SelectionChanged += new System.EventHandler(this.DGV_OConfigPack_SelectionChanged);
            // 
            // DGV_ODesign
            // 
            this.DGV_ODesign.AllowUserToAddRows = false;
            this.DGV_ODesign.AllowUserToDeleteRows = false;
            this.DGV_ODesign.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGV_ODesign.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_ODesign.Location = new System.Drawing.Point(449, 143);
            this.DGV_ODesign.Name = "DGV_ODesign";
            this.DGV_ODesign.ReadOnly = true;
            this.DGV_ODesign.RowHeadersVisible = false;
            this.DGV_ODesign.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGV_ODesign.Size = new System.Drawing.Size(481, 312);
            this.DGV_ODesign.TabIndex = 1;
            // 
            // button_FromDataBaseToApplication
            // 
            this.button_FromDataBaseToApplication.Location = new System.Drawing.Point(225, 19);
            this.button_FromDataBaseToApplication.Name = "button_FromDataBaseToApplication";
            this.button_FromDataBaseToApplication.Size = new System.Drawing.Size(206, 32);
            this.button_FromDataBaseToApplication.TabIndex = 2;
            this.button_FromDataBaseToApplication.Text = "wyeksportuj bazę danych do programu";
            this.button_FromDataBaseToApplication.UseVisualStyleBackColor = true;
            this.button_FromDataBaseToApplication.Click += new System.EventHandler(this.Button_FromDataBaseToApplication_Click);
            // 
            // textBoxHost
            // 
            this.textBoxHost.Location = new System.Drawing.Point(93, 35);
            this.textBoxHost.Name = "textBoxHost";
            this.textBoxHost.Size = new System.Drawing.Size(156, 20);
            this.textBoxHost.TabIndex = 3;
            // 
            // textBoxDBName
            // 
            this.textBoxDBName.Location = new System.Drawing.Point(93, 61);
            this.textBoxDBName.Name = "textBoxDBName";
            this.textBoxDBName.Size = new System.Drawing.Size(156, 20);
            this.textBoxDBName.TabIndex = 4;
            // 
            // labelHost
            // 
            this.labelHost.AutoSize = true;
            this.labelHost.Location = new System.Drawing.Point(57, 38);
            this.labelHost.Name = "labelHost";
            this.labelHost.Size = new System.Drawing.Size(30, 13);
            this.labelHost.TabIndex = 5;
            this.labelHost.Text = "host:";
            // 
            // labelDBName
            // 
            this.labelDBName.AutoSize = true;
            this.labelDBName.Location = new System.Drawing.Point(16, 64);
            this.labelDBName.Name = "labelDBName";
            this.labelDBName.Size = new System.Drawing.Size(71, 13);
            this.labelDBName.TabIndex = 6;
            this.labelDBName.Text = "baza danych:";
            // 
            // labelOConfigPack
            // 
            this.labelOConfigPack.AutoSize = true;
            this.labelOConfigPack.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelOConfigPack.Location = new System.Drawing.Point(444, 115);
            this.labelOConfigPack.Name = "labelOConfigPack";
            this.labelOConfigPack.Size = new System.Drawing.Size(113, 25);
            this.labelOConfigPack.TabIndex = 7;
            this.labelOConfigPack.Text = "formularze";
            // 
            // labelODesign
            // 
            this.labelODesign.AutoSize = true;
            this.labelODesign.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelODesign.Location = new System.Drawing.Point(14, 115);
            this.labelODesign.Name = "labelODesign";
            this.labelODesign.Size = new System.Drawing.Size(220, 25);
            this.labelODesign.TabIndex = 8;
            this.labelODesign.Text = "paczki konfiguracyjne";
            // 
            // button_FromApplicationToXML
            // 
            this.button_FromApplicationToXML.Location = new System.Drawing.Point(437, 19);
            this.button_FromApplicationToXML.Name = "button_FromApplicationToXML";
            this.button_FromApplicationToXML.Size = new System.Drawing.Size(204, 32);
            this.button_FromApplicationToXML.TabIndex = 9;
            this.button_FromApplicationToXML.Text = "zapisz wybrane rekordy w XML";
            this.button_FromApplicationToXML.UseVisualStyleBackColor = true;
            this.button_FromApplicationToXML.Click += new System.EventHandler(this.Button_FromApplicationToXML_Click);
            // 
            // button_FromXMLToDataBase
            // 
            this.button_FromXMLToDataBase.Enabled = false;
            this.button_FromXMLToDataBase.Location = new System.Drawing.Point(225, 57);
            this.button_FromXMLToDataBase.Name = "button_FromXMLToDataBase";
            this.button_FromXMLToDataBase.Size = new System.Drawing.Size(416, 31);
            this.button_FromXMLToDataBase.TabIndex = 10;
            this.button_FromXMLToDataBase.Text = "zaimportuj dane z XML do bazy danych";
            this.button_FromXMLToDataBase.UseVisualStyleBackColor = true;
            this.button_FromXMLToDataBase.Click += new System.EventHandler(this.Button_FromXMLToDataBase_Click);
            // 
            // openFileDialog_XML
            // 
            this.openFileDialog_XML.FileName = "openFileDialog1";
            // 
            // radioButton_FromDBtoXML
            // 
            this.radioButton_FromDBtoXML.AutoSize = true;
            this.radioButton_FromDBtoXML.Checked = true;
            this.radioButton_FromDBtoXML.Location = new System.Drawing.Point(6, 34);
            this.radioButton_FromDBtoXML.Name = "radioButton_FromDBtoXML";
            this.radioButton_FromDBtoXML.Size = new System.Drawing.Size(209, 17);
            this.radioButton_FromDBtoXML.TabIndex = 11;
            this.radioButton_FromDBtoXML.TabStop = true;
            this.radioButton_FromDBtoXML.Text = "eksport danych z bazy danych do XML";
            this.radioButton_FromDBtoXML.UseVisualStyleBackColor = true;
            this.radioButton_FromDBtoXML.CheckedChanged += new System.EventHandler(this.RadioButton_FromDBtoXML_CheckedChanged);
            // 
            // radioButton_FromXMLtoDB
            // 
            this.radioButton_FromXMLtoDB.AutoSize = true;
            this.radioButton_FromXMLtoDB.Location = new System.Drawing.Point(6, 57);
            this.radioButton_FromXMLtoDB.Name = "radioButton_FromXMLtoDB";
            this.radioButton_FromXMLtoDB.Size = new System.Drawing.Size(202, 17);
            this.radioButton_FromXMLtoDB.TabIndex = 12;
            this.radioButton_FromXMLtoDB.Text = "import danych z XML do bazy danych";
            this.radioButton_FromXMLtoDB.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_FromDataBaseToApplication);
            this.groupBox1.Controls.Add(this.radioButton_FromDBtoXML);
            this.groupBox1.Controls.Add(this.button_FromApplicationToXML);
            this.groupBox1.Controls.Add(this.button_FromXMLToDataBase);
            this.groupBox1.Controls.Add(this.radioButton_FromXMLtoDB);
            this.groupBox1.Location = new System.Drawing.Point(277, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(653, 100);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // textBoxLogs
            // 
            this.textBoxLogs.Location = new System.Drawing.Point(19, 475);
            this.textBoxLogs.Multiline = true;
            this.textBoxLogs.Name = "textBoxLogs";
            this.textBoxLogs.ReadOnly = true;
            this.textBoxLogs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxLogs.Size = new System.Drawing.Size(911, 173);
            this.textBoxLogs.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 668);
            this.Controls.Add(this.textBoxLogs);
            this.Controls.Add(this.labelOConfigPack);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelODesign);
            this.Controls.Add(this.labelDBName);
            this.Controls.Add(this.labelHost);
            this.Controls.Add(this.textBoxDBName);
            this.Controls.Add(this.textBoxHost);
            this.Controls.Add(this.DGV_ODesign);
            this.Controls.Add(this.DGV_OConfigPack);
            this.Name = "Form1";
            this.Text = "DataBase to XML and vice versa 3.0";
            ((System.ComponentModel.ISupportInitialize)(this.DGV_OConfigPack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_ODesign)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGV_OConfigPack;
        private System.Windows.Forms.DataGridView DGV_ODesign;
        private System.Windows.Forms.Button button_FromDataBaseToApplication;
        private System.Windows.Forms.TextBox textBoxHost;
        private System.Windows.Forms.TextBox textBoxDBName;
        private System.Windows.Forms.Label labelHost;
        private System.Windows.Forms.Label labelDBName;
        private System.Windows.Forms.Label labelOConfigPack;
        private System.Windows.Forms.Label labelODesign;
        private System.Windows.Forms.Button button_FromApplicationToXML;
        private System.Windows.Forms.Button button_FromXMLToDataBase;
        private System.Windows.Forms.SaveFileDialog saveFileDialog_XML;
        private System.Windows.Forms.OpenFileDialog openFileDialog_XML;
        private System.Windows.Forms.RadioButton radioButton_FromDBtoXML;
        private System.Windows.Forms.RadioButton radioButton_FromXMLtoDB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxLogs;
    }
}

