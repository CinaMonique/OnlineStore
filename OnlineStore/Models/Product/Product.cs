using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [MaxLength(500)]
        public string ProductDescription { get; set; }
         
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<Photos> ProductPhotos { get; set; }
        public virtual ICollection<Size> ProductSize { get; set; }
        //public virtual ICollection<CartItem> Items { get; set; }
    }
}