using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        //Хранение результата
        ConcurrentDictionary<int, long> factorials = new ConcurrentDictionary<int, long>();

        //Параллельное вычисление
        var stopwatch = Stopwatch.StartNew();
        Parallel.For(1, 11, i =>
        {
            factorials[i] = Factorial(i);
        });
        stopwatch.Stop();
        Console.WriteLine("Параллельное вычисление:");
        foreach (var kvp in factorials)
        {
            Console.WriteLine($"Факториал {kvp.Key} = {kvp.Value}");
        }
        Console.WriteLine($"Время выполнения (параллельно): {stopwatch.ElapsedMilliseconds} мс");

        //Обычное вычисление
        factorials.Clear(); //Очищаение хранения, для обычного вычисления
        stopwatch.Restart();
        for (int i = 1; i <= 10; i++)
        {
            factorials[i] = Factorial(i);
        }
        stopwatch.Stop();
        Console.WriteLine("\nОбычное вычисление:");
        foreach (var kvp in factorials)
        {
            Console.WriteLine($"Факториал {kvp.Key} = {kvp.Value}");
        }
        Console.WriteLine($"Время выполнения (последовательно): {stopwatch.ElapsedMilliseconds} мс");
    }

    static long Factorial(int n)
    {
        if (n == 0 || n == 1)
            return 1;
        long result = 1;
        for (int i = 2; i <= n; i++)
        {
            result *= i;
        }
        return result;
    }
}