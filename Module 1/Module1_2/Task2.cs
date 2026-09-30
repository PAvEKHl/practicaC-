using System;
//Задача 2. Определить и инициализировать целочисленный массив из 10-ти элементов.
//Ввести целое число и заменить им значение максимального элемента в массиве.
class Task2
{
    static void Main()
    {
        Console.Write("Введите чсло, которым заменить макс по знач. элемент: ");
        int user_input = int.Parse(Console.ReadLine());

        int[] arr = new int[10];
        Random rand = new Random(); //объект класса Random
        int maxELm_Index = 0;
        Console.WriteLine("Исходный массив: ");
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = rand.Next(-100, 101);
            Console.Write($"arr[{i}] = {arr[i]}, ");
            if (arr[i] > arr[maxELm_Index])
            {
                maxELm_Index = i;
            }
        }
        arr[maxELm_Index] = user_input;

        Console.WriteLine("-----------------------------");

        Console.WriteLine($"Элемент #{maxELm_Index} заменён на ваше число!");
        Console.WriteLine("Новый массив: ");
        for (int i = 0; i < arr.Length; i++)
        {
            Console.Write($"arr[{i}] = {arr[i]}, ");
        }
    }
}