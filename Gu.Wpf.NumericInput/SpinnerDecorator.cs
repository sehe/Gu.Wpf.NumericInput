namespace Gu.Wpf.NumericInput
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    [ContentProperty(nameof(Child))]
    public class SpinnerDecorator : Control
    {
        public static readonly DependencyProperty ChildProperty = DependencyProperty.Register(
            "Child",
            typeof(BaseBox),
            typeof(SpinnerDecorator),
            new PropertyMetadata(default(BaseBox)));

        public BaseBox Child
        {
            get { return (BaseBox)this.GetValue(ChildProperty); }
            set { this.SetValue(ChildProperty, value); }
        }
    }
}
