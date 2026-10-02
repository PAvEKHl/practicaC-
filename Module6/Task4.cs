using Microsoft.Data.Sqlite; // Подключение библиотеки для работы с SQLite
using System;                // Базовые системные классы
using System.Data;           // Классы для таблиц DataTable
using System.Windows.Forms;  // Классы Windows Forms

namespace Module_6
{
    public partial class Task4 : Form
    {
        string connString = "Data Source=module6.db"; // Путь к файлу базы данных

        public Task4()
        {
            InitializeComponent(); // Инициализация элементов формы
            LoadData();            // Загрузка финансовых записей при открытии
        }

        private void LoadData() // Метод выгрузки записей в DataGridView
        {
            using (var conn = new SqliteConnection(connString))
            {
                conn.Open();
                string query = "SELECT * FROM task4"; // Выборка из таблицы task4
                using (var cmd = new SqliteCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        var table = new DataTable();
                        table.Load(reader);
                        dataGridView1.DataSource = table; // Вывод в таблицу на форме
                    }
                }
            }
        }

        private void Add(object sender, EventArgs e) // Добавление записи (доход/расход)
        {
            using (var conn = new SqliteConnection(connString))
            {
                conn.Open();
                string query = "INSERT INTO task4 (Type, Category, Amount) VALUES (@type, @cat, @amt)";
                using (var cmd = new SqliteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@type", textBox1.Text); // Тип (Доход/Расход)
                    cmd.Parameters.AddWithValue("@cat", textBox2.Text);  // Категория
                    cmd.Parameters.AddWithValue("@amt", textBox3.Text);  // Сумма
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void Change(object sender, EventArgs e) // Изменение выбранной записи
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                var id = dataGridView1.Rows[rowIndex].Cells["Id"].Value;

                using (var conn = new SqliteConnection(connString))
                {
                    conn.Open();
                    string query = "UPDATE task4 SET Type = @type, Category = @cat, Amount = @amt WHERE Id = @id";
                    using (var cmd = new SqliteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@type", textBox1.Text);
                        cmd.Parameters.AddWithValue("@cat", textBox2.Text);
                        cmd.Parameters.AddWithValue("@amt", textBox3.Text);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadData();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
            }
            else
            {
                MessageBox.Show("Кликните на нужную строку в таблице!");
            }
        }

        private void Del(object sender, EventArgs e) // Удаление записи
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                var id = dataGridView1.Rows[rowIndex].Cells["Id"].Value;

                using (var conn = new SqliteConnection(connString))
                {
                    conn.Open();
                    string query = "DELETE FROM task4 WHERE Id = @id";
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
                MessageBox.Show("Кликните на нужную строку в таблице!");
            }
        }
    }
}