using System.Reflection;

namespace PathDrift
{
    public static class ResourceHelper
    {
        public static Stream GetResourceAsStream(this string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream(resourceName);
            return stream == null ? throw new Exception($"Cannot find Embedded Resource : '{resourceName}'") : stream;
        }
    }
}
