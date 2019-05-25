using System.Configuration;

namespace French.Tools.Extensions
{
    public static class AppSettings
    {
        public static string Get(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        public static bool GetBoolean(string key)
        {
            var valor = ConfigurationManager.AppSettings[key];
            if (valor.IsNullOrEmptyOrWhiteSpace())
            {
                return false;
            }
            if (valor.ToLower() == "true")
            {
                return true;
            }
            return false;
        }
        public static int GetInt(string key)
        {

            var valor = ConfigurationManager.AppSettings[key];

            int.TryParse(valor, out var retorno);

            return retorno;

        }
        public static byte GetByte(string key)
        {

            var valor = ConfigurationManager.AppSettings[key];

            byte.TryParse(valor, out var retorno);

            return retorno;

        }        public static string GetConnectionString(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }
    }
}