using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OnlineStore.Models.Product;

namespace OnlineStore.ViewModels.ProductSizes
{
    public class SizeViewModel
    {
        public long SizeId { get; set; }

        [Required(ErrorMessage = "Wybierz kategorię produktów")]
        [DisplayName("Kategoria produktów")]
        public long CategoryId { get; set; }

        [Required(ErrorMessage = "Podaj nazwę rozmiaru")]
        [MinLength(1, ErrorMessage = "Nazwa rozmiaru musi się składać z co najmniej 1 znaku")]
        [MaxLength(10, ErrorMessage = "Nazwa rozmiaru nie może być dłuższa niż 10 znaków")]
        [DisplayName("Nazwa rozmiaru")]
        public string SizeName { get; set; }


        public SizeViewModel(Size size)
        {
            this.SizeId = size.SizeId;
            this.CategoryId = size.CategoryId;
            this.SizeName = size.SizeName;
        }

        // MVC requires public constructor
        public SizeViewModel() { }


        public Size CreateSize()
        {
            return new Size()
            {
                SizeId = this.SizeId,
                CategoryId = this.CategoryId,
                SizeName = this.SizeName
            };
        }
    }
}