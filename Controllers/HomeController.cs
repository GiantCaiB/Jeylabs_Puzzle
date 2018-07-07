using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Jeylabs_puzzle.Models;

namespace Jeylabs_puzzle.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Info()
        {
            ViewData["Message"] = "Puzzles from Jeylabs";

            return View();
        }

        [HttpPost]
        public IActionResult HandleCommand()
        {
            var raw = Request.Form["command"].ToString();
            Shape shape = Command.RawHandler(raw);
            if(shape==null)
            {
                ViewData["Error"] = "Invalid input";
                return View("Index");
            }
            bool DrawnWell = new Drawing().Draw(shape);
            if(!DrawnWell)
            {
                ViewData["Error"] = "The figures made no sense..";
            }
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
