using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ActivosFijo.Models;
using Mayur.Web.Attributes;
using PagedList;
using System.Web.WebPages;

namespace ActivosFijo.Controllers
{
    [SessionTimeout]
    public class TblMovimientoesController : Controller
    {
        private ActivosFijosEntities db = new ActivosFijosEntities();

        // GET: TblMovimientoes
        public ActionResult Index(string currentFilter, string busqueda, int? page, string name)
        {
            if (busqueda != null)
            {
                page = 1;
            }
            else
            {
                busqueda = currentFilter;
            }

            ViewBag.CurrentFilter = busqueda;

            if (!String.IsNullOrEmpty(busqueda) || !String.IsNullOrEmpty(name) || page > 1)
            {
                var tblActivoes = db.TblActivoes.Include(t => t.TblCategoria).Include(t => t.TblLocacione).Include(t => t.TblOrigene).Include(t => t.TblTiposActivo).Include(t => t.TblPersona);

                if (busqueda.IsInt())
                {
                    int id = Convert.ToInt32(busqueda);
                    tblActivoes = tblActivoes.Where(buscar => buscar.Id == id || buscar.codigoBN.Contains(busqueda) || buscar.codigoOrigen.Contains(busqueda) || buscar.cNoSerial.Contains(busqueda));
                }
                else if (busqueda.IsDateTime())
                {
                    busqueda = Convert.ToDateTime(busqueda).ToString("yyyy-dd-MM");
                    DateTime fechaActivo = Convert.ToDateTime(busqueda);
                    tblActivoes = tblActivoes.Where(buscar => DbFunctions.TruncateTime(buscar.dFechaActivo) == fechaActivo);
                }
                else if (!String.IsNullOrEmpty(busqueda))
                {
                    tblActivoes = tblActivoes.Where(buscar => buscar.cDescripcion.Contains(busqueda) || buscar.cMarca.Contains(busqueda) || buscar.cModelo.Contains(busqueda)
                                                                || buscar.cNoSerial.Contains(busqueda) || buscar.codigoOrigen.Contains(busqueda) || buscar.codigoBN.Contains(busqueda)
                                                                || buscar.TblCategoria.cDescripcion.Contains(busqueda) || buscar.TblOrigene.cDescripcion.Contains(busqueda)
                                                                || buscar.TblLocacione.cDescripcion.Contains(busqueda) || buscar.cAsignacion.Contains(busqueda));
                }

                int pageSize = 20;
                int pageNumber = (page ?? 1);
                //return View(tblActivoes.ToPagedList(pageNumber, pageSize));
                return View(tblActivoes.ToList().ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return View();
            }
        }

        // GET: TblMovimientoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblMovimiento tblMovimiento = db.TblMovimientos.Find(id);
            if (tblMovimiento == null)
            {
                return HttpNotFound();
            }
            return View(tblMovimiento);
        }

