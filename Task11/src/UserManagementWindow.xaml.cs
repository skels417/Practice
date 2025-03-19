using FortuneTellerApp.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace FortuneTellerApp
{
    public partial class UserManagementWindow : Window
    {
        private List<User> _users; // Список пользователей

        public UserManagementWindow()
        {
            InitializeComponent();
            _users = new List<User>(); // Инициализируем список пользователей
            UpdateUserList(); 
        }

        private void UpdateUserList()
        {
            UserList.ItemsSource = null; 
            UserList.ItemsSource = _users; 
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            
            var newUser = new User { Name = "Новый пользователь" }; 
            _users.Add(newUser);
            UpdateUserList();
        }

        private void EditUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserList.SelectedItem is User selectedUser )
            {
               
                var editUserWindow = new EditUserWindow(selectedUser);
                if (editUserWindow.ShowDialog() == true)
                {
                    
                    UpdateUserList();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пользователя для редактирования.");
            }
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserList.SelectedItem is User selectedUser )
            {
                _users.Remove(selectedUser);
                UpdateUserList();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пользователя для удаления.");
            }
        }
    }
}