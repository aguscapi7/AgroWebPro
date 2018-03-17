﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AgroWebPro.Entidades
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class AgroWebProEntities : DbContext
    {
        public AgroWebProEntities()
            : base("name=AgroWebProEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual ObjectResult<PA_ConsultarZonasHorarias_Result> PA_ConsultarZonasHorarias(ObjectParameter estado, ObjectParameter mensaje)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PA_ConsultarZonasHorarias_Result>("PA_ConsultarZonasHorarias", estado, mensaje);
        }
    
        public virtual int PA_MantenimientoEmpresa(Nullable<int> tipoOperacion, Nullable<System.Guid> idEmpresa, string nombreEmpresa, string descripcionEmpresa, string telefono, string cedulaJuridica, Nullable<System.Guid> idZonaHoraria, string direccion, Nullable<bool> activa, ObjectParameter estado, ObjectParameter mensaje)
        {
            var tipoOperacionParameter = tipoOperacion.HasValue ?
                new ObjectParameter("TipoOperacion", tipoOperacion) :
                new ObjectParameter("TipoOperacion", typeof(int));
    
            var idEmpresaParameter = idEmpresa.HasValue ?
                new ObjectParameter("IdEmpresa", idEmpresa) :
                new ObjectParameter("IdEmpresa", typeof(System.Guid));
    
            var nombreEmpresaParameter = nombreEmpresa != null ?
                new ObjectParameter("NombreEmpresa", nombreEmpresa) :
                new ObjectParameter("NombreEmpresa", typeof(string));
    
            var descripcionEmpresaParameter = descripcionEmpresa != null ?
                new ObjectParameter("DescripcionEmpresa", descripcionEmpresa) :
                new ObjectParameter("DescripcionEmpresa", typeof(string));
    
            var telefonoParameter = telefono != null ?
                new ObjectParameter("Telefono", telefono) :
                new ObjectParameter("Telefono", typeof(string));
    
            var cedulaJuridicaParameter = cedulaJuridica != null ?
                new ObjectParameter("CedulaJuridica", cedulaJuridica) :
                new ObjectParameter("CedulaJuridica", typeof(string));
    
            var idZonaHorariaParameter = idZonaHoraria.HasValue ?
                new ObjectParameter("idZonaHoraria", idZonaHoraria) :
                new ObjectParameter("idZonaHoraria", typeof(System.Guid));
    
            var direccionParameter = direccion != null ?
                new ObjectParameter("Direccion", direccion) :
                new ObjectParameter("Direccion", typeof(string));
    
            var activaParameter = activa.HasValue ?
                new ObjectParameter("Activa", activa) :
                new ObjectParameter("Activa", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("PA_MantenimientoEmpresa", tipoOperacionParameter, idEmpresaParameter, nombreEmpresaParameter, descripcionEmpresaParameter, telefonoParameter, cedulaJuridicaParameter, idZonaHorariaParameter, direccionParameter, activaParameter, estado, mensaje);
        }
    
        public virtual int PA_MantenimientoUsuario(Nullable<int> tipoOperacion, Nullable<System.Guid> idUsuario, string nombre, string apellidos, string correo, string password, string direccion, string telefono, Nullable<System.Guid> ingresadoPor, Nullable<System.Guid> rol, Nullable<bool> activo, Nullable<System.Guid> idEmpresa, ObjectParameter estado, ObjectParameter mensaje)
        {
            var tipoOperacionParameter = tipoOperacion.HasValue ?
                new ObjectParameter("TipoOperacion", tipoOperacion) :
                new ObjectParameter("TipoOperacion", typeof(int));
    
            var idUsuarioParameter = idUsuario.HasValue ?
                new ObjectParameter("IdUsuario", idUsuario) :
                new ObjectParameter("IdUsuario", typeof(System.Guid));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var apellidosParameter = apellidos != null ?
                new ObjectParameter("Apellidos", apellidos) :
                new ObjectParameter("Apellidos", typeof(string));
    
            var correoParameter = correo != null ?
                new ObjectParameter("Correo", correo) :
                new ObjectParameter("Correo", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var direccionParameter = direccion != null ?
                new ObjectParameter("Direccion", direccion) :
                new ObjectParameter("Direccion", typeof(string));
    
            var telefonoParameter = telefono != null ?
                new ObjectParameter("Telefono", telefono) :
                new ObjectParameter("Telefono", typeof(string));
    
            var ingresadoPorParameter = ingresadoPor.HasValue ?
                new ObjectParameter("IngresadoPor", ingresadoPor) :
                new ObjectParameter("IngresadoPor", typeof(System.Guid));
    
            var rolParameter = rol.HasValue ?
                new ObjectParameter("Rol", rol) :
                new ObjectParameter("Rol", typeof(System.Guid));
    
            var activoParameter = activo.HasValue ?
                new ObjectParameter("Activo", activo) :
                new ObjectParameter("Activo", typeof(bool));
    
            var idEmpresaParameter = idEmpresa.HasValue ?
                new ObjectParameter("IdEmpresa", idEmpresa) :
                new ObjectParameter("IdEmpresa", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("PA_MantenimientoUsuario", tipoOperacionParameter, idUsuarioParameter, nombreParameter, apellidosParameter, correoParameter, passwordParameter, direccionParameter, telefonoParameter, ingresadoPorParameter, rolParameter, activoParameter, idEmpresaParameter, estado, mensaje);
        }
    
        public virtual ObjectResult<PA_ConsultarUsuarioLogin_Result> PA_ConsultarUsuarioLogin(string correo, string password, ObjectParameter estado, ObjectParameter mensaje)
        {
            var correoParameter = correo != null ?
                new ObjectParameter("Correo", correo) :
                new ObjectParameter("Correo", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PA_ConsultarUsuarioLogin_Result>("PA_ConsultarUsuarioLogin", correoParameter, passwordParameter, estado, mensaje);
        }
    
        public virtual int PA_MantenimientoCultivo(Nullable<int> tipoOperacion, Nullable<System.Guid> idCultivo, string nombre, string descripcion, Nullable<int> idFamilia, Nullable<System.Guid> ingresadoPor, Nullable<bool> activo, Nullable<System.Guid> idEmpresa, ObjectParameter estado, ObjectParameter mensaje)
        {
            var tipoOperacionParameter = tipoOperacion.HasValue ?
                new ObjectParameter("TipoOperacion", tipoOperacion) :
                new ObjectParameter("TipoOperacion", typeof(int));
    
            var idCultivoParameter = idCultivo.HasValue ?
                new ObjectParameter("IdCultivo", idCultivo) :
                new ObjectParameter("IdCultivo", typeof(System.Guid));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("Descripcion", descripcion) :
                new ObjectParameter("Descripcion", typeof(string));
    
            var idFamiliaParameter = idFamilia.HasValue ?
                new ObjectParameter("IdFamilia", idFamilia) :
                new ObjectParameter("IdFamilia", typeof(int));
    
            var ingresadoPorParameter = ingresadoPor.HasValue ?
                new ObjectParameter("IngresadoPor", ingresadoPor) :
                new ObjectParameter("IngresadoPor", typeof(System.Guid));
    
            var activoParameter = activo.HasValue ?
                new ObjectParameter("Activo", activo) :
                new ObjectParameter("Activo", typeof(bool));
    
            var idEmpresaParameter = idEmpresa.HasValue ?
                new ObjectParameter("IdEmpresa", idEmpresa) :
                new ObjectParameter("IdEmpresa", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("PA_MantenimientoCultivo", tipoOperacionParameter, idCultivoParameter, nombreParameter, descripcionParameter, idFamiliaParameter, ingresadoPorParameter, activoParameter, idEmpresaParameter, estado, mensaje);
        }
    
        public virtual ObjectResult<PA_ConsultarFamilias_Result> PA_ConsultarFamilias(ObjectParameter estado, ObjectParameter mensaje)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PA_ConsultarFamilias_Result>("PA_ConsultarFamilias", estado, mensaje);
        }
    
        public virtual ObjectResult<PA_ConsultarCultivosEmpresa_Result> PA_ConsultarCultivosEmpresa(Nullable<System.Guid> idEmpresa, ObjectParameter estado, ObjectParameter mensaje)
        {
            var idEmpresaParameter = idEmpresa.HasValue ?
                new ObjectParameter("IdEmpresa", idEmpresa) :
                new ObjectParameter("IdEmpresa", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PA_ConsultarCultivosEmpresa_Result>("PA_ConsultarCultivosEmpresa", idEmpresaParameter, estado, mensaje);
        }
    
        public virtual ObjectResult<PA_ConsultarEmpleadosEmpresa_Result> PA_ConsultarEmpleadosEmpresa(Nullable<System.Guid> idEmpresa, ObjectParameter estado, ObjectParameter mensaje)
        {
            var idEmpresaParameter = idEmpresa.HasValue ?
                new ObjectParameter("IdEmpresa", idEmpresa) :
                new ObjectParameter("IdEmpresa", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PA_ConsultarEmpleadosEmpresa_Result>("PA_ConsultarEmpleadosEmpresa", idEmpresaParameter, estado, mensaje);
        }
    
        public virtual ObjectResult<PA_ConsultarRoles_Result> PA_ConsultarRoles(ObjectParameter estado, ObjectParameter mensaje)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PA_ConsultarRoles_Result>("PA_ConsultarRoles", estado, mensaje);
        }
    
        public virtual ObjectResult<PA_ConsultarMonedas_Result> PA_ConsultarMonedas(ObjectParameter estado, ObjectParameter mensaje)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PA_ConsultarMonedas_Result>("PA_ConsultarMonedas", estado, mensaje);
        }
    
        public virtual ObjectResult<PA_ConsultarUnidadesVenta_Result> PA_ConsultarUnidadesVenta(ObjectParameter estado, ObjectParameter mensaje)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PA_ConsultarUnidadesVenta_Result>("PA_ConsultarUnidadesVenta", estado, mensaje);
        }
    
        public virtual int PA_MantenimientoTarea(Nullable<int> tipoOperacion, Nullable<System.Guid> idTarea, Nullable<System.Guid> idUsuario, Nullable<System.Guid> idTerreno, Nullable<System.Guid> asignadaPor, Nullable<System.DateTime> fechaInicio, Nullable<System.DateTime> fechaFinalizacion, Nullable<decimal> horasEstimadas, string resumen, string observaciones, Nullable<int> idTipoTarea, ObjectParameter estado, ObjectParameter mensaje)
        {
            var tipoOperacionParameter = tipoOperacion.HasValue ?
                new ObjectParameter("TipoOperacion", tipoOperacion) :
                new ObjectParameter("TipoOperacion", typeof(int));
    
            var idTareaParameter = idTarea.HasValue ?
                new ObjectParameter("IdTarea", idTarea) :
                new ObjectParameter("IdTarea", typeof(System.Guid));
    
            var idUsuarioParameter = idUsuario.HasValue ?
                new ObjectParameter("IdUsuario", idUsuario) :
                new ObjectParameter("IdUsuario", typeof(System.Guid));
    
            var idTerrenoParameter = idTerreno.HasValue ?
                new ObjectParameter("IdTerreno", idTerreno) :
                new ObjectParameter("IdTerreno", typeof(System.Guid));
    
            var asignadaPorParameter = asignadaPor.HasValue ?
                new ObjectParameter("AsignadaPor", asignadaPor) :
                new ObjectParameter("AsignadaPor", typeof(System.Guid));
    
            var fechaInicioParameter = fechaInicio.HasValue ?
                new ObjectParameter("FechaInicio", fechaInicio) :
                new ObjectParameter("FechaInicio", typeof(System.DateTime));
    
            var fechaFinalizacionParameter = fechaFinalizacion.HasValue ?
                new ObjectParameter("FechaFinalizacion", fechaFinalizacion) :
                new ObjectParameter("FechaFinalizacion", typeof(System.DateTime));
    
            var horasEstimadasParameter = horasEstimadas.HasValue ?
                new ObjectParameter("HorasEstimadas", horasEstimadas) :
                new ObjectParameter("HorasEstimadas", typeof(decimal));
    
            var resumenParameter = resumen != null ?
                new ObjectParameter("Resumen", resumen) :
                new ObjectParameter("Resumen", typeof(string));
    
            var observacionesParameter = observaciones != null ?
                new ObjectParameter("Observaciones", observaciones) :
                new ObjectParameter("Observaciones", typeof(string));
    
            var idTipoTareaParameter = idTipoTarea.HasValue ?
                new ObjectParameter("IdTipoTarea", idTipoTarea) :
                new ObjectParameter("IdTipoTarea", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("PA_MantenimientoTarea", tipoOperacionParameter, idTareaParameter, idUsuarioParameter, idTerrenoParameter, asignadaPorParameter, fechaInicioParameter, fechaFinalizacionParameter, horasEstimadasParameter, resumenParameter, observacionesParameter, idTipoTareaParameter, estado, mensaje);
        }
    
        public virtual ObjectResult<PA_ConsultarTerreno_Result> PA_ConsultarTerreno(Nullable<System.Guid> idTerreno, ObjectParameter estado, ObjectParameter mensaje)
        {
            var idTerrenoParameter = idTerreno.HasValue ?
                new ObjectParameter("IdTerreno", idTerreno) :
                new ObjectParameter("IdTerreno", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PA_ConsultarTerreno_Result>("PA_ConsultarTerreno", idTerrenoParameter, estado, mensaje);
        }
    
        public virtual int PA_MantenimientoTerreno(Nullable<int> tipoOperacion, Nullable<System.Guid> idTerreno, string nombre, string descripcion, string coordenadas, Nullable<System.Guid> idCultivo, Nullable<System.Guid> ingresadoPor, Nullable<bool> activo, Nullable<bool> actualizarCoordenadas, Nullable<System.Guid> idEmpresa, ObjectParameter estado, ObjectParameter mensaje)
        {
            var tipoOperacionParameter = tipoOperacion.HasValue ?
                new ObjectParameter("TipoOperacion", tipoOperacion) :
                new ObjectParameter("TipoOperacion", typeof(int));
    
            var idTerrenoParameter = idTerreno.HasValue ?
                new ObjectParameter("IdTerreno", idTerreno) :
                new ObjectParameter("IdTerreno", typeof(System.Guid));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("Descripcion", descripcion) :
                new ObjectParameter("Descripcion", typeof(string));
    
            var coordenadasParameter = coordenadas != null ?
                new ObjectParameter("Coordenadas", coordenadas) :
                new ObjectParameter("Coordenadas", typeof(string));
    
            var idCultivoParameter = idCultivo.HasValue ?
                new ObjectParameter("IdCultivo", idCultivo) :
                new ObjectParameter("IdCultivo", typeof(System.Guid));
    
            var ingresadoPorParameter = ingresadoPor.HasValue ?
                new ObjectParameter("IngresadoPor", ingresadoPor) :
                new ObjectParameter("IngresadoPor", typeof(System.Guid));
    
            var activoParameter = activo.HasValue ?
                new ObjectParameter("Activo", activo) :
                new ObjectParameter("Activo", typeof(bool));
    
            var actualizarCoordenadasParameter = actualizarCoordenadas.HasValue ?
                new ObjectParameter("ActualizarCoordenadas", actualizarCoordenadas) :
                new ObjectParameter("ActualizarCoordenadas", typeof(bool));
    
            var idEmpresaParameter = idEmpresa.HasValue ?
                new ObjectParameter("IdEmpresa", idEmpresa) :
                new ObjectParameter("IdEmpresa", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("PA_MantenimientoTerreno", tipoOperacionParameter, idTerrenoParameter, nombreParameter, descripcionParameter, coordenadasParameter, idCultivoParameter, ingresadoPorParameter, activoParameter, actualizarCoordenadasParameter, idEmpresaParameter, estado, mensaje);
        }
    
        public virtual ObjectResult<PA_ConsultarTerrenosEmpresa_Result> PA_ConsultarTerrenosEmpresa(Nullable<System.Guid> idEmpresa, ObjectParameter estado, ObjectParameter mensaje)
        {
            var idEmpresaParameter = idEmpresa.HasValue ?
                new ObjectParameter("IdEmpresa", idEmpresa) :
                new ObjectParameter("IdEmpresa", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PA_ConsultarTerrenosEmpresa_Result>("PA_ConsultarTerrenosEmpresa", idEmpresaParameter, estado, mensaje);
        }
    
        public virtual ObjectResult<PA_ConsultarEmpresa_Result> PA_ConsultarEmpresa(Nullable<System.Guid> idEmpresa, ObjectParameter estado, ObjectParameter mensaje)
        {
            var idEmpresaParameter = idEmpresa.HasValue ?
                new ObjectParameter("IdEmpresa", idEmpresa) :
                new ObjectParameter("IdEmpresa", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PA_ConsultarEmpresa_Result>("PA_ConsultarEmpresa", idEmpresaParameter, estado, mensaje);
        }
    
        public virtual ObjectResult<PA_ConsultarUsuario_Result> PA_ConsultarUsuario(Nullable<System.Guid> idUsuario, ObjectParameter estado, ObjectParameter mensaje)
        {
            var idUsuarioParameter = idUsuario.HasValue ?
                new ObjectParameter("IdUsuario", idUsuario) :
                new ObjectParameter("IdUsuario", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PA_ConsultarUsuario_Result>("PA_ConsultarUsuario", idUsuarioParameter, estado, mensaje);
        }
    
        public virtual ObjectResult<PA_ConsultarClientesProveedoresEmpresa_Result> PA_ConsultarClientesProveedoresEmpresa(Nullable<System.Guid> idEmpresa, Nullable<bool> esCliente, ObjectParameter estado, ObjectParameter mensaje)
        {
            var idEmpresaParameter = idEmpresa.HasValue ?
                new ObjectParameter("IdEmpresa", idEmpresa) :
                new ObjectParameter("IdEmpresa", typeof(System.Guid));
    
            var esClienteParameter = esCliente.HasValue ?
                new ObjectParameter("EsCliente", esCliente) :
                new ObjectParameter("EsCliente", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PA_ConsultarClientesProveedoresEmpresa_Result>("PA_ConsultarClientesProveedoresEmpresa", idEmpresaParameter, esClienteParameter, estado, mensaje);
        }
    
        public virtual ObjectResult<PA_ConsultarReporteCosechas_Result> PA_ConsultarReporteCosechas(Nullable<System.DateTime> fechaInicio, Nullable<System.DateTime> fechaFin, Nullable<System.Guid> idCultivo, ObjectParameter estado, ObjectParameter mensaje)
        {
            var fechaInicioParameter = fechaInicio.HasValue ?
                new ObjectParameter("FechaInicio", fechaInicio) :
                new ObjectParameter("FechaInicio", typeof(System.DateTime));
    
            var fechaFinParameter = fechaFin.HasValue ?
                new ObjectParameter("FechaFin", fechaFin) :
                new ObjectParameter("FechaFin", typeof(System.DateTime));
    
            var idCultivoParameter = idCultivo.HasValue ?
                new ObjectParameter("IdCultivo", idCultivo) :
                new ObjectParameter("IdCultivo", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PA_ConsultarReporteCosechas_Result>("PA_ConsultarReporteCosechas", fechaInicioParameter, fechaFinParameter, idCultivoParameter, estado, mensaje);
        }
    
        public virtual ObjectResult<PA_ConsultarReporteVentas_Result> PA_ConsultarReporteVentas(Nullable<System.DateTime> fechaInicio, Nullable<System.DateTime> fechaFin, Nullable<System.Guid> idEmpresa, ObjectParameter estado, ObjectParameter mensaje)
        {
            var fechaInicioParameter = fechaInicio.HasValue ?
                new ObjectParameter("FechaInicio", fechaInicio) :
                new ObjectParameter("FechaInicio", typeof(System.DateTime));
    
            var fechaFinParameter = fechaFin.HasValue ?
                new ObjectParameter("FechaFin", fechaFin) :
                new ObjectParameter("FechaFin", typeof(System.DateTime));
    
            var idEmpresaParameter = idEmpresa.HasValue ?
                new ObjectParameter("IdEmpresa", idEmpresa) :
                new ObjectParameter("IdEmpresa", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PA_ConsultarReporteVentas_Result>("PA_ConsultarReporteVentas", fechaInicioParameter, fechaFinParameter, idEmpresaParameter, estado, mensaje);
        }
    
        public virtual ObjectResult<PA_ConsultarEstadoTarea_Result> PA_ConsultarEstadoTarea(ObjectParameter estado, ObjectParameter mensaje)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PA_ConsultarEstadoTarea_Result>("PA_ConsultarEstadoTarea", estado, mensaje);
        }
    
        public virtual ObjectResult<PA_ConsultarTiposTareas_Result> PA_ConsultarTiposTareas(ObjectParameter estado, ObjectParameter mensaje)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PA_ConsultarTiposTareas_Result>("PA_ConsultarTiposTareas", estado, mensaje);
        }
    
        public virtual int PA_MantenimientoAvanceTareaUsuario(Nullable<int> tipoOperacion, Nullable<System.Guid> idTarea, Nullable<int> idEstadoTarea, string observaciones, Nullable<decimal> horas, ObjectParameter estado, ObjectParameter mensaje)
        {
            var tipoOperacionParameter = tipoOperacion.HasValue ?
                new ObjectParameter("TipoOperacion", tipoOperacion) :
                new ObjectParameter("TipoOperacion", typeof(int));
    
            var idTareaParameter = idTarea.HasValue ?
                new ObjectParameter("IdTarea", idTarea) :
                new ObjectParameter("IdTarea", typeof(System.Guid));
    
            var idEstadoTareaParameter = idEstadoTarea.HasValue ?
                new ObjectParameter("IdEstadoTarea", idEstadoTarea) :
                new ObjectParameter("IdEstadoTarea", typeof(int));
    
            var observacionesParameter = observaciones != null ?
                new ObjectParameter("Observaciones", observaciones) :
                new ObjectParameter("Observaciones", typeof(string));
    
            var horasParameter = horas.HasValue ?
                new ObjectParameter("Horas", horas) :
                new ObjectParameter("Horas", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("PA_MantenimientoAvanceTareaUsuario", tipoOperacionParameter, idTareaParameter, idEstadoTareaParameter, observacionesParameter, horasParameter, estado, mensaje);
        }
    }
}
