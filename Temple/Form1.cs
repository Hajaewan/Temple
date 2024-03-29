﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
///////// 220801 20:54 YTJ 해결해야할 사항  1. 좌표의 오차   2. picturebox1 or picruebox2 하나만 열었을 시 해당영역 마우스 오류   3. 축소 시 picture 원 사이즈로의 복원  4.매칭 오류
namespace Temple
{
    public partial class Form1 : Form
    {
        Bitmap origin1;
        Bitmap origin2;
        Bitmap img1_backup;
        Bitmap img2_backup;

        private double zoomRatio1 = 1.0;
        private double zoomRatio2 = 1.0;

        private Form2 ZoomImageRectIndex = null;

        Point pt = new Point(0, 0);
        Point pt2 = new Point(0, 0);
        public Form1()
        {
            InitializeComponent();
        }
        //string file_path = ""; //20220801_09:26 hjw 수정
        private void Btn_Read1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd1 = new OpenFileDialog())
            {
                ofd1.InitialDirectory = Directory.GetCurrentDirectory();

                if (ofd1.ShowDialog() == DialogResult.OK)
                {
                    origin1 = new Bitmap(ofd1.FileName);                            /////////20220801_09:06 hjw 수정
                    Bitmap Copy_origin1 = new Bitmap(origin1.Width, origin1.Height);
                    CopyImage(origin1, ref Copy_origin1);
                    pictureBox1.Image = Copy_origin1;
                    img1_backup = Copy_origin1;
                    //if (pictureBox2.Image == null)                 ///////////220801_19:42 YTJ picture1 OR picture2가 열리지 않았을 경우 해당 영역 위에 마우스가 지나갈시 오류 --> 주석처리하면 해결됨
                    //{
                    //    pictureBox2.Image = new Bitmap(origin1.Width, origin1.Height);
                    //}
                }
            }
        }

        private void Btn_Read2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd2 = new OpenFileDialog())
            {
                ofd2.InitialDirectory = Directory.GetCurrentDirectory();
                if (ofd2.ShowDialog() == DialogResult.OK)
                {
                    //file_path = ofd2.FileName;
                    //string file_name = Path.GetFileName(file_path);
                    //pictureBox2.Load(file_path);

                    origin2 = new Bitmap(ofd2.FileName);                            ////20220801_09:07 hjw 수정
                    Bitmap Copy_origin2 = new Bitmap(origin2.Width, origin2.Height);
                    CopyImage(origin2, ref Copy_origin2);
                    pictureBox2.Image = Copy_origin2;
                    img2_backup = Copy_origin2;
                    //if (pictureBox1.Image == null)         ///////////220801_19:42 YTJ picture1 OR picture2가 열리지 않았을 경우 해당 영역 위에 마우스가 지나갈시 오류 --> 주석처리하면 해결됨
                    //{
                    //    pictureBox1.Image = new Bitmap(origin2.Width, origin2.Height);
                    //}
                }
            }
        }

        private void Btn_Save1_Click(object sender, EventArgs e)
        {
            SaveFileDlg dlg = new SaveFileDlg(img1_backup, (Bitmap)(pictureBox1.Image));
            if (DialogResult.OK == dlg.ShowDialog())
            {
                MessageBox.Show("완료");
            }
        }

        private void Btn_Save2_Click(object sender, EventArgs e)
        {
            SaveFileDlg dlg = new SaveFileDlg(img2_backup, (Bitmap)(pictureBox2.Image));
            if (DialogResult.OK == dlg.ShowDialog())
            {
                MessageBox.Show("완료");
            }
        }

        public void LaplacianFilter(Bitmap bm, byte[] buf)
        {
            byte[] C = new byte[bm.Width * bm.Height];
            byte[] C2 = buf;
            for (int i = 0; i < bm.Height; ++i)
            {
                progressBar1.Value = i + 1;
                for (int j = 0; j < bm.Width; ++j)
                {
                    for (int x = -1; x < 2; ++x)
                    {
                        for (int k = i - 1; k < i + 2; ++k)
                        {
                            if ((j + x >= bm.Height) || (j + x < 0) || (k < 0) || (k >= bm.Height))
                                continue;
                            else
                            {
                                if (((x == -1) && (k == i - 1)) || ((x == -1) && (k == i + 1)) || ((x == 1) && (k == i - 1)) || ((x == 1) && (k == i + 1)))             //0
                                {
                                    C[bm.Height * k + j + x] = (byte)(C[bm.Height * k + j + x] + (C2[bm.Height * i + j]) * 0);
                                }

                                else if (((x == 0) && (k == i - 1)) || ((x == 0) && (k == i + 1)) || ((x == -1) && (k == i)) || ((x == 1) && (k == i)))    //1
                                {
                                    C[bm.Height * k + j + x] = (byte)(C[bm.Height * k + j + x] + (C2[bm.Height * i + j]) * 1);
                                }

                                else if ((x == 0) && (k == i))   // 4
                                {
                                    C[bm.Height * k + j + x] = (byte)(C[bm.Height * k + j + x] + (C2[bm.Height * i + j]) * -4);
                                }
                            }
                        }
                    }
                }
            }

            int m = 0;
            for (int i = 0; i < bm.Height; ++i)
            {
                for (int j = 0; j < bm.Width; ++j)
                {
                    m = (byte)(C[bm.Height * i + j] + 128);
                    Color G = Color.FromArgb(255, m, m, m);
                    bm.SetPixel(i, j, G);
                }
            }
        }

        public void GaussianFilter(Bitmap bm, byte[] buf)
        {
            byte[] C = new byte[bm.Width * bm.Height];
            byte[] C2 = buf;

            for (int i = 0; i < bm.Height; ++i)
            {
                progressBar1.Value = i + 1;
                for (int j = 0; j < bm.Width; ++j)
                {
                    for (int x = -1; x < 2; ++x)
                    {
                        for (int k = i - 1; k < i + 2; ++k)
                        {
                            if ((j + x >= bm.Height) || (j + x < 0) || (k < 0) || (k >= bm.Width))
                                continue;
                            else
                            {
                                if (((x == -1) && (k == i - 1)) || ((x == -1) && (k == i + 1)) || ((x == 1) && (k == i - 1)) || ((x == 1) && (k == i + 1)))             //1
                                {
                                    C[bm.Height * k + j + x] = ((byte)((double)C[bm.Height * k + j + x] + (double)(C2[bm.Height * i + j]) / 16));
                                }

                                else if (((x == 0) && (k == i - 1)) || ((x == 0) && (k == i + 1)) || ((x == -1) && (k == i)) || ((x == 1) && (k == i)))    //2
                                {
                                    C[bm.Height * k + j + x] = ((byte)((double)C[bm.Height * k + j + x] + (double)(C2[bm.Height * i + j]) / 8));
                                }

                                else if ((x == 0) && (k == i))   // 4
                                {
                                    C[bm.Height * k + j + x] = ((byte)((double)C[bm.Height * k + j + x] + (double)(C2[bm.Height * i + j]) / 4));
                                }
                            }
                        }
                    }
                }
            }
            int m = 0;
            for (int i = 0; i < bm.Height; ++i)
            {
                for (int j = 0; j < bm.Width; ++j)
                {
                    m = C[bm.Height * i + j];
                    Color G = Color.FromArgb(255, m, m, m);
                    bm.SetPixel(i, j, G);
                }
            }
        }

        public void MakeBuf(Bitmap bm, byte[] buf)
        {
            for (int i = 0; i < bm.Height; ++i)
                for (int j = 0; j < bm.Width; ++j)
                {
                    buf[i * bm.Height + j] = bm.GetPixel(i, j).R;
                }
        }

        private void CopyImage(Bitmap ori, ref Bitmap copy)
        {
            int w = ori.Width;
            int h = ori.Height;

            copy = ori.Clone(new Rectangle(0, 0, w, h), PixelFormat.Format32bppArgb);

        }

        private void InitProgressBar(int w)
        {
            progressBar1.Maximum = w;
            progressBar1.Value = 0;
        }

        private void Btn_Dilation_Click(object sender, EventArgs e)
        {
            int w = ((Bitmap)pictureBox1.Image).Width;
            int h = ((Bitmap)pictureBox1.Image).Height;
            InitProgressBar(w);
            Bitmap Copyimg = new Bitmap(w, h);
            CopyImage((Bitmap)pictureBox1.Image, ref Copyimg);
            for (int i = 0; i < w * h; ++i)
            {
                progressBar1.Value = (i / h) + 1;
                byte max = 0;
                for (int py = -1; py <= 1; ++py)
                {
                    for (int px = -1; px <= 1; ++px)
                    {
                        int posi = i + px + (h * py);

                        if (posi < 0 || posi >= w * h)
                        {
                            continue;
                        }
                        if (i % w == 0 && px < 0 || i % w == w - 1 && px > 0)
                        {
                            continue;
                        }
                        if (((Bitmap)pictureBox1.Image).GetPixel(posi % w, posi / w).R > max)
                        {
                            max = ((Bitmap)pictureBox1.Image).GetPixel(posi % w, posi / w).R;
                        }
                    }
                }

                (Copyimg).SetPixel(i % w, i / w, Color.FromArgb(max, max, max));
            }
            pt2.X = 0;
            pt2.Y = 0;
            pictureBox2.Image = Copyimg;
            pictureBox2.Refresh();
            img2_backup = Copyimg;
        }

        private void Btn_Erosion_Click(object sender, EventArgs e)
        {
            int w = ((Bitmap)pictureBox1.Image).Width;
            int h = ((Bitmap)pictureBox1.Image).Height;
            InitProgressBar(w);
            Bitmap Copyimg = new Bitmap(w, h);
            CopyImage((Bitmap)pictureBox1.Image, ref Copyimg);
            for (int i = 0; i < w * h; ++i)
            {
                progressBar1.Value = (i / h) + 1;
                byte min = 255;
                for (int py = -1; py <= 1; ++py)
                {
                    for (int px = -1; px <= 1; ++px)
                    {
                        int posi = i + px + (h * py);

                        if (posi < 0 || posi >= w * h)
                        {
                            continue;
                        }
                        if (i % w == 0 && px < 0 || i % w == w - 1 && px > 0)
                        {
                            continue;
                        }
                        if (((Bitmap)pictureBox1.Image).GetPixel(posi % w, posi / w).R < min)
                        {
                            min = ((Bitmap)pictureBox1.Image).GetPixel(posi % w, posi / w).R;
                        }
                    }
                }
                 (Copyimg).SetPixel(i % w, i / w, Color.FromArgb(min, min, min));
            }
            pt2.X = 0;
            pt2.Y = 0;
            pictureBox2.Image = Copyimg;
            pictureBox2.Refresh();
            img2_backup = Copyimg;

        }

        private void Btn_Guassian_Click(object sender, EventArgs e)
        {
            //Bitmap originImage = (Bitmap)pictureBox1.Image;  
            //Bitmap originImage_copy = CopyImage(originImage);   
            //20220801_09:21 hjw 수정

            int w = pictureBox1.Image.Width;
            int h = pictureBox1.Image.Height;
            Bitmap Copyimg = new Bitmap(w, h);
            CopyImage((Bitmap)pictureBox1.Image, ref Copyimg);
            byte[] C2 = new byte[w * h];
            InitProgressBar(w);
            MakeBuf((Bitmap)pictureBox1.Image, C2);
            GaussianFilter((Bitmap)Copyimg, C2);
            pt2.X = 0;
            pt2.Y = 0;
            pictureBox2.Image = (Bitmap)Copyimg;
            img2_backup = Copyimg;


        }

        private void Btn_Laplacian_Click(object sender, EventArgs e)
        {
            //Bitmap originImage = (Bitmap)pictureBox1.Image;
            //Bitmap originImage_copy = CopyImage(originImage);
            //20220801_09:21 hjw 수정

            int w = pictureBox1.Image.Width;
            int h = pictureBox1.Image.Height;
            Bitmap Copyimg = new Bitmap(w, h);
            CopyImage((Bitmap)pictureBox1.Image, ref Copyimg);
            byte[] C2 = new byte[w * h];
            InitProgressBar(w);

            MakeBuf((Bitmap)pictureBox1.Image, C2);
            LaplacianFilter((Bitmap)Copyimg, C2);
            pt2.X = 0;
            pt2.Y = 0;
            pictureBox2.Image = (Bitmap)Copyimg;
            img2_backup = Copyimg;

        }

        private void Btn_Equalization_Click(object sender, EventArgs e)
        {
            int row = ((Bitmap)pictureBox1.Image).Height;
            int col = ((Bitmap)pictureBox1.Image).Width;
            InitProgressBar(col);

            Bitmap bmp = new Bitmap(row, col);
            double[] histogram = new double[256];
            int[] sum = new int[256];
            int[] New = new int[256];
            double size = row * col;
            for (int i = 0; i < row; ++i)
            {
                for (int j = 0; j < col; ++j)
                {
                    histogram[((Bitmap)pictureBox1.Image).GetPixel(i, j).R] = histogram[((Bitmap)pictureBox1.Image).GetPixel(i, j).R] + 1;
                }
            }

            for (int i = 0; i < 256; ++i)
            {
                if (i == 0)
                {
                    sum[i] = (int)(histogram[i]);
                }
                else
                {
                    sum[i] = (int)(sum[i - 1] + histogram[i]);
                }
            }
            /*double scal = 255.0 / size;*/
            for (int i = 0; i < 256; ++i)
            {
                New[i] = (int)(sum[i] * (255.0 / size));
            }
            for (int i = 0; i < row; ++i)
            {
                progressBar1.Value = i + 1;
                for (int j = 0; j < col; ++j)
                {
                    byte color = (byte)New[((Bitmap)pictureBox1.Image).GetPixel(i, j).R];
                    bmp.SetPixel(i, j, Color.FromArgb(color, color, color));
                }
            }
            pt2.X = 0;
            pt2.Y = 0;
            pictureBox2.Image = bmp;
            img2_backup = bmp;

        }

        private void Btn_Otsu_Click(object sender, EventArgs e)
        {
            int row = pictureBox1.Image.Height;
            int col = pictureBox1.Image.Width;
            InitProgressBar(col);
            //filename = "Otsu.bmp";
            Bitmap bmp = new Bitmap(row, col);
            double[] histogram = new double[256];
            double sum = 0;
            double P1 = 0;
            double P2 = 0;
            double Mg = 0;
            double M1 = 0;
            double M2 = 0;
            double MS = 0;
            double Ob2 = 0;
            double max = 0;
            int t = 0;
            double size = row * col;

            for (int i = 0; i < row; ++i)
            {
                for (int j = 0; j < col; ++j)
                {
                    histogram[((Bitmap)pictureBox1.Image).GetPixel(i, j).R]++;
                }
            }

            for (int i = 0; i < 256; ++i)
            {
                Mg += histogram[i] * i;
            }
            for (int i = 0; i < 256; ++i)
            {
                sum += histogram[i];
                MS += histogram[i] * i;
                P1 = sum / size;
                P2 = 1 - P1;
                M1 = MS / sum;
                M2 = (Mg - MS) / (size - sum);
                Ob2 = (P1 * (M1 - (Mg / size)) * (M1 - (Mg / size))) + (P2 * (M2 - (Mg / size)) * (M2 - (Mg / size)));
                if (max < Ob2)
                {
                    max = Ob2;
                    t = i;
                }
            }
            for (int i = 0; i < row; ++i)
            {
                progressBar1.Value = i + 1;

                for (int j = 0; j < col; ++j)
                {
                    if (((Bitmap)pictureBox1.Image).GetPixel(i, j).R >= t)
                    {
                        bmp.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                    }
                    else if (((Bitmap)pictureBox1.Image).GetPixel(i, j).R < t)
                    {
                        bmp.SetPixel(i, j, Color.FromArgb(0, 0, 0));

                    }
                }
            }
            pt2.X = 0;
            pt2.Y = 0;
            pictureBox2.Image = bmp;
            img2_backup = bmp;

        }

        private void Btn_Matching_Click(object sender, EventArgs e)
        {
            int min = 2100000000;
            int Templatewidth = ((Bitmap)pictureBox1.Image).Width;
            int Templateheight = ((Bitmap)pictureBox1.Image).Height;
            int mewidth = ((Bitmap)pictureBox2.Image).Height;
            int meheight = ((Bitmap)pictureBox2.Image).Width;
            InitProgressBar(mewidth - Templatewidth);

            Bitmap bmp = new Bitmap(mewidth, meheight);
            byte[] data = new byte[mewidth * meheight];
            for (int i = 0; i < mewidth; i++)
            {
                for (int j = 0; j < meheight; ++j)
                {
                    bmp.SetPixel(i, j, (origin2).GetPixel(i, j));
                    data[j * mewidth + i] = (origin2).GetPixel(i, j).R;
                }
            }

            byte[] Temp = new byte[Templatewidth * Templateheight];
            for (int i = 0; i < Templatewidth; i++)
            {
                for (int j = 0; j < Templateheight; ++j)
                {
                    Temp[j * Templatewidth + i] = ((Bitmap)pictureBox1.Image).GetPixel(i, j).R;
                }
            }
            int sum = 0;
            int row = 0;
            int col = 0;

            for (int y = 0; y < mewidth - Templatewidth; ++y)
            {
                progressBar1.Value = y + 1;
                for (int x = 0; x < meheight - Templateheight; ++x)
                {
                    for (int i = 0; i < Templatewidth; ++i)
                    {
                        for (int j = 0; j < Templateheight; ++j)
                        {
                            sum += (data[(x + j) * mewidth + (y + i)] - Temp[j * Templatewidth + i]) * (data[(x + j) * mewidth + (y + i)] - Temp[j * Templatewidth + i]);
                        }
                    }
                    if (sum < min)
                    {
                        min = sum;
                        row = x;
                        col = y;
                    }
                    sum = 0;
                }
            }
            MessageBox.Show($"{row},{col}");
            for (int y = row; y < row + Templateheight; ++y)
            {
                for (int x = col; x < col + Templatewidth; ++x)
                {
                    if (y == row || y == (row + Templateheight - 1) || x == col || x == (col + Templatewidth - 1))
                        bmp.SetPixel((x), (y), Color.FromArgb(255, 0, 0));
                }
            }
            pt2.X = 0;
            pt2.Y = 0;
            pictureBox2.Image = bmp;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                int newX = (int)(((double)e.X * pictureBox1.Image.Width / (double)pictureBox1.Width) + pt.X);
                int newY = (int)(((double)e.Y * pictureBox1.Image.Height / (double)pictureBox1.Height) + pt.Y);

                Bitmap RectIndex = img1_backup.Clone(new Rectangle(0, 0, img1_backup.Width, img1_backup.Height), PixelFormat.Format32bppArgb);
                Color G = Color.FromArgb(255, 0, 0);

                if (ZoomImageRectIndex == null || ZoomImageRectIndex.IsDisposed)
                {

                    ZoomImageRectIndex = new Form2(RectIndex);
                    ZoomImageRectIndex.Owner = this;
                    ZoomImageRectIndex.Show();
                }
                else if (ZoomImageRectIndex != null) // Picture1 2번째 누를때 부터 
                {
                    if (e.Button == MouseButtons.Left)     // 확대
                    {
                        zoomRatio1 *= 0.80;
                        int zoomSize = (int)Math.Round(zoomRatio1 * pictureBox1.Image.Width);     // 수정 TJ
                        if (zoomSize > img1_backup.Width)       //수정TJ
                        {
                            zoomSize = img1_backup.Width;         //수정TJ
                        }

                        Bitmap zoomImage = new Bitmap(zoomSize, zoomSize);

                        if (newX + zoomSize / 2 > img1_backup.Width)
                            newX = newX - Math.Abs(zoomSize / 2 - (img1_backup.Width - newX));

                        if (newY + zoomSize / 2 > img1_backup.Height)
                            newY = newY - Math.Abs(zoomSize / 2 - (img1_backup.Height - newY));

                        if (newX - zoomSize / 2 < 0)
                            newX = zoomSize / 2;

                        if (newY - zoomSize / 2 < 0)
                            newY = zoomSize / 2;
                        pt.X = newX - (zoomSize / 2);
                        pt.Y = newY - (zoomSize / 2);
                        for (int i = -zoomSize / 2; i < zoomSize / 2; ++i)
                            for (int j = -zoomSize / 2; j < zoomSize / 2; ++j)
                            {

                                zoomImage.SetPixel(zoomSize / 2 + i, zoomSize / 2 + j, img1_backup.GetPixel((int)(newX + (i)), (int)(newY + (j)))); // 줌이미지

                                if ((-zoomSize / 2 <= i && i < zoomSize / 2) && (j == -zoomSize / 2 || j == zoomSize / 2 - 1) || (-zoomSize / 2 <= j && j < zoomSize / 2) && (i == -zoomSize / 2 || i == zoomSize / 2 - 1))
                                {
                                    RectIndex.SetPixel((int)(newX + (i)), (int)(newY + (j)), G);   // 원본에 사각 인덱스 표시
                                }

                            }
                        pictureBox1.Image = zoomImage;
                        ZoomImageRectIndex.Bitmap = RectIndex;

                    }
                    else if (e.Button == MouseButtons.Right)     // 축소
                    {
                        Bitmap zoomImage = null;
                        zoomRatio1 *= 1.3;
                        int zoomSize = (int)(zoomRatio1 * pictureBox1.Image.Width);   //수정TJ
                        if (zoomSize > img1_backup.Width)     //수정TJ
                        {
                            zoomSize = img1_backup.Width;  //수정TJ
                        }
                        if (zoomSize == img1_backup.Width && zoomSize == img1_backup.Height)
                        {
                            zoomImage = new Bitmap(img1_backup.Width, img1_backup.Height);
                            for (int i = 0; i < img1_backup.Height; i++)
                            {
                                for (int j = 0; j < img1_backup.Width; j++)
                                {
                                    if (i == 0 || j == 0 || i == img1_backup.Height - 1 || j == img1_backup.Width - 1)
                                    {
                                        zoomImage.SetPixel(i, j, G);
                                    }
                                    else
                                    {
                                        zoomImage.SetPixel(i, j, img1_backup.GetPixel(i, j));
                                    }
                                }

                            }

                            pictureBox1.Image = img1_backup;
                            ZoomImageRectIndex.Bitmap = zoomImage;
                            pt.X = 0;
                            pt.Y = 0;
                            newX = (int)(((double)e.X * pictureBox1.Image.Width / (double)pictureBox1.Width) + pt.X);
                            newY = (int)(((double)e.Y * pictureBox1.Image.Height / (double)pictureBox1.Height) + pt.Y);
                        }
                        else
                        {
                            zoomImage = new Bitmap(zoomSize, zoomSize);


                            if (newX + zoomSize / 2 > img1_backup.Width)
                                newX = newX - Math.Abs(zoomSize / 2 - (img1_backup.Width - newX));

                            if (newY + zoomSize / 2 > img1_backup.Height)
                                newY = newY - Math.Abs(zoomSize / 2 - (img1_backup.Height - newY));

                            if (newX - zoomSize / 2 < 0)
                                newX = zoomSize / 2;

                            if (newY - zoomSize / 2 < 0)
                                newY = zoomSize / 2;
                            pt.X = newX - (zoomSize / 2);
                            pt.Y = newY - (zoomSize / 2);
                            for (int i = -zoomSize / 2; i < zoomSize / 2; ++i)
                                for (int j = -zoomSize / 2; j < zoomSize / 2; ++j)
                                {
                                    zoomImage.SetPixel(zoomSize / 2 + i, zoomSize / 2 + j, img1_backup.GetPixel((int)(newX + (i)), (int)(newY + (j)))); // 줌이미지

                                    if ((-zoomSize / 2 <= i && i < zoomSize / 2) && (j == -zoomSize / 2 || j == zoomSize / 2 - 1) || (-zoomSize / 2 <= j && j < zoomSize / 2) && (i == -zoomSize / 2 || i == zoomSize / 2 - 1))
                                    {
                                        RectIndex.SetPixel((int)(newX + (i)), (int)(newY + (j)), G);   // 원본에 사각 인덱스 표시
                                    }

                                }
                            pictureBox1.Image = zoomImage;
                            ZoomImageRectIndex.Bitmap = RectIndex;
                        }
                    }
                }
            }

            else
                MessageBox.Show("이미지 로드해주세요");
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                int newX = (int)(((double)e.X * pictureBox2.Image.Width / (double)pictureBox2.Width) + pt2.X);
                int newY = (int)(((double)e.Y * pictureBox2.Image.Height / (double)pictureBox2.Height) + pt2.Y);

                Bitmap RectIndex = img2_backup.Clone(new Rectangle(0, 0, img2_backup.Width, img2_backup.Height), PixelFormat.Format32bppArgb);
                Color G = Color.FromArgb(255, 0, 0);

                if (ZoomImageRectIndex == null || ZoomImageRectIndex.IsDisposed)
                {

                    ZoomImageRectIndex = new Form2(RectIndex);
                    ZoomImageRectIndex.Owner = this;
                    ZoomImageRectIndex.Show();
                }
                else if (ZoomImageRectIndex != null) // Picture1 2번째 누를때 부터 
                {
                    if (e.Button == MouseButtons.Left)     // 확대
                    {
                        zoomRatio2 *= 0.8;
                        int zoomSize = (int)(zoomRatio2 * pictureBox2.Image.Height);    //수정 TJ
                        if (zoomSize > img2_backup.Width)                    //수정TJ
                        {
                            zoomSize = img2_backup.Width;
                        }
                        Bitmap zoomImage = new Bitmap(zoomSize, zoomSize);

                        if (newX + zoomSize / 2 > img2_backup.Width)
                            newX = newX - Math.Abs(zoomSize / 2 - (img2_backup.Width - newX));

                        if (newY + zoomSize / 2 > img2_backup.Height)
                            newY = newY - Math.Abs(zoomSize / 2 - (img2_backup.Height - newY));

                        if (newX - zoomSize / 2 < 0)
                            newX = zoomSize / 2;

                        if (newY - zoomSize / 2 < 0)
                            newY = zoomSize / 2;
                        pt2.X = newX - (zoomSize / 2);
                        pt2.Y = newY - (zoomSize / 2);
                        for (int i = -zoomSize / 2; i < zoomSize / 2; ++i)
                            for (int j = -zoomSize / 2; j < zoomSize / 2; ++j)
                            {
                                zoomImage.SetPixel(zoomSize / 2 + i, zoomSize / 2 + j, img2_backup.GetPixel((int)(newX + i), (int)(newY + j))); // 줌이미지

                                if ((-zoomSize / 2 <= i && i < zoomSize / 2) && (j == -zoomSize / 2 || j == zoomSize / 2 - 1) || (-zoomSize / 2 <= j && j < zoomSize / 2) && (i == -zoomSize / 2 || i == zoomSize / 2 - 1))
                                {
                                    RectIndex.SetPixel((int)(newX + i), (int)(newY + j), G);   // 원본에 사각 인덱스 표시
                                }
                            }
                        pictureBox2.Image = zoomImage;
                        ZoomImageRectIndex.Bitmap = RectIndex;

                    }
                    else if (e.Button == MouseButtons.Right)     // 축소
                    {
                        Bitmap zoomImage = null;
                        zoomRatio2 *= 1.3;
                        int zoomSize = (int)Math.Round(zoomRatio2 * 260);       //수정TJ
                        if (zoomSize > img2_backup.Width)                    //수정TJ
                        {
                            zoomSize = img2_backup.Width;
                        }
                        if (zoomSize == img2_backup.Width && zoomSize == img2_backup.Height)
                        {
                            zoomImage = new Bitmap(img2_backup.Width, img2_backup.Height);
                            for (int i = 0; i < img2_backup.Height; i++)
                            {
                                for (int j = 0; j < img2_backup.Width; j++)
                                {
                                    if (i == 0 || j == 0 || i == img2_backup.Height - 1 || j == img2_backup.Width - 1)
                                    {
                                        zoomImage.SetPixel(i, j, G);
                                    }
                                    else
                                    {
                                        zoomImage.SetPixel(i, j, img2_backup.GetPixel(i, j));
                                    }
                                }
                            }
                            pictureBox2.Image = img2_backup;
                            ZoomImageRectIndex.Bitmap = zoomImage;
                            pt2.X = 0;
                            pt2.Y = 0;
                            newX = (int)(((double)e.X * pictureBox2.Image.Width / (double)pictureBox2.Width) + pt2.X);
                            newY = (int)(((double)e.Y * pictureBox2.Image.Height / (double)pictureBox2.Height) + pt2.Y);
                        }
                        else
                        {
                            zoomImage = new Bitmap(zoomSize, zoomSize);

                            if (newX + zoomSize / 2 > img2_backup.Width)
                                newX = newX - Math.Abs(zoomSize / 2 - (img2_backup.Width - newX));

                            if (newY + zoomSize / 2 > img2_backup.Height)
                                newY = newY - Math.Abs(zoomSize / 2 - (img2_backup.Height - newY));

                            if (newX - zoomSize / 2 < 0)
                                newX = zoomSize / 2;

                            if (newY - zoomSize / 2 < 0)
                                newY = zoomSize / 2;
                            pt2.X = newX - (zoomSize / 2);
                            pt2.Y = newY - (zoomSize / 2);
                            for (int i = -zoomSize / 2; i < zoomSize / 2; ++i)
                                for (int j = -zoomSize / 2; j < zoomSize / 2; ++j)
                                {
                                    zoomImage.SetPixel(zoomSize / 2 + i, zoomSize / 2 + j, img2_backup.GetPixel((int)(newX + i), (int)(newY + j))); // 줌이미지

                                    if ((-zoomSize / 2 <= i && i < zoomSize / 2) && (j == -zoomSize / 2 || j == zoomSize / 2 - 1) || (-zoomSize / 2 <= j && j < zoomSize / 2) && (i == -zoomSize / 2 || i == zoomSize / 2 - 1))
                                    {
                                        RectIndex.SetPixel((int)(newX + i), (int)(newY + j), G);   // 원본에 사각 인덱스 표시
                                    }

                                }
                            pictureBox2.Image = zoomImage;
                            ZoomImageRectIndex.Bitmap = RectIndex;
                        }
                    }
                }
            }
            else
                MessageBox.Show("이미지 로드해주세요");
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                double ratio = (double)pictureBox1.Image.Width / (double)260;

                int newX = (int)(((double)e.X * pictureBox1.Image.Width / (double)pictureBox1.Width) + pt.X);
                int newY = (int)(((double)e.Y * pictureBox1.Image.Height / (double)pictureBox1.Height) + pt.Y);

                if (newY < 512 && newX < 512 && newY > 0 && newX > 0)                         ////////220801_19:56 YTJ 예외처리 사항 추가
                {
                    label1.Text = $" x : {newX}";
                    label2.Text = $" Y : {newY}";
                    Bitmap zoomImage = new Bitmap(40, 40);

                    label3.Text = $"ColorIndex : {img1_backup.GetPixel(newX, newY).R}";

                    for (int i = -20; i < 20; ++i)
                        for (int j = -20; j < 20; ++j)
                        {
                            if ((newX + i) < 0 || (newY + j) < 0 || (newY + j) >= (img1_backup).Width || (newX + i) >= (img1_backup).Height)
                            {
                                continue;
                            }
                            else
                            {
                                zoomImage.SetPixel(20 + i, 20 + j, (img1_backup).GetPixel(newX + i, newY + j));
                            }
                        }
                    pictureBox3.Image = zoomImage;
                }
            }
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                double ratio = (double)pictureBox2.Width / (double)pictureBox2.Image.Width;
                int newX = (int)(((double)e.X * pictureBox2.Image.Width / (double)pictureBox2.Width) + pt2.X);  // ROUND 제거  16:55 TJY
                int newY = (int)(((double)e.Y * pictureBox2.Image.Height / (double)pictureBox2.Height) + pt2.Y);

                if (newY < 512 && newX < 512 && newY > 0 && newX > 0)                  ////////220801_19:56 YTJ 예외처리 사항 추가
                {
                    label1.Text = $" x : {newX}";
                    label2.Text = $" Y : {newY}";
                    Bitmap zoomImage = new Bitmap(40, 40);


                    for (int i = -20; i < 20; ++i)
                        for (int j = -20; j < 20; ++j)
                        {
                            if ((newX + i) < 0 || (newY + j) < 0 || (newY + j) >= (img2_backup).Width || (newX + i) >= (img2_backup).Height)
                            {
                                continue;
                            }
                            else
                            {
                                label3.Text = $"ColorIndex : {img2_backup.GetPixel(newX, newY).R}";              //수정 15:53 tj
                                zoomImage.SetPixel(20 + i, 20 + j, img2_backup.GetPixel(newX + i, newY + j));
                            }
                        }
                    pictureBox3.Image = zoomImage;
                }
            }
        }
    }
}