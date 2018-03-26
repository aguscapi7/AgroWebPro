using AgroWebPro.Entidades.Consultas.Salida;
using AgroWebPro.Utilitarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace AgroWebPro.Web.Models
{
    public class ReporteModels
    {
        [Display(Name = "Fecha de inicio")]
        public string fechaInicio { get; set; }
        [Display(Name = "Fecha de fin")]
        public string fechaFin { get; set; }
        public Guid idCultivo { get; set; }
        public Guid idEmpresa { get; set; }
        public List<VentaModels> listaVentas { get; set; }
        public List<TerrenoModels> listaTerrenosEmpresa { get; set; }
        [Display(Name = "Terreno")]
        public Guid? idTerreno { get; set; }
        public List<TareaModels> listaTareas { get; set; }

        public void CopiarReporteVentas(ConsultarReporteVentasResponse reporteVentasResponse)
        {

        }

        public void CopiarTerrenosEmpresa(ConsultarTerrenosEmpresaResponse terrenosEmpresaResponse)
        {
            try
            {
                listaTerrenosEmpresa = new List<TerrenoModels>();
                if (terrenosEmpresaResponse != null && terrenosEmpresaResponse.estado.Equals(Constantes.EstadoCorrecto) && terrenosEmpresaResponse.listaTerrenosEmpresa.Count >= 0)
                {
                    TerrenoModels terreno = null;
                    foreach (var terrenoItem in terrenosEmpresaResponse.listaTerrenosEmpresa)
                    {
                        terreno = new TerrenoModels();
                        terreno.idTerreno = terrenoItem.IdTerreno;
                        terreno.nombreTerreno = terrenoItem.NombreTerreno;
                        terreno.descripcionTerreno = terrenoItem.DescripcionTerreno;
                        terreno.listaCoordenadas = terrenoItem.Coordenadas;
                        terreno.nombreCultivo = terrenoItem.NombreCultivo;
                        terreno.coordenadas = terrenoItem.Coordenadas;
                        terreno.idCultivo = terrenoItem.IdCultivo;
                        listaTerrenosEmpresa.Add(terreno);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void CopiarReporteTareas(ConsultarReporteTareasResponse tareasResponse)
        {
            try
            {
                listaTareas = new List<TareaModels>();
                if(tareasResponse != null && tareasResponse.estado.Equals(Constantes.EstadoCorrecto) && tareasResponse.listaReporteTareas.Count > 0)
                {
                    TareaModels tarea = null;
                    foreach (var itemTarea in tareasResponse.listaReporteTareas)
                    {
                        tarea = new TareaModels();
                        tarea.fechaInicio = itemTarea.FechaInicio.ToString("dd/MM/yyyy");
                        tarea.fechaFinalizacion = itemTarea.FechaFinalizacion.ToString("dd/MM/yyyy");
                        tarea.resumen = itemTarea.Resumen;
                        tarea.nombreEmpleado = itemTarea.nombre + " " + itemTarea.apellidos;
                        tarea.horasEstimadas = itemTarea.HorasEstimadas;
                        tarea.horasReportadas = itemTarea.horasReportadas;
                        tarea.nombreTerreno = itemTarea.nombreTerreno;
                        tarea.nombreTipoTarea = itemTarea.NombreTarea;
                        tarea.idEstadoTarea = itemTarea.idEstadoTarea;
                        listaTareas.Add(tarea);
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
    
}