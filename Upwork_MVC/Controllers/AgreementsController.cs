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
    public class AgreementsController : Controller
    {
        private ProductAgreementDbContext db = new ProductAgreementDbContext();

        // GET: Agreements
        public async Task<ActionResult> Index()
        {
            var agreements = db.Agreements.Include(a => a.Product).Include(a => a.Product.ProductGroup);
            CreateLists();
            return View(await agreements.Take(500).ToListAsync());
        }

        // GET: Agreements/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agreement agreement = await db.Agreements.FindAsync(id);
            if (agreement == null)
            {
                return HttpNotFound();
            }
            return View(agreement);
        }
        [HttpGet]
        public async Task<ActionResult> GetAgreement(int id)
        {
            if (id > 0)
            {
                Agreement agreement = await db.Agreements.Where(v => v.Id == id).FirstOrDefaultAsync();
                return Json(agreement, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        private void CreateLists()
        {
            ViewBag.ProductId = new SelectList(db.Products.OrderBy(o => o.ProductNumber), "Id", "ProductDescription");
        }

        // POST: Agreements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int? agreementId, string UserId, int ProductId, DateTime EffectiveDate, DateTime ExpirationDate, decimal ProductPrice, decimal NewPrice)
        {
            UserId = User.Identity.Name;
            Agreement agreement = PopulateAgreementCreate(agreementId, UserId, ProductId, EffectiveDate, ExpirationDate, ProductPrice, NewPrice);

            if (ModelState.IsValid)
            {
                db.Agreements.Add(agreement);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "Id", "ProductDescription", agreement.ProductId);
            return View(agreement);
        }
        Agreement PopulateAgreementEdit(int? agreementId, string userId, int productId, string effectiveDate, string expirationDate, decimal productPrice, decimal newPrice)
        {
            Agreement agreement = agreement = db.Agreements.Where(v => v.Id == agreementId).FirstOrDefault();
            string dateString;
            DateTime date;
            if (ConvertDateFromString(effectiveDate, out dateString, out date))
            {
                agreement.EffectiveDate = date;
            }
            if (ConvertDateFromString(expirationDate, out dateString, out date))
            {
                agreement.ExpirationDate = date;
            }

            //agreement.UserId = userId;
            agreement.ProductId = productId;
            agreement.ProductPrice = productPrice;
            agreement.NewPrice = newPrice;
            return agreement;
        }
        Agreement PopulateAgreementCreate(int? agreementId, string userId, int productId, DateTime effectiveDate, DateTime expirationDate, decimal productPrice, decimal newPrice)
        {
            Agreement agreement = new Agreement();
            agreement.UserId = userId;
            agreement.ProductId = productId;
            agreement.EffectiveDate = effectiveDate;
            agreement.ExpirationDate = expirationDate;
            agreement.ProductPrice = productPrice;
            agreement.NewPrice = newPrice;
            return agreement;
        }

        private bool ConvertDateFromString(string effectiveDate, out string dateString, out DateTime date)
        {
            dateString = $"{effectiveDate.Substring(6, 4)}-{effectiveDate.Substring(0, 2)}-{effectiveDate.Substring(3, 2)} 00:00 AM";
            try
            {
                date = DateTime.ParseExact(dateString, "yyyy-MM-dd HH:mm tt", null);
            }
            catch (Exception)
            {
                date = DateTime.MinValue;
                return false;
            }
            return true;
        }

        // GET: Agreements/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agreement agreement = await db.Agreements.FindAsync(id);
            if (agreement == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "ProductDescription", agreement.ProductId);
            return View(agreement);
        }

        // POST: Agreements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? agreementId, string UserId, int ProductId, string EffectiveDate, string ExpirationDate, decimal ProductPrice, decimal NewPrice)
        {
            Agreement agreement = PopulateAgreementEdit(agreementId, UserId, ProductId, EffectiveDate, ExpirationDate, ProductPrice, NewPrice);
            if (ModelState.IsValid)
            {
                db.Entry(agreement).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "ProductDescription", agreement.ProductId);
            return View(agreement);
        }

        // GET: Agreements/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agreement agreement = await db.Agreements.FindAsync(id);
            if (agreement == null)
            {
                return HttpNotFound();
            }
            return View(agreement);
        }

        // POST: Agreements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int agreementId)
        {
            Agreement agreement = await db.Agreements.FindAsync(agreementId);
            db.Agreements.Remove(agreement);
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
