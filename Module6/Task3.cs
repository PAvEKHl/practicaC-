using Microsoft.Data.Sqlite; // Подключение библиотеки для работы с SQLite
using System;                // Базовые системные классы
using System.Data;           // Классы для таблиц DataTable
using System.Windows.Forms;  // Классы Windows Forms

namespace Module_6
{
    public partial class Task3 : Form
    {
        string connString = "Data Source=module6.db"; // Путь к файлу базы данных

        public Task3()
        {
            InitializeComponent(); // Инициализация элементов формы
            LoadData();            // Загрузка списка книг при открытии
        }

        private void LoadData(string searchFilter = "") // Метод выгрузки книг с учетом поиска
        {
            using (var conn = new SqliteConnection(connString))
            {
                conn.Open();
                string query = "SELECT * FROM task3";

                // Если введено слово для поиска, добавляем фильтр
                if (!string.IsNullOrEmpty(searchFilter))
                {
                    query += " WHERE Title LIKE @search OR Author LIKE @search";
                }

                using (var cmd = new SqliteCommand(query, conn))
                {
                    if (!string.IsNullOrEmpty(searchFilter))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + searchFilter + "%");
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        var table = new DataTable();
                        table.Load(reader);
                        dataGridView1.DataSource = table; // Вывод книг в таблицу
                    }
                }
            }
        }

        private void AddBook(object sender, EventArgs e) // Добавление новой книги в библиотеку
        {
            using (var conn = new SqliteConnection(connString))
            {
                conn.Open();
                string query = "INSERT INTO task3 (Title, Author, IsAvailable) VALUES (@title, @author, 'Доступна')";
                using (var cmd = new SqliteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@title", textBox1.Text);  // Название книги
                    cmd.Parameters.AddWithValue("@author", textBox2.Text); // Автор книги
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();
            textBox1.Clear();
            textBox2.Clear();
        }

        private void RentBook(object sender, EventArgs e) // Аренда выбранной книги (изменение статуса)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                var id = dataGridView1.Rows[rowIndex].Cells["Id"].Value;

                using (var conn = new SqliteConnection(connString))
                {
                    conn.Open();
                    string query = "UPDATE task3 SET IsAvailable = 'Арендована' WHERE Id = @id";
                    using (var cmd = new SqliteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadData();
            }
            else
            {
                MessageBox.Show("Выберите книгу в таблице для аренды!");
            }
        }

        private void SearchBook(object sender, EventArgs e) // Поиск книг по названию или автору
        {
            LoadData(textBox1.Text); // Передаем текст из первого поля в качестве фильтра
        }
    }
}