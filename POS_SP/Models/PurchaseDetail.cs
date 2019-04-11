using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_SP.Models
{
    public class PurchaseDetail
    {
        public int Id { get; set; }

        [Required, Display(Name = "Purchase No.")]
        public int PurchaseId { get; set; }

        [Required, Display(Name = "Product")]
        public int ProductId { get; set; }

        [Required, Display(Name = "Quantity")]
        public double Quantity { get; set; }

        [Required, Display(Name = "Unit of Measurement")]
        public string UOM { get; set; }

        [Required, Display(Name = "Unit Price")]
        public double UnitPrice { get; set; }

        public double IndividualTotal { get; set; }

        [ForeignKey("PurchaseId")]
        public Purchase Purchase { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}