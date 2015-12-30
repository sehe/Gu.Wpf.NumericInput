namespace Gu.Wpf.NumericInput
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    internal class ToStringConverter<T> : IValueConverter
        where T : struct, IComparable<T>, IFormattable, IConvertible, IEquatable<T>
    {
        public static readonly ToStringConverter<T> Default = new ToStringConverter<T>();

        private ToStringConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var box = (NumericBox<T>)parameter;
            var formattable = (IFormattable)value;
            if (formattable == null)
            {
                return string.Empty;
            }

            return formattable.ToString(box.StringFormat, box.Culture);
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Use Mode = \"OneWay\"");
        }
    }
}