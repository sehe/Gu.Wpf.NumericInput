namespace Gu.Wpf.NumericInput
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    using Gu.Wpf.NumericInput.Validation;

    internal class StringFormatConverter<T> : IValueConverter
        where T : struct, IComparable<T>, IFormattable, IConvertible, IEquatable<T>
    {
        internal static readonly StringFormatConverter<T> Default = new StringFormatConverter<T>();

        private StringFormatConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var box = (NumericBox<T>)parameter;
            if (box.TextSource == TextSource.ValueBinding)
            {
                return box.Format(box.Value);
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = (string)value;
            if (string.IsNullOrWhiteSpace(text))
            {
                return null;
            }

            var box = (NumericBox<T>)parameter;
            T result;
            if (box.TryParse(text, out result))
            {
                return result;
            }

            return Binding.DoNothing;
        }
    }
}