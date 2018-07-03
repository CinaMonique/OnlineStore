using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineStore.Models;
using OnlineStore.Models.Product;
using OnlineStore.ViewModels.ProductCategories;
using OnlineStore.ViewModels.ProductSizes;

namespace OnlineStore.Controllers
{
    public class SizesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sizes/ShowCategories
        public ActionResult ShowCategories()
        {
            List<ProductCategory> productCategories = db.ProductCategories.ToList();
            List<ProductCategoryViewModel> categoriesViewModel = new List<ProductCategoryViewModel>();
            foreach (ProductCategory category in productCategories)
            {
                ProductCategoryViewModel productCategoryViewModel = new ProductCategoryViewModel(category);
                categoriesViewModel.Add(productCategoryViewModel);
            }

            return View("ShowCategories", categoriesViewModel);
        }


        // GET: Sizes?categoryId=3
        public ActionResult Index(long? categoryId)
        {
            if (categoryId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.CategoryId = categoryId;
            var sizes = db.Sizes.Where(s => s.CategoryId == categoryId).Include(s => s.ProductCategory);
            ProductCategory category = db.ProductCategories.Find(categoryId);
            if (category == null)
            {
                return HttpNotFound("Wybrana kategoria nie istnieje lub została usunięta.");
            }
            ViewBag.CategoryName = category.CategoryName;
            List<SizeViewModel> sizesViewModels = new List<SizeViewModel>();
            foreach (Size size in sizes)
            {
                SizeViewModel sizeViewModel = new SizeViewModel(size);
                sizesViewModels.Add(sizeViewModel);
            }
            return View(sizesViewModels);
        }

        // GET: Sizes/Create/5
        public ActionResult Create(long categoryId)
        {
            Size size = new Size() {CategoryId = categoryId};
            SizeViewModel sizeViewModel = new SizeViewModel(size);
            return View(sizeViewModel);
        }

        // POST: Sizes/Create/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SizeId,CategoryId,SizeName,")] SizeViewModel sizeViewModel)
        {
            List<Size> sizes = db.Sizes.ToList();
            if (sizes.Exists(x => String.Equals(x.SizeName, sizeViewModel.SizeName, StringComparison.OrdinalIgnoreCase)))
            {
                ModelState.AddModelError("SizeName", "Wpisany rozmiar już istnieje");
            }
            if (ModelState.IsValid)
            {
                Size size = sizeViewModel.UpdateToDomainModel();
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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Szukane id rozmiaru nie istnieje.");
            }
            Size size = db.Sizes.Find(id);
            if (size == null)
            {
                return HttpNotFound($"Rozmiar o id {id} nie istnieje lub został usunięty.");
            }
            ProductCategory category = db.ProductCategories.Find(size.CategoryId);
            if (category == null)
            {
                return HttpNotFound("Wybrana kategoria nie istnieje lub została usunięta.");
            }
            ViewBag.CategoryName = category.CategoryName;
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
                return HttpNotFound("Wybrany rozmiar nie istnieje lub został usunięty.");
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
