using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using OnlineStore.Models.Product;
using System.Linq;
using System.Web;

namespace OnlineStore.ViewModels.ProductCategories
{
    public class ProductCategoryViewModel
    {
        public long CategoryId { get; set; }

        [Required(ErrorMessage = "Podaj nazwę kategorii")]
        [MinLength(4, ErrorMessage = "Nazwa kategorii musi się składać z co najmniej 4 znaków")]
        [MaxLength(30, ErrorMessage = "Nazwa kategorii nie może być dłuższa niż 30 znaków")]
        [DataType(DataType.MultilineText)]
        [DisplayName("Nazwa kategorii")]
        public string CategoryName { get; set; }


        public ProductCategoryViewModel(ProductCategory productCategory)
        {
            this.CategoryId = productCategory.CategoryId;
            this.CategoryName = productCategory.CategoryName;
        }

        // MVC requires public constructor
        public ProductCategoryViewModel() { }


        public ProductCategory CreateProductCategory()
        {
            return new ProductCategory()
            {
                CategoryId = this.CategoryId,
                CategoryName = this.CategoryName
            };
        }

        public void UpdateProductCategory(ProductCategory productCategory)
        {
            productCategory.CategoryName = CategoryName;
        }
    }
}