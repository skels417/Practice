using System.Windows;

namespace Task10
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            fanIndicator.AddImage("Images/0.png");
            fanIndicator.AddImage("Images/120.png");
            fanIndicator.AddImage("Images/240.png");
        }

        private void NextImage_Click(object sender, RoutedEventArgs e)
        {
            fanIndicator.Value++;
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(intervalTextBox.Text, out int interval))
            {
                fanIndicator.StartAnimation(interval);
            }
        }

        private void StartAnimation_Click(object sender, RoutedEventArgs e)
        {
            fanIndicator.StartAnimation(1000); // Запуск анимации с интервалом 1000 мс
        }

        private void StopAnimation_Click(object sender, RoutedEventArgs e)
        {
            fanIndicator.StopAnimation(); // Остановка анимации
        }
    }
}