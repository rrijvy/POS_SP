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
    public class PurchasesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStockService _stockService;

        public PurchasesController(ApplicationDbContext context, IStockService stockService)
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

            var purchase = _context.Purchases
                .Include(s => s.Supplier)
                .Include(x => x.PurchaseDetails)
                .ThenInclude(x => x.Product)
                .OrderByDescending(x => x.PurchaseDate)
                .AsQueryable();

            if (!String.IsNullOrEmpty(searching))
            {
                purchase = purchase.Where(e => e.OrderRefNo.Contains(searching) || e.Supplier.CompanyName.Contains(searching));
            }

            int pageSize = 10;

            return View(await PaginatedList<Purchase>.CreateAsync(purchase.AsNoTracking(), page ?? 1, pageSize));
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases
                .Include(p => p.Supplier)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }
        
        public IActionResult Create()
        {
            ViewData["SupplierId"] = new SelectList(_context.Set<Supplier>(), "Id", "CompanyName");
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Purchase model)
        {
            var resultExist = _context.Purchases.Where(x => x.OrderRefNo == model.OrderRefNo).FirstOrDefault();

            if (resultExist == null)
            {
                Purchase purchase = new Purchase
                {
                    OrderNo = "P-" + DateTime.UtcNow.Millisecond,
                    OrderRefNo = model.OrderRefNo,
                    PurchaseDate = model.PurchaseDate,
                    SupplierId = model.SupplierId,
                    TaxPercent = model.TaxPercent,
                    TaxAmount = model.TaxAmount,
                    VatPercent = model.VatPercent,
                    VatAmount = model.VatAmount,
                    DiscountPercent = model.DiscountPercent,
                    DiscountAmount = model.DiscountAmount,
                    PaymentType = model.PaymentType,
                    PaymentAmount = model.PaymentAmount,
                    DueAmount = model.DueAmount,
                    TotalAmount = model.TotalAmount
                };
                _context.Purchases.Add(purchase);
                _context.SaveChanges();
                foreach (var item in model.PurchaseDetails)
                {
                    PurchaseDetail purchaseDetail = new PurchaseDetail
                    {
                        PurchaseId = purchase.Id,
                        ProductId = item.ProductId,
                        UnitPrice = item.UnitPrice,
                        UOM = item.UOM,
                        Quantity = item.Quantity,
                        IndividualTotal = item.IndividualTotal
                    };
                    _context.PurchaseDetails.Add(purchaseDetail);
                    _context.SaveChanges();

                    _stockService.StockMaintain(DateTime.Now.Date, item);
                    _stockService.AddStockDetail("Purchase", item);
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

            var purchase = await _context.Purchases.SingleOrDefaultAsync(m => m.Id == id);
            if (purchase == null)
            {
                return NotFound();
            }
            ViewData["SupplierId"] = new SelectList(_context.Set<Supplier>(), "Id", "Address1", purchase.SupplierId);
            return View(purchase);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PurchaseDate,OrderRefNo,SupplierId,OrderNo,LoadingBill,LabourCost,TaxPercent,TaxAmount,VatPercent,VatAmount,DiscountPercent,DiscountAmount,PaymentType,PaymentAmount,DueAmount,TotalAmount")] Purchase purchase)
        {
            if (id != purchase.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseExists(purchase.Id))
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
            ViewData["SupplierId"] = new SelectList(_context.Set<Supplier>(), "Id", "Address1", purchase.SupplierId);
            return View(purchase);
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases
                .Include(p => p.Supplier)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchase = await _context.Purchases.SingleOrDefaultAsync(m => m.Id == id);
            _context.Purchases.Remove(purchase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseExists(int id)
        {
            return _context.Purchases.Any(e => e.Id == id);
        }
    }
}
