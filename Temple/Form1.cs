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
        string file_path = "";
        private void Btn_Read1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd1 = new OpenFileDialog())
            {
                ofd1.InitialDirectory = Directory.GetCurrentDirectory();

                if (ofd1.ShowDialog() == DialogResult.OK)
                {
                    file_path = ofd1.FileName;
                    string file_name = Path.GetFileName(file_path);
                    pictureBox1.Load(file_path);
                    Bitmap originImage = (Bitmap)pictureBox1.Image;
                    Bitmap originImage_copy = CopyImage(originImage);
                    if (pictureBox2.Image == null)
                    {
                        pictureBox2.Image = new Bitmap(originImage.Width, originImage.Height);
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
                    file_path = ofd2.FileName;
                    string file_name = Path.GetFileName(file_path);
                    pictureBox2.Load(file_path);
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
        private Bitmap CopyImage(Bitmap ori)
        {
            int w = ori.Width;
            int h = ori.Height;

            Bitmap copy = ori.Clone(new Rectangle(0, 0, w, h), PixelFormat.Format32bppArgb);
            return copy;

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
            Bitmap originImage = (Bitmap)pictureBox1.Image;
            Bitmap originImage_copy = CopyImage(originImage);

            int w = ((Bitmap)pictureBox1.Image).Width;
            int h = ((Bitmap)pictureBox1.Image).Height;
            byte[] C = new byte[w * h];
            byte[] C2 = new byte[w * h];
            MakeBuf(originImage_copy, C2);
            GaussianFilter(originImage_copy, C2);
            pictureBox2.Image = originImage_copy;
        }

        private void Btn_Laplacian_Click(object sender, EventArgs e)
        {
            Bitmap originImage = (Bitmap)pictureBox1.Image;
            Bitmap originImage_copy = CopyImage(originImage);

            int w = ((Bitmap)pictureBox1.Image).Width;
            int h = ((Bitmap)pictureBox1.Image).Height;
            byte[] C = new byte[w * h];
            byte[] C2 = new byte[w * h];
            MakeBuf(originImage_copy, C2);
            LaplacianFilter(originImage_copy, C2);
            pictureBox2.Image = originImage_copy;
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
            pictureBox1.Load(file_path);

            double ratio = (double)512 / (double)260;
            int newX = (int)((double)e.X * ratio);
            int newY = (int)((double)e.Y * ratio);
            Bitmap zoomImage = new Bitmap(260, 260);
            Bitmap originImage = (Bitmap)pictureBox1.Image;
            Bitmap RectIndex = originImage.Clone(new Rectangle(0, 0, originImage.Width, originImage.Height), PixelFormat.Format32bppArgb);
            Color G = Color.FromArgb(255, 255, 255, 255);

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
    }

}
