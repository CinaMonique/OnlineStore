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
using OnlineStore.ViewModels.Products;
using OnlineStore.ViewModels.ProductsDetails;

namespace OnlineStore.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ActionResult Index(long? categoryId)
        {
            if (categoryId == null)
            {
               return ShowCategories();
            }
            ViewBag.CategoryId = categoryId;
            ProductCategory category = db.ProductCategories.Find(categoryId);
            if (category == null)
            {
                return HttpNotFound(ErrorMessage.CategoryDoesNotExist);
            }
            var products = db.Products.Where(p => p.CategoryId == categoryId).Include(p => p.ProductCategory);
            ViewBag.CategoryName = category.CategoryName;
            List<ProductBriefViewModel> productsViewModels = new List<ProductBriefViewModel>();
            foreach (Product product in products)
            {
                ProductBriefViewModel productViewModel = new ProductBriefViewModel(product);
                productsViewModels.Add(productViewModel);
            }
            return View(productsViewModels);
        }

        private ActionResult ShowCategories()
        {
            List<ProductCategory> productCategories = db.ProductCategories.ToList();
            List<ProductCategoryViewModel> categoriesViewModel = new List<ProductCategoryViewModel>();
            foreach (ProductCategory category in productCategories)
            {
                ProductCategoryViewModel productCategoryViewModel = new ProductCategoryViewModel(category);
                categoriesViewModel.Add(productCategoryViewModel);
            }
            ViewBag.Message = "Wybierz konkretną kategorię, aby zobaczyć produkty";
            return View("ShowCategories", categoriesViewModel);
        }

        // GET: Products/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create(long? categoryId)
        {
            if (categoryId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ErrorMessage.NoCategoryParameterProvided);
            }
            int categoryCount = db.ProductCategories.Count(c => c.CategoryId == categoryId);
            if (categoryCount == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ErrorMessage.CategoryDoesNotExist);
            }
            ViewBag.CategoryId = categoryId;
            List<Size> sizes = db.Sizes.Where(s => s.CategoryId == categoryId).ToList();
            if (!sizes.Any())
            {
                return View("LackOfSizes");
            }
            Product product = new Product() { CategoryId = categoryId.Value };
            ProductViewModel productViewModel = new ProductViewModel(product, sizes);
            return View(productViewModel);
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,ProductName,Price,ProductDescription, ProductDetailsListViewModel")] ProductViewModel productViewModel)
        {
            int categoryCount = db.ProductCategories.Count(c => c.CategoryId == productViewModel.CategoryId);
            if (categoryCount == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ErrorMessage.CategoryDoesNotExist);
            }
            if (ModelState.IsValid)
            {
                Product product = productViewModel.UpdateToDomainModel();
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index", new { categoryId = product.CategoryId } );
            }

            return View(productViewModel);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,CategoryId,ProductName,Price,ProductDescription")] Product product)
        {
            if (ModelState.IsValid) //Uwaga na foreach i indeksy
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
