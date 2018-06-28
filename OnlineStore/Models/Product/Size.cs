using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineStore.Models.Product
{
    public class Size
    {
        [Key]
        public long SizeId { get; set; }

        [Required]
        public long CategoryId { get; set; }

        [Required]
        public string SizeName { get; set; }

        [Required]
        public int SizeAmount { get; set; } = 0;

        public virtual ProductCategory ProductCategory { get; set; }
    }
}