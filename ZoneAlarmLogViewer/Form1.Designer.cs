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
            this.listView = new System.Windows.Forms.ListView();
            this.eventCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dateCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timeCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.inAddrCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.outAddrCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.protocolCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fullFileListView = new System.Windows.Forms.ListView();
            this.processedFiles = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.processedLinesCountLabel = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // fileNameTextBox
            // 
            this.fileNameTextBox.Location = new System.Drawing.Point(186, 12);
            this.fileNameTextBox.Name = "fileNameTextBox";
            this.fileNameTextBox.Size = new System.Drawing.Size(439, 20);
            this.fileNameTextBox.TabIndex = 0;
            this.fileNameTextBox.Text = "D:\\rizzai\\ZoneAlarmLogViewer\\ZoneAlarmLogViewer\\dane\\db\\ZALog2003.10.03.txt";
            this.fileNameTextBox.TextChanged += new System.EventHandler(this.fileNameTextBox_TextChanged);
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
            this.openCatalogButton.Size = new System.Drawing.Size(93, 23);
            this.openCatalogButton.TabIndex = 11;
            this.openCatalogButton.Text = "Otwórz katalog";
            this.openCatalogButton.UseVisualStyleBackColor = true;
            this.openCatalogButton.Click += new System.EventHandler(this.openCatalogButton_Click);
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.eventCol,
            this.dateCol,
            this.timeCol,
            this.inAddrCol,
            this.outAddrCol,
            this.protocolCol});
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(12, 198);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(776, 229);
            this.listView.TabIndex = 12;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.VirtualMode = true;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            // 
            // eventCol
            // 
            this.eventCol.Text = "Zdarzenie";
            // 
            // dateCol
            // 
            this.dateCol.Text = "Data";
            this.dateCol.Width = 90;
            // 
            // timeCol
            // 
            this.timeCol.Text = "Czas";
            this.timeCol.Width = 120;
            // 
            // inAddrCol
            // 
            this.inAddrCol.Text = "AdresWe";
            this.inAddrCol.Width = 120;
            // 
            // outAddrCol
            // 
            this.outAddrCol.Text = "AdresWy";
            this.outAddrCol.Width = 120;
            // 
            // protocolCol
            // 
            this.protocolCol.Text = "Protokół";
            this.protocolCol.Width = 120;
            // 
            // fullFileListView
            // 
            this.fullFileListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.processedFiles});
            this.fullFileListView.HideSelection = false;
            this.fullFileListView.Location = new System.Drawing.Point(13, 39);
            this.fullFileListView.Name = "fullFileListView";
            this.fullFileListView.Size = new System.Drawing.Size(775, 129);
            this.fullFileListView.TabIndex = 13;
            this.fullFileListView.UseCompatibleStateImageBehavior = false;
            this.fullFileListView.View = System.Windows.Forms.View.Details;
            this.fullFileListView.VirtualMode = true;
            // 
            // processedFiles
            // 
            this.processedFiles.Text = "Przetworzone pliki";
            this.processedFiles.Width = 600;
            // 
            // processedLinesCountLabel
            // 
            this.processedLinesCountLabel.AutoSize = true;
            this.processedLinesCountLabel.Location = new System.Drawing.Point(12, 434);
            this.processedLinesCountLabel.Name = "processedLinesCountLabel";
            this.processedLinesCountLabel.Size = new System.Drawing.Size(99, 13);
            this.processedLinesCountLabel.TabIndex = 14;
            this.processedLinesCountLabel.Text = "Przetworzone linijki:";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(11, 174);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(681, 23);
            this.progressBar1.TabIndex = 15;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(699, 174);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 16;
            this.cancelButton.Text = "Anuluj";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.processedLinesCountLabel);
            this.Controls.Add(this.fullFileListView);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.openCatalogButton);
            this.Controls.Add(this.aboutProgramButton);
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
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader eventCol;
        private System.Windows.Forms.ColumnHeader dateCol;
        private System.Windows.Forms.ColumnHeader inAddrCol;
        private System.Windows.Forms.ColumnHeader outAddrCol;
        private System.Windows.Forms.ColumnHeader protocolCol;
        private System.Windows.Forms.ColumnHeader timeCol;
        private System.Windows.Forms.ListView fullFileListView;
        private System.Windows.Forms.ColumnHeader processedFiles;
        private System.Windows.Forms.Label processedLinesCountLabel;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button cancelButton;
    }
}

