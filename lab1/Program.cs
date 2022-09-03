using System;

namespace d0gge
{
    class Rectangle
    {
        public struct Position
        {
            public int x1;
            public int y1;
            public int x2;
            public int y2;

            public int x3;
            public int y3;
            public int x4;
            public int y4;
        }

        private uint _id;

        private int _height;
        private int _width;
        private int _originX;
        private int _originY;

        private int _area;
        private int _perimeter;
        private Position _pos;

        public Rectangle(uint id, int orirginX, int originY, int width, int height)
        {
            _id = id;
            _originX = orirginX;
            _originY = originY;
            _height = height;
            _width = width;
            _area = _width * _height;
            _perimeter = 2*_width + 2*_height;
            ReconstructVertities();
        }

        public int Area { get; }
        public int Perimeter { get; }
        public int Height { get; }
        public int Width { get; }
        
        public int OriginX { get; } 
        public int OriginY { get; } 
        public Position GetPos()
        {
            return _pos;
        }

        public void SplitBy2Rec(Rectangle rec1, Rectangle rec2)
        {
            Position pos1 = rec1.GetPos();
            Position pos2 = rec1.GetPos();
            _width = rec1.Width + rec2.Width;
            _height = rec1.Height + rec2.Height;
            _originX = rec1.OriginX;
            _originY = rec1.OriginY;
            ReconstructVertities();
        }

        public void Print()
        {
            Console.WriteLine($"Rectangle#{_id}. Location: v1({GetPos().x1}, {GetPos().y1}), " 
            + $"v2({GetPos().x2}, {GetPos().y2}), v3({GetPos().x3}, {GetPos().y3}), v4({GetPos().x4}, {GetPos().y4})");
            Console.WriteLine($"Width: {_width}, height: {_height}");
        }

        public void CropIntersection(Rectangle rec1, Rectangle rec2)
        {
            bool intersectsX = rec2.OriginX - rec1.OriginX < rec2.Width && rec2.OriginX - rec1.OriginX > -rec2.Width;
            bool intersectsY = rec2.OriginY - rec1.OriginY < rec2.Height && rec2.OriginY - rec1.OriginY > -rec2.Height;
            if ( intersectsX && intersectsY)
            {
                Position pos1 = rec1.GetPos();
                Position pos2 = rec2.GetPos();
                // across
                if (pos2.y1 - pos2.y1 < 0 && pos2.y4 - pos1.y4 > 0)
                {
                    _height = rec1.Height;
                    _originY = rec1.OriginY;
                }
                //below
                else if (pos2.y1 - pos2.y1 < 0 && pos2.y4 - pos1.y4 < 0)
                {
                    _height = pos2.y4 - rec1.OriginY;
                    _originY = rec1.OriginY;

                }
                //above
                else if (pos2.y1 - pos2.y1 > 0 && pos2.y4 - pos1.y4 > 0)
                {
                    _height = pos1.x4 - rec2.OriginY;
                    _originY = rec2.OriginY;
                }

                //inside
                if (pos2.x2 - pos1.x2 < 0 && pos2.x1 - pos1.x1 > 0)
                {
                    _width = rec2.Width;
                    _originX = rec2.OriginX;
                }
                //right
                else if (pos2.x2 - pos1.x2 > 0 && pos2.x1 - pos1.x1 > 0)
                {
                    _width = pos1.x2 - rec1.OriginX;
                    _originX = rec1.OriginX;
                }
                //left
                else if (pos2.x2 - pos1.x2 < 0 && pos2.x1 - pos1.x1 < 0)
                {
                    _width = pos2.x2 - rec1.OriginX;
                    _originX = rec1.OriginX;
                }
                //across
                else
                {
                    _width = rec1.Width;
                    _originX = rec1.OriginX;
                }
                ReconstructVertities();
            }
            else
            {
                Console.WriteLine("These rectangles do not intersect!");
            }
        }

        public void Resize(int width, int height)
        {
            _width = width;
            _height = height;
            ReconstructVertities();
        }

        public void Translate(int x, int y)
        {
            _originX += x;
            _originY += y;
            ReconstructVertities();

        }

        private void ReconstructVertities()
        {
            _pos.x1 = _originX;
            _pos.y1 = _originY;
            _pos.x2 = _pos.x1 + _width;
            _pos.y2 = _pos.y1;
            _pos.x3 = _pos.x2;
            _pos.y3 = _pos.y2 + _height;
            _pos.x4 = _pos.x1;
            _pos.y4 = _pos.y1 + _height;
        }
    }
    class Program
    {
        static private uint s_id = 0;
        public enum TestType 
        {
            Exit = 0,
            Translate = 1,
            Resize,
            Construction,
            Cropping
        }

