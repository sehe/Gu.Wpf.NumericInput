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
        public class Spinners
        {
            private static string ValueBoxId = "ValueBox";
            private static string IncreaseButtonId = "IncreaseButton";
            private static string DecreaseButtonId = "DecreaseButton";
            [Test]
            public void UpdatesViewModel()
            {
                using (var app = Application.AttachOrLaunch(Info.ProcessStartInfo))
                {
                    var window = app.GetWindow(AutomationIds.MainWindow, InitializeOption.NoCache);
                    var page = window.Get<TabPage>(AutomationIds.DebugTab);
                    page.Select();
                    var groupBox = window.Get<GroupBox>(AutomationIds.DoubleBoxGroupBox);
                    groupBox.Get<CheckBox>(AutomationIds.AllowSpinnersBox).Checked = true;
                    var inputBox = groupBox.Get<TextBox>(ValueBoxId);
                    var increaseButton = groupBox.Get<Button>(IncreaseButtonId);
                    var decreaseButton = groupBox.Get<Button>(DecreaseButtonId);
                    var vmValueBox = groupBox.Get<TextBox>(AutomationIds.VmValueBox);
                    Assert.AreEqual("0", inputBox.Text);
                    Assert.AreEqual("0", vmValueBox.Text);
                    Assert.AreEqual("0", groupBox.Get<Label>(AutomationIds.ValueBlock).Text);
                    Assert.AreEqual(Gu.Wpf.NumericInput.TextSource.ValueBinding.ToString(), groupBox.Get<Label>(AutomationIds.TextSourceBlock).Text);
                    Assert.AreEqual(Gu.Wpf.NumericInput.Status.Idle.ToString(), groupBox.Get<Label>(AutomationIds.StatusBlock).Text);

                    increaseButton.Click();
                    //vmValueBox.Click();
                    Assert.AreEqual("1", inputBox.Text);
                    Assert.AreEqual(false, inputBox.HasValidationError());
                    Assert.AreEqual("1", vmValueBox.Text);
                    Assert.AreEqual("1", groupBox.Get<Label>(AutomationIds.ValueBlock).Text);
                    Assert.AreEqual(Gu.Wpf.NumericInput.TextSource.UserInput.ToString(), groupBox.Get<Label>(AutomationIds.TextSourceBlock).Text);
                    Assert.AreEqual(Gu.Wpf.NumericInput.Status.Idle.ToString(), groupBox.Get<Label>(AutomationIds.StatusBlock).Text);

                    groupBox.Get<TextBox>(AutomationIds.IncrementBox).Enter("5");

                    increaseButton.Click();
                    //vmValueBox.Click();
                    Assert.AreEqual("6", inputBox.Text);
                    Assert.AreEqual(false, inputBox.HasValidationError());
                    Assert.AreEqual("6", vmValueBox.Text);
                    Assert.AreEqual("6", groupBox.Get<Label>(AutomationIds.ValueBlock).Text);
                    Assert.AreEqual(Gu.Wpf.NumericInput.TextSource.UserInput.ToString(), groupBox.Get<Label>(AutomationIds.TextSourceBlock).Text);
                    Assert.AreEqual(Gu.Wpf.NumericInput.Status.Idle.ToString(), groupBox.Get<Label>(AutomationIds.StatusBlock).Text);

                    decreaseButton.Click();
                    //vmValueBox.Click();
                    Assert.AreEqual("1", inputBox.Text);
                    Assert.AreEqual(false, inputBox.HasValidationError());
                    Assert.AreEqual("1", vmValueBox.Text);
                    Assert.AreEqual("1", groupBox.Get<Label>(AutomationIds.ValueBlock).Text);
                    Assert.AreEqual(Gu.Wpf.NumericInput.TextSource.UserInput.ToString(), groupBox.Get<Label>(AutomationIds.TextSourceBlock).Text);
                    Assert.AreEqual(Gu.Wpf.NumericInput.Status.Idle.ToString(), groupBox.Get<Label>(AutomationIds.StatusBlock).Text);
                }
            }

            [Test]
            public void Undo()
            {
                using (var app = Application.AttachOrLaunch(Info.ProcessStartInfo))
                {
                    var window = app.GetWindow(AutomationIds.MainWindow, InitializeOption.NoCache);
                    var page = window.Get<TabPage>(AutomationIds.DebugTab);
                    page.Select();
                    var groupBox = window.Get<GroupBox>(AutomationIds.DoubleBoxGroupBox);
                    groupBox.Get<CheckBox>(AutomationIds.AllowSpinnersBox).Checked = true;
                    var inputBox = groupBox.Get<TextBox>(ValueBoxId);
                    var increaseButton = groupBox.Get<Button>(IncreaseButtonId);
                    var decreaseButton = groupBox.Get<Button>(DecreaseButtonId);
                    var vmValueBox = groupBox.Get<TextBox>(AutomationIds.VmValueBox);
                    Assert.AreEqual("0", inputBox.Text);
                    var keyboard = window.Keyboard;
                    increaseButton.Click();
                    Assert.AreEqual("1", inputBox.Text);
                    Assert.AreEqual(false, inputBox.HasValidationError());
                    Assert.AreEqual("1", vmValueBox.Text);
                    Assert.AreEqual("1", groupBox.Get<Label>(AutomationIds.ValueBlock).Text);
                    Assert.AreEqual(Gu.Wpf.NumericInput.TextSource.UserInput.ToString(), groupBox.Get<Label>(AutomationIds.TextSourceBlock).Text);
                    Assert.AreEqual(Gu.Wpf.NumericInput.Status.Idle.ToString(), groupBox.Get<Label>(AutomationIds.StatusBlock).Text);

                    keyboard.HoldKey(KeyboardInput.SpecialKeys.CONTROL);
                    keyboard.Enter("z");
                    keyboard.LeaveKey(KeyboardInput.SpecialKeys.CONTROL);
                    Assert.AreEqual("0", inputBox.Text);
                    Assert.AreEqual(false, inputBox.HasValidationError());
                    Assert.AreEqual("1", vmValueBox.Text);
                    Assert.AreEqual("0", groupBox.Get<Label>(AutomationIds.ValueBlock).Text);
                    Assert.AreEqual(Gu.Wpf.NumericInput.TextSource.UserInput.ToString(), groupBox.Get<Label>(AutomationIds.TextSourceBlock).Text);
                    Assert.AreEqual(Gu.Wpf.NumericInput.Status.Idle.ToString(), groupBox.Get<Label>(AutomationIds.StatusBlock).Text);
                }
            }

        }
    }
}
