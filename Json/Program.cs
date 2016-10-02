using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Json
{
    /// <summary>
    /// Класс, содержащий данные о человеке.
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

    class Program
    {
        static void Main(string[] args)
        {
            // Создаём JSON объект и добавляем ему свойства в виде пары ключ-значение.
            var jObj = new JObject();
            jObj.Add("name", "Ivan Ivanov");
            jObj.Add("age", 18);

            // Создаём JSON массив и добавляем элементы массива.
            var jArray = new JArray();
            jArray.Add("Nikolay");
            jArray.Add("Fedor");

            // Добавляем массив как свойство объека.
            jObj.Add("childs", jArray);

            // Печатаем полученный результат.
            Console.WriteLine(jObj.ToString());

            Console.WriteLine();

            // Разбор входной JSON строки.
            var parsedObj = JObject.Parse("{\"manufacturer\": \"BMW\", \"model\": \"X5\"}");

            // Печать результата.
            Console.WriteLine($"Manufacturer: {parsedObj.GetValue("manufacturer")}, Model: {parsedObj.GetValue("model")}");

            Console.WriteLine();

            // Создаём коллекцию с данными о некоторых людях.
            var persons = new List<Person>
            {
                new Person { FirstName = "Ivan", LastName = "Ivanov", Age = 40 },
                new Person { FirstName = "Andrey", LastName = "Ivanov", Age = 18 },
                new Person { FirstName = "Zahar", LastName = "Plushkin", Age = 22 },
                new Person { FirstName = "Alexey", LastName = "Petrov", Age = 36 },
            };

            // Сериализуем и печатаем первый элемент списка.
            Console.WriteLine(JsonConvert.SerializeObject(persons[0]));

            Console.WriteLine();

            // Сериализуем и печатаем всю коллекцию.
            Console.WriteLine(JsonConvert.SerializeObject(persons));

            Console.WriteLine();

            // Сначала сериализуем объект в JSON строку.
            var personJson = JsonConvert.SerializeObject(persons[1]);

            // Затем преобразуем строку в объект.
            var personObj = JsonConvert.DeserializeObject<Person>(personJson);

            // Убеждаемся, что получили объект с такими же значениями свойств.
            if (persons[1].FirstName == personObj.FirstName &&
                persons[1].LastName == personObj.LastName &&
                persons[1].Age == personObj.Age)
            {
                Console.WriteLine("Persons are equal!");
            }
        }
    }
}
