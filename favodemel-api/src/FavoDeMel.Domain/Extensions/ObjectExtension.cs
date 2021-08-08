using System;
using System.ComponentModel.DataAnnotations;

namespace FavoDeMel.Domain.Extensions
{
    public static class EntityExtension
    {
        public static T GetAttribute<T>(this object instance, string propertyName) where T : Attribute
        {
            if (instance == null)
            {
                return default;
            }

            var property = instance.GetType().GetProperty(propertyName);

            if (property == null)
            {
                return null;
            }

            object[] attributes = property.GetCustomAttributes(typeof(T), false);
            return attributes.Length > 0 ? (T)attributes[0] : null;
        }

        public static string GetDisplayName<T>(this T entity, string propertyName)
        {
            DisplayAttribute attr = GetAttribute<DisplayAttribute>(entity, propertyName);

            if (attr != null)
            {
                return attr.Name;
            }

            return propertyName;
        }
    }
}
