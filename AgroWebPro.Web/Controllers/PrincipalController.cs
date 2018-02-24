using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroWebPro.Web.Controllers
{
    public class PrincipalController : Controller
    {
        public ActionResult Inicio()
        {
            return View();
        }

        public ActionResult Mapa()
        {
            Session["menu-activo"] = "terreno";
            return View();
        }

        public ActionResult Cultivo()
        {
            Session["menu-activo"] = "cultivo";
            return View();
        }
    }
}
