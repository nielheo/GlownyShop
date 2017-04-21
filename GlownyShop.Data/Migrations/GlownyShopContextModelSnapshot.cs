using GlownyShop.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace GlownyShop.Data.Migrations
{
    [DbContext(typeof(GlownyShopContext))]
    partial class GlownyShopContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("GlownyShop.Models.AdminRole", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("AdminRoles");
                });

            modelBuilder.Entity("GlownyShop.Models.AdminUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<bool>("IsActive");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("AdminUsers");
                });

            modelBuilder.Entity("GlownyShop.Models.AdminUserRole", b =>
                {
                    b.Property<int>("AdminUserId");

                    b.Property<int>("AdminRoleId");

                    b.HasKey("AdminUserId", "AdminRoleId");

                    b.HasIndex("AdminRoleId");

                    b.ToTable("AdminUserRoles");
                });

            modelBuilder.Entity("GlownyShop.Models.AdminUserRole", b =>
                {
                    b.HasOne("GlownyShop.Models.AdminRole", "AdminRole")
                        .WithMany("AdminUserRoles")
                        .HasForeignKey("AdminRoleId");

                    b.HasOne("GlownyShop.Models.AdminUser", "AdminUser")
                        .WithMany("AdminUserRoles")
                        .HasForeignKey("AdminUserId");
                });
        }
    }
}