using System;

// Задача 5. Определить символьный массив из К элементов.
// Присвоить элементам случайные значения букв русского алфавита.
// Создать новый массив, поместив в него только согласные буквы из первого массива.
// Значение К ввести с клавиатуры. Вывести элементы обоих массивов.
class Task5
{
    static void Main()
    {
        string rus = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        string cons = "бвгджзйклмнпрстфхцчшщ";

        Console.Write("Размер K: ");
        int k = int.Parse(Console.ReadLine());
        char[] original = new char[k];

        Random rnd = new Random();
        for (int i = 0; i < k; i++)
        {
            original[i] = rus[rnd.Next(rus.Length)]; // Присвоить элементам случайные значения букв русского алфавита.
        }

        Console.WriteLine($"Исходный массив: {string.Join(", ",original)}");

        // 1. Считаем, сколько согласных выпало, чтобы знать размер нового массива
        int count = 0;
        for (int i = 0; i < k; i++)
        {
            if (cons.Contains(original[i])) //.Contains --> проверка на содержание 
            {
                count++;
            }
        }

        // 2. Заполняем новый массив согласными буквами из 1-го массива
        char[] onlyCons = new char[count];
        int index = 0; //индекс для нового массива
        for (int i = 0; i < k; i++) //индекс эл-на исход-го массива
        {
            if (cons.Contains(original[i]))
            {
                onlyCons[index] = original[i];
                index++;
            }
        }

        Console.WriteLine("Массив согласных: " + string.Join(" ", onlyCons));
    }
}