using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMC.Wpf.CoverFlow.NMT.ChartComponent.Reportes;


namespace IMC.Wpf.CoverFlow.NMT.ChartComponent
{     
    
    class FactoryChart
    {
        private static FactoryChart instance;
        /// <summary>
        /// Patron de disenio solitario
        /// </summary>
        public static FactoryChart Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FactoryChart();
                }
                return instance;
            }
        }

        /// <summary>
        /// Obtiene el reporte solicitado
        /// </summary>
        /// <param name="IdReporte"></param>
        /// <returns></returns>
        public IChart getReporte(String nombreReporte) 
        {
            IChart retorno = null;
            //Pro reflection se obtiene la clase graficadora.
            Type t = Type.GetType("IMC.Wpf.CoverFlow.NMT.ChartComponent.Reportes."+nombreReporte, false, true);
            if (t!=null)
                retorno = (IChart)Activator.CreateInstance(t);           
            return retorno;
        }

    }
}
