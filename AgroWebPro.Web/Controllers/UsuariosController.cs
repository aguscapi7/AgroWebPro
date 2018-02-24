using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroWebPro.Web.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult Mantenimiento()
        {
            Session["menu-activo"] = "mantenimiento-usuarios";
            return View();
        }
    }
}