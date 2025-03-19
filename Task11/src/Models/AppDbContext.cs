using System;
using System.Data.Entity;

namespace FortuneTellerApp.Models
{
    public class AppDbContext : DbContext
    {
       
        public DbSet<Prediction> Predictions { get; set; }
        public DbSet<User> Users { get; set; }

      
        public AppDbContext() : base("name=DefaultConnection")
        {
        }

        // Метод для настройки модели 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
         
        }
    }

    // Класс для инициализации базы данных
    public class AppDbInitializer : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            context.Predictions.Add(new Prediction
            {
                Text = "Любовь ждет вас за углом.",
                Author = "Машина Судьбы",
                CreatedAt = DateTime.Now,
                Category = PredictionCategory.Love
            });
            context.Predictions.Add(new Prediction
            {
                Text = "Ваша карьера скоро пойдет в гору.",
                Author = "Машина Судьбы",
                CreatedAt = DateTime.Now,
                Category = PredictionCategory.Career
            });
            context.Predictions.Add(new Prediction
            {
                Text = "Заботьтесь о своем здоровье.",
                Author = "Машина Судьбы",
                CreatedAt = DateTime.Now,
                Category = PredictionCategory.Health
            });
            context.Predictions.Add(new Prediction
            {
                Text = "Случайная удача на этой неделе!",
                Author = "Машина Судьбы",
                CreatedAt = DateTime.Now,
                Category = PredictionCategory.Random
            });

            context.SaveChanges(); 
        }
    }
}