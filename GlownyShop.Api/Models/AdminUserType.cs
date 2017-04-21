using GlownyShop.Models;
using GraphQL;
using GraphQL.Types;
using System.Linq;

namespace GlownyShop.Api.Models
{
    public class AdminUserType : ObjectGraphType<AdminUser>
    {
        public AdminUserType(Core.Data.IAdminUserRepository adminUserRepository)
        {
            Name = "AdminUser";
            Description = "User for Admin Site";

            Field(x => x.Id).Description("The Id of the Admin User.");
            Field(x => x.Email, nullable: false).Description("Admin User's email");
            Field(x => x.FirstName, nullable: false).Description("Admin User's first name");
            Field(x => x.LastName, nullable: false).Description("Admin User's last name");
            Field(x => x.IsActive, nullable: false).Description("Admin User's active status");

            Field<ListGraphType<AdminRoleType>>(
                "AdminRoles",
                description: "List of Roles for Admin Site",
                resolve: context =>
                {
                    var userContext = context.UserContext.As<GraphQLUserContext>();
                    var adminUser = adminUserRepository.Get(context.Source.Id,
                        "AdminUserRoles.AdminRole").Result;

                    return adminUser.AdminUserRoles.Select(x=>x.AdminRole);
                }
                );
        }
    }
}