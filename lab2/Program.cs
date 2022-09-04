using System;

namespace d0gge
{
    public class Vector2
    {
        public Vector2() {}
        public Vector2(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public double Length()
        {
            return Math.Sqrt(this.X * this.X + this.Y * this.Y);
        }
        public static Vector2 operator +(Vector2 lhs, Vector2 rhs)
            => new Vector2(lhs.X + rhs.X, lhs.Y + rhs.Y);

        public static Vector2 operator -(Vector2 lhs, Vector2 rhs) 
            => new Vector2(lhs.X - rhs.X, lhs.Y - rhs.Y);

        public static Vector2 operator *(Vector2 lhs, int scalar)
            => new Vector2(lhs.X * scalar, lhs.Y * scalar);
        public static Vector2 operator *(int scalar, Vector2 rhs)
            => new Vector2(rhs.X * scalar, rhs.Y * scalar);

        public override string ToString() => $"({_x}, {_y})";
        public int X { get { return _x; } private set { _x = value; } }
        public int Y { get { return _y; } private set { _y = value; }}

        private int _x;
        private int _y;
    }
    class Program
    {
        enum TestType
        {
            Exit = 0,
            Sum,
            Sub,
            Mult,
            Length
        }
        
        static void SumTest()
        {
            Console.Clear();
            Console.WriteLine("Sum Test.");
            Vector2 vec1 = new Vector2(1, 2);
            Vector2 vec2 = new Vector2(2, 1);
            Console.WriteLine($"vec1: {vec1} + vec2: {vec2} = vec3: {vec1 + vec2}");
        }
        static void SubTest()
        {
            Console.Clear();
            Console.WriteLine("Sub Test.");
            Vector2 vec1 = new Vector2(1, 2);
            Vector2 vec2 = new Vector2(2, 1);
            Console.WriteLine($"vec1: {vec1} - vec2: {vec2} = vec3: {vec1 - vec2}");
        }
        static void MultiplyTest()
        {
            Console.Clear();
            Console.WriteLine("Multiplication by scalar Test.");
            Vector2 vec1 = new Vector2(1, 2);
            Console.WriteLine($"3 * vec1{vec1} = {3 * vec1}");

        }
        static void LengthTest()
        {
            Console.Clear();
            Console.WriteLine("Length of a vector test.");
            Vector2 vec1 = new Vector2(6, 6);
            Console.WriteLine($"Length of vec1{vec1}: {vec1.Length()}");
        }
        static void Main()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Variant #2. Gavrilov E.A. KE-219");
                Console.WriteLine("1. Test sum.");
                Console.WriteLine("2. Test subtraction.");
                Console.WriteLine("3. Test multipling by scalar.");
                Console.WriteLine("4. Test obtaining length.");
                Console.WriteLine("0. Exit.");

                Console.WriteLine();
                Console.WriteLine("Choose test: ");
                TestType number = (TestType) Int32.Parse(Console.ReadLine());
                if (number is TestType.Exit)
                {
                    break;
                }
                switch (number)
                {
                    case TestType.Sum:
                        SumTest();
                        break;
                    case TestType.Sub:
                        SubTest();
                        break;
                    case TestType.Mult:
                        MultiplyTest();
                        break;
                    case TestType.Length:
                        LengthTest();
                        break;
                    default:
                        break;
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            Console.Clear();
        }
    }
}