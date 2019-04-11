using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace POS_SP.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required, StringLength(255), Display(Name = "Name")]
        public string Name { get; set; }

        [Required, Display(Name = "Address")]
        public string Address1 { get; set; }

        [Display(Name = "Address")]
        public string Address2 { get; set; }

        [Display(Name = "Address")]
        public string Address3 { get; set; }

        [Phone, Display(Name = "Contact Person Phone No.")]
        public string PhoneNo { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public List<Sale> Sales { get; set; }
    }
}