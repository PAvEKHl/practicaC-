// Блок подключения системных библиотек и пространств имен
using System; // Подключение базовых классов .NET (DateTime, EventArgs и др.)
using System.Data; // Подключение классов для работы с таблицами данных (DataTable)
using System.Data.SQLite; // Подключение библиотеки для работы с базой данных SQLite
using System.IO; // Подключение классов для работы с файловой системой (File)
using System.Windows.Forms; // Подключение элементов управления Windows Forms (Form, MessageBox и др.)
// Блок объявления пространства имен проекта
namespace InfoSort
{
    // Блок объявления главного класса формы приложения
    public partial class Form1 : Form // Объявление частичного класса Form1, наследующегося от базового класса окна Form
    {
        // Блок объявления глобальных переменных и констант класса
        private const string DbFileName = "tasks.db"; // Константа, хранящая имя файла базы данных SQLite
        private readonly string connectionString = $"Data Source={DbFileName};Version=3;"; // Строка подключения к базе данных с указанием файла и версии SQLite
        private int selectedTaskId = -1; // Переменная для хранения ID выбранной в таблице записи (-1 означает, что ничего не выбрано)
        // Блок конструктора главной формы
        public Form1() // Конструктор класса Form1, вызываемый при создании окна
        {
            InitializeComponent(); // Вызов метода отрисовки и инициализации всех элементов интерфейса из Form1.Designer.cs
        }
        // Блок обработчика события первичной загрузки формы
        private void Form1_Load(object sender, EventArgs e) // Метод, автоматически срабатывающий при открытии окна программы
        {
            InitializeDatabase(); // Вызов метода проверки и создания файла базы данных и таблицы
            // Блок установки начальных значений для выпадающих списков
            cmbPriority.SelectedIndex = 1; // Выбор второго пункта ("Средний") в списке приоритетов по умолчанию
            cmbStatus.SelectedIndex = 0;   // Выбор первого пункта ("В процессе") в списке статусов по умолчанию
            cmbFilterStatus.SelectedIndex = 0; // Выбор первого пункта ("Все") в фильтре статусов по умолчанию
            cmbSort.SelectedIndex = 0;     // Выбор первого пункта ("Сначала новые") в списке сортировки по умолчанию
            LoadTasks(); // Вызов метода загрузки и отображения списка задач из базы данных в таблицу на форме
        }
        // Блок создания файла базы данных и таблицы при первом запуске приложения
        private void InitializeDatabase() // Метод инициализации структуры базы данных
        {
            // Блок проверки существования файла базы данных на диске
            if (!File.Exists(DbFileName)) // Проверка: если файл tasks.db еще не существует в папке с программой
            {
                SQLiteConnection.CreateFile(DbFileName); // Создание нового пустого файла базы данных tasks.db
            }
            // Блок подключения к БД и выполнения SQL-запроса на создание таблицы
            using (var conn = new SQLiteConnection(connectionString)) // Создание объекта подключения к БД с автоматическим освобождением ресурсов после закрытия блока
            {
                conn.Open(); // Открытие соединения с базой данных
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Notes (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Title TEXT NOT NULL,
                        Description TEXT,
                        Priority TEXT NOT NULL,
                        Status TEXT NOT NULL,
                        CreatedAt DATETIME NOT NULL
                    );"; // Строка SQL-запроса для создания таблицы Notes (с полями Id, Title, Description, Priority, Status, CreatedAt), если она еще не создана
                // Блок создания и запуска SQL-команды
                using (var cmd = new SQLiteCommand(createTableQuery, conn)) // Создание SQL-команды на основе текста запроса и открытого соединения
                {
                    cmd.ExecuteNonQuery(); // Выполнение SQL-команды без возврата результирующих строк (создание таблицы)
                }
            }
        }
        // Блок загрузки задач из БД с учетом поиска, фильтрации и сортировки
        private void LoadTasks() // Метод чтения записей из БД и вывода их в таблицу DataGridView
        {
            // Блок открытия соединения с базой данных для чтения данных
            using (var conn = new SQLiteConnection(connectionString)) // Создание подключения к базе данных
            {
                conn.Open(); // Открытие соединения с базой данных
                // Блок формирования базового SQL-запроса выборки с поиском по тексту
                string query = @"
                    SELECT 
                        Id, 
                        Title AS 'Заголовок', 
                        Description AS 'Описание', 
                        Priority AS 'Приоритет', 
                        Status AS 'Статус', 
                        strftime('%d.%m.%Y %H:%M', CreatedAt) AS 'Дата создания'
                    FROM Notes 
                    WHERE (Title LIKE @search OR Description LIKE @search)"; // SQL-запрос выборки столбцов с русскими заголовками и условием поиска подстроки в заголовке или описании
                // Блок добавления условия фильтрации по выбранному статусу
                string selectedFilter = cmbFilterStatus.SelectedItem?.ToString() ?? "Все"; // Получение выбранного текста из фильтра статусов (или "Все", если ничего не выбрано)
                if (selectedFilter != "Все") // Проверка: если выбран конкретный статус, а не все записи
                {
                    query += " AND Status = @status"; // Добавление к SQL-запросу дополнительного условия отбора по полю Status
                }
                // Блок определения порядка сортировки записей
                string selectedSort = cmbSort.SelectedItem?.ToString() ?? "Сначала новые"; // Получение выбранного варианта сортировки из выпадающего списка
                switch (selectedSort) // Переключатель вариантов сортировки в зависимости от выбора пользователя
                {
                    case "Сначала новые": // Если выбрана сортировка от новых к старым
                        query += " ORDER BY CreatedAt DESC"; // Добавление к запросу сортировки по дате создания по убыванию
                        break; // Выход из переключателя switch
                    case "Сначала старые": // Если выбрана сортировка от старых к новым
                        query += " ORDER BY CreatedAt ASC"; // Добавление к запросу сортировки по дате создания по возрастанию
                        break; // Выход из переключателя switch
                    case "По алфавиту (А-Я)": // Если выбрана сортировка по алфавиту в прямом порядке
                        query += " ORDER BY Title ASC"; // Добавление к запросу сортировки по заголовку по возрастанию (от А до Я)
                        break; // Выход из переключателя switch
                    case "По алфавиту (Я-А)": // Если выбрана сортировка по алфавиту в обратном порядке
                        query += " ORDER BY Title DESC"; // Добавление к запросу сортировки по заголовку по убыванию (от Я до А)
                        break; // Выход из переключателя switch
                    case "По приоритету": // Если выбрана сортировка по важности задачи
                        query += @" ORDER BY CASE Priority 
                                    WHEN 'Высокий' THEN 1 
                                    WHEN 'Средний' THEN 2 
                                    WHEN 'Низкий' THEN 3 
                                    ELSE 4 END ASC"; // Добавление к запросу сортировки по числовому весу приоритета (1 - Высокий, 2 - Средний, 3 - Низкий)
                        break; // Выход из переключателя switch
                }
                // Блок подготовки параметров SQL-команды и заполнения таблицы интерфейса
                using (var cmd = new SQLiteCommand(query, conn)) // Создание объекта команды с итоговым текстом SQL-запроса
                {
                    cmd.Parameters.AddWithValue("@search", $"%{txtSearch.Text.Trim()}%"); // Передача текста из поля поиска в параметр @search со знаками % по краям для поиска любой части слова
                    if (selectedFilter != "Все") // Проверка: если активен фильтр по конкретному статусу
                    {
                        cmd.Parameters.AddWithValue("@status", selectedFilter); // Передача выбранного статуса в параметр @status SQL-запроса
                    }
                    // Блок получения данных через адаптер и привязки их к таблице DataGridView
                    using (var adapter = new SQLiteDataAdapter(cmd)) // Создание адаптера данных для выполнения команды и скачивания результата в память
                    {
                        DataTable dt = new DataTable(); // Создание виртуальной таблицы DataTable в оперативной памяти
                        adapter.Fill(dt); // Заполнение виртуальной таблицы данными, полученными из базы данных
                        dgvTasks.DataSource = dt; // Привязка заполненной таблицы DataTable как источника данных для элемента dgvTasks на форме
                        // Блок скрытия служебного столбца идентификатора от пользователя
                        if (dgvTasks.Columns["Id"] != null) // Проверка: существует ли колонка с именем "Id" в таблице на форме
                        {
                            dgvTasks.Columns["Id"].Visible = false; // Скрытие технической колонки "Id", чтобы она не отображалась на экране
                        }
                    }
                }
            }
        }
        // Блок добавления новой записи в базу данных по нажатию кнопки "Добавить"
        private void btnAdd_Click(object sender, EventArgs e) // Обработчик клика по кнопке добавления новой заметки
        {
            // Блок проверки заполненности обязательного поля заголовка
            if (string.IsNullOrWhiteSpace(txtTitle.Text)) // Проверка: пустое ли поле заголовка или состоит только из пробелов
            {
                MessageBox.Show("Введите заголовок задания или заметки!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning); // Вывод всплывающего окна с предупреждением
                return; // Прерывание выполнения метода, чтобы пустая запись не сохранилась
            }
            // Блок подключения к БД и вставки новой строки
            using (var conn = new SQLiteConnection(connectionString)) // Создание соединения с базой данных
            {
                conn.Open(); // Открытие соединения с базой данных
                string insertQuery = @"
                    INSERT INTO Notes (Title, Description, Priority, Status, CreatedAt) 
                    VALUES (@title, @desc, @priority, @status, @date)"; // Текст параметризованного SQL-запроса на добавление новой записи в таблицу Notes
                // Блок передачи значений из полей формы в параметры SQL-запроса
                using (var cmd = new SQLiteCommand(insertQuery, conn)) // Создание SQL-команды для вставки данных
                {
                    cmd.Parameters.AddWithValue("@title", txtTitle.Text.Trim()); // Передача текста заголовка (без лишних пробелов по краям) в параметр @title
                    cmd.Parameters.AddWithValue("@desc", txtDescription.Text.Trim()); // Передача текста описания в параметр @desc
                    cmd.Parameters.AddWithValue("@priority", cmbPriority.SelectedItem.ToString()); // Передача выбранного приоритета в параметр @priority
                    cmd.Parameters.AddWithValue("@status", cmbStatus.SelectedItem.ToString()); // Передача выбранного статуса в параметр @status
                    cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")); // Передача текущей даты и времени компьютера в параметр @date
                    cmd.ExecuteNonQuery(); // Выполнение команды добавления записи в базу данных
                }
            }
            // Блок обновления интерфейса после добавления записи
            ClearInputs(); // Очистка полей ввода на форме
            LoadTasks(); // Перезагрузка таблицы для отображения новой добавленной записи
        }
        // Блок сохранения изменений выбранной записи по нажатию кнопки "Изменить"
        private void btnUpdate_Click(object sender, EventArgs e) // Обработчик клика по кнопке редактирования записи
        {
            // Блок проверки, выбрана ли запись в таблице для редактирования
            if (selectedTaskId == -1) // Проверка: если ID равен -1 (ни одна строка в таблице не выделена)
            {
                MessageBox.Show("Выберите запись в таблице для изменения!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning); // Вывод предупреждения пользователю
                return; // Выход из метода без выполнения обновления
            }
            // Блок проверки, не стер ли пользователь заголовок при редактировании
            if (string.IsNullOrWhiteSpace(txtTitle.Text)) // Проверка: если поле заголовка пустое
            {
                MessageBox.Show("Заголовок не может быть пустым!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning); // Вывод сообщения об ошибке
                return; // Выход из метода
            }
            // Блок подключения к БД и выполнения SQL-запроса на обновление записи
            using (var conn = new SQLiteConnection(connectionString)) // Создание подключения к базе данных
            {
                conn.Open(); // Открытие соединения с базой данных
                string updateQuery = @"
                    UPDATE Notes 
                    SET Title = @title, Description = @desc, Priority = @priority, Status = @status 
                    WHERE Id = @id"; // SQL-запрос на обновление полей записи в таблице Notes по её уникальному идентификатору Id
                // Блок заполнения параметров запроса новыми данными из полей ввода
                using (var cmd = new SQLiteCommand(updateQuery, conn)) // Создание команды SQL для обновления строки
                {
                    cmd.Parameters.AddWithValue("@title", txtTitle.Text.Trim()); // Запись нового заголовка в параметр @title
                    cmd.Parameters.AddWithValue("@desc", txtDescription.Text.Trim()); // Запись нового описания в параметр @desc
                    cmd.Parameters.AddWithValue("@priority", cmbPriority.SelectedItem.ToString()); // Запись нового приоритета в параметр @priority
                    cmd.Parameters.AddWithValue("@status", cmbStatus.SelectedItem.ToString()); // Запись нового статуса в параметр @status
                    cmd.Parameters.AddWithValue("@id", selectedTaskId); // Передача ID редактируемой записи в параметр @id для условия WHERE
                    cmd.ExecuteNonQuery(); // Выполнение SQL-запроса обновления в базе данных
                }
            }
            // Блок сброса полей и обновления таблицы после редактирования
            ClearInputs(); // Очистка полей ввода и сброс выделения
            LoadTasks(); // Обновление списка задач в таблице DataGridView
        }
        // Блок удаления выбранной записи из БД по нажатию кнопки "Удалить"
        private void btnDelete_Click(object sender, EventArgs e) // Обработчик клика по кнопке удаления записи
        {
            // Блок проверки, выбрана ли строка для удаления
            if (selectedTaskId == -1) // Проверка: если ни одна задача не выбрана в таблице
            {
                MessageBox.Show("Выберите запись в таблице для удаления!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning); // Вывод предупреждающего сообщения
                return; // Прерывание выполнения метода
            }
            // Блок запроса подтверждения удаления у пользователя
            var confirm = MessageBox.Show("Вы уверены, что хотите удалить выбранную запись?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question); // Вывод диалогового окна с кнопками "Да" и "Нет"
            if (confirm == DialogResult.Yes) // Проверка: если пользователь нажал кнопку "Да"
            {
                // Блок подключения к БД и удаления записи по её ID
                using (var conn = new SQLiteConnection(connectionString)) // Создание соединения с базой данных
                {
                    conn.Open(); // Открытие соединения с базой данных
                    string deleteQuery = "DELETE FROM Notes WHERE Id = @id"; // SQL-запрос на удаление строки из таблицы Notes по полю Id
                    using (var cmd = new SQLiteCommand(deleteQuery, conn)) // Создание SQL-команды удаления
                    {
                        cmd.Parameters.AddWithValue("@id", selectedTaskId); // Подстановка ID выбранной задачи в параметр @id
                        cmd.ExecuteNonQuery(); // Выполнение удаления записи в базе данных
                    }
                }
                // Блок очистки полей и обновления таблицы после удаления
                ClearInputs(); // Сброс текстовых полей и переменной selectedTaskId
                LoadTasks(); // Перезагрузка данных в таблицу на форме
            }
        }
        // Блок переноса данных из выбранной строки таблицы в поля ввода справа
        private void dgvTasks_SelectionChanged(object sender, EventArgs e) // Обработчик события смены выделенной строки в таблице dgvTasks
        {
            // Блок проверки наличия выделенной строки в таблице
            if (dgvTasks.SelectedRows.Count > 0) // Проверка: выделена ли хотя бы одна строка в DataGridView
            {
                DataGridViewRow row = dgvTasks.SelectedRows[0]; // Получение ссылки на первую выделенную строку таблицы
                // Блок извлечения ID задачи и заполнения элементов управления данными из ячеек строки
                if (row.Cells["Id"].Value != null && int.TryParse(row.Cells["Id"].Value.ToString(), out int id)) // Проверка: не пуста ли ячейка "Id" и удалось ли преобразовать её значение в целое число id
                {
                    selectedTaskId = id; // Сохранение ID выбранной записи в глобальную переменную selectedTaskId
                    txtTitle.Text = row.Cells["Заголовок"].Value?.ToString(); // Копирование текста из ячейки "Заголовок" в текстовое поле txtTitle
                    txtDescription.Text = row.Cells["Описание"].Value?.ToString(); // Копирование текста из ячейки "Описание" в многострочное поле txtDescription
                    cmbPriority.SelectedItem = row.Cells["Приоритет"].Value?.ToString(); // Установка в выпадающем списке cmbPriority значения из ячейки "Приоритет"
                    cmbStatus.SelectedItem = row.Cells["Статус"].Value?.ToString(); // Установка в выпадающем списке cmbStatus значения из ячейки "Статус"
                }
            }
        }
        // Блок обработчика кнопки ручной очистки полей ввода
        private void btnClear_Click(object sender, EventArgs e) // Обработчик нажатия на кнопку "Очистить поля"
        {
            ClearInputs(); // Вызов вспомогательного метода сброса всех полей ввода
        }
        // Блок вспомогательного метода сброса полей ввода в исходное состояние
        private void ClearInputs() // Метод для очистки текстовых полей и снятия выделения с таблицы
        {
            selectedTaskId = -1; // Сброс ID выбранной задачи в значение -1 (ничего не выбрано)
            txtTitle.Clear(); // Очистка текстового поля заголовка
            txtDescription.Clear(); // Очистка текстового поля описания заметки
            cmbPriority.SelectedIndex = 1; // Возврат выпадающего списка приоритета к значению "Средний"
            cmbStatus.SelectedIndex = 0; // Возврат выпадающего списка статуса к значению "В процессе"
            dgvTasks.ClearSelection(); // Снятие синего выделения со строк таблицы DataGridView
        }
        // Блок обработчика изменений в строке поиска, фильтре статуса или сортировке
        private void FilterAndSortChanged(object sender, EventArgs e) // Единый метод-обработчик, срабатывающий при вводе текста в поиск или смене пунктов в фильтрах
        {
            LoadTasks(); // Перезагрузка списка задач из БД с учетом новых параметров поиска, фильтрации и сортировки
        }
    }
}