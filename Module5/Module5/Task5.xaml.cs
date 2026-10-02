using System.Data;
using System.Windows;
using System.Windows.Controls;
//5.	Создайте калькулятор с графическим интерфейсом.
//Пользователь должен иметь возможность выполнять арифметические операции.
namespace Module5
{
    public partial class Task5 : Window
    {
        public Task5() => InitializeComponent();
        private void Btn_Click(object s, RoutedEventArgs e)
        {
            string b = ((Button)e.OriginalSource).Content.ToString();
            if (b == "C") txt.Clear();
            else if (b == "=") try { txt.Text = new DataTable().Compute(txt.Text, null).ToString(); } catch { txt.Text = "Ошибка"; }
            else txt.Text += b;
        }
    }
}