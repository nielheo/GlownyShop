using GlownyShop.Api.Models;
using GlownyShop.Data.EntityFramework;
using GlownyShop.Data.EntityFramework.Seed;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using GlownyShop.Core.Data;
using GlownyShop.Data.EntityFramework.Repositories;

namespace GlownyShop.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        private IHostingEnvironment Env { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddScoped<GlownyShopQuery>();
            services.AddTransient<IAdminRoleRepository, AdminRoleRepository>();

            //if (Env.IsEnvironment("Test"))
            //{
            //    services.AddDbContext<GlownyShopContext>(options =>
            //        options.UseInMemoryDatabase(databaseName: "StarWars"));
            //}
            //else
            //{
                services.AddDbContext<GlownyShopContext>(options =>
                    options.UseSqlServer(Configuration["ConnectionStrings:GlownyShopDatabaseConnection"]));
            //}
            services.AddScoped<IDocumentExecuter, DocumentExecuter>();

            services.AddTransient<AdminRoleType>();
            services.AddTransient<ViewerType>();
            var sp = services.BuildServiceProvider();
            services.AddScoped<ISchema>(_ => new GlownyShopSchema(type => (GraphType)sp.GetService(type)) { Query = sp.GetService<GlownyShopQuery>() });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILoggerFactory loggerFactory, GlownyShopContext db)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseStaticFiles();
            app.UseMvc();

            db.EnsureSeedData();
        }
    }
}