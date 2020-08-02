namespace System.Reflection
{
    public static class ReflectionExtensions
    {
        public static FieldInfo GetBackingField(this Type type, string propertyName)
        {
            var backingFieldName = string.Format("<{0}>k__BackingField", propertyName);
            return type.GetField(backingFieldName, BindingFlags.Instance | BindingFlags.NonPublic);
        }
    }
}