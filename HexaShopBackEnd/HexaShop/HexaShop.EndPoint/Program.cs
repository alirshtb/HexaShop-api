using HexaShop.ApiEndPoint.AddServiceConfigurations;
using HexaShop.ApiEndPoint.DynamicAuthorization.JWT;
using HexaShop.Application;
using HexaShop.Persistance;
using HexaShop.Persistance.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.
ConfigureServices(builder.Services, builder.Configuration);


var app = builder.Build();


// Configure the HTTP request pipeline.
Configure(app);




// --- configure services --- //
void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{

    services.ConfigureCommonBaseServices(configuration);
    services.ConfigurePersistanceServices(configuration);
    services.ConfigureApplicationServices();
    services.AddScoped<IJWTService, JWTService>();


    services.AddControllers()
        .AddNewtonsoftJson(option =>
        {
            option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        });
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hexa Shop", Version = "v1" });

    });


    services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
                                                          .AllowAnyMethod()
                                                          .AllowAnyHeader());
    });

}






// --- config middle wares --- //
void Configure(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "HexaShop.Api v1");
            c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        });

    }


    app.UseStaticFiles();


    #if DEBUG
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HexaShop.Api v1"));
    #else
    #endif

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseCors("CorsPolicy");

    app.MapControllers();



    using (IServiceScope scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetService<HexaShopDbContext>();
        //await context.Database.MigrateAsync();
        context.Seed();
    }


    app.Run();
}