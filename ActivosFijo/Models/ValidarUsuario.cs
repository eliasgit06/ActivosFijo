using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActivosFijo.Models
{
    public class ValidarUsuario
    {

        public static bool EsUsuarioValido()
        {
            HttpContext context = HttpContext.Current;

            if(context.Session["idUsuario"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}