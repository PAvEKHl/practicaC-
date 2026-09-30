using System;

// Задача 6: Структуры
// Создайте структуру "Студент" со свойствами: имя, фамилия, возраст и средний балл.
// Создайте несколько экземпляров этого класса и выведите информацию о них.

namespace Task6App
{
    struct Student
    {
        // Поля структуры
        public string Name;
        public string SurName;
        public int Age;
        public double AverageScore;

        public Student(string name, string surName, int age, double averageScore) =>
            (Name, SurName, Age, AverageScore) = (name, surName, age, averageScore);

        public void WriteUserInfo()
        {
            Console.WriteLine($"\nСтудент: {Name} {SurName}");
            Console.WriteLine($"Возраст: {Age}, Средний балл: {AverageScore}");
        }
    }
    class Task6
    {
        static void Main()
        {
            // Создайте несколько экземпляров этого класса и выведите информацию о них.
            Console.Write("Введите имя студента#1: ");
            string name1 = Console.ReadLine();
            Console.Write("Введите имя студента#2: ");
            string name2 = Console.ReadLine();

            Console.Write("Введите фамилию студента#1: ");
            string surName1 = Console.ReadLine();
            Console.Write("Введите фамилию студента#2: ");
            string surName2 = Console.ReadLine();

            Console.Write("Введите возраст студента#1: ");
            int age1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите возраст студента#2: ");
            int age2 = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите средний балл студента#1: ");
            double score1 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Введите средний балл студента#2: ");
            double score2 = Convert.ToDouble(Console.ReadLine());

            // Создание экземпляров структуры и вывод
            Student student1 = new Student(name1, surName1, age1, score1);
            student1.WriteUserInfo();
            Student student2 = new Student(name2, surName2, age2, score2);
            student2.WriteUserInfo();

        }
    }
}