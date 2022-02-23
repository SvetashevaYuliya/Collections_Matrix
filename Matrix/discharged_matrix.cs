using System;
using System.Collections.Generic;
using System.Text;

namespace SparseMatrix
{
    class discharged_matrix
    {
         static void Main(string[] args)
         {
             Console.WriteLine("\nМатрица");
             Matrix<Figure> matrix = new Matrix<Figure>(3, 3, 3, new FigureMatrixCheckEmpty());
             Figure rect = new Rectangle(3, 9);
             Figure square = new Square(7);
             Figure circle = new Circle(6);
             matrix[0, 0, 0] = rect;
             matrix[1, 1, 1] = square;
             matrix[2, 2, 2] = circle;
             Console.WriteLine(matrix.ToString());
         }

        public class Matrix<T>
        {
            Dictionary<string, T> _matrix = new Dictionary<string, T>();
            int maxX;
            int maxY;
            int maxZ;
            // Реализация интерфейса для проверки пустого элемента
            IMatrixCheckEmpty<T> сheckEmpty;
   
            public Matrix(int px, int py, int pz, IMatrixCheckEmpty<T> сheckEmptyParam)//внедрение зависимостей через контруктор
            {
                this.maxX = px;
                this.maxY = py;
                this.maxZ = pz;
                this.сheckEmpty = сheckEmptyParam;
            }
            // Индексатор для доступа к данных
            public T this[int x, int y, int z]
            {
                set
                {
                    CheckBounds(x, y, z);
                    string key = DictKey(x, y, z);
                    this._matrix.Add(key, value);
                }
                get
                {
                    CheckBounds(x, y, z);
                    string key = DictKey(x, y, z);
                    if (this._matrix.ContainsKey(key))
                    {
                        return this._matrix[key];
                    }
                    else
                    {
                        return this.сheckEmpty.getEmptyElement();
                    }
                }
            }

            // Проверка границ

            void CheckBounds(int x, int y, int z)
            {

                if (x < 0 || x >= this.maxX)
                {
                    throw new ArgumentOutOfRangeException("x", "x=" + x + " выходит за границы");
                }
                if (y < 0 || y >= this.maxY)
                {
                    throw new ArgumentOutOfRangeException("y", "y=" + y + " выходит за границы");
                }
                if (z < 0 || z >= this.maxZ)
                {
                    throw new ArgumentOutOfRangeException("z", "z=" + z + " выходит за границы");
                }
            }

            // Формирование ключа

            string DictKey(int x, int y, int z)
            {
                return x.ToString() + "_" + y.ToString() + "_" + z.ToString();
            }

            // Приведение к строке

            public override string ToString()
            {
                StringBuilder b = new StringBuilder();


                for (int z = 0; z < this.maxZ; z++)
                {
                    for (int j = 0; j < this.maxY; j++)
                    {
                        b.Append("[");
                        for (int i = 0; i < this.maxX; i++)
                        {
                            //Добавление разделителя-табуляции
                            if (i > 0)
                            {
                                b.Append("\t");
                            }
                            //Если текущий элемент не пустой
                            if (!this.сheckEmpty.checkEmptyElement(this[i, j, z]))
                            {
                                //Добавить приведенный к строке текущий элемент
                                b.Append(this[i, j, z].ToString());
                            }
                            else
                            {
                                //Иначе добавить признак пустого значения
                                b.Append(" - ");
                            }
                        }// i
                        b.Append("]\n");

                    }//j

                }//z
                return b.ToString();
            }
        }

        // Проверка пустого элемента матрицы

        public interface IMatrixCheckEmpty<T>
        {

            // Возвращает пустой элемент

            T getEmptyElement();

            // Проверка что элемент является пустым

            bool checkEmptyElement(T element);
        }

        class FigureMatrixCheckEmpty : IMatrixCheckEmpty<Figure>
        {
           
            public Figure getEmptyElement()
            {
                return null;
            }

            // Проверка что переданный параметр равен null

            public bool checkEmptyElement(Figure element)
            {
                bool Result = false;
                if (element == null)
                {
                    Result = true;
                }
                return Result;
            }
        }//тут*/

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

}
