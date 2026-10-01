using System;

// Задача 1: Создание классов
// Создайте класс Person, представляющий человека.
// У этого класса должны быть поля для хранения имени, возраста и адреса.
// Добавьте методы для установки и получения значений этих полей.
// Затем создайте объекты этого класса и выведите информацию о них.

namespace Task1App
{
    class Person1
    {
        // Поля для хранения данных
        private string _name;
        private int _age;
        private string _address;

        // Методы установки значений (сеттеры)
        public void SetName(string name) => _name = name; //void --> ничего не возвращает
        public void SetAge(int age) => _age = age;
        public void SetAddress(string address) => _address = address;

        // Методы получения значений (геттеры)
        public string GetName() => _name;
        public int GetAge() => _age;
        public string GetAddress() => _address;

        // Вывод информации на экран
        public void DisplayInfo() => Console.WriteLine($"Имя: {_name}, Возраст: {_age}, Адрес: {_address}"); //тут и срабатывают Get-методы
    }

    class Task1
    {
        static void Main()
        {
            Person1 person = new Person1(); //экземпляр/обьект класса)
            person.SetName("Паша");
            person.SetAge(17);
            person.SetAddress("Орша");
            person.DisplayInfo();
            string name = person.GetName();
            Console.WriteLine($"Имя мне: {name}");
            string address = person.GetAddress();
            Console.WriteLine($"Найти меня можно в городе: {address}");
        }
    }
}