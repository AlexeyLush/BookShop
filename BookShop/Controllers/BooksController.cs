using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookShop.ViewModels.Book;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.DataAccess;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace BookShop.Controllers
{
    public class BooksController : Controller
    {
        private readonly BooksDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public BooksController(BooksDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            if (_userManager.FindByEmailAsync("admin@admin.com").Result == null)
            {
                User user = new User { Email = "admin@admin.com", UserName = "admin@admin.com", UserType = UserType.Admin };
                await _userManager.CreateAsync(user, "Admin@375");
            }
            return View(await _context.Books.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {

            if (id == null)
            {
                return NotFound();
            }


            var book = await _context.Books
                .FirstOrDefaultAsync(m => id == m.Id);


            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {

            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login", "Account");
            }

            var userType = UserType.User;

            if (User.Identity.Name != null)
            {
                if (_userManager.FindByNameAsync(User.Identity.Name).Result != null)
                {
                    userType = _userManager.FindByNameAsync(User.Identity.Name).Result.UserType;
                }
            }

            if (userType == UserType.User)
            {
                return RedirectToAction("Index");
            }
            ViewData["Genres"] = _context.Genres.ToList();
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookViewModel book)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login", "Account");
            }
            var userType = UserType.User;

            if (User.Identity.Name != null)
            {
                if (_userManager.FindByNameAsync(User.Identity.Name).Result != null)
                {
                    userType = _userManager.FindByNameAsync(User.Identity.Name).Result.UserType;
                }
            }
            if (userType == UserType.User)
            {
                return RedirectToAction("Index");
            }
            ViewData["Genres"] = _context.Genres.ToList();
            if (ModelState.IsValid)
            {
                var bookDb = new Book()
                {
                    Author = book.Author,
                    Description = book.Description,
                    GenreId = _context.Genres.FirstOrDefault(x => x.Id == book.GenreId).Id,
                    Title = book.Title,
                    Price = book.Price,
                    Year = book.Year
                };

                if (book.Photo != null)
                {
                    byte[] imageData;
                    using (var binaryReader = new BinaryReader(book.Photo.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)book.Photo.Length);
                    }

                    bookDb.Photo = imageData;
                }

                await _context.Books.AddAsync(bookDb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {

            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login", "Account");
            }

            var userType = UserType.User;

            if (User.Identity.Name != null)
            {
                if (_userManager.FindByNameAsync(User.Identity.Name).Result != null)
                {
                    userType = _userManager.FindByNameAsync(User.Identity.Name).Result.UserType;
                }
            }

            if (userType == UserType.User)
            {
                return RedirectToAction("Index");
            }
            ViewData["Genres"] = _context.Genres.ToList();
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var bookVM = new BookViewModel()
            {
                Id = book.Id,
                Author = book.Author,
                Description = book.Description,
                GenreId = book.GenreId,
                Title = book.Title,
                Price = book.Price,
                Year = book.Year
            };
            return View(bookVM);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BookViewModel book)
        {

            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login", "Account");
            }
            var userType = UserType.User;

            if (User.Identity.Name != null)
            {
                if (_userManager.FindByNameAsync(User.Identity.Name).Result != null)
                {
                    userType = _userManager.FindByNameAsync(User.Identity.Name).Result.UserType;
                }
            }
            if (userType == UserType.User)
            {
                return RedirectToAction("Index");
            }
            ViewData["Genres"] = _context.Genres.ToList();
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {
                    var bookDb = _context.Books.First(x => x.Id == book.Id);
                    if (book.Photo != null)
                    {
                        byte[] imageData;
                        using (var binaryReader = new BinaryReader(book.Photo.OpenReadStream()))
                        {
                            imageData = binaryReader.ReadBytes((int)book.Photo.Length);
                        }
                        bookDb.Photo = imageData;
                    }

                    bookDb.Id = book.Id;
                    bookDb.Author = book.Author;
                    bookDb.Description = book.Description;
                    bookDb.GenreId = _context.Genres.First(x => x.Id == book.GenreId).Id;
                    bookDb.Title = book.Title;
                    bookDb.Year = book.Year;
                    bookDb.Price = book.Price;

                    _context.Books.Update(bookDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {

            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login", "Account");
            }
            var userType = UserType.User;

            if (User.Identity.Name != null)
            {
                if (_userManager.FindByNameAsync(User.Identity.Name).Result != null)
                {
                    userType = _userManager.FindByNameAsync(User.Identity.Name).Result.UserType;
                }
            }
            if (userType == UserType.User)
            {
                return RedirectToAction("Index");
            }
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(Guid id)
        {
            return _context.Books.Any(e => e.Id == id);
        }


        public async Task<IActionResult> Buy(Guid Id)
        {

            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login", "Account");
            }

            ViewData["Error"] = "";
            var book = _context.Books.Find(Id);
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            if (_context.BookUser.Where(x => x.UserId == Guid.Parse(user.Id)).FirstOrDefault(x => x.BookId == book.Id) == null)
            {
                var tempCash = user.Cash - book.Price;
                if (tempCash >= 0)
                {
                    user.Cash = tempCash;

                    var bookUser = new BookUser()
                    {
                        UserId = Guid.Parse(user.Id),
                        BookId = book.Id
                    };

                    _context.Users.Update(user);
                    _context.BookUser.Add(bookUser);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Books");
                }
                else
                {
                    ViewData["Error"] = "У вас недостаточно денег";
                    return View("Details", book);
                }
            }

            return View("Details", book);
        }
    }
}
