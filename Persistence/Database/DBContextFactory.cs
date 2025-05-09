using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Database
{
    public static class DBContextManager
    {
        private static readonly object _lock = new();
        private static AppDbContext? _instance;

        public static AppDbContext GetContext()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        var configuration = new ConfigurationBuilder()
                            .AddJsonFile("DbSettings.json")
                            .Build();

                        var options = new DbContextOptionsBuilder<AppDbContext>()
                            .UseSqlite(configuration.GetConnectionString("DefaultConnection"))
                            .Options;

                        _instance = new AppDbContext(options);
                        _instance.Database.EnsureCreated();
                    }
                }
            }

            return _instance;
        }
    }
}
