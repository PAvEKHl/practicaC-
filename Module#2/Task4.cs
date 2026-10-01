using System;

// Задача 4: Интерфейсы и абстрактные классы
// Создайте интерфейс IDrawable с методом Draw(), который выводит информацию о рисуемом объекте.
// Создайте классы Circle, Rectangle и Triangle, реализующие этот интерфейс.
// Создайте массив объектов, реализующих интерфейс IDrawable, и вызовите метод Draw() для каждого из них.

namespace Task4App
{
    // Интерфейс IDrawable с методом Draw()
    interface IDrawable
    {
        void Draw();
    }
    class Circle : IDrawable
    {
        public void Draw() => Console.WriteLine("Рисую круг");
    }
    class Rectangle : IDrawable
    {
        public void Draw() => Console.WriteLine("Рисую прямоугольник");
    }
    class Triangle : IDrawable
    {
        public void Draw() => Console.WriteLine("Рисую треугольник");
    }
    class Task4
    {
        static void Main()
        {
            // Массив объектов через общий интерфейс IDrawable
            IDrawable[] shapes = { new Circle(), new Rectangle(), new Triangle() }; //аналогия IDrawable[] shapes = new IDrawable[3]; shapes[0] = new и тд. Circle();

            // Вызов метода Draw() для каждой фигуры
            foreach (IDrawable shape in shapes)
                shape.Draw();
        }
    }
}