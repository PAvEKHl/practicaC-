using System;

// Задача 5: События
// Создайте класс TemperatureSensor, который генерирует событие TemperatureChanged,
// когда измеренная температура меняется. Создайте класс Thermostat,
// который подписывается на событие TemperatureChanged и реагирует на изменение температуры, включая или выключая отопление.

namespace Task5App
{
    class TemperatureSensor
    {
        public event Action<int> TemperatureChanged; // Событие срабатывает при изменении значения температуры Action — это встроенный стандартный делегат event — защитный модификатор.
        private int _temperature; //поле)

        public int Temperature //свойство нашего поля  _temperature
        { 
            get => _temperature; // Когда кто-то читает значение, просто отдаем _temperature
            set
            {
                // Проверяем, изменилось ли значение
                if (_temperature != value) // В блоке set работает ключевое слово 'value' — это то число, которое написали справа от знака '='
                {
                    _temperature = value;
                    TemperatureChanged?.Invoke(_temperature); // Оповещение подписчиков
                }
            }
        }
    }
    class Thermostat
    {
        // Метод-обработчик события
        public void OnTemperatureChanged(int temp)
        {
            if (temp > 25)
                Console.WriteLine("Температура слишком высокая, включаю кондиционер.");
            else if (temp < 11)
                Console.WriteLine("Температура слишком низкая, включаю отопление.");
            else
                Console.WriteLine("Температура в норме.");
        }
    }
    class Task5
    {
        static void Main()
        {
            var sensor = new TemperatureSensor();
            var thermostat = new Thermostat();

            // Подписка метода термостата на событие датчика
            sensor.TemperatureChanged += thermostat.OnTemperatureChanged; //вызываеться только при изменении температуры

            // Изменение температуры для проверки разных условий
            sensor.Temperature = 30; // Выше 25 -> кондиционер
            sensor.Temperature = 10; // Ниже 11 -> отопление
            sensor.Temperature = 22; // В норме
        }
    }
}