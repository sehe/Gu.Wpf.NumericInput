namespace Gu.Wpf.NumericInput.Validation
{
    using System;
    using System.Globalization;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class IsGreaterThanOrEqualToMinRule<T> : ValidationRule
        where T : struct, IComparable<T>, IFormattable, IConvertible, IEquatable<T>
    {
        public static readonly IsGreaterThanOrEqualToMinRule<T> Default = new IsGreaterThanOrEqualToMinRule<T>();

        private IsGreaterThanOrEqualToMinRule()
            : base(ValidationStep.ConvertedProposedValue, false)
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo, BindingExpressionBase owner)
        {
            var box = (NumericBox<T>)((Binding)owner.ParentBindingBase).Source;
            if (box.MinValue == null)
            {
                return ValidationResult.ValidResult;
            }

            var min = box.MinValue.Value;

            if (value == null)
            {
                return new IsLessThanMinValidationResult(null, min, false, $"null < min ({min})");
            }

            var typedValue = (T)value;
            if (typedValue.CompareTo(min) < 0)
            {
                return new IsLessThanMinValidationResult(typedValue, min, false, $"{typedValue} < min ({min})");
            }

            return ValidationResult.ValidResult;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            throw new NotImplementedException("Should not get here");
        }
    }
}