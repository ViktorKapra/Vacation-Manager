using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using VacationManager.Models.ViewModel.TimeOffs;
using Data.Entity.TimeOffs;
using VacationManager.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace VacationManager.Controllers
{
    public class TimeOffsController : Controller
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly VacationManagerContext _context;

        private int FindUserId()
        {

            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            int userId = _context.Users.FirstOrDefault(u => u.UserName == userEmail).Id;
            return userId;
        }
        public TimeOffsController()
        { 
            _context = new VacationManagerContext();
        }
        // GET: TimeOffsController
        public ActionResult Index()
        {
            
            TimeOffsIndexVM model = new TimeOffsIndexVM();
            int currentUserId =  FindUserId();
            model.PaidTimeOffs = _context.PaidTimeOffs.Where(pto=> pto.RequestorId==currentUserId).ToList();
            model.UnpaidTimeOffs = _context.UnpaidTimeOffs.Where(pto => pto.RequestorId == currentUserId).ToList();
            model.SickTimeOffs = _context.SickTimeOffs.Where(pto => pto.RequestorId == currentUserId).ToList();

            return View(model);
        }

        // GET: TimeOffsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TimeOffsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TimeOffsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TimeOffsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TimeOffsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TimeOffsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TimeOffsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
