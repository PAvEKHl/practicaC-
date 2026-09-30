using System;
// Задача 4. Определить целочисленный массив из К элементов.
// Присвоить элементам случайные значения из диапазона [А, В).
// Найти индексы минимального и максимального элементов массива.
// Вывести значения элементов, расположенных между найденными(включая найденные)
class Task4
{
    static void Main()
    {
        Console.Write("Введите размер массива K: ");
        int k = int.Parse(Console.ReadLine());
        Console.Write("Введите начало диапазона A: ");
        int a = int.Parse(Console.ReadLine());
        Console.Write("Введите конец диапазона B: ");
        int b = int.Parse(Console.ReadLine());

        int[] arr = new int[k];
        Random rnd = new Random();
        for (int i = 0; i < k; i++) arr[i] = rnd.Next(a, b); // Присвоить элементам случайные значения из диапазона [А, В).
        Console.WriteLine("Исходный массив:");
        for (int i = 0; i < k; i++)
        {
            Console.Write($"arr[{i}] = {arr[i]}, ");
        }

        // Найти индексы минимального и максимального элементов массива.
        int minIdx = 0, maxIdx = 0;
        for (int i = 1; i < k; i++)
        {
            if (arr[i] < arr[minIdx]) minIdx = i;
            if (arr[i] > arr[maxIdx]) maxIdx = i;
        }
        // массив генерируется случайно. Поэтому Math.Min чтобы  максимум не оказался в начале массива, а минимум — в конце
        int start = Math.Min(minIdx, maxIdx);
        int end = Math.Max(minIdx, maxIdx);
        Console.WriteLine("");
        Console.WriteLine("------------------------- ");
        Console.WriteLine("Элементы между ними: ");
        for (int i = start; i <= end; i++)
            Console.Write($"arr[{i}] = {arr[i]}, ");
    }
}