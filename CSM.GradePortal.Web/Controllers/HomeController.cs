﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using CSM.GradePortal.Web.Models;
using CSM.GradePortal.Web.Infrastructure.Data.Helpers;
using CSM.GradePortal.Web.ViewModels.Users;
using CSM.GradePortal.Web.Models;

namespace CSM.GradePortal.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly DefaultDbContext _context;

        public HomeController(DefaultDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //var users = this._context.Users.ToList();
            //return View();

            return View(new IndexViewModel()
            {
                Users = this._context.Users.ToList()
            });
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
