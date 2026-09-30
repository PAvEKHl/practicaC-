using System;
//Задача 3. Вычислить К простых чисел. Значение К ввести с клавиатуры.
//Вывести значения чисел, размещая их по 10 на строке
class Task3
{
    static void Main()
    {
        Console.Write("Введите K: ");
        int k = int.Parse(Console.ReadLine());

        int count = 0, n = 2;
        while (count < k) 
        {
            bool isPrime = true;
            for (int i = 2; i * i <= n; i++)
            {
                if (n % i == 0) { isPrime = false; break; } // 8 % 2 == 0 (true)
            }

            if (isPrime)
            {
                Console.Write($"{n,5} ");
                count++;
                if (count % 10 == 0) Console.WriteLine();
            }
            n++;
        }
    }
}