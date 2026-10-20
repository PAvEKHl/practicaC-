using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;// Бд
using System.Drawing; //Работа с картинками   код item.Icon = Image.FromStream(ms); 
using System.IO; //Работа с картинками   код чють выше чем для библиотки (конструкция using) System.Drawing; using (MemoryStream ms = new MemoryStream(Photo))
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace энциклопедияПоГайдамЮтюб
{
    public partial class Form1 : Form
    {
        private List<ListItem> listItems = new List<ListItem>(); // Объявляем массив как поле класса
        private List<SpecificationsElements> listItemsSpecifications = new List<SpecificationsElements>(); // Объявляем массив как поле класса

        public Form1()
        {
            InitializeComponent();

        }
     
        private void listItem3_Load_1(object sender, EventArgs e)
        {
            AllAirplanes();
            flowLayoutPanel1.Controls.Clear(); // Очищаем панель
            for (int i = 0; i < listItems.Count; i++) //вывод всего бд в  flowLayoutPanel1
            {
                flowLayoutPanel1.Controls.Add(listItems[i]);
            }
        }
        private void button1_Click(object sender, EventArgs e) // Кнопка Применить
        {
            AllAirplanes();
            AllISpecifications();
            FilterPlane();
        }
        private void button2_Click(object sender, EventArgs e) //Кнопка сбросить
        {
            resetting_filters();
        }
        private void button3_Click(object sender, EventArgs e) //Кнопка Найти
        {
            AllAirplanes();
            flowLayoutPanel1.Controls.Clear(); // Очищаем список перед поиском
            string search = textBox1.Text.ToLower().Trim(); // Текст поиска в нижний регистр ToLower()  и удаление случайных пробелов в начале и конуе

            for (int i = 0; i < listItems.Count; i++)
            {
                if (listItems[i] == null) continue; //чтобы прога не встала,вроде это логично но не уверен)
                string fullData = (listItems[i].Title + " " + listItems[i].Message + " " + listItems[i].Country + " "
                    + listItems[i].Category + " " + listItems[i].YearProduction).ToLower();
                // 3. Проверяем наличие подстроки
                if (fullData.Contains(search))
                {
                    flowLayoutPanel1.Controls.Add(listItems[i]);
                }
            }
        }
        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {


        }
        private void FilterPlane()
        {
            flowLayoutPanel1.Controls.Clear();
            //для Года выпуска (диапозон пгроничных значений)
            decimal count_from_Year = YearfromDown.Value;
            decimal count_to_Year = YearToDown.Value;
            //для Стоимости единицы(диапозон пгроничных значений)
            decimal count_from_nmbr_unitCost = unitCostfromDown.Value;
            decimal count_to_nmbr_unitCost = unitCostToDown.Value;
            //для Макс.Скорости(диапозон пгроничных значений)
            decimal count_from_maximumSpeed = maximumSpeedfromDown.Value;
            decimal count_to_maximumSpeed = maximumSpeedToDown.Value;
            //для масса(диапозон пгроничных значений)
            decimal count_from_Weight = WeightfromDown.Value;
            decimal count_to_Weight = WeightToDown.Value;
            //присвоение переменным bool значения ListBox1/2 галочки по индексу в фильтрах (Активна - True)
            //для  категории
            bool Military_bool = checkedListBox1.GetItemChecked(0);
            bool Passenger_bool = checkedListBox1.GetItemChecked(1);
            bool MultipPurpose_bool = checkedListBox1.GetItemChecked(2);

            //для страны
            bool Russia_bool = checkedListBox2.GetItemChecked(0);
            bool USA_bool = checkedListBox2.GetItemChecked(1);
            bool Europe_bool = checkedListBox2.GetItemChecked(2);
            bool France_bool = checkedListBox2.GetItemChecked(3);
            bool Sweden_bool = checkedListBox2.GetItemChecked(4);
            bool China_bool = checkedListBox2.GetItemChecked(5);
            bool Brazil_bool = checkedListBox2.GetItemChecked(6);
            //для типа топлива
            bool kerosene_bool = checkedListBox3.GetItemChecked(0);
            bool Automobile_gasoline_bool = checkedListBox3.GetItemChecked(1);
            bool Aviation_gasoline_bool = checkedListBox3.GetItemChecked(2);


            bool checked_category = Military_bool || Passenger_bool || MultipPurpose_bool; //bool для anyChecked и условия выбранны только страны
            bool checked_counry = Russia_bool || USA_bool || Europe_bool || France_bool || Sweden_bool || China_bool || Brazil_bool;  //bool для условия выбранны только категории
            bool checked_fuel_type = kerosene_bool || Automobile_gasoline_bool || Aviation_gasoline_bool; // истина если выбран тип топлива           
            bool anyChecked = checked_category || checked_counry || checked_fuel_type;  // Если ни одна галочка не выбрана


            for (int i = 0; i < listItemsSpecifications.Count; i++) //так как в таблицу Specifications строк заполнено меньше
            {
                if (listItems[i] == null) return;
                //присвоение переменным  значений для текущего элемента из таблицы Plane
                string current_сategory = listItems[i].Category;
                string current_сountries = listItems[i].Country;
                int current_YearProduction = int.Parse(listItems[i].YearProduction);
                //присвоение переменным  значений для текущего элемента из таблицы Specifications
                string current_fuel_type = listItemsSpecifications[i].Tipefuel;
                int current_Cost = int.Parse(listItemsSpecifications[i].unitCost);
                int current_Weight = int.Parse(listItemsSpecifications[i].Weight);
                int current_maximumSpeed = int.Parse(listItemsSpecifications[i].maximumSpeed);

                // bool совподения фильров выбранных пользователем с данными о самолётах(из БД)
                //для таблицы Plane
                bool categoryAll_bool = (current_сategory == "Военный" && Military_bool || current_сategory == "Пассажирский" && Passenger_bool || current_сategory == "Многоцелевой" && MultipPurpose_bool);
                bool countiesAll_bool = (current_сountries == "Россия" && Russia_bool || current_сountries == "США" && USA_bool || current_сountries == "ЕС" && Europe_bool || current_сountries == "Франция" && France_bool || current_сountries == "Швеция" && Sweden_bool || current_сountries == "Китай" && China_bool || current_сountries == "Бразилия" && Brazil_bool);
                bool YearProduction_bool = (current_YearProduction >= count_from_Year && current_YearProduction <= count_to_Year);
                //для таблицы Specifications
                bool fuel_type_bool = (current_fuel_type == "авиационный керосин" || current_fuel_type == "автомобильный бензин" || current_fuel_type == "авиационный бензин");
                bool unitCost_type_bool = (current_Cost >= count_from_nmbr_unitCost && current_Cost <= count_to_nmbr_unitCost);
                bool maximumSpeed_type_bool = (current_maximumSpeed >= count_from_maximumSpeed && current_maximumSpeed <= count_to_maximumSpeed);
                bool Weight_type_bool = (current_Weight >= count_from_Weight && current_Weight <= count_to_Weight);

                // логика отсева для lisItem при нажатии на кнопку "применить".
                if (YearProduction_bool && (!checked_category || categoryAll_bool) && (!checked_counry || countiesAll_bool) && unitCost_type_bool && maximumSpeed_type_bool && Weight_type_bool)
                {
                    flowLayoutPanel1.Controls.Add(listItems[i]);
                }
            }
        }

        private void AllAirplanes()
        {
            string sqlExpression = "SELECT * FROM Planes";
            listItems.Clear(); // Очищаем список перед новым чтением, чтобы данные не дублировались

            using (var connection = new SqliteConnection(@"Data Source=airplanes.db"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        ListItem item = new ListItem(); // Создаем объект
                        // Используем IsDBNull, чтобы программа не "падала"
                        item.Id = reader.GetInt32(0);
                        item.Message = reader.IsDBNull(1) ? "Без описания" : reader.GetString(1);
                        item.Country = reader.IsDBNull(2) ? "Неизвестно" : reader.GetString(2);
                        item.Category = reader.IsDBNull(3) ? "Общая" : reader.GetString(3);
                        item.YearProduction = reader.IsDBNull(4) ? "0" : reader.GetValue(4).ToString();
                        if (!reader.IsDBNull(5))
                        {
                            try
                            {
                                byte[] Photo = (byte[])reader[5];
                                using (MemoryStream ms = new MemoryStream(Photo))
                                {
                                    item.Icon = Image.FromStream(ms);
                                }
                            }
                            catch { /* Если картинка битая */ }
                        }
                        item.BtnDetail = "Подробнее";
                        item.Title = reader.IsDBNull(6) ? "Title" : reader.GetString(6);

                        listItems.Add(item); // Добавляем готовый самолет в наш список
                    }
                }
            }
        }
        private void AllISpecifications()
        {
            string sqlExpression = "SELECT * FROM Specifications";
            listItemsSpecifications.Clear(); // Очищаем список перед новым чтением, чтобы данные не дублировались

            using (var connection = new SqliteConnection(@"Data Source=airplanes.db"))
            {
                connection.Open();
                SqliteCommand commandSp = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = commandSp.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SpecificationsElements item = new SpecificationsElements(); // Создаем объект
                        // извликаем данные из таблицы Specifications 
                        item.Weight = reader.GetValue(4).ToString();  // Используем IsDBNull, чтобы программа не "падала"                      

                        item.maximumSpeed = reader.GetValue(7).ToString();
                        item.unitCost = reader.GetValue(11).ToString();
                        item.Tipefuel = reader.GetString(13);
                        listItemsSpecifications.Add(item); // Добавляем данные из бд в массив списков
                    }
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void resetting_filters()
        {

            //Убираем галочки для checkedListBox
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                checkedListBox2.SetItemChecked(i, false);
            }
            for (int i = 0; i < checkedListBox3.Items.Count; i++)
            {
                checkedListBox2.SetItemChecked(i, false);
            }
            //Сбрасываем года на стандартные
            YearfromDown.Value = 1990;
            YearToDown.Value = 2026;
            //Стокавая стоимость еденицы
            unitCostfromDown.Value = 1;
            unitCostToDown.Value = 987654;
            //Стоковые значения для скорости
            maximumSpeedfromDown.Value = 1;
            maximumSpeedToDown.Value = 9876;
            //Стоковые значчения стоимости еденицы
            WeightfromDown.Value = 1;
            WeightToDown.Value = 98765;
            //Очищаем вывод данных перед предостовлением полного списка из БД
            flowLayoutPanel1.Controls.Clear();
            AllAirplanes();
            for (int i = 0; i < listItems.Count; i++) //вывод всего бд в  flowLayoutPanel1
            {
                flowLayoutPanel1.Controls.Add(listItems[i]);
            }
            textBox1.Clear();
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Берем путь к папке bin\Debug и добавляем точное имя файла
            string helpPath = System.IO.Path.Combine(Application.StartupPath, "справка_CHM.chm");

            // Проверяем, нашла ли программа файл в своей папке
            if (System.IO.File.Exists(helpPath))
            {
                Help.ShowHelp(this, helpPath);
            }
            else
            {
                // Если файла там нет, ты увидишь это окно со встроенным точным путем
                MessageBox.Show($"Файл справки не найден! Положите его по пути:\n{helpPath}", "Ошибка поиска файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Если нажали F1
            if (e.KeyCode == Keys.F1)
            {
                string helpPath = System.IO.Path.Combine(Application.StartupPath, "справка_CHM.chm");

                if (System.IO.File.Exists(helpPath))
                {
                    Help.ShowHelp(this, helpPath);
                }
                else
                {
                    MessageBox.Show($"Файл справки не найден! Положите его по пути:\n{helpPath}", "Ошибка поиска файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}