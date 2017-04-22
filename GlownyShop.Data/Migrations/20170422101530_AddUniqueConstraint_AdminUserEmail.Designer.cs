using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GlownyShop.Data.EntityFramework;

namespace GlownyShop.Data.Migrations
{
    [DbContext(typeof(GlownyShopContext))]
    [Migration("20170422101530_AddUniqueConstraint_AdminUserEmail")]
    partial class AddUniqueConstraint_AdminUserEmail
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

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
