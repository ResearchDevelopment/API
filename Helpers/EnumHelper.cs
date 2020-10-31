using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel;
using System.Reflection;

namespace ShadiWebApplication.Helpers
{
    public static class EnumHelper
    {
        public static string GetDescription<TEnum>(TEnum enumValue) where TEnum : Enum
        {
            try
            {
                var descriptionAttribute =
                    enumValue.GetType()
                             .GetMember(enumValue.ToString())[0]
                             .GetCustomAttribute<DescriptionAttribute>();

                return descriptionAttribute.Description;

            }
            catch
            {
                return "Invalid enum description";
            }
        }
    }
}
