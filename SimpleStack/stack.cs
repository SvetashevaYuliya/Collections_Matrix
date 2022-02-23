using System;
using System.Collections.Generic;


namespace ConsoleApp1
{
    class stack
    {
          static void Main(string[] args)
          {
              Console.WriteLine("Hello World!");
              SimpleList<Figure> list = new SimpleList<Figure>();
              Figure rect = new Rectangle(3, 9);
              Figure square = new Square(7);
              Figure circle = new Circle(6);
              list.Add(circle);
              list.Add(rect);
              list.Add(square);
              Console.WriteLine("\nПеред сортировкой:");
              foreach (var x in list) Console.WriteLine(x);
              //сортировка
              list.Sort();
              Console.WriteLine("\nПосле сортировки:");
              foreach (var x in list) Console.WriteLine(x);


              SimpleStack<Figure> stack = new SimpleStack<Figure>();
              //добавление данных в стек
              stack.Push(rect);
              stack.Push(square);
              stack.Push(circle);
              //чтение данных из стека
              Console.WriteLine(" ");
              Console.WriteLine("Чтение днных из стека после добавоения новых элементов");

              while (stack.Count > 0)
              {
                  Figure f = stack.Pop();
                  Console.WriteLine(f);
              }


          }


        public class SimpleListItem<T>
        {
            public T data { get; set; }
            public SimpleListItem<T> next { get; set; }
            public SimpleListItem(T param)
            {
                this.data = param;
            }
        }

        public class SimpleList<T> : IEnumerable<T>//верширшина стека-конец списка
 where T : IComparable
        {
            /// <summary>
            /// Первый элемент списка
            /// </summary>
            protected SimpleListItem<T> first = null;
            /// <summary>
            /// Последний элемент списка
            /// </summary>
            protected SimpleListItem<T> last = null;
            /// <summary>
            /// Количество элементов
            /// </summary>
            public int Count
            {
                get { return _count; }
                protected set { _count = value; }
            }
            int _count;
            /// <summary>
            /// Добавление элемента
            /// </summary>
            public void Add(T element)
            {
                SimpleListItem<T> newItem = new SimpleListItem<T>(element);
                this.Count++;

                //Добавление первого элемента
                if (last == null)
                {
                    this.first = newItem;
                    this.last = newItem;
                }
                //Добавление следующих элементов
                else
                {
                    //Присоединение элемента к цепочке
                    this.last.next = newItem;
                    //Присоединенный элемент считается последним
                    this.last = newItem;
                }
            }
            /// <summary>
            /// Чтение контейнера с заданным номером
            /// </summary>
            public SimpleListItem<T> GetItem(int number)
            {
                if ((number < 0) || (number >= this.Count))
                {
                    //Можно создать собственный класс исключения
                    throw new Exception("Выход за границу индекса");
                }
                SimpleListItem<T> current = this.first;
                int i = 0;
                //Пропускаем нужное количество элементов
                while (i < number)
                {
                    //Переход к следующему элементу
                    current = current.next;
                    //Увеличение счетчика
                    i++;
                }
                return current;
            }
            /// <summary>
            /// Чтение элемента с заданным номером
            /// </summary>
            public T Get(int number)
            {
                return GetItem(number).data;
            }
            /// <summary>
            /// Для перебора коллекции
            /// </summary>

            public IEnumerator<T> GetEnumerator()
            {
                SimpleListItem<T> current = this.first;
                //Перебор элементов
                while (current != null)
                {
                    //Возврат текущего значения
                    yield return current.data;
                    //Переход к следующему элементу
                    current = current.next;
                }
            }
            //Реализация обобщенного IEnumerator<T> требует реализации
            //необобщенного интерфейса
            //Данный метод добавляется автоматически при реализации интерфейса
            System.Collections.IEnumerator
           System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
            /// <summary>
            /// Cортировка
            /// </summary>
            public void Sort()
            {
                Sort(0, this.Count - 1);
            }
            /// <summary>
            /// Алгоритм быстрой сортировки
            /// </summary>
            private void Sort(int low, int high)
            {
                int i = low;
                int j = high;
                T x = Get((low + high) / 2);
                do
                {
                    while (Get(i).CompareTo(x) < 0) ++i;
                    while (Get(j).CompareTo(x) > 0) --j;
                    if (i <= j)
                    {
                        Swap(i, j);
                        i++; j--;
                    }
                } while (i <= j);

                if (low < j) Sort(low, j);
                if (i < high) Sort(i, high);
            }
            /// <summary>
            /// Вспомогательный метод для обмена элементов при
            //сортировке
            /// </summary>
            private void Swap(int i, int j)
            {
                SimpleListItem<T> ci = GetItem(i);
                SimpleListItem<T> cj = GetItem(j);
                T temp = ci.data;
                ci.data = cj.data;
                cj.data = temp;
            }
        }

