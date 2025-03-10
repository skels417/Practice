using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Task9
{
    public class CustomButton : Button
    {
        static CustomButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomButton), new FrameworkPropertyMetadata(typeof(CustomButton)));
        }

        protected override void OnClick()
        {
            base.OnClick();
            AnimateButton();
        }

        private void AnimateButton()
        {
            var scaleTransform = new ScaleTransform(1, 1);
            RenderTransform = scaleTransform;

            var animation = new DoubleAnimation(0.9, 1, TimeSpan.FromMilliseconds(100));
            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }
    }
}
