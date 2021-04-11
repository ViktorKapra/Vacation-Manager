using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.Entity;
using VacationManager.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace VacationManager.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly VacationManagerContext _context;

        public UsersController()
        {
            _context = new VacationManagerContext();
        }

        // GET: Users
       [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vacationManagerContext = _context.Users.Include(u => u.Role).Include(u => u.Team);
            ViewBag.UserRole = UserCredentialsHelper.FindUserRole(_context,User);

            return View(await vacationManagerContext.ToListAsync());
        }
       [HttpPost]
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "LastName" : "";
            ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "FirstName" : "";
            ViewData["RoleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "RoleName" : "";
            ViewData["UserNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "UserName" : "";
            ViewData["CurrentFilter"] = searchString;
            var user = from s in _context.Users
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                user = user.Where(s => s.LastName.Contains(searchString) || s.FirstName.Contains(searchString) || s.Role.Name.Contains(searchString)|| s.UserName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "LastName":
                    user = user.OrderByDescending(s => s.LastName);
                    break;
                case "FirstName":
                    user = user.OrderBy(s => s.FirstName);
                    break;
                case "RoleName":
                    user = user.OrderBy(s => s.Role.Name);
                    break;
                case "UserName":
                    user = user.OrderBy(s => s.UserName);
                    break;


                default:
                    user = user.OrderBy(s => s.UserName);
                    break;
            }
            return View(await user.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserName,Password,FirstName,LastName,RoleId,TeamId,Id")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", user.RoleId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", user.TeamId);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", user.RoleId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", user.TeamId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserName,Password,FirstName,LastName,RoleId,TeamId,Id")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", user.RoleId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", user.TeamId);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
