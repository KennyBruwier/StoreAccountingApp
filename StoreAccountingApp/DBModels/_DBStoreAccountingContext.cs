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
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<_DBStoreAccountingContext>());
            this.Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //modelBuilder.Entity<ArtistSong>()
            //   .HasRequired(f => f.Song)
            //   .WithRequiredDependent()
            //   .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(a => a.JobFunctions)
                .WithMany(s => s.Employees)
                .Map(cs =>
                {
                    cs.MapLeftKey(nameof(Employee.EmployeeId));
                    cs.MapRightKey(nameof(JobFunction.JobFunctionId));
                    cs.ToTable("EmployeeJobFunctions");
                });
            modelBuilder.Entity<Employee>()
                .HasMany(a => a.Stores)
                .WithMany(s => s.Employees)
                .Map(cs =>
                {
                    cs.MapLeftKey(nameof(Employee.EmployeeId));
                    cs.MapRightKey(nameof(Store.StoreId));
                    cs.ToTable("EmployeeStores");
                });
        }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Client>Clients { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<JobFunction> JobFunctions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleProduct> SaleProducts { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierProduct> SupplierProducts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
