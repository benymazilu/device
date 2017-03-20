using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DeviceManagement.Content;
using DeviceManagement.Models;
using DeviceManagement.Utils;
using System.Threading.Tasks;

namespace DeviceManagement.Controllers
{
    public class UserController : Controller
    {
        private DeviceManagementDbContext db = new DeviceManagementDbContext();
        private RestUtility restUtility = new RestUtility();
        // GET: User
        public async Task<ActionResult> Index()
        {
            var users = await restUtility.GetAsync<List<User>>("api/Users");
            return View(users);
        }

        // GET: User/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await restUtility.GetAsync<User>("api/Users/" + id.ToString());
            var devices = await restUtility.GetAsync<List<Device>>("api/FindByUserId/" + id.ToString());
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Devices = devices;
            return View(user);
        }

        // GET: User/Create
        public async Task<ActionResult> Create()
        {
            var users = await restUtility.GetAsync<List<User>>("api/Users");
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Role,Location")] User user)
        {
            if (ModelState.IsValid)
            {
                var result = await restUtility.PostAsync<User>("api/Users", user);
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: User/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await restUtility.GetAsync<User>("api/Users/" + id.ToString());
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Role,Location")] User user)
        {
            if (ModelState.IsValid)
            {
                var result = await restUtility.PutAsync<User>("api/Users/" + user.Id.ToString(), user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: User/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await restUtility.GetAsync<User>("api/Users/" + id.ToString()); 
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var result = await restUtility.DeleteAsync("api/Users/", id);
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
