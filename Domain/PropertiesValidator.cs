using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Domain
{
    public static class PropertiesValidator
    {
        public static void Validate<T>(this T value)
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
        }
    }
}