using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using ActivosFijo.Models;
using Mayur.Web.Attributes;
using PagedList;

namespace ActivosFijo.Controllers
{
    [SessionTimeout]
    public class TblActivoesController : Controller
    {
        private ActivosFijosEntities db = new ActivosFijosEntities();

        // GET: TblActivoes
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
                else if(!String.IsNullOrEmpty(busqueda))
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

        // GET: TblActivoes/Details/5
        public ActionResult Details(int? id)
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
        }

        // GET: TblActivoes/Create
        public ActionResult Create(string value)
        {
            ViewBag.CurrentFilter = value;
            ViewBag.IdCategoria = new SelectList(db.TblCategorias, "Id", "cDescripcion");
            ViewBag.IdLocacion = new SelectList(db.TblLocaciones, "Id", "cDescripcion");
            ViewBag.IdOrigen = new SelectList(db.TblOrigenes, "Id", "cDescripcion");
            ViewBag.IdTipoActivo = new SelectList(db.TblTiposActivos, "Id", "cDescripcion");
            ViewBag.IdPersona = new SelectList(db.TblPersonas, "Id", "cNombre");
            return View();
        }

        // POST: TblActivoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,cDescripcion,cDetalle,IdOrigen,IdCategoria,IdLocacion,IdTipoActivo,nValor,nDepreciacion,nValorLibro,cEstatus,dFechaActivo,cCuentaContable,cAsignacion,dfechaRegistro,cUsuario,cMarca,cModelo,cNoSerial,codigoOrigen,codigoBN,CodigoMINPRE,CodigoMINERD,CodigoDIGEPEP,FechaActualizacion,ActualizadoPor,IdPersona")] TblActivo tblActivo)
        {
            string noSerial = Request["cNoSerial"];
            var numero = db.TblActivoes.Where(serie => serie.cNoSerial == noSerial);
            if (!numero.Any())
            {
                if (ModelState.IsValid)
                {
                    db.TblActivoes.Add(tblActivo);
                    db.SaveChanges();
                    TempData["Message"] = "Formulario fue registrado con exito!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["Message"] = "Este número de serial "+ noSerial + " ya existe!";
                ViewBag.ErrorSerial = "Número de Serial ya existe";
            }

            ViewBag.IdCategoria = new SelectList(db.TblCategorias, "Id", "cDescripcion", tblActivo.IdCategoria);
            ViewBag.IdLocacion = new SelectList(db.TblLocaciones, "Id", "cDescripcion", tblActivo.IdLocacion);
            ViewBag.IdOrigen = new SelectList(db.TblOrigenes, "Id", "cDescripcion", tblActivo.IdOrigen);
            ViewBag.IdTipoActivo = new SelectList(db.TblTiposActivos, "Id", "cDescripcion", tblActivo.IdTipoActivo);
            ViewBag.IdPersona = new SelectList(db.TblPersonas, "Id", "cNombre", tblActivo.IdPersona);
            return View(tblActivo);
        }

        // GET: TblActivoes/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.IdLocacion = new SelectList(db.TblLocaciones, "Id", "cDescripcion", tblActivo.IdLocacion);
            ViewBag.IdOrigen = new SelectList(db.TblOrigenes, "Id", "cDescripcion", tblActivo.IdOrigen);
            ViewBag.IdTipoActivo = new SelectList(db.TblTiposActivos, "Id", "cDescripcion", tblActivo.IdTipoActivo);
            ViewBag.IdPersona = new SelectList(db.TblPersonas, "Id", "cNombre", tblActivo.IdPersona);
            return View(tblActivo);
        }

        // POST: TblActivoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,cDescripcion,cDetalle,IdOrigen,IdCategoria,IdLocacion,IdTipoActivo,nValor,nDepreciacion,nValorLibro,cEstatus,dFechaActivo,cCuentaContable,cAsignacion,dfechaRegistro,cUsuario,cMarca,cModelo,cNoSerial,codigoOrigen,codigoBN,CodigoMINPRE,CodigoMINERD,CodigoDIGEPEP,FechaActualizacion,ActualizadoPor,IdPersona")] TblActivo tblActivo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblActivo).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Formulario fue editado con exito!";
                return RedirectToAction("Index");
            }
            ViewBag.IdCategoria = new SelectList(db.TblCategorias, "Id", "cDescripcion", tblActivo.IdCategoria);
            ViewBag.IdLocacion = new SelectList(db.TblLocaciones, "Id", "cDescripcion", tblActivo.IdLocacion);
            ViewBag.IdOrigen = new SelectList(db.TblOrigenes, "Id", "cDescripcion", tblActivo.IdOrigen);
            ViewBag.IdTipoActivo = new SelectList(db.TblTiposActivos, "Id", "cDescripcion", tblActivo.IdTipoActivo);
            ViewBag.IdPersona = new SelectList(db.TblPersonas, "Id", "cNombre", tblActivo.IdPersona);
            return View(tblActivo);
        }

        // GET: TblActivoes/Delete/5
        public ActionResult Delete(int? id)
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
        }

        // POST: TblActivoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TblActivo tblActivo = db.TblActivoes.Find(id);
            db.TblActivoes.Remove(tblActivo);
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
