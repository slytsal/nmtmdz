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
using System.Globalization;

namespace IMC.Wpf.CoverFlow.NMT.ChartComponent.Reportes
{
    /// <summary>
    /// Reporte
    /// </summary>
    class EjecucionDeCalidad : IMC.Wpf.CoverFlow.NMT.ChartComponent.IChart
    {

        private List<Filtro> filtros = new List<Filtro>();
        private List<String> ciclos = new List<String>();

        /// <summary>
        /// 
        /// </summary>
        public List<Filtro> ObtenerFiltros()
        {
            filtros = new List<Filtro>();
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
            title.Text = "Ejecucion De Calidad";
            // Add title to Titles collection
            chart.Titles.Add(title);
            // Create a new instance of DataSeries
            DataSeries dataSeries = new DataSeries();
            // Set DataSeries property
            dataSeries.RenderAs = RenderAs.Column;
            //dataSeries.Name = "Calidad";

            #region configuracion eje X

            // Creating AxisX
            //Axis axisX = new Axis();
            //axisX.AxisOffset = 0.08;
            //chart.AxesX.Add(axisX);

            #endregion

            // Create a DataPoint
            DataPoint dataPoint;
            #region consulta
            DataTable kpis;
            try
            {
                kpis = new DataTable();
                Consultas consulta = new Consultas();
                kpis = consulta.SelectKPI_Ejecución_Calidad().Tables[0];
                consulta = null;
                string serAnt = "-5555";
                string ser = "-1111";
                int i = 0;

                foreach (DataRow g in kpis.Rows)
                {

                    Decimal? y1 = null;
                    Decimal? y2 = null;
                    Decimal? y3 = null;
                    Decimal? y4 = null;
                    Decimal? y5 = null;
                    Decimal? y6 = null;
                    Decimal? y7 = null;
                    Decimal? y8 = null;
                    Decimal? y9 = null;
                    Decimal? y10 = null;
                    Decimal? y11 = null;
                    Decimal? y12 = null;

                    if (!string.IsNullOrEmpty(Convert.ToString(g["Ciclo_1"])))
                    {
                        y1 = (Decimal)g["Ciclo_1"];
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(g["Ciclo_2"])))
                    {
                        y2 = (Decimal)g["Ciclo_2"];
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(g["Ciclo_3"])))
                    {
                        y3 = (Decimal)g["Ciclo_3"];
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(g["Ciclo_4"])))
                    {
                        y4 = (Decimal)g["Ciclo_4"];
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(g["Ciclo_5"])))
                    {
                        y5 = (Decimal)g["Ciclo_5"];
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(g["Ciclo_6"])))
                    {
                        y6 = (Decimal)g["Ciclo_6"];
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(g["Ciclo_7"])))
                    {
                        y7 = (Decimal)g["Ciclo_7"];
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(g["Ciclo_8"])))
                    {
                        y8 = (Decimal)g["Ciclo_8"];
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(g["Ciclo_9"])))
                    {
                        y9 = (Decimal)g["Ciclo_9"];
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(g["Ciclo_10"])))
                    {
                        y10 = (Decimal)g["Ciclo_10"];
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(g["Ciclo_11"])))
                    {
                        y11 = (Decimal)g["Ciclo_11"];
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(g["Ciclo_12"])))
                    {
                        y12 = (Decimal)g["Ciclo_12"];
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(g["LastCycle"])))
                    {
                        string lastCycle = Convert.ToString((long)g["LastCycle"]);

                        CultureInfo culture = new CultureInfo("es-CO");
                        string sFecha = Convert.ToString("01/"+lastCycle.Substring(4, 2) + "/" + lastCycle.Substring(0, 4));
                        DateTime cycle;
                        cycle = Convert.ToDateTime(sFecha, culture);
                        
                        
                        //long y = (long)g["TimeId"];
                        ser = (string)g["Canal"];
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
                            dataSeries.RenderAs = RenderAs.Column;
                            dataSeries.MarkerType = Visifire.Commons.MarkerTypes.Circle;
                            dataSeries.SelectionEnabled = true;
                            dataSeries.LineThickness = 1;
                            //System.Windows.Thickness s = new System.Windows.Thickness(10);
                            //dataSeries.Padding = s;
                            serAnt = ser;
                        }

                        // Create a new instance of DataPoint
                        dataPoint = new DataPoint();
                        // Set YValue for a DataPoint
                        if (i == 0)
                        {
                            dataPoint.AxisXLabel = cycle.AddMonths(-11).ToString("yyyyMM");
                        }
                        if (y1 != null)
                        {
                            dataPoint.YValue = System.Convert.ToDouble(y1);
                        }
                        // Add dataPoint to DataPoints collection.
                        dataSeries.DataPoints.Add(dataPoint);

                        dataPoint = new DataPoint();
                        // Set YValue for a DataPoint
                        if (i == 0)
                        {
                            dataPoint.AxisXLabel = cycle.AddMonths(-10).ToString("yyyyMM");
                        }
                        if (y2 != null)
                        {
                            dataPoint.YValue = System.Convert.ToDouble(y2);
                        }
                        // Add dataPoint to DataPoints collection.
                        dataSeries.DataPoints.Add(dataPoint);

                        dataPoint = new DataPoint();
                        // Set YValue for a DataPoint
                        if (i == 0)
                        {
                            dataPoint.AxisXLabel = cycle.AddMonths(-9).ToString("yyyyMM");
                        }
                        if (y3 != null)
                        {
                            dataPoint.YValue = System.Convert.ToDouble(y3);
                        }
                        // Add dataPoint to DataPoints collection.
                        dataSeries.DataPoints.Add(dataPoint);

                        dataPoint = new DataPoint();
                        // Set YValue for a DataPoint
                        if (i == 0)
                        {
                            dataPoint.AxisXLabel = cycle.AddMonths(-8).ToString("yyyyMM");
                        }
                        if (y4 != null)
                        {
                            dataPoint.YValue = System.Convert.ToDouble(y4);
                        }
                        // Add dataPoint to DataPoints collection.
                        dataSeries.DataPoints.Add(dataPoint);

                        dataPoint = new DataPoint();
                        // Set YValue for a DataPoint
                        if (i == 0)
                        {
                            dataPoint.AxisXLabel = cycle.AddMonths(-7).ToString("yyyyMM");
                        }
                        if (y5 != null)
                        {
                            dataPoint.YValue = System.Convert.ToDouble(y5);
                        }
                        // Add dataPoint to DataPoints collection.
                        dataSeries.DataPoints.Add(dataPoint);

                        dataPoint = new DataPoint();
                        // Set YValue for a DataPoint
                        if (i == 0)
                        {
                            dataPoint.AxisXLabel = cycle.AddMonths(-6).ToString("yyyyMM");
                        }
                        if (y6 != null)
                        {
                            dataPoint.YValue = System.Convert.ToDouble(y6);
                        }
                        // Add dataPoint to DataPoints collection.
                        dataSeries.DataPoints.Add(dataPoint);

                        dataPoint = new DataPoint();
                        // Set YValue for a DataPoint
                        if (i == 0)
                        {
                            dataPoint.AxisXLabel = cycle.AddMonths(-5).ToString("yyyyMM");
                        }
                        if (y7 != null)
                        {
                            dataPoint.YValue = System.Convert.ToDouble(y7);
                        }
                        // Add dataPoint to DataPoints collection.
                        dataSeries.DataPoints.Add(dataPoint);

                        dataPoint = new DataPoint();
                        // Set YValue for a DataPoint
                        if (i == 0)
                        {
                            dataPoint.AxisXLabel = cycle.AddMonths(-4).ToString("yyyyMM");
                        }
                        if (y8 != null)
                        {
                            dataPoint.YValue = System.Convert.ToDouble(y8);
                        }
                        // Add dataPoint to DataPoints collection.
                        dataSeries.DataPoints.Add(dataPoint);

                        dataPoint = new DataPoint();
                        // Set YValue for a DataPoint
                        if (i == 0)
                        {
                            dataPoint.AxisXLabel = cycle.AddMonths(-3).ToString("yyyyMM");
                        }
                        if (y9 != null)
                        {
                            dataPoint.YValue = System.Convert.ToDouble(y9);
                        }
                        // Add dataPoint to DataPoints collection.
                        dataSeries.DataPoints.Add(dataPoint);

                        dataPoint = new DataPoint();
                        // Set YValue for a DataPoint
                        if (i == 0)
                        {
                            dataPoint.AxisXLabel = cycle.AddMonths(-2).ToString("yyyyMM");
                        }
                        if (y10 != null)
                        {
                            dataPoint.YValue = System.Convert.ToDouble(y10);
                        }

                        // Add dataPoint to DataPoints collection.
                        dataSeries.DataPoints.Add(dataPoint);

                        dataPoint = new DataPoint();
                        // Set YValue for a DataPoint
                        if (i == 0)
                        {
                            dataPoint.AxisXLabel = cycle.AddMonths(-1).ToString("yyyyMM");
                        }
                        if (y11 != null)
                        {
                            dataPoint.YValue = System.Convert.ToDouble(y11);
                        }
                        // Add dataPoint to DataPoints collection.
                        dataSeries.DataPoints.Add(dataPoint);

                        dataPoint = new DataPoint();
                        // Set YValue for a DataPoint
                        if (i == 0)
                        {
                            dataPoint.AxisXLabel = cycle.ToString("yyyyMM"); //"Ciclo 12"
                        }
                        if (y12 != null)
                        {
                            dataPoint.YValue = System.Convert.ToDouble(y12);
                        }
                        // Add dataPoint to DataPoints collection.
                        dataSeries.DataPoints.Add(dataPoint);
                        i++;
                    }
                }
                //axisX.Zoom(201102, 201103);
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
