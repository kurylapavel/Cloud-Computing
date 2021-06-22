using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataAccess
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CcprojectDb>
    {

        public CcprojectDb CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration =
                new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();

            var builder = new DbContextOptionsBuilder<CcprojectDb>();

            var connectionString = configuration.GetConnectionString("CcprojectDb");

            builder.UseSqlServer(connectionString);

            return new CcprojectDb(builder.Options);
        }
    }
}
