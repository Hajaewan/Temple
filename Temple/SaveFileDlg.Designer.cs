namespace Temple
{
    partial class SaveFileDlg
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
            this.Btn_SavePart = new System.Windows.Forms.Button();
            this.Btn_SaveOrigin = new System.Windows.Forms.Button();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_SavePart
            // 
            this.Btn_SavePart.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Btn_SavePart.Location = new System.Drawing.Point(199, 108);
            this.Btn_SavePart.Name = "Btn_SavePart";
            this.Btn_SavePart.Size = new System.Drawing.Size(150, 25);
            this.Btn_SavePart.TabIndex = 1;
            this.Btn_SavePart.Text = "부분 이미지 저장";
            this.Btn_SavePart.UseVisualStyleBackColor = true;
            this.Btn_SavePart.Click += new System.EventHandler(this.Btn_SavePart_Click);
            // 
            // Btn_SaveOrigin
            // 
            this.Btn_SaveOrigin.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Btn_SaveOrigin.Location = new System.Drawing.Point(27, 108);
            this.Btn_SaveOrigin.Name = "Btn_SaveOrigin";
            this.Btn_SaveOrigin.Size = new System.Drawing.Size(150, 25);
            this.Btn_SaveOrigin.TabIndex = 2;
            this.Btn_SaveOrigin.Text = "원본 이미지 저장";
            this.Btn_SaveOrigin.UseVisualStyleBackColor = true;
            this.Btn_SaveOrigin.Click += new System.EventHandler(this.Btn_SaveOrigin_Click);
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Btn_Cancel.Location = new System.Drawing.Point(370, 110);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Btn_Cancel.TabIndex = 2;
            this.Btn_Cancel.Text = "취소";
            this.Btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // SaveFileDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 255);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.Btn_SaveOrigin);
            this.Controls.Add(this.Btn_SavePart);
            this.Name = "SaveFileDlg";
            this.Text = "FileSaveDlg";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button Btn_SavePart;
        private System.Windows.Forms.Button Btn_SaveOrigin;
        private System.Windows.Forms.Button Btn_Cancel;
    }
}