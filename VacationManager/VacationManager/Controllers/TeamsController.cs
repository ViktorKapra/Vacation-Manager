using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.Entity;
using System.Data.SqlTypes;
using VacationManager.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace VacationManager.Controllers
{
    [Authorize]
    public class TeamsController : Controller
    {
        private readonly VacationManagerContext _context;

        public TeamsController()
        {
            _context = new VacationManagerContext();
        }

        // GET: Teams
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vacationManagerContext = _context.Teams.Include(t => t.Project).Include(t => t.TeamLeader);
            ViewBag.UserRole = UserCredentialsHelper.FindUserRole(_context, User);
            return View(await vacationManagerContext.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Name" : "";
            ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Project" : "";
            ViewData["CurrentFilter"] = searchString;
            var Team = from s in _context.Teams
                       select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                Team = Team.Where(s => s.Name.Contains(searchString) || s.Project.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Name":
                    Team = Team.OrderByDescending(s => s.Name);
                    break;
                case "FirstName":
                    Team = Team.OrderBy(s => s.Project.Name);
                    break;
                default:
                    Team = Team.OrderBy(s => s.Name);
                    break;
            }
            return View(await Team.ToListAsync());
        }


        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(t => t.Project)
                .Include(t => t.TeamLeader)
                .FirstOrDefaultAsync(m => m.Id == id);
            List<string> developersNames= new List<string>();
            if (team == null)
            {
                return NotFound();
            }
            try
            {
                 developersNames = team.Developers.Select(dev => dev.FirstName).ToList();
            }
            catch(SqlNullValueException)
            {
                developersNames.Add(new string("There is no assigned developers"));
            }
            if (developersNames != null)
            {
                ViewBag.DevelopersNames = developersNames; 
            }

                return View(team);
           
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name");
            ViewData["TeamLeaderId"] = new SelectList(_context.Users, "Id", "FirstName");
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ProjectId,TeamLeaderId,Id")] Team team)
        {
            if (ModelState.IsValid)
            {
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", team.ProjectId);
            ViewData["TeamLeaderId"] = new SelectList(_context.Users, "Id", "FirstName", team.TeamLeaderId);
            return View(team);
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", team.ProjectId);
            ViewData["TeamLeaders"] = new SelectList(_context.Users, "Id", "FirstName", team.TeamLeaderId);
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,ProjectId,TeamLeaderId,Id")] Team team)
        {
            if (id != team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.Id))
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
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", team.ProjectId);
            ViewData["TeamLeaderId"] = new SelectList(_context.Users, "Id", "FirstName", team.TeamLeaderId);
            return View(team);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(t => t.Project)
                .Include(t => t.TeamLeader)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }
    }
}
