using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace StudentConsoleApp
{
    // Класс модели студента
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Group { get; set; }
        public int Course { get; set; }
        public double AverageGrade { get; set; }
    }

    internal class Program
    {
        private static List<Student> students = new List<Student>();
        private static int nextId = 1;
        private const string DefaultFileName = "students_data.csv";

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Console.Title = "Управление списком студентов (Консольное приложение)";

            // Автоматическая загрузка из файла при запуске (или создание тестовых данных)
            if (File.Exists(DefaultFileName))
            {
                LoadFromFile(DefaultFileName, silent: true);
            }
            else
            {
                SeedDemoStudents();
                SaveToFile(DefaultFileName, silent: true);
            }

            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("================================================================");
                Console.WriteLine("           СИСТЕМА УПРАВЛЕНИЯ СПИСКОМ СТУДЕНТОВ                 ");
                Console.WriteLine("================================================================");
                Console.ResetColor();
                Console.WriteLine("1. Показать всех студентов");
                Console.WriteLine("2. Добавить студента");
                Console.WriteLine("3. Редактировать студента");
                Console.WriteLine("4. Удалить студента");
                Console.WriteLine("5. Поиск студентов (по ФИО, группе или курсу)");
                Console.WriteLine("6. Сортировка списка студентов по различным критериям");
                Console.WriteLine("7. Сохранить список студентов в файл");
                Console.WriteLine("8. Загрузить список студентов из файла");
                Console.WriteLine("0. Выход");
                Console.WriteLine("----------------------------------------------------------------");
                Console.Write("Выберите пункт меню (0-8): ");

                string choice = Console.ReadLine()?.Trim();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        PrintTable(students);
                        Pause();
                        break;
                    case "2":
                        AddStudent();
                        Pause();
                        break;
                    case "3":
                        EditStudent();
                        Pause();
                        break;
                    case "4":
                        DeleteStudent();
                        Pause();
                        break;
                    case "5":
                        SearchStudents();
                        Pause();
                        break;
                    case "6":
                        SortStudents();
                        Pause();
                        break;
                    case "7":
                        Console.Write($"Введите имя файла для сохранения (Enter для '{DefaultFileName}'): ");
                        string savePath = Console.ReadLine()?.Trim();
                        if (string.IsNullOrEmpty(savePath)) savePath = DefaultFileName;
                        SaveToFile(savePath, silent: false);
                        Pause();
                        break;
                    case "8":
                        Console.Write($"Введите имя файла для загрузки (Enter для '{DefaultFileName}'): ");
                        string loadPath = Console.ReadLine()?.Trim();
                        if (string.IsNullOrEmpty(loadPath)) loadPath = DefaultFileName;
                        LoadFromFile(loadPath, silent: false);
                        Pause();
                        break;
                    case "0":
                        SaveToFile(DefaultFileName, silent: true);
                        isRunning = false;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Некорректный выбор. Введите цифру от 0 до 8.");
                        Console.ResetColor();
                        Pause();
                        break;
                }
            }
        }

        // Начальные тестовые данные
        private static void SeedDemoStudents()
        {
            students.Add(new Student { Id = nextId++, FullName = "Иванов Алексей Сергеевич", Group = "ПОИТ-31", Course = 3, AverageGrade = 8.9 });
            students.Add(new Student { Id = nextId++, FullName = "Петрова Мария Викторовна", Group = "ПОИТ-31", Course = 3, AverageGrade = 9.4 });
            students.Add(new Student { Id = nextId++, FullName = "Сидоров Дмитрий Игоревич", Group = "ИСИТ-22", Course = 2, AverageGrade = 7.6 });
        }

        // Вывод таблицы студентов на экран
        private static void PrintTable(IEnumerable<Student> list)
        {
            var items = list.ToList();
            Console.WriteLine($"{"ID",-4} | {"ФИО студента",-30} | {"Группа",-10} | {"Курс",-5} | {"Ср. балл",-8}");
            Console.WriteLine(new string('-', 69));

            if (items.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return;
            }

            foreach (var s in items)
            {
                Console.WriteLine($"{s.Id,-4} | {s.FullName,-30} | {s.Group,-10} | {s.Course,-5} | {s.AverageGrade,-8:F2}");
            }
        }

        // 1. Добавление студента
        private static void AddStudent()
        {
            Console.WriteLine("--- ДОБАВЛЕНИЕ НОВОГО СТУДЕНТА ---");
            Console.Write("Введите ФИО: ");
            string name = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Ошибка: ФИО не может быть пустым!");
                return;
            }

            Console.Write("Введите группу (например, ПОИТ-31): ");
            string group = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(group)) group = "Без группы";

            Console.Write("Введите курс (1-6): ");
            if (!int.TryParse(Console.ReadLine(), out int course) || course < 1 || course > 6)
            {
                course = 1;
            }

            Console.Write("Введите средний балл (0.0 - 10.0): ");
            string gradeInput = Console.ReadLine()?.Trim().Replace(',', '.') ?? "0";
            if (!double.TryParse(gradeInput, NumberStyles.Any, CultureInfo.InvariantCulture, out double grade) || grade < 0 || grade > 10)
            {
                grade = 0.0;
            }

            students.Add(new Student
            {
                Id = nextId++,
                FullName = name,
                Group = group,
                Course = course,
                AverageGrade = grade
            });

            SaveToFile(DefaultFileName, silent: true);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Студент успешно добавлен!");
            Console.ResetColor();
        }

        // 2. Редактирование студента
        private static void EditStudent()
        {
            PrintTable(students);
            Console.Write("\nВведите ID студента для редактирования: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Ошибка: некорректный ID.");
                return;
            }

            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                Console.WriteLine("Студент с указанным ID не найден.");
                return;
            }

            Console.WriteLine($"Редактирование: {student.FullName} (оставьте поле пустым, чтобы не менять значение)");

            Console.Write($"Новое ФИО [{student.FullName}]: ");
            string newName = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(newName)) student.FullName = newName;

            Console.Write($"Новая группа [{student.Group}]: ");
            string newGroup = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(newGroup)) student.Group = newGroup;

            Console.Write($"Новый курс [{student.Course}]: ");
            string newCourseStr = Console.ReadLine()?.Trim();
            if (int.TryParse(newCourseStr, out int newCourse) && newCourse >= 1 && newCourse <= 6)
                student.Course = newCourse;

            Console.Write($"Новый средний балл [{student.AverageGrade:F2}]: ");
            string newGradeStr = Console.ReadLine()?.Trim().Replace(',', '.');
            if (!string.IsNullOrEmpty(newGradeStr) &&
                double.TryParse(newGradeStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double newGrade) &&
                newGrade >= 0 && newGrade <= 10)
            {
                student.AverageGrade = newGrade;
            }

            SaveToFile(DefaultFileName, silent: true);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Данные студента успешно обновлены!");
            Console.ResetColor();
        }

        // 3. Удаление студента
        private static void DeleteStudent()
        {
            PrintTable(students);
            Console.Write("\nВведите ID студента для удаления: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Ошибка: некорректный ID.");
                return;
            }

            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                Console.WriteLine("Студент с таким ID не найден.");
                return;
            }

            students.Remove(student);
            SaveToFile(DefaultFileName, silent: true);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Студент «{student.FullName}» успешно удален.");
            Console.ResetColor();
        }

        // 4. Поиск студентов по различным критериям
        private static void SearchStudents()
        {
            Console.WriteLine("--- ПОИСК СТУДЕНТОВ ---");
            Console.WriteLine("1. По ФИО (или части ФИО)");
            Console.WriteLine("2. По номеру группы");
            Console.WriteLine("3. По курсу");
            Console.Write("Выберите критерий поиска (1-3): ");
            string mode = Console.ReadLine()?.Trim();

            Console.Write("Введите значение для поиска: ");
            string query = Console.ReadLine()?.Trim() ?? "";
            Console.WriteLine();

            IEnumerable<Student> results = Enumerable.Empty<Student>();

            switch (mode)
            {
                case "1":
                    results = students.Where(s => s.FullName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0);
                    break;
                case "2":
                    results = students.Where(s => s.Group.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0);
                    break;
                case "3":
                    if (int.TryParse(query, out int c))
                        results = students.Where(s => s.Course == c);
                    break;
                default:
                    Console.WriteLine("Неверный критерий.");
                    return;
            }

            Console.WriteLine("Результаты поиска:");
            PrintTable(results);
        }

        // 5. Сортировка студентов по различным критериям
        private static void SortStudents()
        {
            Console.WriteLine("--- СОРТИРОВКА СПИСКА СТУДЕНТОВ ---");
            Console.WriteLine("1. По ФИО (по алфавиту А-Я)");
            Console.WriteLine("2. По ФИО (в обратном порядке Я-А)");
            Console.WriteLine("3. По среднему баллу (по убыванию)");
            Console.WriteLine("4. По среднему баллу (по возрастанию)");
            Console.WriteLine("5. По курсу (по возрастанию) и группе");
            Console.Write("Выберите критерий сортировки (1-5): ");
            string sortChoice = Console.ReadLine()?.Trim();
            Console.WriteLine();

            switch (sortChoice)
            {
                case "1":
                    students = students.OrderBy(s => s.FullName).ToList();
                    break;
                case "2":
                    students = students.OrderByDescending(s => s.FullName).ToList();
                    break;
                case "3":
                    students = students.OrderByDescending(s => s.AverageGrade).ToList();
                    break;
                case "4":
                    students = students.OrderBy(s => s.AverageGrade).ToList();
                    break;
                case "5":
                    students = students.OrderBy(s => s.Course).ThenBy(s => s.Group).ToList();
                    break;
                default:
                    Console.WriteLine("Неверный выбор сортировки.");
                    return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Список отсортирован:");
            Console.ResetColor();
            PrintTable(students);
        }

        // 6. Сохранение списка студентов в файл
        private static void SaveToFile(string filePath, bool silent)
        {
            try
            {
                var lines = new List<string> { "Id;FullName;Group;Course;AverageGrade" };
                foreach (var s in students)
                {
                    lines.Add($"{s.Id};{s.FullName};{s.Group};{s.Course};{s.AverageGrade.ToString(CultureInfo.InvariantCulture)}");
                }
                File.WriteAllLines(filePath, lines, Encoding.UTF8);

                if (!silent)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Список успешно сохранен в файл: {Path.GetFullPath(filePath)}");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении файла: {ex.Message}");
            }
        }

        // 7. Загрузка списка студентов из файла
        private static void LoadFromFile(string filePath, bool silent)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    if (!silent) Console.WriteLine($"Файл '{filePath}' не найден.");
                    return;
                }

                string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
                students.Clear();

                for (int i = 1; i < lines.Length; i++) // Пропускаем заголовок
                {
                    if (string.IsNullOrWhiteSpace(lines[i])) continue;
                    string[] parts = lines[i].Split(';');
                    if (parts.Length >= 5)
                    {
                        int.TryParse(parts[0], out int id);
                        string name = parts[1];
                        string group = parts[2];
                        int.TryParse(parts[3], out int course);
                        double.TryParse(parts[4].Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double grade);

                        students.Add(new Student
                        {
                            Id = id,
                            FullName = name,
                            Group = group,
                            Course = course,
                            AverageGrade = grade
                        });
                    }
                }

                nextId = students.Count > 0 ? students.Max(s => s.Id) + 1 : 1;

                if (!silent)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Загружено студентов из файла: {students.Count}");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
            }
        }

        private static void Pause()
        {
            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey(true);
        }
    }
}