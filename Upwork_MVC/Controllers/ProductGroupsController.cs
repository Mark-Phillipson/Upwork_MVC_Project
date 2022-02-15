using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Upwork_MVC.Data;
using Upwork_MVC.Models;

namespace Upwork_MVC.Controllers
{
    [Authorize]
    public class ProductGroupsController : Controller
    {
        private ProductAgreementDbContext db = new ProductAgreementDbContext();

        // GET: ProductGroups
        public async Task<ActionResult> Index()
        {
            return View(await db.ProductGroups.ToListAsync());
        }

        // GET: ProductGroups/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGroup productGroup = await db.ProductGroups.FindAsync(id);
            if (productGroup == null)
            {
                return HttpNotFound();
            }
            return View(productGroup);
        }

        // GET: ProductGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,GroupDescription,GroupCode,Active")] ProductGroup productGroup)
        {
            if (ModelState.IsValid)
            {
                db.ProductGroups.Add(productGroup);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(productGroup);
        }

        // GET: ProductGroups/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGroup productGroup = await db.ProductGroups.FindAsync(id);
            if (productGroup == null)
            {
                return HttpNotFound();
            }
            return View(productGroup);
        }

        // POST: ProductGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,GroupDescription,GroupCode,Active")] ProductGroup productGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productGroup).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(productGroup);
        }

        // GET: ProductGroups/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGroup productGroup = await db.ProductGroups.FindAsync(id);
            if (productGroup == null)
            {
                return HttpNotFound();
            }
            return View(productGroup);
        }

        // POST: ProductGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProductGroup productGroup = await db.ProductGroups.FindAsync(id);
            db.ProductGroups.Remove(productGroup);
            await db.SaveChangesAsync();
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
