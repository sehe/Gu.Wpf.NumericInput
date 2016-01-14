namespace Gu.Wpf.NumericInput.UITests
{
    using Gu.Wpf.NumericInput.Demo;
    using NUnit.Framework;
    using TestStack.White;
    using TestStack.White.Factory;
    using TestStack.White.UIItems;
    using TestStack.White.UIItems.TabItems;
    using TestStack.White.UIItems.WPFUIItems;
    using TestStack.White.WindowsAPI;

    public partial class DoubleBoxTests
    {
        public class Focus
        {
            [Test]
            public void NoSpinnersNoSuffix()
            {
                using (var app = Application.AttachOrLaunch(Info.ProcessStartInfo))
                {
                    var window = app.GetWindow(AutomationIds.MainWindow, InitializeOption.NoCache);
                    var page = window.Get<TabPage>(AutomationIds.FocusTab);
                    page.Select();
                    var doubleBox1 = page.Get<TextBox>(AutomationIds.DoubleBox1);
                    var doubleBox2 = page.Get<TextBox>(AutomationIds.DoubleBox2);
                    var doubleBox3 = page.Get<TextBox>(AutomationIds.DoubleBox3);
                    doubleBox1.Click();
                    Assert.AreEqual(true, doubleBox1.IsFocussed);
                    Assert.AreEqual(false, doubleBox2.IsFocussed);
                    Assert.AreEqual(false, doubleBox3.IsFocussed);
                    doubleBox1.Enter("2");
                    Assert.AreEqual("1.234", page.Get<TextBox>(AutomationIds.TextBox1).Text);

                    window.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.TAB);
                    Assert.AreEqual("2", page.Get<TextBox>(AutomationIds.TextBox1).Text);
                    Assert.AreEqual(false, doubleBox1.IsFocussed);
                    Assert.AreEqual(true, doubleBox2.IsFocussed);
                    Assert.AreEqual(false, doubleBox3.IsFocussed);

                    window.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.TAB);
                    Assert.AreEqual(false, doubleBox1.IsFocussed);
                    Assert.AreEqual(false, doubleBox2.IsFocussed);
                    Assert.AreEqual(true, doubleBox3.IsFocussed);

                    window.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.TAB);
                    Assert.AreEqual(true, doubleBox1.IsFocussed);
                    Assert.AreEqual(false, doubleBox2.IsFocussed);
                    Assert.AreEqual(false, doubleBox3.IsFocussed);
                }
            }

            [Test]
            public void WithSpinners()
            {
                using (var app = Application.AttachOrLaunch(Info.ProcessStartInfo))
                {
                    var window = app.GetWindow(AutomationIds.MainWindow, InitializeOption.NoCache);
                    var page = window.Get<TabPage>(AutomationIds.FocusTab);
                    page.Select();
                    var doubleBox1 = page.Get<TextBox>(AutomationIds.DoubleBox1);
                    var doubleBox2 = page.Get<TextBox>(AutomationIds.DoubleBox2);
                    var doubleBox3 = page.Get<TextBox>(AutomationIds.DoubleBox3);
                    page.Get<CheckBox>(AutomationIds.AllowSpinnersBox).Checked = true;
                    doubleBox1.Click();
                    Assert.AreEqual(true, doubleBox1.IsFocussed);
                    Assert.AreEqual(false, doubleBox2.IsFocussed);
                    Assert.AreEqual(false, doubleBox3.IsFocussed);
                    doubleBox1.Enter("2");
                    Assert.AreEqual("1.234", page.Get<TextBox>(AutomationIds.TextBox1).Text);

                    window.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.TAB);
                    Assert.AreEqual("2", page.Get<TextBox>(AutomationIds.TextBox1).Text);
                    Assert.AreEqual(false, doubleBox1.IsFocussed);
                    Assert.AreEqual(true, doubleBox2.IsFocussed);
                    Assert.AreEqual(false, doubleBox3.IsFocussed);
                    doubleBox2.Get<Button>(BaseBox.IncreaseButtonName).Click();
                    Assert.AreEqual("1.345", doubleBox1.Text);
                    Assert.AreEqual("2.345", page.Get<TextBox>(AutomationIds.TextBox2).Text);

                    window.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.TAB);
                    Assert.AreEqual("1.345", doubleBox1.Text);
                    Assert.AreEqual("1.345", page.Get<TextBox>(AutomationIds.TextBox2).Text);
                    Assert.AreEqual(false, doubleBox1.IsFocussed);
                    Assert.AreEqual(false, doubleBox2.IsFocussed);
                    Assert.AreEqual(true, doubleBox3.IsFocussed);

                    window.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.TAB);
                    Assert.AreEqual(true, doubleBox1.IsFocussed);
                    Assert.AreEqual(false, doubleBox2.IsFocussed);
                    Assert.AreEqual(false, doubleBox3.IsFocussed);
                }
            }
        }
    }
}
