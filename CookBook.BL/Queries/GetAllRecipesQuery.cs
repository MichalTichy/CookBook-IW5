using System.Collections.Generic;
using System.Linq;
using CookBook.BL.Mappers;
using CookBook.BL.Models;
using CookBook.DAL;

namespace CookBook.BL.Queries
{
    public class GetAllRecipesQuery
    {
        protected readonly CookBookDbContext context;
        protected RecipeMapper mapper;

        public GetAllRecipesQuery(CookBookDbContext context)
        {
            this.context = context;
            this.mapper = new RecipeMapper();
        }

        internal IEnumerable<RecipeListModel> Execute()
        {
            return context.Recipes.Select(r => mapper.MapEntityToListModel(r));
        }

    }
}