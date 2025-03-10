using System.Windows;

namespace Task9
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Изначально цвет индикатора будет красным, так как IsOn = false
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            indicator.IsOn = !indicator.IsOn; // Переключаем состояние индикатора
        }
    }
}