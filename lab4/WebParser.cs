using System.Net;

namespace d0gge
{
    public class WebParser 
    {
        public WebParser()
        {
            using (var handler = new HttpClientHandler())
            {
                handler.UseDefaultCredentials = true;
                _client = new HttpClient(handler);
            }
        }
        public static async Task<string> DownloadUrl(string url)
        {
            var res = await _client.GetStringAsync(url);
            return res;
        }

        public void SaveToFile(string path, string content)
        {
            File.WriteAllText(path, content);
        }

        private static HttpClient? _client;
    }
}