        class SimpleStack<T> : SimpleList<T> where T : IComparable
        {
            /// <summary>
            /// Добавление в стек
            /// </summary>
            public void Push(T element)
            {
                //Добавление в конец списка уже реализовано
                Add(element);
            }
            /// <summary>
            /// Удаление и чтение из стека
            /// </summary>
            public T Pop()
            {
                //default(T) - значение для типа T по умолчанию
                T Result = default(T);
                //Если стек пуст, возвращается значение по умолчанию для типа
                if (this.Count == 0) return Result;
                //Если элемент единственный
                if (this.Count == 1)
                {
                    //то из него читаются данные

                    Result = this.first.data;
                    //обнуляются указатели начала и конца списка
                    this.first = null;
                    this.last = null;
                }
                //В списке более одного элемента
                else
                {
                    //Поиск предпоследнего элемента
                    SimpleListItem<T> newLast = this.GetItem(this.Count - 2);
                    //Чтение значения из последнего элемента
                    Result = newLast.next.data;
                    //предпоследний элемент считается последним
                    this.last = newLast;
                    //последний элемент удаляется из списка
                    newLast.next = null;
                }
                //Уменьшение количества элементов в списке
                this.Count--;
                //Возврат результата
                return Result;
            }
        }


    }

    public abstract class Figure : IComparable
    {
        public abstract double Area();

        public string Type { get; set; }
        public override string ToString()
        {
            return this.Type + "площадью" + this.Area().ToString();
        }

        //Реализация IComparable
        public int CompareTo(object other)
        {
            Figure p = (Figure)other;
            if (this.Area() > p.Area())
                return 1;
            if (this.Area() < p.Area())
                return -1;
            else
                return 0;
        }
    }



    public class Rectangle : Figure, IPrint
    {
        private double _heigth;

        public double heigth
        {
            set
            {
                if (value < 0)
                    Console.WriteLine("Введено некоректное значение высоты");
                else _heigth = value;
            }
            get { return _heigth; }

        }

        private double _width;

        public double width
        {
            set
            {
                if (value < 0)
                    Console.WriteLine("Введено некоректное значение ширины");
                else
                    _width = value;
            }
            get { return _width; }
        }
        public Rectangle(double width, double heigth)
        {
            this.width = width;
            this.heigth = heigth;

        }

        public override double Area()
        {
            return this.heigth * this.width;
        }
        public override string ToString()
        {
            return "Прямоугольник высотой " + this.heigth.ToString() + ", шириной " + this.width.ToString() + " и площадью " + this.Area().ToString();
        }

        void IPrint.Print()
        {
            Console.WriteLine(this.ToString());
        }



    }

    public class Square : Rectangle, IPrint
    {
        public double length;
        public Square(double length)
            : base(length, length)
        {
            this.length = length;

        }
        public override double Area()
        {
            return Math.Pow(this.length, 2);
        }
        public override string ToString()
        {
            return "Квадрат со стороной " + this.length.ToString() + " и площадью " + this.Area().ToString();
        }

        void IPrint.Print()
        {
            Console.WriteLine(this.ToString());
        }


    }

    public class Circle : Figure, IPrint
    {
        private double _radius;
        public double radius
        {
            set
            {
                if (value < 0)
                    Console.WriteLine("Введено некоректное значение радиуса");
                else
                    _radius = value;
            }
            get { return _radius; }

        }
        public Circle(double radius)
        {
            this.radius = radius;
        }
        public override double Area()
        {
            return Math.Pow(this.radius, 2) * 3.14;
        }
        public override string ToString()
        {
            return "Круг радиусом " + this.radius.ToString() + " и площадью " + this.Area().ToString();
        }

        void IPrint.Print()
        {
            Console.WriteLine(this.ToString());
        }


    }

    interface IPrint
    {
        void Print();
    }
}
