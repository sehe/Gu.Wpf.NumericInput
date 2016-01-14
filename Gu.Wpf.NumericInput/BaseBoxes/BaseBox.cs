﻿namespace Gu.Wpf.NumericInput
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Threading;

    /// <summary>
    /// The reason for having this stuff here is enabling a shared style
    /// </summary>
    [TemplatePart(Name = IncreaseButtonName, Type = typeof(RepeatButton))]
    [TemplatePart(Name = DecreaseButtonName, Type = typeof(RepeatButton))]
    [TemplatePart(Name = FormattedName, Type = typeof(TextBox))]
    [TemplatePart(Name = SuffixBoxName, Type = typeof(TextBox))]
    public abstract partial class BaseBox : TextBox
    {
        public const string DecreaseButtonName = "PART_DecreaseButton";
        public const string IncreaseButtonName = "PART_IncreaseButton";
        public const string EditBoxName = "PART_EditText";
        public const string FormattedName = "PART_FormattedText";
        public const string SuffixBoxName = "PART_SuffixText";

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
            base.OnApplyTemplate();
            this.ValueBox = (TextBox)this.GetTemplateChild(EditBoxName) ?? this;
            this.UpdateFormattedView();
        }

        protected virtual void SetTextAndCreateUndoAction(string text)
        {
            var canUndo = this.CanUndo;
            this.TextSource = TextSource.UserInput;
            this.ValueBox.SetCurrentValue(TextProperty, text);
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

        protected void UpdateFormattedView()
        {
            var scrollViewer = this.ValueBox?.NestedChildren().OfType<ScrollViewer>().SingleOrDefault();
            var whenFocused = scrollViewer?.NestedChildren().OfType<ScrollContentPresenter>().SingleOrDefault();
            var grid = whenFocused?.Parent as Grid;
            if (scrollViewer == null || whenFocused == null || grid == null)
            {
                if (this.ValueBox?.IsArrangeValid == false)
                {
                    // retry after arrange
                    // using the Loaded event does not work if template is changed in runtime.
                    this.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(this.UpdateFormattedView));
                    return;
                }

                if (DesignerProperties.GetIsInDesignMode(this))
                {
                    var message = $"The template does not match the expected template. Cannot use formatting\r\n" +
                                  $"The expected template is (pseudo)\r\n" +
                                  $"{nameof(ScrollViewer)}: {(scrollViewer == null ? "null" : string.Empty)}\r\n" +
                                  $"  {nameof(Grid)}: {(grid == null ? "null" : string.Empty)}\r\n" +
                                  $"    {nameof(ScrollContentPresenter)}: {(whenFocused == null ? "null" : string.Empty)}";
                    throw new InvalidOperationException(message);
                }
                else
                {
                    // Falling back to vanilla textbox in runtime
                    return;
                }
            }

            var whenNotFocused = new TextBlock { Margin = new Thickness(2, 0, 2, 0), Name = FormattedName };
            whenNotFocused.Bind(TextBlock.TextProperty)
                          .OneWayTo(this, FormattedTextProperty);

            whenNotFocused.Bind(TextBox.VisibilityProperty)
                          .OneWayTo(this, IsKeyboardFocusWithinProperty, HiddenWhenTrueConverter.Default);

            grid.Children.Add(whenNotFocused);

            whenFocused.Bind(UIElement.VisibilityProperty)
                       .OneWayTo(this, IsKeyboardFocusWithinProperty, VisibleWhenTrueConverter.Default);
        }

        private void OnValueBoxLoaded(object sender, RoutedEventArgs e)
        {
            ((Control)sender).Loaded -= this.OnValueBoxLoaded;
            this.UpdateFormattedView();
        }
    }
}
