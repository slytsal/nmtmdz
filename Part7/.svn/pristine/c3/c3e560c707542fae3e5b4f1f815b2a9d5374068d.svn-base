using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NMT_ESC_BLL.Domain;
using Visifire.Charts;
using System.Data;
using NMT_ESC_DAL;
using IMC.Wpf.CoverFlow.NMT.ChartComponent.Domain;
using IMC.Wpf.CoverFlow.NMT.ChartComponent.Utils;

namespace IMC.Wpf.CoverFlow.NMT.ChartComponent.Reportes
{
    /// <summary>
    /// Reporte
    /// </summary>
    class Manejos : IMC.Wpf.CoverFlow.NMT.ChartComponent.IChart
    {
        private List<Filtro> filtros = new List<Filtro>();
        private List<String> ciclos = new List<String>();

        /// <summary>
        /// 
        /// </summary>
        public List<Filtro> ObtenerFiltros()
        {
            filtros = new List<Filtro>();
            Consultas consulta = new Consultas();
            DataTable filtro1 = consulta.SelectKPI_Manejos_Flt_IndustryLevel().Tables[0];
            Filtro filterIndustryLevel = Herramientas.Instance.obtenerFiltro(filtro1, "Potencial", "IndustryLevel", "IndustryLevel", false);
            DataTable filtro2 = consulta.SelectKPI_Manejos_Flt_BrandDescription().Tables[0];
            Filtro filterBrandDescription = Herramientas.Instance.obtenerFiltro(filtro2, "Subfamilia", "BrandDescription", "BrandDescription", true);//Subfamilia
            DataTable filtro3 = consulta.SelectKPI_Manejos_Flt_BrandCode().Tables[0];
            Filtro filterBrandCode = Herramientas.Instance.obtenerFiltro(filtro3, "Brand Code", "BrandCode", "BrandCode", true);
            filtros.Add(filterIndustryLevel);
            filtros.Add(filterBrandCode);
            filtros.Add(filterBrandDescription);
            return filtros;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<String> ObtenerCiclos()
        {
            return ciclos;
        }

        /// <summary>
        /// Metodo de graficación.
        /// </summary>
        /// <returns></returns>
        public Chart Draw(params string[] filtros) 
        {
            // Create a new instance of Chart
            Chart chart = new Chart();
            
            
            chart.AnimationEnabled = true;
            // Create a new instance of Title
            Title title = new Title();
            // Set title property
            title.Text = "Manejos";
            // Add title to Titles collection
            chart.Titles.Add(title);
            // Create a new instance of DataSeries
            DataSeries dataSeries = new DataSeries();
            // Set DataSeries property
            dataSeries.RenderAs = RenderAs.Line;

            #region configuracion eje X
            // Creating AxisX
            Axis axisX = new Axis();
            // Date time standard format
            axisX.ValueFormatString = "000000";
            // To avoid auto skip
            
            chart.AxesX.Add(axisX);
            #endregion
            // Create a DataPoint
            DataPoint dataPoint;
            #region consulta
            DataTable kpis;
            try
            {
                kpis = new DataTable();
                Consultas consulta = new Consultas();
                kpis = consulta.SelectKPI_Manejos(filtros).Tables[0];
                consulta = null;
                string serAnt = "-5555";
                string ser = "-1111";
                foreach (DataRow g in kpis.Rows)
                {
                    Decimal? y = g["NbrHandling"]!=DBNull.Value?(Decimal?)g["NbrHandling"]:null;
                    long x = (long)g["TimeId"];
                    ser = (string)g["BrandDescription"];
                    //Creando las series
                    if (serAnt != ser)
                    {
                        if (serAnt != "-5555")
                        {
                            chart.Series.Add(dataSeries);
                        }
                        // Create a new instance of DataSeries
                        dataSeries = new DataSeries();
                        dataSeries.Name = ser;
                        // Set DataSeries property
                        dataSeries.RenderAs = RenderAs.Line;
                        dataSeries.MarkerType = Visifire.Commons.MarkerTypes.Circle;
                        dataSeries.SelectionEnabled = true;
                        dataSeries.LineThickness = 3;

                    }

                    // Create a new instance of DataPoint
                    dataPoint = new DataPoint();
                    // Set YValue for a DataPoint
                    if (x != null) dataPoint.AxisXLabel = x.ToString();
                    if (!ciclos.Contains(x.ToString()))  ciclos.Add(x.ToString());
                    dataPoint.YValue = System.Convert.ToDouble(y);
                    // Add dataPoint to DataPoints collection.
                    dataSeries.DataPoints.Add(dataPoint);
                    serAnt = ser;
                }
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
            #endregion             
            // Add dataSeries to Series collection.
            chart.Series.Add(dataSeries);
            return chart;
        }
    }
}
