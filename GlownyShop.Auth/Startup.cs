using GlownyShop.Core.Data;
using GlownyShop.Core.Logic;
using GlownyShop.Data.EntityFramework;
using GlownyShop.Data.EntityFramework.Repositories;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySQL.Data.EntityFrameworkCore.Extensions;

namespace GlownyShop.Auth
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            Env = env;
        }

        public IConfigurationRoot Configuration { get; }
        private IHostingEnvironment Env { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            // configure identity server with in-memory stores, keys, clients and resources
            services.AddTransient<IAdminRoleRepository, AdminRoleRepository>();
            services.AddTransient<IAdminUserRepository, AdminUserRepository>();
            services.AddTransient<ISecurityService, SecurityService>();

            if (Env.IsEnvironment("Test"))
            {
                services.AddDbContext<GlownyShopContext>(options =>
                    options.UseInMemoryDatabase(databaseName: "GlownyShop"));
            }
            else
            {
                services.AddDbContext<GlownyShopContext>(options =>
                    options.UseMySQL(Configuration["ConnectionStrings:GlownyShopDatabaseConnection"]));
            }
            
            services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                //.AddTestUsers(Config.GetUsers())
                .Services.AddScoped<IProfileService, ProfileService>()
                        .AddScoped<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>()
                ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //if (env.IsDevelopment())
            //{
            app.UseDeveloperExceptionPage();
            //}

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});

            app.UseIdentityServer();
        }
    }
}