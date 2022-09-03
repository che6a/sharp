using System;

namespace d0gge
{
    public class Rectangle
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

        private int _height;
        private int _width;
        private int _originX;
        private int _originY;

        private int _area;
        private int _perimeter;
        private Position _pos;

        public Rectangle(int x, int y, int width, int height)
        {
            _originX = x;
            _originY = y;
            _height = height;
            _width = width;
            _area = _width * _height;
            _perimeter = 2*_width + 2*_height;
            Reconstruct();
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
            Reconstruct();
        }

        public bool Intersects(Rectangle rec)
        {
            if (rec.OriginX - OriginX < Width)
            {
            }
            return false;
        }

        public void Intersected(Rectangle rec1, Rectangle rec2)
        {
            Position pos1 = rec1.GetPos();
            Position pos2 = rec2.GetPos();
        }

        public void Resize(int width, int height)
        {
            _width = width;
            _height = height;
            Reconstruct();
        }

        public void Translate(int x, int y)
        {
            _originX += x;
            _originY += y;
            Reconstruct();

        }

        private void Reconstruct()
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
        static void Main()
        {
        }
    }
}