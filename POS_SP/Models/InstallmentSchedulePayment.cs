using System;
using System.ComponentModel.DataAnnotations;

namespace POS_SP.Models
{
    public class InstallmentSchedulePayment
    {
        public int Id { get; set; }

        public bool IsPaid { get; set; }

        [Display(Name = "Schedule Date"), DataType(DataType.Date)]
        public DateTime ScheduleDate { get; set; }

        [Display(Name = "Schedule Amount")]
        public double ScheduleAmount { get; set; }

        [Display(Name = "Payment Date"), DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }

        [Display(Name = "Paid Amount")]
        public double PaidAmount { get; set; }

        [Display(Name = "Fine Amount")]
        public double FineAmount { get; set; }

        [Display(Name = "Due Amount")]
        public double DueAmount { get; set; }

        [Display(Name = "Total Paid")]
        public double TotalPaid { get; set; }

        [Display(Name = "Total Due")]
        public double TotalDue { get; set; }

        [Display(Name = "Sale Number")]
        public int SalesId { get; set; }

        public Sale Sale { get; set; }
    }
}