using StoreAccountingApp.CustomMethods;
using StoreAccountingApp.DTO;
using StoreAccountingApp.Services;
using StoreAccountingApp.Services.DBTables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.Models
{
    
    public class DBStoreAccountingContext : DbContext
    {
        /* When database is still in use and you can't change it, do the following query in msSQL:
         * 
         * alter database StoreAccountingDB set single_user with rollback immediate

           alter database StoreAccountingDB set MULTI_USER
         */
        public DBStoreAccountingContext() : base("name=StoreAccountingAppDBConnectionString")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DBStoreAccountingContext>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<DBStoreAccountingContext>());
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
            DBStoreAccountingContext ctx = new DBStoreAccountingContext();
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
            foreach (AccountType accountType in accountypes)
            {
                if (ctx.AccountTypes.FirstOrDefault(a => a.Name == accountType.Name)==null)
                    ctx.AccountTypes.Add(accountType);
            }
            ctx.SaveChanges();
            AccountType adminAccount = ctx.AccountTypes.FirstOrDefault(a => (a.Name == "Admin"));

            foreach (Employee employee in employees)
            {
                if (ctx.Employees.FirstOrDefault(e => (e.Lastname == employee.Lastname) && (e.Firstname == employee.Firstname))==null)
                    ctx.Employees.Add(employee);
            }
            ctx.SaveChanges();
            AccountService accountService = new AccountService();
            DBStoreAccountingContext context = new DBStoreAccountingContext();
            Employee userToAdd = null;
            userToAdd = context.Employees.FirstOrDefault(e=>e.Firstname.Equals("kenny",StringComparison.OrdinalIgnoreCase));
            if (userToAdd != null && !accountService.UserNameExists("kenny"))
                accountService.Add(new AccountDTO { Username = userToAdd.Firstname, Password = "123", EmailAddress = userToAdd.EmailAddress, AccountTypeId = 1, EmployeeId = 1});
            ProductService productService = new ProductService();
            ProductDTO[] productDTOs =
            {
                new ProductDTO()
                {
                    Name = "G700 Mouse",
                    Manufacturer = "Logitech",
                    Description = "My awesome Logitech mouse description"
                },
                new ProductDTO()
                {
                    Name = "Blackwidow Keyboard",
                    Manufacturer = "Razor",
                    Description = "My awesome Blackwidow Razor keyboard description"
                },
                new ProductDTO()
                {
                    Name = "Dell Monitor",
                    Manufacturer = "Dell",
                    Description = "My awesome Dell monitor description"
                }
            };
            foreach (ProductDTO item in productDTOs)
            {
                if (ctx.Products.FirstOrDefault(p=>p.Name == item.Name)==null)
                    productService.Add(item);
            }
            ClientService clientService = new ClientService();
            CountryService countryService = new CountryService();
            if (ctx.Countries.FirstOrDefault(c=>c.Name=="Belgium")==null)
                countryService.Add(new CountryDTO() { Name = "Belgium" });
            DistrictService districtService = new DistrictService();
            if (ctx.Districts.FirstOrDefault(d => d.PostalCodeId == "2000") == null)
                districtService.Add(new DistrictDTO()
                {
                    PostalCodeId = "2000",
                    Name = "Antwerpen-centrum",
                    CountryId = ctx.Countries.FirstOrDefault(c => c.Name.Equals("Belgium", StringComparison.OrdinalIgnoreCase)).CountryId
                }); 
            ShopDTO[] shopDTOs =
            {
                new ShopDTO()
                {
                     BuildingName = "ShopAntwerpen",
                     PostalCodeId = "2000",
                     EmailAddress = "ShopAntwerpen@gmail.com",
                     Status = "Active"
                }
            };
            ShopService shopService = new ShopService();
            foreach (ShopDTO item in shopDTOs)
            {
                if (ctx.Shops.FirstOrDefault(s => s.BuildingName == item.BuildingName) == null)
                    shopService.Add(item);
            }
            ClientDTO[] clientDTOs =
{
                new ClientDTO()
                {
                    Firstname = "Jan",
                    Lastname = "DeBruyn",
                    EmailAddress = "Jan.DeBruyn@gmail.com",
                    PostalCodeId = ctx.Districts.FirstOrDefault(d=>d.PostalCodeId=="2000").PostalCodeId,
                    Organisation = "Particulier"
                },
                new ClientDTO()
                {
                    Firstname = "Els",
                    Lastname = "Janssens",
                    EmailAddress = "Els.Janssens@gmail.com",
                    PostalCodeId = ctx.Districts.FirstOrDefault(d=>d.PostalCodeId=="2000").PostalCodeId,
                    Organisation = "Particulier"
                },
                new ClientDTO()
                {
                    Firstname = "Jean",
                    Lastname = "Smith",
                    EmailAddress = "Jean.Smith@gmail.com",
                    PostalCodeId = ctx.Districts.FirstOrDefault(d=>d.PostalCodeId=="2000").PostalCodeId,
                    Organisation = "Smith BvBa"
                }
            };
            foreach (ClientDTO item in clientDTOs)
            {
                if (ctx.Clients.FirstOrDefault(c => c.Firstname == item.Firstname && c.Lastname == item.Lastname) == null)
                {
                    clientService.Add(item);
                }
            }

            SaleDTO[] saleDTOs =
            {
                new SaleDTO()
                {
                    InvoiceNumber="INV123321-20",
                    ClientId = ctx.Clients.FirstOrDefault(c=>c.Firstname == "Jean").ClientId,
                    Status = "Delivered",
                    ShopId = ctx.Shops.FirstOrDefault(s=>s.BuildingName == "ShopAntwerpen").ShopId,
                    EmployeeId = ctx.Employees.FirstOrDefault(e=>e.Firstname == "Kenny").EmployeeId
                },
                new SaleDTO()
                {
                    InvoiceNumber="INV123322-20",
                    ClientId = ctx.Clients.FirstOrDefault(c=>c.Firstname == "Els").ClientId,
                    Status = "Delivered",
                    ShopId = ctx.Shops.FirstOrDefault(s=>s.BuildingName == "ShopAntwerpen").ShopId,
                    EmployeeId = ctx.Employees.FirstOrDefault(e=>e.Firstname == "Kenny").EmployeeId
                },
                new SaleDTO()
                {
                    InvoiceNumber="INV123323-20",
                    ClientId = ctx.Clients.FirstOrDefault(c=>c.Firstname == "Jan").ClientId,
                    Status = "Send",
                    ShopId = ctx.Shops.FirstOrDefault(s=>s.BuildingName == "ShopAntwerpen").ShopId,
                    EmployeeId = ctx.Employees.FirstOrDefault(e=>e.Firstname == "Kenny").EmployeeId
                },

            };
            //ObjMethods.SaveDataIfNotFound<SaleDTO, Sale, SaleService>(saleDTOs, "InvoiceNumber");
            SaleService saleService = new SaleService();
            foreach (SaleDTO item in saleDTOs)
            {
                if (ctx.Sales.FirstOrDefault(s => s.InvoiceNumber == item.InvoiceNumber) == null)
                    saleService.Add(item);
            }
            SaleProductDTO[] saleProductDTOs =
            {
                new SaleProductDTO()
                {
                    ProductId = ctx.Products.FirstOrDefault(p=>p.Name.Equals("G700 Mouse",StringComparison.OrdinalIgnoreCase)).ProductId,
                    SaleId = ctx.Sales.FirstOrDefault(s=>s.InvoiceNumber.Equals("INV123321-20",StringComparison.OrdinalIgnoreCase)).SaleId,
                    Amount = 1,
                    UnitPrice = 300,
                    Status = "Done"
                },
                new SaleProductDTO()
                {
                    ProductId = ctx.Products.FirstOrDefault(p=>p.Name.Equals("Blackwidow Keyboard",StringComparison.OrdinalIgnoreCase)).ProductId,
                    SaleId = ctx.Sales.FirstOrDefault(s=>s.InvoiceNumber.Equals("INV123321-20",StringComparison.OrdinalIgnoreCase)).SaleId,
                    Amount = 1,
                    UnitPrice = 450,
                    Status = "Done"
                },
                new SaleProductDTO()
                {
                    ProductId = ctx.Products.FirstOrDefault(p=>p.Name.Equals("Dell Monitor",StringComparison.OrdinalIgnoreCase)).ProductId,
                    SaleId = ctx.Sales.FirstOrDefault(s=>s.InvoiceNumber.Equals("INV123321-20",StringComparison.OrdinalIgnoreCase)).SaleId,
                    Amount = 1,
                    UnitPrice = 500,
                    Status = "Done"
                },
                new SaleProductDTO()
                {
                    ProductId = ctx.Products.FirstOrDefault(p=>p.Name.Equals("G700 Mouse",StringComparison.OrdinalIgnoreCase)).ProductId,
                    SaleId = ctx.Sales.FirstOrDefault(s=>s.InvoiceNumber.Equals("INV123322-20",StringComparison.OrdinalIgnoreCase)).SaleId,
                    Amount = 1,
                    UnitPrice = 300,
                    Status = "Done"
                },
                new SaleProductDTO()
                {
                    ProductId = ctx.Products.FirstOrDefault(p=>p.Name.Equals("Blackwidow Keyboard",StringComparison.OrdinalIgnoreCase)).ProductId,
                    SaleId = ctx.Sales.FirstOrDefault(s=>s.InvoiceNumber.Equals("INV123322-20",StringComparison.OrdinalIgnoreCase)).SaleId,
                    Amount = 1,
                    UnitPrice = 450,
                    Status = "Done"
                },
                new SaleProductDTO()
                {
                    ProductId = ctx.Products.FirstOrDefault(p=>p.Name.Equals("Dell Monitor",StringComparison.OrdinalIgnoreCase)).ProductId,
                    SaleId = ctx.Sales.FirstOrDefault(s=>s.InvoiceNumber.Equals("INV123322-20",StringComparison.OrdinalIgnoreCase)).SaleId,
                    Amount = 2,
                    UnitPrice = 500,
                    Status = "Done"
                },
                new SaleProductDTO()
                {
                    ProductId = ctx.Products.FirstOrDefault(p=>p.Name.Equals("G700 Mouse",StringComparison.OrdinalIgnoreCase)).ProductId,
                    SaleId = ctx.Sales.FirstOrDefault(s=>s.InvoiceNumber.Equals("INV123323-20",StringComparison.OrdinalIgnoreCase)).SaleId,
                    Amount = 2,
                    UnitPrice = 300,
                    Status = "Done"
                },
                new SaleProductDTO()
                {
                    ProductId = ctx.Products.FirstOrDefault(p=>p.Name.Equals("Blackwidow Keyboard",StringComparison.OrdinalIgnoreCase)).ProductId,
                    SaleId = ctx.Sales.FirstOrDefault(s=>s.InvoiceNumber.Equals("INV123323-20",StringComparison.OrdinalIgnoreCase)).SaleId,
                    Amount = 1,
                    UnitPrice = 450,
                    Status = "Done"
                },
                new SaleProductDTO()
                {
                    ProductId = ctx.Products.FirstOrDefault(p=>p.Name.Equals("Dell Monitor",StringComparison.OrdinalIgnoreCase)).ProductId,
                    SaleId = ctx.Sales.FirstOrDefault(s=>s.InvoiceNumber.Equals("INV123323-20",StringComparison.OrdinalIgnoreCase)).SaleId,
                    Amount = 1,
                    UnitPrice = 500,
                    Status = "Done"
                }
            };
            SaleProductService saleProductService = new SaleProductService();
            foreach (SaleProductDTO item in saleProductDTOs)
            {
                if (ctx.SaleProducts.FirstOrDefault(sp => sp.ProductId == item.ProductId && sp.SaleId == item.SaleId) == null)
                    saleProductService.Add(item);
            }
            //ObjMethods.SaveDataIfNotFound<SaleProductDTO, SaleProduct, SaleProductService>(saleProductDTOs, "Name", "InvoiceNumber");
        }
        //private void SaveDataIfNotFound<DTOModel,DBModel, ServiceModel>(DTOModel[] dataToAdd,params string[] uniqueColumns)
        //    where DBModel : BaseModel,new()
        //    where DTOModel : BaseDTO,new()
        //    where ServiceModel : BaseService<DTOModel,DBModel>,new()
        //{
        //    DBStoreAccountingContext ctx = new DBStoreAccountingContext();
        //    ServiceModel serviceModel = new ServiceModel();
        //    List<PropertyInfo> dtoProperties = new List<PropertyInfo>();
        //    foreach (var item in uniqueColumns)
        //    {
        //        PropertyInfo propertyInfo = typeof(DTOModel).GetProperties().Where(x => x.CanRead).Where(x => item.Equals(x.Name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        //        if (propertyInfo != null)
        //            dtoProperties.Add(propertyInfo);
        //    }
        //    if (dtoProperties != null)
        //    {
        //        foreach (DTOModel item in dataToAdd)
        //        {
        //            int propIndex = 0;
        //            var recordsFound = ctx.Set<DBModel>().Where(x => dtoProperties[propIndex].GetValue(x, null) == dtoProperties[propIndex].GetValue(item, null)).DefaultIfEmpty();
        //            propIndex++;
        //            while (recordsFound!=null)
        //            {
        //                recordsFound = recordsFound.Where(x => dtoProperties[propIndex].GetValue(x, null) == dtoProperties[propIndex].GetValue(item, null)).DefaultIfEmpty();
        //                propIndex++;
        //            }
        //            if (recordsFound == null)
        //                serviceModel.Add(item);
        //        }
        //    }
        //}

    }
}
