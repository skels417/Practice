using System.Windows;

namespace Task12
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new TaskViewModel();
        }
    }
}