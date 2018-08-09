using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineStore.Helpers;
using OnlineStore.Models;
using OnlineStore.Models.Product;
using OnlineStore.Reusorces;
using OnlineStore.ViewModels.ProductCategories;
using OnlineStore.ViewModels.ProductSizes;

namespace OnlineStore.Controllers
{
    [AuthorizeRole(Roles = RoleNames.ManagerOrAdmin)]
    public class SizesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sizes?categoryId=3
        public ActionResult Index(long? categoryId)
        {
            if (categoryId == null)
            {
                return ShowCategories();
            }
            ViewBag.CategoryId = categoryId;
            string categoryName = db.ProductCategories.Where(c => c.CategoryId == categoryId).Select(c => c.CategoryName).First();
            if (categoryName == null)
            {
                return HttpNotFound(ErrorMessage.CategoryDoesNotExist);
            }
            var sizes = db.Sizes.Where(s => s.CategoryId == categoryId);
            ViewBag.CategoryName = categoryName;
            List<SizeViewModel> sizesViewModels = new List<SizeViewModel>();
            foreach (Size size in sizes)
            {
                SizeViewModel sizeViewModel = new SizeViewModel(size);
                sizesViewModels.Add(sizeViewModel);
            }
            return View(sizesViewModels);
        }

        private ActionResult ShowCategories()
        {
            List<ProductCategory> productCategories = db.ProductCategories.ToList();
            if (!productCategories.Any())
            {
                ViewBag.DefineCategory = "Proszę zdefiniować co najmniej jedną kategorię, aby móc dodać rozmiary";
                return View("LackOfCategories");
            }
            List<ProductCategoryViewModel> categoriesViewModel = new List<ProductCategoryViewModel>();
            foreach (ProductCategory category in productCategories)
            {
                ProductCategoryViewModel productCategoryViewModel = new ProductCategoryViewModel(category);
                categoriesViewModel.Add(productCategoryViewModel);
            }
            ViewBag.ChooseCategory = "Wybierz konkretną kategorię, aby zobaczyć rozmiary";
            return View("ShowCategories", categoriesViewModel);
        }

        // GET: Sizes/Create/5
        public ActionResult Create(long? categoryId)
        {
            if (categoryId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ErrorMessage.NoCategoryParameterProvided);
            }
            string categoryName = db.ProductCategories.Where(c => c.CategoryId == categoryId).Select(c => c.CategoryName).First();
            if (categoryName == null)
            {
                return HttpNotFound(ErrorMessage.CategoryDoesNotExist);
            }
            ViewBag.CategoryName = categoryName;
            Size size = new Size() {CategoryId = categoryId.Value};
            SizeViewModel sizeViewModel = new SizeViewModel(size);
            return View(sizeViewModel);
        }

        // POST: Sizes/Create/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,SizeName")] SizeViewModel sizeViewModel)
        {
            string categoryName = db.ProductCategories.Where(c => c.CategoryId == sizeViewModel.CategoryId).Select(c => c.CategoryName).First();
            if (categoryName == null)
            {
                return HttpNotFound(ErrorMessage.CategoryDoesNotExist);
            }
            ViewBag.CategoryName = categoryName;
            if (ModelState.IsValid)
            {
                List<Size> sizes = db.Sizes.Where(s => s.CategoryId == sizeViewModel.CategoryId).ToList();
                if (sizes.Exists(x => String.Equals(x.SizeName, sizeViewModel.SizeName, StringComparison.OrdinalIgnoreCase)))
                {
                    ModelState.AddModelError("SizeName", "Wpisany rozmiar już istnieje");
                    return View(sizeViewModel);
                }
                Size size = sizeViewModel.CreateSize();
                db.Sizes.Add(size);
                db.SaveChanges();
                return RedirectToAction("Index", new {categoryId = size.CategoryId});
            }

            return View(sizeViewModel);
        }


        // GET: Sizes/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ErrorMessage.SizeIdNotSpecified);
            }
            Size size = db.Sizes.Find(id);
            if (size == null)
            {
                return HttpNotFound(ErrorMessage.SizeDoesNotExist);
            }
            string categoryName = db.ProductCategories.Where(c => c.CategoryId == size.CategoryId).Select(c => c.CategoryName).First();
            if (categoryName == null)
            {
                return HttpNotFound(ErrorMessage.CategoryDoesNotExist);
            }
            ViewBag.CategoryName = categoryName;
            SizeViewModel sizeViewModel = new SizeViewModel(size);
            return View(sizeViewModel);
        }

        // POST: Sizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Size size = db.Sizes.Find(id);
            if (size == null)
            {
                return HttpNotFound(ErrorMessage.SizeDoesNotExist);
            }
            long categoryIdFromSize = size.CategoryId;
            db.Sizes.Remove(size);
            db.SaveChanges();
            return RedirectToAction("Index", new {categoryId = categoryIdFromSize});
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
