using System;
using System.Threading;

class Program
{
    private static int counter = 0; //Общий счётчик
    private static readonly object lockObject = new object(); //Объект для блокировки

    static void Main(string[] args)
    {
        Thread thread1 = new Thread(IncrementCounter);
        Thread thread2 = new Thread(IncrementCounter);

        //Запуск
        thread1.Start();
        thread2.Start();

        //Завершение
        thread1.Join();
        thread2.Join();

        //Результат
        Console.WriteLine($"Итоговый счетчик: {counter}");
    }

    static void IncrementCounter()
    {
        for (int i = 0; i < 1000; i++)
        {
            lock (lockObject) //Блокировка доступа к счётчику
            {
                counter++; //Увеличение счётчика
            }
        }
    }
}