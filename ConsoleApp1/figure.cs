using System;
using System.Collections;
using System.Collections.Generic;



namespace SparseMatrix
{

    public class Program1
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Светашева Юлия ИУ5-34Б");
            Figure rect = new Rectangle(3, 9);
            Figure square = new Square(7);
            Figure circle = new Circle(6);
            List<Figure> figa = new List<Figure> {
                 circle, rect, square  //Добавили наши объекты в коллекцию
            };
            Console.WriteLine("List <Figure> До сортировки:");
            foreach (var f in figa)
            {//выводим отортированный список
                Console.WriteLine(f);
            }
            Console.WriteLine("***********");
            //NameComparer nc= new NameComparer();
            figa.Sort();
            Console.WriteLine("List <Figure> После сортировки:");
            foreach (var f in figa)
            {//выводим отортированный список
                Console.WriteLine(f);
            }

            ArrayList figaCollection = new ArrayList(3) { circle, rect, square };
            Console.WriteLine("");
            Console.WriteLine("***********");
            Console.WriteLine("ArrayList До сортировки:");
            foreach (var f in figaCollection)
            {//выводим отортированный список
                Console.WriteLine(f);
            }
            Console.WriteLine("***********");
            //NameComparer nc1 = new NameComparer();
            figaCollection.Sort();
            Console.WriteLine("ArrayList После сортировки:");
            foreach (var f in figaCollection)
            {//выводим отортированный список
                Console.WriteLine(f);
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
}
