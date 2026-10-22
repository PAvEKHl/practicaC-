using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace TaskFlow_UI
{
    public partial class Form1 : Form
    {
        private const string DbFileName = "app_tasks.db";
        private readonly string connectionString = $"Data Source={DbFileName};Version=3;";
        private int selectedTaskId = -1;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeDatabase();
            cmbPriority.SelectedIndex = 1; // Средний
            cmbStatus.SelectedIndex = 0;   // В процессе
            dtpDueDate.Value = DateTime.Today.AddDays(3);
            LoadTasks();
            CheckDeadlines();
        }

        private void InitializeDatabase()
        {
            if (!File.Exists(DbFileName))
            {
                SQLiteConnection.CreateFile(DbFileName);
            }

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sql = @"
                    CREATE TABLE IF NOT EXISTS UserTasks (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Title TEXT NOT NULL,
                        Description TEXT,
                        Priority TEXT NOT NULL,
                        Status TEXT NOT NULL,
                        Progress INTEGER NOT NULL DEFAULT 0,
                        DueDate TEXT NOT NULL
                    );";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void LoadTasks()
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT Id, Title AS 'Задача', Description AS 'Описание', Priority AS 'Приоритет', Status AS 'Статус', Progress AS 'Прогресс (%)', DueDate AS 'Срок выполнения' FROM UserTasks ORDER BY DueDate ASC";
                using (var adapter = new SQLiteDataAdapter(sql, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvTasks.DataSource = dt;
                    if (dgvTasks.Columns["Id"] != null) dgvTasks.Columns["Id"].Visible = false;
                }
            }
        }

        private void CheckDeadlines()
        {
            int urgentCount = 0;
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT Title, DueDate FROM UserTasks WHERE Status != 'Выполнено'";
                using (var cmd = new SQLiteCommand(sql, conn))
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        if (DateTime.TryParse(r["DueDate"].ToString(), out DateTime due))
                        {
                            double daysLeft = (due.Date - DateTime.Today).TotalDays;
                            if (daysLeft <= 1) urgentCount++;
                        }
                    }
                }
            }

            if (urgentCount > 0)
            {
                MessageBox.Show($"Внимание! У вас есть {urgentCount} задач(и) с горящим или истекшим сроком выполнения!", "Уведомление о дедлайнах", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text)) return;

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO UserTasks (Title, Description, Priority, Status, Progress, DueDate) VALUES (@t, @d, @pr, @s, @prog, @due)";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@t", txtTitle.Text.Trim());
                    cmd.Parameters.AddWithValue("@d", txtDesc.Text.Trim());
                    cmd.Parameters.AddWithValue("@pr", cmbPriority.Text);
                    cmd.Parameters.AddWithValue("@s", cmbStatus.Text);
                    cmd.Parameters.AddWithValue("@prog", (int)numProgress.Value);
                    cmd.Parameters.AddWithValue("@due", dtpDueDate.Value.ToString("yyyy-MM-dd"));
                    cmd.ExecuteNonQuery();
                }
            }
            ClearInputs();
            LoadTasks();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedTaskId == -1) return;

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sql = "UPDATE UserTasks SET Title=@t, Description=@d, Priority=@pr, Status=@s, Progress=@prog, DueDate=@due WHERE Id=@id";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@t", txtTitle.Text.Trim());
                    cmd.Parameters.AddWithValue("@d", txtDesc.Text.Trim());
                    cmd.Parameters.AddWithValue("@pr", cmbPriority.Text);
                    cmd.Parameters.AddWithValue("@s", cmbStatus.Text);
                    cmd.Parameters.AddWithValue("@prog", (int)numProgress.Value);
                    cmd.Parameters.AddWithValue("@due", dtpDueDate.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@id", selectedTaskId);
                    cmd.ExecuteNonQuery();
                }
            }
            ClearInputs();
            LoadTasks();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedTaskId == -1) return;

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("DELETE FROM UserTasks WHERE Id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", selectedTaskId);
                    cmd.ExecuteNonQuery();
                }
            }
            ClearInputs();
            LoadTasks();
        }

        private void dgvTasks_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count > 0)
            {
                var row = dgvTasks.SelectedRows[0];
                if (int.TryParse(row.Cells["Id"].Value?.ToString(), out int id))
                {
                    selectedTaskId = id;
                    txtTitle.Text = row.Cells["Задача"].Value?.ToString();
                    txtDesc.Text = row.Cells["Описание"].Value?.ToString();
                    cmbPriority.Text = row.Cells["Приоритет"].Value?.ToString();
                    cmbStatus.Text = row.Cells["Статус"].Value?.ToString();
                    if (int.TryParse(row.Cells["Прогресс (%)"].Value?.ToString(), out int prog))
                        numProgress.Value = Math.Min(100, Math.Max(0, prog));
                    if (DateTime.TryParse(row.Cells["Срок выполнения"].Value?.ToString(), out DateTime due))
                        dtpDueDate.Value = due;
                }
            }
        }

        private void ClearInputs()
        {
            selectedTaskId = -1;
            txtTitle.Clear();
            txtDesc.Clear();
            cmbPriority.SelectedIndex = 1;
            cmbStatus.SelectedIndex = 0;
            numProgress.Value = 0;
            dtpDueDate.Value = DateTime.Today.AddDays(3);
            dgvTasks.ClearSelection();
        }

        private void btnCheckDeadlines_Click(object sender, EventArgs e) => CheckDeadlines();
    }
}