using CookBook.BL.Models;
using CookBook.DAL.Entities;

namespace CookBook.BL.Mappers
{
    public class RecipeMapper
    {
        public RecipeListModel MapEntityToListModel(RecipeEntity entity)
        {
            return new RecipeListModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Type = entity.Type,
                Duration = entity.Duration
            };
        }

        public RecipeDetailModel MapEntityToDetailModel(RecipeEntity entity)
        {
            return new RecipeDetailModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Type = entity.Type,
                Description = entity.Description,
                Duration = entity.Duration,
                Ingredients = entity.Ingredients
            };
        }

        public RecipeEntity MapDetailModelToEntity(RecipeDetailModel entity)
        {
            return new RecipeEntity()
            {
                Id = entity.Id,
                Name = entity.Name,
                Type = entity.Type,
                Description = entity.Description,
                Duration = entity.Duration,
                Ingredients = entity.Ingredients
            };
        }

        public RecipeListModel MapDetailModelToListModel(RecipeDetailModel detailModel)
        {
            return new RecipeListModel
            {
                Id = detailModel.Id,
                Duration = detailModel.Duration,
                Name = detailModel.Name,
                Type = detailModel.Type
            };
        }
    }
}
