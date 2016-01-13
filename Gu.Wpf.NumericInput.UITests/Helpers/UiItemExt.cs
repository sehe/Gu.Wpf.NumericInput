namespace Gu.Wpf.NumericInput.UITests
{
    using System;
    using Gu.Wpf.NumericInput.Demo;
    using TestStack.White.UIItems;
    using TestStack.White.UIItems.Custom;
    using TestStack.White.UIItems.WPFUIItems;

    public static class UiItemExt
    {
        public static string ItemStatus(this IUIItem item)
        {
            return (string)item.AutomationElement.Current.ItemStatus;
        }

        public static bool HasValidationError(this UIItem item)
        {
            var itemStatus = item.ItemStatus();
            if (itemStatus == "HasValidationError: True")
            {
                return true;
            }

            if (itemStatus == "HasValidationError: False")
            {
                return false;
            }

            throw new InvalidOperationException();
        }

        internal static TextSource TextSource(this GroupBox groupBox)
        {
            TextSource result;
            var text = groupBox.Get<Label>(AutomationIds.TextSourceBlock).Text;
            if (!Enum.TryParse(text, out result))
            {
                throw new ArgumentException();
            }

            return result;
        }

        internal static Status Status(this GroupBox groupBox)
        {
            Status result;
            var text = groupBox.Get<Label>(AutomationIds.StatusBlock).Text;
            if (!Enum.TryParse(text, out result))
            {
                throw new ArgumentException();
            }

            return result;
        }

        internal static string EditText(this TextBox textBox)
        {
            var groupBox = textBox.GetParent<CustomUIItem>().GetParent<GroupBox>();
            if (groupBox.Get<CheckBox>(AutomationIds.AllowSpinnersBox).Checked ||
                !string.IsNullOrEmpty(groupBox.Get<TextBox>(AutomationIds.SuffixBox).Text))
            {
                return textBox.Get<TextBox>(BaseBox.EditBoxName).Text;
            }

            return textBox.Text;
        }

        internal static string FormattedText(this TextBox textBox)
        {
            return textBox.Get<Label>(BaseBox.FormattedName).Text;
        }
    }
}
