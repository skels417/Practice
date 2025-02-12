using System;


namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();

            Author author = new Author("Жюль Габриэль Верн", " французский писатель-фантаст, классик приключенческой литературы, автор '20 тысяч льё под водой'.");
            Book book1 = new Book("20 тысяч льё под водой", 1869, author);
            Book book2 = new Book("Путешествие к центру Земли", 1864, author);

            library.AddBook(book1);
            library.AddBook(book2);

            Reader reader = new Reader("Иван Парсаев", "07.05.2004");
            library.AddReader(reader);

            try
            {
                library.LendBook(reader, book1);
                Console.WriteLine($"{reader.Name} взял книгу: {book1.GetDetails()}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                library.ReturnBook(reader, book1);
                Console.WriteLine($"{reader.Name} вернул книгу: {book1.GetDetails()}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
