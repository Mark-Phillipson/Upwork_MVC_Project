using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Upwork_MVC.Models
{
    public class Agreement
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        [Display(Name ="Username")]
        public string UserId { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Effective Date")]
        public DateTime EffectiveDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Expiration Date")]
        public DateTime ExpirationDate { get; set; }
        [Required]
        [Display(Name ="Product Price")]
        public decimal ProductPrice { get; set; }=decimal.Zero;
        [Required]
        [Display(Name = "New Price")]
        public decimal NewPrice { get; set; } = decimal.Zero;
    }

}