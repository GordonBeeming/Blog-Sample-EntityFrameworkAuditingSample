using System;
using System.Reflection;

namespace EntityFrameworkAuditingSample
{
    public static class ReflectionHelper
    {
        public static void CopyLikeProperties(object source, object target)
        {
            if (source == null || target == null)
            {
                return;
            }
            var sourceGetProperties = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);
            var targetSetProperties = target.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);
            foreach (var sourceGetProperty in sourceGetProperties)
            {
                foreach (var targetSetProperty in targetSetProperties)
                {
                    if (sourceGetProperty.Name == targetSetProperty.Name &&
                        sourceGetProperty.PropertyType == targetSetProperty.PropertyType &&
                        sourceGetProperty.CanRead && targetSetProperty.CanWrite)
                    {
                        try
                        {
                            targetSetProperty.SetValue(target, sourceGetProperty.GetValue(source));
                            break;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"Failed to copy source '{sourceGetProperty.Name}' of type '{sourceGetProperty.PropertyType}' to target '{targetSetProperty.Name}' of type '{targetSetProperty.PropertyType}'.", ex);
                        }
                    }
                }
            }
        }
    }
}