using System;
using System.Collections.Generic;
using System.IO;

namespace ClassesAndInterfaces
{
    /// <summary>
    /// Интерфейс объекта, который умеет печатать текст.
    /// </summary>
    interface IPrinter
    {
        /// <summary>
        /// Метод, при вызове которого происходит печать.
        /// </summary>
        /// <param name="text">Текст, который будет напечатан.</param>
        void Print(string text);
    }

    /// <summary>
    /// Класс для печати в консоль. Реализует интерфейс IPrinter.
    /// </summary>
    class ConsolePrinter : IPrinter
    {
        /// <summary>
        /// Реализуем метод печати, требуемый для соответствия интерфейсу IPrinter.
        /// </summary>
        public void Print(string text)
        {
            Console.Write(text);
        }
    }

    /// <summary>
    /// Класс для печати в файл. Реализует интерфейс IPrinter.
    /// </summary>
    class FilePrinter : IPrinter
    {
        /// <summary>
        /// Поле класса для хранения имени файла, в который будет выполнятся печать.
        /// </summary>
        protected readonly string _fileName;

        /// <summary>
        /// Конструктор класса, который принимает имя файла и сохраняет его в поле _fileName.
        /// </summary>
        /// <param name="fileName"></param>
        public FilePrinter(string fileName)
        {
            _fileName = fileName;
        }

        /// <summary>
        /// Реализуем метод печати, требуемый для соответствия интерфейсу IPrinter.
        /// </summary>
        public void Print(string text)
        {
            // Открываем файл для записи.
            var streamWriter = new StreamWriter(_fileName);
            
            // Записываем текст.
            streamWriter.Write(text);
            
            // Закрываем файл.
            streamWriter.Close();
        }
    }

    /// <summary>
    /// Класс для печати в файл лога log.txt. Наследутся от класса FilePrinter.
    /// </summary>
    class LogFilePrinter : FilePrinter
    {
        /// <summary>
        /// Конструктор по умолчанию, который вызывает конструктор базового класса и передаёт в него имя файла для записи.
        /// </summary>
        public LogFilePrinter() : base("log.txt")
        {
            
        }
    }

    /// <summary>
    /// Класс, содержащий точку входа в программу.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Точка входа в программу.
        /// </summary>
        /// <param name="args">Аргументы командной строки.</param>
        static void Main(string[] args)
        {
            // Текст для печати.
            var text = "text";

            // Создаём коллекцию из описанных принтеров.
            var printers = new List<IPrinter>
            {
                new ConsolePrinter(),
                new FilePrinter("file.txt"),
                new LogFilePrinter()
            };

            // Отравляем текст на печать каждому принтеру из коллекции.
            foreach (var printer in printers)
            {
                printer.Print(text);
            }

            // Форматируем строку с помощью интерполяции для вывода на консоль.
            var consoleText = $"Console {text}";

            // Находим принтер для консоли с помощью лямбда-выражения-предиката.
            var consolePrinter = printers.Find(printer => printer is ConsolePrinter);

            // Печатаем текст на консоль.
            consolePrinter.Print(consoleText);

            // Ожидаем пользовательского ввода, чтобы консоль не закрылась.
            Console.Read();
        }
    }
}
