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
        public int Value { get; set; }
        public int ChangeAmount { get; set; }
        private double _rectangleMinimal;
        public string NameText { get; set; }
        public event MouseButtonEventHandler LeftArrowButtonUp;

        public event MouseButtonEventHandler RightArrowButtonUp;

        public Slider(string nameText = "Slider", int defaultValue = 0, int changeAmount = 1)
        {
            InitializeComponent();
            Value = defaultValue;
            _rectangleMinimal = Convert.ToDouble(amountRectangle.Width) / 100;
            amountRectangle.Width = _rectangleMinimal * defaultValue;
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
            leftArrow.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.ActionButton.EnterColor);
        }

        private void leftGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            leftArrow.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.ActionButton.Color);
        }

        private void leftGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            leftArrow.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.ActionButton.DownColor);
        }

        private void leftGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OnLeftArrowMouseUp(e);
            leftArrow.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.ActionButton.EnterColor);
        }

        #endregion LeftArrow

        #region RightArrow

        private void rightGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            rightArrow.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.ActionButton.EnterColor);
        }

        private void rightGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            rightArrow.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.ActionButton.Color);
        }

        private void rightGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            rightArrow.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.ActionButton.DownColor);
        }

        private void rightGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OnRightArrowMouseUp(e);
            rightArrow.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.ActionButton.EnterColor);
        }

        #endregion RightArrow

        private void ChangeValue(int change)
        {
            Value += change;
            if (Value >= 0 && Value <= 100)
            {
                title.Content = $"{NameText}: {Value}";
                amountRectangle.Width += _rectangleMinimal * change;
            }
            else
            {
                if (Value < 0)
                    Value = 0;
                if (Value > 100)
                    Value = 100;
            }
        }
    }
}