﻿using System;
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
    /// <summary>
    /// Reporte
    /// </summary>
    class Volumen : IMC.Wpf.CoverFlow.NMT.ChartComponent.IChart
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
            DataTable filtro1 = consulta.SelectKPI_Volumen_Flt_Chain().Tables[0];
            Filtro filterChain = Herramientas.Instance.obtenerFiltro(filtro1, "Cadena", "Chain", "Chain", false);
            filtros.Add(filterChain);
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
            Dictionary<String, decimal?> Pdvs = new Dictionary<string, decimal?>();
            
            chart.AnimationEnabled = true;
            
            // Create a new instance of Title
            Title title = new Title();
            // Set title property
            title.Text = "Volumen";
            // Add title to Titles collection
            chart.Titles.Add(title);
            // Create a new instance of DataSeries
            DataSeries dataSeries = new DataSeries();
            // Set DataSeries property
            dataSeries.RenderAs = RenderAs.Line;          
            // Create a new instance of DataSeries secundary
            DataSeries dataSeriesSecundary = new DataSeries();
            // Set DataSeries property
            dataSeriesSecundary.AxisYType = AxisTypes.Secondary;
            
            dataSeriesSecundary.Name = "PDVs DE CADENA";
            dataSeriesSecundary.Padding = new Thickness(30);
            dataSeriesSecundary.RenderAs = RenderAs.Line;            
            dataSeriesSecundary.MarkerType = Visifire.Commons.MarkerTypes.Cross;
            dataSeriesSecundary.SelectionEnabled = true;
            dataSeriesSecundary.LineThickness = 3;
            //dataSeriesSecundary.ZIndex = 4;

            #region configuracion eje X
            // Creating AxisX
            Axis axisX = new Axis();           
            // Date time standard format
            axisX.ValueFormatString = "000000";
            axisX.AxisOffset = 0.02;
            
            //axisX.IntervalType = IntervalTypes.Months;
            // To avoid auto skip
            //
            chart.AxesX.Add(axisX);
            #endregion

            // Create a DataPoint
            DataPoint dataPoint;
            #region consulta
            DataTable kpis;

            //Adicionando 2do eje
            //Orientation axisYOrientation = Orientation.Vertical;
            //Axis axisY = new Axis();
            //axisY._isAutoGenerated = true;
            //axisY.Chart = chart;
            //axisY.AxisOrientation = axisYOrientation;
            //axisY.AxisType = AxisTypes.Secondary;
            //axisY.PlotDetails = this;
            ////axisY.AxisRepresentation = AxisRepresentations.AxisY;
            //chart.InternalAxesY.Add(axisY);
            //chart.AxesY.Add(axisY);

            string ser = "-1111";
            try
            {
                kpis = new DataTable();
                Consultas consulta = new Consultas();
                kpis = consulta.SelectKPI_Volumen(filtros).Tables[0];
                consulta = null;
                string serAnt = "-5555";
                
                foreach (DataRow g in kpis.Rows)
                {
                    Decimal? y = g["QtySellOut"]!=DBNull.Value?(Decimal?)g["QtySellOut"]:null;
                    Decimal? y2 = g["QtyCPW"]!=DBNull.Value?(Decimal?)g["QtyCPW"]:null;
                    long x = (long)g["TimeId"];
                    ser = (string)g["ManufacturerCode"];
                    if (serAnt != ser)
                    {
                        if (serAnt != "-5555" )
                        {                           
                            chart.Series.Add(dataSeries);                          
                        }
                        
                        // Create a new instance of DataSeries
                        dataSeries = new DataSeries();
                        // Set DataSeries property
                        dataSeries.RenderAs = RenderAs.Line;
                        dataSeries.MarkerType = Visifire.Commons.MarkerTypes.Circle;
                        dataSeries.SelectionEnabled = true;
                        dataSeries.LineThickness = 3;
                        dataSeries.Name = "CPW "+ser; 
                        /*
                            // Create a new instance of DataSeries
                            //dataSeriesSecundary = new DataSeries();
                            // Set DataSeries property
                            dataSeriesSecundary.RenderAs = RenderAs.Line;
                            dataSeriesSecundary.Name = ser;
                            dataSeriesSecundary.MarkerType = Visifire.Commons.MarkerTypes.Circle;
                            dataSeriesSecundary.SelectionEnabled = true;
                            dataSeriesSecundary.LineThickness = 3;
                        */
                    }
                    // Create a new instance of DataPoint
                    dataPoint = new DataPoint();
                    // Set YValue for a DataPoint
                    dataPoint.AxisXLabel = x.ToString();
                    if (!ciclos.Contains(x.ToString())) ciclos.Add(x.ToString());
                    dataPoint.YValue = System.Convert.ToDouble(y);
                    // Add dataPoint to DataPoints collection.
                    dataSeries.DataPoints.Add(dataPoint);
                    //Almacenando los maximos de QtyCPW
                    if (!Pdvs.ContainsKey(x.ToString()))
                    {
                        Pdvs.Add(x.ToString(), y2);
                    }
                    else
                    {
                        decimal? value = Pdvs[x.ToString()];
                        if (y2 > value)
                        {
                            Pdvs.Remove(x.ToString());
                            Pdvs.Add(x.ToString(),y2);
                        }
                    }
                    /*
                    if (ser.Contains("PDV"))
                    {
                        // Create a new instance of DataPoint
                        dataPoint = new DataPoint();
                        // Set YValue for a DataPoint
                        if (x != null) dataPoint.AxisXLabel = x.ToString();
                        if (!ciclos.Contains(x.ToString()))  ciclos.Add(x.ToString());
                        //dataPoint.XValue = x;
                        dataPoint.YValue = System.Convert.ToDouble(y);
                        // Add dataPoint to DataPoints collection.                    
                        dataSeriesSecundary.DataPoints.Add(dataPoint);
                    }
                     */
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
            //Llenando eje secundario
            foreach (string cicloTMP in ciclos)
            {
                // Create a new instance of DataPoint
                dataPoint = new DataPoint();
                // Set YValue for a DataPoint
                dataPoint.AxisXLabel = cicloTMP;                
                //dataPoint.XValue = x;
                decimal? value = -111111111;
                if (Pdvs.ContainsKey(cicloTMP))
                {
                    value = Pdvs[cicloTMP];
                    dataPoint.YValue = System.Convert.ToDouble(value);
                }
                // Add dataPoint to DataPoints collection.                    
                dataSeriesSecundary.DataPoints.Add(dataPoint);
                
            }
            chart.Series.Add(dataSeriesSecundary);
            return chart;
        }
    }
}
