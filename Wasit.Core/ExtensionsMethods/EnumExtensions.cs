using System.ComponentModel;

namespace Wasit.Core.ExtensionsMethods
{
    public static partial class ExtensionMethods
    {
        public static string GetDescription(this Enum enumType)
        {
            var fieldInfo = enumType.GetType().GetField(enumType.ToString());

            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumType.ToString();
        }

        public static string GetName(this Enum enumType)
        {
            return enumType.ToString().SplitPascal();
        }

        public static string GetEnumTextById<TEnum>(this int id) where TEnum : Enum
        {
            foreach (var item in Enum.GetValues(typeof(TEnum)))
            {
                if ((int)item == id) return item.ToString();
            }
            return string.Empty;
        }
    }
}
