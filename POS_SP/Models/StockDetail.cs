using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_SP.Models
{
    public class StockDetail
    {
        public int Id { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "Transaction Date")]
        public DateTime TRSDate { get; set; }

        [Display(Name = "Transaction No")]
        public string TRSNo { get; set; }

        [Display(Name = "Reference No")]
        public int RefId { get; set; }

        [Display(Name = "Stock")]
        public int StockId { get; set; }

        [Display(Name = "Product")]
        public int ProductId { get; set; }

        [Display(Name = "Unit Price")]
        public double UnitPrice { get; set; }

        public double Quantity { get; set; }

        [DataType(DataType.Date), Display(Name = "Entry Time")]
        public DateTime EntryTime { get; set; }

        public string Remarks { get; set; }

        [ForeignKey("StockId")]
        public Stock Stock { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}