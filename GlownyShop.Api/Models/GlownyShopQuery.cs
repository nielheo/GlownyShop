using AutoMapper;
using GlownyShop.Core.Data;
using GlownyShop.Models;
using GraphQL.Types;
using System.Threading.Tasks;

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