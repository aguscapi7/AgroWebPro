﻿using AgroWebPro.Entidades.Consultas.Salida;
using AgroWebPro.Utilitarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgroWebPro.Web.Models
{
    public class TareaModels
    {
        [Display(Name = "Empleado")]
        public Guid idUsuarioTarea { get; set; }
        public List<TipoTarea> listaTiposTareas { get; set; }
        public List<UsuarioModels> listaEmpleadosEmpresa { get; set; }
        public List<TerrenoModels> listaTerrenosEmpresa { get; set; }
        [Display(Name = "Terreno")]
        public Guid idTerreno { get; set; }
        [Display(Name = "Tipo de tarea")]
        public int idTipoTarea { get; set; }
        [Display(Name = "Resumen")]
        public string resumen { get; set; }
        [Display(Name = "Observaciones")]
        public string observaciones { get; set; }
        [Display(Name = "Fecha de inicio")]
        public DateTime fechaInicio { get; set; }
        [Display(Name = "Fecha de finalización")]
        public DateTime fechaFinalizacion { get; set; }
        [Display(Name = "Horas estimadas")]
        public decimal horasEstimadas { get; set; }
        public List<TareaModels> listaTareas { get; set; }
        public Guid idTarea { get; set; }
        public string nombreTerreno { get; set; }
        public string nombreTipoTarea { get; set; }
        public string nombreEmpleado { get; set; }
        public string apellidoEmpleado { get; set; }
        public decimal kilogramosPrimera { get; set; }
        public decimal kilogramosSegunda { get; set; }
        public decimal kilogramosRechazo { get; set; }
        public string causaRechazo { get; set; }
        public Guid idUsuarioSupervisor { get; set; }
        public string codigoVerificadorSupervisor { get; set; }
        
        public void CopiarTiposTareas(ConsultarTiposTareasResponse tiposTareasResponse)
        {
            try
            {
                listaTiposTareas = new List<TipoTarea>();
                if(tiposTareasResponse != null && tiposTareasResponse.estado.Equals(Constantes.EstadoCorrecto) && tiposTareasResponse.listaTiposTareas.Count > 0)
                {
                    TipoTarea tipoTarea = null;
                    foreach (var tipoTareaItem in tiposTareasResponse.listaTiposTareas)
                    {
                        tipoTarea = new TipoTarea();
                        tipoTarea.idTipoTarea = tipoTareaItem.IdTipoTarea;
                        tipoTarea.nombreTarea = tipoTareaItem.NombreTarea;
                        tipoTarea.recoleccion = tipoTareaItem.Recoleccion;
                        listaTiposTareas.Add(tipoTarea);
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        public void CopiarEmpleadosEmpresa(ConsultarEmpleadosEmpresaResponse empleadosEmpresaResponse)
        {
            try
            {
                listaEmpleadosEmpresa = new List<UsuarioModels>();
                if (empleadosEmpresaResponse != null && empleadosEmpresaResponse.estado.Equals(Constantes.EstadoCorrecto) && empleadosEmpresaResponse.listaEmpleadosEmpresa.Count >= 0)
                {
                    UsuarioModels usuario = null;
                    foreach (var usuarioItem in empleadosEmpresaResponse.listaEmpleadosEmpresa)
                    {
                        usuario = new UsuarioModels();
                        usuario.idUsuario = usuarioItem.IdUsuario;
                        usuario.nombre = usuarioItem.Nombre;
                        usuario.apellidos = usuarioItem.Apellidos;
                        usuario.correo = usuarioItem.Correo;
                        usuario.idRol = usuarioItem.IdRol;
                        usuario.nombreRol = usuarioItem.NombreRol;
                        usuario.telefono = usuarioItem.Telefono;
                        usuario.direccion = usuarioItem.Direccion;
                        listaEmpleadosEmpresa.Add(usuario);
                    }
                }
            }
            catch (Exception ex)
            {

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
                        listaTerrenosEmpresa.Add(terreno);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void CopiarTareasEmpresa(ConsultarTareasEmpresaResponse tareasEmpresaResponse)
        {
            try
            {
                listaTareas = new List<TareaModels>();
                if (tareasEmpresaResponse != null && tareasEmpresaResponse.estado.Equals(Constantes.EstadoCorrecto) && tareasEmpresaResponse.listaTareasEmpresa.Count >= 0)
                {
                    TareaModels tarea = null;
                    foreach (var tareaItem in tareasEmpresaResponse.listaTareasEmpresa)
                    {
                        tarea = new TareaModels();
                        tarea.idTerreno = tareaItem.IdTerreno;
                        tarea.nombreTerreno = tareaItem.NombreTerreno;
                        tarea.idTarea = tareaItem.IdTarea;
                        tarea.resumen = tareaItem.Resumen;
                        tarea.observaciones = tareaItem.Observaciones;
                        tarea.nombreTipoTarea = tareaItem.NombreTarea;
                        tarea.fechaInicio = tareaItem.FechaInicio;
                        tarea.fechaFinalizacion = tareaItem.FechaFinalizacion;
                        tarea.horasEstimadas = tareaItem.HorasEstimadas;
                        tarea.idUsuarioTarea = tareaItem.IdUsuario;
                        tarea.nombreEmpleado = tareaItem.NombreEmpleado;
                        tarea.apellidoEmpleado = tareaItem.ApellidosEmpleado;
                        listaTareas.Add(tarea);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

    }

    public class TipoTarea
    {
        public int idTipoTarea { get; set; }
        public string nombreTarea { get; set; }
        public bool recoleccion { get; set; }
    }
    

}