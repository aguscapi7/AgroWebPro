using AgroWebPro.Entidades.Consultas.Salida;
using AgroWebPro.Utilitarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgroWebPro.Web.Models
{
    public class AvanceTareaModels
    {
        public decimal kilogramosPrimera { get; set; }
        public decimal kilogramosSegunda { get; set; }
        public decimal kilogramosRechazo { get; set; }
        public string causaRechazo { get; set; }
        public Guid idUsuarioSupervisor { get; set; }
        public string codigoVerificadorSupervisor { get; set; }
        public List<EstadoTareaModels> listaEstadoTarea { get; set; }
        public List<TareaModels> listaTareaUsuario { get; set; }
        [Display(Name = "Tarea")]
        [Required(ErrorMessage = "Debe seleccionar la tarea")]
        public Guid? idTarea { get; set; }
        [Display(Name = "Estado de la tarea")]
        [Required(ErrorMessage = "Debe seleccionar el estado")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar el estado")]
        public int idEstadoTarea { get; set; }
        [Display(Name = "Horas")]
        [Required(ErrorMessage = "Las horas son requeridas")]
        [Range(0, int.MaxValue, ErrorMessage = "Las horas deben ser mayor a cero")]
        public decimal horas { get; set; }
        [Display(Name = "Observaciones")]
        [Required(ErrorMessage = "Las observaciones son requeridas")]
        public string observaciones { get; set; }

        public void CopiarEstadoTarea(ConsultarEstadoTareaResponse estadoTareaResponse)
        {
            try
            {
                listaEstadoTarea = new List<EstadoTareaModels>();
                if(estadoTareaResponse != null && estadoTareaResponse.estado.Equals(Constantes.EstadoCorrecto) && estadoTareaResponse.listaEstadoTarea.Count > 0)
                {
                    EstadoTareaModels estadoTarea = null;
                    foreach (var estadoTareaItem in estadoTareaResponse.listaEstadoTarea)
                    {
                        estadoTarea = new EstadoTareaModels();
                        estadoTarea.idEstadoTarea = estadoTareaItem.IdEstadoTarea;
                        estadoTarea.nombreEstadoTarea = estadoTareaItem.NombreEstadoTarea;
                        listaEstadoTarea.Add(estadoTarea);
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        public void CopiarTareasUsuario(ConsultarTareasUsuarioResponse tareasUsuarioResponse)
        {
            try
            {
                listaTareaUsuario = new List<TareaModels>();
                if(tareasUsuarioResponse != null && tareasUsuarioResponse.estado.Equals(Constantes.EstadoCorrecto) && tareasUsuarioResponse.listaTareasUsuario.Count > 0)
                {
                    TareaModels tarea = null;
                    foreach (var tareaItem in tareasUsuarioResponse.listaTareasUsuario)
                    {
                        tarea = new TareaModels();
                        tarea.idTarea = tareaItem.IdTarea;
                        tarea.resumen = tareaItem.Resumen;
                        tarea.nombreTerreno = tareaItem.NombreTerreno;
                        tarea.nombreTipoTarea = tareaItem.NombreTarea;
                        tarea.recoleccion = tareaItem.Recoleccion;
                        tarea.fechaInicio = tareaItem.FechaInicio.ToShortDateString();
                        tarea.fechaFinalizacion = tareaItem.FechaFinalizacion.ToShortDateString();
                        listaTareaUsuario.Add(tarea);
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
    }

    public class EstadoTareaModels
    {
        public int idEstadoTarea { get; set; }
        public string nombreEstadoTarea { get; set; }
    }
}