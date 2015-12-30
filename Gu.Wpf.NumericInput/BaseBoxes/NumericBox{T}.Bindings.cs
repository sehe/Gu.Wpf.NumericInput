namespace Gu.Wpf.NumericInput
{
    using System.Windows;
    using System.Windows.Data;
    using Gu.Wpf.NumericInput.Validation;

    public abstract partial class NumericBox<T>
    {
        private static readonly DependencyProperty TextProxyProperty = DependencyProperty.Register(
            "TextProxy",
            typeof(string),
            typeof(NumericBox<T>),
            new PropertyMetadata(null, OnTextProxyChanged));

        private static readonly DependencyProperty ValueProxyProperty = DependencyProperty.Register(
            "ValueProxy",
            typeof(T?),
            typeof(NumericBox<T>),
            new PropertyMetadata(default(T?), OnValueProxyChanged));

        private NumericBox()
        {
            var textBinding = this.CreateSelfBinding(TextProperty);
            textBinding.ValidationRules.Add(CanParseRule<T>.Default);
            textBinding.ValidationRules.Add(RegexRule.Default);
            textBinding.ValidationRules.Add(IsGreaterThanMinRule<T>.Default);
            textBinding.Converter = FromStringConverter<T>.Default;
            BindingOperations.SetBinding(this, ValueProxyProperty, textBinding);

            var valueBinding = this.CreateSelfBinding(ValueProperty);
            valueBinding.Converter = ToStringConverter<T>.Default;
            BindingOperations.SetBinding(this, TextProxyProperty, valueBinding);
        }

        internal T? ValueProxy
        {
            get { return (T?)GetValue(ValueProxyProperty); }
            set { SetValue(ValueProxyProperty, value); }
        }

        private static void OnValueProxyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(ValueProperty, e.NewValue);
        }

        private static void OnTextProxyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(TextProperty, e.NewValue);
        }

        private Binding CreateSelfBinding(DependencyProperty property)
        {
            return new Binding
            {
                Path = BindingHelper.GetPath(property),
                Source = this,
                Mode = BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                ConverterParameter = this
            };
        }
    }
}
