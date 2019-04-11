using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace POS_SP.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        [Required, Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Owner Name")]
        public string CompaneyOwnerName { get; set; }

        [Display(Name = "Ower Phone No.")]
        public string OwnerPhoneNo { get; set; }

        [Required, Display(Name = "Company/Agent")]
        public string CompanyOrAgent { get; set; }

        [Required, Display(Name = "Address")]
        public string Address1 { get; set; }

        [Display(Name = "Address")]
        public string Address2 { get; set; }

        [Display(Name = "Address")]
        public string Address3 { get; set; }

        [Display(Name = "Contact Person Name")]
        public string ContactPersonName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Contact Person Phone No.")]
        public string PhoneNo { get; set; }

        public List<Purchase> Purchases { get; set; }
    }
}