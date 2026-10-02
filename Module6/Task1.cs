using Microsoft.Data.Sqlite; // Подключение библиотеки для работы с SQLite
using System;                // Базовые системные классы
using System.Data;           // Классы для работы с таблицами данных (DataTable)
using System.Windows.Forms;  // Классы для создания графического интерфейса Windows Forms

namespace Module_6
{
    public partial class Form1 : Form
    {
        string connString = "Data Source=module6.db"; // Строка подключения к файлу базы данных

        public Form1()
        {
            InitializeComponent(); // Инициализация элементов формы
            LoadData();            // Загрузка данных из БД при запуске программы
        }

        private void LoadData() // Метод выгрузки данных из базы в сетку DataGridView
        {
            using (var conn = new SqliteConnection(connString)) // Создание подключения к БД
            {
                conn.Open();                                   // Открытие соединения
                string query = "SELECT * FROM module6";        // SQL-запрос на выборку всех записей
                using (var cmd = new SqliteCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())   // Чтение данных
                    {
                        var table = new DataTable();           // Создание таблицы в памяти
                        table.Load(reader);                    // Загрузка данных
                        dataGridView1.DataSource = table;      // Вывод на экран в DataGridView
                    }
                }
            }
        }

        private void Add(object sender, EventArgs e) // Метод добавления записи
        {
            using (var conn = new SqliteConnection(connString))
            {
                conn.Open();
                string query = "INSERT INTO module6 (Name, Positon) VALUES (@name, @pos)";
                using (var cmd = new SqliteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", textBox1.Text); // Имя из textBox1
                    cmd.Parameters.AddWithValue("@pos", textBox2.Text);  // Должность из textBox2
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();       // Обновление таблицы
            textBox1.Clear(); // Очистка полей ввода
            textBox2.Clear();
        }

        private void Change(object sender, EventArgs e) // Метод изменения выбранной записи
        {
            if (dataGridView1.SelectedCells.Count > 0) // Проверка, выбрана ли ячейка в таблице
            {
                int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                var id = dataGridView1.Rows[rowIndex].Cells["Id"].Value; // Получение ID строки

                using (var conn = new SqliteConnection(connString))
                {
                    conn.Open();
                    string query = "UPDATE module6 SET Name = @name, Positon = @pos WHERE Id = @id";
                    using (var cmd = new SqliteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", textBox1.Text); // Новое имя
                        cmd.Parameters.AddWithValue("@pos", textBox2.Text);  // Новая должность
                        cmd.Parameters.AddWithValue("@id", id);              // ID для поиска записи
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadData();       // Обновление таблицы
                textBox1.Clear();
                textBox2.Clear();
            }
            else
            {
                MessageBox.Show("Кликните на нужную строку в таблице!");
            }
        }

        private void Del(object sender, EventArgs e) // Метод удаления выбранной записи
        {
            if (dataGridView1.SelectedCells.Count > 0) // Проверка выбора ячейки
            {
                int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                var id = dataGridView1.Rows[rowIndex].Cells["Id"].Value; // Получение ID строки

                using (var conn = new SqliteConnection(connString))
                {
                    conn.Open();
                    string query = "DELETE FROM module6 WHERE Id = @id";
                    using (var cmd = new SqliteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id); // Передача ID для удаления
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadData(); // Обновление таблицы
            }
            else
            {
                MessageBox.Show("Кликните на нужную строку в таблице!");
            }
        }
    }
}