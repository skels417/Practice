using System;

namespace FortuneTellerApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PredictionCount { get; set; }
        public DateTime LastPredictionDate { get; set; }

        public override string ToString()
        {
            return Name; 
        }
    }
}