using System;
using System.Collections.Generic;
using System.Data.Entity;
using CrudOperation_With_WebApi.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CrudOperation_With_WebApi.Entity
{
    public class UserDbContext:DbContext
    {
        public UserDbContext():base("Dbcon")
        {

        }
        public DbSet<UserMaster> usermaster { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}