using HexaShop.Domain;
using HexaShop.Domain.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading;


namespace HexaShop.Persistance
{
    public class HexaShopDbContext : IdentityDbContext<AppIdentityUser, AppIdentityRole, string,
                                                       IdentityUserClaim<string>,
                                                       AppIdentityUserRole,
                                                       IdentityUserLogin<string>,
                                                       IdentityRoleClaim<string>,
                                                       IdentityUserToken<string>>
    {


        public HexaShopDbContext(DbContextOptions<HexaShopDbContext> options) : base(options)
        {

        }



        #region DbSets
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ImageSource> ImageSources { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductInCategory> ProductInCategories { get; set; }

        #endregion





        // --- configurations --- //
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }





        #region methods 

        //public IDbContextTransaction BeginTransaction()
        //{
        //    var transaction = Database.BeginTransaction();
        //    return transaction;
        //}

        //public async Task<IDbContextTransaction> BeginTransactionAsync()
        //{
        //    var transaction = await Database.BeginTransactionAsync();
        //    return transaction;
        //}

        //public void Commit()
        //{
        //    base.SaveChanges();
        //    Database.CommitTransaction();
        //}

        //public async Task CommitAsync()
        //{
        //    await base.SaveChangesAsync();
        //    await Database.CommitTransactionAsync();
        //}

        //public bool IsBeganTransaction()
        //{
        //    return Database.CurrentTransaction != null;
        //}

        //public async Task RollBackTransactionAsync()
        //{
        //    await Database.RollbackTransactionAsync();
        //}

        //public void RollBackTrasaction()
        //{
        //    Database.RollbackTransaction();
        //}

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            foreach (var entry in this.ChangeTracker.Entries<BaseDomainEntity>())
            {
                entry.Entity.LastModifiedDate = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now;
                }

            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            foreach (var entry in this.ChangeTracker.Entries<BaseDomainEntity>())
            {
                entry.Entity.LastModifiedDate = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now;
                }

            }
            return base.SaveChanges();
        }

        #endregion



    }
}
