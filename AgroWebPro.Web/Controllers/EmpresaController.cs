using AgroWebPro.Entidades.Consultas.Entrada;
using AgroWebPro.Entidades.Consultas.Salida;
using AgroWebPro.LogicaNegocios.Interfaces;
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
    public class EmpresaController : BaseController
    {
        public ActionResult Mantenimiento()
        {
            EmpresaModels empresaModels = new EmpresaModels();
            ConsultarZonasHorariasRequest zonasHorariasRequest = null;
            ICatalogos catalogos = null;
            try
            {
                zonasHorariasRequest = new ConsultarZonasHorariasRequest();
                catalogos = new Catalogos();

                empresaModels.CopiarListaZonasHorarias(catalogos.ConsultarZonasHorarias(zonasHorariasRequest));

                
            }
            catch(Exception ex)
            {

                AgroWebPro.Utilitarios.Utilitarios.BitacoraErrores(ex.Message + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty),
                                                    "Error WEB: ",
                                                    this.GetType().Name,
                                                    System.Reflection.MethodInfo.GetCurrentMethod().Name);
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