using System;

// Задача 7: Работа с массивом структур
// Создайте структуру с именем train, содержащую поля: название пункта назначения, номер поезда, время отправления.
// Ввести данные в массив из пяти элементов типа train, упорядочить элементы по номерам поездов.
// Добавить возможность вывода информации о поезде, номер которого введен пользователем.
// Добавить возможность сортировки массива по пункту назначения,
// причем поезда с одинаковыми пунктами назначения должны быть упорядочены по времени отправления.

namespace Task7App
{
    struct Train
    {
        public string Destination;      // Пункт назначения
        public int TrainNumber;         // Номер поезда
        public TimeSpan DepartureTime;  // Время отправления (часы минуты к примиеру 9:30)

        public Train(string destination, int trainNumber, TimeSpan departureTime) =>
            (Destination, TrainNumber, DepartureTime) = (destination, trainNumber, departureTime);  

        public void DisplayInfo() =>
            Console.WriteLine($"Пункт назначения: {Destination}, Поезд №: {TrainNumber}, Отправление: {DepartureTime}");
    }
    class Task7
    {
        static void Main()
        {
            Train[] trains = new Train[5];

            // Заполнение массива поездов
            for (int i = 0; i < trains.Length; i++)
            {
                Console.WriteLine($"\nВведите данные для поезда {i + 1}:");
                Console.Write("Пункт назначения: ");
                string dest = Console.ReadLine();

                Console.Write("Номер поезда: ");
                int number = int.Parse(Console.ReadLine());

                Console.Write("Время отправления (чч:мм): ");
                TimeSpan time = TimeSpan.Parse(Console.ReadLine()); //часы:минуты

                trains[i] = new Train(dest, number, time);
            }

            // 1. Сортировка по номерам поездов
            Array.Sort(trains, (a, b) => a.TrainNumber.CompareTo(b.TrainNumber));

            // 2. Поиск поезда по номеру
            Console.Write("\nВведите номер поезда для поиска: ");
            int searchNumber = int.Parse(Console.ReadLine());
            bool found = false;

            foreach (var train in trains)
            {
                if (train.TrainNumber == searchNumber)
                {
                    train.DisplayInfo();
                    found = true;
                    break;
                }
            }
            if (!found) Console.WriteLine("Поезд с таким номером не найден.");

            // 3. Сортировка по пункту назначения (при совпадении — по времени)
            // .Sort -1 осталяем(a<b), 1 меняет(a>b), 0 оставляем(a=b)
            Array.Sort(trains, (a, b) => // a и b — это два конкретных поезда из вашего массива trains (два объекта типа Train)
            {
                int cmp = string.Compare(a.Destination, b.Destination, StringComparison.OrdinalIgnoreCase); // .Compare --) метод сравнения двух строк по алфавиту: 0 -совпадают, <0 если  строка a по алфавиту идёт раньше строки b
                return cmp != 0 ? cmp : a.DepartureTime.CompareTo(b.DepartureTime);
            });

            // Вывод отсортированного списка
            Console.WriteLine("\nПоезда, отсортированные по пункту назначения и времени:");
            foreach (var train in trains)
                train.DisplayInfo();
        }
    }
}