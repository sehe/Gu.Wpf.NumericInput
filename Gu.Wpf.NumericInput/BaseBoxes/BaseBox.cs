namespace Gu.Wpf.NumericInput
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    /// <summary>
    /// The reason for having this stuff here is enabling a shared style
    /// </summary>
    [TemplatePart(Name = IncreaseButtonName, Type = typeof(RepeatButton))]
    [TemplatePart(Name = DecreaseButtonName, Type = typeof(RepeatButton))]
    [TemplatePart(Name = ValueBoxName, Type = typeof(TextBox))]
    [TemplatePart(Name = SuffixBoxName, Type = typeof(TextBox))]
    public abstract partial class BaseBox : TextBox
    {
        public const string DecreaseButtonName = "DecreaseButton";
        public const string IncreaseButtonName = "IncreaseButton";
        public const string ValueBoxName = "ValueBox";
        public const string SuffixBoxName = "SuffixBox";

        protected BaseBox()
        {
            this.IncreaseCommand = new ManualRelayCommand(this.Increase, this.CanIncrease);
            this.DecreaseCommand = new ManualRelayCommand(this.Decrease, this.CanDecrease);
            this.Bind(TextProxyProperty).OneWayTo(this, TextProperty);
            this.ValueBox = this;
        }

        protected TextBox ValueBox { get; private set; }

        public override void OnApplyTemplate()
        {
            this.ValueBox = (TextBox)this.GetTemplateChild(ValueBoxName) ?? this;
            base.OnApplyTemplate();
        }

        protected virtual void SetTextAndCreateUndoAction(string text)
        {
            var canUndo = this.CanUndo;
            this.TextSource = TextSource.UserInput;
            this.ValueBox.SetCurrentValue(TextProperty, text);
            Debug.WriteLine(canUndo, this.CanUndo);
        }

        protected virtual void SetTextAndMergeUndoAction(string text)
        {
            var canUndo = this.CanUndo;
            if (this.TextSource == TextSource.UserInput)
            {
                var merger = new TextChangeMerger();
                this.ValueBox.TextChanged += merger.OnTextChanged;
                this.ValueBox.SetCurrentValue(TextProperty, text);
                var textChangedEventArgs = merger.GetMergeEventArgs();
                if (textChangedEventArgs != null)
                {
                    this.ValueBox.RaiseEvent(textChangedEventArgs);
                }

                this.ValueBox.TextChanged -= merger.OnTextChanged;
            }
            else
            {
                this.SetTextClearUndo(text);
            }

            Debug.WriteLine(canUndo, this.CanUndo);
        }

        protected virtual void SetTextClearUndo(string text)
        {
            var canUndo = this.CanUndo;
            var isUndoEnabled = this.ValueBox.IsUndoEnabled;
            this.ValueBox.IsUndoEnabled = false;
            this.ValueBox.SetCurrentValue(TextProperty, text);
            this.ValueBox.IsUndoEnabled = isUndoEnabled;
            Debug.WriteLine(canUndo, this.CanUndo);
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            base.OnPreviewTextInput(e);
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
        }

        /// <summary>
        /// Invoked when IncreaseCommand.CanExecute() is executed
        /// </summary>
        /// <param name="parameter">The inner <see cref="System.Windows.Controls.TextBox"/> showing the value in the controltemplate</param>
        /// <returns>True if the value can be increased</returns>
        protected abstract bool CanIncrease(object parameter);

        /// <summary>
        /// Invoked when IncreaseCommand.Execute() is executed
        /// </summary>
        /// <param name="parameter">The inner <see cref="System.Windows.Controls.TextBox"/> showing the value in the controltemplate</param>
        protected abstract void Increase(object parameter);

        /// <summary>
        /// Invoked when DecreaseCommand.CanExecute() is executed
        /// </summary>
        /// <param name="parameter">The inner <see cref="System.Windows.Controls.TextBox"/> showing the value in the controltemplate</param>
        /// <returns>True if the value can be decreased</returns>
        protected abstract bool CanDecrease(object parameter);

        /// <summary>
        /// Invoked when DecreaseCommand.Execute() is executed
        /// </summary>
        /// <param name="parameter">The inner <see cref="System.Windows.Controls.TextBox"/> showing the value in the controltemplate</param>
        protected abstract void Decrease(object parameter);

        protected virtual void CheckSpinners()
        {
            if (this.AllowSpinners)
            {
                // Not nice to cast like this but want to have ManualRelayCommand as internal
                ((ManualRelayCommand)this.IncreaseCommand).RaiseCanExecuteChanged();
                ((ManualRelayCommand)this.DecreaseCommand).RaiseCanExecuteChanged();
            }
        }

        protected override void OnIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs e)
        {
            if (Equals(e.NewValue, BooleanBoxes.False))
            {
                // this is needed because the inner textbox gets focus
                this.RaiseEvent(new RoutedEventArgs(LostFocusEvent));
            }

            base.OnIsKeyboardFocusWithinChanged(e);
        }

        protected virtual void OnStringFormatChanged(string oldFormat, string newFormat)
        {
        }

        protected virtual void OnCultureChanged(IFormatProvider oldCulture, IFormatProvider newCulture)
        {
        }
    }
}
