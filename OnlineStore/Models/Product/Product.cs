using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OnlineStore.Models.ShoppingCart;

namespace OnlineStore.Models.Product
{
    public class Product
    {
        [Key]
        public long ProductId { get; set; }

        [Required]
        public long CategoryId { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string ProductName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string ProductCode { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0, Double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [MinLength(15)]
        [MaxLength(500)]
        public string ProductDescription { get; set; }
         
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<Photos> ProductPhotos { get; set; }
        public virtual ICollection<ProductDetails> ProductDetailsList { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}