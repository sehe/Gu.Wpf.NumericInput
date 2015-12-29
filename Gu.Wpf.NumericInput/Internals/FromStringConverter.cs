namespace Gu.Wpf.NumericInput
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    internal class FromStringConverter<T> : IMultiValueConverter
        where T : struct, IComparable<T>, IFormattable, IConvertible, IEquatable<T>
    {
        public static readonly FromStringConverter<T> Default = new FromStringConverter<T>();

        private FromStringConverter()
        {
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var box = (NumericBox<T>)values[0];
            var text = (string)values[1];
            if (string.IsNullOrEmpty(text))
            {
                if (box.CanValueBeNull)
                {
                    return null;
                }

                box.ValueProxy = box.Value;
                return Binding.DoNothing;
            }

            T result;
            if (box.TryParse(text, out result))
            {
                return result;
            }

            box.ValueProxy = box.Value;
            return Binding.DoNothing;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Use Mode = \"OneWay\"");
        }
    }
}
