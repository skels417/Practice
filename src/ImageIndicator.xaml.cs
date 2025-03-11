using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Task10
{
    public partial class ImageIndicator : UserControl
    {
        private List<string> _images = new List<string>();
        private DispatcherTimer _timer;

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(ImageIndicator), new PropertyMetadata(0, OnValueChanged));

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public ImageIndicator()
        {
            InitializeComponent();
            _timer = new DispatcherTimer();
            _timer.Tick += Timer_Tick;
        }

        public void AddImage(string imagePath)
        {
            _images.Add(imagePath);
            UpdateImage();
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ImageIndicator;
            control.UpdateImage();
        }

        private void UpdateImage()
        {
            if (_images.Count == 0)
            {
                // Заглушка
                DisplayedImage.Source = null; // или укажите путь к изображению-заглушке
                return;
            }

            if (Value < 0 || Value >= _images.Count)
            {
                // Заглушка
                DisplayedImage.Source = null; // или укажите путь к изображению-заглушке
            }
            else
            {
                DisplayedImage.Source = new BitmapImage(new Uri(_images[Value], UriKind.RelativeOrAbsolute));
            }
        }

        public void StartAnimation(int interval)
        {
            _timer.Interval = TimeSpan.FromMilliseconds(interval);
            _timer.Start();
        }

        public void StopAnimation()
        {
            _timer.Stop();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Value = (Value + 1) % _images.Count;
        }
    }
}