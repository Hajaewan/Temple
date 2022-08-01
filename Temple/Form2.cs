using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Temple
{
    public partial class Form2 : Form
    {
        public Bitmap Bitmap { set { pictureBox1.Image = value; } }
        public Form2(Bitmap bmp)
        {
            InitializeComponent();
            Bitmap = bmp;

        }
    }
}
