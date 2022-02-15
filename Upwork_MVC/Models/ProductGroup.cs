using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Upwork_MVC.Models
{
    public class ProductGroup
    {
        public ProductGroup()
        {
            Products = new HashSet<Product>();
        }
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Group Description")]
        public string GroupDescription { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name = "Group Code")]
        public string GroupCode { get; set; }
        [Required]
        public bool Active { get; set; } = true;
        public virtual ICollection<Product> Products { get; set; }
    }
}