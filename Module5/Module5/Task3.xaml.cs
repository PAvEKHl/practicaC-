using System.Windows;
//3.	Создайте приложение для учета задач с использованием Windows Forms.
//Пользователь должен иметь возможность добавлять, удалять и отмечать задачи как выполненные.
namespace Module5
{
    public partial class Task3 : Window
    {
        public Task3() => InitializeComponent();
        private void Add(object s, RoutedEventArgs e) { if (!string.IsNullOrWhiteSpace(txtTask.Text)) { lbTasks.Items.Add(txtTask.Text); txtTask.Clear(); } }
        private void Del(object s, RoutedEventArgs e) { if (lbTasks.SelectedIndex >= 0) lbTasks.Items.RemoveAt(lbTasks.SelectedIndex); }
    }
}