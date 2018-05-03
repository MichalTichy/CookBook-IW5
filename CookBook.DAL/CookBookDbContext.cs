using System;
using CookBook.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CookBook.DAL
{
    public class CookBookDbContext : DbContext
    {
        public DbSet<RecipeEntity> Recipes { get; set; }
        public DbSet<IngredientEntity> Ingredients { get; set; }

        public CookBookDbContext()
            : base()
        {
        }

        public CookBookDbContext(DbContextOptions<CookBookDbContext> options) : base(options)
        {
        }
    }
}