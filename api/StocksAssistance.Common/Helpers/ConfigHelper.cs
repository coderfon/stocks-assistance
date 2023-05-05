using Microsoft.Extensions.Configuration;

namespace StocksAssistance.Common.Helpers
{
    public static class ConfigHelper
    {
        public static IConfiguration GetConfig()
        {
            // Make sure the appsettings.json file is copied to the output directory

            var builder = new ConfigurationBuilder().SetBasePath(System.AppContext.BaseDirectory).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            return builder.Build();
        }
    }
}
