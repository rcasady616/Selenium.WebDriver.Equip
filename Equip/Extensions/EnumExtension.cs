using System.ComponentModel;

namespace System
{
    public static class EnumExtension
    {
        /// <summary>
        /// Gets the <see cref="Description"/> attribute of an <see cref="enum"/> 
        /// </summary>
        /// <returns>The <see cref="string"/> description of the <see cref="enum"/></returns>
        public static string GetDescription(this Enum @enum)
        {
            var type = @enum.GetType();
            var memberInfo = type.GetMember(@enum.ToString());
            if (memberInfo.Length > 0)
            {
                var attr = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attr.Length > 0)
                    return ((DescriptionAttribute)attr[0]).Description;
            }
            return @enum.ToString();
        }

        public static Enum FromString(this Enum @enum, string value)
        {
            return (Enum)Enum.Parse(@enum.GetType(), value);
        }
    }
}
