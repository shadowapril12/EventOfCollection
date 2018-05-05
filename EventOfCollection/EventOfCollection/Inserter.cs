using System;
using System.Collections.Generic;
using System.Text;

namespace EventOfCollection
{
    /// <summary>
    /// Класс предназначенный для формирования объектов события - вставки элемента в коллекцию
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class Inserter<T>
    {
        /// <summary>
        /// Элемент вставляемый в коллекцию
        /// </summary>
        public T Element { get; private set; }

        /// <summary>
        /// Индекс, под которым вставляется элемент
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// Свойство флаг, в случае возврата 'true', вставка отменяется
        /// </summary>
        public bool Cancel { get; set; }

        public Inserter(int index, T element)
        {
            Index = index;
            Element = element;
        }
    }


}
