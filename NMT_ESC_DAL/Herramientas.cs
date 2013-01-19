using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

namespace IMC
{
    class Herramientas
    {

        private static Herramientas instance;

        /// <summary>
        /// Patron de disenio solitario
        /// </summary>
        public static Herramientas Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Herramientas();
                }
                return instance;
            }
        }

        

        /// <summary>
        /// Devuelve la cadena separada por comas y con comillas sencillas
        /// </summary>
        /// <param name="init"></param>
        /// <returns></returns>
        public string obtenerMultiseleccionText(string init)
        {
            string retorno = "";
            if (init.Length > 0)
            {
                retorno = "'" + init.Replace(",", "','") + "'";
            }
            return retorno;
        }
    }
}
