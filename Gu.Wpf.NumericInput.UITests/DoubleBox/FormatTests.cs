namespace Gu.Wpf.NumericInput.UITests.DoubleBox
{
    using System.Globalization;
    using NUnit.Framework;

    public class FormatTests : DoubleBoxTestsBase
    {
        private static readonly CultureInfo EnUs = CultureInfo.GetCultureInfo("en-US");
        private static readonly CultureInfo SvSe = CultureInfo.GetCultureInfo("sv-SE");

        public static readonly FormatData[] Source =
            {
                new FormatData("1", "F1",EnUs, "1.0","1"),
                new FormatData("1", "F1",SvSe, "1.0","1"),
                //new FormatData(" 1", "1"),
                //new FormatData("1 ", "1"),
                //new FormatData(" 1 ", "1"),
                //new FormatData("1.2", "1.2"),
                //new FormatData("-1.2", "-1.2"),
                //new FormatData("+1.2", "1.2"),
                //new FormatData(".1", "0.1"),
                //new FormatData("-.1", "-0.1"),
                //new FormatData("0.1", "0.1"),
                //new FormatData("1e1", "10"),
                //new FormatData("1e0", "1"),
                //new FormatData("1e-1", "0.1"),
                //new FormatData("1E1", "10"),
                //new FormatData("1E0", "1"),
                //new FormatData("1E-1", "0.1"),
                //new FormatData("-1e1", "-10"),
                //new FormatData("-1e0", "-1"),
                //new FormatData("-1e-1", "-0.1"),
                //new FormatData("-1E1", "-10"),
                //new FormatData("-1E0", "-1"),
                //new FormatData("-1E-1", "-0.1"),
            };

        public static readonly FormatData[] SwedishSource =
            {
                //new FormatData("1", "1"),
                //new FormatData(" 1", "1"),
                //new FormatData("1 ", "1"),
                //new FormatData(" 1 ", "1"),
                //new FormatData("1,2", "1.2"),
                //new FormatData("-1,2", "-1.2"),
                //new FormatData("+1,2", "1.2"),
                //new FormatData(",1", "0.1"),
                //new FormatData("-,1", "-0.1"),
                //new FormatData("0,1", "0.1"),
                //new FormatData("1e1", "10"),
                //new FormatData("1e0", "1"),
                //new FormatData("1e-1", "0.1"),
                //new FormatData("1E1", "10"),
                //new FormatData("1E0", "1"),
                //new FormatData("1E-1", "0.1"),
                //new FormatData("-1e1", "-10"),
                //new FormatData("-1e0", "-1"),
                //new FormatData("-1e-1", "-0.1"),
                //new FormatData("-1E1", "-10"),
                //new FormatData("-1E0", "-1"),
                //new FormatData("-1E-1", "-0.1"),
            };

        [SetUp]
        public void SetUp()
        {
            this.ViewModelValueBox.Text = "0";
            this.CultureBox.Select("en-US");
            this.StringFormatBox.Text = "";
            this.CanValueBeNullBox.Checked = false;

            this.AllowLeadingWhiteBox.Checked = true;
            this.AllowTrailingWhiteBox.Checked = true;
            this.AllowLeadingSignBox.Checked = true;
            this.AllowDecimalPointBox.Checked = true;
            this.AllowThousandsBox.Checked = false;
            this.AllowExponentBox.Checked = true;

            this.MinBox.Text = "";
            this.MaxBox.Text = "";

            this.LoseFocusButton.Click();
        }

        [TestCaseSource(nameof(Source))]
        public void WithStringFormat(FormatData formatData)
        {
            var boxes = this.LostFocusValidateOnLostFocusBoxes;
            var doubleBox = boxes.DoubleBox;
            this.StringFormatBox.Text = formatData.Text;
            this.CultureBox.Select(formatData.Culture.Name);

            doubleBox.Text = formatData.Text;

            Assert.AreEqual(false, doubleBox.HasValidationError());
            Assert.AreEqual(formatData.Text, doubleBox.Text);
            Assert.AreEqual(formatData.Formatted, doubleBox.FormattedView().Text);
            Assert.AreEqual("0", this.ViewModelValueBox.Text);
            Assert.AreEqual(TextSource.UserInput, doubleBox.TextSource());

            this.LoseFocusButton.Click();
            Assert.AreEqual(false, doubleBox.HasValidationError());
            Assert.AreEqual(formatData.Text, doubleBox.Text);
            Assert.AreEqual(formatData.Formatted, doubleBox.FormattedView().Text);
            Assert.AreEqual(formatData.ViewModel, this.ViewModelValueBox.Text);
            Assert.AreEqual(TextSource.UserInput, doubleBox.TextSource());
        }

        public class FormatData
        {
            public readonly string Text;
            public readonly string StringFormat;
            public readonly CultureInfo Culture;
            public readonly string Formatted;
            public readonly string ViewModel;

            public FormatData(string text, string stringFormat, CultureInfo culture, string formatted, string viewModel)
            {
                this.Text = text;
                this.StringFormat = stringFormat;
                this.Culture = culture;
                this.Formatted = formatted;
                this.ViewModel = viewModel;
            }

            public override string ToString() => $"Text: {this.Text}, Expected: {this.Formatted}";
        }
    }
}