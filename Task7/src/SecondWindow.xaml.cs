using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Serialization;
using Newtonsoft.Json.Linq;

namespace Task7
{
    [Serializable]
    public class HistoryEntry
    {
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
    }

    public partial class SecondWindow : Window
    {
        public SecondWindow(string name)
        {
            InitializeComponent();
            GreetingLabel.Content = $"Привет, {name}!";
            LoadHistory();
            SetBackgroundColorBasedOnWeather();
            SaveToXml(name); //имя в XML
        }

        private async void SetBackgroundColorBasedOnWeather()
        {
            string weatherCondition = await GetWeatherCondition();
            if (weatherCondition != null)
            {
                SetBackgroundColor(weatherCondition);
            }
        }

        private async Task<string> GetWeatherCondition()
        {
            using (HttpClient client = new HttpClient())
            {
                string apiKey = "c2de128dbbd6beace8ab8674f3881aa9"; 
                string city = "Seversk, ru"; 
                string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

                try
                {
                    var response = await client.GetStringAsync(url);
                    var json = JObject.Parse(response);
                    var weather = json["weather"][0]["main"].ToString(); //основное состояние погоды
                    return weather;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при получении данных о погоде: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }
        }

        private void SetBackgroundColor(string weatherCondition)
        {
            switch (weatherCondition.ToLower())
            {
                case "clear":
                    this.Background = Brushes.LightBlue; 
                    break;
                case "clouds":
                    this.Background = Brushes.Gray;
                    break;
                case "rain":
                    this.Background = Brushes.Blue; 
                    break;
                case "snow":
                    this.Background = Brushes.White; 
                    break;
                default:
                    this.Background = Brushes.LightGray; 
                    break;
            }
        }

        private void LoadHistory()
        {
            //Логика загрузки истории
            List<HistoryEntry> historyEntries = LoadHistoryEntries();
            HistoryDataGrid.ItemsSource = historyEntries;
        }

        private List<HistoryEntry> LoadHistoryEntries()
        {
            
            return new List<HistoryEntry>
            {
                new HistoryEntry { Name = "Запись 1", DateTime = DateTime.Now.AddDays(-1) },
                new HistoryEntry { Name = "Запись 2", DateTime = DateTime.Now.AddDays(-2) }
            };
        }

        private void SaveToXml(string name)
        {
            //сохранение имени в XML
            var entries = new List<HistoryEntry>
            {
                new HistoryEntry { Name = name, DateTime = DateTime.Now }
            };

            XmlSerializer serializer = new XmlSerializer(typeof(List<HistoryEntry>));
            using (FileStream stream = new FileStream("history.xml", FileMode.Create))
            {
                serializer.Serialize(stream, entries);
            }
        }

        private void HistoryDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //обработка изменения выбора
            if (HistoryDataGrid.SelectedItem is HistoryEntry selectedEntry)
            {
                MessageBox.Show($"Вы выбрали: {selectedEntry.Name} - {selectedEntry.DateTime}");
            }
        }
    }
}