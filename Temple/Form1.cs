using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Temple
{
    public partial class Form1 : Form
    {
        Bitmap origin1;
        Bitmap origin2;


        public Form1()
        {
            InitializeComponent();
        }

        private void Btn_Read1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd1 = new OpenFileDialog())
            {
                ofd1.InitialDirectory = Directory.GetCurrentDirectory();
                if (ofd1.ShowDialog() == DialogResult.OK)
                {
                    origin1 = new Bitmap(ofd1.FileName);
                    Bitmap Copy_origin1 = new Bitmap(origin1.Width, origin1.Height);
                    CopyImage(origin1, Copy_origin1);
                    pictureBox1.Image = Copy_origin1;
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
                    origin2 = new Bitmap(ofd2.FileName);
                    Bitmap Copy_origin2 = new Bitmap(origin2.Width, origin2.Height);
                    CopyImage(origin2, Copy_origin2);
                    pictureBox2.Image = Copy_origin2;
                    if (pictureBox1.Image == null)
                    {
                        pictureBox1.Image = new Bitmap(origin1.Width, origin1.Height);
                    }
                }
            }
        }

        private void CopyImage(Bitmap ori, Bitmap copy)
        {
            int w = ori.Width;
            int h = ori.Height;
            int val = 0;
            for (int i = 0; i < h; ++i)
            {
                for (int j = 0; j < w; ++j)
                {
                    val = ori.GetPixel(i, j).R;
                    copy.SetPixel(i, j, Color.FromArgb(val, val, val));
                }
            }

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


    }
}
