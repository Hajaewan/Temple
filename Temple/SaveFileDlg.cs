using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Temple
{
    public partial class SaveFileDlg : Form
    {
        Bitmap origin;
        Bitmap part;

        public SaveFileDlg(Bitmap origin, Bitmap part)
        {
            InitializeComponent();
            this.origin = origin;
            this.part = part;
        }

        private void SaveFile(Bitmap bit)
        {
            byte[] HD = BitConverter.GetBytes((short)19778)
              .Concat(BitConverter.GetBytes(1080 + bit.Height * bit.Width))
              .Concat(BitConverter.GetBytes((short)0))
              .Concat(BitConverter.GetBytes((short)0))
              .Concat(BitConverter.GetBytes(1078)).ToArray();

            byte[] IH = BitConverter.GetBytes(40)
                .Concat(BitConverter.GetBytes(bit.Width))
                .Concat(BitConverter.GetBytes(bit.Height))
                .Concat(BitConverter.GetBytes((short)1))
                .Concat(BitConverter.GetBytes((short)8))
                .Concat(BitConverter.GetBytes(0))
                .Concat(BitConverter.GetBytes(0))
                .Concat(BitConverter.GetBytes(2844))
                .Concat(BitConverter.GetBytes(2844))
                .Concat(BitConverter.GetBytes(0))
                .Concat(BitConverter.GetBytes(0)).ToArray();

            byte[] PAL = new byte[1024];

            for (int i = 0; i < 1024; i += 4)
            {
                Array.Copy(BitConverter.GetBytes(i / 4), 0, PAL, i, 1);
                Array.Copy(BitConverter.GetBytes(i / 4), 0, PAL, i + 1, 1);
                Array.Copy(BitConverter.GetBytes(i / 4), 0, PAL, i + 2, 1);
                Array.Copy(BitConverter.GetBytes(0), 0, PAL, i + 3, 1);
            }

            using (FileStream fs = new FileStream("savefile.bmp", FileMode.Create))
            {
                fs.Write(HD, 0, 14);
                fs.Write(IH, 0, 40);
                fs.Write(PAL, 0, 1024);
                byte[] data = new byte[bit.Height * bit.Width];

                for (int i = 0; i < bit.Height * bit.Width; ++i)
                {
                    data[bit.Height * bit.Width - i - 1] = bit.GetPixel((bit.Width - 1) - (i % bit.Width), i / bit.Width).R;
                }

                fs.Write(data, 0, bit.Height * bit.Width);
            }
        }

        private void Btn_SaveOrigin_Click(object sender, EventArgs e)
        {
            SaveFile(origin);
        }

        private void Btn_SavePart_Click(object sender, EventArgs e)
        {
            SaveFile(part);
        }
    }
}
