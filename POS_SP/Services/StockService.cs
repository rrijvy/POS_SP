using POS_SP.Data;
using POS_SP.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace POS_SP.Services
{
    public class StockService : IStockService
    {
        private readonly ApplicationDbContext _context;

        public StockService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddStockDetail(string remarks, SalesDetail salesDetail)
        {
            List<StockDetail> stockDetails = _context.StockDetails.Where(x => x.ProductId == salesDetail.ProductId).ToList();

            StockDetail lastEntry = stockDetails.OrderByDescending(x => x.EntryTime).FirstOrDefault();

            StockDetail stockDetail = new StockDetail
            {
                EntryTime = DateTime.Now.Date,
                ProductId = salesDetail.ProductId,
                Quantity = -salesDetail.Quantity,
                RefId = 5,
                Remarks = remarks,
                StockId = lastEntry.StockId,
                TRSDate = DateTime.Now.Date,
                TRSNo = "TRS-" + DateTime.UtcNow.Millisecond,
                UnitPrice = salesDetail.UnitPrice
            };
            _context.StockDetails.Add(stockDetail);
            _context.SaveChanges();
        }

        public void AddStockDetail(string remarks, PurchaseDetail purchaseOrderDetail)
        {
            List<StockDetail> stockDetails = _context.StockDetails.Where(x => x.ProductId == purchaseOrderDetail.ProductId).ToList();

            StockDetail lastEntry = stockDetails.OrderByDescending(x => x.EntryTime).FirstOrDefault();

            Stock stock = _context.Stocks.Where(x => x.ProductId == purchaseOrderDetail.ProductId).OrderBy(x => x.StockDate).LastOrDefault();

            if (lastEntry == null)
            {
                StockDetail stockDetail = new StockDetail
                {
                    EntryTime = DateTime.Now.Date,
                    ProductId = purchaseOrderDetail.ProductId,
                    Quantity = purchaseOrderDetail.Quantity,
                    RefId = 5,
                    Remarks = remarks,
                    StockId = stock.Id,
                    TRSDate = DateTime.Now.Date,
                    TRSNo = "50000",
                    UnitPrice = purchaseOrderDetail.UnitPrice
                };
                _context.StockDetails.Add(stockDetail);
                _context.SaveChanges();
            }
            else
            {
                StockDetail stockDetail = new StockDetail
                {
                    EntryTime = DateTime.Now.Date,
                    ProductId = purchaseOrderDetail.ProductId,
                    Quantity = purchaseOrderDetail.Quantity,
                    RefId = 5,
                    Remarks = remarks,
                    StockId = lastEntry.StockId,
                    TRSDate = DateTime.Now.Date,
                    TRSNo = "50000",
                    UnitPrice = purchaseOrderDetail.UnitPrice
                };
                _context.StockDetails.Add(stockDetail);
                _context.SaveChanges();
            }

        }

        public void StockMaintain(DateTime dateTime, SalesDetail salesDetail)
        {
            List<Stock> stocks = _context.Stocks.Where(x => x.ProductId == salesDetail.ProductId).ToList();

            Stock lastEntry = stocks.OrderByDescending(x => x.StockDate).FirstOrDefault();

            if (lastEntry.StockDate.ToString("dd/MM/yyyy") == dateTime.ToString("dd/MM/yyyy"))
            {
                lastEntry.Quantity -= salesDetail.Quantity;
                _context.Stocks.Update(lastEntry);
                _context.SaveChanges();
            }
            else
            {
                Stock newEntry = new Stock
                {
                    ProductId = salesDetail.ProductId,
                    Quantity = lastEntry.Quantity - salesDetail.Quantity,
                    StockDate = dateTime,
                    UOM = lastEntry.UOM
                };
                _context.Add(newEntry);
                _context.SaveChanges();
            }
        }

        public void StockMaintain(DateTime dateTime, PurchaseDetail purchaseOrderDetail)
        {
            List<Stock> stocks = _context.Stocks.Where(x => x.ProductId == purchaseOrderDetail.ProductId).ToList();

            Stock lastEntry = stocks.OrderByDescending(x => x.StockDate).FirstOrDefault();
            if (lastEntry == null)
            {
                Stock newEntry = new Stock
                {
                    ProductId = purchaseOrderDetail.ProductId,
                    Quantity = purchaseOrderDetail.Quantity,
                    StockDate = dateTime,
                    UOM = purchaseOrderDetail.UOM
                };
                _context.Add(newEntry);
                _context.SaveChanges();
            }
            else
            {
                if (lastEntry.StockDate.ToString("dd/MM/yyyy") == dateTime.ToString("dd/MM/yyyy"))
                {
                    lastEntry.Quantity += purchaseOrderDetail.Quantity;
                    _context.Stocks.Update(lastEntry);
                    _context.SaveChanges();
                }
                else
                {
                    Stock newEntry = new Stock
                    {
                        ProductId = purchaseOrderDetail.ProductId,
                        Quantity = lastEntry.Quantity + purchaseOrderDetail.Quantity,
                        StockDate = dateTime,
                        UOM = lastEntry.UOM
                    };
                    _context.Add(newEntry);
                    _context.SaveChanges();
                }
            }

        }
    }
}
