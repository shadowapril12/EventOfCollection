using System;
using System.Collections.Generic;
using System.Text;

namespace EventOfCollection
{
    /// <summary>
    /// Класс предназначенный для формирования объекта события - отмена удаления элемента на определенной позиции
    /// </summary>
    class RemoverAtCancel
    {
        //Индекс удаляемого элемента
        public int Index { get; private set; }

        //Сообщение об отмене удаления
        public string Message { get; private set; }

        public RemoverAtCancel(int index, string message)
        {
            Index = index;
            Message = message;
        }
    }
}
