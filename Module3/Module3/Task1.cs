using System;

//1. Создайте базовый класс "Фигура" с методом для вычисления площади.
//Затем создайте производные классы для разных геометрических фигур (круг, прямоугольник, треугольник) и
//используйте делегат для динамического вызова метода вычисления площади.

namespace Task1_Shapes
{
    //Делегаты представляют такие объекты, которые указывают на методы(инфа с Metanit). 
    // 1. Объявляем делегат 2. Создаем переменную делегата 3. Присваиваем этой переменной адрес метода 4. Вызываем метод
    public delegate double AreaCalculator();

    // Базовый класс
    public abstract class Shape
    {
        public string Name { get; set; }
        protected Shape(string name) =>  Name = name; //консруктор
        public abstract double GetArea(); // Абстрактный метод вычисления площади
    }

    // Производный класс: Круг
    public class Circle : Shape
    {
        public double Radius { get; set; }
        public Circle(double radius) : base("Круг") => Radius = radius; //: base("Круг") означает ссылаться на  protected Shape ивызывать его
        public override double GetArea() =>  Math.PI * Radius * Radius;
    }

    // Производный класс: Прямоугольник
    public class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public Rectangle(double width, double height) : base("Прямоугольник")
        {
            Width = width;
            Height = height;
        }
        public override double GetArea() => Width * Height;

    }

    // Производный класс: Треугольник (по основанию и высоте)
    public class Triangle : Shape
    {
        public double BaseLength { get; set; }
        public double Height { get; set; }
        public Triangle(double baseLength, double height) : base("Треугольник")
        {
            BaseLength = baseLength;
            Height = height;
        }
        public override double GetArea() =>  0.5 * BaseLength * Height;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Shape[] shapes = new Shape[]
            {
                new Circle(5), //срабатывает set
                new Rectangle(4, 6),
                new Triangle(3, 8)
            };

            Console.WriteLine("=== Вычисление площади с помощью делегата ===");

            foreach (var shape in shapes)
            {
                // Привязка метода экземпляра к делегату
                AreaCalculator calculateArea = shape.GetArea; //к примеру AreaCalculator calculateArea = shape.GetArea;  т.е. создаём пер-ую  делегата

                // Динамический вызов через делегат
                double area = calculateArea(); // срабатывает метод GetArea
                Console.WriteLine($"Фигура: {shape.Name,-15} | Площадь: {area:F2}");
            }
        }
    }
}