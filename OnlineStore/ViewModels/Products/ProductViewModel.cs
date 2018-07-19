using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using OnlineStore.Models.Product;
using OnlineStore.Reusorces;
using OnlineStore.ViewModels.ProductsDetails;
using WebGrease.Css.Extensions;

namespace OnlineStore.ViewModels.Products
{
    public class ProductViewModel
    {
        public long ProductId { get; set; }

        [Required(ErrorMessage = "Wybierz kategorię produktu")]
        [DisplayName("Kategoria")]
        public long CategoryId { get; set; }

        [DisplayName("Nazwa kategorii")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Podaj nazwę produktu")]
        [MinLength(6, ErrorMessage = "Nazwa produktu nie może być krótsza niż 6 znaków")]
        [MaxLength(50, ErrorMessage = "Nazwa produktu nie może być dłuższa niż 50 znaków")]
        [DataType(DataType.MultilineText)]
        [DisplayName("Nazwa produktu")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Podaj cenę produktu")]
        [DataType(DataType.Currency)]
        [Range(0, Double.MaxValue, ErrorMessage = "Proszę wpisać poprawną cenę")]
        [DisplayName("Cena")]
        public decimal Price { get; set; }

        [MaxLength(500, ErrorMessage = "Opis produktu nie może być dłuższy niż 500 znaków")]
        [DataType(DataType.MultilineText)]
        [DisplayName("Opis")]
        public string ProductDescription { get; set; }

        public List<ProductDetailsViewModel> ProductDetailsListViewModel { get; set; }
        //public ICollection<Photos> ProductPhotos { get; set; } //na ViewModel


        public ProductViewModel(Product product)
        {
            this.ProductId = product.ProductId;
            this.CategoryId = product.CategoryId;
            this.CategoryName = product.ProductCategory.CategoryName;
            this.ProductName = product.ProductName;
            this.Price = product.Price;
            this.ProductDescription = product.ProductDescription;
            this.ProductDetailsListViewModel = product.ProductDetailsList
                .Select(productDetails => new ProductDetailsViewModel(productDetails)).ToList();
            //this.ProductPhotos = product.ProductPhotos;
        }

        public ProductViewModel(Product product, List<Size> sizes)
        {
            this.ProductId = product.ProductId;
            this.CategoryId = product.CategoryId;
            this.ProductName = product.ProductName;
            this.Price = product.Price;
            this.ProductDescription = product.ProductDescription;
            this.ProductDetailsListViewModel =
                sizes.Select(size => new ProductDetailsViewModel(size)).ToList();
            //this.ProductPhotos = product.ProductPhotos;
        }

        // MVC requires public constructor
        public ProductViewModel () { }

        public Product UpdateToDomainModel()
        {
            return new Product()
            {
                ProductId = this.ProductId,
                CategoryId = this.CategoryId,
                ProductName = this.ProductName,
                Price = this.Price,
                ProductDescription = this.ProductDescription,
                ProductDetailsList = 
                    this.ProductDetailsListViewModel.Select(productDetailsVM => productDetailsVM.UpdateToDomainModel()).ToList()
                //ProductPhotos = this.ProductPhotos,
            };
        }
    }
}