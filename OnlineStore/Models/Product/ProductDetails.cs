using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineStore.Models.Product
{
    public class ProductDetails
    {
        [Key]
        public long ProductDetailsId { get; set; }

        [Required]
        public long ProductId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(10)]
        public string SizeName { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Amount { get; set; } = 0;

        public virtual Product Product { get; set; }
    }
}