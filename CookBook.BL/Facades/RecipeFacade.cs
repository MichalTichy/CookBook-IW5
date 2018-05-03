using System;
using System.Collections.Generic;
using System.Text;
using CookBook.BL.Models;
using CookBook.BL.Queries;
using CookBook.BL.Repositories;

namespace CookBook.BL.Facades
{
    public class RecipeFacade
    {
        private readonly RecipeRepository recipeRepository;
        private readonly GetAllRecipesQuery query;

        public RecipeFacade(RecipeRepository recipeRepository, GetAllRecipesQuery recipesQueryFactory)
        {
            this.recipeRepository = recipeRepository;
            query = recipesQueryFactory;
        }

        public RecipeDetailModel GetRecipe(Guid id)
        {
            return recipeRepository.GetById(id);
        }


        public RecipeDetailModel GetRecipe(string name)
        {
            return recipeRepository.FindByName(name);
        }

        public RecipeDetailModel Save(RecipeDetailModel model)
        {
            if (model.Id == Guid.Empty)
            {
                return recipeRepository.Insert(model);
            }
            else
            {
                recipeRepository.Update(model);
                return model;
            }
        }

        public void Remove(Guid id)
        {
            recipeRepository.Remove(id);
        }

        public IEnumerable<RecipeListModel> GetAllRecipes()
        {
            return query.Execute();
        }

    }
}
