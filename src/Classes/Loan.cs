using System;
using System.Collections.Generic;


namespace Task2
{
    public class Loan
    {
        public Book Book { get; set; }
        public Reader Reader { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public Loan(Book book, Reader reader)
        {
            Book = book;
            Reader = reader;
            LoanDate = DateTime.Now;
            ReturnDate = LoanDate.AddDays(14);
        }

        public bool IsOverdue()
        {
            return DateTime.Now > ReturnDate;
        }

        public string GetLoanInfo()
        {
            return $"Книга: {Book.Title}, Читатель: {Reader.Name}, Дата выдачи: {LoanDate.ToShortDateString()}, Срок возврата: {ReturnDate.ToShortDateString()}";
        }
    }
}
