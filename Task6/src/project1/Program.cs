using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        // Создаем три потока
        Thread thread1 = new Thread(() => PrintThreadInfo(1));
        Thread thread2 = new Thread(() => PrintThreadInfo(2));
        Thread thread3 = new Thread(() => PrintThreadInfo(3));

        // Запускаем потоки
        DateTime startTime = DateTime.Now;
        thread1.Start();
        thread2.Start();
        thread3.Start();

        // Ожидаем завершения потоков
        thread1.Join();
        thread2.Join();
        thread3.Join();
        DateTime endTime = DateTime.Now;

        // Выводим время выполнения
        TimeSpan executionTime = endTime - startTime;
        Console.WriteLine($"Общее время выполнения: {executionTime.TotalMilliseconds} мс");
    }

    static void PrintThreadInfo(int threadNumber)
    {
        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine($"Поток {threadNumber} - итерация {i}");
            Thread.Sleep(500); // Пауза 500 мс
        }
    }
}