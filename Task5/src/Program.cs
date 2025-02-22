using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Task5
{
    class Program
    {
        private static Dictionary<string, DeviceData> configuration;
        private static List<string> dataLines; // Для хранения строк данных

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Меню:");
                Console.WriteLine("1. Прочитать конфигурацию");
                Console.WriteLine("2. Вывести конфигурацию");
                Console.WriteLine("3. Прочитать файл с данными");
                Console.WriteLine("4. Вывести строки данных (с N до M)");
                Console.WriteLine("5. Вывести полезные данные");
                Console.WriteLine("6. Экспортировать полезные данные в XLS");
                Console.WriteLine("7. Выход");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ReadConfiguration();
                        break;
                    case "2":
                        PrintConfiguration();
                        break;
                    case "3":
                        ReadDataFile();
                        break;
                    case "4":
                        PrintDataLines();
                        break;
                    case "5":
                        PrintUsefulData();
                        break;
                    case "6":
                        ExportUsefulDataToExcel();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        private static void ReadConfiguration()
        {
            string configPath = "config.json"; // Путь к конфигурации
            using (var streamReader = new StreamReader(configPath))
            {
                var str = streamReader.ReadToEnd();
                configuration = JsonSerializer.Deserialize<Dictionary<string, DeviceData>>(str);
            }
            Console.WriteLine("Конфигурация прочитана.");
        }

        private static void PrintConfiguration()
        {
            if (configuration == null)
            {
                Console.WriteLine("Конфигурация не загружена.");
                return;
            }

            foreach (var device in configuration)
            {
                Console.WriteLine($"ID: {device.Key}, Name: {device.Value.Name}");
            }
        }

        private static void ReadDataFile()
        {
            string[] csvFiles = Directory.GetFiles("data", "*.csv");
            Console.WriteLine("Выберите файл для чтения:");
            for (int i = 0; i < csvFiles.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileName(csvFiles[i])}");
            }

            int fileIndex = int.Parse(Console.ReadLine()) - 1;
            if (fileIndex < 0 || fileIndex >= csvFiles.Length)
            {
                Console.WriteLine("Неверный выбор файла.");
                return;
            }

            dataLines = new List<string>(File.ReadAllLines(csvFiles[fileIndex]));
            Console.WriteLine("Данные из файла прочитаны.");
        }

        private static void PrintDataLines()
        {
            Console.WriteLine("Введите N:");
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите M:");
            int m = int.Parse(Console.ReadLine());

            if (dataLines == null || dataLines.Count == 0)
            {
                Console.WriteLine("Данные не загружены. Пожалуйста, сначала прочитайте файл с данными.");
                return;
            }

            if (n < 1 || m < 1 || n > dataLines.Count || m > dataLines.Count || n > m)
            {
                Console.WriteLine("Неверные значения N и M.");
                return;
            }

            for (int i = n - 1; i < m; i++)
            {
                Console.WriteLine(dataLines[i]);
            }
        }

        private static void PrintUsefulData()
        {
            if (configuration == null)
            {
                Console.WriteLine("Конфигурация не загружена.");
                return;
            }

            foreach (var device in configuration)
            {
                Console.WriteLine($"ID: {device.Key}, Name: {device.Value.Name}");
                foreach (var dataItem in device.Value.Data)
                {
                    Console.WriteLine($"  Variable: {dataItem.Variable}, Format: {dataItem.Format}, Unit: {dataItem.Unit}");
                }
            }
        }

        private static void ExportUsefulDataToExcel()
        {
            if (configuration == null)
            {
                Console.WriteLine("Конфигурация не загружена.");
                return;
            }

            var interpretedDataList = new List<InterpretedData>();

            foreach (var device in configuration)
            {
                foreach (var dataItem in device.Value.Data)
                {
                    var interpretedData = new InterpretedData
                    {
                        Time = DateTime.Now,
                        Values = new Dictionary<string, object>
                    {
                        { "Variable", dataItem.Variable },
                        { "Format", dataItem.Format },
                        { "Unit", dataItem.Unit },
                        { "Coefficient", dataItem.Coefficient },
                        { "Offset", dataItem.Offset }
                    }
                    };
                    interpretedDataList.Add(interpretedData);
                }
            }

            ExportToExce.ExportToExcel(interpretedDataList);
        }
    }
}