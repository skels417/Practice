using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var urls = new[]
        {
            "https://www.google.com",
            "https://www.example.com",
            "https://www.github.com"
        };

        var tasks = new Task<string>[urls.Length];

        for (int i = 0; i < urls.Length; i++)
        {
            tasks[i] = LoadPageAsync(urls[i]);
        }

        try
        {
            var results = await Task.WhenAll(tasks);

            for (int i = 0; i < results.Length; i++)
            {
                Console.WriteLine($"Длина содержимого страницы {urls[i]}: {results[i].Length} символов");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }

    static async Task<string> LoadPageAsync(string url)
    {
        using (var client = new HttpClient())
        {
            try
            {
                return await client.GetStringAsync(url);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Ошибка при загрузке {url}: {e.Message}");
                return string.Empty; 
            }
        }
    }
}