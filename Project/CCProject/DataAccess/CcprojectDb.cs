using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class CcprojectDb : DbContext
    {
        public CcprojectDb(DbContextOptions<CcprojectDb> options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<GeolocationEntity> Geolocation { get; set; }
    }
}
