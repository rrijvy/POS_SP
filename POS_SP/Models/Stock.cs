using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_SP.Models
{
    public class Stock
    {
        public int Id { get; set; }

        [Display(Name = "Store")]
        public int StoreId { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime StockDate { get; set; }

        [Display(Name = "Product")]
        public int ProductId { get; set; }

        public string UOM { get; set; }

        public double Quantity { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public List<StockDetail> StockDetails { get; set; }
    }
}
