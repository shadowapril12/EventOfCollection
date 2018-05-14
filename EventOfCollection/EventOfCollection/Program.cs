using System;

namespace EventOfCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            ///Создание экземпляра класса CustomList
            CustomList<string> str = new CustomList<string>();

            ///Событию добавления присваивается функция обработчик AddingString, так как у нас коллекция
            ///типа string
            str.OnAdding += AddingString;

            ///Функция обработчик ShowMessage срабатывает после добавления элемента в коллекцию
            str.OnAdded += ShowMessage;

            ///Функция RemovingAtIndex срабатывает при удалении элемента по индексу
            str.OnRemovingAt += RemovingAtIndex;

            ///Лямбда выражения является обработчиком события удаления элемента из коллекции
            str.OnRemove += (sender, e) =>
            {
                Console.WriteLine($"Из коллекции удален элемент '{e.Value}'");
            };

            ///Функция Cleaning является обработчиком события очистки коллекции
            str.OnClear += Cleaning;

            ///Функция Inserting является обработчиком события вставки элемента в коллекцию
            str.OnInsert += Inserting;

            ///Функция Copy является обработчиком ссобытия копирования элементов коллекции в массив
            str.OnCopy += Copy;

            //Функция CancelnRemoverAt является обработчиком события отмены удаления элемента в коллекции
            str.OnCancelRemoverAt += CancelRemoverAt;

            //Функция RemoveException является обработчиком возникновения исключения при удалении элемента
            str.OnRemoverException += RemoveException;

            //Функция InsertCancel является обработчиком события отмены вставкиэлемента в коллекцию
            str.OnInsertCancel += InsertCancel;

            ///Добавление элементов в массив
            str.Add("привет");
            str.Add("Василий Петрович");
            str.Add("Андрей");
            str.Add("Игорь");

            ///Удаление первого элемента из массива
            str.RemoveAt(0);

            ///Намеренное удаление отсутствующего элемента
            str.Remove("Ub");

            ///Массив в который будут копироваться элементы из коллекции
            string[] arr = new string[str.Count];

            ///Копирование элементов из коллекции в массив
            str.CopyTo(arr, 0);

            ///Вывод всех элементов копированных в массив
            Console.WriteLine("Коллекция скопированная в массив:");
            foreach (string el in arr)
            {
                Console.WriteLine(el);
            }

            ///Вывод количества элементов в коллекции
            Console.WriteLine($"Количество элементов в коллекции - {str.Count}");


            
            Console.ReadLine();
        }

        /// <summary>
        /// Функция простого вывода сообщения. В данном случае вызывается после добавления элемента в коллекцию
        /// </summary>
        /// <param name="message">Сообщение, которое выводится в консоль после добавления</param>
        private static void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Функция вызываемая при добавлении элемента типа string в коллекцию
        /// </summary>
        /// <param name="sender">Объект, в контексте которого вызывается функция</param>
        /// <param name="args">Объект события, который содержит добавляемый в коллекцию элемент</param>
        private static void AddingString(object sender, ListAddingEventArgs<string> args)
        {
            Console.WriteLine($"Добавляется элемент {args.Value}");
            ///При попытке добавления пустой строки
            if(args.Value == "")
            {
                ///Свойству Cancel присваевается значение true. Таким образом отменяется добавление
                args.Cancel = true;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Пустые строки не добавляются в коллекцию");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Функция вызываемая при добавлении элемента типа int в коллекцию
        /// </summary>
        /// <param name="sender">Объект, в контексте которого вызывается функция</param>
        /// <param name="args">Объект события, который содержит добавляемый в коллекцию элемент</param>
        private static void AddingInt(object sender, ListAddingEventArgs<int> args)
        {
            Console.WriteLine($"Добавляется элемент {args.Value}");

            ///При добавлении четного элемента
            if(args.Value % 2 == 0)
            {
                /// Свойству Cancel присваевается значение true.Таким образом отменяется добавление
                args.Cancel = true;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Четные числа не добавляются в коллекцию");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Функция RemovingAtIndex вызывается при удалении элемента под определенным индексом
        /// </summary>
        /// <param name="sender">Объект, в контексте которого вызывается функция</param>
        /// <param name="rem"> события, который содержит индекс удаляемого из коллекции элемента</param>
        private static void RemovingAtIndex(object sender, RemoverAt rem)
        {
            ///Вывод запроса на подтверждение удаления
            Console.WriteLine($"Вы действительно хотите удалить элемент под индексом {rem.Index}? Y/N");

            try
            {
                switch (Console.ReadKey(true).KeyChar)
                {
                    ///Если нажата кнопка 'n'
                    case 'n':
                        ///Свойство Cancel принимает значение 'true' и удаление отменяется
                        rem.Cancel = true;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Удаление отменено");
                        Console.ResetColor();
                        break;
                    ///Если нажата клавиша 'Y', то происходит удаление
                    case 'y':
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Элемент под индексом '{rem.Index}' -  удален");
                        Console.ResetColor();
                        break;
                    ///Во всех остальных случаях выбрасывается исключение
                    default:
                        rem.Cancel = true;
                        throw new ArgumentException("Нажата неверная клавиша.");
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
        /// Метод Cleaning вызывается при вызове метода Clear. Запускается на каждом элементе коллекции
        /// </summary>
        /// <param name="sender">Объект, в контексте которого вызывается функция</param>
        /// <param name="el">Удаляемый элемент</param>
        private static void Cleaning(object sender, Cleaner<string> el)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Из коллекции удален элемент {el.Element}");
            Console.ResetColor();
        }

        /// <summary>
        /// Метод Inserting вызывается при вставке нового элемента в коллекцию на определенную позицию
        /// </summary>
        /// <param name="sender">Объект, в контексте которого вызывается функция</param>
        /// <param name="el">Вставляемый в коллекцию элемент</param>
        private static void Inserting(object sender, Inserter<string> el)
        {
            ///При попытке вставить пустую строку
            if (el.Element == "")
            {
                ///При попытке вставить пустую строку
                el.Cancel = true;
            }
            else
            {
                //В противном случае происходит вставка элемента на позицию 'index'
                Console.WriteLine($"{el.Element} вставлен в коллекцию на пизоцию '{el.Index}'");
            }
        }

        /// <summary>
        /// Метод Copy вызывается при копировании элементов коллекции в массив
        /// </summary>
        /// <param name="sender">Объект, в контексте которого вызывается функция</param>
        /// <param name="copy">Объект хранящий имя массива в который будут копироваться значения и индекс</param>
        private static void Copy(object sender, Copyier<string> copy)
        {
            Console.WriteLine($"Элементы коллекции были скопированы в массив {copy.Arr}, начиная с индекса {copy.Index}");
        }

        /// <summary>
        /// Метод CancelRemoverAt вызывается при отмене удаления элемента на определенной позиции
        /// </summary>
        /// <param name="sender">Объект, в контексте которого вызывается функция</param>
        /// <param name="rem">Объект хранящий индекс элемента и сообщение об удалении элемента</param>
        private static void CancelRemoverAt(object sender, RemoverAtCancel rem)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{rem.Message} Индекс - {rem.Index}");
            Console.ResetColor();
        }

        /// <summary>
        /// Метод RemoveException вызывается при возникновении исключения при удалении определенного элемента из коллекции
        /// </summary>
        /// <param name="sender">Объект, в контексте которого вызывается функция</param>
        /// <param name="remover">Объект хранящий сообщени о возникновении исключения</param>
        private static void RemoveException(object sender, RemoverException<string> remover)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{remover.Message}");
            Console.ResetColor();
        }

        /// <summary>
        /// Метод InsertCancel вызывается при возникновении события - отмены вставки элемента на орпеделенную позицию
        /// </summary>
        /// <param name="sender">Объект, в контексте которого вызывается функция</param>
        /// <param name="inserter">Объект хранящий сообщение о вставляемом элементе и его индексе</param>
        private static void InsertCancel(object sender, InserterCancel inserter)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(inserter.Message);
            Console.ResetColor();
        }
    }
}
