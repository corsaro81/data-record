using System.Reflection;
using Force.DeepCloner;

namespace System
{
    public static class SystemDataRecordExtensions
    {
        private static BindingFlags bindingFlags =
           BindingFlags.Instance |
           BindingFlags.Public |
           BindingFlags.NonPublic;

        public static TDataRecord With<TDataRecord>(this TDataRecord dataRecord, object changes)
        {
            if(dataRecord is null)
            {
                throw new ArgumentNullException(nameof(dataRecord));
            }
            if(changes is null)
            {
                throw new ArgumentNullException(nameof(changes));
            }

            var instance = dataRecord.DeepClone();

            var properties = changes.GetType().GetProperties(bindingFlags);

            foreach(var property in properties)
            {
                var ownProperty = dataRecord.GetType().GetProperty(property.Name);
                
                if(ownProperty != null)
                {
                    var backingField = dataRecord.GetType().GetBackingField(ownProperty.Name);

                    if(backingField == null)
                    {
                        throw new ReadonlyViolationException(ownProperty.Name);
                    }

                    backingField.SetValue(instance, property.GetValue(changes));
                }
            }

            return instance;
        }
    }
}