using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace энциклопедияПоГайдамЮтюб
{
    public partial class MoreDetailed : Form
    {
        int indexpage = 0;  //добавил для понимания строки в БД 
        public MoreDetailed(int indexpage_nmb)
        {
            InitializeComponent();
            indexpage = indexpage_nmb;

        }

      
        private void MoreDetailed_Load(object sender, EventArgs e)//Текущее значение строки
        {
            
            label2.Text = indexpage.ToString();

            // Загружаем данные в элемент
            DetailedINF();
            DetailedSpecifications();
        }
        private Image GetImageFromBytes(object readerValue)
        {
            //  Проверяю, что данные из базы — это действительно массив байт
            if (readerValue is byte[] bytes)
            {
                try
                {
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        return Image.FromStream(ms);
                    }
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        private void DetailedINF()
        {
            string dbPath = @"Data Source=airplanes.db";
            string sqlExpression = "SELECT * FROM More WHERE ID = " + indexpage;

            using (var connection = new SqliteConnection(dbPath))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        mainInfoElement1.Descriptions = reader.IsDBNull(1) ? "Нету основной инфы" : reader.GetString(1);
                        mainInfoElement1.Internetfacts = reader.IsDBNull(2) ? "Нету фактов" : reader.GetString(2);

                        // Передаем в метод чистый объект reader[индекс]. 
                        // Теперь нет принудительного приведения к (byte[]), и падать нечему!
                        mainInfoElement1.Icon1 = reader.IsDBNull(3) ? null : GetImageFromBytes(reader[3]);
                        mainInfoElement1.Icon2 = reader.IsDBNull(4) ? null : GetImageFromBytes(reader[4]);
                        mainInfoElement1.Icon3 = reader.IsDBNull(5) ? null : GetImageFromBytes(reader[5]);
                        mainInfoElement1.Icon4 = reader.IsDBNull(6) ? null : GetImageFromBytes(reader[6]);
                    }
                }
            }
        }
        private void DetailedSpecifications()
        {
            string dbPath = @"Data Source=airplanes.db";
            string sqlExpression = "SELECT * FROM Specifications WHERE ID = " + indexpage; //для понимания номера сомолёта 

            using (var connection = new SqliteConnection(dbPath))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        specificationsElements1.Wingspan = reader.IsDBNull(1) ? "Размах крыльев" : reader.GetString(1) + " м";
                        specificationsElements1.Length = reader.IsDBNull(2) ? "Длина самолёта = N" : reader.GetString(2) + " м";
                        specificationsElements1.HeightSpec = reader.IsDBNull(3) ? "Высота самолёта = N" : reader.GetString(3) + " м" ;
                        specificationsElements1.Weight = reader.IsDBNull(4) ? "Вес самолёта = N" : reader.GetString(4) + " кг";
                        specificationsElements1.Fuel = reader.IsDBNull(5) ? "Топливо самолёта = N" : reader.GetString(5) + " л/кг";
                        specificationsElements1.engineType = reader.IsDBNull(6) ? "Текущий тип двигателя = Куросин/Бензин" : reader.GetString(6);
                        specificationsElements1.maximumSpeed = reader.IsDBNull(7) ? "Максимальная скорость самолёта = N" : reader.GetString(7) + " км/ч";
                        specificationsElements1.cruisingSpeed = reader.IsDBNull(8) ? "Крейсерная скорость" : reader.GetString(8) + " км/ч";
                        specificationsElements1.crewPeople = reader.IsDBNull(9) ? "Экипаж кол-во чел" : reader.GetString(9) + " чел";
                        specificationsElements1.serviceLife = reader.IsDBNull(10) ? "Срок эксплкатации" : reader.GetString(10) + " лет/год";
                        specificationsElements1.unitCost = reader.IsDBNull(11) ? "Стоимость единицы" : reader.GetString(11) + " тыс $";
                        specificationsElements1.Armament = reader.IsDBNull(12) ? "Вооружение" : reader.GetString(12);                    
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }        
}
