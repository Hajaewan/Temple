﻿namespace Temple
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.Btn_Read1 = new System.Windows.Forms.Button();
            this.Btn_Save1 = new System.Windows.Forms.Button();
            this.Btn_Read2 = new System.Windows.Forms.Button();
            this.Btn_Save2 = new System.Windows.Forms.Button();
            this.Btn_Dilation = new System.Windows.Forms.Button();
            this.Btn_Erosion = new System.Windows.Forms.Button();
            this.Btn_Equalization = new System.Windows.Forms.Button();
            this.Btn_Otsu = new System.Windows.Forms.Button();
            this.Btn_Guassian = new System.Windows.Forms.Button();
            this.Btn_Laplacian = new System.Windows.Forms.Button();
            this.Btn_Matching = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(85, 53);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(260, 260);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(462, 53);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(260, 260);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseDown);
            this.pictureBox2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseMove);
            // 
            // Btn_Read1
            // 
            this.Btn_Read1.Location = new System.Drawing.Point(95, 337);
            this.Btn_Read1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Read1.Name = "Btn_Read1";
            this.Btn_Read1.Size = new System.Drawing.Size(66, 18);
            this.Btn_Read1.TabIndex = 2;
            this.Btn_Read1.Text = "파일열기";
            this.Btn_Read1.UseVisualStyleBackColor = true;
            this.Btn_Read1.Click += new System.EventHandler(this.Btn_Read1_Click);
            // 
            // Btn_Save1
            // 
            this.Btn_Save1.Location = new System.Drawing.Point(247, 337);
            this.Btn_Save1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Save1.Name = "Btn_Save1";
            this.Btn_Save1.Size = new System.Drawing.Size(66, 18);
            this.Btn_Save1.TabIndex = 3;
            this.Btn_Save1.Text = "파일저장";
            this.Btn_Save1.Click += new System.EventHandler(this.Btn_Save1_Click);
            // 
            // Btn_Read2
            // 
            this.Btn_Read2.Location = new System.Drawing.Point(462, 337);
            this.Btn_Read2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Read2.Name = "Btn_Read2";
            this.Btn_Read2.Size = new System.Drawing.Size(66, 18);
            this.Btn_Read2.TabIndex = 4;
            this.Btn_Read2.Text = "파일열기";
            this.Btn_Read2.UseVisualStyleBackColor = true;
            this.Btn_Read2.Click += new System.EventHandler(this.Btn_Read2_Click);
            // 
            // Btn_Save2
            // 
            this.Btn_Save2.Location = new System.Drawing.Point(621, 337);
            this.Btn_Save2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Save2.Name = "Btn_Save2";
            this.Btn_Save2.Size = new System.Drawing.Size(66, 18);
            this.Btn_Save2.TabIndex = 5;
            this.Btn_Save2.Text = "파일저장";
            this.Btn_Save2.Click += new System.EventHandler(this.Btn_Save2_Click);
            // 
            // Btn_Dilation
            // 
            this.Btn_Dilation.Location = new System.Drawing.Point(749, 66);
            this.Btn_Dilation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Dilation.Name = "Btn_Dilation";
            this.Btn_Dilation.Size = new System.Drawing.Size(90, 18);
            this.Btn_Dilation.TabIndex = 6;
            this.Btn_Dilation.Text = "Dilation";
            this.Btn_Dilation.UseVisualStyleBackColor = true;
            this.Btn_Dilation.Click += new System.EventHandler(this.Btn_Dilation_Click);
            // 
            // Btn_Erosion
            // 
            this.Btn_Erosion.Location = new System.Drawing.Point(860, 66);
            this.Btn_Erosion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Erosion.Name = "Btn_Erosion";
            this.Btn_Erosion.Size = new System.Drawing.Size(90, 18);
            this.Btn_Erosion.TabIndex = 7;
            this.Btn_Erosion.Text = "Erosion";
            this.Btn_Erosion.UseVisualStyleBackColor = true;
            this.Btn_Erosion.Click += new System.EventHandler(this.Btn_Erosion_Click);
            // 
            // Btn_Equalization
            // 
            this.Btn_Equalization.Location = new System.Drawing.Point(749, 106);
            this.Btn_Equalization.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Equalization.Name = "Btn_Equalization";
            this.Btn_Equalization.Size = new System.Drawing.Size(90, 18);
            this.Btn_Equalization.TabIndex = 8;
            this.Btn_Equalization.Text = "Equalization";
            this.Btn_Equalization.UseVisualStyleBackColor = true;
            this.Btn_Equalization.Click += new System.EventHandler(this.Btn_Equalization_Click);
            // 
            // Btn_Otsu
            // 
            this.Btn_Otsu.Location = new System.Drawing.Point(860, 106);
            this.Btn_Otsu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Otsu.Name = "Btn_Otsu";
            this.Btn_Otsu.Size = new System.Drawing.Size(90, 18);
            this.Btn_Otsu.TabIndex = 9;
            this.Btn_Otsu.Text = "Otsu";
            this.Btn_Otsu.UseVisualStyleBackColor = true;
            this.Btn_Otsu.Click += new System.EventHandler(this.Btn_Otsu_Click);
            // 
            // Btn_Guassian
            // 
            this.Btn_Guassian.Location = new System.Drawing.Point(749, 149);
            this.Btn_Guassian.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Guassian.Name = "Btn_Guassian";
            this.Btn_Guassian.Size = new System.Drawing.Size(90, 18);
            this.Btn_Guassian.TabIndex = 10;
            this.Btn_Guassian.Text = "Guassian";
            this.Btn_Guassian.UseVisualStyleBackColor = true;
            this.Btn_Guassian.Click += new System.EventHandler(this.Btn_Guassian_Click);
            // 
            // Btn_Laplacian
            // 
            this.Btn_Laplacian.Location = new System.Drawing.Point(860, 149);
            this.Btn_Laplacian.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Laplacian.Name = "Btn_Laplacian";
            this.Btn_Laplacian.Size = new System.Drawing.Size(90, 18);
            this.Btn_Laplacian.TabIndex = 11;
            this.Btn_Laplacian.Text = "Laplacian";
            this.Btn_Laplacian.UseVisualStyleBackColor = true;
            this.Btn_Laplacian.Click += new System.EventHandler(this.Btn_Laplacian_Click);
            // 
            // Btn_Matching
            // 
            this.Btn_Matching.Location = new System.Drawing.Point(749, 192);
            this.Btn_Matching.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Matching.Name = "Btn_Matching";
            this.Btn_Matching.Size = new System.Drawing.Size(90, 18);
            this.Btn_Matching.TabIndex = 12;
            this.Btn_Matching.Text = "Matching";
            this.Btn_Matching.UseVisualStyleBackColor = true;
            this.Btn_Matching.Click += new System.EventHandler(this.Btn_Matching_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(765, 233);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(150, 150);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 14;
            this.pictureBox3.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(763, 414);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "DX:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(831, 414);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "DY:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(890, 414);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "Color";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(95, 391);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(592, 17);
            this.progressBar1.TabIndex = 18;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 435);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.Btn_Matching);
            this.Controls.Add(this.Btn_Laplacian);
            this.Controls.Add(this.Btn_Guassian);
            this.Controls.Add(this.Btn_Otsu);
            this.Controls.Add(this.Btn_Equalization);
            this.Controls.Add(this.Btn_Erosion);
            this.Controls.Add(this.Btn_Dilation);
            this.Controls.Add(this.Btn_Save2);
            this.Controls.Add(this.Btn_Read2);
            this.Controls.Add(this.Btn_Save1);
            this.Controls.Add(this.Btn_Read1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button Btn_Read1;
        private System.Windows.Forms.Button Btn_Save1;
        private System.Windows.Forms.Button Btn_Read2;
        private System.Windows.Forms.Button Btn_Save2;
        private System.Windows.Forms.Button Btn_Dilation;
        private System.Windows.Forms.Button Btn_Erosion;
        private System.Windows.Forms.Button Btn_Equalization;
        private System.Windows.Forms.Button Btn_Otsu;
        private System.Windows.Forms.Button Btn_Guassian;
        private System.Windows.Forms.Button Btn_Laplacian;
        private System.Windows.Forms.Button Btn_Matching;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

