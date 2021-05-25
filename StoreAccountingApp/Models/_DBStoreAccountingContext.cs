using StoreAccountingApp.DTO;
using StoreAccountingApp.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.Models
{
    
    public class _DBStoreAccountingContext : DbContext
    {
        /* When database is still in use and you can't change it, do the following query in msSQL:
         * 
         * alter database YourDb set single_user with rollback immediate

           alter database YourDb set MULTI_USER
         */
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
                .HasMany(a => a.Shops)
                .WithMany(s => s.Employees)
                .Map(cs =>
                {
                    cs.MapLeftKey(nameof(Employee.EmployeeId));
                    cs.MapRightKey(nameof(Shop.ShopId));
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
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierProduct> SupplierProducts { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public void LoadDemoData()
        {
            AccountType[] accountypes = 
            { 
                new AccountType("Admin"), 
                new AccountType("Manager"), 
                new AccountType("Verkoper"),
                new AccountType("Magazijnier")
            };
            Employee[] employees =
            {
                new Employee("Kenny","Bruwier","kenny.bruwier@gmail.com"),
                new Employee("Tom", "Dilen", "tom.dilen@gmail.com"),
                new Employee("Tim", "geenIdee", "tim.geenidee@gmail.com"),
                new Employee("Guy","Bruwier","Guy.bruwier@gmail.com")
            };
            //foreach (Employee employee in employees)
            //{
            //    User newUser = new User($"{employee.Firstname} {employee.Lastname}", "123", employee.EmailAddress);
            //    if (employee.Users == null) employee.Users = new List<User>();
            //    employee.Users.Add(newUser);
            //}
            //WriteDataSet(accountypes.ToList());
            using (_DBStoreAccountingContext ctx = new _DBStoreAccountingContext())
            {
                foreach (AccountType accountType in accountypes)
                {
                    if (ctx.AccountTypes.FirstOrDefault(a => a.Name == accountType.Name)==null)
                        ctx.AccountTypes.Add(accountType);
                }
                ctx.SaveChanges();
            }
            using(_DBStoreAccountingContext ctx = new _DBStoreAccountingContext())
            {
                AccountType adminAccount = ctx.AccountTypes.FirstOrDefault(a => (a.Name == "Admin"));

                foreach (Employee employee in employees)
                {
                    if (ctx.Employees.FirstOrDefault(e => (e.Lastname == employee.Lastname) && (e.Firstname == employee.Firstname))==null)
                        ctx.Employees.Add(employee);
                }
                ctx.SaveChanges();
            }
            AccountService accountService = new AccountService();
            _DBStoreAccountingContext context = new _DBStoreAccountingContext();
            Employee userToAdd = null;
            userToAdd = context.Employees.FirstOrDefault(e=>e.Firstname.Equals("kenny",StringComparison.OrdinalIgnoreCase));
            if (userToAdd != null)
                accountService.Add(new AccountDTO { Username = userToAdd.Firstname, Password = "123", EmailAddress = userToAdd.EmailAddress, Employee = userToAdd});
        }
        private bool WriteDataSet<T>(List<T>dataToAdd)
        {
            bool bSuccess = false;
            using (_DBStoreAccountingContext ctx = new _DBStoreAccountingContext())
            {
                //DbSet dbSet1 = ctx.Set(dataToAdd.GetType());
                //bSuccess = true;
                //var dbSets = ctx.GetType().GetProperties().Where(p => p.PropertyType.Name.StartsWith("DbSet"));
                //foreach (var dbSetProps in dbSets)
                //{
                //    var dbSet = dbSetProps.GetValue(ctx, null);
                //    var dbSetType = dbSet.GetType().GetGenericArguments().First();
                //    var temp1 = dbSetType.Name;
                //    var temp2 = dataToAdd.GetType().Name;
                //    if (dbSetType.Name == dataToAdd.GetType().ToString())
                //    {
                //        foreach (var data in dataToAdd)
                //        {
                //            dbSet.
                //        }
                //    }
                //}
            }

            return bSuccess;
        }
    }
}
