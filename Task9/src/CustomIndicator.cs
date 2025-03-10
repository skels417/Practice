using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Task9
{
    public class CustomIndicator : Control
    {
        static CustomIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomIndicator), new FrameworkPropertyMetadata(typeof(CustomIndicator)));
        }

        public static readonly DependencyProperty IsOnProperty =
            DependencyProperty.Register("IsOn", typeof(bool), typeof(CustomIndicator), new PropertyMetadata(false, OnIsOnChanged));

        public bool IsOn
        {
            get { return (bool)GetValue(IsOnProperty); }
            set { SetValue(IsOnProperty, value); }
        }

        private static void OnIsOnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var indicator = d as CustomIndicator;
            indicator.UpdateIndicator();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UpdateIndicator(); // Убедитесь, что цвет устанавливается при применении шаблона
        }

        private void UpdateIndicator()
        {
            // Обновляем цвет Ellipse в зависимости от состояния
            if (GetTemplateChild("IndicatorEllipse") is Ellipse ellipse)
            {
                ellipse.Fill = IsOn ? Brushes.Green : Brushes.Red; // Красный, если выключен, зеленый, если включен
            }
        }
    }
}