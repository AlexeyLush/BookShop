using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.DataAccess;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace BookShop.Controllers
{
    public class GenresController : Controller
    {
        private readonly BooksDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public GenresController(BooksDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Genres
        public async Task<IActionResult> Index()
        {
            return View(await _context.Genres.ToListAsync());
        }



        // GET: Genres/Create
        public IActionResult Create()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] Genre genre)
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

            if (userType == UserType.Admin)
            {
                if (ModelState.IsValid)
                {
                    genre.Id = Guid.NewGuid();
                    _context.Add(genre);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(genre);
            }
            else
            {
                return RedirectToAction("Index", "Books");
            }


        }

        // GET: Genres/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        // POST: Genres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title")] Genre genre)
        {
            if (id != genre.Id)
            {
                return NotFound();
            }

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

            if (userType == UserType.Admin)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(genre);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!GenreExists(genre.Id))
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
                return View(genre);
            }
            else
            {
                return RedirectToAction("Index", "Books");
            }


        }

        // GET: Genres/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres.FindAsync(id);
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenreExists(Guid id)
        {
            return _context.Genres.Any(e => e.Id == id);
        }
    }
}
