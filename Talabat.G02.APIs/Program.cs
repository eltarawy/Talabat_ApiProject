using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StackExchange.Redis;
using TalabatG02.APIs.Errors;
using TalabatG02.APIs.Extentions;
using TalabatG02.APIs.Helpers;
using TalabatG02.APIs.MiddleWares;
using TalabatG02.Core.Entities;
using TalabatG02.Core.Entities.Identity;
using TalabatG02.Core.Repositories;
using TalabatG02.Repository;
using TalabatG02.Repository.Data;
using TalabatG02.Repository.Identity;

namespace Talabat.G02.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services works with DI
            // Add services to the container.

            builder.Services.AddControllers(); //Add Services ASP web APIs



            //-----------------------------------------------------------
            //--------------------------DataBases
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });
            builder.Services.AddDbContext<AppIdentityDbcontext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            //-----------------------------------------------------------
            //--------------------------Extention Services
            builder.Services.AddApplicationServices();
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddSwaggerServices();




            builder.Services.AddCors(option =>
            {
                option.AddPolicy("MyPolicy", option =>
                {
                    option.AllowAnyHeader().AllowAnyMethod().WithOrigins(builder.Configuration["FrontUrl"]);
                });
            });
            #endregion


            var app = builder.Build();

            #region Updat-Database inside Main
            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;



            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                //Update-Database
                var dbContext = services.GetRequiredService<StoreContext>();
                await dbContext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(dbContext);


                var identityDbContext = services.GetRequiredService<AppIdentityDbcontext>();
                await identityDbContext.Database.MigrateAsync();

                var userManger = services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUsersAsync(userManger);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error Occursd during apply the Migration");
            }
            #endregion

             
            #region Configure request into pipeline 
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWares();
            }
            app.UseMiddleware<Exceptionmiddleware>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStatusCodePagesWithRedirects("/errors/{0}");
            app.UseCors("MyPolicy");
            app.UseAuthentication(); 
            app.UseAuthorization();


            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}