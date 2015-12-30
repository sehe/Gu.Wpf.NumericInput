namespace Gu.Wpf.NumericInput
{
    using System.ComponentModel;
    using System.Windows;

    public abstract partial class NumericBox<T>
    {
        /// <summary>
        /// Identifies the ValueChanged event
        /// </summary>
        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent(
            "ValueChanged",
            RoutingStrategy.Direct,
            typeof(ValueChangedEventHandler<T>),
            typeof(NumericBox<T>));

        internal static readonly RoutedEvent FormatDirtyEvent = EventManager.RegisterRoutedEvent(
            "FormatDirty",
            RoutingStrategy.Direct,
            typeof(RoutedEventHandler),
            typeof(NumericBox<T>));

        internal static readonly RoutedEvent ValidationDirtyEvent = EventManager.RegisterRoutedEvent(
            "ValidationDirty",
            RoutingStrategy.Direct,
            typeof(RoutedEventHandler),
            typeof(NumericBox<T>));

        [Category("NumericBox")]
        [Browsable(true)]
        public event ValueChangedEventHandler<T> ValueChanged
        {
            add { this.AddHandler(ValueChangedEvent, value); }
            remove { this.RemoveHandler(ValueChangedEvent, value); }
        }

        internal event RoutedEventHandler FormatDirty
        {
            add { this.AddHandler(FormatDirtyEvent, value); }
            remove { this.RemoveHandler(FormatDirtyEvent, value); }
        }

        internal event RoutedEventHandler ValidationDirty
        {
            add { this.AddHandler(ValidationDirtyEvent, value); }
            remove { this.RemoveHandler(ValidationDirtyEvent, value); }
        }
    }
}
