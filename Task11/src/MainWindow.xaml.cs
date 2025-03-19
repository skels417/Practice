using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using FortuneTellerApp.Models;

namespace FortuneTellerApp
{
    public partial class MainWindow : Window
    {
        private AppDbContext _context;
        private User _currentUser;

        public MainWindow()
        {
            InitializeComponent();

            // Удаляем базу данных, если она существует
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;";
            string databaseName = "FortuneTellerDB";
            DatabaseManager.DropDatabase(connectionString, databaseName);
            Console.WriteLine("База данных удалена, если она существовала.");

            // Устанавливаем инициализатор базы данных
            Database.SetInitializer(new AppDbInitializer());
            _context = new AppDbContext();
            _context.Database.Initialize(force: true); // Инициализируем базу данных
            _currentUser = new User { Name = "Пользователь", PredictionCount = 0, LastPredictionDate = DateTime.Now };
            UpdateUserSummary();

            var shakeAnimation = (Storyboard)FindResource("ShakeAnimation");
            shakeAnimation.Completed += ShakeAnimation_Completed;
        }

        private void UpdateUserSummary()
        {
            UserSummaryTextBlock.Text = $"Пользователь: {_currentUser.Name}, Предсказания сегодня: {_currentUser.PredictionCount}";
        }

        private void GetPredictionButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentUser.PredictionCount >= 3)
            {
                MessageBox.Show("Вы достигли лимита предсказаний на сегодня.");
                return;
            }

            // анимация тряски
            var shakeAnimation = (Storyboard)FindResource("ShakeAnimation");
            shakeAnimation.Begin();
        }

        private void ShakeAnimation_Completed(object sender, EventArgs e)
        {
            ShowPrediction(); 
        }


        private string GetPrediction(PredictionCategory category)
        {
            // Загружаем предсказания из базы данных
            var predictions = _context.Predictions.Where(p => p.Category == category).ToList();

           
            Console.WriteLine($"Количество предсказаний для категории {category}: {predictions.Count}");

            if (predictions.Count > 0)
            {
                
                var random = new Random();
                // Получаем случайное предсказание
                return predictions[random.Next(predictions.Count)].Text;
            }
            else
            {
                // Если предсказаний нет, возвращаем сообщение
                return "Ваше предсказание: Удача на этой неделе!";
            }
        }

        private void ShowPrediction()
        {
            if (PredictionCategoryComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedTag = selectedItem.Tag.ToString();
                PredictionCategory selectedCategory = (PredictionCategory)Enum.Parse(typeof(PredictionCategory), selectedTag);

              
                var prediction = GetPrediction(selectedCategory);
                MessageBox.Show(prediction);

                _currentUser.PredictionCount++;
                _currentUser.LastPredictionDate = DateTime.Now;
                _context.SaveChanges(); 
                UpdateUserSummary();
            }
        }



        private void UserManagementButton_Click(object sender, RoutedEventArgs e)
        {
            var userManagementWindow = new UserManagementWindow();
            userManagementWindow.Show();
        }

        private void PredictionManagementButton_Click(object sender, RoutedEventArgs e)
        {
            var predictionManagementWindow = new PredictionManagementWindow();
            predictionManagementWindow.Show();
        }
    }
}