using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace POS_SP.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required, StringLength(255), Display(Name = "Category Name")]
        public string Name { get; set; }

        [Display(Name = "Sort Order")]
        public byte SortOrder { get; set; }

        public List<SubCategory> SubCategories { get; set; }
    }
}
