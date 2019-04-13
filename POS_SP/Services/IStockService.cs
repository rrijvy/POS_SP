using POS_SP.Models;
using System;

namespace POS_SP.Services
{
    public interface IStockService
    {
        void StockMaintain(DateTime dateTime, SalesDetail salesDetail);
        void AddStockDetail(string remarks, SalesDetail salesDetail);

        void StockMaintain(DateTime dateTime, PurchaseDetail purchaseDetail);
        void AddStockDetail(string remarks, PurchaseDetail purchaseDetail);
    }
}
