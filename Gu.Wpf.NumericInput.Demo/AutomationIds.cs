﻿namespace Gu.Wpf.NumericInput.Demo
{
    using System.Runtime.CompilerServices;

    public static class AutomationIds
    {
        public static readonly string MainWindow = Create();
        public static readonly string DebugTab = Create();
        public static readonly string DoubleBoxGroupBox = Create();
        public static readonly string InputBox = Create();
        public static readonly string VmValueBox = Create();
        public static readonly string IsReadonlyBox = Create();
        public static readonly string CultureBox = Create();
        public static readonly string SuffixBox = Create();
        public static readonly string DigitsBox = Create();
        public static readonly string MaxBox = Create();
        public static readonly string MinBox = Create();
        public static readonly string AllowSpinnersBox = Create();
        public static readonly string IncrementBox = Create();
        public static readonly string RegexPatternBox = Create();
        public static readonly string FocusTab = Create();
        public static readonly string TextBox1 = Create();
        public static readonly string TextBox2 = Create();
        public static readonly string TextBox3 = Create();
        public static readonly string DoubleBox1 = Create();
        public static readonly string DoubleBox2 = Create();
        public static readonly string DoubleBox3 = Create();

        private static string Create([CallerMemberName] string name = null)
        {
            return name;
        }
    }
}
