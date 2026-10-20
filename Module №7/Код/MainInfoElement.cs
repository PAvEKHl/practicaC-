using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace энциклопедияПоГайдамЮтюб
{
    public partial class MainInfoElement : UserControl
    {
        public MainInfoElement()
        {
            InitializeComponent();
            // Теперь вы точно увидите границы ваших текстовых полей
            DescriptionLbl.BorderStyle = BorderStyle.FixedSingle;
            InternetfactsLbl.BorderStyle = BorderStyle.FixedSingle;

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private Image _icon1;
        private Image _icon2;
        private Image _icon3;
        private Image _icon4;
        private string _description;
        private string _internetfacts;

       [Category("Custom Props")]
        public string Internetfacts
        {
            get { return _internetfacts;  }
            set
            {
                _internetfacts = value;
                if (label5 != null)
                    label5.Text = value;
            }
        }
        [Category("Custom Props")]
        public string Descriptions
        {
            get { return _description; }
            set
            {
                _description = value;
                if (DescriptionLbl != null)
                    DescriptionLbl.Text = value;
            }
        }
        //создание свойства для Первой картинки
        [Category("Custom Props")]
        public Image Icon1
        {
            get { return _icon1; }
            set
            {
                _icon1 = value;
                if (pictureBox1 != null)
                {
                    pictureBox1.Image = value;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }
        //создание свойства для 2-ой картинки
        [Category("Custom Props")]
        public Image Icon2
        {
            get { return _icon2; }
            set
            {
                _icon2 = value;
                if (pictureBox2 != null)
                {
                    pictureBox2.Image = value;
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }
        //создание свойства для 3-ей картинки
        [Category("Custom Props")]
        public Image Icon3
        {
            get { return _icon3; }
            set
            {
                _icon3 = value;
                if (pictureBox3 != null)
                {
                    pictureBox3.Image = value;
                    pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }
        //создание свойства для 4-ой картинки
        [Category("Custom Props")]
        public Image Icon4
        {
            get { return _icon4; }
            set
            {
                _icon4 = value;
                if (pictureBox4 != null)
                {
                    pictureBox4.Image = value;
                    pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }
        
        public int ID; //ID кнопки
        
    }
}
