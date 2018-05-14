using System;
using System.Collections.Generic;
using System.Text;

namespace EventOfCollection
{
    /// <summary>
    /// Класс предназначеннный для формирования объектов события - добавления
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class ListAddingEventArgs<T> : EventArgs
    {
        //Свойство Value хранит добавляемый элемент
        public T Value { get; private set; }

        //Свойство - флаг, если возвращает true то добавление отменяется
        public bool Cancel { get; set; }

        public ListAddingEventArgs(T value)
        {
            this.Value = value;
        }
    }
}
