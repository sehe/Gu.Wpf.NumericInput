namespace Gu.Wpf.NumericInput.Validation
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using System.Windows.Controls;
    using System.Windows.Data;

    internal class RegexRule : ValidationRule
    {
        public static readonly RegexRule Default = new RegexRule();

        private RegexRule()
            : base(ValidationStep.RawProposedValue, false)
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo, BindingExpressionBase owner)
        {
            var box = (TextBox)((Binding)owner.ParentBindingBase).Source;
            var text = box.Text;
            var pattern = (string)box.GetValue(BaseBox.RegexPatternProperty);
            if (pattern == null)
            {
                return ValidationResult.ValidResult;
            }

            if (text == null)
            {
                return new RegexValidationResult(null, pattern, false, $"'null' does not match pattern '{pattern}'");
            }

            try
            {
                if (Regex.IsMatch(text, pattern))
                {
                    return ValidationResult.ValidResult;
                }

                return new RegexValidationResult(null, pattern, false, $"'{text}' does not match pattern '{pattern}'");
            }
            catch (Exception e)
            {
                return new RegexValidationResult(null, pattern, false, $"'{text}' does not match pattern '{pattern}'. Threw exception: {e.Message}");
            }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            throw new NotImplementedException("Should not get here");
        }
    }
}
