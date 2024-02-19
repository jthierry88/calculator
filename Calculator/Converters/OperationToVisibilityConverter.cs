using Calculator.ViewModel;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Calculator.Converters
{
    public class OperationToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value is Operation currentOperation && parameter is Operation buttonOperation)
            {
                if (currentOperation != Operation.None)
                {
                    if (currentOperation == buttonOperation)
                    {
                        return Visibility.Visible;
                    }
                    else
                    {
                        return Visibility.Collapsed;
                    }
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }



            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
