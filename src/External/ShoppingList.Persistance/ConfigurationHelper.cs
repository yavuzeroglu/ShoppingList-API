using Microsoft.Extensions.Configuration;

namespace ShoppingList.Persistance;

static class ConfigurationHelper
{
    public static string GetConnectionString
    {

        get
        {
            ConfigurationManager configurationManager = new();

            configurationManager.SetBasePath(Path.Combine(
                Directory.GetCurrentDirectory(), "../ShoppingList.WebAPI"));
            configurationManager.AddJsonFile("appsettings.json");



            return configurationManager.GetConnectionString("PostgreSQL");
        }
    }
}