using Domain.DataAccess;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Controllers
{
    public class AdminController : Controller
    {
        // GET: AdminController
        private readonly BooksDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AdminController(BooksDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public ActionResult Index()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login", "Account");
            }

            var userType = UserType.User;
            if (User.Identity.Name == null)
            {
                ViewData["UserType"] = 0;

            }
            else if (_userManager.FindByNameAsync(User.Identity.Name).Result != null)
            {
                ViewData["UserType"] = _userManager.FindByNameAsync(User.Identity.Name).Result.UserType;
                userType = _userManager.FindByNameAsync(User.Identity.Name).Result.UserType;
            }
            else
            {
                ViewData["UserType"] = 0;
                userType = UserType.User;
            }
            if (userType == UserType.Admin)
            {
                return View();
            }
            return RedirectToAction("Index", "Books");
        }
    }
}
