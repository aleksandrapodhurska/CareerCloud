using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public static class Connection
    {
        private static readonly IConfiguration _config;

        static Connection()
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            _config = config.Build();

        }

        public static string GetConnectionString()
        {
            return _config.GetConnectionString("DataConnection");
        }
    }
}
