using System;
using System.Collections.Generic;
using System.Text;

namespace EventOfCollection
{
    /// <summary>
    /// Класс предназначенный для формирования объектов события - копирования элементов коллекции в массив
    /// </summary>
    /// /// <typeparam name="T"></typeparam>
    class Copyier<T>
    {
        /// <summary>
        /// Массив, в который будут копироваться элементы коллекции
        /// </summary>
        public T[] Arr { get; private set; }

        /// <summary>
        /// Индекс массива, скоторого начнется вставка
        /// </summary>
        public int Index { get; private set; }

        public Copyier(T[] arr, int index)
        {
            Arr = arr;
            Index = index;
        }
    }
}
