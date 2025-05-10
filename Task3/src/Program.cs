using System;
using Task3.Models;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();
            library.ReadFile();

            while (true)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Чтение из файла");
                Console.WriteLine("2. Сохранить в файл");
                Console.WriteLine("3. Добавить книгу");
                Console.WriteLine("4. Удалить книгу");
                Console.WriteLine("5. Вывести информацию об одной книге");
                Console.WriteLine("6. Вывести информацию о нескольких книгах");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите пункт меню: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        library.ReadFile();
                        break;
                    case "2":
                        library.WriteFile();
                        break;
                    case "3":
                        library.AddBook();
                        break;
                    case "4":
                        library.RemoveBook();
                        break;
                    case "5":
                        Console.Write("Введите ISBN книги для отображения: ");
                        string isbn = Console.ReadLine();
                        library.PrintBook(isbn);
                        break;
                    case "6":
                        Console.Write(" Введите начальный индекс: ");
                        int startIndex = int.Parse(Console.ReadLine());
                        Console.Write("Введите конечный индекс: ");
                        int endIndex = int.Parse(Console.ReadLine());
                        library.PrintBook(startIndex, endIndex);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор, попробуйте снова.");
                        break;
                }
            }
        }
    }
}