using System;
using System.Collections.Generic;
using System.Text;

namespace EventOfCollection
{
    /// <summary>
    /// Класс предназначенный для формирования объекта события - исключение при удаление определенного элемента из коллекции, если 
    /// элемент отсутствует в коллекции
    /// </summary>
    class RemoverException<T>
    {
        //Элемент, который пытались удалить
        public T Element { get; private set; }

        //Сообщение о возникновении исключения
        public string Message;

        public RemoverException(T el, string message)
        {
            Element = el;
            Message = message;
        }
    }
}
