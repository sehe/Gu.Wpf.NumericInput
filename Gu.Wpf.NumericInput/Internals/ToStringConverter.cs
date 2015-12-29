namespace Gu.Wpf.NumericInput
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    internal class ToStringConverter<T> : IMultiValueConverter
        where T : struct, IComparable<T>, IFormattable, IConvertible, IEquatable<T>
    {
        public static readonly ToStringConverter<T> Default = new ToStringConverter<T>();

        private ToStringConverter()
        {
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var box = (NumericBox<T>)values[0];
            var value = (IFormattable)values[1];
            if (value == null)
            {
                return string.Empty;
            }

            return value.ToString(box.StringFormat, box.Culture);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Use Mode = \"OneWay\"");
        }
    }
}