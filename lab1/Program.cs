using System;

// All source code is available on https://github.com/d0gge/sharp.git
// Гаврилов Е.А. КЭ-219
// Вариант 2
// Составить описание класса прямоугольников со сторонами, параллельными осям координат.
// Предусмотреть возможность перемещения прямоугольников на плоскости, изменение размеров,
// построение наименьшего прямоугольника, содержащего два заданных прямоугольника, и
// прямоугольника, являющегося общей частью (пересечением) двух прямоугольников. Программа
// должна содержать меню, позволяющее осуществлять проверку всех методов.

namespace d0gge
{
  class Rectangle
  {
    // Структура, характеризующая позицию прямоугольника
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
    private Position _pos;

    // Конструктор для создания прямоугольника
    // int id - индетификатор прямоугольника, int originX - абсцисса начала точки отрисовки прямоугольника
    // int originY - ордината начала точки отрисовки прямоугольника
    // int widht, height - ширина и высота прямоугольника соответственно
    public Rectangle(uint id, int orirginX_, int originY_, int width_, int height_)
    {
      _id = id;
      OriginX = orirginX_;
      OriginY = originY_;
      Height = height_;
      Width = width_;
      Area = Width * Height;
      Perimeter = 2 * Width + 2 * Height;
      ReconstructVertities();
    }

    public int Area { get; set; }
    public int Perimeter { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }

    public int OriginX { get; set; }
    public int OriginY { get; set; }

    // Возвращает позицию прямоугольника
    public Position GetPos()
    {
      return _pos;
    }

    // Метод создающий новый прямоугольник, содержащий в себе
    // два данных
    public void CreateFromRectangles(Rectangle rec1, Rectangle rec2)
    {
      Position pos1 = rec1.GetPos();
      Position pos2 = rec1.GetPos();
      if (!Convert.ToBoolean(rec1.OriginX - rec2.OriginX))
      {
        Width = rec1.Width;
      }
      else
      {
        Width = rec1.Width + rec2.Width;
      }
      if (!Convert.ToBoolean(rec1.OriginY - rec2.OriginY))
      {
        Height = rec1.Height;
      }
      else
      {
        Height = rec1.Height + rec2.Height;
      }
      OriginX = rec1.OriginX;
      OriginY = rec1.OriginY;
      ReconstructVertities();
    }

    public void Print()
    {
      Console.WriteLine($"Rectangle#{_id}. Location: v1({GetPos().x1}, {GetPos().y1}), "
      + $"v2({GetPos().x2}, {GetPos().y2}), v3({GetPos().x3}, {GetPos().y3}), v4({GetPos().x4}, {GetPos().y4})");
      Console.WriteLine($"Width: {Width}, height: {Height}");
    }

    // Метод обрезающий пересечение двух задданых прямоугольников
    // Rectangle rec1, rec2 -- пересекающиеся прямоугольники
    public void CropIntersection(Rectangle rec1, Rectangle rec2)
    {
      bool intersectsX = rec2.OriginX - rec1.OriginX < rec2.Width && rec2.OriginX - rec1.OriginX > -rec2.Width;
      bool intersectsY = rec2.OriginY - rec1.OriginY < rec2.Height && rec2.OriginY - rec1.OriginY > -rec2.Height;
      if (intersectsX && intersectsY)
      {
        Position pos1 = rec1.GetPos();
        Position pos2 = rec2.GetPos();
        // across
        if (pos2.y1 - pos2.y1 < 0 && pos2.y4 - pos1.y4 > 0)
        {
          Height = rec1.Height;
          OriginY = rec1.OriginY;
        }
        //below
        else if (pos2.y1 - pos2.y1 < 0 && pos2.y4 - pos1.y4 < 0)
        {
          Height = pos2.y4 - rec1.OriginY;
          OriginY = rec1.OriginY;

        }
        //above
        else if (pos2.y1 - pos2.y1 > 0 && pos2.y4 - pos1.y4 > 0)
        {
          Height = pos1.x4 - rec2.OriginY;
          OriginY = rec2.OriginY;
        }

        //inside
        if (pos2.x2 - pos1.x2 < 0 && pos2.x1 - pos1.x1 > 0)
        {
          Width = rec2.Width;
          OriginX = rec2.OriginX;
        }
        //right
        else if (pos2.x2 - pos1.x2 > 0 && pos2.x1 - pos1.x1 > 0)
        {
          Width = pos1.x2 - rec1.OriginX;
          OriginX = rec1.OriginX;
        }
        //left
        else if (pos2.x2 - pos1.x2 < 0 && pos2.x1 - pos1.x1 < 0)
        {
          Width = pos2.x2 - rec1.OriginX;
          OriginX = rec1.OriginX;
        }
        //across
        else
        {
          Width = rec1.Width;
          OriginX = rec1.OriginX;
        }
        ReconstructVertities();
      }
      else
      {
        Console.WriteLine("These rectangles do not intersect!");
      }
    }

