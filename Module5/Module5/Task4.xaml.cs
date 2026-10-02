using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
//4.	Реализуйте приложение для просмотра изображений.
//Пользователь должен иметь возможность выбирать изображение для просмотра и масштабировать его.
namespace Module5
{
    public partial class Task4 : Window
    {
        public Task4() => InitializeComponent();
        private void Open_Click(object s, RoutedEventArgs e)
        {
            var d = new OpenFileDialog { Filter = "Изображения|*.jpg;*.png;*.bmp" };
            if (d.ShowDialog() == true) { img.Source = new BitmapImage(new Uri(d.FileName)); zoom.Value = 1; }
        }
    }
}