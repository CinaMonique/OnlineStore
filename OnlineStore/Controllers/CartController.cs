using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OnlineStore.Helpers;
using OnlineStore.Models;
using OnlineStore.Models.Product;
using OnlineStore.Models.ShoppingCart;
using OnlineStore.Reusorces;
using OnlineStore.ViewModels.ShoppingCart;

namespace OnlineStore.Controllers
{
    [AuthorizeRole(Roles = RoleNames.User)]
    public class CartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cart/ShowCart
        public ActionResult ShowCart()
        {
            string userId = User.Identity.GetUserId();
            Cart cart = db.Carts.FirstOrDefault(c => c.CartId == userId);
            if (cart == null)
            {
                return HttpNotFound(ErrorMessage.CartDoesNotExist);
            }
            List<CartItemViewModel> cartItemsViewModel = new List<CartItemViewModel>();
            foreach (CartItem cartItem in cart.Items)
            {
                CartItemViewModel cartItemViewModel = new CartItemViewModel(cartItem);
                cartItemsViewModel.Add(cartItemViewModel);
            }
            return View(cartItemsViewModel);
        }

        // POST: Cart/AddToCart
        [HttpPost]
        public ContentResult AddToCart(long? productId, string checkedSize)
        {
            if (productId == null)
            {
                return Content("Nie podano produktu do dodania");
            }

            if (checkedSize == null)
            {
                return Content("Proszę wybrać rozmiar produktu");
            }
            Product product = db.Products.Include(p => p.ProductPhotos)
                .SingleOrDefault(p => p.ProductId == productId && p.ProductDetailsList.FirstOrDefault(s => s.SizeName == checkedSize).Amount > 0);
            if (product == null)
            {
                return Content("Niestety produkt został właśnie wyprzedany");
            }

            string userId = User.Identity.GetUserId();
            Cart cart = db.Carts.FirstOrDefault(c => c.CartId == userId);
            if (cart == null)
            {
                return Content("Nie znaleziono koszyka dla wybranego użytkownika");
            }
            if (cart.Items == null)
            {
                cart.Items = new List<CartItem>();
            }

            CartItem cartItem = cart.Items.SingleOrDefault(i => i.ProductId == productId && i.CheckedSize == checkedSize);
            if (cartItem != null)
            {
                if (cartItem.Quantity >= product.ProductDetailsList.FirstOrDefault(s => s.SizeName == checkedSize).Amount)
                {
                    return Content("Brak wystarczającej ilości produktu");
                }
                cartItem.Quantity++;
            }
            else
            {
                cartItem = new CartItem()
                {
                    ProductId = productId.Value,
                    CartId = userId,
                    CheckedSize = checkedSize,
                    Quantity = 1
                };
                cart.Items.Add(cartItem);
            }
            db.SaveChanges();
            return Content("Produkt dodano do koszyka");
        }

        // POST: Cart/DeleteItem
        public ActionResult DeleteItem(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ErrorMessage.CartItemIdNotSpecified);
            }
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound(ErrorMessage.CartItemDoesNotExist);
            }
            db.Entry(cartItem).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("ShowCart");
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}