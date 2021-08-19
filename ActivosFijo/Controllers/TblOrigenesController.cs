using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ActivosFijo.Models;
using Mayur.Web.Attributes;

namespace ActivosFijo.Controllers
{
    [SessionTimeout]
    public class TblOrigenesController : Controller
    {
        private ActivosFijosEntities db = new ActivosFijosEntities();

        // GET: TblOrigenes
        public ActionResult Index()
        {
            return View(db.TblOrigenes.ToList());
        }

        // GET: TblOrigenes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblOrigene tblOrigene = db.TblOrigenes.Find(id);
            if (tblOrigene == null)
            {
                return HttpNotFound();
            }
            return View(tblOrigene);
        }

        // GET: TblOrigenes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TblOrigenes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,cDescripcion")] TblOrigene tblOrigene)
        {
            if (ModelState.IsValid)
            {
                db.TblOrigenes.Add(tblOrigene);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblOrigene);
        }

        // GET: TblOrigenes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblOrigene tblOrigene = db.TblOrigenes.Find(id);
            if (tblOrigene == null)
            {
                return HttpNotFound();
            }
            return View(tblOrigene);
        }

        // POST: TblOrigenes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,cDescripcion")] TblOrigene tblOrigene)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblOrigene).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblOrigene);
        }

        // GET: TblOrigenes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblOrigene tblOrigene = db.TblOrigenes.Find(id);
            if (tblOrigene == null)
            {
                return HttpNotFound();
            }
            return View(tblOrigene);
        }

        // POST: TblOrigenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TblOrigene tblOrigene = db.TblOrigenes.Find(id);
            db.TblOrigenes.Remove(tblOrigene);
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
