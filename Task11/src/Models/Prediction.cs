using System;

namespace FortuneTellerApp.Models
{
    public class Prediction
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public PredictionCategory Category { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }

        public override string ToString()
        {
            return Text; 
        }
    }
}