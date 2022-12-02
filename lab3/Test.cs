namespace d0gge
{
  public static class Test
  {
    public enum Type
    {
      Exit = 0,
      Sort,
      Print,
      PrintLast,
      AverageTakeOffMass,
      Serialization,
      WrongFormat
    }

    static string s_fileName = "testXml.xml";
    static string s_allowedExtension = "xml";

    // вспомогательный метод для заполнения авиалинии тестовыми значениями
    private static void FillAirline(Airline airline, bool printAvgMass = false)
    {
      airline.AddPlane(new CargoPlane("Antonov An-22", airline.RaceNumber,
          14230, 10000, new String[4] { "john", "lil john", "Sarah", "Bella" }));
      if (printAvgMass) Console.WriteLine(airline.AverageTakeOffMass);
      airline.AddPlane(new PassengerPlane("Airbus A350 XWB", airline.RaceNumber,
          100000, 120, new String[4] { "Jim", "Livesey", "Trelawney", "Smollett" }));
      if (printAvgMass) Console.WriteLine(airline.AverageTakeOffMass);
      airline.AddPlane(new CargoPlane("Antonov An-225", airline.RaceNumber,
          12801, 5000, new String[4] { "Bob", "Tom", "Angel", "Saren" }));
      if (printAvgMass) Console.WriteLine(airline.AverageTakeOffMass);
      airline.AddPlane(new PassengerPlane("Boeing 737", airline.RaceNumber,
          13845, 200, new String[4] { "Matthew", "Bred", "Peggy", "Alice" }));
      if (printAvgMass) Console.WriteLine(airline.AverageTakeOffMass);
      airline.AddPlane(new CargoPlane("Boeing C-17", airline.RaceNumber,
          14999, 7000, new String[4] { "Slim", "Shady", "Charley", "Penny" }));
      if (printAvgMass) Console.WriteLine(airline.AverageTakeOffMass);
      airline.AddPlane(new PassengerPlane("Boeing 747", airline.RaceNumber,
          12384, 240, new String[4] { "William", "Richard", "Johanna", "Scarlet" }));
      if (printAvgMass) Console.WriteLine(airline.AverageTakeOffMass);
      airline.AddPlane(new PassengerPlane("Ticket-to-Haven", airline.RaceNumber,
          88888, 4, new String[4] { "XXX", "XXX", "XXX", "XXX" }));
      if (printAvgMass) Console.WriteLine(airline.AverageTakeOffMass);
      airline.AddPlane(new CargoPlane("Bomb", airline.RaceNumber,
          66666, 6666, new String[4] { "peppe", "stickman", "someone_else", "anonymus" }));
      if (printAvgMass) Console.WriteLine(airline.AverageTakeOffMass);
      Console.WriteLine();
    }

    // Вспомогательный метод, для вывода конца теста
    private static void End()
    {
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    // Tест сортировки самолетов в авиалайне
    public static void SortTest()
    {
      Console.Clear();
      Console.WriteLine("Sort test.");
      Airline airline = new Airline();
      FillAirline(airline);
      Console.WriteLine("Before test: ");
      foreach (Plane plane in airline.Planes)
        Console.WriteLine(plane);
      airline.SortFlights();
      Console.WriteLine("After sort: ");
      foreach (Plane plane in airline.Planes)
        Console.WriteLine(plane);
      End();
    }

    // Тест вывода рейсов в авиалинии
    public static void PrintTest()
    {
      Console.Clear();
      Console.WriteLine("Print test");
      Airline airline = new Airline();
      FillAirline(airline);
      airline.SortFlights();
      airline.PrintFilgths();
      End();
    }

    // Тест вывода последних рейсов
    public static void PrintLastTest()
    {
      Console.Clear();
      Console.WriteLine("Print last test");
      Airline airline = new Airline();
      FillAirline(airline);
      airline.SortFlights();
      airline.PrintLastFlights();
      End();
    }

    // Tecт расчёт взлётной массы
    public static void AverageTakeOffMassTest()
    {
      Console.Clear();
      Console.WriteLine("Average take off mass test.");
      Airline airline = new Airline();
      FillAirline(airline, true);
      Console.WriteLine($"Total average take off mass: {airline.AverageTakeOffMass}");
      Console.WriteLine();
      End();
    }

    // Tест сериализации/десериализации в/из XML
    public static void SerializationTest()
    {
      Console.Clear();
      Console.WriteLine("Serialization tets.");
      Console.WriteLine("Serializing to XML.");
      Airline airline = new Airline();
      FillAirline(airline);
      airline.ToXml(s_fileName);
      End();
      Console.WriteLine("Deserialization.");
      try
      {
        var newAirline = Airline.FromXml(s_fileName);
        newAirline.PrintFilgths();
      }
      catch (Exception e) { Console.WriteLine($"[ERROR]: {e.Message} "); }
      End();
    }

    // Tест неверного формата файла при сериализации 
    public static void WrongFormatTest()
    {
      Console.Clear();
      Console.WriteLine("Wrong format test.");
      Console.WriteLine("Write a file name to save xml: ");
      string? fileName = Console.ReadLine();
      if (fileName != null)
      {
        string[] subs = fileName.Split('.');
        string ext = subs[subs.Length - 1];
        if (!ext.Equals(s_allowedExtension))
        {
          Console.WriteLine("[ERROR]: WRONG FORMAT");
          End();
          return;
        }
        Airline airline = new Airline();
        FillAirline(airline);
        airline.ToXml(fileName);
      }
      else
      {
        Console.WriteLine("Please, write name of the file.");
      }
      End();
    }
  }
}