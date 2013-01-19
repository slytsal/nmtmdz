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
    class VisitasPlaneadas : IMC.Wpf.CoverFlow.NMT.ChartComponent.IChart
    {
        private List<Filtro> filtros = new List<Filtro>();
        private List<String> ciclos = new List<String>();
        public List<Filtro> ObtenerFiltros()
        {
            filtros = new List<Filtro>();
            //Consultas consulta = new Consultas();
            //DataTable filtro1 = consulta.SelectKPI_MDZ_VisitasPlaneadas_MDZDescription().Tables[0];
            //Filtro filterIndustryLevel = Herramientas.Instance.obtenerFiltro(filtro1, "Segmentacion", "Segmentacion", "Segmentacion", false);
            ////filtros.Add(filterIndustryLevel);
            return filtros;
        }
        public List<Filtro> ObtenerFiltrosDescripcion()
        {
            filtros = new List<Filtro>();
            Consultas consulta = new Consultas();
            DataTable filtro1 = consulta.SelectKPI_MDZ_VisitasPlaneadas_MDZDescription().Tables[0];
            Filtro filterIndustryLevel = Herramientas.Instance.obtenerFiltro(filtro1, "MDZ Description", "MDZ Description", "MDZ Description", false);
            filtros.Add(filterIndustryLevel);
            return filtros;
        }
        public Chart Draw(params string[] filtros)
        {
            //// Create a new instance of Chart
            Chart chart = new Chart();
            //chart.AnimationEnabled = true;
            //// Create a new instance of Title
            //Title title = new Title();
            //// Set title property
            //title.Text = "CLIENTES POR VISIBILIDAD";
            //// Add title to Titles collection
            //chart.Titles.Add(title);
            //// Create a new instance of DataSeries
            //DataSeries dataSeries = new DataSeries();
            //// Set DataSeries property
            //dataSeries.RenderAs = RenderAs.Line;
            //#region configuracion eje X
            //// Creating AxisX
            //Axis axisX = new Axis();
            //// Date time standard format
            //axisX.ValueFormatString = "000000";
            //// To avoid auto skip

            //chart.AxesX.Add(axisX);
            //#endregion
            //// Create a DataPoint
            //DataPoint dataPoint;
            //#region consulta
            //DataTable kpis;
            //try
            //{
            //    kpis = new DataTable();
            //    Consultas consulta = new Consultas();
            //    kpis = consulta.SelectKPI_MDZ_VisitasPlaneadas(filtros).Tables[0];
            //    consulta = null;
            //    string serAnt = "-5555";
            //    string ser = "-1111";
            //    foreach (DataRow g in kpis.Rows)
            //    {
            //        int? y = g["NumeroClientes"] != DBNull.Value ? (int?)g["NumeroClientes"] : null;
            //        int x = (int)g["Ciclo"];
            //        if (g["Rango"] != DBNull.Value)
            //            ser = (string)g["Rango"];
            //        ser = ser.ToUpper();
            //        //--Condicion para el hueco se vaya a cero y no se corte la linea.
            //        //if (y == null)
            //        //    y = 0;

            //        //Creando las series
            //        if (serAnt != ser && ser != "-1111")
            //        {
            //            if (serAnt != "-5555")
            //            {
            //                chart.Series.Add(dataSeries);
            //            }
            //            // Create a new instance of DataSeries
            //            dataSeries = new DataSeries();
            //            dataSeries.Name = ser;
            //            // Set DataSeries property
            //            dataSeries.RenderAs = RenderAs.Line;
            //            dataSeries.MarkerType = Visifire.Commons.MarkerTypes.Circle;
            //            dataSeries.SelectionEnabled = true;
            //            dataSeries.LineThickness = 3;
            //        }

            //        // Create a new instance of DataPoint
            //        dataPoint = new DataPoint();
            //        // Set YValue for a DataPoint
            //        if (x != null && y != null) dataPoint.AxisXLabel = x.ToString();
            //        if (!ciclos.Contains(x.ToString())) ciclos.Add(x.ToString());
            //        dataPoint.YValue = System.Convert.ToDouble(y);
            //        // Add dataPoint to DataPoints collection.
            //        dataSeries.DataPoints.Add(dataPoint);
            //        serAnt = ser;

            //    }
            //}
            //catch (Exception Error)
            //{
            //    throw (new Exception(Error.ToString()));
            //}
            //#endregion
            //// Add dataSeries to Series collection.
            //chart.Series.Add(dataSeries);
            return chart;
        }
        public List<string> ObtenerCiclos()
        {
            return ciclos;
        }

    }
}
