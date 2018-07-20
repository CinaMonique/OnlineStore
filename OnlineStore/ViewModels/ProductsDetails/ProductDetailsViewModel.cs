using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Web;
using OnlineStore.Models.Product;
using OnlineStore.Reusorces;

namespace OnlineStore.ViewModels.ProductsDetails
{
    public class ProductDetailsViewModel
    {
        public long ProductDetailsId { get; set; }

        [Required]
        public long ProductId { get; set; }

        [DisplayName("Nazwa rozmiaru")]
        public string SizeName { get; set; }

        [Required(ErrorMessage = "Wpisz dostępną ilość produktu")]
        [Range(0, int.MaxValue, ErrorMessage = "Proszę wpisać poprawną liczbę sztuk")]
        [DisplayName("Ilość")]
        public int Amount { get; set; }
        

        public ProductDetailsViewModel(Size size)
        {
            this.SizeName = size.SizeName;
            this.Amount = 0;
        }

        public ProductDetailsViewModel(ProductDetails productDetails)
        {
            this.ProductDetailsId = productDetails.ProductDetailsId;
            this.ProductId = productDetails.ProductId;
            this.SizeName = productDetails.SizeName;
            this.Amount = productDetails.Amount;
        }

        // MVC requires public constructor
        public ProductDetailsViewModel() { }

        public ProductDetails CreateProductDetails()
        {
            return new ProductDetails
            {
                SizeName = this.SizeName,
                Amount = this.Amount
            };
        }

        public void UpdateProductDetails(ProductDetails productDetails)
        {
            productDetails.Amount = this.Amount;
        }
    }
}