    // Метод для изменения размеров прямоугольника
    // int width, height -- ширина и высота соответственно
    public void Resize(int width, int height)
    {
      Width = width;
      Height = height;
      ReconstructVertities();
    }

    // Перемещение на x, y координат соответственно.
    // int x -- координата икс, int y -- координата игрек
    public void Translate(int x, int y)
    {
      OriginX += x;
      OriginY += y;
      ReconstructVertities();

    }

    // Метод для перерисовки(пересоздания) прямоугольника
    private void ReconstructVertities()
    {
      _pos.x1 = OriginX;
      _pos.y1 = OriginY;
      _pos.x2 = _pos.x1 + Width;
      _pos.y2 = _pos.y1;
      _pos.x3 = _pos.x2;
      _pos.y3 = _pos.y2 + Height;
      _pos.x4 = _pos.x1;
      _pos.y4 = _pos.y1 + Height;
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

    // Метод создающий прямоугольник либо из вводимых координат
    // или же с использованием параметров по умолчанию, пользователь отказался от
    // ввода координат
    static Rectangle AskForDefault()
    {
      Console.WriteLine("Do you want to use default rectangle? (1/OK, 0/NO)");
      bool def = Int32.Parse(Console.ReadLine()) != 0;
      if (def)
      {
        return CreateRec();
      }
      Console.WriteLine("Origin(x, y), width, height: ");
      int x = Int32.Parse(Console.ReadLine());
      int y = Int32.Parse(Console.ReadLine()); int width = Int32.Parse(Console.ReadLine());
      int height = Int32.Parse(Console.ReadLine());
      return CreateRec(x, y, width, height);

    }

    // Метод запускающий тестирование перемещения прямоугольника
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

    // Метод запускающий тестирование изменения размеров прямоугольника
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

    // Метод запускающие тестирование конструирование прямоугольника
    // из двух других 
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
      rec.CreateFromRectangles(rec1, rec2);
      Console.WriteLine("Rectangle after construction: ");
      rec.Print();
    }

    // Метод запускающий тестирование выделения пересекающейся части 
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

    // Метод для создания прямоугольника
    // int x, y -- координаты, int width, height -- широта и высота соответственно
    static Rectangle CreateRec(int x = 0, int y = 0, int width = 3, int height = 2)
    {
      return new Rectangle(s_id++, x, y, width, height);
    }

    // Точка входа в программу
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
// ---------- //
// TEST CASES //
// ---------- //
// ВАЖНО!!!: вводить все разделяя enter'ом  
// 1. Перемещение
// Прямоугольник, созданный по умолчанию v1(0, 0), v2(3, 0), v3(3, 2), v4(0, 2)
// ВВОДИТЬ РАЗДЕЛЯЯ ПЕРЕНОСОМ СТРОКИ (enter)
// Перемещение (x: 3, y: 4)
// Ожидание v1(0 + 3, 0 + 4), (и т.д.) v2(6, 4), v3(6, 6), v4(3, 6)
// Получили v1(3, 4), v2(6, 4), v3(6, 6), v4(3, 6)
//
// 2. Изменение размера
// Прямоугольник, созданный по умолчанию v1(0, 0), v2(3, 0), v3(3, 2), v4(0, 2)
// Вводим новую ширину и длину 4, 5
// ожидаем: 3, 2 -> 4, 5
// получили Width: 4, height: 5
//
// 3. создание прямоугольника из двух заданных
// Прямоугольник 1: ширина 3 высота 2, координата начала 0, 0
// Прямоугольник 2: ширина 3 высота 2, координата начала 3, 0
// Ожидание: ширина 6, высота 2, v1(0, 0), v2(6, 0), v3(6, 2), v4(0, 2)
// получили: v1(0, 0), v2(6, 0), v3(6, 2), v4(0, 2)

