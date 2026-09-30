using System;

// Задача 2: Наследование и полиморфизм
// Создайте базовый класс Shape, представляющий геометрическую фигуру, и производные классы Circle и Rectangle.
// В базовом классе определите метод Area(), который возвращает площадь фигуры,
// и метод Perimeter(), который возвращает периметр фигуры.
// В производных классах переопределите эти методы для соответствующих фигур (круг и прямоугольник).
// Создайте объекты всех классов и выведите их площади и периметры.

namespace Task2App
{
    class Shape // Базовый класс Shape, представляющий геометрическую фигуру, и производные классы Circle и Rectangle.
    {
        public virtual double Area() => 0; //virtual --) разрешение на перезапись наследникам
        public virtual double Perimeter() => 0;
    }
    class Circle : Shape  // производный класс Circle
    {
        private readonly double _radius; // Радиус круга, readonly--)Значение переменной можно задать только один раз — в конструкторе при создании объекта

        public Circle(double radius) => _radius = radius; //вот тут мы задали знач для  _radius

        // Переопределение формул: π * r² и 2 * π * r
        public override double Area() => Math.PI * _radius * _radius; // override(связка  с virtual) разрешение на перезапись return в методе
        public override double Perimeter() => 2 * Math.PI * _radius;
    }
    class Rectangle : Shape //производный класс  Rectangle
    {
        private readonly double _width, _height; // Стороны прямоугольника

        public Rectangle(double width, double height) => (_width, _height) = (width, height); //консруктор для присвоения знач-ия обьектам

        // Переопределение формул: a * b и 2 * (a + b)
        public override double Area() => _width * _height;
        public override double Perimeter() => 2 * (_width + _height);
    }
    class Task2
    {
        static void Main()
        {
            // Объекты всех классов и вывод их площади и периметра.
            Shape circle = new Circle(5); //полеморфизм от класса  Shape(методы работыю поиному т.к. override)
            Shape rectangle = new Rectangle(4, 6); 

            Console.WriteLine($"Площадь круга: {circle.Area():F2}, Периметр: {circle.Perimeter():F2}");
            Console.WriteLine($"Площадь прямоугольника: {rectangle.Area()}, Периметр: {rectangle.Perimeter()}");
        }
    }
}