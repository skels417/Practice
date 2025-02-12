using System;

namespace Task2.Classes
{
    public class Person
    {
        public string Name { get; set; }
        public string BirthDate { get; set; }

        public Person(string name, string birthDate)
        {
            Name = name;
            BirthDate = birthDate;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Имя: {Name}, Дата рождения: {BirthDate}");
        }
    }
}
