using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiOpenpayReferencia.Helpers
{
    public static class AppSettings
    {
        public static string GetValorOpenpay(string section)
        {
            var configuation = GetConfiguration();
            string con = configuation.GetSection("Openpay").GetSection(section).Value.ToString();
            return con;
        }
        private static string stCadenaBasicAuth(string seccion)
        {
            var configuation = GetConfiguration();
            string con = configuation.GetSection("AppSettings").GetSection(seccion).Value.ToString();
            return con;
        }
        private static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
    }
}
