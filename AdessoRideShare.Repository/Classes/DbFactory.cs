using AdessoRideShare.Db.Context;
using AdessoRideShare.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Repository.Classes
{
    public class DbFactory : Disposable, IDbFactory
    {
        DbContext dbContext;
        protected DbContextOptions<AdessoRideShareContext> _options;
        public DbFactory(DbContextOptions<AdessoRideShareContext> options)
        {
            _options = options;
        }


        public void SaveChanges()
        {
            if (dbContext != null)
                dbContext.SaveChanges();
        }

        public DbContext Init()
        {
            var context = dbContext ?? (dbContext = new AdessoRideShareContext(_options));

            return context;
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
