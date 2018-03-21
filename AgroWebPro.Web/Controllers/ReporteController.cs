using AgroWebPro.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroWebPro.Web.Controllers
{
    public class ReporteController : BaseController
    {
        public ActionResult Resumen()
        {
            Session[Constantes.MenuActivo] = Constantes.MenuReportes;
            return View();
        }
        
        public ActionResult Ventas()
        {
            Session[Constantes.MenuActivo] = Constantes.MenuReportes;
            return View();
        }

        public ActionResult General()
        {
            Session[Constantes.MenuActivo] = Constantes.MenuReportes;
            return View();
        }

        public ActionResult Cosechas()
        {
            Session[Constantes.MenuActivo] = Constantes.MenuReportes;
            return View();
        }

        public ActionResult Tareas()
        {
            Session[Constantes.MenuActivo] = Constantes.MenuReportes;
            return View();
        }

    }
}