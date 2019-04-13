using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_SP.Models
{
    public class Sale
    {
        public int Id { get; set; }

        [Display(Name = "Order Number")]
        public string OrderNumber { get; set; }

        [Required, Display(Name = "Order Reference No")]
        public string OrderRefNo { get; set; }

        [Required, Display(Name = "Sales Date"), DataType(DataType.Date)]
        public DateTime SalesDate { get; set; }

        [Display(Name = "Tax Amount")]
        public double TaxAmount { get; set; }

        [Display(Name = "Tax In Percent")]
        public double TaxPercent { get; set; }

        [Display(Name = "Vat Amount")]
        public double VatAmount { get; set; }

        [Display(Name = "Vat In Percent")]
        public double VatPercent { get; set; }

        [Display(Name = "Discount Amount")]
        public double DiscountAmount { get; set; }

        [Display(Name = "Dsicount In Percent")]
        public double DiscountPercent { get; set; }

        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; }

        [Display(Name = "Down Payment")]
        public double DownPayment { get; set; }

        [Display(Name ="Installment Period")]
        public int InstallmentPeriod { get; set; }

        [Display(Name = "Payment Amount")]
        public double PaymentAmount { get; set; }

        [Display(Name = "Due Amount")]
        public double DueAmount { get; set; }

        [Display(Name = "Total Number")]
        public double TotalAmount { get; set; }

        [Display(Name = "Referenced Customer")]
        public int ReferenceId { get; set; }

        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        public List<SalesDetail> SalesDetails { get; set; }
    }
}
