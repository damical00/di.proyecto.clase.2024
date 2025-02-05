using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace di.proyecto.clase._2024.Backend.Utiles
{
    public partial class PropertyCopier<T> where T : class, new()
    {    
        public static void CopyProperties(T source,  T target)
        {
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo property in properties) 
            { 
                if (property.CanWrite && property.CanWrite)
                {
                    object value = property.GetValue(source);
                    property.SetValue(target, value);
                }
            }

        }

    }
}
