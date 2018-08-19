using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineStore.Models.ShoppingCart
{
    public class  Cart
    {
        [Key, ForeignKey("User")]
        public string CartId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<CartItem> Items { get; set; }
    }
}