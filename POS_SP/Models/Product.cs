using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_SP.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(255), Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required, Display(Name = "Product Code")]
        public string ProductCode { get; set; }

        [Required, Display(Name = "Unit")]
        public string UOM { get; set; }

        [Required, Display(Name = "Unit Price")]
        public double UnitPrice { get; set; }

        [Required, Display(Name = "Brand")]
        public int BrandId { get; set; }

        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }

        [Required, Display(Name = "Sub Category")]
        public int SubCategoryId { get; set; }

        [ForeignKey("SubCategoryId")]
        public SubCategory SubCategory { get; set; }
    }
}