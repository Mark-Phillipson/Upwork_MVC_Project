using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Upwork_MVC.Models
{
    public class Product
    {
        public Product()
        {
            Agreements = new HashSet<Agreement>();
        }
        public int Id { get; set; }
        [Required]
        [Display(Name ="Product Group")]
        public int ProductGroupId { get; set; }
        public ProductGroup ProductGroup { get; set; }
        [Required]
        [Display(Name = "Product Description")]
        [StringLength(200)]
        public string ProductDescription { get; set; }
        [Required]
        [Display(Name = "Product Number")]
        public  int ProductNumber { get; set; }
        [Required]
        public decimal Price { get; set; } = decimal.Zero;
        public bool Active { get; set; } = true;
        public virtual ICollection<Agreement> Agreements { get; set; }

    }
}