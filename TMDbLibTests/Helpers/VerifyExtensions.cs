using VerifyTests;

namespace TMDbLibTests.Helpers
{
    internal static class VerifyExtensions
    {
        public static VerifySettings IgnoreProperty(this VerifySettings settings, string property)
        {
            settings.ScrubLines(x => x.Contains($" {property}:"));
            return settings;
        }
    }
}