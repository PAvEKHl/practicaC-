using System;
//5. Напишите программу, которая проверяет, является ли введенное
// пользователем число простым (не имеет делителей, кроме 1 и самого себя).
class Task5
{
    static void Main()
    {
        while (true)
        {
            Console.Write("Введите целое число: ");
            if (!int.TryParse(Console.ReadLine(), out int n))
            {
                Console.WriteLine("Некорректный ввод.");
                return; //завершаем работу
            }

            bool isPrime = true;

            if(n >= 3)
            {
                for (int i = 2; i * i <= n; i++)
                {
                    if (n % i == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    Console.WriteLine($"Число {n} — простое.");
                }
                if (!isPrime)
                {
                    Console.WriteLine($"Число {n} — составное (не является простым).");
                }
            }
            else
            {
                Console.WriteLine($"Число {n} — составное (не является простым).");
            }
        }
    }
}