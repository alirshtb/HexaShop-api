using HexaShop.Common;
using HexaShop.Domain;
using HexaShop.Domain.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel;
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
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItems> CartItems { get; set; }
        public DbSet<History> History { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Order> Orders { get; set; }

        #endregion





        // --- configurations --- //
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }





        #region methods 

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            foreach (var entry in this.ChangeTracker.Entries<BaseDomainEntity>())
            {
                entry.Entity.LastModifiedDate = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now;
                }

            }
            return await LogAndSaveChangesAsync();
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

            return LogAndSaveChanges();
        }


        /// <summary>
        /// log and save Changes Asyncronously
        /// </summary>
        /// <returns></returns>
        private async Task<int> LogAndSaveChangesAsync()
        {

            this.ChangeTracker.DetectChanges();

            // --- get tracked entities --- //
            var trackedEntities = this.ChangeTracker.Entries().Where(en => en.State != Microsoft.EntityFrameworkCore.EntityState.Unchanged).ToList();

            var historiesList = LogChanges(trackedEntities);

            await base.SaveChangesAsync();

            // --- History = Table Column or Entity Property --- //
            // --- List<List<History>>() = Table or Entity --- //
            // --- List<History>() = List<Table> or List<Entity> --- //

            foreach (var entityLogs in historiesList)
            {

                await this.History.AddRangeAsync(entityLogs.Histories);

                foreach (var history in entityLogs.Histories)
                {

                    history.RecordId = entityLogs.entity.Entity.GetType().GetProperty("Id").GetValue(entityLogs.entity.Entity).ToString();

                }
            }


            var result = await base.SaveChangesAsync();

            return await Task.FromResult(result);

        }

        /// <summary>
        /// log and save Changes synchronously
        /// </summary>
        /// <returns></returns>
        private int LogAndSaveChanges()
        {
            this.ChangeTracker.DetectChanges();

            // --- get tracked entities --- //
            var trackedEntities = this.ChangeTracker.Entries().Where(en => en.State != Microsoft.EntityFrameworkCore.EntityState.Unchanged).ToList();

            // --- get current loged in user --- //
            //var currentUser = this.IdtUser.GetCurrentIdtUserAsync(includes: new List<string>() { "User" }).Result;


            var historiesList = LogChanges(trackedEntities);

            base.SaveChanges();

            // --- History = Table Column or Entity Property --- //
            // --- List<List<History>>() = Table or Entity --- //
            // --- List<History>() = List<Table> or List<Entity> --- //

            foreach (var entityLogs in historiesList)
            {
                this.History.AddRange(entityLogs.Histories);

                foreach (var history in entityLogs.Histories)
                {

                    history.RecordId = entityLogs.entity.Entity.GetType().GetProperty("Id").GetValue(entityLogs.entity.Entity).ToString();

                }
            }

            var result = base.SaveChanges();

            return result;
        }

        /// <summary>
        /// log changes and return history list
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private List<(List<History> Histories, EntityEntry entity)> LogChanges(List<EntityEntry> entityEntries)
        {

            var historiesList = new List<(List<History>, EntityEntry)>();

            foreach (var entry in entityEntries)
            {
                var entityName = entry.Metadata.Name.Split('.').Last();

                // --- if entity name is defined in the exceptions enumeration, it will not be loged --- //
                if (Enum.IsDefined(typeof(HistoryExceptTables), entityName))
                {
                    continue;
                }

                if (entry == null) continue;

                var histories = GetChangesHistoryList(entry, entry.State);

                historiesList.Add((histories, entry));
            }

            return historiesList;
        }

        /// <summary>
        /// get history list for Modified object - a history row for each Modified Table Column
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private List<History> GetChangesHistoryList(EntityEntry entity, EntityState state)
        {

            var historyList = new List<History>();

            // --- get properties --- //
            var entityProperties = entity.Properties;

            // --- get entity name - TableName --- //
            var entityName = GetDomainDisplayName(entity);

            // --- initial RecordId --- //
            string recordId = null;

            foreach (var property in entityProperties.ToList())
            {
                // --- get property name --- //
                var propertyName = property.Metadata.Name;

                // --- Get property display name --- //
                var propertyDisplayName = GetPropertyDisplayName(entity, propertyName);

                // --- if property = Id -> will not be loged --- //
                if (propertyName.ToString().ToLower() == "id" || propertyName.ToString().ToLower().Contains("image"))
                {
                    recordId = property.CurrentValue.ToString();
                    continue;
                }

                // --- property old and current values --- //
                GetValues(property, state, out string oldValue, out string currentValue);

                if (state == EntityState.Modified && oldValue == currentValue)
                {
                    continue;
                }

                //var currentUser = this.IdtUser.GetCurrentIdtUserAsync(includes: new List<string>() { "User" }).Result;

                //int? userId = null;
                //string userFullName = "-";
                //if (currentUser != null)
                //{
                //    userId = currentUser.UserId;
                //    userFullName = currentUser.User.FullName;
                //}

                // --- record history --- //
                historyList.Add(new History()
                {
                    Id = Guid.NewGuid().ToString(),
                    PreviousValue = oldValue,
                    NextValue = currentValue,
                    ChangeDate = DateTime.Now,
                    ColumnName = propertyDisplayName,
                    //UserId = userId,
                    //UserFullName = userFullName,
                    TableName = entityName,
                    State = state.ToString(),
                    RecordId = recordId
                });
            }

            return historyList;
        }

        /// <summary>
        /// get property old and current value
        /// </summary>
        /// <param name="property"></param>
        /// <param name="state"></param>
        /// <param name="oldValue"></param>
        /// <param name="currentValue"></param>
        private void GetValues(PropertyEntry property, EntityState state, out string oldValue, out string currentValue)
        {


            oldValue = null;
            currentValue = null;

            switch (state)
            {
                case EntityState.Deleted:
                    oldValue = property.OriginalValue == null ? null : property.OriginalValue.ToString();
                    break;
                case EntityState.Modified:
                    oldValue = property.OriginalValue == null ? null : property.OriginalValue.ToString();
                    currentValue = property.CurrentValue == null ? null : property.CurrentValue.ToString();
                    break;
                case EntityState.Added:
                    currentValue = property.CurrentValue == null ? null : property.CurrentValue.ToString();
                    break;
            }
        }

        /// <summary>
        /// get column Name
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private string GetPropertyDisplayName(EntityEntry entity, string propertyName)
        {

            var properties = entity.Entity.GetType().GetProperties();

            var propery = properties.Where(prop => prop.Name.ToLower() == propertyName.ToLower()).FirstOrDefault();

            // --- if property has display name --- //
            #region
            //var displayName = propery.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault() as DisplayNameAttribute;

            //if (displayName != null)
            //{
            //    return displayName.DisplayName;
            //}
            #endregion

            // --- else --- //
            var columnName = propery.Name;

            return columnName;
        }

        /// <summary>
        /// get Table Name
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private string GetDomainDisplayName(EntityEntry entity)
        {

            // --- if models has display name --- //
            #region
            //var displayName = entity.Entity.GetType().GetCustomAttributes(typeof(DisplayNameAttribute), true)
            //                                  .FirstOrDefault()
            //                                  as DisplayNameAttribute;

            //if (displayName != null)
            //{
            //    return displayName.DisplayName;
            //}

            //return entity.Metadata.Name.Split('.').Last();
            #endregion

            // --- else --- //
            var tableName = entity.Entity.GetType().Name;
            return tableName;
        }




        #endregion



    }
}
