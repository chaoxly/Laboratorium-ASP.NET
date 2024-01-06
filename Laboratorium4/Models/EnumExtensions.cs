using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Laboratorium4.Models
{
    static public class EnumExtensions
    {
       /* public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }*/
        public static string GetDisplayName(this Enum enumValue)
        {
            var member = enumValue.GetType()
                                 .GetMember(enumValue.ToString())
                                 .FirstOrDefault();

            if (member != null)
            {
                var displayAttribute = member.GetCustomAttribute<DisplayAttribute>();
                if (displayAttribute != null)
                {
                    return displayAttribute.GetName();
                }
            }

            return enumValue.ToString();
        }
    }
}
