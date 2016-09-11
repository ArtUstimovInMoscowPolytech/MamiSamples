using System;

namespace EventsAndDelegates
{
    /// <summary>
    /// Тип делагата для события поступления сообщения.
    /// </summary>
    /// <param name="mailbox">Почтовый ящик, в котоый поступило сообщение.</param>
    /// <param name="message">Поступившее сообщение.</param>
    delegate void IncomingMessage(Mailbox mailbox, Message message);

    /// <summary>
    /// Класс, представляющий собой почтовый ящик.
    /// </summary>
    class Mailbox
    {
        /// <summary>
        /// Событие поступления сообщения.
        /// </summary>
        public event IncomingMessage OnIncomingMessage;

        /// <summary>
        /// Метод для добавления сообщения в почтовый ящик.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        public void Receive(Message message)
        {
            // Оповещаем подписчиков, что поступило сообщение.
            OnIncomingMessage?.Invoke(this, message); 
        }
    }

    /// <summary>
    /// Класс, представляющий собой сообщение.
    /// </summary>
    class Message
    {
        /// <summary>
        /// От кого поступило сообщение.
        /// </summary>
        public string From { get; private set; }

        /// <summary>
        /// Текст сообщения.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Конструктор, принимающий значения для отправителя и текста сообщения.
        /// </summary>
        /// <param name="from">Отправитель.</param>
        /// <param name="text">Текст сообщения.</param>
        public Message(string from, string text)
        {
            From = from;
            Text = text;
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
            // Создаём почтовый ящик.
            var mailbox = new Mailbox();

            // Подписываемся на оповещения о новых сообщениях.
            // При поступлении сообщения печатаем в консоль его содержимое.
            mailbox.OnIncomingMessage +=
                (mb, msg) => Console.WriteLine($"From \"{msg.From}\" received message \"{msg.Text}\"");

            // Добавляем новое сообщение в почтовый ящик.
            mailbox.Receive(new Message("me", "hello"));

            // Ожидаем пользовательского ввода, чтобы консоль не закрылась.
            Console.Read();
        }
    }
}
