using System;
//Задача 1. Определить функцию (статический метод) для вычисления наибольшего общего
//делителя двух целых натуральных чисел (Greatest Common Measure).
//В основной программе, используя функцию, сократить неотрицательную обыкновенную дробь.
//Дробь вводится с клавиатуры в виде неотрицательного числителя и положительного знаменателя
class Task1
{
    static int GetGcm(int a, int b) //метод  ане ф-ия т.к. мы в классе на уроке)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    static void Main()
    {
        Console.Write("Числитель: ");
        int num = int.Parse(Console.ReadLine());
        Console.Write("Знаменатель: ");
        int den = int.Parse(Console.ReadLine());

        int nod = GetGcm(num, den);
        Console.WriteLine($" НОД: {nod}"); // Делим числитель и знаменатель на их общий делитель
        Console.WriteLine($"Сокращенная дробь: {num / nod}/{den / nod}");
    }
}