using System;
//4.	Реализуйте приложение для работы с библиотекой книг с использованием интерфейсов.
//Создайте интерфейс "Книга" с методами для проверки доступности и выдачи книги.
//Реализуйте этот интерфейс в классах различных книг.
class Task4
{
    interface IBook { bool IsAvailable(); void Checkout(); }

    class PaperBook : IBook { bool avail = true; public bool IsAvailable() => avail; public void Checkout() => avail = false; }
    class EBook : IBook { public bool IsAvailable() => true; public void Checkout() => Console.WriteLine("Электронная копия скачана."); }

    public static void Main()
    {
        IBook[] lib = { new PaperBook(), new EBook() };
        foreach (var b in lib)
        {
            if (b.IsAvailable()) b.Checkout();
            Console.WriteLine($"Осталась доступна? {b.IsAvailable()}");
        }
    }
}