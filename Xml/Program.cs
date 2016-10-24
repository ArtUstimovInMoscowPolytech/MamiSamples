using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Xml
{
    /// <summary>
    /// Класс, содержащий данные о человеке.
    /// </summary>
    public class Person
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
        // Объявляем пространства имён
        private static XNamespace defaultNamespace = "http://schemas.servicestack.net/types";
        private static XNamespace instanceNamespace = "http://www.w3.org/2001/XMLSchema-instance";

        static void Main(string[] args)
        {            
            // Создаём XML-документ
            var xml = new XDocument();

            // Создаём корневой элемент с атрибутами и добавляем в документ
            var person = new XElement(defaultNamespace + "person");
            person.Add(new XAttribute(XNamespace.Xmlns + "i", instanceNamespace));
            xml.Add(person);

            // Добавляем вложенные в корневой элементы
            person.Add(new XElement(defaultNamespace + "name", "Ivan Ivanov"));
            person.Add(new XElement(defaultNamespace + "age", 18));
           
            // Создаём подобие массива и добавляем в корневой элемент
            var childs = new XElement(defaultNamespace + "childs");
            childs.Add(new XElement(defaultNamespace + "child", "Nikolay"));
            childs.Add(new XElement(defaultNamespace + "child", "Fedor"));
            person.Add(childs);

            Console.WriteLine(xml.ToString());

            Console.WriteLine();

            // Разбор входного XML-документа
            var parsedXml = XDocument.Parse("<root><manufacturer>BMW</manufacturer><model>X5</model></root>");

            Console.WriteLine($"Manufacturer: {parsedXml.Root.Element("manufacturer").Value}, " +
                              $"Model: {parsedXml.Root.Element("model").Value}");

            Console.WriteLine();

            // Создаём коллекцию с данными о некоторых людях.
            var persons = new List<Person>
            {
                new Person { FirstName = "Ivan", LastName = "Ivanov", Age = 40 },
                new Person { FirstName = "Andrey", LastName = "Ivanov", Age = 18 },
                new Person { FirstName = "Zahar", LastName = "Plushkin", Age = 22 },
                new Person { FirstName = "Alexey", LastName = "Petrov", Age = 36 },
            };

            // Сериализуем один элемент из коллекции
            Console.WriteLine(SerializeObject(persons[0]));
            
            Console.WriteLine();

            // Сериализуем коллекцию полностью
            Console.WriteLine(SerializeObject(persons));

            Console.WriteLine();

            // Сериализуем и десериализуем объект
            var parsedPerson = DeserializeObject<Person>(SerializeObject(persons[1]));

            // Сверяем, что все свойства равны
            if (persons[1].FirstName == parsedPerson.FirstName &&
                    persons[1].LastName == parsedPerson.LastName &&
                    persons[1].Age == parsedPerson.Age)
            {
                Console.WriteLine("Persons are equal!");
            }

            Console.WriteLine();

            // Сериализуем и десериализуем список
            var parsedPersons = DeserializeObject<List<Person>>(SerializeObject(persons));

            // Сверяем количество элементов в списках
            if (persons.Count == parsedPersons.Count)
            {
                Console.WriteLine("Count is equal!");
            }
        }

        private static string SerializeObject<T>(T obj)
        {
            // Создаём сериалайзер с пространством имён по умолчанию
            var xmlSerializer = new XmlSerializer(typeof(T), defaultNamespace.ToString());

            // Добавляем дополнительное пространство имён с префиксом
            var xmlSerializerNamespaces = new XmlSerializerNamespaces();
            xmlSerializerNamespaces.Add("i", instanceNamespace.ToString());

            // Сериализуем и печатаем
            using (var stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, obj, xmlSerializerNamespaces);
                return stringWriter.ToString();
            }
        }

        private static T DeserializeObject<T>(string xml)
        {
            // Создаём сериалайзер с пространством имён по умолчанию
            var xmlSerializer = new XmlSerializer(typeof(T), defaultNamespace.ToString());

            using (var stringReader = new StringReader(xml))
            {
                // Десериализуем
                return (T)xmlSerializer.Deserialize(stringReader);
            }
        }
    }
}
