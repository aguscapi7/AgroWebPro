using AgroWebPro.Entidades.Consultas.Salida;
using AgroWebPro.Utilitarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgroWebPro.Web.Models
{
    public class EmpresaModels
    {
        public Guid idEmpresa { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es requerido")]
        public string nombreEmpresa { get; set; }
        [Display(Name = "Descripción")]
        public string descripcionEmpresa { get; set; }
        [Display(Name = "Teléfono")]
        public string telefono { get; set; }
        [Display(Name = "Cédula Jurídica")]
        public string cedulaJuridica { get; set; }
        [Display(Name = "Dirección")]
        public string direccion { get; set; }
        [Display(Name = "Zona horaria")]
        [Required(ErrorMessage = "Seleccione una zona")]
        public Guid? idZonaHoraria { get; set; }

        public UsuarioModels usuario { get; set; }

        public List<ZonaHoraria> listaZonasHorarias { get; set; }
        public List<TerrenoModels> listaTerrenosEmpresa { get; set; }
        public string coordenadasTerrenos { get; set; }
        public Guid idTerreno { get; set; }
        public List<TareaModels> listaTareasUsuario { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }

        public void CopiarListaZonasHorarias(ConsultarZonasHorariasResponse zonasHorariasResponse)
        {
            listaZonasHorarias = new List<ZonaHoraria>();
            if(zonasHorariasResponse != null && zonasHorariasResponse.estado.Equals(Constantes.EstadoCorrecto) && zonasHorariasResponse.listaZonasHorarias.Count() > 0)
            {
                ZonaHoraria zona = new ZonaHoraria()
                {
                    idZonaHoraria = Guid.Empty,
                    zonaHoraria = "Seleccione zona horaria"
                };
                listaZonasHorarias.Add(zona);
                foreach (var item in zonasHorariasResponse.listaZonasHorarias)
                {
                    zona = new ZonaHoraria();
                    zona.zonaHoraria = item.ZonaHoraria;
                    zona.idZonaHoraria = item.IdZonaHoraria;
                    listaZonasHorarias.Add(zona);
                }
                
            }
        }

        public void CopiarTerrenosEmpresa(ConsultarTerrenosEmpresaResponse cultivosEmpresaResponse)
        {
            try
            {
                listaTerrenosEmpresa = new List<TerrenoModels>();
                if (cultivosEmpresaResponse != null && cultivosEmpresaResponse.estado.Equals(Constantes.EstadoCorrecto) && cultivosEmpresaResponse.listaTerrenosEmpresa.Count >= 0)
                {
                    TerrenoModels terreno = null;
                    int contador = 0;
                    foreach (var terrenoItem in cultivosEmpresaResponse.listaTerrenosEmpresa)
                    {
                        terreno = new TerrenoModels();
                        terreno.idTerreno = terrenoItem.IdTerreno;
                        terreno.nombreTerreno = terrenoItem.NombreTerreno;
                        terreno.descripcionTerreno = terrenoItem.DescripcionTerreno;
                        terreno.listaCoordenadas = terrenoItem.Coordenadas;
                        terreno.nombreCultivo = terrenoItem.NombreCultivo;
                        terreno.coordenadas = terrenoItem.Coordenadas;
                        terreno.idCultivo = terrenoItem.IdCultivo;
                        coordenadasTerrenos += terrenoItem.Coordenadas + "|";
                        contador++;
                        listaTerrenosEmpresa.Add(terreno);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void CopiarTareasUsuario(ConsultarTareasUsuarioResponse tareasUsuarioResponse)
        {
            try
            {
                listaTareasUsuario = new List<TareaModels>();
                if (tareasUsuarioResponse != null && tareasUsuarioResponse.estado.Equals(Constantes.EstadoCorrecto) && tareasUsuarioResponse.listaTareasUsuario.Count > 0)
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
                        tarea.horasEstimadas = tareaItem.HorasEstimadas;
                        tarea.observaciones = tareaItem.Observaciones;
                        tarea.listaCoordenadas = tareaItem.CoordenadasTarea;
                        tarea.listaCoordenadasTerreno = tareaItem.CoordenadasTerreno;
                        tarea.idTipoTarea = tareaItem.IdTipoTarea;
                        listaTareasUsuario.Add(tarea);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void CopiarEmpresa(ConsultarEmpresaResponse empresaResponse)
        {
            try
            {
                if(empresaResponse != null && empresaResponse.estado.Equals(Constantes.EstadoCorrecto) && empresaResponse.listaEmpresa.Count > 0)
                {
                    this.nombreEmpresa = empresaResponse.listaEmpresa[0].NombreEmpresa;
                    this.descripcionEmpresa = empresaResponse.listaEmpresa[0].DescripcionEmpresa;
                    this.telefono = empresaResponse.listaEmpresa[0].Telefono;
                    this.cedulaJuridica = empresaResponse.listaEmpresa[0].CedulaJuridica;
                    this.idZonaHoraria = empresaResponse.listaEmpresa[0].IdZonaHoraria;
                    this.latitud = empresaResponse.listaEmpresa[0].Latitud;
                    this.longitud = empresaResponse.listaEmpresa[0].Longitud;
                    this.idEmpresa = empresaResponse.listaEmpresa[0].IdEmpresa;
                    this.direccion = empresaResponse.listaEmpresa[0].Direccion;
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }

    public class ZonaHoraria
    {
        public Guid idZonaHoraria { get; set; }
        public string zonaHoraria { get; set; }
        public string abreviatura { get; set; }
        public string coordenadas { get; set; }
        public decimal diferenciaUtcViejo { get; set; }
        public decimal diferenciaVeranoUtcViejo { get; set; }
        public decimal diferenciaUtc { get; set; }
        public decimal diferenciaVeranoUtc { get; set; }
    }
}