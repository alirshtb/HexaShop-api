using HexaShop.Common;
using HexaShop.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Persistance.Extensions
{
    public static class DbContextInitialization
    {
        public static void Seed(this HexaShopDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Roles.Any())
            {
                return;
            }


            // --- add identity role --- //
            var role = new AppIdentityRole()
            {
                Name = RoleNames.Admin,
                DisplayName = "مدیر کل",
                NormalizedName = RoleNames.Admin.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            using var transaction = context.Database.BeginTransaction();
            context.Roles.Add(role);
            context.SaveChanges();

            // --- add identity user --- //
            var user = new AppIdentityUser()
            {
                FirstName = "Ali",
                LastName = "Rashtbari",
                UserName = "admin",
                Email = "admin@hexashop.com",
                NormalizedUserName = "admin".ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var passwordHasher = new PasswordHasher<AppIdentityUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "123456789");

            context.Users.Add(user);
            context.SaveChanges();


            // --- add appuser --- //

            var appUser = new AppUser()
            {
                DateOfBirth = new DateTime(2001, 1, 17),
                CreatedBy = "Admin",
                Email = user.Email,
                Mobile = "09917586411",
                IsDeleted = false,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = Gender.Male,
                DateCreated = DateTime.Now,
                AppIdentityUserId = user.Id
            };

            context.AppUsers.Add(appUser);
            context.SaveChanges();


            var userRole = new AppIdentityUserRole()
            {
                UserId = user.Id,
                RoleId = role.Id
            };

            context.UserRoles.Add(userRole);
            context.SaveChanges();

            transaction.Commit();

        }
    }
}
