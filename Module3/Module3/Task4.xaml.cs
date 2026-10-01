using System;
using System.Collections.Generic;
using System.Windows;

//4.	Разработайте систему фильтрации данных с использованием делегатов.
//Пользователь должен иметь возможность выбрать фильтр для списка данных (например, фильтр по дате или по ключевым словам).

namespace Module3
{
    public partial class Task4 : Window
    {
        // 1. Объявляем делегат для фильтрации элементов
        public delegate bool DataFilterDelegate(string item);

        private List<string> dataList; // Исходный список данных

        public Task4()
        {
            InitializeComponent();
            dataList = new List<string>
            {
                "2025-10-01 - Задача 1",
                "2025-10-02 - Задача 2",
                "2025-10-03 - Задача 3",
                "2025-10-04 - Задача 4",
                "2025-10-05 - Задача 5"
            };
            UpdateDataList(dataList); // Изначально показываем все данные
        }

        // Обновление списка данных на экране
        private void UpdateDataList(List<string> items)
        {
            if (DataListBox != null)
            {
                DataListBox.Items.Clear();
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        DataListBox.Items.Add(item);
                    }
                }
            }
        }

        // 2. Универсальный метод фильтрации, принимающий делегат
        private void FilterAndDisplayData(DataFilterDelegate filter)
        {
            var filteredData = new List<string>();

            foreach (var item in dataList)
            {
                // Вызываем делегат для проверки каждого элемента
                if (filter(item))
                {
                    filteredData.Add(item);
                }
            }

            UpdateDataList(filteredData);
        }

        // Фильтрация данных по ключевым словам с использованием делегата
        private void KeywordFilterButton_Click(object sender, RoutedEventArgs e)
        {
            string keyword = KeywordTextBox?.Text ?? string.Empty;

            if (string.IsNullOrWhiteSpace(keyword))
            {
                UpdateDataList(dataList);
                return;
            }

            // Создаем экземпляр делегата с помощью лямбда-выражения
            DataFilterDelegate keywordFilter = (string item) =>
            {
                return item.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;
            };

            // Применяем фильтр
            FilterAndDisplayData(keywordFilter);
        }

        // Фильтрация данных по дате с использованием делегата
        private void DateFilterButton_Click(object sender, RoutedEventArgs e)
        {
            string dateInput = DateTextBox?.Text ?? string.Empty;

            if (string.IsNullOrWhiteSpace(dateInput))
            {
                MessageBox.Show("Введите дату.");
                return;
            }

            if (DateTime.TryParse(dateInput, out var targetDate))
            {
                string formattedDate = targetDate.ToString("yyyy-MM-dd");

                // Создаем делегат для проверки соответствия даты в строке
                DataFilterDelegate dateFilter = delegate (string item)
                {
                    // Проверяем, начинается ли строка с выбранной даты или содержит её
                    return item.Contains(formattedDate);
                };

                // Применяем фильтр
                FilterAndDisplayData(dateFilter);
            }
            else
            {
                MessageBox.Show("Некорректный формат даты. Пожалуйста, используйте формат yyyy-MM-dd.");
                UpdateDataList(new List<string> { "Неверная дата. Используйте формат yyyy-MM-dd." });
            }
        }
    }
}