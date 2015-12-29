namespace Gu.Wpf.NumericInput
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Data;
    using Gu.Wpf.NumericInput.Validation;

    [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
    public abstract partial class NumericBox<T>
    {
        internal static readonly DependencyProperty ValueProxyProperty = DependencyProperty.Register(
            "ValueProxy",
            typeof(T?),
            typeof(NumericBox<T>),
            new PropertyMetadata(default(T?), OnValueProxyChanged));

        private void SetupValidation()
        {
            var selfBinding = new Binding()
            {
                Source = this,
                Mode = BindingMode.OneTime
            };

            var textBinding = new Binding
            {
                Path = BindingHelper.GetPath(TextProperty),
                Source = this,
                Mode = BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            var formatBinding = new Binding
            {
                Path = BindingHelper.GetPath(StringFormatProperty),
                Source = this,
                Mode = BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            var cultureBinding = new Binding
            {
                Path = BindingHelper.GetPath(CultureProperty),
                Source = this,
                Mode = BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            var multiBinding = new MultiBinding
            {
                Converter = FromStringConverter<T>.Default,
                Mode = BindingMode.OneWay,
            };

            multiBinding.ValidationRules.Add(CanParseRule<T>.Default);
            multiBinding.ValidationRules.Add(RegexRule.Default);
            multiBinding.ValidationRules.Add(IsGreaterThanMinRule<T>.Default);
            multiBinding.Bindings.Add(selfBinding);
            multiBinding.Bindings.Add(textBinding);
            multiBinding.Bindings.Add(formatBinding);
            multiBinding.Bindings.Add(cultureBinding);

            BindingOperations.SetBinding(this, ValueProperty, multiBinding);
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
    }
}
