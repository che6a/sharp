using System;

// all source code is available on https://github.com/d0gge/sharp.git
// Гаврилов Е.А. КЭ-219
// Вариант 2
// Составить описание класса для объектов-векторов, задаваемых координатами концов в
// двухмерном пространстве. Обеспечить операции сложения и вычитания векторов с получением
// нового вектора (суммы и разности), вычисления длины вектора, умножения вектора на скалярную
// величину. Операции сложения, вычитания и умножения на скалярную величину реализовать в
// виде перегрузки операторов. Программа должна содержать меню, позволяющее осуществлять
// проверку всех методов

namespace d0gge
{
  public class Vector2
  {
    public Vector2() { }
    public Vector2(int x, int y)
    {
      X = x;
      Y = y;
    }

    // Длина вектора return value: double длина
    public double Length()
    {
      return Math.Sqrt(this.X * this.X + this.Y * this.Y);
    }

    // Переопределение оператора + для сложения векторов
    public static Vector2 operator +(Vector2 lhs, Vector2 rhs)
        => new Vector2(lhs.X + rhs.X, lhs.Y + rhs.Y);

    // Переопределение оператора - для сложения векторов
    public static Vector2 operator -(Vector2 lhs, Vector2 rhs)
        => new Vector2(lhs.X - rhs.X, lhs.Y - rhs.Y);

    // Переопределение оператора + умножения на скаляр
    public static Vector2 operator *(Vector2 lhs, int scalar)
        => new Vector2(lhs.X * scalar, lhs.Y * scalar);

    // Переопределение оператора * умножения на скаляр 
    public static Vector2 operator *(int scalar, Vector2 rhs)
        => new Vector2(rhs.X * scalar, rhs.Y * scalar);

    // Переопределение стандартного метода для ввывода в консоль
    public override string ToString() => $"({X}, {Y})";
    public int X { get; private set; }
    public int Y { get; private set; }
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

    // Тест суммирования векторов
    static void SumTest()
    {
      Console.Clear();
      Console.WriteLine("Sum Test.");
      Vector2 vec1 = new Vector2(1, 2);
      Vector2 vec2 = new Vector2(2, 1);
      Console.WriteLine($"vec1: {vec1} + vec2: {vec2} = vec3: {vec1 + vec2}");
    }

    // Тест вычитания векторов
    static void SubTest()
    {
      Console.Clear();
      Console.WriteLine("Sub Test.");
      Vector2 vec1 = new Vector2(1, 2);
      Vector2 vec2 = new Vector2(2, 1);
      Console.WriteLine($"vec1: {vec1} - vec2: {vec2} = vec3: {vec1 - vec2}");
    }
    // Тест умножения вектора на скаляр
    static void MultiplyTest()
    {
      Console.Clear();
      Console.WriteLine("Multiplication by scalar Test.");
      Vector2 vec1 = new Vector2(1, 2);
      Console.WriteLine($"3 * vec1{vec1} = {3 * vec1}");

    }

    //тест получения длины вектора
    static void LengthTest()
    {
      Console.Clear();
      Console.WriteLine("Length of a vector test.");
      Vector2 vec1 = new Vector2(6, 6);
      Console.WriteLine($"Length of vec1{vec1}: {vec1.Length()}");
    }

    // точка входа в программу
    static void Main()
    {
      while (true)
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
        TestType number = (TestType)Int32.Parse(Console.ReadLine());
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

// --------- //
// TEST CASES //
// --------- //

// 1. (1, 2) + (2, 1) = (3, 3)
// 2. (1, 2) - (2, 1) = (-1, 1)
// 3. 3 * (1, 2) = (3, 6)
// 4. (6, 6) = sqrt(6^2 + 6^2) ~ 8.48...