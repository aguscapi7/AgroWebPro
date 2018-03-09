using AgroWebPro.Entidades.Consultas.Entrada;
using AgroWebPro.Entidades.Consultas.Salida;
using AgroWebPro.LogicaNegocios.Metodos;
using AgroWebPro.Utilitarios;
using AgroWebPro.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroWebPro.Web.Controllers
{
    public class EmpresaController : Controller
    {
        public ActionResult Mantenimiento()
        {
            EmpresaModels empresaModels = new EmpresaModels();
            ConsultarZonasHorariasRequest zonasHorariasRequest = null;
            Consultas consultas = null;
            try
            {
                zonasHorariasRequest = new ConsultarZonasHorariasRequest();
                consultas = new Consultas();

                empresaModels.CopiarListaZonasHorarias(consultas.ConsultarZonasHorarias(zonasHorariasRequest));

                
            }
            catch(Exception ex)
            {

            }
            return View(empresaModels);
        }

        public ActionResult Terrenos()
        {
            Session[Constantes.MenuActivo] = Constantes.MenuTerreno;
            return View();
        }
    }
}