using System;

namespace Module3
{
    public class Notification
    {
        // События
        public event Action<string> OnMessageSent;
        public event Action<string> OnCallMade;
        public event Action<string> OnEmailSent;

        // Методы для вызова событий
        public void SendMessage(string message)
        {
            OnMessageSent?.Invoke(message); //.Invoke пробегает по всему списку подписанных методов и по очереди запускает их.  
        }

        public void MakeCall(string phoneNumber)
        {
            OnCallMade?.Invoke(phoneNumber);
        }

        public void SendEmail(string email)
        {
            OnEmailSent?.Invoke(email);
        }
    }
}