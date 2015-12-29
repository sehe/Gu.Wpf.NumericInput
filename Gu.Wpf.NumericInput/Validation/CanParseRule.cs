namespace Gu.Wpf.NumericInput.Validation
{
    using System;
    using System.Globalization;
    using System.Windows.Controls;
    using System.Windows.Data;

    internal class CanParseRule<T> : ValidationRule
        where T : struct, IComparable<T>, IFormattable, IConvertible, IEquatable<T>
    {
        public static readonly CanParseRule<T> Default = new CanParseRule<T>();

        private CanParseRule()
            : base(ValidationStep.RawProposedValue, false)
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo, BindingExpressionBase owner)
        {
            var box = (NumericBox<T>)((Binding)owner.ParentBindingBase).Source;
            var text = box.Text;
            if (string.IsNullOrWhiteSpace(text))
            {
                if (box.CanValueBeNull)
                {
                    return ValidationResult.ValidResult;
                }

                box.ValueProxy = box.Value;
                var formatted = text == null ? "null" : "string.empty";
                return new CanParseValidationResult(typeof(T), text, false, $"Cannot parse '{formatted}' to a {typeof(T).Name}");
            }

            if (box.CanParse(text))
            {
                return ValidationResult.ValidResult;
            }

            return new CanParseValidationResult(typeof(T), text, false, $"Cannot parse '{text}' to a {typeof(T).Name}");
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            throw new NotImplementedException("Should not get here");
        }
    }
}
