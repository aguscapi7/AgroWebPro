using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.AccesoDatos.Metodos
{
    //Las clases son sustantivos, empiezan con mayuscula y luego el inicio de cada palabra en mayuscula
    public class EstandarCodigo
    {
        //Se definen las variables de clase al inicio y los metodos después

        //Incorrecto, no se deben poner sufijos como i, dt, str...
        public int iContador;
        public DateTime dtFechaIngreso;
        public string strNombre;
        //Correcto
        public int contador;
        public DateTime fechaIngreso;
        public string nombre;

        //Incorrecto, las constantes no deben definirse solo con mayusculas
        public static readonly String VARIABLE_CONSTANTE = "ValorVariableConstante";
        //Correcto
        public static readonly String VariableConstante = "ValorVariableConstante";

        //Incorrecto, no se deben usar abreviaturas. Excepto id.
        public string usrGrp;
        public string empAssig;
        //Correcto
        public string userGroup;
        public string employeeAssignment;

        //Incorrecto, no usar underscore excepto en variables private y solo al principio
        public DateTime _fecha_registro;
        //Correcto
        public DateTime fechaRegistro;

        //Correcto, las variables privadas empiezan con underscore
        private string _nombreUsuario;

        //Incorrecto, se deben alinear los brackets verticalmente y usar camelcase para parametros y variables dentro del metodo
        //El metodo empieza con mayuscula y sin underscore
        public void metodo_prueba(string ParametroPrueba) {
            int VariableDentroDeMetodo;
        }

        //Correcto
        public void MetodoPrueba(string parametroPrueba)
        {
            int variableDentroDeMetodo;
        }


    }

    public interface IPruebaInterface
    {

    }
}
