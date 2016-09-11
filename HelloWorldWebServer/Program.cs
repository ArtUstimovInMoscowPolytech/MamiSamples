using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace HelloWorldWebServer
{
    /// <summary>
    /// Http сервер, который может обслуживать одновременно максимум одного клиента и на все запросы отвечает Hello, World!
    /// </summary>
    class Program
    {
        /// <summary>
        /// Точка входа в программу.
        /// </summary>
        /// <param name="args">Аргументы командной строки.</param>
        static void Main(string[] args)
        {
            // Строка ответа в соответсвии с протоколом HTTP (http://www.ietf.org/rfc/rfc2616.txt).
            var responseString = "HTTP/1.1 200 OK\r\n" +
                                 "Content-Type: text/plain\r\n" +
                                 "\r\n" +
                                 "Hello, World!";

            // Конвертируем строку в байты.
            var responseBytes = Encoding.UTF8.GetBytes(responseString);

            // Создаём буфер для считывания запросов.
            var buffer = new byte[1024];

            // Создаём Tcp сокет.
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Привязываем его к адресу http://127.0.0.1:8080/
            // (после запуска открыть ссылку в браузере, чтобы увидеть Hello, World!).
            socket.Bind(new IPEndPoint(IPAddress.Loopback, 8080));

            // Начиинаем ждать подключений.
            socket.Listen(1);

            Console.WriteLine("Server started at 127.0.0.1:8080");

            // Обрабатываем подключения в бесконечном цикле.
            while (true)
            {
                // Принимаем поключение.
                using (var connection = socket.Accept())
                {
                    // Считываем запрос.
                    connection.Receive(buffer);

                    // Преобразуем запрос в строку и печатаем на экран.
                    Console.WriteLine(Encoding.UTF8.GetString(buffer));

                    // Отправляем ответ.
                    connection.Send(responseBytes);
                }
            }
        }
    }
}