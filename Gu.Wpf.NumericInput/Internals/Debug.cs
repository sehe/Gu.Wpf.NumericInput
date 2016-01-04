namespace Gu.Wpf.NumericInput
{
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal static class Debug
    {
        internal static void WriteLine(string message, [CallerMemberName] string caller = null)
        {
            System.Diagnostics.Debug.WriteLine($"{caller}: {message}");
        }

        internal static void WriteLine(DependencyPropertyChangedEventArgs args, [CallerMemberName] string caller = null)
        {
            System.Diagnostics.Debug.WriteLine($"{caller}: From: {args.OldValue.Formatted()} To: {args.NewValue.Formatted()}");
        }

        private static string Formatted(this object o)
        {
            if (o == null)
            {
                return "null";
            }

            var text = o as string;
            if (text != null)
            {
                if (string.IsNullOrWhiteSpace(text))
                {
                    return "string.Empty";
                }

                return $"\"{text}\"";
            }

            return o.ToString();
        }
    }
}
