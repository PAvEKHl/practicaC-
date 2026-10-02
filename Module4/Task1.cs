using System;

//1.	Создайте интерфейс "Фигура" с методами для вычисления площади и периметра.
//Затем реализуйте этот интерфейс в классах геометрических фигур (круг, прямоугольник, треугольник).
class Task1
{
    interface IShape { double Area(); double Perimeter(); }

    class Circle : IShape { double r; public Circle(double r) => this.r = r; public double Area() => Math.PI * r * r; public double Perimeter() => 2 * Math.PI * r; }
    class Rect : IShape { double w, h; public Rect(double w, double h) => (this.w, this.h) = (w, h); public double Area() => w * h; public double Perimeter() => 2 * (w + h); }
    class Triangle : IShape
    {
        double a, b, c;
        public Triangle(double a, double b, double c) => (this.a, this.b, this.c) = (a, b, c);
        public double Perimeter() => a + b + c;
        public double Area() { double p = Perimeter() / 2; return Math.Sqrt(p * (p - a) * (p - b) * (p - c)); }
    }

    public static void Main()
    {
        IShape[] shapes = { new Circle(2), new Rect(3, 4), new Triangle(3, 4, 5) };
        foreach (var s in shapes) Console.WriteLine($"S: {s.Area():F2}, P: {s.Perimeter():F2}");
    }
}