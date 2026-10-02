using System.IO;
using System.Windows;
using Microsoft.Win32;
//2.	Разработайте текстовый редактор с возможностью открытия и сохранения текстовых файлов.
namespace Module5
{
    public partial class Task2 : Window
    {
        public Task2() => InitializeComponent();
        private void Open_Click(object s, RoutedEventArgs e) { var d = new OpenFileDialog(); if (d.ShowDialog() == true) txt.Text = File.ReadAllText(d.FileName); }
        private void Save_Click(object s, RoutedEventArgs e) { var d = new SaveFileDialog(); if (d.ShowDialog() == true) File.WriteAllText(d.FileName, txt.Text); }
    }
}