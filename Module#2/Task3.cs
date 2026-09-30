using System;

// Задача 3: Композиция
// Создайте классы Author и Book.
// Класс Author должен содержать информацию об авторе (имя и год рождения).
// Класс Book должен содержать информацию о книге (название, год выпуска и автора).
// Используйте композицию, чтобы связать объекты Author и Book.
// Создайте несколько объектов Author и Book и выведите информацию о них.

namespace Task3App
{
    class Author // Класс Author должен содержать информацию об авторе (имя и год рождения)
    {
        public string Name { get; set; }
        public int BirthYear { get; set; }

        public Author(string name, int birthYear) => (Name, BirthYear) = (name, birthYear); //конструктор
    }
    class Book // Класс Book должен содержать информацию о книге (название, год выпуска и автора).
    {
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public Author Author { get; set; } // Композиция: книга ссылается на автора (ссылка на обьект Author)

        public Book(string title, int releaseYear, Author author) => // конструктор
            (Title, ReleaseYear, Author) = (title, releaseYear, author);

        public void DisplayInfo() =>
            Console.WriteLine($"Книга: {Title}, Год: {ReleaseYear}, Автор: {Author.Name} ({Author.BirthYear} г.р.)");
    }
    class Task3
    {
        static void Main()
        {
            // Создайте объектов Author и Book и вывод информации о них.
            // Первый автор и его книга
            Author author1 = new Author("Владимир Маяковский", 1893);
            Book book1 = new Book("Облако в штанах", 1915, author1);
            // Второй автор и его книга
            Author author2 = new Author("Михаил Булгаков", 1891);
            Book book2 = new Book("Мастер и Маргарита", 1967, author2);
            // Вывод информации о книгах
            book1.DisplayInfo(); book2.DisplayInfo();
        }
    }
}