namespace Gu.Wpf.NumericInput
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Data;
    using Gu.Wpf.NumericInput.Validation;

    [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
    public abstract partial class NumericBox<T>
    {
        private void SetupValidation()
        {
            var selfBinding = new Binding()
            {
                Source = this,
                Mode = BindingMode.OneTime
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


        }
    }
}
