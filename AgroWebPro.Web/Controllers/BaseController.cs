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
    public class BaseController : Controller
    {
        public bool ConsultarOpcionPorUsuario(string controlador, string accion)
        {
            UsuarioModels usuario = new UsuarioModels();
            ConsultarOpcionesRolRequest opcionesRolRequest = null;
            ConsultarOpcionesRolResponse opcionesRolResponse = null;
            ISeguridad seguridad = new Seguridad();
            bool valido = false;
            try
            {
                string idRol = Request.Cookies["usuario"]["rol"];
                if (!string.IsNullOrEmpty(idRol))
                {
                    opcionesRolRequest = new ConsultarOpcionesRolRequest();
                    opcionesRolRequest.idRol = Guid.Parse(idRol);
                    opcionesRolResponse = seguridad.ConsultarOpcionesRol(opcionesRolRequest);

                    if(opcionesRolResponse != null && opcionesRolResponse.estado.Equals(Constantes.EstadoCorrecto) && opcionesRolResponse.listaOpcionesRol.Count > 0)
                    {
                        valido = opcionesRolResponse.listaOpcionesRol.Where(x => x.controlador == controlador && x.accion == accion).Count() > 0;
                    }

                }
            }
            catch(Exception ex)
            {

            }
            return valido;
        }
        

    }
}