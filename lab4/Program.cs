using System.Net;
using System.Text.RegularExpressions;

namespace d0gge
{
    public class Program
    {
        static void Main()
        {
            string uri = "https://www.susu.ru";
            string uri2 = "https://www.avito.ru/";
            WebParser parser = new WebParser();
            try
            {
                var res = WebParser.DownloadUrl(uri);
                parser.SaveToFile("response.txt", res.Result);
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            // client.SaveToFile("response.txt", res.Result);
        }
    }
}