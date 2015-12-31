﻿namespace Gu.Wpf.NumericInput
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using Gu.Wpf.NumericInput.Validation;

    /// <summary>
    /// Baseclass with common functionality for numeric textboxes
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Value"/> property</typeparam>
    public abstract partial class NumericBox<T> : BaseBox
        where T : struct, IComparable<T>, IFormattable, IConvertible, IEquatable<T>
    {
        private static readonly T TypeMin = (T)typeof(T).GetField("MinValue").GetValue(null);
        private static readonly T TypeMax = (T)typeof(T).GetField("MaxValue").GetValue(null);
        private readonly Func<T, T, T> add;
        private readonly Func<T, T, T> subtract;
        private static readonly EventHandler<ValidationErrorEventArgs> ValidationErrorHandler = OnValidationError;
        private static readonly RoutedEventHandler FormatDirtyHandler = OnFormatDirty;
        private static readonly RoutedEventHandler ValidationDirtyHandler = OnValidationDirty;

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericBox{T}"/> class.
        /// </summary>
        /// <param name="add">How to add two values (x, y) => x + y</param>
        /// <param name="subtract">How to subtract two values (x, y) => x - y</param>
        protected NumericBox(Func<T, T, T> add, Func<T, T, T> subtract)
        {
            this.add = add;
            this.subtract = subtract;

            var binding = new Binding
            {
                Path = BindingHelper.GetPath(ValueProperty),
                Source = this,
                Mode = BindingMode.TwoWay,
                NotifyOnValidationError = true,
                Converter = StringFormatConverter<T>.Default,
                ConverterParameter = this,
            };

            binding.ValidationRules.Add(CanParse<T>.Default);
            binding.ValidationRules.Add(IsMatch.Default);
            binding.ValidationRules.Add(IsGreaterThanOrEqualToMinRule<T>.Default);
            binding.ValidationRules.Add(IsLessThanOrEqualToMaxRule<T>.Default);
            BindingOperations.SetBinding(this, TextBindableProperty, binding);
            this.AddHandler(System.Windows.Controls.Validation.ErrorEvent, ValidationErrorHandler);
            this.AddHandler(FormatDirtyEvent, FormatDirtyHandler);
            this.AddHandler(ValidationDirtyEvent, ValidationDirtyHandler);
        }

        /// <summary>
        /// Gets the current value. Will throw if bad format
        /// </summary>
        internal T? CurrentValue => this.Parse(this.Text);

        internal T MaxLimit => this.MaxValue ?? TypeMax;

        internal T MinLimit => this.MinValue ?? TypeMin;

        public abstract bool TryParse(string text, out T result);

        public bool CanParse(string text)
        {
            if (this.CanValueBeNull && string.IsNullOrEmpty(text))
            {
                return true;
            }

            T temp;
            return this.TryParse(text, out temp);
        }

        public T? Parse(string text)
        {
            if (this.CanValueBeNull && string.IsNullOrEmpty(text))
            {
                return null;
            }

            T result;
            if (this.TryParse(text, out result))
            {
                return result;
            }

            throw new FormatException($"Could not parse {text} to an instance of {typeof(T)}");
        }

        public void UpdateFormat()
        {
            this.IsFormatting = true;
            if (System.Windows.Controls.Validation.GetHasError(this))
            {
                T result;
                if (this.TryParse(this.Text, out result))
                {
                    this.Text = result.ToString(this.StringFormat, this.Culture);
                }
            }
            else
            {
                this.Text = StringFormatConverter<T>.Default.GetFormattedText(this);
            }

            this.IsFormatting = false;
            this.IsFormattingDirty = false;
        }

        public void UpdateValidation()
        {
            var bindingExpression = BindingOperations.GetBindingExpression(this, TextBindableProperty);
            bindingExpression.ValidateWithoutUpdate();
            this.IsValidationDirty = false;
        }

        protected virtual void OnValueChanged(object newValue, object oldValue)
        {
            if (newValue != oldValue)
            {
                var args = new ValueChangedEventArgs<T?>((T?)oldValue, (T?)newValue, ValueChangedEvent, this);
                this.RaiseEvent(args);
                this.CheckSpinners();
            }
        }

        protected override bool CanIncrease(object parameter)
        {
            if (this.IsReadOnly || !this.IsEnabled)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(this.Text) || !this.CanParse(this.Text))
            {
                return false;
            }

            if (Comparer<T>.Default.Compare(this.CurrentValue.Value, this.MaxLimit) >= 0)
            {
                return false;
            }

            return true;
        }

        protected override void Increase(object parameter)
        {
            var value = this.AddIncrement();
            var text = value.ToString(this.StringFormat, this.Culture);

            var textBox = parameter as TextBox;
            SetTextUndoable(textBox ?? this, text);
        }

        protected override bool CanDecrease(object parameter)
        {
            if (this.IsReadOnly || !this.IsEnabled)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(this.Text) || !this.CanParse(this.Text))
            {
                return false;
            }

            if (Comparer<T>.Default.Compare(this.CurrentValue.Value, this.MinLimit) <= 0)
            {
                return false;
            }

            return true;
        }

        protected override void Decrease(object parameter)
        {
            var value = this.SubtractIncrement();
            var text = value.ToString(this.StringFormat, this.Culture);

            var textBox = parameter as TextBox;
            SetTextUndoable(textBox ?? this, text);
        }

        private static void SetTextUndoable(TextBox textBox, string text)
        {
            // http://stackoverflow.com/questions/27083236/change-the-text-in-a-textbox-with-text-binding-sometext-so-it-is-undoable/27083548?noredirect=1#comment42677255_27083548
            // Dunno if nice, testing it for now
            textBox.SelectAll();
            textBox.SelectedText = text;
            textBox.Select(0, 0);
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == IsReadOnlyProperty)
            {
                this.CheckSpinners();
            }

            base.OnPropertyChanged(e);
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            this.CheckSpinners();
            base.OnTextChanged(e);
        }

        private static void OnValidationError(object sender, ValidationErrorEventArgs e)
        {
            var box = (NumericBox<T>)sender;
            var bindingExpression = BindingOperations.GetBindingExpression(box, TextBindableProperty);
            if (bindingExpression != null)
            {
                box.IsUpdatingValue = true;
                bindingExpression.UpdateTarget(); // Reset Value to value from from vm binding.
                box.IsUpdatingValue = false;
            }
        }

        private static void OnFormatDirty(object sender, RoutedEventArgs e)
        {
            var box = (NumericBox<T>)sender;
            if (box.IsFocused || box.IsKeyboardFocusWithin)
            {
                return;
            }

            box.UpdateFormat();
        }

        private static void OnValidationDirty(object sender, RoutedEventArgs e)
        {
            var box = (NumericBox<T>)sender;
            box.UpdateValidation();
        }

        private T AddIncrement()
        {
            var min = this.MaxLimit.CompareTo(TypeMax) < 0
                ? this.MaxLimit
                : TypeMax;
            var incremented = this.subtract(min, this.Increment);
            var currentValue = this.CurrentValue.Value;
            return currentValue.CompareTo(incremented) < 0
                            ? this.add(currentValue, this.Increment)
                            : min;
        }

        private T SubtractIncrement()
        {
            var max = this.MinLimit.CompareTo(TypeMin) > 0
                                ? this.MinLimit
                                : TypeMin;
            var incremented = this.add(max, this.Increment);
            var currentValue = this.CurrentValue.Value;
            return currentValue.CompareTo(incremented) > 0
                            ? this.subtract(currentValue, this.Increment)
                            : max;
        }
    }
}