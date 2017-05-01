using GlownyShop.Api.Models;
using GlownyShop.Core.Data;
using GlownyShop.Core.Logic;
using GlownyShop.Data.EntityFramework;
using GlownyShop.Data.EntityFramework.Repositories;
using GlownyShop.Data.EntityFramework.Seed;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySQL.Data.EntityFrameworkCore.Extensions;

namespace GlownyShop.Api
{
    public class Startup
    {
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddScoped<GlownyShopQuery>();
            services.AddTransient<IAdminRoleRepository, AdminRoleRepository>();
            services.AddTransient<IAdminUserRepository, AdminUserRepository>();
            services.AddTransient<ISecurityService, SecurityService>();

            if (Env.IsEnvironment("Test"))
            {
                services.AddDbContext<GlownyShopContext>(options =>
                {
                    options.UseInMemoryDatabase(databaseName: "GlownyShop");
                });
            }
            else
            {
                services.AddDbContext<GlownyShopContext>(options =>
                {
                    options.UseMySQL(Configuration["ConnectionStrings:GlownyShopDatabaseConnection"]);
                });
            }

            services.AddDbContext<DbContext>(options =>
            {
                // Configure the context to use an in-memory store.
                options.UseInMemoryDatabase();
                // Register the entity sets needed by OpenIddict.
                // Note: use the generic overload if you need
                // to replace the default OpenIddict entities.
                options.UseOpenIddict();
            });

            services.AddOpenIddict(options =>
            {
                // Register the Entity Framework stores.
                options.AddEntityFrameworkCoreStores<DbContext>();
                // Register the ASP.NET Core MVC binder used by OpenIddict.
                // Note: if you don't call this method, you won't be able to
                // bind OpenIdConnectRequest or OpenIdConnectResponse parameters.
                options.AddMvcBinders();
                // Enable the token endpoint.
                options.EnableTokenEndpoint("/connect/token");
                // Enable the password flow.
                options.AllowPasswordFlow();
                // During development, you can disable the HTTPS requirement.
                options.DisableHttpsRequirement();
            });
            
            services.AddCors(setupAction =>
            {
                setupAction.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:3000"));
            });

            services.AddScoped<IDocumentExecuter, DocumentExecuter>();

            services.AddTransient<AdminRoleType>();
            services.AddTransient<AdminUserType>();
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

            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                );

            app.UseOAuthValidation();
            // Register the OpenIddict middleware.
            app.UseOpenIddict();
            app.UseMvcWithDefaultRoute();

            app.UseStaticFiles();
            app.UseMvc();

            db.EnsureSeedData();
        }
    }
}