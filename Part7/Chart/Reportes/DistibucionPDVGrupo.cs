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
    class DistibucionPDVGrupo : IMC.Wpf.CoverFlow.NMT.ChartComponent.IChart
    {
        private List<Filtro> filtros = new List<Filtro>();
        private List<String> ciclos = new List<String>();

        public List<Filtro> ObtenerFiltros()
        {
            filtros = new List<Filtro>();
            Consultas consulta = new Consultas();
            DataTable filtro1 = consulta.SelectKPI_DistibucionPDVGrupo_Flt_Segmentacion().Tables[0];
            Filtro filterIndustryLevel = Herramientas.Instance.obtenerFiltro(filtro1, "Segmentacion", "Segmentacion", "Segmentacion", true);
            filtros.Add(filterIndustryLevel);
            return filtros;
        }

        public Chart Draw(params string[] filtros)
        {
            // Create a new instance of Chart
            Chart chart = new Chart();


            chart.AnimationEnabled = true;
            // Create a new instance of Title
            Title title = new Title();
            // Set title property
            title.Text = "DISTRIBUCION PDV GRUPO";
            // Add title to Titles collection
            chart.Titles.Add(title);
            // Create a new instance of DataSeries
            DataSeries dataSeries = new DataSeries();
            DataSeries dataSeriesSecundary = new DataSeries();
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
                kpis = consulta.SelectKPI_DistibucionPDVGrupo(filtros).Tables[0];
                consulta = null;
                bool band1 = true;
                bool band2 = true;
                string serAnt = "-5555";
                string ser = "-1111";
                foreach (DataRow g in kpis.Rows)
                {

                    int? y = g["TotalClientes"] != DBNull.Value ? (int?)g["TotalClientes"] : null;
                    int x = (int)g["Ciclo"];
                    ser = (string)g["Segmentacion"];
                    ser = ser.ToUpper();
                    //--Condicion para el hueco se vaya a cero y no se corte la linea.
                    //if (y == null)
                    //    y = 0;
                    if (ser != "TOTAL MAYORISTAS")
                    { //Creando las series
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
                            if (ser == "TIGRE")
                                dataSeries.MarkerType = Visifire.Commons.MarkerTypes.Diamond;
                            if (ser == "LOBO")
                                dataSeries.MarkerType = Visifire.Commons.MarkerTypes.Triangle;
                            if (ser == "AGUILA")
                                dataSeries.MarkerType = Visifire.Commons.MarkerTypes.Square;
                            if (ser == "COBRA")
                                dataSeries.MarkerType = Visifire.Commons.MarkerTypes.Cross;
                            if (ser == "PEZ")
                                dataSeries.MarkerType = Visifire.Commons.MarkerTypes.Line;
                            dataSeries.SelectionEnabled = true;
                            dataSeries.LineThickness = 3;
                            band1 = false;
                        }

                        // Create a new instance of DataPoint
                        dataPoint = new DataPoint();
                        // Set YValue for a DataPoint
                        if (x != null && y != null) dataPoint.AxisXLabel = x.ToString();
                        if (!ciclos.Contains(x.ToString())) ciclos.Add(x.ToString());
                        dataPoint.YValue = System.Convert.ToDouble(y);
                        // Add dataPoint to DataPoints collection.
                        dataSeries.DataPoints.Add(dataPoint);
                        serAnt = ser;

                    }
                    else
                    {
                        if (serAnt != ser)
                        {
                            //////////////dataseries secundario para segundo eje y
                            // Create a new instance of DataSeries secundary
                            // Set DataSeries property
                            dataSeriesSecundary.AxisYType = AxisTypes.Secondary;
                            dataSeriesSecundary.Name = "TOTAL MAYORISTAS";
                            dataSeriesSecundary.Padding = new Thickness(30);
                            dataSeriesSecundary.RenderAs = RenderAs.Line;
                            dataSeriesSecundary.MarkerType = Visifire.Commons.MarkerTypes.Circle;
                            dataSeriesSecundary.SelectionEnabled = true;
                            dataSeriesSecundary.LineThickness = 3;
                            //////////////////////////////////////////////   
                            band2 = false;

                        }
                        dataPoint = new DataPoint();
                        if (x != null && y != null) dataPoint.AxisXLabel = x.ToString();
                        if (!ciclos.Contains(x.ToString())) ciclos.Add(x.ToString());
                        dataPoint.YValue = System.Convert.ToDouble(y);
                        dataSeriesSecundary.DataPoints.Add(dataPoint);
                        serAnt = ser;
                    }
                }
                // Add dataSeries to Series collection.
                if(band1!=true)
                    chart.Series.Add(dataSeries);
                if (band2 != true)
                    chart.Series.Add(dataSeriesSecundary);
                
                return chart;
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
            #endregion
            
        }

        public List<string> ObtenerCiclos()
        {
            return ciclos;
        }
    }
}
