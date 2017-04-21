using GlownyShop.Models;
using GraphQL;
using GraphQL.Types;
using System.Linq;

namespace GlownyShop.Api.Models
{
    public class AdminRoleType : ObjectGraphType<AdminRole>
    {
        public AdminRoleType(Core.Data.IAdminRoleRepository adminRoleRepository)
        {
            Name = "AdminRole";
            Description = "Roles for Admin Site";

            Field(x => x.Id).Description("The Id of the Admin Role.");
            Field(x => x.Name, nullable: false).Description("The name of the Admin Role.");

            Field<ListGraphType<AdminUserType>>(
                "AdminUsers",
                description: "List of Users for Admin Site",
                resolve: context =>
                {
                    var userContext = context.UserContext.As<GraphQLUserContext>();
                    var adminRole = adminRoleRepository.Get(context.Source.Id,
                        "AdminUserRoles.AdminUser").Result;

                    return adminRole.AdminUserRoles.Select(x => x.AdminUser);
                }
                );
        }
    }
}