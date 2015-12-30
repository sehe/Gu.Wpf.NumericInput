namespace Gu.Wpf.NumericInput
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    internal class FromStringConverter<T> : IValueConverter
        where T : struct, IComparable<T>, IFormattable, IConvertible, IEquatable<T>
    {
        public static readonly FromStringConverter<T> Default = new FromStringConverter<T>();

        private FromStringConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var box = (NumericBox<T>)parameter;
            var text = (string)value;
            if (string.IsNullOrEmpty(text))
            {
                if (box.CanValueBeNull)
                {
                    return null;
                }

                return Binding.DoNothing;
            }

            T result;
            if (box.TryParse(text, out result))
            {
                return result;
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Use Mode = \"OneWay\"");
        }
    }
}
