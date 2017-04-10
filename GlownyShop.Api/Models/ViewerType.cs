using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlownyShop.Api.Models
{
    public class ViewerType : ObjectGraphType<Viewer>
    {
        public ViewerType(Core.Data.IAdminRoleRepository adminRoleRepository)
        {
            Name = "Viewer";
            Description = "Root Query";

            Field<ListGraphType<AdminRoleType>>(
                "AdminRoles",
                description: "List of Roles for Admin Site",
                resolve: context => { return adminRoleRepository.GetAll().Result; }
                );

            Field<AdminRoleType>(
                "AdminRole",
                description: "Role Detail for Admin Site by id",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "id of the droid" }
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    var adminRole = adminRoleRepository.Get(id).Result;
                    //var mapped = mapper.Map<Droid>(droid);
                    return adminRole;
                }
                );
        }
    }
}
