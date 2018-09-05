using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineStore.Models.ShoppingCart
{
    public class CartItem
    {
        [Key]
        public long CartItemId { get; set; }

        [Required]
        public long ProductId { get; set; }

        [Required]
        public string CartId { get; set; }

        public string CheckedSize { get; set; }

        [Required]
        [Range(1,10)]
        public int Quantity { get; set; }

        public virtual Product.Product Product { get; set; }
        public virtual Cart Cart { get; set; }
    }
}