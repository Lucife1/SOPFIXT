using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Customer : PictureBox
    {
        public Customer()
        {
            InitializeComponent();
        }

        private Image NormalImage;
        private Image HoverImage;

        public Image ImageNormal
        {
            get { return NormalImage;}
            set { NormalImage = value;}
        }

        public Image ImageHover
        {
            get { return HoverImage;}
            set { HoverImage = value;}
        }

        private void Customer_MouseHover(object sender, EventArgs e)
        {
            this.Image = HoverImage;
        }

        private void Customer_MouseLeave(object sender, EventArgs e)
        {
            this.Image = NormalImage;
        }
    }
}
