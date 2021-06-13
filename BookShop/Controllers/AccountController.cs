using BookShop.ViewModels.Account;
using Domain.DataAccess;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly BooksDbContext _context;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, BooksDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        [HttpGet]
        public IActionResult Register()
        {
            var userType = UserType.User;

            if (User.Identity.Name != null)
            {
                if (_userManager.FindByNameAsync(User.Identity.Name).Result != null)
                {
                    userType = _userManager.FindByNameAsync(User.Identity.Name).Result.UserType;
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            var userType = UserType.User;

            if (User.Identity.Name != null)
            {
                if (_userManager.FindByNameAsync(User.Identity.Name).Result != null)
                {
                    userType = _userManager.FindByNameAsync(User.Identity.Name).Result.UserType;
                }
            }
            if (ModelState.IsValid)
            {

                User user = new User { Email = model.Email, UserName = model.Email, UserType = UserType.User};
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Books");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            var userType = UserType.User;

            if (User.Identity.Name != null)
            {
                if (_userManager.FindByNameAsync(User.Identity.Name).Result != null)
                {
                    userType = _userManager.FindByNameAsync(User.Identity.Name).Result.UserType;
                }
            }
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var userType = UserType.User;

            if (User.Identity.Name != null)
            {
                if (_userManager.FindByNameAsync(User.Identity.Name).Result != null)
                {
                    userType = _userManager.FindByNameAsync(User.Identity.Name).Result.UserType;
                }
            }
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Books");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {

            var userType = UserType.User;

            if (User.Identity.Name != null)
            {
                if (_userManager.FindByNameAsync(User.Identity.Name).Result != null)
                {
                    userType = _userManager.FindByNameAsync(User.Identity.Name).Result.UserType;
                }
            }
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Books");
        }

        [Authorize]
        public async Task<IActionResult> AddCash()
        {
            var userType = UserType.User;

            if (User.Identity.Name != null)
            {
                if (_userManager.FindByNameAsync(User.Identity.Name).Result != null)
                {
                    userType = _userManager.FindByNameAsync(User.Identity.Name).Result.UserType;
                }
            }

            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            user.Cash += 2500;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Books");
        }

        [Authorize]
        public IActionResult UserBooks()
        {

            var userType = UserType.User;

            if (User.Identity.Name != null)
            {
                if (_userManager.FindByNameAsync(User.Identity.Name).Result != null)
                {
                    userType = _userManager.FindByNameAsync(User.Identity.Name).Result.UserType;

                }
            }

            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            var bookUser = _context.BookUser.Where(x => x.UserId == Guid.Parse(user.Id)).ToList();

            var bookList = new List<Book>();

            foreach (var item in bookUser)
            {
                bookList.Add(_context.Books.FirstOrDefault(x => x.Id == item.BookId));
            }
            return View(bookList);
        }
    }
}