        static Rectangle AskForDefault()
        {
            Console.WriteLine("Do you want to use default rectangle? (1/OK, 0/NO)");
            bool res = Int32.Parse(Console.ReadLine()) != 0;
            if (res)
            {
                return CreateRec(); 
            }
            Console.WriteLine("Origin(x, y), width, height: ");
            int x = Int32.Parse(Console.ReadLine());
            int y = Int32.Parse(Console.ReadLine()); int width = Int32.Parse(Console.ReadLine());
            int height = Int32.Parse(Console.ReadLine());
            return CreateRec(x, y, width, height);

        }
        static void TestTranslation()
        {
            Console.Clear();
            Console.WriteLine("Testing translation.");
            Rectangle rec = AskForDefault();
            Console.WriteLine("Enter coordinates(x, y) to translate original rectangle: ");
            int x = Int32.Parse(Console.ReadLine());
            int y = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Before translation.");
            rec.Print();
            rec.Translate(x, y);
            Console.WriteLine("After translation.");
            rec.Print();
        }

        static void TestResizing()
        {
            Console.Clear();
            Console.WriteLine("Testing resizing.");
            Rectangle rec = AskForDefault();
            Console.WriteLine("Enter new width and height: ");
            int width = Int32.Parse(Console.ReadLine());
            int height = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Before.");
            rec.Print();
            rec.Resize(width, height);
            Console.WriteLine("After.");
            rec.Print();
        }

        static void TestConstruction()
        {
            Console.Clear();
            Console.WriteLine("Testing construction.");
            Console.WriteLine("Rec#1. Enter origin(x, y), width, height: ");
            int x = Int32.Parse(Console.ReadLine());
            int y = Int32.Parse(Console.ReadLine());
            int width = Int32.Parse(Console.ReadLine());
            int height = Int32.Parse(Console.ReadLine());
            Rectangle rec1 = CreateRec(x, y, width, height);
            Console.WriteLine("Rec#2. Enter origin(x, y), width, height: ");
            x = Int32.Parse(Console.ReadLine());
            y = Int32.Parse(Console.ReadLine());
            width = Int32.Parse(Console.ReadLine());
            height = Int32.Parse(Console.ReadLine());
            Rectangle rec2 = CreateRec(x, y, width, height);
            Rectangle rec = CreateRec();
            rec.SplitBy2Rec(rec1, rec2);
            Console.WriteLine("Rectangle after construction: ");
            rec.Print();
        }

        static void TestCropping()
        {
            Console.Clear();
            Console.WriteLine("Testing cropping.");
            Console.WriteLine("Rec#1. Enter origin(x, y), width, height: ");
            int x = Int32.Parse(Console.ReadLine());
            int y = Int32.Parse(Console.ReadLine());
            int width = Int32.Parse(Console.ReadLine());
            int height = Int32.Parse(Console.ReadLine());
            Rectangle rec1 = CreateRec(x, y, width, height);
            Console.WriteLine("Rec#2. Enter origin(x, y), width, height: ");
            x = Int32.Parse(Console.ReadLine());
            y = Int32.Parse(Console.ReadLine());
            width = Int32.Parse(Console.ReadLine());
            height = Int32.Parse(Console.ReadLine());
            Rectangle rec2 = CreateRec(x, y, width, height);
            Console.WriteLine("Croppping intersection.");
            Rectangle rec = CreateRec();
            rec.CropIntersection(rec1, rec2);
            Console.WriteLine("Cropped rectangle.");
            rec.Print();
        }

        static Rectangle CreateRec(int x = 0, int y = 0, int width = 3, int height = 2)
        {
            return new Rectangle(s_id++, x, y, width, height);
        }

        static void Main()
        {
            while (true)
            {
                s_id = 0;
                Console.Clear();
                Console.WriteLine("\"Hilarious rectangles\" v1.0");
                Console.WriteLine("1. Test translating a rectangle.");
                Console.WriteLine("2. Test resizing a rectangle.");
                Console.WriteLine("3. Test constructing of minimal rectangle consisting of 2 given.");
                Console.WriteLine("4. Test cropping intersection of 2 given rectangles.");
                Console.WriteLine("0. Press to exit.");
                TestType number = (TestType)Int32.Parse(Console.ReadLine());
                if (number is TestType.Exit)
                {
                    break;
                }
                switch (number)
                {
                    case TestType.Translate:
                        TestTranslation();
                        break;
                    case TestType.Resize:
                        TestResizing();
                        break;
                    case TestType.Construction:
                        TestConstruction();
                        break;
                    case TestType.Cropping:
                        TestCropping();
                        break;
                    default:
                        Console.WriteLine("There is no such test...");
                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            Console.Clear();
        }
    }
}