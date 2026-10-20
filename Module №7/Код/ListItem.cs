using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace энциклопедияПоГайдамЮтюб
{
    public partial class ListItem : UserControl
    {
        public ListItem()
        {
            InitializeComponent();
        }

        private string _title;
        private string _message;
        private Image _icon;
        private Color _iconBack;

        [Category("Custom Props")]
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                if (lblTitle != null)  
                    lblTitle.Text = value;
            }
        }
        //Только ттчо дабавил категорию страну и год производства
        private string _category;
        [Category("Custom Props")]
        public string Category
        {
            get { return _category; }
            set
            {
                _category = value;
                if (lbCategory != null)
                    lbCategory.Text = value;
            }
        }

        private string _country;
        [Category("Custom Props")]
        public string Country
        {
            get { return _country; }
            set
            {
                _country = value;
                if (lblCountry != null)
                    lblCountry.Text = value;
            }
        }

        private string _yearproduction;
        [Category("Custom Props")]
        public string YearProduction
        {
            get { return _yearproduction; }
            set
            {
                _yearproduction = value;
                if (lblYearProduction != null)
                    lblYearProduction.Text = value;
            }
        }
        //Крнец нового необузданного кода
        [Category("Custom Props")]
        public Color IconBackground
        {
            get { return _iconBack; }
            set
            {
                _iconBack = value;
                if (panel1 != null)  
                    panel1.BackColor = value;
            }
        }

        [Category("Custom Props")]
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                if (lblMessage != null)  
                    lblMessage.Text = value;
            }
        }

        [Category("Custom Props")]
        public Image Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                if (pictureBox1 != null)  
                {
                    pictureBox1.Image = value;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }

        private string _button;
        [Category("Custom Props")]

        public string BtnDetail
        {
            get { return _button; }
            set
            {
                _button = value;
                if (btnDetail != null)
                    btnDetail.Text = value;
            }
        }
        private void ListItem_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor= Color.White;
        }

        private void ListItem_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor= Color.Silver;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public int Id; //ID кнопки
        private void btnDetail_Click(object sender, EventArgs e)  //Кнопка Подробнее
        {
            // Ищем форму в списке уже открытых приложений
            MoreDetailed moreDetailed = (MoreDetailed)Application.OpenForms["MoreDetail"];

            if (moreDetailed == null)
            {

                // Если не нашли — создаем и показываем
                moreDetailed = new MoreDetailed(Convert.ToInt32(Id));
                moreDetailed.Show();
            }
            else
            {
                // Если нашли — выводим на передний план
                moreDetailed.Activate();              
            }
        }
    }
}   