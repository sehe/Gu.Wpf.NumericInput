namespace Gu.Wpf.NumericInput
{
    using System.Runtime.CompilerServices;

    internal static class Debug
    {
        internal static void WriteLine(string message, [CallerMemberName] string caller = null)
        {
            System.Diagnostics.Debug.WriteLine($"{caller}: {message}");
        }
    }
}
