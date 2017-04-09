using GlownyShop.Models;
using GraphQL.Types;

namespace GlownyShop.Api.Models
{
    public class AdminRoleType : ObjectGraphType<AdminRole>
    {
        public AdminRoleType()
        {
            Name = "AdminRole";
            Description = "Roles for Admin Site";

            Field(x => x.Id).Description("The Id of the Admin Role.");
            Field(x => x.Name, nullable: false).Description("The name of the Admin Role.");
        }
    }
}