using System.Text;

namespace d0gge
{
    public class Program
    {
        private static readonly string _csv = "emails.csv";
        static void Main()
        {
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