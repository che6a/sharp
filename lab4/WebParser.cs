using System.Text.RegularExpressions;

namespace d0gge
{
    public class WebParser : IDisposable
    {
        private static int s_counter = 0;
        private readonly HttpClient _client = new HttpClient();
        private readonly HashSet<string> _excludeExtensions =
            new HashSet<string>() 
            { 
                ".xml", ".ico", ".css", 
                ".png", ".jpg", ".svg", 
                ".jpeg", ".rng", ".rnc",
                ".xsd", ".ttl"
            };
        private List<string> _relativeLinks = new List<string>();
        private Queue<Uri> _linksQueue = new Queue<Uri>();
        private Stack<string> _fileStack = new Stack<string>();
        private int _currentDepth = 0;
        private Uri _uri;
        private int i = 0;

        public WebParser(Uri uri)
        {
            _client.DefaultRequestHeaders.Add("User-Agent", "Other");
            _uri = uri;
            Directory.CreateDirectory("tmp");
        }

        private void OnTargetFound(List<string> emails)
        {
            foreach (var email in emails)
                TargetFound?.Invoke(email);
        }

        public event Action<string> TargetFound;  

        public void Dispose()
        {
            _client.Dispose();
            Directory.Delete("tmp");
        }

        public int MaxDepth { get; set; } = 3;
        public int MaxLinks { get; set; } = 5;

        public void Parse(Uri uri = null)
        {
            if (uri == null) uri = _uri; 
            ++_currentDepth;
            if (_currentDepth >= MaxDepth)
            {
                DownloadPageByUrl(uri);
                GetEmails(_fileStack.Peek());
                File.Delete(_fileStack.Peek());
                _fileStack.Pop();
                --_currentDepth;
                return;
            }
            _linksQueue.Enqueue(uri);
            DownloadPageByUrl(uri);
            string file =_fileStack.Peek();
            List<Uri> links = ParseLinks(file);
            for (int i = 0; i < MaxLinks; ++i)
            {
                _linksQueue.Dequeue();
                _linksQueue.Enqueue(links[i]);
                Parse(_linksQueue.Peek());
            }
            --_currentDepth;
            GetEmails(_fileStack.Peek());
            File.Delete(_fileStack.Peek());
            _fileStack.Pop();
        }

        // метод для скачивания страницы. Параметры: string url - адрес страницы.
        private async Task<string> GetPage(Uri uri)
            => await _client.GetStringAsync(uri);

        private bool IsValid(string uri)
        {
            string ext = Path.GetExtension(uri).ToLower();
            if (_excludeExtensions.Contains(ext)) return false;
            if (!uri.Contains(_uri.Host)) return false;
            return true; 
        }
        public void DownloadPageByUrl(Uri uri)
        {
            string page = GetPage(uri).Result;
            string filePath = $"tmp/{Convert.ToString(s_counter++)}"; 
            SaveToFile(filePath, page);
        }
        private List<Uri> ParseLinks(string filePath)
        {
            string page = File.ReadAllText(filePath);
            var links = (from href in Regex.Matches(page, @"href=""[\/\w-\.:]+""").Cast<Match>()
                let url = href.Value.Replace("href=", "").Trim('"')
                let loc = url.StartsWith("/")
                let uri = loc ? $"{_uri.Scheme}://{_uri.Host}{url}" : url
                select new String(uri) 
            ).ToList();
            List<Uri> validatedLinks = new List<Uri>();
            foreach(var link in links)
            {
                if (!IsValid(link)) continue;
                validatedLinks.Add(new Uri(link));
            }
            return validatedLinks;
        }
        public void GetEmails(string filePath)
        {
            string page = File.ReadAllText(filePath);
            var emails = (from email in Regex.Matches(page, @"([-\w]+\[dot\])*[-\w]+\[at\][-\w]+(\[dot\][-\w]+)+").Cast<Match>()
                let formatted = email.Value.Replace("[at]", "@").Replace("[dot]", ".")
                select new String(formatted)
            ).ToList();
            OnTargetFound(emails);
        }
        private void SaveToFile(string path, string content)
        {
            File.WriteAllText(path, content);
            _fileStack.Push(path);
        }
    }
}