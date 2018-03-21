using AgroWebPro.Utilitarios;
using AgroWebPro.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroWebPro.Web.Controllers
{
    public class VentaController : BaseController
    {
        public ActionResult CrearVenta()
        {
            Session[Constantes.MenuActivo] = Constantes.MenuVenta;
            return View();
        }

        [HttpPost]
        public ActionResult CrearVenta(VentaModels ventaModels)
        {
            return View(ventaModels);
        }

        public ActionResult MantenimientoClienteProveedor()
        {
            Session[Constantes.MenuActivo] = Constantes.MenuClienteProveedor;
            return View();
        }

        [HttpPost]
        public ActionResult MantenimientoClienteProveedor(ClienteProveedorModels clienteProveedorModels)
        {
            return View();
        }
    }
}