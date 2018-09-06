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
using CSM.GradePortal.Web.Infrastructure.Data.Models;

namespace CSM.GradePortal.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly DefaultDbContext _context;

        public HomeController(DefaultDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int pageSize = 5, int pageIndex = 1, string keyword = "")
        {
            Page<User> result = new Page<User>();
            if (pageSize < 1)
            {
                pageSize = 1;
            }
            IQueryable<User> userQuery = (IQueryable<User>)this._context.Users;
            if (string.IsNullOrEmpty(keyword) == false)
            {
                userQuery = userQuery.Where(u => u.FirstName.Contains(keyword)
                                            || u.LastName.Contains(keyword)
                                            || u.EmailAddress.Contains(keyword));
            }
            long queryCount = userQuery.Count();
            int pageCount = (int)Math.Ceiling((decimal)(queryCount / pageSize));
            long mod = (queryCount % pageSize);
            if (mod > 0)
            {
                pageCount = pageCount + 1;
            }
            int skip = (int)(pageSize * (pageIndex - 1));
            List<User> users = userQuery.ToList();
            result.Items = users.Skip(skip).Take((int)pageSize).ToList();
            result.PageCount = pageCount;
            result.PageSize = pageSize;
            result.QueryCount = queryCount;
            result.CurrentPage = pageIndex;

            return View(new IndexViewModel()
            {
                Users = result
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
