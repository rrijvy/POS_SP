using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace POS_SP.Models
{
    public class Purchase
    {
        public int Id { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "Purchase Date")]
        public DateTime PurchaseDate { get; set; }

        [Required, Display(Name = "Order Reference No.")]
        public string OrderRefNo { get; set; }

        [Required, Display(Name = "Supplier Name")]
        public int SupplierId { get; set; }

        [Display(Name = "Order No.")]
        public string OrderNo { get; set; }

        [Display(Name = "Loading Bill")]
        public double LoadingBill { get; set; }

        [Display(Name = "Labour Cost")]
        public double LabourCost { get; set; }

        [Display(Name = "Tax Percent")]
        public double TaxPercent { get; set; }

        [Display(Name = "Tax Amount")]
        public double TaxAmount { get; set; }

        [Display(Name = "Vat Percent")]
        public double VatPercent { get; set; }

        [Display(Name = "Vat Amount")]
        public double VatAmount { get; set; }

        [Display(Name = "Discount Percent")]
        public double DiscountPercent { get; set; }

        [Display(Name = "Discount Amount")]
        public double DiscountAmount { get; set; }

        public string PaymentType { get; set; }

        public double PaymentAmount { get; set; }

        public double DueAmount { get; set; }

        [Required]
        [Display(Name = "Total Amount")]
        public double TotalAmount { get; set; }

        [ForeignKey("SupplierId")]
        public Supplier Supplier { get; set; }

        public List<PurchaseDetail> PurchaseDetails { get; set; }
    }
}
