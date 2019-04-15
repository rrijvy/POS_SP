using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POS_SP.Data;
using POS_SP.Models;

namespace POS_SP.Controllers
{
    public class InstallmentSchedulePaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstallmentSchedulePaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InstallmentSchedulePayments
        public async Task<IActionResult> Index()
        {
            return View(await _context.InstallmentSchedulePayments.ToListAsync());
        }

        // GET: InstallmentSchedulePayments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var installmentSchedulePayment = await _context.InstallmentSchedulePayments
                .SingleOrDefaultAsync(m => m.Id == id);
            if (installmentSchedulePayment == null)
            {
                return NotFound();
            }

            return View(installmentSchedulePayment);
        }

        // GET: InstallmentSchedulePayments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InstallmentSchedulePayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ScheduleDate,ScheduleAmount,PaymentDate,PaidAmount,FineAmount,DueAmount,TotalPaid,TotalDue,SalesId")] InstallmentSchedulePayment installmentSchedulePayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(installmentSchedulePayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(installmentSchedulePayment);
        }

        // GET: InstallmentSchedulePayments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var installmentSchedulePayment = await _context.InstallmentSchedulePayments.SingleOrDefaultAsync(m => m.Id == id);
            if (installmentSchedulePayment == null)
            {
                return NotFound();
            }
            return View(installmentSchedulePayment);
        }

        // POST: InstallmentSchedulePayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ScheduleDate,ScheduleAmount,PaymentDate,PaidAmount,FineAmount,DueAmount,TotalPaid,TotalDue,SalesId")] InstallmentSchedulePayment installmentSchedulePayment)
        {
            if (id != installmentSchedulePayment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(installmentSchedulePayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstallmentSchedulePaymentExists(installmentSchedulePayment.Id))
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
            return View(installmentSchedulePayment);
        }

        // GET: InstallmentSchedulePayments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var installmentSchedulePayment = await _context.InstallmentSchedulePayments
                .SingleOrDefaultAsync(m => m.Id == id);
            if (installmentSchedulePayment == null)
            {
                return NotFound();
            }

            return View(installmentSchedulePayment);
        }

        // POST: InstallmentSchedulePayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var installmentSchedulePayment = await _context.InstallmentSchedulePayments.SingleOrDefaultAsync(m => m.Id == id);
            _context.InstallmentSchedulePayments.Remove(installmentSchedulePayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstallmentSchedulePaymentExists(int id)
        {
            return _context.InstallmentSchedulePayments.Any(e => e.Id == id);
        }

        [HttpGet]
        public IActionResult FindSchedule()
        {
            return View();
        }
        
        public IActionResult UpdateSchedule(int id)
        {
            var result = _context.InstallmentSchedulePayments
                .Where(x => x.SalesId == id)
                .Include(x => x.Sale)
                .ToList();

            if (result == null)
            {
                return RedirectToAction(nameof(FindSchedule));
            }

            return View(result);
        }
    }
}
