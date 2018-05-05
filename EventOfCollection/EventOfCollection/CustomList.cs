using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace EventOfCollection
{ 
    class CustomList<T> : IList<T>
    {
        ///Кастомный делегат CustomListStateHandler представляет метод, который
        ///в качестве параметра принимает строку и ничего не возвращает
        public delegate void CustomListStateHandler(string message);

        /// <summary>
        /// Делегат EventHandler<ListAddingEventArgs<T>> представляет метод, который в качестве параметра принимает
        /// текущий объект и объект включающий данные о событии добавления элемента в коллекцию
        /// </summary>
        public EventHandler<ListAddingEventArgs<T>> OnAdding;

        /// <summary>
        /// Событие делегата CustomListStateHandler представляет собой метод OnAdded, который в качестве параметра
        /// принимает строку, сообщающую о добавлении элемента в коллекцию
        /// </summary>
        public event CustomListStateHandler OnAdded;

        /// <summary>
        /// Делегат EventHandler<RemoverAt> представляет метод OnRemovingAt, принимающий в качестве
        /// параметра объект, вызывающий этот метод, и объект содержащий информацию об удалении из коллекции
        /// элемента под переданным индексом
        /// </summary>
        public EventHandler<RemoverAt> OnRemovingAt;

        /// <summary>
        /// Делегат EventHandler<Remover<T>> представляет метод OnRemove, который в качетсве параметров
        /// принимает текущий объект, и объект содерэащий информациюю об удалении соответсвующего
        /// элемента из коллекции
        /// </summary>
        public EventHandler<Remover<T>> OnRemove;

        /// <summary>
        /// Делегат EventHandler<Cleaner<T>> представляет метода OnCleaner, принимающий в качестве
        /// параметров текущий объект
        /// и объект содержащий информацию об удалении всех элементов из коллекции
        /// </summary>
        public EventHandler<Cleaner<T>> OnClear;

        /// <summary>
        /// Делегат EventHandler<Inserter<T>> представляет метод OnInsert, принимающий в качестве
        /// параметров текущий объект
        /// и объект содержащий информацию о вставляемом в коллекцию элементе
        /// </summary>
        public EventHandler<Inserter<T>> OnInsert;

        /// <summary>
        /// Делегат EventHandler<Copyier<T>> представляет метод OnCopy, принимающий в качестве параметров
        /// текущий объект и объект содержащий информацию о массиве в который копируется коллекция и с какого 
        /// индекса вставляется в массив
        /// </summary>
        public EventHandler<Copyier<T>> OnCopy;
        
        //Массив над которым производятся выше описанные операции
        private T[] collection = new T[0];

        ///Свойство позволяющее определенному индексу в коллекции назначить определенное значение
        public T this[int index]
        {
            get
            {
                return collection[index];
            }
            set
            {
                collection[index] = value;
            }
        }

        //Свойство содержащее размерность массива collection
        public int Count
        {
            get
            {
                return collection.Length;
            }
        }

        ///Свойство возвращающее булевый тип, в зависимости от того является ли коллекция доступной только для
        ///чтения
        public bool IsReadOnly
        {
            get
            {
                List<T> col = collection.ToList();
                return collection.IsReadOnly;
            }
        }

        /// <summary>
        /// Add - метод добавления в коллекцию элемента
        /// </summary>
        /// <param name="el"></param>
        public void Add(T el)
        {
            //Если событию добаления назначена функция, то выполняется данная функция обработчик
            if(OnAdding != null)
            {
                ///eventArgs - экземпляр класса ListAddingEventArgs<T>, содержащий информацию о добавляемом
                ///элементе
                var eventArgs = new ListAddingEventArgs<T>(el);

                //выполнение функции OnAdding
                OnAdding(this, eventArgs);

                //Если свойству Cancel в объекте eventArgs было присвоено значение true, то добавление элемента 
                //прерывается
                if(eventArgs.Cancel)
                {
                    return;
                }
                else
                {
                    //В противном случае происходит добавление элемента в коллекцию
                    List<T> col = collection.ToList();
                    col.Add(el);

                    if (OnAdded != null)
                    {
                        OnAdded($"В коллекцию добавлен элемент {el}");
                    }

                    //Переменной collection присваевается ссылка на коллеекцию преобразованную в массив
                    collection = col.ToArray();
                }
            }
            else
            {
                //Если событию не назначен обработчик, то просто происходит добавления элемента в коллекцию
                List<T> col = collection.ToList();
                col.Add(el);
                collection = col.ToArray();
            }
           
        }

        //Метод IndexOf возвращает индекс первого вхождения элемента в коллекцию
        public int IndexOf(T el)
        {
            List<T> col = collection.ToList();
            return col.IndexOf(el);
        }

        ///Метод Contains возвращает логическое значение true, если элемент указанный в качестве параметра функции
        ///содержится в коллекции
        public bool Contains(T el)
        {
            List<T> col = collection.ToList();

            return col.Contains(el);
        }

        /// <summary>
        /// Метод удаляющий элемент коллекции под указанным индексом
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            //Переменной col типа List присваевается ссылка на массив collection преобразованный в коллекцию List
            List<T> col = collection.ToList();
            try
            {
                //Далее осуществляется проверка наличия передаваемого индекса в коллекции
                int count = 0;
                for(int i = 0; i < collection.Length; i++)
                {
                    if(i == index)
                    {
                        count++;
                    }
                }

                //Если совпадение найдено
                if(count == 1)
                {
                    //Проверяется задан ли обработчик на событие удаления элемента по заданному индексу
                    if (OnRemovingAt != null)
                    {
                        //Экземпляр класса RemovrtAt содержит индекс удаляемого элемента из коллекции
                        var eventArgs = new RemoverAt(index);

                        //Вызов функции обработчика
                        OnRemovingAt(this, eventArgs);

                        //Если свойству Cancel присвоено значение 'false', то удаление отменяется
                        if (eventArgs.Cancel)
                        {
                            return;
                        }
                    }
                    //Удаление элемента под заданным индексом
                    col.RemoveAt(index);

                    ///переменной collection присваевается ссылка на коллекцию преобразованную к массиву после
                    ///удаления из нее элемента
                    collection = col.ToArray();
                }
                else
                {
                    //Если совпадение не найдено, выбрасывается исключение
                    throw new ArgumentException("Передаваемый индекс не найден");
                }             
            }
            catch(ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Метод Remove отвечает за удаление указанного в параметрах элемента
        /// </summary>
        /// <param name="el">Удаляемый элемент</param>
        /// <returns>Возвращает true если элемент был удален и false в обратном случае</returns>
        public bool Remove(T el)
        {
            //Переменной col типа List присваевается ссылка на массив collection преобразованный в коллекцию List
            List<T> col = collection.ToList();

            try
            {
                //Проверка, содержит ли коллекция указанный элемент
                if(col.Contains(el))
                {
                    //Если событию удаления указанного элемента задан обработчик
                    if (OnRemove != null)
                    {
                        //Создается экземпляр класса eventArgs содержащий удаляемый элемент
                        var eventArgs = new Remover<T>(el);

                        //Вызов функции обработчика
                        OnRemove(this, eventArgs);
                    }
                    //Если операция удаления элемента возвращает 'true'
                    if (col.Remove(el))
                    {
                        ///Переменной collection присваивается ссылка на коллекцию col преобразованную в массив
                        collection = col.ToArray();

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    //Если указанного элемента в коллекции нет, то выбрасывается исключение
                    throw new ArgumentException($"Элемент {el} отсутствует в коллекции");                   
                }
                
            }
            catch(ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();

                return false;
            }
        }

        /// <summary>
        /// Метод Clear удаляет все элементы из коллекции
        /// </summary>
        public void Clear()
        {
            //Переменной col типа List присваевается ссылка на массив collection преобразованный в коллекцию List
            List<T> col = collection.ToList();

            //Событию очистки коллекции присвена функция обработчик
            if(OnClear != null)
            {
                //Для каждого элемента коллекции создается экземпляр класса с элементом, который будет удаляться
                foreach(T el in col)
                {
                    var eventArgs = new Cleaner<T>(el);

                    //Вызов функции обработчика
                    OnClear(this, eventArgs);
                }
            }
            //Если обработчик не присвоен, то просто происходит очиска коллекции
            col.Clear();

            ///Переменной collection присваивается ссылка на коллекцию col преобразованную в массив
            collection = col.ToArray();
        }

        /// <summary>
        /// Метод CopyTo служит для копирования всех элементов коллекции в заданный массив
        /// </summary>
        /// <param name="arr">Массив в который происходит копирование элементов коллекции</param>
        /// <param name="index">Индекс, с которого начнется копирование элементов в массив arr</param>
        public void CopyTo(T[] arr, int index)
        {
            //Переменной col типа List присваевается ссылка на массив collection преобразованный в коллекцию List
            List<T> col = collection.ToList();

            //Если событию копирования присвоена функция обработчик
            if(OnCopy != null)
            {
                ///Создается экземпляр класса Copier, хрянящий индекс и массив в котрый будет
                ///производится копирование
                var eventArgs = new Copyier<T>(arr, index);

                //Вызов функции обработчика
                OnCopy(this, eventArgs);
            }

            ///Переменной collection присваивается ссылка на коллекцию col преобразованную в массив
            col.CopyTo(arr, index);
        }

        /// <summary>
        /// Метод Insert вставляет в коллекцию элемент под указанным индексом
        /// </summary>
        /// <param name="index">Индекс, под которым в коллекцию будет вставляться элемент</param>
        /// <param name="el">Вставляемый элемент</param>
        public void Insert(int index, T el)
        {
            //Переменной col типа List присваевается ссылка на массив collection преобразованный в коллекцию List
            List<T> col = collection.ToList();

            //Если событию вставки присвоен обработчик
            if (OnInsert != null)
            {
                //Создается экземпляр класса Inserter, ссодержащий индекс и вставляемый элемент
                var eventArgs = new Inserter<T>(index, el);

                //Вызов функции обработчика
                OnInsert(this, eventArgs);

                //Если свойству объекта Cancel присвоено значение 'true'
                if (eventArgs.Cancel)
                {
                    //Выводится сообщение об отмене вставки
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Элемент {el} не был вставлен на позицию {index}");
                    Console.ResetColor();
                    return;
                }
            }

            //Если функция обработчик не была задана, то происходит вставка элемента в коллекцию
            col.Insert(index, el);

            ///Переменной collection присваивается ссылка на коллекцию col преобразованную в массив
            collection = col.ToArray();     
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach(T el in collection)
            {
                yield return el;
            }
        }
    }
}
