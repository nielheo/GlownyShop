using GlownyShop.Models;
using GraphQL;
using GraphQL.Types;
using System.Collections.Generic;

namespace GlownyShop.Api.Models
{
    public class ViewerType : ObjectGraphType<Viewer>
    {
        public ViewerType(Core.Data.IAdminRoleRepository adminRoleRepository,
            Core.Data.IAdminUserRepository adminUserRepository)
        {
            Name = "Viewer";
            Description = "Root Query";

            Field<ListGraphType<AdminRoleType>>(
                "AdminRoles",
                description: "Role Detail for Admin Site by id",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "id", Description = "id of the droid" }
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<int?>("id");
                    var userContext = context.UserContext.As<GraphQLUserContext>();
                    if (id != null)
                    {
                        IList<AdminRole> adminRoles = new List<AdminRole>();
                        var adminRole = adminRoleRepository.Get(id.Value).Result;
                        if (adminRole != null)
                            adminRoles.Add(adminRole);

                        return adminRoles;
                    }
                    else
                    {
                        return adminRoleRepository.GetAll().Result;
                    }
                }
                );

            Field<ListGraphType<AdminUserType>>(
                "AdminUsers",
                description: "List of Users for Admin Site",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "id", Description = "id of the user" }
                ),
                resolve: context =>
                {
                    var userContext = context.UserContext.As<GraphQLUserContext>();

                    var id = context.GetArgument<int?>("id");
                    if (id != null)
                    {
                        IList<AdminUser> adminUsers = new List<AdminUser>();
                        var adminUser = adminUserRepository.Get(id.Value).Result;
                        if (adminUser != null)
                            adminUsers.Add(adminUser);

                        return adminUsers;
                    }

                    else {
                        return adminUserRepository.GetAll().Result;
                    }
                }
                );
        }
    }
}