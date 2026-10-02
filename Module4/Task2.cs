using System;

//2.	Разработайте приложение для учета продуктов в магазине с использованием интерфейсов.
//Создайте интерфейс "Товар" с методами для определения стоимости и остатка товара на складе.
//Реализуйте этот интерфейс в классах различных товаров.
class Task2
{
    interface IProduct { decimal GetPrice(); int GetStock(); }

    class Food : IProduct { decimal p; int s; public Food(decimal p, int s) => (this.p, this.s) = (p, s); public decimal GetPrice() => p; public int GetStock() => s; }
    class Tech : IProduct { decimal p; int s; public Tech(decimal p, int s) => (this.p, this.s) = (p, s); public decimal GetPrice() => p; public int GetStock() => s; }

    public static void Main()
    {
        IProduct[] stock = { new Food(150.5m, 100), new Tech(45000m, 5) };
        foreach (var p in stock) Console.WriteLine($"Цена: {p.GetPrice()}, На складе: {p.GetStock()}");
    }
}