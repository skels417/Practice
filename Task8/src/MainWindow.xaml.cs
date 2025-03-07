using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Task8
{
    public partial class MainWindow : Window
    {
        private bool isDragging = false;
        private Point clickPosition;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            textBox.Text = $"{button.Content} нажата!";
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Submitted: " + textBox.Text);
        }

        private void FontSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            textBox.FontSize = e.NewValue;
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (comboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string colorName = selectedItem.Content.ToString();
                this.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(colorName);
            }
        }

        private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (listBox.SelectedItem is ListBoxItem selectedItem)
            {
                textBox.Text = selectedItem.Content.ToString();
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            progressBar.IsIndeterminate = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            progressBar.IsIndeterminate = false;
        }

        private void DatePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (datePicker.SelectedDate.HasValue)
            {
                textBox.Text = datePicker.SelectedDate.Value.ToShortDateString();
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(canvas);
                Canvas.SetLeft(ellipse, currentPosition.X - clickPosition.X);
                Canvas.SetTop(ellipse, currentPosition.Y - clickPosition.Y);
            }
        }

        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            clickPosition = e.GetPosition(ellipse);
            ellipse.CaptureMouse();
        }

        private void Ellipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            ellipse.ReleaseMouseCapture();
        }
    }
}