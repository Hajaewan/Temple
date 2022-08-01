using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Temple
{
    public partial class Form1 : Form
    {
        Bitmap origin1;
        Bitmap origin2;
        Bitmap img1_backup;
        Bitmap img2_backup;

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
                    //file_path = ofd1.FileName;
                    //string file_name = Path.GetFileName(file_path);
                    //pictureBox1.Load(file_path);
                    //Bitmap originImage = (Bitmap)pictureBox1.Image;
                    //Bitmap originImage_copy = CopyImage(originImage);
                    //if (pictureBox2.Image == null)
                    //{
                    //    pictureBox2.Image = new Bitmap(originImage.Width, originImage.Height);
                    //}

                    origin1 = new Bitmap(ofd1.FileName);                            /////////20220801_09:06 hjw 수정
                    Bitmap Copy_origin1 = new Bitmap(origin1.Width, origin1.Height);
                    CopyImage(origin1,ref Copy_origin1);
                    pictureBox1.Image = Copy_origin1;
                    img1_backup = Copy_origin1;
                    if (pictureBox2.Image == null)
                    {
                        pictureBox2.Image = new Bitmap(origin1.Width, origin1.Height);
                    }
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
                    if (pictureBox1.Image == null)
                    {
                        pictureBox1.Image = new Bitmap(origin1.Width, origin1.Height);
                    }
                }
            }
        }
        public void LaplacianFilter(Bitmap bm, byte[] buf)
        {
            byte[] C = new byte[bm.Width * bm.Height];
            byte[] C2 = buf;
            for (int i = 0; i < bm.Height; ++i)
            {
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
                    m = (byte)(C[512 * i + j] + 128);
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
                    m = C[512 * i + j];
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
        private void CopyImage(Bitmap ori,ref Bitmap copy)
        {
            int w = ori.Width;
            int h = ori.Height;

            copy = ori.Clone(new Rectangle(0, 0, w, h), PixelFormat.Format32bppArgb);

            //int val = 0;
            //for (int i = 0; i < h; ++i)
            //{
            //    for (int j = 0; j < w; ++j)
            //    {
            //        val = ori.GetPixel(i, j).R;
            //        copy.SetPixel(i, j, Color.FromArgb(val, val, val));
            //    }
            //}
        }
        private void Btn_Dilation_Click(object sender, EventArgs e)
        {
            int w = ((Bitmap)pictureBox1.Image).Width;
            int h = ((Bitmap)pictureBox1.Image).Height;

            for (int i = 0; i < w * h; ++i)
            {
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
                ((Bitmap)pictureBox2.Image).SetPixel(i % w, i / w, Color.FromArgb(max, max, max));
            }
            pictureBox2.Refresh();
        }

        private void Btn_Erosion_Click(object sender, EventArgs e)
        {
            int w = ((Bitmap)pictureBox1.Image).Width;
            int h = ((Bitmap)pictureBox1.Image).Height;
            for (int i = 0; i < w * h; ++i)
            {
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
                ((Bitmap)pictureBox2.Image).SetPixel(i % w, i / w, Color.FromArgb(min, min, min));
            }
            pictureBox2.Refresh();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap PictureBox1Image = (Bitmap)pictureBox1.Image;
                double ratio = (double)PictureBox1Image.Width / (double)260;
                label1.Text = $" x : {(int)((double)e.X * ratio)}";
                label2.Text = $" Y : {(int)((double)e.Y * ratio)}";
                int newX = (int)((double)e.X * ratio);
                int newY = (int)((double)e.Y * ratio);
                Bitmap zoomImage = new Bitmap(40, 40);

                label3.Text = $"ColorIndex : {PictureBox1Image.GetPixel(newX, newY).R}";

                for (int i = -20; i < 20; ++i)
                    for (int j = -20; j < 20; ++j)
                    {
                        if ((newX + i) < 0 || (newY + j) < 0 || (newY + j) >= PictureBox1Image.Height || (newX + i) >= PictureBox1Image.Width)
                        {
                            continue;
                        }
                        else
                        {
                            zoomImage.SetPixel(20 + i, 20 + j, PictureBox1Image.GetPixel(newX + i, newY + j));
                        }
                    }
                pictureBox3.Image = zoomImage;
            }
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                double ratio = (double)512 / (double)260;
                label1.Text = $" x : {(int)((double)e.X * ratio)}";
                label2.Text = $" Y : {(int)((double)e.Y * ratio)}";
                int newX = (int)((double)e.X * ratio);
                int newY = (int)((double)e.Y * ratio);
                Bitmap zoomImage = new Bitmap(40, 40);
                Bitmap originImage = (Bitmap)pictureBox2.Image;
                label3.Text = $"ColorIndex : {originImage.GetPixel(newX, newY).R}";

                for (int i = -20; i < 20; ++i)
                    for (int j = -20; j < 20; ++j)
                    {
                        if ((newX + i) < 0 || (newY + j) < 0 || (newY + j) >= 512 || (newX + i) >= 512)
                        {
                            continue;
                        }
                        else
                        {
                            zoomImage.SetPixel(20 + i, 20 + j, originImage.GetPixel(newX + i, newY + j));
                        }
                    }

                pictureBox3.Image = zoomImage;
            }
        }

        private void Btn_Guassian_Click(object sender, EventArgs e)
        {
            //Bitmap originImage = (Bitmap)pictureBox1.Image;  
            //Bitmap originImage_copy = CopyImage(originImage);   
            //20220801_09:21 hjw 수정

            int w = ((Bitmap)pictureBox1.Image).Width;
            int h = ((Bitmap)pictureBox1.Image).Height;
            byte[] C = new byte[w * h];
            byte[] C2 = new byte[w * h];

            MakeBuf((Bitmap)pictureBox1.Image, C2);
            GaussianFilter((Bitmap)pictureBox1.Image, C2);
            pictureBox2.Image = (Bitmap)pictureBox1.Image;
        }

        private void Btn_Laplacian_Click(object sender, EventArgs e)
        {
            //Bitmap originImage = (Bitmap)pictureBox1.Image;
            //Bitmap originImage_copy = CopyImage(originImage);
            //20220801_09:21 hjw 수정

            int w = ((Bitmap)pictureBox1.Image).Width;
            int h = ((Bitmap)pictureBox1.Image).Height;
            byte[] C = new byte[w * h];
            byte[] C2 = new byte[w * h];
            MakeBuf((Bitmap)pictureBox1.Image, C2);
            LaplacianFilter((Bitmap)pictureBox1.Image, C2);
            pictureBox2.Image = (Bitmap)pictureBox1.Image;
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



        //수정사항 1. 픽쳐2에 마우스올라갈시 오류    2. form1 ,2 의 표시 바꾸기 3. form2의 표시사항 
        private int ClickCount = 0;
        private double zoomRatio = 1.0;
        private Form2 ZoomImageRectIndex = null;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //pictureBox1.Load(file_path);
            pictureBox1.Image = img1_backup; //20220801_09:08 hjw 수정

            double ratio = (double)512 / (double)260;
            int newX = (int)((double)e.X * ratio);
            int newY = (int)((double)e.Y * ratio);
            Bitmap zoomImage = new Bitmap(260, 260);
            Bitmap originImage = (Bitmap)pictureBox1.Image;
            Bitmap RectIndex = originImage.Clone(new Rectangle(0, 0, originImage.Width, originImage.Height), PixelFormat.Format32bppArgb);
            Color G = Color.FromArgb( 255, 0, 0);

            if (ZoomImageRectIndex == null)
            {
                if (ClickCount == 0)
                {
                    ZoomImageRectIndex = new Form2(RectIndex);
                    ZoomImageRectIndex.Owner = this;
                    ZoomImageRectIndex.Show();
                    ++ClickCount;
                }
            }

            else if (ZoomImageRectIndex != null) // Picture1 2번째 누를때 부터 
            {

                if (e.Button == MouseButtons.Left)     // 확대
                {
                    zoomRatio *= 0.95;
                    for (int i = -130; i < 130; ++i)
                        for (int j = -130; j < 130; ++j)
                        {
                            if (((newX + (zoomRatio * i)) > originImage.Width) || ((newY + (zoomRatio * j))) > originImage.Height || ((newX + (zoomRatio * i)) < 0) || ((newY + (zoomRatio * j))) < 0)
                            {
                                break;
                            }

                            else
                            {
                                zoomImage.SetPixel(130 + i, 130 + j, originImage.GetPixel((int)(double)(newX + (zoomRatio * i)), (int)(double)(newY + (zoomRatio * j)))); // 줌이미지
                            }

                            if ((-130 <= i && i < 130) && (j == -130 || j == 129) || (-130 <= j && j < 130) && (i == -130 || i == 129))
                            {
                                RectIndex.SetPixel((int)(double)(newX + (zoomRatio * i)), (int)(double)(newY + (zoomRatio * j)), G);   // 원본에 사각 인덱스 표시
                            }
                        }
                }
                else if (e.Button == MouseButtons.Right)     // 축소
                {
                    zoomRatio *= 1.05;

                    for (int i = -130; i < 130; ++i)
                        for (int j = -130; j < 130; ++j)
                        {
                            if (((newX + (zoomRatio * i)) < 0) || ((newY + (zoomRatio * j))) < 0 || ((newX + (zoomRatio * i)) >= originImage.Width) || ((newY + (zoomRatio * j))) >= originImage.Height)
                            {
                                break;
                            }
                            else
                            {
                                zoomImage.SetPixel(130 + i, 130 + j, originImage.GetPixel((int)(double)(newX + (zoomRatio * i)), (int)(double)(newY + (zoomRatio * j)))); // 줌이미지
                            }
                            if ((-130 <= i && i < 130) && (j == -130 || j == 129) || (-130 <= j && j < 130) && (i == -130 || i == 129))
                            {
                                RectIndex.SetPixel((int)(double)(newX + (zoomRatio * i)), (int)(double)(newY + (zoomRatio * j)), G);   // 원본에 사각 인덱스 표시
                            }
                        }
                }
                pictureBox1.Image = zoomImage;
                ZoomImageRectIndex.Bitmap = RectIndex;
            }

        }

        private void Btn_Equalization_Click(object sender, EventArgs e)
        {
            int row = ((Bitmap)pictureBox1.Image).Height;
            int col = ((Bitmap)pictureBox1.Image).Width;
            //filename = "Equalization.bmp";
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
                for (int j = 0; j < col; ++j)
                {
                    byte color = (byte)New[((Bitmap)pictureBox1.Image).GetPixel(i, j).R];
                    bmp.SetPixel(i, j, Color.FromArgb(color, color, color));
                }
            }

            pictureBox2.Image = bmp;
        }

        private void Btn_Otsu_Click(object sender, EventArgs e)
        {
            int row = pictureBox1.Image.Height;
            int col = pictureBox1.Image.Width;
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
            pictureBox2.Image = bmp;
        }

        private void Btn_Matching_Click(object sender, EventArgs e)
        {
            int min = 2100000000;
            int Templatewidth = ((Bitmap)pictureBox1.Image).Width;
            int Templateheight = ((Bitmap)pictureBox1.Image).Height;
            int mewidth = ((Bitmap)pictureBox2.Image).Height;
            int meheight = ((Bitmap)pictureBox2.Image).Width;
            //filename = "Equalization.bmp";
            Bitmap bmp = new Bitmap(mewidth, meheight);
            byte[] data = new byte[mewidth * meheight];
            for (int i = 0; i < mewidth; i++)
            {
                for (int j = 0; j < meheight; ++j)
                {
                    bmp.SetPixel(i, j, ((Bitmap)pictureBox2.Image).GetPixel(i, j));
                    data[j * mewidth + i] = ((Bitmap)pictureBox2.Image).GetPixel(i, j).R;
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
                        bmp.SetPixel((y), (x), Color.FromArgb(255, 0, 0));

                }
            }

            pictureBox2.Image = bmp;
        }
    }

}
