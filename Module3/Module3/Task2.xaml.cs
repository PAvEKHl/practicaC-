using System;
using System.Windows;
using WpfApp1;

// 2.	Реализуйте систему событий для мобильного приложения.
// Создайте класс "Уведомление" с событиями для отправки уведомлений (сообщения, звонки, электронные письма).
// Зарегистрируйте обработчики событий для разных типов уведомлений.

namespace Module3
{
    public partial class Task2 : Window //partial означает, что класс «разорван» на две части: одна часть описывается визуально в XAML, а вторая — логически в C#.
    {
        private Notification _notification; //экземпляр класса

        public Task2()
        {
            InitializeComponent(); //соединение с XAML

            _notification = new Notification(); //Создание вспомогательного логического объекта для уведомлений.

            // Регистрация обработчиков событий
            _notification.OnMessageSent += HandleMessageNotification; 
            _notification.OnCallMade += HandleCallNotification;
            _notification.OnEmailSent += HandleEmailNotification;
        }

        // Обработчик для события отправки сообщения
        private void HandleMessageNotification(string message)
        {
            if (NotificationResult != null)
            {
                NotificationResult.Text = $"Сообщение отправлено: {message}";
            }
        }

        // Обработчик для события звонка
        private void HandleCallNotification(string phoneNumber)
        {
            if (NotificationResult != null)
            {
                NotificationResult.Text = $"Звонок совершен на номер: {phoneNumber}";
            }
        }

        // Обработчик для события отправки email
        private void HandleEmailNotification(string email)
        {
            if (NotificationResult != null)
            {
                NotificationResult.Text = $"Gmail отправлен на: {email}";
            }
        }

        // Кнопка для отправки сообщения
        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            _notification.SendMessage("Сообщение вывелось с помощью делегата!");
        }

        // Кнопка для совершения звонка
        private void MakeCall_Click(object sender, RoutedEventArgs e)
        {
            _notification.MakeCall("+37525493254");
        }

        // Кнопка для отправки email
        private void SendEmail_Click(object sender, RoutedEventArgs e)
        {
            _notification.SendEmail("HlebkoPavel.com");
        }
    }
}
