using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Models.Product
{
    public class ProductCategory
    {
        [Key]
        public long CategoryId { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string CategoryName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Size> Sizes { get; set; }

    }
}