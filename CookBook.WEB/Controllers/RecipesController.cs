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

        [HttpGet]
        public ViewResult Index()
        {
            var recipes = recipeFacade.GetAllRecipes();
            return View(recipes);
        }
        
        [HttpGet]
        public IActionResult Detail(Guid? id)
        {
            if (!id.HasValue)
            {
                return View(recipeFacade.CreateNew());
            }

            var recipe = recipeFacade.GetRecipe(id.Value);

            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save([Bind("Name,Type,Description,Duration,Id")] RecipeDetailModel recipe)
        {
            if (!ModelState.IsValid)
            {
                return View("Detail", recipe);
            }

            var savedRecipe = recipeFacade.Save(recipe);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(Guid? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

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
    }
}
