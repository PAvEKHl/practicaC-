using System;
//Задача 1. Ввести размер массива N и значения его элементов.
//Нормировать элементы массива, разделив их на значение максимального по модулю элемента.
//Вывести значения элементов измененного массива.
class Task1
{
    //Нормирование элементов массива выполняется путем поиска максимального абсолютного значения (по модулю)
    //и последующего деления каждого элемента на это числ
    static void Main()
    {
        //создаём массив и заполняем его данными
        Console.Write("Введите размер массива N: ");
        if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
        {
            Console.WriteLine("Ошибка: некорректный размер массива.");
            return;
        }
        double[] array = new double[n];
        Console.WriteLine($"Введите {n} элементов массива (через Enter):");
        for (int i = 0; i < n; i++)
        {
            Console.Write($"array[{i}] = ");
            while (!double.TryParse(Console.ReadLine(), out array[i]))
            {
                Console.Write($"Некорректное значение. Повторите array[{i}] = ");
            }
        }

        // Поиск максимального по модулю элемента
        double maxAbs = Math.Abs(array[0]);
        for (int i = 1; i < n; i++)
        {
            if (Math.Abs(array[i]) > maxAbs)
            {
                maxAbs = Math.Abs(array[i]);
            }
        }

        // Нормирование массива
        for (int i = 0; i < n; i++)
        {
            array[i] /= maxAbs;
        }
        Console.WriteLine("Нормированный массив:");
        for (int i = 0; i < n; i++)
        {
            Console.Write($"array[i]: {array[i]:f4}, ");
        }
        Console.WriteLine();
    }
}