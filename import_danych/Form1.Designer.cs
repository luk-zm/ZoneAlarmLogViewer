namespace import_danych
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
            this.fileNameTextBox = new System.Windows.Forms.TextBox();
            this.fileLoadButton = new System.Windows.Forms.Button();
            this.fileDialogButton = new System.Windows.Forms.Button();
            this.aboutProgramButton = new System.Windows.Forms.Button();
            this.openCatalogButton = new System.Windows.Forms.Button();
            this.fullFileListBox = new System.Windows.Forms.ListBox();
            this.col6ListBox = new System.Windows.Forms.ListBox();
            this.col5ListBox = new System.Windows.Forms.ListBox();
            this.col4ListBox = new System.Windows.Forms.ListBox();
            this.col3ListBox = new System.Windows.Forms.ListBox();
            this.col2ListBox = new System.Windows.Forms.ListBox();
            this.col1ListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // fileNameTextBox
            // 
            this.fileNameTextBox.Location = new System.Drawing.Point(186, 12);
            this.fileNameTextBox.Name = "fileNameTextBox";
            this.fileNameTextBox.Size = new System.Drawing.Size(439, 20);
            this.fileNameTextBox.TabIndex = 0;
            this.fileNameTextBox.Text = "C:\\Users\\Luk\\source\\repos\\import_danych\\import_danych\\dane\\db_small\\ZALog2003.10." +
    "06.txt";
            // 
            // fileLoadButton
            // 
            this.fileLoadButton.Location = new System.Drawing.Point(105, 10);
            this.fileLoadButton.Name = "fileLoadButton";
            this.fileLoadButton.Size = new System.Drawing.Size(75, 23);
            this.fileLoadButton.TabIndex = 1;
            this.fileLoadButton.Text = "Wczytaj plik";
            this.fileLoadButton.UseVisualStyleBackColor = true;
            this.fileLoadButton.Click += new System.EventHandler(this.fileLoadButton_Click);
            // 
            // fileDialogButton
            // 
            this.fileDialogButton.Location = new System.Drawing.Point(631, 12);
            this.fileDialogButton.Name = "fileDialogButton";
            this.fileDialogButton.Size = new System.Drawing.Size(75, 23);
            this.fileDialogButton.TabIndex = 2;
            this.fileDialogButton.Text = "Otwórz plik";
            this.fileDialogButton.UseVisualStyleBackColor = true;
            this.fileDialogButton.Click += new System.EventHandler(this.fileDialogButton_Click);
            // 
            // aboutProgramButton
            // 
            this.aboutProgramButton.Location = new System.Drawing.Point(713, 12);
            this.aboutProgramButton.Name = "aboutProgramButton";
            this.aboutProgramButton.Size = new System.Drawing.Size(75, 23);
            this.aboutProgramButton.TabIndex = 10;
            this.aboutProgramButton.Text = "O programie";
            this.aboutProgramButton.UseVisualStyleBackColor = true;
            this.aboutProgramButton.Click += new System.EventHandler(this.aboutProgramButton_Click);
            // 
            // openCatalogButton
            // 
            this.openCatalogButton.Location = new System.Drawing.Point(13, 9);
            this.openCatalogButton.Name = "openCatalogButton";
            this.openCatalogButton.Size = new System.Drawing.Size(75, 23);
            this.openCatalogButton.TabIndex = 11;
            this.openCatalogButton.Text = "Otwórz katalog";
            this.openCatalogButton.UseVisualStyleBackColor = true;
            this.openCatalogButton.Click += new System.EventHandler(this.openCatalogButton_Click);
            // 
            // fullFileListBox
            // 
            this.fullFileListBox.FormattingEnabled = true;
            this.fullFileListBox.Location = new System.Drawing.Point(12, 44);
            this.fullFileListBox.Name = "fullFileListBox";
            this.fullFileListBox.Size = new System.Drawing.Size(776, 147);
            this.fullFileListBox.TabIndex = 3;
            // 
            // col6ListBox
            // 
            this.col6ListBox.FormattingEnabled = true;
            this.col6ListBox.Location = new System.Drawing.Point(644, 207);
            this.col6ListBox.Name = "col6ListBox";
            this.col6ListBox.Size = new System.Drawing.Size(120, 238);
            this.col6ListBox.TabIndex = 9;
            // 
            // col5ListBox
            // 
            this.col5ListBox.FormattingEnabled = true;
            this.col5ListBox.Location = new System.Drawing.Point(518, 207);
            this.col5ListBox.Name = "col5ListBox";
            this.col5ListBox.Size = new System.Drawing.Size(120, 238);
            this.col5ListBox.TabIndex = 8;
            // 
            // col4ListBox
            // 
            this.col4ListBox.FormattingEnabled = true;
            this.col4ListBox.Location = new System.Drawing.Point(392, 207);
            this.col4ListBox.Name = "col4ListBox";
            this.col4ListBox.Size = new System.Drawing.Size(120, 238);
            this.col4ListBox.TabIndex = 7;
            // 
            // col3ListBox
            // 
            this.col3ListBox.FormattingEnabled = true;
            this.col3ListBox.Location = new System.Drawing.Point(266, 207);
            this.col3ListBox.Name = "col3ListBox";
            this.col3ListBox.Size = new System.Drawing.Size(120, 238);
            this.col3ListBox.TabIndex = 6;
            // 
            // col2ListBox
            // 
            this.col2ListBox.FormattingEnabled = true;
            this.col2ListBox.Location = new System.Drawing.Point(140, 207);
            this.col2ListBox.Name = "col2ListBox";
            this.col2ListBox.Size = new System.Drawing.Size(120, 238);
            this.col2ListBox.TabIndex = 5;
            // 
            // col1ListBox
            // 
            this.col1ListBox.FormattingEnabled = true;
            this.col1ListBox.Location = new System.Drawing.Point(13, 207);
            this.col1ListBox.Name = "col1ListBox";
            this.col1ListBox.Size = new System.Drawing.Size(120, 238);
            this.col1ListBox.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.openCatalogButton);
            this.Controls.Add(this.aboutProgramButton);
            this.Controls.Add(this.col6ListBox);
            this.Controls.Add(this.col5ListBox);
            this.Controls.Add(this.col4ListBox);
            this.Controls.Add(this.col3ListBox);
            this.Controls.Add(this.col2ListBox);
            this.Controls.Add(this.col1ListBox);
            this.Controls.Add(this.fullFileListBox);
            this.Controls.Add(this.fileDialogButton);
            this.Controls.Add(this.fileLoadButton);
            this.Controls.Add(this.fileNameTextBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox fileNameTextBox;
        private System.Windows.Forms.Button fileLoadButton;
        private System.Windows.Forms.Button fileDialogButton;
        private System.Windows.Forms.Button aboutProgramButton;
        private System.Windows.Forms.Button openCatalogButton;
        private System.Windows.Forms.ListBox fullFileListBox;
        private System.Windows.Forms.ListBox col6ListBox;
        private System.Windows.Forms.ListBox col5ListBox;
        private System.Windows.Forms.ListBox col4ListBox;
        private System.Windows.Forms.ListBox col3ListBox;
        private System.Windows.Forms.ListBox col2ListBox;
        private System.Windows.Forms.ListBox col1ListBox;
    }
}

