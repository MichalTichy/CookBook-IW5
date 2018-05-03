using System;
using System.Collections.Generic;
using System.Linq;
using CookBook.BL.Mappers;
using CookBook.BL.Models;
using CookBook.DAL;
using CookBook.DAL.Entities;
using CookBook.DAL.Entities.Base.Interface;
using Microsoft.EntityFrameworkCore;

namespace CookBook.BL.Repositories
{
    public class RecipeRepository
    {
        private readonly RecipeMapper mapper = new RecipeMapper();
        internal RecipeDetailModel FindByName(string name)
        {
            using (var cookBookDbContext = new CookBookDbContext())
            {
                var recipe = cookBookDbContext
                    .Recipes
                    .Include(r => r.Ingredients.Select(i => i.Ingredient))
                    .FirstOrDefault(r => r.Name == name);
                return mapper.MapEntityToDetailModel(recipe);
            }
        }

        internal RecipeDetailModel GetById(Guid id)
        {
            using (var cookBookDbContext = new CookBookDbContext())
            {
                var recipeEntity = cookBookDbContext.Recipes
                    .Include(r => r.Ingredients.Select(i => i.Ingredient))
                    .FirstOrDefault(r => r.Id == id);

                return mapper.MapEntityToDetailModel(recipeEntity);
            }
        }


        internal RecipeDetailModel Insert(RecipeDetailModel detail)
        {
            using (var cookBookDbContext = new CookBookDbContext())
            {
                var entity = mapper.MapDetailModelToEntity(detail);
                entity.Id = Guid.NewGuid();

                cookBookDbContext.Recipes.Add(entity);
                cookBookDbContext.SaveChanges();

                return mapper.MapEntityToDetailModel(entity);
            }
        }

        internal void Update(RecipeDetailModel detail)
        {
            using (var cookBookDbContext = new CookBookDbContext())
            {
                var entity = cookBookDbContext.Recipes.First(r => r.Id == detail.Id);

                entity.Name = detail.Name;
                entity.Description = detail.Description;
                entity.Duration = detail.Duration;
                entity.Type = detail.Type;

                cookBookDbContext.SaveChanges();
            }
        }

        internal void Remove(Guid id)
        {
            using (var cookBookDbContext = new CookBookDbContext())
            {
                var entity = new RecipeEntity() { Id = id };
                cookBookDbContext.Recipes.Attach(entity);

                cookBookDbContext.Recipes.Remove(entity);
                cookBookDbContext.SaveChanges();
            }
        }
    }
}
