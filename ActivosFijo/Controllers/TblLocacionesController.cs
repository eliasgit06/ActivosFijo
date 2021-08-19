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
using PagedList;

namespace ActivosFijo.Controllers
{
    [SessionTimeout]
    public class TblLocacionesController : Controller
    {
        private ActivosFijosEntities db = new ActivosFijosEntities();

        // GET: TblLocaciones
        public ActionResult Index(string busqueda, int? page)
        {
            var tblLocaciones = db.TblLocaciones.AsQueryable();

            if (!String.IsNullOrEmpty(busqueda))
            {
                tblLocaciones = tblLocaciones.Where(buscar => buscar.cDescripcion.Contains(busqueda));
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);

            return View(tblLocaciones.ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: TblLocaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblLocacione tblLocacione = db.TblLocaciones.Find(id);
            if (tblLocacione == null)
            {
                return HttpNotFound();
            }
            return View(tblLocacione);
        }

        // GET: TblLocaciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TblLocaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,cDescripcion")] TblLocacione tblLocacione)
        {
            if (ModelState.IsValid)
            {
                db.TblLocaciones.Add(tblLocacione);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblLocacione);
        }

        // GET: TblLocaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblLocacione tblLocacione = db.TblLocaciones.Find(id);
            if (tblLocacione == null)
            {
                return HttpNotFound();
            }
            return View(tblLocacione);
        }

        // POST: TblLocaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,cDescripcion")] TblLocacione tblLocacione)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblLocacione).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblLocacione);
        }

        // GET: TblLocaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblLocacione tblLocacione = db.TblLocaciones.Find(id);
            if (tblLocacione == null)
            {
                return HttpNotFound();
            }
            return View(tblLocacione);
        }

        // POST: TblLocaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TblLocacione tblLocacione = db.TblLocaciones.Find(id);
            db.TblLocaciones.Remove(tblLocacione);
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
