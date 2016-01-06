namespace Gu.Wpf.NumericInput
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Controls;
    using System.Windows.Input;

    internal class TextChangeMerger : IDisposable
    {
        private readonly TextBox valueBox;
        private readonly List<TextChangedEventArgs> args = new List<TextChangedEventArgs>();

        public TextChangeMerger(TextBox valueBox)
        {
            this.valueBox = valueBox;
            this.valueBox.PreviewTextInput += this.OnPreviewTextInput;
            this.valueBox.TextChanged += this.OnTextChanged;
        }

        public void Dispose()
        {
            this.valueBox.PreviewTextInput -= this.OnPreviewTextInput;
            this.valueBox.TextChanged -= this.OnTextChanged;
        }

        internal IReadOnlyList<TextChangedEventArgs> GetMergeEventArgs()
        {
            return this.args;
        }

        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            this.args.Add(e);
            e.Handled = true;
        }
    }
}
