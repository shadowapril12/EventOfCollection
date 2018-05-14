using System;
using System.Collections.Generic;
using System.Text;

namespace EventOfCollection
{
    /// <summary>
    /// Класс предназначенный для формирования объектов события - удаления всех элементов коллекции
    /// </summary>
    /// <typeparam name="T"></typeparam>

    class Cleaner<T>
    {
        /// <summary>
        /// Элемент удаляемый в конкретный момент
        /// </summary>
        public T Element { get; private set; }

        public Cleaner(T value)
        {
            Element = value;
        }
    }
}
