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
using OnlineStore.Reusorces;
using OnlineStore.ViewModels.ProductCategories;

namespace OnlineStore.Controllers
{
    public class ProductCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProductCategories
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = db.ProductCategories.ToList();
            List<ProductCategoryViewModel> categoriesViewModel = new List<ProductCategoryViewModel>();
            foreach (ProductCategory category in productCategories)
            {
                ProductCategoryViewModel productCategoryViewModel = new ProductCategoryViewModel(category);
                categoriesViewModel.Add(productCategoryViewModel);
            }
            
            return View(categoriesViewModel);
        }

        // GET: ProductCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductCategories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryName")] ProductCategoryViewModel productCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                List<ProductCategory> productCategories = db.ProductCategories.ToList();
                if (productCategories.Exists(x => String.Equals(x.CategoryName, productCategoryViewModel.CategoryName, StringComparison.OrdinalIgnoreCase)))
                {
                    ModelState.AddModelError("CategoryName", "Wpisana kategoria już istnieje");
                    return View(productCategoryViewModel);
                }
                ProductCategory productCategory = productCategoryViewModel.UpdateToDomainModel();
                db.ProductCategories.Add(productCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productCategoryViewModel);
        }

        // GET: ProductCategories/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ErrorMessage.CategoryIdDoesNotExist);
            }
            ProductCategory productCategory = db.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound(ErrorMessage.CategoryDoesNotExist);
            }
            ProductCategoryViewModel productCategoryViewModel = new ProductCategoryViewModel(productCategory);

            return View(productCategoryViewModel);
        }

        // POST: ProductCategories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,CategoryName")] ProductCategoryViewModel productCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                List<ProductCategory> productCategories = db.ProductCategories.ToList();
                if (productCategories.Exists(x => String.Equals(x.CategoryName, productCategoryViewModel.CategoryName, StringComparison.OrdinalIgnoreCase)))
                {
                    ModelState.AddModelError("CategoryName", "Wpisana kategoria już istnieje");
                    return View(productCategoryViewModel);
                }
                ProductCategory productCategory = productCategoryViewModel.UpdateToDomainModel();
                db.Entry(productCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productCategoryViewModel);
        }

        // GET: ProductCategories/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ErrorMessage.CategoryIdDoesNotExist);
            }
            ProductCategory productCategory = db.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound(ErrorMessage.CategoryDoesNotExist);
            }
            ProductCategoryViewModel productCategoryViewModel = new ProductCategoryViewModel(productCategory);

            return View(productCategoryViewModel);
        }

        // POST: ProductCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ProductCategory productCategory = db.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound(ErrorMessage.CategoryDoesNotExist);
            }
            db.ProductCategories.Remove(productCategory);
            db.SaveChanges();
            return RedirectToAction("Index");
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
