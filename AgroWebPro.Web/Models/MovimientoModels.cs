using AgroWebPro.Entidades.Consultas.Salida;
using AgroWebPro.Utilitarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Web;

namespace AgroWebPro.Web.Models
{  
    public class MovimientoModels
    {
        public decimal totalIngresos { get; set; }
        public decimal totalGastos { get; set; }
        public string fechaActual { get; set; }
        public bool errorValidacion { get; set; }
        public Guid idMovimiento { get; set; }
        [Display(Name = "Fecha")]
        public string fecha { get; set; }
        [Display(Name = "Monto")]
        [Required(ErrorMessage = "Debe ingresar el monto")]
        public decimal monto { get; set; }
        [Display(Name = "Tipo de ")]
        [Required(ErrorMessage = "Debe seleccionar el tipo")]
        public Guid idCategoriaMovimiento { get; set; }
        public bool ingreso { get; set; }
        public string nombreCategoria { get; set; }
        [Display(Name = "Observaciones")]
        public string observaciones { get; set; }
        public string nombreMes { get; set; }
        public int annioSeleccionado { get; set; }
        public int mesSeleccionado { get; set; }
        public DateTime primerDia { get; set; }
        public DateTime ultimoDia { get; set; }
        public DateTime fechaIngreso { get; set; }
        public List<CategoriaMovimiento> listaCategoriasMovimiento { get; set; }
        public List<MovimientoModels> listaIngresos { get; set; }
        public List<MovimientoModels> listaGastos { get; set; }

        public void CopiarCategoriasMovimientos(ConsultarCategoriasMovimientoResponse categoriasMovimientosResponse)
        {
            try
            {
                this.listaCategoriasMovimiento = new List<CategoriaMovimiento>();
                if (categoriasMovimientosResponse != null && categoriasMovimientosResponse.estado.Equals(Constantes.EstadoCorrecto) && categoriasMovimientosResponse.listaCategorias.Count > 0)
                {
                    CategoriaMovimiento categoriaMovimiento = null;
                    foreach (var item in categoriasMovimientosResponse.listaCategorias)
                    {
                        categoriaMovimiento = new CategoriaMovimiento()
                        {
                            idCategoriaMovimiento = item.IdCategoriaMovimiento,
                            ingreso = item.Ingreso,
                            nombreCategoria = item.NombreCategoria
                        };
                        this.listaCategoriasMovimiento.Add(categoriaMovimiento);
                    }
                }

            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public void CopiarMovimientos(ConsultarMovimientosResponse movimientosResponse)
        {
            try
            {
                this.listaGastos = new List<MovimientoModels>();
                this.listaIngresos = new List<MovimientoModels>();
                if(movimientosResponse != null && movimientosResponse.estado.Equals(Constantes.EstadoCorrecto) && movimientosResponse.listaMovimientos.Count > 0)
                {
                    MovimientoModels nuevoMovimiento = null;
                    foreach (var item in movimientosResponse.listaMovimientos)
                    {
                        nuevoMovimiento = new MovimientoModels()
                        {
                            fecha = item.Fecha.ToString("dd/MM/yyyy"),
                            observaciones = item.Observaciones,
                            ingreso = item.Ingreso,
                            monto = item.Monto,
                            idMovimiento = item.IdMovimiento,
                            nombreCategoria = item.NombreCategoria,  
                            fechaIngreso = item.FechaIngreso,
                            idCategoriaMovimiento = item.IdCategoriaMovimiento
                            
                        };
                        if (item.Ingreso)
                        {
                            this.listaIngresos.Add(nuevoMovimiento);
                        }
                        else
                        {
                            this.listaGastos.Add(nuevoMovimiento);
                        }

                    }
                    this.listaGastos = this.listaGastos.OrderByDescending(x => x.fechaIngreso).OrderByDescending(x => x.fecha).ToList();
                    this.listaIngresos = this.listaIngresos.OrderByDescending(x => x.fechaIngreso).OrderByDescending(x => x.fecha).ToList();
                    this.totalGastos = this.listaGastos.Sum(x => x.monto);
                    this.totalIngresos = this.listaIngresos.Sum(x => x.monto);
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }

    public class CategoriaMovimiento
    {
        public string nombreCategoria { get; set; }
        public Guid idCategoriaMovimiento { get; set; }
        public bool ingreso { get; set; }
    }
}