using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

//5.	Создайте приложение для сортировки числовых данных.
//Пользователь должен иметь возможность выбрать метод сортировки (например, сортировка пузырьком или быстрая сортировка)
//с помощью делегатов.

namespace Module3
{
    public partial class Task5 : Window
    {
        public Task5()
        {
            InitializeComponent();
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            string input = NumbersTextBox.Text; //Считываем всё, что пользователь ввел в поле ввода
            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("Введите хотя бы одно число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int[] numbers = input.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries) //Превращение строки в массив чисел
                                 .Select(n => int.TryParse(n.Trim(), out int num) ? num : 0)
                                 .ToArray();

            if (numbers.Length == 0)
            {
                MessageBox.Show("Введите корректные числа.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Action<int[]> sortingMethod = null; //делегат

            if (SortMethodSelector.SelectedItem is ComboBoxItem selectedMethod)
            {
                switch (selectedMethod.Content.ToString())
                {
                    case "От меньшего к большему": 
                        // По возрастанию через встроенный Array.Sort
                        sortingMethod = array => Array.Sort(array); //с лева array это входной параметр,как props в react
                        break;

                    case "От большего к меньшему":
                        // По убыванию через встроенные Array.Sort и Array.Reverse
                        sortingMethod = array =>
                        {
                            Array.Sort(array);
                            Array.Reverse(array);
                        };
                        break;
                }

                // Вызов через делегат
                sortingMethod?.Invoke(numbers);

                // Вывод результата
                ResultTextBox.Text = string.Join(", ", numbers);
            }
            else
            {
                MessageBox.Show("Выберите метод сортировки.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}