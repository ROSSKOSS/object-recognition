using ObjectRecognition.Foundation.Utilities;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace ObjectRecognition.Foundation.UI
{
    /// <summary>
    /// Interaction logic for Slider.xaml
    /// </summary>
    public partial class Slider : UserControl
    {
        public double Value { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public double ChangeAmount { get; set; }
        public double DefaultValue { get; set; }
        private double _rectangleMinimal;
        public string NameText { get; set; }
        public event MouseButtonEventHandler LeftArrowButtonUp;

        public event MouseButtonEventHandler RightArrowButtonUp;

        public Slider(string nameText = "Slider", double minValue = 0, double maxValue = 100, double defaultValue = 0,
            double changeAmount = 1)
        {
            InitializeComponent();
            Value = defaultValue;
            MinValue = minValue;
            MaxValue = maxValue;
            DefaultValue = defaultValue;
            _rectangleMinimal = Convert.ToDouble(amountRectangle.Width) / (MaxValue - MinValue);
            amountRectangle.Width *= 0.5;
            NameText = nameText;
            title.Content = $"{NameText}: {Value}";
            ChangeAmount = changeAmount;
        }

        protected virtual void OnLeftArrowMouseUp(MouseButtonEventArgs e)
        {
            ChangeValue(-ChangeAmount);
            if (LeftArrowButtonUp != null)
                LeftArrowButtonUp(this, e);
        }

        protected virtual void OnRightArrowMouseUp(MouseButtonEventArgs e)
        {
            ChangeValue(ChangeAmount);
            if (RightArrowButtonUp != null)
                RightArrowButtonUp(this, e);
        }

        #region LeftArrow

        private void leftGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            leftArrow.Fill =
                Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.ActionButton.EnterColor);
        }

        private void leftGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            leftArrow.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.ActionButton.Color);
        }

        private void leftGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            leftArrow.Fill =
                Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.ActionButton.DownColor);
        }

        private void leftGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OnLeftArrowMouseUp(e);
            leftArrow.Fill =
                Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.ActionButton.EnterColor);
        }

        #endregion LeftArrow

        #region RightArrow

        private void rightGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            rightArrow.Fill =
                Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.ActionButton.EnterColor);
        }

        private void rightGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            rightArrow.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.ActionButton.Color);
        }

        private void rightGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            rightArrow.Fill =
                Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.ActionButton.DownColor);
        }

        private void rightGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OnRightArrowMouseUp(e);
            rightArrow.Fill =
                Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.ActionButton.EnterColor);
        }

        #endregion RightArrow

        private void ChangeValue(double change)
        {
            Value += change;
            if (Value >= MinValue && Value <= MaxValue)
            {
                title.Content = $"{NameText}: {Value}";
                amountRectangle.Width += _rectangleMinimal * change;
            }
            else
            {
                if (Value < MinValue)
                    Value = MinValue;
                if (Value > MaxValue)
                    Value = MaxValue;
            }
        }

        public void Reset()
        {
            amountRectangle.Width = 128;
            Value = DefaultValue;
            title.Content = $"{NameText}: {Value}";
        }
    }
}