        // GET: TblMovimientoes/Details/5
        public ActionResult AsignarPersona(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblActivo tblActivo = db.TblActivoes.Find(id);
            if (tblActivo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPersona = new SelectList(db.TblPersonas, "Id", "cNombre", tblActivo.IdPersona);
            return View(tblActivo);
            //return View();
        }

        [HttpPost, ActionName("AsignarPersona")]
        public ActionResult AsignarPersonapost(int? id, string cAsignacion, int? IdPersona, DateTime? fechamovimiento, string usuario, string IdLocacion)
        {
            TblActivo tblActivo = db.TblActivoes.Find(id);
            string[] valores = new string[] { "cAsignacion", "IdPersona" };

            if (TryUpdateModel(tblActivo, valores))
            {
                try
                {
                    db.SaveChanges(); 
                    TempData["Message"] = "Formulario fue editado con exito!";
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    TempData["Message"] = "Oops, Algo salió mal!";
                }
            }

            ViewBag.IdPersona = new SelectList(db.TblPersonas, "Id", "cNombre", tblActivo.IdPersona);
            return View(tblActivo);
        }

        public ActionResult AsignarLocacion(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblActivo tblActivo = db.TblActivoes.Find(id);
            if (tblActivo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdLocacion = new SelectList(db.TblLocaciones, "Id", "cDescripcion", tblActivo.IdLocacion);
            return View(tblActivo);
            //return View();
        }

        [HttpPost]
        public ActionResult AsignarLocacion(int? id, int IdLocacion)
        {
            TblActivo tblActivo = db.TblActivoes.Find(id);
            string[] valores = new string[] { "IdLocacion" };

            if (TryUpdateModel(tblActivo, valores))
            {
                try
                {
                    db.SaveChanges();
                    TempData["Message"] = "Formulario fue editado con exito!";
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    TempData["Message"] = "Oops, Algo salió mal!";
                }
            }
            ViewBag.IdLocacion = new SelectList(db.TblLocaciones, "Id", "cDescripcion", tblActivo.IdLocacion);
            return View(tblActivo);
            //return View();
        }

        public ActionResult AsignarCategoria(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblActivo tblActivo = db.TblActivoes.Find(id);
            if (tblActivo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCategoria = new SelectList(db.TblCategorias, "Id", "cDescripcion", tblActivo.IdCategoria);
            return View(tblActivo);
            //return View();
        }

        [HttpPost]
        public ActionResult AsignarCategoria(int? id, int IdCategoria)
        {
            TblActivo tblActivo = db.TblActivoes.Find(id);
            string[] valores = new string[] { "IdCategoria" };

            if (TryUpdateModel(tblActivo, valores))
            {
                try
                {
                    db.SaveChanges();
                    TempData["Message"] = "Formulario fue editado con exito!";
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    TempData["Message"] = "Oops, Algo salió mal!";
                }
            }
            ViewBag.IdCategoria = new SelectList(db.TblCategorias, "Id", "cDescripcion", tblActivo.IdCategoria);
            return View(tblActivo);
            //return View();
        }

        public ActionResult DebajaalActivo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblActivo tblActivo = db.TblActivoes.Find(id);
            if (tblActivo == null)
            {
                return HttpNotFound();
            }
            return View(tblActivo);
            //return View();
        }

        [HttpPost]
        public ActionResult DebajaalActivo(int? id, int cEstatus)
        {
            TblActivo tblActivo = db.TblActivoes.Find(id);
            string[] valores = new string[] { "cEstatus" };

            if (TryUpdateModel(tblActivo, valores))
            {
                try
                {
                    db.SaveChanges();
                    TempData["Message"] = "Formulario fue editado con exito!";
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    TempData["Message"] = "Oops, Algo salió mal!";
                }
            }
            return View(tblActivo);
            //return View();
        }
        // GET: TblMovimientoes/Create
        public ActionResult Create()
        {
            ViewBag.IdCategoria = new SelectList(db.TblCategorias, "Id", "cDescripcion");
            ViewBag.IdLocacion = new SelectList(db.TblLocaciones, "Id", "cDescripcion");
            ViewBag.IdTipoBaja = new SelectList(db.TblTipoBajas, "Id", "cDescripcion");
            ViewBag.IdTipoActivo = new SelectList(db.TblTiposActivos, "Id", "cDescripcion");
            ViewBag.IdPersona = new SelectList(db.TblPersonas, "Id", "cDocumento");
            return View();
        }

        // POST: TblMovimientoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,nTipo,cDescripcion,dFechaMovimiento,nMontoMovimiento,dFechaRegistro,cUsuario,IdActivo,cAsignacion,IdLocacion,IdTipoActivo,IdTipoBaja,IdCategoria,IdPersona")] TblMovimiento tblMovimiento)
        {
            if (ModelState.IsValid)
            {
                db.TblMovimientos.Add(tblMovimiento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCategoria = new SelectList(db.TblCategorias, "Id", "cDescripcion", tblMovimiento.IdCategoria);
            ViewBag.IdLocacion = new SelectList(db.TblLocaciones, "Id", "cDescripcion", tblMovimiento.IdLocacion);
            ViewBag.IdTipoBaja = new SelectList(db.TblTipoBajas, "Id", "cDescripcion", tblMovimiento.IdTipoBaja);
            ViewBag.IdTipoActivo = new SelectList(db.TblTiposActivos, "Id", "cDescripcion", tblMovimiento.IdTipoActivo);
            ViewBag.IdPersona = new SelectList(db.TblPersonas, "Id", "cDocumento", tblMovimiento.IdPersona);
            return View(tblMovimiento);
        }

        // GET: TblMovimientoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblMovimiento tblMovimiento = db.TblMovimientos.Find(id);
            if (tblMovimiento == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCategoria = new SelectList(db.TblCategorias, "Id", "cDescripcion", tblMovimiento.IdCategoria);
            ViewBag.IdLocacion = new SelectList(db.TblLocaciones, "Id", "cDescripcion", tblMovimiento.IdLocacion);
            ViewBag.IdTipoBaja = new SelectList(db.TblTipoBajas, "Id", "cDescripcion", tblMovimiento.IdTipoBaja);
            ViewBag.IdTipoActivo = new SelectList(db.TblTiposActivos, "Id", "cDescripcion", tblMovimiento.IdTipoActivo);
            ViewBag.IdPersona = new SelectList(db.TblPersonas, "Id", "cDocumento", tblMovimiento.IdPersona);
            return View(tblMovimiento);
        }

        // POST: TblMovimientoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,nTipo,cDescripcion,dFechaMovimiento,nMontoMovimiento,dFechaRegistro,cUsuario,IdActivo,cAsignacion,IdLocacion,IdTipoActivo,IdTipoBaja,IdCategoria,IdPersona")] TblMovimiento tblMovimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblMovimiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCategoria = new SelectList(db.TblCategorias, "Id", "cDescripcion", tblMovimiento.IdCategoria);
            ViewBag.IdLocacion = new SelectList(db.TblLocaciones, "Id", "cDescripcion", tblMovimiento.IdLocacion);
            ViewBag.IdTipoBaja = new SelectList(db.TblTipoBajas, "Id", "cDescripcion", tblMovimiento.IdTipoBaja);
            ViewBag.IdTipoActivo = new SelectList(db.TblTiposActivos, "Id", "cDescripcion", tblMovimiento.IdTipoActivo);
            ViewBag.IdPersona = new SelectList(db.TblPersonas, "Id", "cDocumento", tblMovimiento.IdPersona);
            return View(tblMovimiento);
        }

        // GET: TblMovimientoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblMovimiento tblMovimiento = db.TblMovimientos.Find(id);
            if (tblMovimiento == null)
            {
                return HttpNotFound();
            }
            return View(tblMovimiento);
        }

        // POST: TblMovimientoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TblMovimiento tblMovimiento = db.TblMovimientos.Find(id);
            db.TblMovimientos.Remove(tblMovimiento);
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
