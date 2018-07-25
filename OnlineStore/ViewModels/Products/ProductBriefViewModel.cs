using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OnlineStore.Models.Product;

namespace OnlineStore.ViewModels.Products
{
    public class ProductBriefViewModel
    {
        public long ProductId { get; set; }

        public long CategoryId { get; set; }

        [DisplayName("Zdjęcie produktu")]
        public string PhotoPath { get; set; }

        [DisplayName("Nazwa produktu")]
        public string ProductName { get; set; }

        [DataType(DataType.Currency)]
        [DisplayName("Cena")]
        public decimal Price { get; set; }


        public ProductBriefViewModel(Product product)
        {
            this.ProductId = product.ProductId;
            this.CategoryId = product.CategoryId;
            this.PhotoPath = product.ProductPhotos.FirstOrDefault().PhotoName;
            this.ProductName = product.ProductName;
             this.Price = product.Price;
        }

        // MVC requires public constructor
        public ProductBriefViewModel() { }

    }
}