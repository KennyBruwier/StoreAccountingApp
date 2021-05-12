using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.DBModels
{
    public class _DBStoreAccountingContext : DbContext
    {
        public _DBStoreAccountingContext() : base("name=StoreAccountingAppDBConnectionString")
        {
            Database.SetInitializer<_DBStoreAccountingContext>(new DropCreateDatabaseAlways<_DBStoreAccountingContext>());
            this.Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
