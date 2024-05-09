#nullable disable
using System.Globalization;

namespace BigBazar.Utils
{
    public class StringToEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum enumValue)
            {
                return enumValue.ToString();
            }

            throw new InvalidOperationException("The target must be an Enum.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                return Enum.Parse(targetType, stringValue);
            }
            if (value == null) return targetType.GetEnumValues().GetValue(0);
            if (value is not Enum) return targetType.GetEnumValues().GetValue(0);

            throw new InvalidOperationException("The source must be a string.");
        }
    }
}