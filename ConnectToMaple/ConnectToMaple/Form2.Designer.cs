namespace ConnectToMaple
{
    partial class Form2
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
            this.btOCR = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btOCR
            // 
            this.btOCR.Cursor = System.Windows.Forms.Cursors.Default;
            this.btOCR.Location = new System.Drawing.Point(72, 83);
            this.btOCR.Name = "btOCR";
            this.btOCR.Size = new System.Drawing.Size(75, 23);
            this.btOCR.TabIndex = 7;
            this.btOCR.Text = "Mở File Đề";
            this.btOCR.UseVisualStyleBackColor = true;
            this.btOCR.Click += new System.EventHandler(this.btOCR_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 198);
            this.Controls.Add(this.btOCR);
            this.Name = "Form2";
            this.Text = "Nhập Đề";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btOCR;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}