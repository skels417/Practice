using FortuneTellerApp.Models;
using System.Windows;

namespace FortuneTellerApp
{
    public partial class EditUserWindow : Window
    {
        private User _user; // Пользователь, который редактируется

    public EditUserWindow(User user)
    {
        InitializeComponent();
        _user = user;
        UserNameTextBox.Text = _user.Name; // Устанавливаем текущее имя пользователя в TextBox
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        _user.Name = UserNameTextBox.Text; // Сохраняем новое имя
        DialogResult = true; //  редактирование прошло успешно
        Close(); // Закрываем окно
    }
}
}