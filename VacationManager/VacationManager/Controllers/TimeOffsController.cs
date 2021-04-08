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
using Microsoft.EntityFrameworkCore;
using VacationManager.Helpers;

namespace VacationManager.Controllers
{
    public class TimeOffsController : Controller
    {

        private readonly VacationManagerContext _context;
        private int currentUserId;

       
        public TimeOffsController()
        { 
            _context = new VacationManagerContext();

        }
        // GET: TimeOffsController
        public ActionResult Index()
        {
           
            currentUserId = UserCredentialsHelper.FindUserId(_context,User);
            TimeOffsIndexVM model = new TimeOffsIndexVM();
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
        public async Task<IActionResult> EditPaid(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeOff = await _context.PaidTimeOffs.FindAsync(id);
            if (timeOff == null)
            {
                return NotFound();
            }

            return View(timeOff);
        }

        // POST: TimeOffsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPaidAsync(int id, [Bind("StartDate,EndDate,IsHalfDay,IsApproved,Id")] PaidTimeOff paidTimeOff)
        {
            currentUserId = UserCredentialsHelper.FindUserId(_context,User);
            paidTimeOff.Requestor = _context.Users.FirstOrDefault(u => u.Id == currentUserId);
            paidTimeOff.RequestorId = currentUserId;
            paidTimeOff.IsApproved = false;
            paidTimeOff.CreationDate = DateTime.Now;

            if (id != paidTimeOff.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.PaidTimeOffs.Update(paidTimeOff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaidTimeOffExists(paidTimeOff.Id))
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

            return RedirectToAction("Index");
        }

        // POST: TimeOffsController/Delete/5

        public async  Task<ActionResult> DeletePaid(int id)
        {   
            var timeOff = await _context.PaidTimeOffs.FindAsync(id);
            if(timeOff==null)
            {
                return NotFound();
            }
            _context.PaidTimeOffs.Remove(timeOff);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PaidTimeOffExists(int id)
        {
            return _context.PaidTimeOffs.Any(e => e.Id == id);
        }
    }
}
