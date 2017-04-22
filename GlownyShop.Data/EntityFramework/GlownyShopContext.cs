using GlownyShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using MySQL.Data.EntityFrameworkCore.Extensions;

namespace GlownyShop.Data.EntityFramework
{
    public class GlownyShopContext : DbContext
    {
        public readonly ILogger _logger;
        private bool _migrations;

        public GlownyShopContext()
        {
            _migrations = true;
        }

        public GlownyShopContext(DbContextOptions options, ILogger<GlownyShopContext> logger)
            : base(options)
        {
            _logger = logger;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_migrations)
            {
                //optionsBuilder.UseMySQL("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GlownyShop;Integrated Security=SSPI;integrated security=true;MultipleActiveResultSets=True;");

                optionsBuilder.UseMySQL("server=localhost;userid=root;pwd=123qwe!@#;port=3306;database=glownyshop;sslmode=none;");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // https://docs.microsoft.com/en-us/ef/core/modeling/relationships
            // http://stackoverflow.com/questions/38520695/multiple-relationships-to-the-same-table-in-ef7core

            // adminRoles
            modelBuilder.Entity<AdminRole>().HasKey(c => c.Id);
            modelBuilder.Entity<AdminRole>().Property(e => e.Id).ValueGeneratedNever();

            // adminUsers
            modelBuilder.Entity<AdminUser>().HasKey(c => c.Id);
            modelBuilder.Entity<AdminUser>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<AdminUser>()
                .HasIndex(p => new { p.Email })
                .IsUnique();

            // adminUser-Roles
            modelBuilder.Entity<AdminUserRole>().HasKey(t => new { t.AdminUserId, t.AdminRoleId });

            modelBuilder.Entity<AdminUserRole>()
                .HasOne(cf => cf.AdminUser)
                .WithMany(c => c.AdminUserRoles)
                .HasForeignKey(cf => cf.AdminUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AdminUserRole>()
                .HasOne(cf => cf.AdminRole)
                .WithMany(t => t.AdminUserRoles)
                .HasForeignKey(cf => cf.AdminRoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public virtual DbSet<AdminRole> AdminRoles { get; set; }
        public virtual DbSet<AdminUser> AdminUsers { get; set; }
        public virtual DbSet<AdminUserRole> AdminUserRoles { get; set; }
    }
}