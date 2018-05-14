using System;
using System.Collections.Generic;
using System.Text;

namespace EventOfCollection
{
    /// <summary>
    /// Класс предназначенный для формирования объекта события - удаления элемента на определенной позиции
    /// </summary>
    class RemoverAt
    {
        //Индекс под которым будет удаляться элемент из коллекции
        public int Index { get; private set; }

        //Свойство - флаг, если возвращает true, то удаление отменяется
        public bool Cancel { get; set; }

        public RemoverAt(int index)
        {
            Index = index;
        }
    }
}
