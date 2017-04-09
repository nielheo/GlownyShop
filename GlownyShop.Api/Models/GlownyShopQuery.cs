using AutoMapper;
using GlownyShop.Models;
using GraphQL.Types;

namespace GlownyShop.Api.Models
{
    public class GlownyShopQuery : ObjectGraphType
    {
        //public GlownyShopQuery() { }

        public GlownyShopQuery(Core.Data.IAdminRoleRepository adminRoleRepository)
        {
            Name = "Query";

            Field<ListGraphType<AdminRoleType>>(
                "AdminRoles",
                resolve: context => { return adminRoleRepository.GetAll().Result; }
                );

            Field<AdminRoleType>(
                "AdminRole",
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