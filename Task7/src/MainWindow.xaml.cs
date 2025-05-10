using System.Linq;
using System.Windows;
using System.Windows.Media;
using Task7;

namespace Task7
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void VerifyButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameInput.Text;

            if (IsValidName(name))
            {
                SecondWindow secondWindow = new SecondWindow(name);
                secondWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Введите корректное имя! Имя не должно быть пустым и не должно содержать цифр.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                NameInput.BorderBrush = Brushes.Red; //Визуальная индикация
                NameInput.Background = Brushes.LightPink; //Изменение фона для визуальной индикации
            }
        }

        private bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && !name.Any(char.IsDigit);
        }

        private void NameInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            NameInput.BorderBrush = Brushes.Gray; //Сброс цвета рамки
            NameInput.Background = Brushes.White; //Сброс фона
        }
    }
}