using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OnlineStore.Helpers;
using OnlineStore.Models;
using OnlineStore.Reusorces;
using OnlineStore.ViewModels.Administration;

namespace OnlineStore.Controllers
{
    [Authorize(Roles = RoleNames.Manager)]
    public class AdministrationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administration
        public ActionResult Index()
        {
            string adminRoleId = GetRoleId(RoleNames.Admin);
            List<AdminViewModel> adminsViewModel = db.Users.Where(u => u.Roles.Any(r => r.RoleId == adminRoleId))
                .Select(u => new AdminViewModel
                {
                    UserId = u.Id,
                    UserName = u.UserName
                }).ToList();
            return View(adminsViewModel);
        }

        private string GetRoleId(string roleName)
        {
            return db.Roles.First(r => r.Name == roleName).Id;
        }

        //GET: Administration/Create
        public ActionResult Create()
        {
            string userRoleId = GetRoleId(RoleNames.User);
            var shopEmployees = db.Users.Where(u => u.Email.Contains("@cinamonn.pl") && u.Roles.Any(r => r.RoleId == userRoleId)).ToList();
            ViewBag.UserId = new SelectList(shopEmployees, "Id", "UserName", shopEmployees.Any() ? shopEmployees[0].Id : null);
            return View();
        }

        //POST: Administration/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId")]AdminViewModel adminViewModel)
        {
            if (ModelState.IsValid)
            {
                UserManager<ApplicationUser> userManager = GetUserManager();
                ApplicationUser user = userManager.FindById(adminViewModel.UserId);
                if (user == null)
                {
                    return HttpNotFound(ErrorMessage.UserDoesNotExist);
                }
                userManager.RemoveFromRole(adminViewModel.UserId, RoleNames.User);
                userManager.AddToRole(adminViewModel.UserId, RoleNames.Admin);
                return RedirectToAction("Index");
            }
            string userRoleId = GetRoleId(RoleNames.User);
            var shopEmployees = db.Users.Where(u => u.Email.Contains("@cinamonn.pl") && u.Roles.Any(r => r.RoleId == userRoleId)).ToList();
            ViewBag.UserId = new SelectList(shopEmployees, "Id", "UserName", shopEmployees.Any() ? shopEmployees[0].Id : null);
            return View();
        }

        private UserManager<ApplicationUser> GetUserManager()
        {
            return new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        //GET: Administration/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ErrorMessage.UserIdNotSpecified);
            }
            UserManager<ApplicationUser> userManager = GetUserManager();
            ApplicationUser user = userManager.FindById(id);
            if (user == null)
            {
                return HttpNotFound(ErrorMessage.UserDoesNotExist);
            }
            AdminViewModel adminViewModel = new AdminViewModel(id, user.UserName);
            return View(adminViewModel);
        }

        //POST: Administration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserManager<ApplicationUser> userManager = GetUserManager();
            ApplicationUser user = userManager.FindById(id);
            if (user == null)
            {
                return HttpNotFound(ErrorMessage.UserDoesNotExist);
            }
            userManager.RemoveFromRole(id, RoleNames.Admin);
            userManager.AddToRole(id, RoleNames.User);
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