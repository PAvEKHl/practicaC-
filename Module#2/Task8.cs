using System;

// Задача 8: Абстрактные классы и иерархия
// Разработайте иерархию классов для геометрических фигур (круг, прямоугольник, треугольник).
// Реализуйте методы вычисления площади и периметра для каждой фигуры.

namespace Task8App
{
    abstract class Shape //Вызов new Shape() запрещён компилятором
    {
        // Обязательные для реализации методы в дочерних классах
        public abstract double Area(); //Метод не имеет тела вообще, в отличает от virtual 
        public abstract double Perimeter(); //каждый класс-наследник (Circle, Rectangle, Triangle) обязан написать свою реализацию через override
    }
    class Circle : Shape
    {
        private readonly double _radius;
        public Circle(double radius) => _radius = radius;

        public override double Area() => Math.PI * _radius * _radius;
        public override double Perimeter() => 2 * Math.PI * _radius;
    }
    class Rectangle : Shape
    {
        private readonly double _width, _height;
        public Rectangle(double width, double height) => (_width, _height) = (width, height);

        public override double Area() => _width * _height;
        public override double Perimeter() => 2 * (_width + _height);
    }
    class Triangle : Shape
    {
        private readonly double _a, _b, _c;
        public Triangle(double a, double b, double c) => (_a, _b, _c) = (a, b, c);

        // Формула Герона: S = √(p * (p-a) * (p-b) * (p-c))
        public override double Area()
        {
            double p = Perimeter() / 2; // Полупериметр
            return Math.Sqrt(p * (p - _a) * (p - _b) * (p - _c));
        }

        public override double Perimeter() => _a + _b + _c;
    }
    class Task8
    {
        static void Main()
        {
            // Создание объектов фигур
            Shape circle = new Circle(6);
            Shape rectangle = new Rectangle(5, 7);
            Shape triangle = new Triangle(5, 3, 6);

            // Вывод результатов расчетов
            Console.WriteLine($"Круг: площадь = {circle.Area():F2}, периметр = {circle.Perimeter():F2}");
            Console.WriteLine($"Прямоугольник: площадь = {rectangle.Area()}, периметр = {rectangle.Perimeter()}");
            Console.WriteLine($"Треугольник: площадь = {triangle.Area():F2}, периметр = {triangle.Perimeter():F2}");
        }
    }
}