using System;

namespace Task3.Models
{
    public class Book
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Year { get; set; }
        public DateTime CreateDate { get; set; }

        public Book(string author, string title, string isbn, string year)
        {
            Author = author;
            Title = title;
            ISBN = isbn;
            Year = year;
            CreateDate = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Title: {Title}, Author: {Author}, ISBN: {ISBN}, Year: {Year}, Added: {CreateDate}";
        }
    }
}