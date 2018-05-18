using System;
using System.Collections.Generic;
using System.Linq;
using CookBook.BL.Mappers;
using CookBook.BL.Models;
using CookBook.DAL;
using CookBook.DAL.Entities;

namespace CookBook.BL.Queries
{
    public class GetAllRecipesQuery
    {
        private readonly Func<CookBookDbContext> dbContextFactory;
        protected RecipeMapper mapper;

        public GetAllRecipesQuery(Func<CookBookDbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
            this.mapper = new RecipeMapper();
        }

        internal ICollection<RecipeListModel> Execute()
        {
            using (var context=dbContextFactory())
            {
                return context.Set<RecipeEntity>().Select(r => mapper.MapEntityToListModel(r)).ToList();
            }
        }

    }
}