using System;
using System.Collections.Generic;
using System.Text;

namespace EventOfCollection
{
    /// <summary>
    /// Класс предназначенный для формирования объекта события - отмена вставки элемента под определенным индексом в коллекцию
    /// </summary>
    class InserterCancel
    {
        //Сообение об отмене вставки
        public string Message { get; private set; }

        public InserterCancel(string message)
        {
            Message = message;
        }
    }
}
