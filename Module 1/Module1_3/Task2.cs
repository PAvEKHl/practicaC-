using System;
// Задача 2. Присваивая последовательным элементам массива случайные значения от 1 до 9
// создать массив с минимальным количеством элементов, сумма которых не превышает заданного пользователем числа.
class Task2
{
    static void Main()
    {
        Console.Write("Введите число (предельную сумму): ");
        int max = int.Parse(Console.ReadLine());

        // Максимум элементов не превысит самого числа
        int[] arr = new int[max];
        int[] min = new int[max];
        int count = 0;
        int sum = 0;
        Random rnd = new Random();

        Console.WriteLine($"Исходный массив:");
        for (int i = 0; i < max; i++) { //Присваивем последовательным элементам массива случайные значения от 1 до 9
            int val = rnd.Next(1, 10); 
            arr[i] = val;
            Console.Write($"arr({i}):{arr[i]}, ");
        }
        Console.WriteLine(" \n------------------------------------------");
        for (int i = 9; i > 0; i--) { //пробираем все вариан-ты чисел от большего
                for (int j = 0; j < arr.Length; j++) {  // проверяем каждый эл-нт
                    if (sum + arr[j] > max) {
                        break;
                    }
                    if (arr[j] == i ) {
                            sum += arr[j];
                            min[count] = arr[j];
                            count++;
                    }
                }
        }
            
        Console.Write(" \n Массив: ");
        for (int i = 0; i < count; i++)
            Console.Write(min[i] + " ");

        Console.WriteLine($"\nЭлементов: {count}, Итоговая сумма: {sum}");
    }
}