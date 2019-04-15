using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POS_SP.Data;
using POS_SP.Models;
using POS_SP.Services;

namespace POS_SP.Controllers
{
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStockService _stockService;

        public SalesController(ApplicationDbContext context, IStockService stockService)
        {
            _context = context;
            _stockService = stockService;
        }
        
        public async Task<IActionResult> Index(string currentFilter, string searching, int? page)
        {
            if (searching != null)
            {
                page = 1;
            }
            else
            {
                searching = currentFilter;
            }
            ViewData["currentFilter"] = searching;

            var sale = _context.Sales
                .Include(s => s.Customer)
                .Include(x => x.SalesDetails)
                .ThenInclude(x => x.Product)
                .OrderByDescending(x => x.SalesDate)
                .AsQueryable();

            if (!String.IsNullOrEmpty(searching))
            {
                sale = sale.Where(e => e.OrderRefNo.Contains(searching) || e.Customer.Name.Contains(searching));
            }

            int pageSize = 10;
            return View(await PaginatedList<Sale>.CreateAsync(sale.AsNoTracking(), page ?? 1, pageSize));
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .Include(s => s.Customer)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }
        
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Set<Customer>(), "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Sale model)
        {
            var resultExist = _context.Sales.Where(x => x.OrderRefNo == model.OrderRefNo).FirstOrDefault();

            if (resultExist == null)
            {
                Sale sale = new Sale
                {
                    OrderNumber = "S-" + DateTime.UtcNow.Millisecond,
                    OrderRefNo = model.OrderRefNo,
                    SalesDate = model.SalesDate,
                    CustomerId = model.CustomerId,
                    TaxPercent = model.TaxPercent,
                    TaxAmount = model.TaxAmount,
                    VatPercent = model.VatPercent,
                    VatAmount = model.VatAmount,
                    DiscountPercent = model.DiscountPercent,
                    DiscountAmount = model.DiscountAmount,
                    PaymentType = model.PaymentType,
                    DownPayment = model.DownPayment,
                    PaymentAmount = model.PaymentAmount,
                    DueAmount = model.DueAmount,
                    InstallmentPeriod = model.InstallmentPeriod,
                    TotalAmount = model.TotalAmount
                };
                _context.Sales.Add(sale);
                _context.SaveChanges();
                foreach (var item in model.SalesDetails)
                {
                    SalesDetail salesDetail = new SalesDetail
                    {
                        SaleId = sale.Id,
                        ProductId = item.ProductId,
                        UnitPrice = item.UnitPrice,
                        UOM = item.UOM,
                        Quantity = item.Quantity,
                        IndividualTotal = item.IndividualTotal
                    };
                    _context.SalesDetails.Add(salesDetail);
                    _context.SaveChanges();

                    _stockService.StockMaintain(DateTime.Now.Date, item);
                    _stockService.AddStockDetail("Sales", item);
                }
                foreach (var item in model.InstallmentSchedulePayments)
                {
                    InstallmentSchedulePayment installment = new InstallmentSchedulePayment
                    {
                        ScheduleDate = item.ScheduleDate,
                        ScheduleAmount = item.ScheduleAmount,
                        PaymentDate = DateTime.MinValue,
                        PaidAmount = 0,
                        FineAmount = 0,
                        DueAmount = item.ScheduleAmount,
                        TotalPaid = sale.DownPayment,
                        TotalDue = sale.TotalAmount - sale.DownPayment,
                        SalesId = sale.Id
                    };
                    _context.InstallmentSchedulePayments.Add(installment);
                    _context.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales.SingleOrDefaultAsync(m => m.Id == id);
            if (sale == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Set<Customer>(), "Id", "Address1", sale.CustomerId);
            return View(sale);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderNumber,OrderRefNo,SalesDate,TaxAmount,TaxPercent,VatAmount,VatPercent,DiscountAmount,DiscountPercent,PaymentType,PaymentAmount,DueAmount,TotalAmount,CustomerId")] Sale sale)
        {
            if (id != sale.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleExists(sale.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Set<Customer>(), "Id", "Address1", sale.CustomerId);
            return View(sale);
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .Include(s => s.Customer)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sale = await _context.Sales.SingleOrDefaultAsync(m => m.Id == id);
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleExists(int id)
        {
            return _context.Sales.Any(e => e.Id == id);
        }
    }
}
