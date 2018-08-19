using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineStore.Helpers;
using OnlineStore.Models;
using OnlineStore.Models.ShoppingCart;
using OnlineStore.Reusorces;
using OnlineStore.ViewModels.ShoppingCart;

namespace OnlineStore.Controllers
{
    [AuthorizeRole(Roles = RoleNames.User)]
    public class CartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cart
        public ActionResult ShowCart(string userId)
        {
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ErrorMessage.UserIdNotSpecified);
            }
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