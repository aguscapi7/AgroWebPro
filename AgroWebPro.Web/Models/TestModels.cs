using AgroWebPro.Entidades.Consultas.Salida;
using AgroWebPro.Utilitarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgroWebPro.Web.Models
{
    public class TestModels
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar el nombre")]
        public string nombre { get; set; }
        public List<TestModels> listaTest { get; set; }
        public int id { get; set; }

        public void CopiarTest(ConsultarTestResponse testResponse)
        {
            try
            {
                listaTest = new List<TestModels>();
                if(testResponse != null && testResponse.estado.Equals(Constantes.EstadoCorrecto) && testResponse.listaNombres.Count > 0)
                {
                    TestModels nuevoTest = null;
                    foreach (var test in testResponse.listaNombres)
                    {
                        nuevoTest = new TestModels();
                        nuevoTest.id = test.id;
                        nuevoTest.nombre = test.nombre;
                        listaTest.Add(nuevoTest);
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

    }
    
    

}