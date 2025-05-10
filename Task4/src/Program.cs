using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System;
using Task4;

class Program
{
    private static List<Worker> workers = new List<Worker>();
    private static readonly string filePath = "workers.csv";
    private static readonly string logFilePath = "error.log";

    static void Main(string[] args)
    {
        LoadWorkersFromFile();

        while (true)
        {
            Console.WriteLine("\nУправление работниками: Меню");
            Console.WriteLine("1. Добавить работника");
            Console.WriteLine("2. Редактировать работника");
            Console.WriteLine("3. Удалить работника");
            Console.WriteLine("4. Вывести список работников на экран");
            Console.WriteLine("5. Сохранить в файл");
            Console.WriteLine("6. Загрузить из файла");
            Console.WriteLine("7. Выход");

            Console.Write("Введите ваш выбор: ");
            string choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        AddWorker();
                        break;
                    case "2":
                        EditWorker();
                        break;
                    case "3":
                        DeleteWorker();
                        break;
                    case "4":
                        DisplayWorkers();
                        break;
                    case "5":
                        SaveWorkersToFile();
                        break;
                    case "6":
                        LoadWorkersFromFile();
                        break;
                    case "7":
                        Console.WriteLine("Выход...");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте еще раз.");
                        break;
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
                Console.WriteLine($"Произошла непредвиденная ошибка: {ex.Message}.  Пожалуйста, посмотрите в файл логов для деталей.");
            }
        }
    }

    static void AddWorker()
    {
        try
        {
            Console.Write("Введите имя работника: ");
            string name = Console.ReadLine();

            Console.Write("Введите возраст работника: ");
            if (!int.TryParse(Console.ReadLine(), out int age))
            {
                throw new FormatException("Неверный формат возраста. Пожалуйста, введите число.");
            }
            if (age < 0)
            {
                throw new ArgumentException("Возраст не может быть отрицательным.");
            }

            Console.Write("Введите дату приема на работу (гггг-ММ-дд): ");
            if (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateOfEmployment))
            {
                throw new FormatException("Неверный формат даты. Пожалуйста, используйте гггг-ММ-дд.");
            }

            Console.Write("Введите должность работника: ");
            string grade = Console.ReadLine();

            Worker newWorker = new Worker(name, age, dateOfEmployment, grade);
            workers.Add(newWorker);

            Console.WriteLine("Работник успешно добавлен.");
        }
        catch (FormatException ex)
        {
            LogError(ex.Message);
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        catch (ArgumentException ex)
        {
            LogError(ex.Message);
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        catch (Exception ex)
        {
            LogError(ex.Message); // Запись в лог любых других непредвиденных исключений
            Console.WriteLine($"Произошла непредвиденная ошибка: {ex.Message}");
        }
    }

    static void EditWorker()
    {
        DisplayWorkers();
        if (workers.Count == 0)
        {
            Console.WriteLine("Нет работников для редактирования.");
            return;
        }

        Console.Write("Введите индекс работника для редактирования: ");
        if (!int.TryParse(Console.ReadLine(), out int index) || index < 0 || index >= workers.Count)
        {
            Console.WriteLine("Неверный индекс.");
            return;
        }

        try
        {
            Console.Write("Введите новое имя работника: ");
            string name = Console.ReadLine();

            Console.Write("Введите новый возраст работника: ");
            if (!int.TryParse(Console.ReadLine(), out int age))
            {
                throw new FormatException("Неверный формат возраста. Пожалуйста, введите число.");
            }
            if (age < 0)
            {
                throw new ArgumentException("Возраст не может быть отрицательным.");
            }

            Console.Write("Введите новую дату приема на работу (гггг-ММ-дд): ");
            if (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateOfEmployment))
            {
                throw new FormatException("Неверный формат даты. Пожалуйста, используйте гггг-ММ-дд.");
            }

            Console.Write("Введите новую должность работника: ");
            string grade = Console.ReadLine();

            workers[index].Name = name;
            workers[index].Age = age;
            workers[index].DateOfEmployment = dateOfEmployment;
            workers[index].Grade = grade;

            Console.WriteLine("Работник успешно отредактирован.");
        }
        catch (FormatException ex)
        {
            LogError(ex.Message);
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        catch (ArgumentException ex)
        {
            LogError(ex.Message);
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        catch (Exception ex)
        {
            LogError(ex.Message); // Запись в лог любых других непредвиденных исключений
            Console.WriteLine($"Произошла непредвиденная ошибка: {ex.Message}");
        }

    }

    static void DeleteWorker()
    {
        DisplayWorkers();
        if (workers.Count == 0)
        {
            Console.WriteLine("Нет работников для удаления.");
            return;
        }

        Console.Write("Введите индекс работника для удаления: ");
        if (!int.TryParse(Console.ReadLine(), out int index) || index < 0 || index >= workers.Count)
        {
            Console.WriteLine("Неверный индекс.");
            return;
        }

        workers.RemoveAt(index);
        Console.WriteLine("Работник успешно удален.");
    }


    static void DisplayWorkers()
    {
        if (workers.Count == 0)
        {
            Console.WriteLine("Нет работников для отображения.");
            return;
        }

        for (int i = 0; i < workers.Count; i++)
        {
            Console.WriteLine($"{i}: {workers[i]}");
        }
    }

    static void SaveWorkersToFile()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Запись заголовков
                writer.WriteLine("Name;Age;DateOfEmployment;Grade");

                foreach (Worker worker in workers)
                {
                    writer.WriteLine($"{worker.Name};{worker.Age};{worker.DateOfEmployment.ToString("yyyy-MM-dd")};{worker.Grade}");
                }
            }

            Console.WriteLine("Работники успешно сохранены в файл.");
        }
        catch (FileNotFoundException)
        {
            LogError("Файл не найден: " + filePath);
            Console.WriteLine("Ошибка: Файл не найден.");
        }
        catch (UnauthorizedAccessException)
        {
            LogError("Недостаточно прав для доступа к файлу: " + filePath);
            Console.WriteLine("Ошибка: Отказано в доступе к файлу.");
        }
        catch (IOException ex)
        {
            LogError("Ошибка ввода-вывода при сохранении в файл: " + ex.Message);
            Console.WriteLine("Ошибка: Произошла ошибка ввода-вывода: " + ex.Message);
        }
        catch (Exception ex)
        {
            LogError("Непредвиденная ошибка при сохранении в файл: " + ex.Message);
            Console.WriteLine($"Произошла непредвиденная ошибка при сохранении: {ex.Message}");
        }
    }

    static void LoadWorkersFromFile()
    {
        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл не найден. Начинаем с пустого списка работников.");
                workers = new List<Worker>(); // Убедитесь, что список инициализирован, если файл не существует.
                return;
            }

            workers.Clear(); // Очистить существующие данные перед загрузкой

            using (StreamReader reader = new StreamReader(filePath))
            {
                // Пропустить строку заголовка
                reader.ReadLine();

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(';');
                    if (parts.Length == 4)
                    {
                        string name = parts[0];
                        if (!int.TryParse(parts[1], out int age))
                        {
                            LogError($"Неверный формат возраста в файле: {line}");
                            Console.WriteLine($"Предупреждение: Строка пропущена из-за неверного формата возраста: {line}");
                            continue; // Перейти к следующей строке
                        }

                        if (!DateTime.TryParseExact(parts[2], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateOfEmployment))
                        {
                            LogError($"Неверный формат даты в файле: {line}");
                            Console.WriteLine($"Предупреждение: Строка пропущена из-за неверного формата даты: {line}");
                            continue; // Перейти к следующей строке
                        }

                        string grade = parts[3];

                        Worker worker = new Worker(name, age, dateOfEmployment, grade);
                        workers.Add(worker);
                    }
                    else
                    {
                        LogError($"Неверный формат данных в файле: {line}");
                        Console.WriteLine($"Предупреждение: Строка пропущена из-за неверного формата данных: {line}");
                    }
                }
            }

            Console.WriteLine("Работники успешно загружены из файла.");
        }
        catch (FileNotFoundException)
        {
            LogError("Файл не найден: " + filePath);
            Console.WriteLine("Ошибка: Файл не найден. Пожалуйста, убедитесь, что файл существует.");
            workers = new List<Worker>(); // Убедитесь, что список инициализирован, если файл не существует.
        }
        catch (UnauthorizedAccessException)
        {
            LogError("Недостаточно прав для доступа к файлу: " + filePath);
            Console.WriteLine("Ошибка: Отказано в доступе к файлу.");
            workers = new List<Worker>();
        }
        catch (IOException ex)
        {
            LogError("Ошибка ввода-вывода при загрузке из файла: " + ex.Message);
            Console.WriteLine("Ошибка: Произошла ошибка ввода-вывода: " + ex.Message);
            workers = new List<Worker>();
        }
        catch (Exception ex)
        {
            LogError("Непредвиденная ошибка при загрузке из файла: " + ex.Message);
            Console.WriteLine($"Произошла непредвиденная ошибка при загрузке: {ex.Message}");
            workers = new List<Worker>();
        }
    }


    static void LogError(string errorMessage)
    {
        try
        {
            using (StreamWriter writer = File.AppendText(logFilePath))
            {
                writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - Ошибка: {errorMessage}");
            }
        }
        catch (Exception)
        {
            // Если запись в лог не удалась, то мало что можно сделать, кроме как проигнорировать это.
            Console.WriteLine("Не удалось записать ошибку в лог. Проверьте права доступа к файлу логов.");
        }
    }
}
