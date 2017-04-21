using GraphQL.Types;

namespace GlownyShop.Api.Models
{
    public class GlownyShopQuery : ObjectGraphType
    {
        //public GlownyShopQuery() { }

        public GlownyShopQuery()
        {
            Name = "Query";

            Field<ViewerType>(
                "viewer",
                resolve: context => { return new Viewer(); }
                );
        }
    }
}