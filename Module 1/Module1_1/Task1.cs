using System;
// Факториал — это произведение всех натуральных чисел от 1 до данного числа включительно
// 1.	Реализуйте приложение для вычисления факториала числа.2
class Task1
{
    static void Main()
    {
        Console.Write("Введите конечное число: ");
        int n = int.Parse(Console.ReadLine());
        if ( n >= 0)
        {
            long factorial = 1;
            for (int i = 2; i <= n; i++)
            {
                factorial *= i;
            }
            Console.WriteLine($"{n}! = {factorial}");
        }
        else
        {
            Console.WriteLine("Ошибка: введено некорректное число.");
        }
    }
}