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
    public class ConsultaPorCliente : IMC.Wpf.CoverFlow.NMT.ChartComponent.IChart
    {
        private List<Filtro> filtros = new List<Filtro>();
        private List<String> ciclos = new List<String>();

        public Chart Draw(params string[] filtros)
        {

            // Create a new instance of Chart
            Chart chart = new Chart();


            chart.AnimationEnabled = true;
            // Create a new instance of Title
            Title title = new Title();
            // Set title property
            title.Text = "AREA";
            // Add title to Titles collection
            chart.Titles.Add(title);
            // Create a new instance of DataSeries
            DataSeries dataSeries = new DataSeries();

            // Set DataSeries property
            dataSeries.RenderAs = RenderAs.Area;
            #region configuracion eje X
            // Creating AxisX
            Axis axisX = new Axis();
            // Date time standard format
            //axisX.ValueFormatString = "000000";
            // To avoid auto skip

            chart.AxesX.Add(axisX);
            #endregion
            // Create a DataPoint
            DataPoint dataPoint;
            DataPoint dataPoint2;
            DataPoint dataPointClientes;
            #region consulta
            DataTable kpis;
            DataTable kpiClientes;
            try
            {
                kpis = new DataTable();
                Consultas consulta = new Consultas();
                kpis = consulta.SelectParametros().Tables[0];
                kpiClientes = new DataTable();
                kpiClientes = consulta.SelectPointConsultaClienteFiltrado(filtros[0]).Tables[0];
                consulta = null;
                string serAnt = "-5555";
                string ser = "-1111";
                foreach (DataRow g in kpis.Rows)
                {
                    Decimal? y = g["y"] != DBNull.Value ? (Decimal?)g["y"] : null;
                    Decimal? x = (Decimal?)g["minbudi"];
                    Decimal? x2 = (Decimal?)g["maxbudi"];
                    ser = (string)g["segment"];
                    ser = ser.ToUpper();
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
                        dataSeries.RenderAs = RenderAs.Area;
                        //dataSeries.MarkerType = Visifire.Commons.MarkerTypes.Circle;
                        dataSeries.SelectionEnabled = true;
                        //dataSeries.LineThickness = 3;
                       dataSeries.ToolTipText = null;
                    }

                    // Create a new instance of DataPoint
                    dataPoint = new DataPoint();
                    dataPoint.XValue = System.Convert.ToDouble(x);
                    dataPoint.YValue = System.Convert.ToDouble(y);
                    // Add dataPoint to DataPoints collection.
                    dataSeries.DataPoints.Add(dataPoint);


                    // Create a new instance of DataPoint
                    dataPoint2 = new DataPoint();
                    dataPoint2.XValue = System.Convert.ToDouble(x2);
                    dataPoint2.YValue = System.Convert.ToDouble(y);
                    dataSeries.DataPoints.Add(dataPoint2);

                    serAnt = ser;


                }
                if (ser != "-1111")
                {
                    chart.Series.Add(dataSeries);

                }
                serAnt = "-5555";
                ser = "-1111";

                foreach (DataRow g in kpiClientes.Rows)
                {

                    Decimal? x = (Decimal?)g["TotalBudi"];
                    Decimal? y = (Decimal?)g["CPW"];
                    ser = "Clientes";
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
                        dataSeries.RenderAs = RenderAs.Point;
                        dataSeries.SelectionEnabled = true;

                    }
                    Decimal? valorXreal = x;
                    Decimal? valorYreal = y;
                    if (x > 4)
                        x = 4;
                    if (y > 2500)
                        y = 2500;
                    // Create a new instance of DataPoint
                    dataPointClientes = new DataPoint();
                    dataPointClientes.XValue = System.Convert.ToDouble(x);
                    dataPointClientes.YValue = System.Convert.ToDouble(y);
                    dataPointClientes.ToolTipText = "TOTAL BUDI: "+valorXreal.ToString() + "\nCPW: " + valorYreal.ToString();
                    dataSeries.DataPoints.Add(dataPointClientes);
                    serAnt = ser;

                }
                // Add dataSeries to Series collection.
                if (ser != "-1111")
                {
                    chart.Series.Add(dataSeries);

                }
                return chart;
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
            #endregion

        }
        public List<Filtro> ObtenerFiltros()
        {
            filtros = new List<Filtro>();
            Consultas consulta = new Consultas();
            DataTable filtro1 = consulta.SelectKPI_ConsultaPorCliente_Flt_Cliente().Tables[0];
            Filtro filterIndustryLevel = Herramientas.Instance.obtenerFiltro(filtro1, "Clientes", "Clientes", "Clientes", false);
            filtros.Add(filterIndustryLevel);
            return filtros;
        }
        public List<string> ObtenerCiclos()
        {
            return ciclos;
        }
        public DataTable ObtenerConsultaClientesFiltrado(string Filtro)
        {

            Consultas consulta = new Consultas();
            DataTable ClientesBudi = consulta.SelectConsultaClientesFiltrado(Filtro).Tables[0];
            return ClientesBudi;
        }  
    }
}
