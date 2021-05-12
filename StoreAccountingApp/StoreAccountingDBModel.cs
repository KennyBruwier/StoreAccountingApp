using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace StoreAccountingApp
{
    public partial class StoreAccountingDBModel : DbContext
    {
        public StoreAccountingDBModel()
            : base("name=StoreAccountingDBModel")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
