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
            this.txtResult = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btswitch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btOCR
            // 
            this.btOCR.Location = new System.Drawing.Point(63, 76);
            this.btOCR.Name = "btOCR";
            this.btOCR.Size = new System.Drawing.Size(75, 23);
            this.btOCR.TabIndex = 7;
            this.btOCR.Text = "button1";
            this.btOCR.UseVisualStyleBackColor = true;
            this.btOCR.Click += new System.EventHandler(this.btOCR_Click);
            // 
            // txtResult
            // 
            this.txtResult.AllowDrop = true;
            this.txtResult.Location = new System.Drawing.Point(284, 67);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResult.Size = new System.Drawing.Size(193, 181);
            this.txtResult.TabIndex = 8;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // btswitch
            // 
            this.btswitch.Location = new System.Drawing.Point(63, 154);
            this.btswitch.Name = "btswitch";
            this.btswitch.Size = new System.Drawing.Size(75, 23);
            this.btswitch.TabIndex = 9;
            this.btswitch.Text = "switch";
            this.btswitch.UseVisualStyleBackColor = true;
            this.btswitch.Click += new System.EventHandler(this.btswitch_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 314);
            this.Controls.Add(this.btswitch);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btOCR);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btOCR;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btswitch;
    }
}