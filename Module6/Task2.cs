using Microsoft.Data.Sqlite; // Подключение библиотеки для работы с SQLite
using System;                // Базовые системные классы
using System.Data;           // Классы для таблиц DataTable
using System.Windows.Forms;  // Классы Windows Forms

namespace Module_6
{
    public partial class Task2 : Form
    {
        string connString = "Data Source=module6.db"; // Путь к файлу базы данных

        public Task2()
        {
            InitializeComponent();
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при запуске: " + ex.Message);
            }
        }

        private void LoadData() // Метод выгрузки задач в DataGridView
        {
            using (var conn = new SqliteConnection(connString))
            {
                conn.Open();
                string query = "SELECT * FROM task2"; // Выборка из таблицы task2
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

        private void Add(object sender, EventArgs e) // Метод добавления задачи
        {
            using (var conn = new SqliteConnection(connString))
            {
                conn.Open();
                string query = "INSERT INTO task2 (Title, Status) VALUES (@title, @status)";
                using (var cmd = new SqliteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@title", textBox1.Text);  // Название задачи
                    cmd.Parameters.AddWithValue("@status", textBox2.Text); // Статус задачи
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();
            textBox1.Clear();
            textBox2.Clear();
        }

        private void Change(object sender, EventArgs e) // Метод обновления задачи
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                var id = dataGridView1.Rows[rowIndex].Cells["Id"].Value; // ID задачи

                using (var conn = new SqliteConnection(connString))
                {
                    conn.Open();
                    string query = "UPDATE task2 SET Title = @title, Status = @status WHERE Id = @id";
                    using (var cmd = new SqliteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@title", textBox1.Text);
                        cmd.Parameters.AddWithValue("@status", textBox2.Text);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadData();
                textBox1.Clear();
                textBox2.Clear();
            }
            else
            {
                MessageBox.Show("Кликните на нужную строку в таблице!");
            }
        }

        private void Del(object sender, EventArgs e) // Метод удаления задачи
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                var id = dataGridView1.Rows[rowIndex].Cells["Id"].Value;

                using (var conn = new SqliteConnection(connString))
                {
                    conn.Open();
                    string query = "DELETE FROM task2 WHERE Id = @id";
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