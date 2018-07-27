using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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

        //TODO: Delete this after creating roles
        public ActionResult UserProductList(long? categoryId)
        {
            var products = db.Products.Include(p => p.ProductPhotos);
            List<ProductBriefViewModel> productsViewModels = new List<ProductBriefViewModel>();
            foreach (Product product in products)
            {
                ProductBriefViewModel productViewModel = new ProductBriefViewModel(product);
                productsViewModels.Add(productViewModel);
            }
            return View("UserProductList",productsViewModels);
        }

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
            var products = db.Products.Where(p => p.CategoryId == categoryId).Include(p => p.ProductCategory).Include(p => p.ProductPhotos);
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
            if (!productCategories.Any())
            {
                ViewBag.DefineCategory = "Proszę zdefiniować co najmniej jedną kategorię, aby móc dodać produkty";
                return View("LackOfCategories");
            }
            List<ProductCategoryViewModel> categoriesViewModel = new List<ProductCategoryViewModel>();
            foreach (ProductCategory category in productCategories)
            {
                ProductCategoryViewModel productCategoryViewModel = new ProductCategoryViewModel(category);
                categoriesViewModel.Add(productCategoryViewModel);
            }
            ViewBag.ChooseCategory = "Wybierz konkretną kategorię, aby zobaczyć produkty";
            return View("ShowCategories", categoriesViewModel);
        }

        //TODO: Delete this after creating roles
        public ActionResult UserDetails(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ErrorMessage.ProductIdDoesNotExist);
            }
            Product product = db.Products.Include(p => p.ProductPhotos).SingleOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return HttpNotFound(ErrorMessage.ProductDoesNotExist);
            }
            ProductViewModel productViewModel = new ProductViewModel(product);
            return View(productViewModel);
        }

        // GET: Products/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ErrorMessage.ProductIdDoesNotExist);
            }
            Product product = db.Products.Include(p => p.ProductPhotos).SingleOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return HttpNotFound(ErrorMessage.ProductDoesNotExist);
            }
            ProductViewModel productViewModel = new ProductViewModel(product);
            return View(productViewModel);
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
        public ActionResult Create([Bind(Include = "CategoryId,ProductName,Price,ProductDescription,ProductDetailsListViewModel")] ProductViewModel productViewModel, IEnumerable<HttpPostedFileBase> upload)
        {
            int categoryCount = db.ProductCategories.Count(c => c.CategoryId == productViewModel.CategoryId);
            if (categoryCount == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ErrorMessage.CategoryDoesNotExist);
            }
            if (ModelState.IsValid)
            {
                if (upload != null && upload.Any(u => u != null && u.ContentLength > 0))
                {
                    string validationError = ValidatePhotoUpload(upload);
                    if (validationError != null)
                    {
                        ModelState.AddModelError("ProductPhotos", validationError);
                        return View(productViewModel);
                    }
                    Product product = productViewModel.CreateProduct();
                    foreach (HttpPostedFileBase photo in upload)
                    {
                        string photoNameWithTimeStamp = AppendTimeStamp(photo.FileName);
                        string path = Path.Combine(Server.MapPath("~/Images"), photoNameWithTimeStamp);
                        photo.SaveAs(path);
                        product.ProductPhotos.Add(new Photos(photoNameWithTimeStamp));
                    }
                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { categoryId = product.CategoryId });
                }
                ModelState.AddModelError("ProductPhotos", "Produkt musi zawierać co najmniej jedno zdjęcie");
            }
            return View(productViewModel);
        }

        private static string ValidatePhotoUpload(IEnumerable<HttpPostedFileBase> upload)
        {
            if (upload.Count() > 5)
            {
                return "Nie można dodać więcej niż 5 zdjęć";
            }
            foreach (HttpPostedFileBase photo in upload)
            {
                if (photo.ContentLength > 6500000)
                {
                    return "Zdjęcie nie może być większe niż 6MB";
                }
            }
            return null;
        }

        private static string AppendTimeStamp(string fileName)
        {
            return string.Concat(
                Path.GetFileNameWithoutExtension(fileName),
                DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                Path.GetExtension(fileName)
                );
        }

        // GET: Products/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ErrorMessage.ProductIdDoesNotExist);
            }
            Product product = db.Products.Include(p => p.ProductPhotos).SingleOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return HttpNotFound(ErrorMessage.ProductDoesNotExist);
            }
            ProductViewModel productViewModel = new ProductViewModel(product);
            return View(productViewModel);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,CategoryId,ProductName,Price,ProductDescription,ProductDetailsListViewModel")] ProductViewModel productViewModel, IEnumerable<HttpPostedFileBase> upload)
        {
            int categoryCount = db.ProductCategories.Count(c => c.CategoryId == productViewModel.CategoryId);
            if (categoryCount == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ErrorMessage.CategoryDoesNotExist);
            }
            if (ModelState.IsValid)
            {
                Product product = db.Products.Include(p => p.ProductPhotos).SingleOrDefault(p => p.ProductId == productViewModel.ProductId);
                if (product == null)
                {
                    return HttpNotFound(ErrorMessage.ProductDoesNotExist);
                }

                if (upload != null && upload.Any(u => u != null && u.ContentLength > 0))
                {
                    string validationError = ValidatePhotoUpload(upload);
                    if (validationError != null)
                    {
                        ModelState.AddModelError("ProductPhotos", validationError);
                        return View(productViewModel); 
                    }
                    DeleteAllPhotosFromFolder(product);
                    foreach (var photo in product.ProductPhotos.ToList())
                    {
                        db.Entry(photo).State = EntityState.Deleted;
                    }
                    db.SaveChanges();
                    product.ProductPhotos = new List<Photos>();
                    foreach (HttpPostedFileBase photo in upload)
                    {
                        string photoNameWithTimeStamp = AppendTimeStamp(photo.FileName);
                        string path = Path.Combine(Server.MapPath("~/Images"), photoNameWithTimeStamp);
                        photo.SaveAs(path);
                        product.ProductPhotos.Add(new Photos(photoNameWithTimeStamp));
                    }
                }

                productViewModel.UpdateProduct(product); 
                foreach (ProductDetails details in product.ProductDetailsList)
                {
                    db.Entry(details).State = EntityState.Modified;
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { categoryId = product.CategoryId });
            }
            return View(productViewModel);
        }

        private void DeleteAllPhotosFromFolder(Product product)
        {
            foreach (Photos photo in product.ProductPhotos)
            {
                string path = Path.Combine(Server.MapPath("~/Images"), photo.PhotoName);
                System.IO.File.Delete(path);
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ErrorMessage.ProductIdDoesNotExist);
            }
            Product product = db.Products.Include(p => p.ProductPhotos).SingleOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return HttpNotFound(ErrorMessage.ProductDoesNotExist);
            }
            ProductBriefViewModel productViewModel = new ProductBriefViewModel(product);
            return View(productViewModel);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Product product = db.Products.Include(p => p.ProductPhotos).SingleOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return HttpNotFound(ErrorMessage.ProductDoesNotExist);
            }
            DeleteAllPhotosFromFolder(product);
            long categoryIdFromProduct = product.CategoryId;
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index", new { categoryId = categoryIdFromProduct });
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
