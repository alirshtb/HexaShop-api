using HexaShop.ApiEndPoint.DynamicAuthorization;
using HexaShop.ApiEndPoint.DynamicAuthorization.HexaIdentityRequirements;
using HexaShop.ApiEndPoint.DynamicAuthorization.JWT;
using HexaShop.ApiEndPoint.DynamicAuthorization.Utilities;
using HexaShop.Common;
using HexaShop.Domain;
using HexaShop.Persistance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace HexaShop.ApiEndPoint.AddServiceConfigurations
{
    public static class EndPointServiceRegisteration
    {
        public static IServiceCollection ConfigureCommonBaseServices(this IServiceCollection services, IConfiguration configuration)
        {



            services.AddIdentity<AppIdentityUser, AppIdentityRole>()
                    .AddEntityFrameworkStores<HexaShopDbContext>()
                    .AddDefaultTokenProviders();


            services.AddAutoMapper(Assembly.GetExecutingAssembly());


            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric= false;
                options.User.RequireUniqueEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedEmail = false;

                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(1);
            });
         
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

            services.AddAuthentication()
                    .AddCookie()
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = false;
                        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                        {
                            ValidateIssuerSigningKey = true,
                            ValidateAudience = true,
                            ValidateIssuer = true,
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero,
                            ValidIssuer = configuration["JwtOptions:Issuer"],
                            ValidAudience = configuration["JwtOptions:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtOptions:Key"]))
                        };
                    });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.SlidingExpiration = false;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.ExpireTimeSpan = TimeSpan.FromHours(1);

                options.LoginPath = new PathString("/api/account/signIn");
                options.AccessDeniedPath = new PathString("/api/account/accessDenied");
                options.LogoutPath = new PathString("/api/accounting/signOut");
                options.Cookie.Name = "HexaShopAuthenticationCookies";
            });


            services.AddSingleton<IAuthorizationHandler, ApiRequirementHandler>();
            services.AddSingleton<IAthorizeUtilities, AuthorizeUtilities>();

            services.AddAuthorization(options =>
            {
                //options.AddPolicy("HexaPolicy", policy =>
                //{
                //    //policy.RequireRole("Admin");
                //    //policy.RequireRole("Admin");
                //    //policy.RequireClaim("Permission");
                //    //policy.RequireClaim("Permission", "Yes");
                //    //policy.RequireClaim("Permission", "Yes", "No", "Ok");

                //});

                //options.AddPolicy("HexaPolicy", policy =>
                //{
                //    policy.RequireAssertion(context =>
                //    {
                //        return context.User.IsInRole(RoleNames.Admin) && context.User.HasClaim("Permmission", "Yes");

                //    });
                //});


                //options.AddPolicy("HexaPolicy", policy =>
                //{
                //    policy.AddRequirements(new HexaRequirement());
                //});

                options.AddPolicy(ApiAuthorizationConstants.HexaPolicy, policy =>
                {
                    policy.AddRequirements(new ApiRequirement());
                });


            });


            return services;
        }
    }
}
