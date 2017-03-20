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
    public class DeviceController : Controller
    {
        private DeviceManagementDbContext db = new DeviceManagementDbContext();
        private RestUtility restUtility = new RestUtility();
        // GET: Device
        public async Task<ActionResult> Index()
        {
            var devices = await restUtility.GetAsync<List<Device>>("api/Devices");
            return View(devices);
        }

        // GET: Device/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            var device = await restUtility.GetAsync<Device>("api/Devices/"+id.ToString());
            if (device == null)
            {
                return HttpNotFound();
            }
            return View(device);
        }

        // GET: Device/Create
        public async Task<ActionResult> Create()
        {
            var users = await restUtility.GetAsync<List<User>>("api/Users");
            ViewBag.Users = users;
            return View();
        }

        // POST: Device/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Manufacturer,Type,OperatingSystem,OsVersion,Processor,Ram,UserId")] Device device)
        {
            if (ModelState.IsValid)
            {
                var result = await restUtility.PostAsync<Device>("api/Devices", device);
                return RedirectToAction("Index");
            }

            return View(device);
        }

        // GET: Device/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var device = await restUtility.GetAsync<Device>("api/Devices/" + id.ToString());
            if (device == null)
            {
                return HttpNotFound();
            }
            
            return View(device);
        }

        // POST: Device/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Manufacturer,Type,OperatingSystem,OsVersion,Processor,Ram,UserId")] Device device)
        {
            if (ModelState.IsValid)
            {
                var result = await restUtility.PutAsync<Device>("api/Devices/"+device.Id.ToString(), device);
                return RedirectToAction("Index");
            }
            return View(device);
        }

        // GET: Device/Delete/5
        public async  Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var device = await restUtility.GetAsync<Device>("api/Devices/" + id.ToString());
            if (device == null)
            {
                return HttpNotFound();
            }
            return View(device);
        }

        // POST: Device/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var result = await restUtility.DeleteAsync("api/Devices/",id);
         
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
