using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookBook.BL.Facades;
using CookBook.BL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CookBook.DAL;
using CookBook.DAL.Entities;

namespace CookBook.WEB.Controllers
{
    public class RecipesController : Controller
    {
        private readonly RecipeFacade recipeFacade;

        public RecipesController(RecipeFacade recipeFacade)
        {
            this.recipeFacade = recipeFacade;
        }
        
        public ViewResult Index()
        {
            var recipes = recipeFacade.GetAllRecipes();
            return View(recipes);
        }
        
        public IActionResult Detail(Guid? id)
        {
            if (!id.HasValue)
                return View(recipeFacade.CreateNew());

            var recipe = recipeFacade.GetRecipe(id.Value);

            if (recipe == null)
                return NotFound();

            return View(recipe);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save([Bind("Name,Type,Description,Duration,Id")] RecipeDetailModel recipeEntity)
        {
            if (!ModelState.IsValid)
                return View("Detail", recipeEntity);

            var savedRecipe = recipeFacade.Save(recipeEntity);
            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult Delete(Guid? id)
        {
            if (!id.HasValue)
                return NotFound();

            var recipe = recipeFacade.GetRecipe(id.Value);
            return View(recipe);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public RedirectToActionResult DeleteConfirmed(Guid id)
        {
            recipeFacade.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeEntityExists(Guid id)
        {
            return recipeFacade.GetRecipe(id) != null;
        }
    }
}
