using GraphQL.Types;
using System;

namespace GlownyShop.Api.Models
{
    public class GlownyShopSchema : Schema
    {
        public GlownyShopSchema(Func<Type, GraphType> resolveType)
            : base(resolveType)
        {
            Query = (GlownyShopQuery)resolveType(typeof(GlownyShopQuery));
        }
    }
}
