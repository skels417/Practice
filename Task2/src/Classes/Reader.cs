using System;
using System.Collections.Generic;
using Task2.Classes;

namespace Task2
{
    public class Reader : Person
    {
        public List<Book> BorrowedBooks { get; private set; }

        public Reader(string name, string birthDate) : base(name, birthDate)
        {
            BorrowedBooks = new List<Book>();
        }

        public void BorrowBook(Book book)
        {
            if (book.IsAvailable)
            {
                book.Lend();
                BorrowedBooks.Add(book);
            }
            else
            {
                throw new InvalidOperationException("Книга недоступна для выдачи.");
            }
        }

        public void ReturnBook(Book book)
        {
            if (BorrowedBooks.Contains(book))
            {
                book.Return();
                BorrowedBooks.Remove(book);
            }
            else
            {
                throw new InvalidOperationException("Эта книга не была взята.");
            }
        }
    }
}
