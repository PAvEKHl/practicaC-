using System;
//2.	Напишите программу, которая запрашивает у пользователя два целых числа и выводит их сумму.
class Task2
{
    static void Main()
    {
        int sum = 0;
        Console.Write("Введите первое целое число: ");
        sum +=  int.Parse(Console.ReadLine());

        Console.Write("Введите второе целое число: ");
        sum += int.Parse(Console.ReadLine());

        Console.WriteLine($"Сумма = {sum}");
    }
}