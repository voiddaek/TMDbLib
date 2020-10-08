using System;
using System.Linq.Expressions;
using System.Reflection;
using Newtonsoft.Json;
using VerifyTests;

namespace TMDbLibTests.Helpers
{
    internal static class VerifyExtensions
    {
        public static VerifySettings IgnoreProperty(this VerifySettings settings, params string[] properties)
        {
            foreach (string property in properties)
                settings.ScrubLines(x => x.Contains($" {property}:"));

            return settings;
        }

        public static VerifySettings IgnoreProperty<T>(this VerifySettings settings, params Expression<Func<T, object>>[] properties)
        {
            foreach (Expression<Func<T, object>> expression in properties)
            {
                PropertyInfo prop = PropertyHelpers.GetPropertyInfo(expression);

                JsonPropertyAttribute jsonPropAttribute = prop.GetCustomAttribute<JsonPropertyAttribute>();
                if (jsonPropAttribute != null && !string.IsNullOrEmpty(jsonPropAttribute.PropertyName))
                    settings.ScrubLines(x => x.Contains($" {jsonPropAttribute.PropertyName}:"));
                else
                    settings.ScrubLines(x => x.Contains($" {prop.Name}:"));
            }
            return settings;
        }
    }
}