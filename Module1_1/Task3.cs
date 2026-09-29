using System;
//3.	Напишите программу, которая принимает на вход строку и выводит ее в обратном порядке.
class Task3
{
    static void Main()
    {
        Console.Write("Введите строку: ");
        string inputUsr = Console.ReadLine();
        char[] chars = inputUsr.ToCharArray(); // Преобразует строку в массив символов(строки в C# неизменяемы)
        // j должно начинаться с последнего индекса (Length - 1)
        for (int i = 0, j = chars.Length - 1; i < j; i++, j--)
        {
            // Правильный обмен местами через temp
            char temp = chars[i];   // прячем левую букву
            chars[i] = chars[j];    // на место левой ставим правую
            chars[j] = temp;        // на место правой ставим спрятанную левую
        }
        string reversed = new string(chars);
        Console.Write($"Слово наоборот будет: {reversed}");
    }
}