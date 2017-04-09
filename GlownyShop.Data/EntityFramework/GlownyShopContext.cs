using GlownyShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GlownyShop.Data.EntityFramework
{
    internal class GlownyShopContext : DbContext
    {
        private bool _migrations;

        public GlownyShopContext()
        {
            _migrations = true;
        }

        public GlownyShopContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_migrations)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GlownyShop;Integrated Security=SSPI;integrated security=true;MultipleActiveResultSets=True;");
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