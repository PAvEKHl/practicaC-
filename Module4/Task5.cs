using System;
//5.	Создайте приложение для рисования на холсте с использованием интерфейсов.
//Создайте интерфейс "Рисунок" с методами для рисования линий, кругов и прямоугольников.
//Реализуйте этот интерфейс в классе для работы с холстом.
class Task5
{
    interface IDrawing { void DrawLine(); void DrawCircle(); void DrawRect(); }

    class Canvas : IDrawing
    {
        public void DrawLine() => Console.WriteLine("Отрисована линия (-)");
        public void DrawCircle() => Console.WriteLine("Отрисован круг (O)");
        public void DrawRect() => Console.WriteLine("Отрисован прямоугольник ([])");
    }

    public static void Main()
    {
        IDrawing canvas = new Canvas();
        canvas.DrawLine();
        canvas.DrawCircle();
        canvas.DrawRect();
    }
}