using System;
using Task2.Classes;
using Task2.Interface;
using ILendable = Task2.Classes.ILendable;


namespace Task2
{
    public class Book : Item, ILendable
    {
        public Author Author { get; set; }
        public bool IsAvailable { get; private set; }

        public Book(string title, int publicationYear, Author author)
        {
            Title = title;
            PublicationYear = publicationYear;
            Author = author;
            IsAvailable = true;
        }

        public void Lend()
        {
            if (IsAvailable)
            {
                IsAvailable = false;
            }
            else
            {
                throw new InvalidOperationException("Книга уже выдана.");
            }
        }

        public void Return()
        {
            IsAvailable = true;
        }

        public string GetDetails()
        {
            return $"{Title} ({PublicationYear}) - Автор: {Author.Name}";
        }
    }
}
