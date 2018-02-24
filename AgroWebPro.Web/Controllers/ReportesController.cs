using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroWebPro.Web.Controllers
{
    public class ReportesController : Controller
    {
        public ActionResult Resumen()
        {
            Session["menu-activo"] = "reporte";
            return View();
        }
        
        public ActionResult Ventas()
        {
            Session["menu-activo"] = "reporte";
            return View();
        }

        public ActionResult Cosechas()
        {
            Session["menu-activo"] = "reporte";
            return View();
        }

        public ActionResult Tareas()
        {
            Session["menu-activo"] = "reporte";
            return View();
        }

    }
}