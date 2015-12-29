namespace Gu.Wpf.NumericInput
{
    using System.Windows;
    using System.Windows.Data;

    public abstract partial class NumericBox<T>
    {
        private static readonly DependencyProperty TextProxyProperty = DependencyProperty.Register(
            "TextProxy",
            typeof(string),
            typeof(NumericBox<T>),
            new PropertyMetadata(string.Empty, OnTextProxyChanged));

        private void BindValue()
        {
            var selfBinding = new Binding()
            {
                Source = this,
                Mode = BindingMode.OneTime
            };

            var valueBinding = new Binding
            {
                Path = BindingHelper.GetPath(ValueProperty),
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
                Converter = ToStringConverter<T>.Default,
                Mode = BindingMode.OneWay,
            };

            multiBinding.Bindings.Add(selfBinding);
            multiBinding.Bindings.Add(valueBinding);
            multiBinding.Bindings.Add(formatBinding);
            multiBinding.Bindings.Add(cultureBinding);

            BindingOperations.SetBinding(this, TextProxyProperty, multiBinding);
        }

        private static void OnTextProxyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(TextProperty, e.NewValue);
        }
    }
}
