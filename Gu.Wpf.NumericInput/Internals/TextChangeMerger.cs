namespace Gu.Wpf.NumericInput
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Controls;

    internal class TextChangeMerger
    {
        private readonly List<TextChangedEventArgs> args = new List<TextChangedEventArgs>();

        public void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            this.args.Add(e);
            e.Handled = true;
        }

        public TextChangedEventArgs GetMergeEventArgs()
        {
            if (this.args.Count == 0)
            {
                return null;
            }

            var changes = this.args.SelectMany(x => x.Changes).ToList();
            var routedEvent = this.args[0].RoutedEvent;
            this.args.Clear();
            return new TextChangedEventArgs(routedEvent, UndoAction.Merge, changes);
        }
    }
}
