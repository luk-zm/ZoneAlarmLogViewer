namespace import_danych
{
    partial class aboutProgramForm
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
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.authorLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(13, 13);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(341, 13);
            this.descriptionLabel.TabIndex = 0;
            this.descriptionLabel.Text = "Program służący do eksploracji danych z plików o określonym formacie";
            this.descriptionLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // authorLabel
            // 
            this.authorLabel.AutoSize = true;
            this.authorLabel.Location = new System.Drawing.Point(16, 40);
            this.authorLabel.Name = "authorLabel";
            this.authorLabel.Size = new System.Drawing.Size(109, 13);
            this.authorLabel.TabIndex = 1;
            this.authorLabel.Text = "Autor: Łukasz Zmuda";
            // 
            // aboutProgramForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 76);
            this.Controls.Add(this.authorLabel);
            this.Controls.Add(this.descriptionLabel);
            this.Name = "aboutProgramForm";
            this.Text = "aboutProgramForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Label authorLabel;
    }
}