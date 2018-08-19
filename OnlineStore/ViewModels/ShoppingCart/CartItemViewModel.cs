using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OnlineStore.Models.ShoppingCart;

namespace OnlineStore.ViewModels.ShoppingCart
{
    public class CartItemViewModel
    {
        public long CartItemId { get; set; }

        public long ProductId { get; set; }

        public string PhotoPath { get; set; }

        [DisplayName("Produkt")]
        public string ProductName { get; set; }

        [DisplayName("Kod produktu")]
        public string ProductCode { get; set; }

        [DisplayName("Rozmiar")]
        public string ProductSize { get; set; }

        [DisplayName("Ilość")]
        [Range(1, 10, ErrorMessage = "Ilość produktu nie powinna być mniejsza niż 1 oraz większa niż 10")]
        public int Quantity { get; set; }

        [DataType(DataType.Currency)]
        [DisplayName("Cena")]
        public decimal Price { get; set; }


        public CartItemViewModel(CartItem cartItem)
        {
            this.CartItemId = cartItem.CartItemId;
            this.ProductId = cartItem.ProductId;
            this.PhotoPath = cartItem.Product.ProductPhotos.First().PhotoName;
            this.ProductName = cartItem.Product.ProductName;
            this.ProductCode = cartItem.Product.ProductCode;
            this.ProductSize = cartItem.CheckedSize;
            this.Quantity = cartItem.Quantity;
            this.Price = cartItem.Product.Price;
        }

        // MVC requires public constructor
        public CartItemViewModel() { }


    }
}