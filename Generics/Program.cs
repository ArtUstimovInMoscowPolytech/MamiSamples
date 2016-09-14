using System;

namespace Generics
{
    /// <summary>
    /// Класс, представляющий собой массив, автоматически увеличивающийся при добавлении новых элементов.
    /// </summary>
    /// <typeparam name="T">Тип хранимого объекта.</typeparam>
    class ResizeArray<T>
    {
        /// <summary>
        /// Массив для хранения объектов.
        /// </summary>
        private T[] _array;

        /// <summary>
        /// Конструктор, принимающий значение начального размера массива.
        /// </summary>
        /// <param name="size"></param>
        public ResizeArray(int size)
        {
            _array = new T[size];
        }

        /// <summary>
        /// Индексатор.
        /// </summary>
        /// <param name="i">Принимает индекс элемента массива, к которому требуется получить доступ.</param>
        /// <returns>Возвращает элемент массива по индексу.</returns>
        public T this[int i]
        {
            get { return _array[i]; }
            set
            {
                // Если индекс устанавливаемого значения выходит за текущие границы массива.
                if (_array.Length <= i)
                {
                    // Тогда увеличиваем размер массива в двое, пока не будет получен размер больше индекса.
                    var newLength = _array.Length * 2;
                    while (newLength <= i)
                    {
                        newLength *= 2;
                    }

                    // Создаём массив нового размера.
                    var temp = new T[newLength];

                    // Копируем все данные из старого массива в новый.
                    Array.Copy(_array, temp, _array.Length);

                    // Устанавливаем новый массив как основной.
                    _array = temp;
                }

                // Присваиваем новое значение по индексу.
                _array[i] = value;
            }
        }

        /// <summary>
        /// Свойство, возвращающее размер массива.
        /// </summary>
        public int Length => _array.Length;
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Создаём массив int из двух элементов.
            var resizeIntArray = new ResizeArray<int>(2);

            // Присваиваем значение четвёртому элементу.
            resizeIntArray[3] = 4;

            Print(resizeIntArray);

            Console.WriteLine();

            // Создаём массив string из одного элемента.
            var resizeStringArray = new ResizeArray<string>(1);

            // Присваиваем значение первому и третьему элменту.
            resizeStringArray[0] = "world";
            resizeStringArray[2] = "hello";

            Print(resizeStringArray);

            Console.Read();
        }

        /// <summary>
        /// Обобщённый метод для печати массива.
        /// </summary>
        /// <typeparam name="T">Тип содержимого массива.</typeparam>
        /// <param name="resizeArray">Массив.</param>
        static void Print<T>(ResizeArray<T> resizeArray)
        {
            for (int i = 0; i < resizeArray.Length; i++)
            {
                Console.WriteLine($"{i}: {resizeArray[i]}");
            }
        }
    }
}
