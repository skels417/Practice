using System;
using System.Collections.Generic;
using System.Windows;
using FortuneTellerApp.Models;

namespace FortuneTellerApp
{
    public partial class PredictionManagementWindow : Window
    {
        private List<Prediction> _predictions; // Список предсказаний

        public PredictionManagementWindow()
        {
            InitializeComponent();
            _predictions = new List<Prediction>(); 
            UpdatePredictionList(); 
        }

        private void UpdatePredictionList()
        {
            PredictionList.ItemsSource = null; 
            PredictionList.ItemsSource = _predictions; 
        }

        private void AddPredictionButton_Click(object sender, RoutedEventArgs e)
        {
           
            var newPrediction = new Prediction { Text = "Новое предсказание", Author = "Автор", CreatedAt = DateTime.Now, Category = PredictionCategory.Random };
            _predictions.Add(newPrediction);
            UpdatePredictionList();
        }

        private void EditPredictionButton_Click(object sender, RoutedEventArgs e)
        {
            if (PredictionList.SelectedItem is Prediction selectedPrediction)
            {
                var editWindow = new EditPredictionWindow(selectedPrediction);
                if (editWindow.ShowDialog() == true)
                {
                    UpdatePredictionList(); 
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите предсказание для редактирования.");
            }
        }

        private void DeletePredictionButton_Click(object sender, RoutedEventArgs e)
        {
            if (PredictionList.SelectedItem is Prediction selectedPrediction)
            {
                _predictions.Remove(selectedPrediction);
                UpdatePredictionList(); 
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите предсказание для удаления.");
            }
        }

        // Обработчик для лайка предсказания
        private void LikePredictionButton_Click(object sender, RoutedEventArgs e)
        {
            if (PredictionList.SelectedItem is Prediction selectedPrediction)
            {
                selectedPrediction.Likes++;
                MessageBox.Show($"Вы лайкнули предсказание: {selectedPrediction.Text}. Лайков: {selectedPrediction.Likes}");
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите предсказание для лайка.");
            }
        }

        // Обработчик для дизлайка предсказания
        private void DislikePredictionButton_Click(object sender, RoutedEventArgs e)
        {
            if (PredictionList.SelectedItem is Prediction selectedPrediction)
            {
                selectedPrediction.Dislikes++;
                MessageBox.Show($"Вы дизлайкнули предсказание: {selectedPrediction.Text}. Дизлайков: {selectedPrediction.Dislikes}");
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите предсказание для дизлайка.");
            }
        }
    }
}