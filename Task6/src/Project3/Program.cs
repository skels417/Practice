using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Запуск асинхронно
        var stopwatch = Stopwatch.StartNew();

        try
        {
            await Task.WhenAll(
                Task.Run(() => PerformTask("Задача 1", 2000)),
                Task.Run(() => PerformTask("Задача 2", 1000)),
                Task.Run(() => PerformTaskWithException("Задача 3", 3000))
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Исключение: {ex.Message}");
        }

        stopwatch.Stop();
        Console.WriteLine($"Общее время выполнения (асинхронно): {stopwatch.ElapsedMilliseconds} мс");

        // Последовательный запуск
        stopwatch.Restart();
        PerformTask("Задача 1", 2000).Wait();
        PerformTask("Задача 2", 1000).Wait();
        PerformTask("Задача 3", 3000).Wait();
        stopwatch.Stop();
        Console.WriteLine($"Общее время выполнения (последовательно): {stopwatch.ElapsedMilliseconds} мс");
    }

    static async Task PerformTask(string taskName, int delay)
    {
        Console.WriteLine($"{taskName} началась.");
        await Task.Delay(delay);
        Console.WriteLine($"{taskName} завершилась.");
    }

    static async Task PerformTaskWithException(string taskName, int delay)
    {
        Console.WriteLine($"{taskName} началась.");
        await Task.Delay(delay);
        throw new Exception($"{taskName} выбросила исключение.");
    }
}