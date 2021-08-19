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
    public class TblTiposActivoesController : Controller
    {
        private ActivosFijosEntities db = new ActivosFijosEntities();

        // GET: TblTiposActivoes
        public ActionResult Index()
        {
            return View(db.TblTiposActivos.ToList());
        }

        // GET: TblTiposActivoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblTiposActivo tblTiposActivo = db.TblTiposActivos.Find(id);
            if (tblTiposActivo == null)
            {
                return HttpNotFound();
            }
            return View(tblTiposActivo);
        }

        // GET: TblTiposActivoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TblTiposActivoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,cDescripcion,nTasaDepreciacion")] TblTiposActivo tblTiposActivo)
        {
            if (ModelState.IsValid)
            {
                db.TblTiposActivos.Add(tblTiposActivo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblTiposActivo);
        }

        // GET: TblTiposActivoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblTiposActivo tblTiposActivo = db.TblTiposActivos.Find(id);
            if (tblTiposActivo == null)
            {
                return HttpNotFound();
            }
            return View(tblTiposActivo);
        }

        // POST: TblTiposActivoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,cDescripcion,nTasaDepreciacion")] TblTiposActivo tblTiposActivo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblTiposActivo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblTiposActivo);
        }

        // GET: TblTiposActivoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblTiposActivo tblTiposActivo = db.TblTiposActivos.Find(id);
            if (tblTiposActivo == null)
            {
                return HttpNotFound();
            }
            return View(tblTiposActivo);
        }

        // POST: TblTiposActivoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TblTiposActivo tblTiposActivo = db.TblTiposActivos.Find(id);
            db.TblTiposActivos.Remove(tblTiposActivo);
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
