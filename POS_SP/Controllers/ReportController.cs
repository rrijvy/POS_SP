using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POS_SP.Data;
using POS_SP.Models;

namespace POS_SP.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Reports()
        {
            return View();
        }

        public IActionResult CurrentStockReport()
        {
            var products = _context.Stocks.Select(x => x.ProductId).Distinct();

            List<Stock> stocks = new List<Stock>();

            foreach (var item in products)
            {
                var stock = _context.Stocks
                    .Where(x => x.ProductId == item)
                    .Include(x => x.Product)
                    .OrderBy(x => x.StockDate)
                    .LastOrDefault();
                stocks.Add(stock);
            }
            return View(stocks);
        }

        [HttpGet]
        public IActionResult DateWiseStockReport()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult GetDateWiseStockReport(int id, DateTime fromDate, DateTime toDate)
        {
            ViewData["FromDate"] = fromDate.ToString("dd/MM/yyyy");
            ViewData["ToDate"] = toDate.ToString("dd/MM/yyyy");
            ViewData["Id"] = id;

            if (fromDate == DateTime.MinValue || toDate == DateTime.MinValue)
            {
                List<Stock> resultStock = _context.Stocks
                    .Where(x => x.ProductId == id)
                    .Include(x => x.Product)
                    .ToList();
                return View(resultStock);
            }
            List<Stock> stocks = _context.Stocks
                .Where(x => x.ProductId == id && x.StockDate >= fromDate && x.StockDate <= toDate)
                .Include(x => x.Product)
                .ToList();

            return View(stocks);
        }

        [HttpGet]
        public IActionResult DateWiseSalesReport()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetDateWiseReport(DateTime fromDate, DateTime toDate)
        {
            ViewData["FromDate"] = fromDate.ToString("dd/MM/yyyy");

            ViewData["ToDate"] = toDate.ToString("dd/MM/yyyy");

            List<Sale> sales = _context.Sales
                .Where(x => x.SalesDate >= fromDate && x.SalesDate <= toDate)
                .Include(x => x.Customer)
                .Include(x => x.SalesDetails)
                .ThenInclude(x => x.Product)
                .ToList();

            return View(sales);
        }

        [HttpGet]
        public IActionResult SalesClientWiseReport()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult GetClientWiseReport(int id, DateTime fromDate, DateTime toDate)
        {
            ViewData["FromDate"] = fromDate.ToString("dd/MM/yyyy");
            ViewData["ToDate"] = toDate.ToString("dd/MM/yyyy");
            ViewData["Id"] = id;

            if (fromDate == DateTime.MinValue || toDate == DateTime.MinValue)
            {
                List<Sale> resultSales = _context.Sales.Where(x => x.CustomerId == id)
                    .Include(x => x.Customer)
                    .Include(x => x.SalesDetails)
                    .ThenInclude(x => x.Product)
                    .ToList();
                return View(resultSales);
            }
            List<Sale> sales = _context.Sales
                .Where(x => x.CustomerId == id && x.SalesDate >= fromDate && x.SalesDate <= toDate)
                .Include(x => x.Customer)
                .Include(x => x.SalesDetails)
                .ThenInclude(x => x.Product)
                .ToList();
            return View(sales);
        }

        [HttpGet]
        public IActionResult SalesProductWiseReport()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult GetProductWiseReport(int id, DateTime fromDate, DateTime toDate)
        {
            ViewData["FromDate"] = fromDate.ToString("dd/MM/yyyy");
            ViewData["ToDate"] = toDate.ToString("dd/MM/yyyy");
            ViewData["Id"] = id;

            if (fromDate == DateTime.MinValue || toDate == DateTime.MinValue)
            {
                List<SalesDetail> resultSalesDetails = _context.SalesDetails
                    .Where(x => x.ProductId == id)
                    .Include(x => x.Sale)
                    .ThenInclude(x => x.Customer)
                    .ToList();
                return View(resultSalesDetails);
            }
            List<SalesDetail> salesDetails = _context.SalesDetails
                .Where(x => x.ProductId == id && x.Sale.SalesDate >= fromDate && x.Sale.SalesDate <= toDate)
                .Include(x => x.Sale)
                .ThenInclude(x => x.Customer)
                .ToList();
            return View(salesDetails);
        }

        [HttpGet]
        public IActionResult CategoryWiseSalesReport()
        {
            ViewData["SubCategoryId"] = new SelectList(_context.SubCategories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult GetCategoryWiseSalesReport(int id, DateTime fromDate, DateTime toDate)
        {
            ViewData["FromDate"] = fromDate.ToString("dd/MM/yyyy");
            ViewData["ToDate"] = toDate.ToString("dd/MM/yyyy");
            ViewData["Id"] = id;

            if (fromDate == DateTime.MinValue || toDate == DateTime.MinValue)
            {
                List<SalesDetail> resultSalesDetails = _context.SalesDetails
                    .Where(x => x.Product.SubCategoryId == id)
                    .Include(x => x.Product)
                    .Include(x => x.Sale)
                    .ThenInclude(x => x.Customer)
                    .ToList();
                return View(resultSalesDetails);
            }
            List<SalesDetail> salesDetails = _context.SalesDetails
                .Where(x => x.Product.SubCategoryId == id && x.Sale.SalesDate >= fromDate && x.Sale.SalesDate <= toDate)
                .Include(x => x.Product)
                .Include(x => x.Sale)
                .ThenInclude(x => x.Customer)
                .ToList();
            return View(salesDetails);
        }

        [HttpGet]
        public IActionResult PurchaseProductReport()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult GetProductWisePurchaseReport(int id, DateTime fromDate, DateTime toDate)
        {
            ViewData["FromDate"] = fromDate.ToString("dd/MM/yyyy");
            ViewData["ToDate"] = toDate.ToString("dd/MM/yyyy");
            ViewData["Id"] = id;

            if (fromDate == DateTime.MinValue || toDate == DateTime.MinValue)
            {
                List<PurchaseDetail> resultPurchaseDetails = _context.PurchaseDetails
                    .Where(x => x.ProductId == id)
                    .Include(x => x.Purchase)
                    .ThenInclude(x => x.Supplier)
                    .ToList();
                return View(resultPurchaseDetails);
            }
            List<PurchaseDetail> purchaseDetails = _context.PurchaseDetails
                .Where(x => x.ProductId == id && x.Purchase.PurchaseDate >= fromDate && x.Purchase.PurchaseDate <= toDate)
                .Include(x => x.Purchase)
                .ThenInclude(x => x.Supplier)
                .ToList();
            return View(purchaseDetails);
        }

        [HttpGet]
        public IActionResult SupplierWisePurchaseReport()
        {
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "CompanyName");
            return View();
        }

        [HttpPost]
        public IActionResult GetSupplierWisePurchaseReport(int id, DateTime fromDate, DateTime toDate)
        {
            ViewData["FromDate"] = fromDate.ToString("dd/MM/yyyy");
            ViewData["ToDate"] = toDate.ToString("dd/MM/yyyy");
            ViewData["Id"] = id;

            if (fromDate == DateTime.MinValue || toDate == DateTime.MinValue)
            {
                List<Purchase> resultPurchase = _context.Purchases.Where(x => x.SupplierId == id)
                    .Include(x => x.Supplier)
                    .Include(x => x.PurchaseDetails)
                    .ThenInclude(x => x.Product)
                    .ToList();
                return View(resultPurchase);
            }
            List<Purchase> purchases = _context.Purchases
                .Where(x => x.SupplierId == id && x.PurchaseDate >= fromDate && x.PurchaseDate <= toDate)
                .Include(x => x.Supplier)
                .Include(x => x.PurchaseDetails)
                .ThenInclude(x => x.Product)
                .ToList();
            return View(purchases);
        }

        [HttpGet]
        public IActionResult DateWisePurchaseReport()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetDateWisePurchaseReport(DateTime fromDate, DateTime toDate)
        {
            ViewData["FromDate"] = fromDate.ToString("dd/MM/yyyy");

            ViewData["ToDate"] = toDate.ToString("dd/MM/yyyy");

            List<Purchase> purchases = _context.Purchases
                .Where(x => x.PurchaseDate >= fromDate && x.PurchaseDate <= toDate)
                .Include(x => x.Supplier)
                .Include(x => x.PurchaseDetails)
                .ThenInclude(x => x.Product)
                .ToList();

            return View(purchases);
        }

        [HttpGet]
        public IActionResult CategoryWisePurchaseReport()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult GetCategoryWisePurchaseReport(int id, DateTime fromDate, DateTime toDate)
        {
            ViewData["FromDate"] = fromDate.ToString("dd/MM/yyyy");
            ViewData["ToDate"] = toDate.ToString("dd/MM/yyyy");
            ViewData["Id"] = id;

            if (fromDate == DateTime.MinValue || toDate == DateTime.MinValue)
            {
                List<PurchaseDetail> resultPurchaseDetails = _context.PurchaseDetails
                    .Where(x => x.Product.SubCategoryId == id)
                    .Include(x => x.Product)
                    .Include(x => x.Purchase)
                    .ThenInclude(x => x.Supplier)
                    .ToList();
                return View(resultPurchaseDetails);
            }
            List<PurchaseDetail> purchaseDetails = _context.PurchaseDetails
                .Where(x => x.Product.SubCategoryId == id && x.Purchase.PurchaseDate >= fromDate && x.Purchase.PurchaseDate <= toDate)
                .Include(x => x.Product)
                .Include(x => x.Purchase)
                .ThenInclude(x => x.Supplier)
                .ToList();
            return View(purchaseDetails);
        }
    }
}