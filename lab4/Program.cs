using System.Text;

// Гаврилов Е.А. КЭ-219 Вариант - 2
// Разработать класс по анализу HTML-содержимого web-страниц заданного интернет ресурса
// (выбирается студентом самостоятельно), например, орг. структуры ЮУрГУ
// (https://www.susu.ru/ru/structure).
// Анализ должен осуществляться по всем страницам, URI которых включает базовый URI ресурса
// (интернет-домен, например, www.susu.ru). Предусмотреть настройку максимального уровня
// вложенности страниц в рекурсивном алгоритме анализа, а также максимального количества
// просматриваемых страниц.
// В класс добавить событие (event) по определению цели поиска, с передачей в его обработчик
// информации о названии ссылки, ведущей на страницу (т.е. имя ссылки, которое видит
// пользователь на базовой странице в браузере, например, «Филиалы»), URI страницы (например,
// https://www.susu.ru/ru/university/structure/electronics-higher-school/kafedry), уровне вложенности
// (например, 1) и самой цели поиска (см. разбивку задания по вариантам). Если целей на странице
// несколько, то событие вызывается для каждой цели. Обработчик события должен выводить
// информацию на консоль (или в окно) и в CSV-файл в табличной форме. CSV-файл можно открыть в
// Excel.

namespace d0gge
{
  public class Program
  {
    private static readonly string _csv = "emails.csv";
    static void Main()
    {
      Console.WriteLine("Для увеличения количества собранных ссылок поменяйте значения параметрoв MaxDepth(глубина поиска) & MaxLinks(ширина поиска) в WebParser.cs");
      File.WriteAllText(_csv, "");
      string uri = "https://www.susu.ru";
      HashSet<string> emails = new HashSet<string>();

      using (WebParser parser = new WebParser(new Uri(uri)))
      {
        parser.TargetFound += (string email) =>
        {
          if (emails.Contains(email)) return;
          emails.Add(email);
          StreamWriter sw = new StreamWriter(_csv, append: true);
          sw.WriteLine(email);
          Console.WriteLine(email);
          sw.Close();
        };
        parser.Parse();
      }
    }
  }
}


// TEST_CASE
// запустить и посмотреть .csv file