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

        private static readonly DependencyProperty RawTextProperty = DependencyProperty.Register(
            "RawText",
            typeof(string),
            typeof(NumericBox<T>),
            new PropertyMetadata(null, OnRawTextChanged));

        private static readonly DependencyProperty ValueProxyProperty = DependencyProperty.Register(
            "ValueProxy",
            typeof(T?),
            typeof(NumericBox<T>),
            new PropertyMetadata(default(T?), OnValueProxyChanged));

        private NumericBox()
        {
            var proxyBinding = this.CreateSelfBinding(TextProxyProperty, BindingMode.TwoWay);
            proxyBinding.NotifyOnValidationError = true;
            proxyBinding.ConverterParameter = this;
            proxyBinding.Converter = StringConverter<T>.Default;
            proxyBinding.ValidationRules.Add(CanParseRule<T>.Default);
            proxyBinding.ValidationRules.Add(RegexRule.Default);
            proxyBinding.ValidationRules.Add(IsGreaterThanOrEqualToMinRule<T>.Default);
            BindingOperations.SetBinding(this, ValueProxyProperty, proxyBinding);

            var textBinding = this.CreateSelfBinding(TextProperty, BindingMode.OneWay);
            BindingOperations.SetBinding(this, RawTextProperty, textBinding);
            //var valueBinding = this.CreateSelfBinding(ValueProperty);
            //valueBinding.Converter = ToStringConverter<T>.Default;
            //BindingOperations.SetBinding(this, TextProxyProperty, valueBinding);
        }

        internal T? ValueProxy
        {
            get { return (T?)GetValue(ValueProxyProperty); }
            set { SetValue(ValueProxyProperty, value); }
        }

        private static void OnValueProxyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private static void OnRawTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetCurrentValue(TextProxyProperty, e.NewValue);
        }

        private static void OnTextProxyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private Binding CreateSelfBinding(DependencyProperty property, BindingMode mode)
        {
            return new Binding
            {
                Path = BindingHelper.GetPath(property),
                Source = this,
                Mode = mode,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            };
        }
    }
}
