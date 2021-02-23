using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FavoDeMel.Framework.Helpers
{
    public static class EnumHelper
    {
        public static bool IsDefaultValue<TEnum>(TEnum enumVal)
        {
            return enumVal.Equals(default(TEnum));
        }

        public static int GetIndex<TEnum>(TEnum enumVal)
        {
            int i = -1;

            foreach (object item in Enum.GetValues(typeof(TEnum)))
            {
                i++;

                if (EqualityComparer<TEnum>.Default.Equals((TEnum)item, enumVal))
                {
                    break;
                }
            }

            return i;
        }

        public static string GetName<TEnum>(TEnum value)
        {
            return Enum.GetName(typeof(TEnum), value);
        }

        public static string GetDisplayNames<TEnum>(IList<TEnum> enumValues)
        {
            return string.Join(", ", enumValues.Select(c => GetDisplayName(c)));
        }

        public static string GetDisplayName<TEnum>(TEnum enumVal)
        {
            DisplayAttribute attr = GetAttribute<DisplayAttribute>(enumVal);

            if (attr != null)
            {
                return attr.Name;
            }

            return enumVal?.ToString() ?? string.Empty;
        }

        public static string GetDisplayGroup<TEnum>(TEnum enumVal)
        {
            DisplayAttribute attr = GetAttribute<DisplayAttribute>(enumVal);

            if (attr != null)
            {
                return attr.GroupName;
            }

            return enumVal?.ToString() ?? string.Empty;
        }

        public static string GetLabelBitwise<TEnum>(int value, string separatorChar = null)
        {
            List<string> list = new List<string>();

            foreach (object item in Enum.GetValues(typeof(TEnum)))
            {
                int valorInt = (int)(Enum.Parse(typeof(TEnum), item.ToString()));

                if ((valorInt & value) > 0)
                {
                    list.Add(GetDisplayName<TEnum>((TEnum)item));
                }
            }

            return string.Join(separatorChar == null ? " " : separatorChar, list);
        }

        public static string GetDisplayShortName<TEnum>(TEnum enumVal)
        {
            DisplayAttribute attr = GetAttribute<DisplayAttribute>(enumVal);

            if (attr != null)
            {
                return attr.ShortName;
            }

            return enumVal?.ToString() ?? string.Empty;
        }

        public static TEnum GetAttribute<TEnum>(object enumVal) where TEnum : Attribute
        {
            if (enumVal == null)
            {
                return default;
            }

            Type type = enumVal.GetType();
            System.Reflection.MemberInfo[] memInfo = type.GetMember(enumVal.ToString());

            if (memInfo.Length == 0)
            {
                return null;
            }

            object[] attributes = memInfo[0].GetCustomAttributes(typeof(TEnum), false);
            return attributes.Length > 0 ? (TEnum)attributes[0] : null;
        }

        public static TEnum Parse<TEnum>(Enum value)
            where TEnum : struct
        {
            return Parse<TEnum>(value.ToString());
        }

        public static TEnum Parse<TEnum>(string strEnum, bool ignoreCase = true) where TEnum : struct
        {
            return (TEnum)Enum.Parse(typeof(TEnum), strEnum, ignoreCase);
        }

        public static TEnum ParseDisplayName<TEnum>(string displayName) where TEnum : struct
        {
            return GetValues<TEnum>().FirstOrDefault(c => GetDisplayName(c) == displayName);
        }

        public static TEnum ParseStringInt<TEnum>(string enumStringInt)
        {
            return (TEnum)Enum.ToObject(typeof(TEnum), Convert.ToInt32(enumStringInt));
        }

        public static TEnum? ParseNullable<TEnum>(string strEnum) where TEnum : struct
        {
            return Enum.TryParse(strEnum, out TEnum saida) ? saida : (TEnum?)null;
        }

        public static TEnum TryParse<TEnum>(string strEnum, bool ignoreCase = true) where TEnum : struct
        {
            Enum.TryParse(strEnum, ignoreCase, out TEnum saida);
            return saida;
        }

        public static IList<TEnum> GetValues<TEnum>()
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToList();
        }

        public static IList<TEnum> GetValues<TEnum>(Func<TEnum, bool> predicate)
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Where(predicate).ToList();
        }

        public static IList<TEnum> GetBitwise<TEnum>(TEnum value)
        {
            List<TEnum> list = new List<TEnum>();

            foreach (object item in Enum.GetValues(typeof(TEnum)))
            {
                if ((value as Enum).HasFlag((Enum)item))
                {
                    list.Add((TEnum)item);
                }
            }

            return list;
        }

        public static IList<TEnum> GetValuesByGroup<TEnum>(params string[] groups)
        {
            IList<TEnum> values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToList();
            return values.Where(c => groups.Contains(GetDisplayGroup(c))).ToList();
        }
    }
}