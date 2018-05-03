using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CookBook.BL.Facades;
using Microsoft.AspNetCore.Mvc;
using CookBook.WEB.Models;

namespace CookBook.WEB.Controllers
{
    public class RecipesController : Controller
    {
        private readonly RecipeFacade recipeFacade;

        public RecipesController(RecipeFacade recipeFacade)
        {
            this.recipeFacade = recipeFacade;
        }
        public IActionResult Recipes()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
