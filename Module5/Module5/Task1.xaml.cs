using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
//1.	Создайте графическое приложение для рисования на холсте с использованием Windows Forms.
// Реализуйте функции рисования линий, кругов и квадратов.
namespace Module5
{
    public partial class Task1 : Window
    {
        Point start; Shape current;
        public Task1() => InitializeComponent();

        private void Down(object s, MouseButtonEventArgs e)
        {
            start = e.GetPosition(canvas);
            current = rbLine.IsChecked == true ? new Line { Stroke = Brushes.Black, X1 = start.X, Y1 = start.Y, X2 = start.X, Y2 = start.Y }
                    : rbCircle.IsChecked == true ? (Shape)new Ellipse { Stroke = Brushes.Black } : new Rectangle { Stroke = Brushes.Black };
            canvas.Children.Add(current);
            canvas.CaptureMouse();
        }

        private void Move(object s, MouseEventArgs e)
        {
            if (!canvas.IsMouseCaptured) return;
            var p = e.GetPosition(canvas);
            if (current is Line l) { l.X2 = p.X; l.Y2 = p.Y; }
            else
            {
                current.Width = Math.Abs(p.X - start.X); current.Height = Math.Abs(p.Y - start.Y);
                Canvas.SetLeft(current, Math.Min(p.X, start.X)); Canvas.SetTop(current, Math.Min(p.Y, start.Y));
            }
        }
        private void Up(object s, MouseButtonEventArgs e) => canvas.ReleaseMouseCapture();
    }
}