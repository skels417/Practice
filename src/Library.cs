using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Task3.Models
{
    public class Library
    {
        public List<Book> Books { get; private set; } = new List<Book>();
        private const string filePath = "books.json";

        public void AddBook()
        {
            Console.Write("Введите автора книги: ");
            string author = Console.ReadLine();
            Console.Write("Введите название книги: ");
            string title = Console.ReadLine();
            Console.Write("Введите ISBN книги: ");
            string isbn = Console.ReadLine();
            Console.Write("Введите год публикации: ");
            string year = Console.ReadLine();

            Books.Add(new Book(author, title, isbn, year));
            Console.WriteLine("Книга добавлена.");
        }

        public void RemoveBook()
        {
            Console.WriteLine("Список книг:");
            PrintBook();

            Console.Write("Введите ISBN книги для удаления: ");
            string isbn = Console.ReadLine();
            var bookToRemove = Books.FirstOrDefault(b => b.ISBN == isbn);

            if (bookToRemove != null)
            {
                Books.Remove(bookToRemove);
                Console.WriteLine("Книга удалена.");
            }
            else
            {
                Console.WriteLine("Книга с таким ISBN не найдена.");
            }
        }

        public void PrintBook()
        {
            if (Books.Count == 0)
            {
                Console.WriteLine("Нет книг в библиотеке.");
                return;
            }

            foreach (var book in Books)
            {
                Console.WriteLine(book);
            }
        }

        public void PrintBook(string isbn)
        {
            var book = Books.FirstOrDefault(b => b.ISBN == isbn);
            if (book != null)
            {
                Console.WriteLine(book);
            }
            else
            {
                Console.WriteLine("Книга с таким ISBN не найдена.");
            }
        }

        public void PrintBook(int startIndex, int endIndex)
        {
            if (startIndex < 0 || endIndex >= Books.Count || startIndex > endIndex)
            {
                Console.WriteLine("Неверные индексы.");
                return;
            }

            for (int i = startIndex; i <= endIndex; i++)
            {
                Console.WriteLine(Books[i]);
            }
        }

        public void ReadFile()
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                Books = JsonConvert.DeserializeObject<List<Book>>(json) ?? new List<Book>();
            }
            else
            {
                Console.WriteLine("Файл не обнаружен.");
            }
        }

        public void WriteFile()
        {
            var json = JsonConvert.SerializeObject(Books, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}