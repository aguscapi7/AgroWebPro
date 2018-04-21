using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroWebPro.Web.Controllers
{
    public class InventarioController : Controller
    {
        // GET: Inventario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Mantenimiento()
        {
            return View();
        }
    }
}