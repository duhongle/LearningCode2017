using Microsoft.Extensions.Configuration;

namespace DotNetCoreClassLibrary
{
    public class ConfigurationManager
    {
        private static IConfigurationRoot config = null;
        static ConfigurationManager()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("app.json");
            config = builder.Build();
        }

        public static IConfigurationRoot AppSettings { get { return config; } }

        public static string Get(string key)
        {
            return config[key];
        }
    }
}
