using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_SP.Models
{
    public class SalesDetail
    {
        public int Id { get; set; }

        [Required, Display(Name = "Unit Price")]
        public double UnitPrice { get; set; }

        [Required]
        public double Quantity { get; set; }

        [Required, Display(Name = "Unit")]
        public string UOM { get; set; }

        [Display(Name = "Total Amount")]
        public double IndividualTotal { get; set; }

        [Required, Display(Name = "Product")]
        public int ProductId { get; set; }

        [Required, Display(Name = "Sales Order")]
        public int SaleId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [ForeignKey("SaleId")]
        public Sale Sale { get; set; }
    }
}
