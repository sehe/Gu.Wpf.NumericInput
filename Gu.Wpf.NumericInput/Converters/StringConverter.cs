namespace Gu.Wpf.NumericInput
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    internal class StringConverter<T> : IValueConverter
        where T : struct, IComparable<T>, IFormattable, IConvertible, IEquatable<T>
    {
        public static readonly StringConverter<T> Default = new StringConverter<T>();

        private StringConverter()
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
            var box = (NumericBox<T>)parameter;
            var formattable = (IFormattable)value;
            if (formattable == null)
            {
                return string.Empty;
            }

            return formattable.ToString(box.StringFormat, box.Culture);
        }
    }
}
