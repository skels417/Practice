using System;

namespace Task2.Classes
{
    public abstract class Item
    {
        public string Title { get; set; }
        public int PublicationYear { get; set; }
    }
    public interface ILendable
    {
        void Lend();
        void Return();
        bool IsAvailable { get; }
    }
}
