using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqAndLambda
{
    /// <summary>
    /// Класс, включающий данные о человеке.
    /// </summary>
    class Person
    {
        /// <summary>
        /// Имя.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Возраст.
        /// </summary>
        public int Age { get; set; }
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
            // Создаём коллекцию с данными о некоторых людях.
            var persons = new List<Person>
            {
                new Person { FirstName = "Ivan", LastName = "Ivanov", Age = 40 },
                new Person { FirstName = "Andrey", LastName = "Ivanov", Age = 18 },
                new Person { FirstName = "Zahar", LastName = "Plushkin", Age = 22 },
                new Person { FirstName = "Alexey", LastName = "Petrov", Age = 36 },
            };

            // Выбираем все имена из коллекции.
            var names = from person in persons
                        select person.FirstName;

            // Печатаем имена.
            foreach (var name in names)
            {
                Console.WriteLine(name);
            }

            Console.WriteLine();

            // Выбираем людей старше 30 лет и сортируем по возрасту.
            var old = from person in persons
                      where person.Age > 30
                      orderby person.Age
                      select person;

            PrintPersons(old);

            Console.WriteLine();

            // Выбираем людей старше 30 лет без сортировки с помощью лямбда-выражения-предиката.
            var old2 = persons.FindAll(person => person.Age > 30);

            PrintPersons(old2);

            Console.WriteLine();

            // Выбор всех Ивановых и их сортировка по убыванию возраста.
            // Также LINQ, но через методы расширений с использованием лямбда-выражений.
            var ivanov = persons.Select(person => person)
                                .Where(person => person.LastName == "Ivanov")
                                .OrderByDescending(person => person.Age);

            PrintPersons(ivanov);

            // Ожидаем пользовательского ввода, чтобы консоль не закрылась.
            Console.Read();
        }

        /// <summary>
        /// Метод для распечатки коллекции объектов класса Person.
        /// </summary>
        /// <param name="persons">Коллекция объектов класса Person.</param>
        static void PrintPersons(IEnumerable<Person> persons)
        {
            foreach (var person in persons)
            {
                Console.WriteLine($"{person.FirstName} {person.LastName} with age {person.Age}");
            }
        }
    }
}
