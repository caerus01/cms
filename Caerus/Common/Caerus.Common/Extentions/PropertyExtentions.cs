using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Extentions
{
    public static class PropertyExtentions
    {
        public static void SafeSetProperty(PropertyInfo target, object destination, dynamic value)
        {
            //fix SQL datetime min here
            if ((target == typeof(DateTime) || Nullable.GetUnderlyingType(target.PropertyType) == typeof(DateTime)) && (value < new DateTime(1900, 01, 01) || value == null))
                value = new DateTime(1900, 01, 01);

            if (Nullable.GetUnderlyingType(target.PropertyType) == null && value == null)
                return;
            if (target.PropertyType == typeof(int) || Nullable.GetUnderlyingType(target.PropertyType) == typeof(int))
            {
                target.SetValue(destination, (int) value);
                return;
            }
            if (!target.PropertyType.IsInstanceOfType(value) && !Nullable.GetUnderlyingType(target.PropertyType).IsInstanceOfType(value))
                return;
            

            target.SetValue(destination, value);
        }
    }
}
