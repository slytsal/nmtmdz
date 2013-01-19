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
using System.Windows.Controls;
using System.Windows;

namespace IMC.Wpf.CoverFlow.NMT.ChartComponent.Reportes
{
    class Facturado : IMC.Wpf.CoverFlow.NMT.ChartComponent.IChart
    {
       
        private List<Filtro> filtros = new List<Filtro>();
        private List<String> ciclos = new List<String>();
        public List<Filtro> ObtenerFiltros()
        {
            filtros = new List<Filtro>();
            Consultas consulta = new Consultas();
            DataTable filtro1 = consulta.SelectKPI_Facturado_Flt_Cliente().Tables[0];
            if(filtro1.Rows.Count>0)
                filtro1.Rows.Add("TODOS");
            Filtro filterIndustryLevel = Herramientas.Instance.obtenerFiltro(filtro1, "Cliente", "Cliente", "Cliente", false);
            filtros.Add(filterIndustryLevel);
            return filtros;
        }
        public Chart Draw(string[] filtros)
        {
            Chart chart = new Chart();
            if (filtros.Length > 1)
            {
                if (filtros[1] == "Detallado")
                {
                    // Create a new instance of Chart

                    chart.AnimationEnabled = true;
                    // Create a new instance of Title
                    Title title = new Title();
                    // Set title property
                    title.Text = "FACTURADO";
                    // Add title to Titles collection
                    chart.Titles.Add(title);
                    // Create a new instance of DataSeries
                    DataSeries dataSeries = new DataSeries();
                    // Set DataSeries property
                    dataSeries.RenderAs = RenderAs.Column;
                    dataSeries.SelectionEnabled = true;
                    dataSeries.LineThickness = 3;

                    // Create a new instance of DataSeries2
                    DataSeries dataSeries2 = new DataSeries();
                    // Set DataSeries property
                    dataSeries2.RenderAs = RenderAs.Column;
                    dataSeries2.SelectionEnabled = true;
                    dataSeries2.LineThickness = 3;

                    // Create a new instance of DataSeries3
                    DataSeries dataSeries3 = new DataSeries();
                    // Set DataSeries property
                    dataSeries3.RenderAs = RenderAs.Column;
                    dataSeries3.SelectionEnabled = true;
                    dataSeries3.LineThickness = 3;


                    // Set DataSeries property
                    dataSeries.RenderAs = RenderAs.Column;
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
                        kpis = consulta.SelectKPI_Facturado(filtros).Tables[0];
                        consulta = null;
                        Boolean band = false;
                        foreach (DataRow g in kpis.Rows)
                        {
                            Decimal? y = g["VolumenActual"] != DBNull.Value ? (Decimal?)g["VolumenActual"] : null;
                            Decimal? y2 = g["VolumenAnterior"] != DBNull.Value ? (Decimal?)g["VolumenAnterior"] : null;
                            Decimal? y3 = g["Variacion"] != DBNull.Value ? (Decimal?)g["Variacion"] : null;
                            int x = (int)g["Ciclo"];

                            // Create a new instance of DataPoint
                            dataPoint = new DataPoint();
                            // Set YValue for a DataPoint
                            if (x != null) dataPoint.AxisXLabel = x.ToString();
                            if (!ciclos.Contains(x.ToString())) ciclos.Add(x.ToString());
                            dataPoint.YValue = System.Convert.ToDouble(y2);
                            dataPoint.ToolTipText = (int.Parse(x.ToString().Substring(0, 4)) - 1).ToString()+" "+y2;
                            // Add dataPoint to DataPoints collection.
                            dataSeries.DataPoints.Add(dataPoint);
                            dataSeries.Name = "ANTERIOR";

                            // Set YValue for a DataPoint
                            dataPoint = new DataPoint();
                            // Set YValue for a DataPoint
                            if (x != null) dataPoint.AxisXLabel = x.ToString();
                            if (!ciclos.Contains(x.ToString())) ciclos.Add(x.ToString());
                            dataPoint.YValue = System.Convert.ToDouble(y);
                            dataPoint.ToolTipText = (int.Parse(x.ToString().Substring(0, 4))).ToString() + " " + y;
                            // Add dataPoint to DataPoints collection.
                            dataSeries2.DataPoints.Add(dataPoint);
                            dataSeries2.Name = "ACTUAL";


                            // Set YValue for a DataPoint
                            dataPoint = new DataPoint();
                            // Set YValue for a DataPoint
                            if (x != null) dataPoint.AxisXLabel = x.ToString();
                            dataPoint.ToolTipText = x.ToString().Substring(0, 4);
                            if (!ciclos.Contains(x.ToString())) ciclos.Add(x.ToString());
                            dataPoint.YValue = System.Convert.ToDouble(y3);
                            dataPoint.ToolTipText = (int.Parse(x.ToString().Substring(0, 4))).ToString() + " " + y3;
                            // Add dataPoint to DataPoints collection.
                            dataSeries3.DataPoints.Add(dataPoint);
                            dataSeries3.Name = "VARIACION";

                            band = true;

                        }

                        // Add dataSeries to Series collection.
                        if (band != false)
                        {
                            chart.Series.Add(dataSeries);
                            chart.Series.Add(dataSeries2);
                            chart.Series.Add(dataSeries3);
                        }
                        return chart;

                    }
                    catch (Exception Error)
                    {
                        throw (new Exception(Error.ToString()));
                    }
                    #endregion
                }
                else
                {
                    // Create a new instance of Chart
                  
                    chart.AnimationEnabled = true;
                    // Create a new instance of Title
                    Title title = new Title();
                    // Set title property
                    title.Text = "FACTURADO";
                    // Add title to Titles collection
                    chart.Titles.Add(title);
                    // Create a new instance of DataSeries
                    DataSeries dataSeries = new DataSeries();
                    // Set DataSeries property
                    dataSeries.RenderAs = RenderAs.Column;
                    dataSeries.SelectionEnabled = true;
                    dataSeries.LineThickness = 3;

                    DataSeries dataSeries2 = new DataSeries();
                    // Set DataSeries property
                    dataSeries2.RenderAs = RenderAs.Column;
                    dataSeries2.SelectionEnabled = true;
                    dataSeries2.LineThickness = 3;

                    // Set DataSeries property
                    dataSeries.RenderAs = RenderAs.Column;
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
                        kpis = consulta.SelectKPI_Facturado(filtros).Tables[0];
                        consulta = null;
                        Boolean band = false;

                        foreach (DataRow g in kpis.Rows)
                        {
                            Decimal? y = g["VolumenActual"] != DBNull.Value ? (Decimal?)g["VolumenActual"] : null;
                            Decimal? y2 = g["VolumenAnterior"] != DBNull.Value ? (Decimal?)g["VolumenAnterior"] : null;
                            int x = 0; //(int)g["Ciclo"];
                            string año = (string)g["ano"];
                            // Create a new instance of DataPoint
                            dataPoint = new DataPoint();

                            // Set YValue for a DataPoint
                            dataPoint.AxisXLabel = x.ToString();
                            if (!ciclos.Contains(x.ToString())) ciclos.Add(x.ToString());
                            dataPoint.YValue = System.Convert.ToDouble(y2);
                            dataPoint.ToolTipText = (int.Parse(año.Substring(0, 4))-1).ToString()+" "+y2;
                            // Add dataPoint to DataPoints collection.
                            dataSeries.DataPoints.Add(dataPoint);
                            dataSeries.Name = "ANTERIOR";

                            // Set YValue for a DataPoint
                            dataPoint = new DataPoint();
                            // Set YValue for a DataPoint
                            dataPoint.AxisXLabel = x.ToString();
                            if (!ciclos.Contains(x.ToString())) ciclos.Add(x.ToString());
                            dataPoint.YValue = System.Convert.ToDouble(y);
                            dataPoint.ToolTipText = año+" "+y;
                            // Add dataPoint to DataPoints collection.
                            dataSeries2.DataPoints.Add(dataPoint);
                            dataSeries2.Name = "ACTUAL";
                            band = true;

                        }

                        // Add dataSeries to Series collection.
                        if (band != false)
                        {
                            chart.Series.Add(dataSeries);
                            chart.Series.Add(dataSeries2);

                        }
                        return chart;

                    }
                    catch (Exception Error)
                    {
                        throw (new Exception(Error.ToString()));
                    }
                    #endregion
                }
            }
            else
                return chart;
        }
        public List<string> ObtenerCiclos()
        {
            return ciclos;
        }

    }
}
