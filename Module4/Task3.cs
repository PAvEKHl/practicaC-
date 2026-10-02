using System;
//3.	Создайте систему учета студентов в университете с помощью интерфейсов.
//Создайте интерфейс "Студент" с методами для определения среднего балла и получения информации о курсе.
//Реализуйте этот интерфейс в классах студентов разных курсов.
class Task3
{
    interface IStudent { double GetGpa(); string GetCourseInfo(); }

    class Bachelor : IStudent { double gpa; int year; public Bachelor(double gpa, int year) => (this.gpa, this.year) = (gpa, year); public double GetGpa() => gpa; public string GetCourseInfo() => $"{year}-й курс бакалавриата"; }
    class Master : IStudent { double gpa; int year; public Master(double gpa, int year) => (this.gpa, this.year) = (gpa, year); public double GetGpa() => gpa; public string GetCourseInfo() => $"{year}-й курс магистратуры"; }

    public static void Main()
    {
        IStudent[] students = { new Bachelor(8.5, 3), new Master(9.2, 1) };
        foreach (var s in students) Console.WriteLine($"{s.GetCourseInfo()} | Средний балл: {s.GetGpa()}");
    }
}