using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace FortuneTellerApp
{
    public enum PredictionCategory { Love, Career, Health, Random }

    public class Prediction
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public PredictionCategory Category { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
