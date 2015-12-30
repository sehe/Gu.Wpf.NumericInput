namespace Gu.Wpf.NumericInput.Demo
{
    using System;

    public static class DefaultValue<TBox>
        where TBox : BaseBox
    {
        private static readonly TBox box = Activator.CreateInstance<TBox>();
        internal static T For<T>(Func<TBox, T> property)
        {
            return property(box);
        }

        /*
        internal static T DefaultValue<TBox, T>(Expression<Func<TBox, T>> property)
            where TBox : BaseBox
        {
            var name = ((MemberExpression)property.Body).Member.Name;
            var boxType = typeof(TBox);
            var field = boxType.GetField($"{name}Property", BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Static);
            var dp = (DependencyProperty)field.GetValue(null);
            var metadata = dp.GetMetadata(boxType); // <- this does not work for some reason
            return (T)metadata.DefaultValue;
        }

        */
    }
}
