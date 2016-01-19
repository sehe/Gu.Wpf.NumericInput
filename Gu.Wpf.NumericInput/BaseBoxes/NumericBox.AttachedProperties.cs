namespace Gu.Wpf.NumericInput
{
    using System;
    using System.Globalization;
    using System.Threading;
    using System.Windows;

    public static partial class NumericBox
    {
        public static readonly DependencyProperty CultureProperty = DependencyProperty.RegisterAttached(
            "Culture",
            typeof(IFormatProvider),
            typeof(NumericBox),
            new FrameworkPropertyMetadata(
                Thread.CurrentThread.CurrentUICulture,
                FrameworkPropertyMetadataOptions.Inherits));

        public static void SetCulture(this UIElement element, CultureInfo value)
        {
            element.SetValue(CultureProperty, value);
        }

        public static CultureInfo GetCulture(this UIElement element)
        {
            return (CultureInfo)element.GetValue(CultureProperty);
        }
    }
}
