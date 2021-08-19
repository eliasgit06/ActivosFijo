using ActivosFijo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ActivosFijo.Controllers
{
    public class LoginController : Controller
    {
        private ActivosFijosEntities db = new ActivosFijosEntities();

        // GET: Login
        public ActionResult Login()
        {
            if (Request.Cookies["usuario"] != null && Request.Cookies["clave"] != null)
            {
                ViewBag.usuarioLoged = Request.Cookies["usuario"].Value;
                ViewBag.claveLoged = Request.Cookies["clave"].Value;
                ViewBag.Checked = "checked";
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(string btnLogin)
        {
            //string btnClick = Request["btnLogin"];           

            if(btnLogin == "Login")
            {
                string usuario = Request["usuario"];
                string clave = Request["clave"];
                string checkedBx = Request["chkRecordar"];

                if (usuario == "" && clave == "")
                {
                    ViewBag.Error = "Debe ingresar un Usuario y contraseña";
                    return View();
                }
                else if(usuario == "")
                {
                    ViewBag.claveLoged = clave;
                    ViewBag.Usuario = "Debe ingresar un usuario";
                    return View();
                }else if(clave == "")
                {
                    ViewBag.usuarioLoged = usuario;
                    ViewBag.Clave = "Debe de ingresar su clave";
                    return View();
                }

                var login = (from tblUsuario in db.TblUsuarios where tblUsuario.cUsuario == usuario && tblUsuario.cClave == clave select tblUsuario).FirstOrDefault();

                if(login != null)
                {
                    if (checkedBx == "recordar")
                    {
                        Response.Cookies["usuario"].Value = usuario;
                        Response.Cookies["clave"].Value = clave;
                        Response.Cookies["usuario"].Expires = DateTime.Now.AddDays(30);
                        Response.Cookies["clave"].Expires = DateTime.Now.AddDays(30);
                    }
                    else
                    {
                        Response.Cookies["usuario"].Expires = DateTime.Now.AddDays(-30);
                        Response.Cookies["clave"].Expires = DateTime.Now.AddDays(-30);
                    }

                    Session["usuario"] = login.cUsuario;
                    Session["idUsuario"] = login.Id;
                    Session["nombreUsuario"] = login.cNombre;

                    var nombreCompleto = login.cNombre.Split(' ');
                    string primeraLetraNombre = nombreCompleto[0];
                    string primeraLetraApellido = nombreCompleto[1];
                    Session["Iniciales"] = primeraLetraNombre.Substring(0,1) + primeraLetraApellido.Substring(0, 1);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Usuario Incorrecto o contraseña incorrecta";
                    return View();
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }
    }
}