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
using System.Configuration;
using Microsoft.Reporting.WebForms;

namespace ActivosFijo.Controllers
{
    [SessionTimeout]
    public class TblPersonasController : Controller
    {
        private ActivosFijosEntities db = new ActivosFijosEntities();

        // GET: TblPersonas
        public ActionResult Index(string busqueda, int? page)
        {

            var tblPersonas = db.TblPersonas.AsQueryable();

            if (!String.IsNullOrEmpty(busqueda))
            {
                tblPersonas = tblPersonas.Where(buscar => buscar.cNombre.Contains(busqueda));
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);

            return View(tblPersonas.ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: TblPersonas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblPersona tblPersona = db.TblPersonas.Find(id);
            if (tblPersona == null)
            {
                return HttpNotFound();
            }
            return View(tblPersona);
        }

        // GET: TblPersonas/Create
        public ActionResult Create()
        {
            ViewBag.municipioid = new SelectList(db.Municipios, "Id", "Nombre");
            ViewBag.IdPlan = new SelectList(db.Planes, "Id", "Plan");
            ViewBag.provinciaid = new SelectList(db.Provincias, "Id", "Nombre");
            return View();
        }

        // POST: TblPersonas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,cDocumento,cNombre,cApellido,cSexo,FechaNacimiento,Correo,IdPlan,RegistradoPor,FechaRegistro,provinciaid,provincia,municipioid,municipio,telefono,Cargo")] TblPersona tblPersona)
        {
            if (ModelState.IsValid)
            {
                db.TblPersonas.Add(tblPersona);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.municipioid = new SelectList(db.Municipios, "Id", "MunicipioId", tblPersona.municipioid);
            ViewBag.IdPlan = new SelectList(db.Planes, "Id", "Plan", tblPersona.IdPlan);
            ViewBag.provinciaid = new SelectList(db.Provincias, "Id", "ProvinciaId", tblPersona.provinciaid);
            return View(tblPersona);
        }

        // GET: TblPersonas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblPersona tblPersona = db.TblPersonas.Find(id);
            if (tblPersona == null)
            {
                return HttpNotFound();
            }
            ViewBag.municipioid = new SelectList(db.Municipios, "Id", "MunicipioId", tblPersona.municipioid);
            ViewBag.IdPlan = new SelectList(db.Planes, "Id", "Plan", tblPersona.IdPlan);
            ViewBag.provinciaid = new SelectList(db.Provincias, "Id", "ProvinciaId", tblPersona.provinciaid);
            return View(tblPersona);
        }

        // POST: TblPersonas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,cDocumento,cNombre,cApellido,cSexo,FechaNacimiento,Correo,IdPlan,RegistradoPor,FechaRegistro,provinciaid,provincia,municipioid,municipio,telefono,Cargo")] TblPersona tblPersona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblPersona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.municipioid = new SelectList(db.Municipios, "Id", "MunicipioId", tblPersona.municipioid);
            ViewBag.IdPlan = new SelectList(db.Planes, "Id", "Plan", tblPersona.IdPlan);
            ViewBag.provinciaid = new SelectList(db.Provincias, "Id", "ProvinciaId", tblPersona.provinciaid);
            return View(tblPersona);
        }

        // GET: TblPersonas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblPersona tblPersona = db.TblPersonas.Find(id);
            if (tblPersona == null)
            {
                return HttpNotFound();
            }
            return View(tblPersona);
        }

        // POST: TblPersonas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TblPersona tblPersona = db.TblPersonas.Find(id);
            db.TblPersonas.Remove(tblPersona);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Report()
        {
            string usuario = ConfigurationManager.AppSettings["ReportServerUser"];
            string clave = ConfigurationManager.AppSettings["ReportServerPassword"];
            string reportServerUrl = ConfigurationManager.AppSettings["ReportServerUrl"];
            Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            viewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            viewer.SizeToReportContent = true;
            viewer.AsyncRendering = true;
            viewer.ServerReport.ReportServerUrl = new Uri(reportServerUrl);
            viewer.ServerReport.ReportPath = "/report/ActivoFijo/rptActaEntregaActivosFijos";
            viewer.ServerReport.ReportServerCredentials = new Microsoft.Reporting.WebForms.ReportServerCredentials(usuario, clave, "http://13.65.93.192/Reports");

            List<ReportParameter> _listPrmNoSolicitud = new List<ReportParameter>();
            ReportParameter prmNoSolicitud = new ReportParameter();
            prmNoSolicitud.Name = "AsignadoA";
            prmNoSolicitud.Values.Add("ABDIAS ROSARIO");
            _listPrmNoSolicitud.Add(prmNoSolicitud);
            //viewer.ServerReport.SetParameters(_listPrmNoSolicitud);
            ViewBag.ReportViewer = viewer;
            /*
            CustomReportCredentials credenciales = new CustomReportCredentials(usuario, clave, reportDomain);
            string serverURL = "http://13.65.93.192/Reports/";
            string reportPath = "";

            Reporter reporter = new Reporter(viewer, serverURL, reportPath, credenciales);

           

            reporter.SetParametros(valoresReporteParametro);
            

            ViewBag.ReportViewer = reporter.GetReportViewer();*/


            return View();
        }

        public class Reporter
        {
            private ReportViewer _report = null;
            List<ReportParameter> _listPrmNoSolicitud = null;
            public Reporter(ReportViewer report, string serverURL, string reportPath, CustomReportCredentials customerReportCredentials)
            {
                _report = report;
                _report.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = customerReportCredentials;
                _report.ServerReport.ReportServerCredentials = irsc;
                _report.ServerReport.ReportServerUrl = new Uri(serverURL);
                _report.ServerReport.ReportPath = reportPath;
            }

            public void SetParametros(Dictionary<string, List<string>> parametros)
            {


                _listPrmNoSolicitud = new List<ReportParameter>();

                foreach (string k in parametros.Keys)
                {
                    ReportParameter prmNoSolicitud = new ReportParameter();
                    prmNoSolicitud.Name = k;
                    foreach (string v in parametros[k])
                    {
                        prmNoSolicitud.Values.Add(v);
                    }

                    _listPrmNoSolicitud.Add(prmNoSolicitud);
                }

                _report.ServerReport.SetParameters(_listPrmNoSolicitud);
            }


            public void RunReport()
            {
                _report.ServerReport.Refresh();
                _report.DataBind();
                _report.Focus();


            }

            public ReportViewer GetReportViewer()
            { 
                return _report;
            }
        }

        public class CustomReportCredentials : IReportServerCredentials
        {
            private string _UserName;
            private string _PassWord;
            private string _DomainName;

            public CustomReportCredentials(string UserName, string PassWord, string DomainName)
            {
                _UserName = UserName;
                _PassWord = PassWord;
                _DomainName = DomainName;
            }

            public System.Security.Principal.WindowsIdentity ImpersonationUser
            {
                get { return null; }
            }

            public ICredentials NetworkCredentials
            {
                get { return new NetworkCredential(_UserName, _PassWord, _DomainName); }
            }

            public bool GetFormsCredentials(out Cookie authCookie, out string user,
             out string password, out string authority)
            {
                authCookie = new System.Net.Cookie(".ASPXAUTH", ".ASPXAUTH", "/", "Domain"); ;
                user = _UserName;
                password = _PassWord;
                authority = _DomainName;
                return false;
            }
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
