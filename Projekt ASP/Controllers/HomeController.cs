using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_ASP.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Models.Movie movie = new Models.Movie()
            {
                Title = "The Godfather",
                ReleaseDate = new DateTime(1972, 3, 24)
            };
            return View(movie);
        }
    }
}
