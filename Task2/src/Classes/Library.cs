using System.Collections.Generic;


namespace Task2
{
    public class Library
    {
        public List<Book> Books { get; private set; }
        public List<Reader> Readers { get; private set; }

        public Library()
        {
            Books = new List<Book>();
            Readers = new List<Reader>();
        }

        public void AddBook(Book book)
        {
            Books.Add(book);
        }

        public void AddReader(Reader reader)
        {
            Readers.Add(reader);
        }

        public void LendBook(Reader reader, Book book)
        {
            reader.BorrowBook(book);
        }

        public void ReturnBook(Reader reader, Book book)
        {
            reader.ReturnBook(book);
        }
    }
}
