namespace System
{
    public class ReadonlyViolationException : Exception
    {
        public string PropertyName {get;}

        public ReadonlyViolationException(string propertyName, Exception? innerException = null)
            : base($"Unable to set readonly property or field: {propertyName}", innerException)
        {
            PropertyName = propertyName;
        }
    }
}
