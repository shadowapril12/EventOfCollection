using System;
using System.Collections.Generic;
using System.Text;

namespace EventOfCollection
{
    /// <summary>
    /// Класс предназначенный для формирования объектов события - удаления определенного элемента из коллекции
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class Remover<T>
    {
        //Value - удаляемый элемент из коллекции
        public T Value { get; private set; }

        public Remover(T value)
        {
            Value = value;
        }
    }
}
