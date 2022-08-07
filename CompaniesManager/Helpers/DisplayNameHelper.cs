using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CompaniesManager.Helpers
{
    public static class DisplayNameHelper
    {
        public static string GetDisplayName(object obj, string propertyName)
        {
            return obj == null ? null : GetDisplayName(obj.GetType(), propertyName);
        }

        public static string GetDisplayName(Type type)
        {
            var attributes = type.GetCustomAttributes(typeof(DisplayNameAttribute), true);

            return attributes.Length == 0
                ? null
                : (attributes[0] as DisplayNameAttribute)?.DisplayName;
        }

        public static string GetDisplayName(Type type, string propertyName)
        {
            var property = type.GetProperty(propertyName);

            return property == null ? null : GetDisplayName(property);
        }

        public static string GetDisplayName(PropertyInfo property)
        {
            var attrName = GetAttributeDisplayName(property);

            if (!string.IsNullOrEmpty(attrName))
            {
                return attrName;
            }

            var metaName = GetMetaDisplayName(property);

            return !string.IsNullOrEmpty(metaName) ? metaName : property.Name;
        }

        private static string GetAttributeDisplayName(PropertyInfo property)
        {
            var attributes = property.GetCustomAttributes(typeof(DisplayNameAttribute), true);

            return attributes.Length == 0
                ? null
                : (attributes[0] as DisplayNameAttribute)?.DisplayName;
        }

        private static string GetMetaDisplayName(PropertyInfo property)
        {
            if (property.DeclaringType != null)
            {
                var attributes = property.DeclaringType.GetCustomAttributes(typeof(MetadataTypeAttribute), true);
                if (attributes.Length == 0)
                {
                    return null;
                }

                if (attributes[0] is MetadataTypeAttribute metaAttr)
                {
                    var metaProperty = metaAttr.MetadataClassType.GetProperty(property.Name);
                    return metaProperty == null
                        ? null
                        : GetAttributeDisplayName(metaProperty);
                }
            }

            return null;
        }
    }
}
