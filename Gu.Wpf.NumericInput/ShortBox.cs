﻿namespace Gu.Wpf.NumericInput
{
    using System.ComponentModel;
    using System.Globalization;
    using System.Windows;

    [ToolboxItem(true)]
    public class ShortBox : NumericBox<short>
    {
        static ShortBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ShortBox), new FrameworkPropertyMetadata(typeof(ShortBox)));
            NumberStylesProperty.OverrideMetadata(
                typeof(ShortBox),
                new PropertyMetadata(NumberStyles.Integer));
            IncrementProperty.OverrideMetadataWithDefaultValue<short>(typeof(ShortBox), 1);
        }

        public ShortBox()
            : base(
            (x, y) => (short)(x + y),
            (x, y) => (short)(x - y))
        {
        }

        public override bool TryParse(string text, out short result)
        {
            return short.TryParse(text, this.NumberStyles, this.Culture, out result);
        }
    }
}