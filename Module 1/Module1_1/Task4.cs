using System;
//4.	Напишите программу, которая создает массив из 15 случайных чисел и находит среднее значение всех положительных чисел в массиве.
class Task4
{
    static void Main()
    {
        int[] numbers = new int[15];
        Random rnd = new Random();

        // Заполнение случайными числами в диапазоне от -50 до 50
        Console.WriteLine("Исходный массив:");
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = rnd.Next(-50, 51);
            Console.Write($"arr{i} = {numbers[i]}, ");
        }

        // находит среднее значение всех положительных чисел в массиве.
        double positiveSum = 0;
        double positiveCount = 0;
        foreach (int num in numbers)
        {
            if (num > 0)
            {
                positiveSum += num;
                positiveCount++; //кол-во эл-ов больше нуля
            }
        }

        Console.WriteLine(" \n--------------------------------");
        string result = (positiveCount > 0)
            ? $"Количество положительных чисел: {positiveCount}\nСреднее арифметическое: {positiveSum / positiveCount}"
            : "В массиве нет положительных чисел.";
        Console.WriteLine(result);
    }
}