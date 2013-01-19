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
    class MDZCoverage : IMC.Wpf.CoverFlow.NMT.ChartComponent.IChart
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
            DataTable filtro1 = consulta.SelectKPI_MDZ_Coverage_Flt_IndustryLevel().Tables[0];
            Filtro filterIndustryLevel = Herramientas.Instance.obtenerFiltro(filtro1, "Potencial", "IndustryLevel", "IndustryLevel", false);
            filtros.Add(filterIndustryLevel);
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
            title.Text = "MDZ Coverage";
            // Add title to Titles collection
            chart.Titles.Add(title);

            #region configuracion eje X
            // Creating AxisX
            Axis axisX = new Axis();
            // Date time standard format
            axisX.ValueFormatString = "000000";
            // To avoid auto skip
            
            chart.AxesX.Add(axisX);
            #endregion

            // Create a new instance of DataSeries
            // Create a new instance of DataSeries
            DataSeries dataSeries = new DataSeries();
            // Set DataSeries property
            dataSeries.RenderAs = RenderAs.Line;
            dataSeries.MarkerType = Visifire.Commons.MarkerTypes.Circle;
            dataSeries.SelectionEnabled = true;
            dataSeries.LineThickness = 3;

            // Create a new instance of DataSeries2
            DataSeries dataSeries2 = new DataSeries();
            // Set DataSeries property
            dataSeries2.RenderAs = RenderAs.Line;
            dataSeries2.MarkerType = Visifire.Commons.MarkerTypes.Circle;
            dataSeries2.SelectionEnabled = true;
            dataSeries2.LineThickness = 3;
            // Create a DataPoint
            DataPoint dataPoint;
            #region consulta
            DataTable kpis;
            try
            {
                kpis = new DataTable();
                Consultas consulta = new Consultas();
                kpis = consulta.SelectKPI_MDZ_Coverage(filtros).Tables[0];
                consulta = null;
                foreach (DataRow g in kpis.Rows)
                {
                    long? y = g["NbrCustUX"]!=DBNull.Value?(long?)g["NbrCustUX"]:null;
                    long? y2 = g["NbrCustITO"]!=DBNull.Value? (long?)g["NbrCustITO"]:null;
                    long x = (long)g["TimeId"];
                    // Create a new instance of DataPoint
                    dataPoint = new DataPoint();
                    // Set YValue for a DataPoint
                    if (x != null) dataPoint.AxisXLabel = x.ToString();
                    if (!ciclos.Contains(x.ToString()))  ciclos.Add(x.ToString());
                    dataPoint.YValue = System.Convert.ToDouble(y);
                    // Add dataPoint to DataPoints collection.
                    dataSeries.DataPoints.Add(dataPoint);
                    dataSeries.Name = "Universo";
                    // Create a new instance of DataPoint
                    dataPoint = new DataPoint();
                    // Set YValue for a DataPoint
                    if (x != null) dataPoint.AxisXLabel = x.ToString();
                    if (!ciclos.Contains(x.ToString()))  ciclos.Add(x.ToString());
                    dataPoint.YValue = System.Convert.ToDouble(y2);
                    // Add dataPoint to DataPoints collection.
                    dataSeries2.DataPoints.Add(dataPoint);
                    dataSeries2.Name = "MDZ";
                }
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
            #endregion
            // Add dataSeries to Series collection.
            chart.Series.Add(dataSeries);
            chart.Series.Add(dataSeries2);
            return chart;
        }
    }
}
