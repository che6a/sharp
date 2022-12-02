// all source code is available on https://github.com/d0gge/sharp.git
// Гаврилов Е.А. КЭ-219 
// Вариант 2
// На основе одной из готовых обобщенных (шаблонных) объектных коллекций .NET создать класс
// «Авиакомпания», включающий список самолетов. Классы самолетов должны образовывать
// иерархию с базовым классом. Самолеты бывают двух типов: пассажирские и грузовые. Описать в
// базовом классе абстрактный метод для расчета взлетного веса самолета. Для пассажирских
// авиалайнеров взлетный вес – это количество мест на средний вес человека (62 кг.), для
// грузовиков взлетный вес задается явно исходя из фактического веса груза, также взлетный вес
// включает вес самого самолета, он определяется исходя из типа авиалайнера. В виде меню
// программы реализовать нижеприведенный функционал.
// 1. Упорядочить всю последовательность авиалайнеров по возрастанию взлетного веса. При
// совпадении взлетного веса – упорядочивать данные по номеру рейса. Вывести номер рейса, тип
// самолета, список ФИО экипажа и взлетный вес для всех элементов списка.
// 2. Вывести первые 6 бортов из полученного в пункте 1 списка.
// 3. Вывести последние 2 номера рейса борта из полученного в пункте 1 списка.
// 4. В реальном времени (в процессе заполнения списка авиалайнеров) рассчитывать и
// поддерживать в актуальном состоянии средний взлетный вес самолета по авиакомпании,
// сохранить значение как поле класса «Авиакомпания».
// 5. Организовать запись и чтение всех данных в/из файла. Реализовать поддержку формата файлов
// XML.
// 6. Организовать обработку некорректного формата входного файла.
namespace d0gge
{
  public class Program
  {
    static void Main()
    {
      while (true)
      {
        Console.Clear();
        Console.WriteLine("Variant #2. Gavrilov E.A. KE-219");
        Console.WriteLine("1. Sort test.");
        Console.WriteLine("2. Print test.");
        Console.WriteLine("3. Print last test.");
        Console.WriteLine("4. Average take off mass test.");
        Console.WriteLine("5. Serialization test.");
        Console.WriteLine("6. Wrong format test.");
        Console.WriteLine("0. Exit.");

        Console.WriteLine();
        Console.WriteLine("Choose test: ");
        Test.Type number = (Test.Type)Int32.Parse(Console.ReadLine());
        if (number is Test.Type.Exit)
        {
          break;
        }
        switch (number)
        {
          case Test.Type.Sort:
            Test.SortTest();
            break;
          case Test.Type.Print:
            Test.PrintTest();
            break;
          case Test.Type.PrintLast:
            Test.PrintLastTest();
            break;
          case Test.Type.AverageTakeOffMass:
            Test.AverageTakeOffMassTest();
            break;
          case Test.Type.Serialization:
            Test.SerializationTest();
            break;
          case Test.Type.WrongFormat:
            Test.WrongFormatTest();
            break;
          default:
            break;
        }
      }
      Console.Clear();
    }
  }
}


// TEST CASES
// Запустить программу и задействовать все функции, после сериализации будет создан файл, который можно посмотреть
// Во всех тестах будет использованы стандартные рейсы, чтобы сэкономить время
// Рейсы можно посмотреть в Test.cs в методе FillAirline