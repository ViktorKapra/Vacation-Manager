using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using VacationManager.Models.ViewModel.TimeOffs;
using Data.Entity.TimeOffs;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using VacationManager.Helpers;
using Data.Entity;

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

        // GET: Users/Create
        public IActionResult CreatePaid()
        {
            return View();
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePaid([Bind("StartDate,EndDate,IsHalfDay,IsApproved,Id")] PaidTimeOff paidTimeOff)
        {
            currentUserId = UserCredentialsHelper.FindUserId(_context, User);
            paidTimeOff.Requestor = _context.Users.FirstOrDefault(u => u.Id == currentUserId);
            paidTimeOff.RequestorId = currentUserId;
            paidTimeOff.IsApproved = false;
            paidTimeOff.CreationDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(paidTimeOff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(paidTimeOff);
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

        public IActionResult CreateUnpaid()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUnpaid([Bind("StartDate,EndDate,IsHalfDay,IsApproved,Id")] UnpaidTimeOff unpaidTimeOff)
        {
            currentUserId = UserCredentialsHelper.FindUserId(_context, User);
            unpaidTimeOff.Requestor = _context.Users.FirstOrDefault(u => u.Id == currentUserId);
            unpaidTimeOff.RequestorId = currentUserId;
            unpaidTimeOff.IsApproved = false;
            unpaidTimeOff.CreationDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(unpaidTimeOff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(unpaidTimeOff);
        }
        // GET: TimeOffsController/Edit/5
        public async Task<IActionResult> EditUnpaid(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var timeOff = await _context.UnpaidTimeOffs.FindAsync(id);
            if (timeOff == null)
            {
                return NotFound();
            }
            return View(timeOff);
        }

        // POST: TimeOffsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUnpaidAsync(int id, [Bind("StartDate,EndDate,IsHalfDay,IsApproved,Id")] UnpaidTimeOff unpaidTimeOff)
        {
            currentUserId = UserCredentialsHelper.FindUserId(_context, User);
            unpaidTimeOff.Requestor = _context.Users.FirstOrDefault(u => u.Id == currentUserId);
            unpaidTimeOff.RequestorId = currentUserId;
            unpaidTimeOff.IsApproved = false;
            unpaidTimeOff.CreationDate = DateTime.Now;

            if (id != unpaidTimeOff.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.UnpaidTimeOffs.Update(unpaidTimeOff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaidTimeOffExists(unpaidTimeOff.Id))
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
        public async Task<ActionResult> DeleteUnpaid(int id)
        {
            var timeOff = await _context.UnpaidTimeOffs.FindAsync(id);
            if (timeOff == null)
            {
                return NotFound();
            }
            _context.UnpaidTimeOffs.Remove(timeOff);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
         private bool UnpaidTimeOffExists(int id)
        {
            return _context.UnpaidTimeOffs.Any(e => e.Id == id);
        }

        public IActionResult CreateSick()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSick([Bind("StartDate,EndDate,IsApproved")] SickTimeOff sickTimeOff)
        {
            currentUserId = UserCredentialsHelper.FindUserId(_context, User);
            sickTimeOff.Requestor = _context.Users.FirstOrDefault(u => u.Id == currentUserId);
            sickTimeOff.RequestorId = currentUserId;
            sickTimeOff.IsApproved = false;
            sickTimeOff.CreationDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(sickTimeOff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(sickTimeOff);
        }
        // GET: TimeOffsController/Edit/5
        public async Task<IActionResult> EditSick(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var timeOff = await _context.SickTimeOffs.FindAsync(id);
            if (timeOff == null)
            {
                return NotFound();
            }
            return View(timeOff);
        }

        // POST: TimeOffsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditSick(int id, [Bind("StartDate,EndDate,IsApproved,Id")] SickTimeOff sickTimeOff)
        {
            currentUserId = UserCredentialsHelper.FindUserId(_context, User);
            sickTimeOff.Requestor = _context.Users.FirstOrDefault(u => u.Id == currentUserId);
            sickTimeOff.RequestorId = currentUserId;
            sickTimeOff.IsApproved = false;
            sickTimeOff.CreationDate = DateTime.Now;

            if (id != sickTimeOff.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.SickTimeOffs.Update(sickTimeOff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaidTimeOffExists(sickTimeOff.Id))
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
        public async Task<ActionResult> DeleteSick(int id)
        {
            var timeOff = await _context.SickTimeOffs.FindAsync(id);
            if (timeOff == null)
            {
                return NotFound();
            }
            _context.SickTimeOffs.Remove(timeOff);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        private bool SickTimeOffExists(int id)
        {
            return _context.SickTimeOffs.Any(e => e.Id == id);
        }

        public ActionResult AllRequests()
        {
            currentUserId = UserCredentialsHelper.FindUserId(_context, User);
           string currentUserRole = UserCredentialsHelper.FindUserRole(_context, User);
            AllRequestsVM model = new AllRequestsVM();
            if (currentUserRole == "CEO")
            {
                model.PaidTimeOffs = _context.PaidTimeOffs.ToList();
                model.UnpaidTimeOffs = _context.UnpaidTimeOffs.ToList();
                model.SickTimeOffs = _context.SickTimeOffs.ToList();
            }
            else 
            {
                var team = _context.Teams.Where(t => t.TeamLeaderId == currentUserId);
                if(team!=null)
                {
                    var developers = _context.Users.Where(u => u.TeamId == team.First().Id).ToList();
                    foreach(var developer in developers)
                    {
                        model.PaidTimeOffs = _context.PaidTimeOffs.Where(timeoff => timeoff.RequestorId == developer.Id).ToList();
                        model.UnpaidTimeOffs = _context.UnpaidTimeOffs.Where(timeoff => timeoff.RequestorId == developer.Id).ToList();
                        model.SickTimeOffs = _context.SickTimeOffs.Where(timeoff => timeoff.RequestorId == developer.Id).ToList();
                    }
                } 
            }
            return View(model);
        }

       public async Task<ActionResult> ApprovePaid(int id)
        {
            var timeOff = await _context.PaidTimeOffs.FindAsync(id);
            timeOff.IsApproved = true;
            _context.Update(timeOff);
            _context.SaveChanges();
            return RedirectToAction("AllRequests");
        }
        public async Task<ActionResult> ApproveUnpaid(int id)
        {

            var timeOff = await _context.UnpaidTimeOffs.FindAsync(id);
            timeOff.IsApproved = true;
            _context.Update(timeOff);
            _context.SaveChanges();
            return RedirectToAction("AllRequests");
        }
        public async Task<ActionResult> ApproveSick(int id)
        {
            var timeOff = await _context.SickTimeOffs.FindAsync(id);
            timeOff.IsApproved = true;
            _context.Update(timeOff);
            _context.SaveChanges();
            return RedirectToAction("AllRequests");
        }
    }
}
