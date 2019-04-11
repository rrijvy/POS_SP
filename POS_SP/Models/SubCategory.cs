using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_SP.Models
{
    public class SubCategory
    {
        public int Id { get; set; }

        [Required, StringLength(255), Display(Name = "Category Name")]
        public string Name { get; set; }

        [Required, Display(Name = "Category Code")]
        public string SubCategoryCode { get; set; }

        [Display(Name="Category")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}