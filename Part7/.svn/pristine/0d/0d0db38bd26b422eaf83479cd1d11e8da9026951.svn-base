using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Visifire.Charts;
using IMC.Wpf.CoverFlow.NMT.ChartComponent.Domain;
using IMC.Wpf.CoverFlow.NMT.Chartcomponent.Domain;
using Microsoft.Win32;
namespace IMC.Wpf.CoverFlow.NMT.ChartComponent
{
    /// <summary>
    /// Lógica de interacción para FacturadoWindow.xaml
    /// </summary>
    public partial class FacturadoWindow : Window
    {
        public FacturadoWindow()
        {
            InitializeComponent();
        }
        public FacturadoWindow(string reporte, string[] filtro)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;                     
            draw(reporte,filtro);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        public void draw(string idReporte, params string[] filtros)
        {
            IChart chart = FactoryChart.Instance.getReporte(idReporte);
            if (chart != null)
            {
                PopFacturado.Children.Clear();
                Axis axis = new Axis();
                ChartGrid grid = new ChartGrid();
                axis.Grids.Add(grid);
                //axis.AxisOffset = 1.5;
                //axis.ValueFormatString = "000.00";
                
                Chart newChar = chart.Draw(filtros);
                // Llenando los AxisXLAbel para llenar los huecos en las series.
                #region llenado de huecos
                if (idReporte != "EjecucionDeCalidad")
                {
                    // Llenando los AxisXLAbel para llenar los huecos en las series.
                    List<String> ciclosOrdered = chart.ObtenerCiclos();
                    ciclosOrdered.Sort();
                    List<DataSeries> oldSeries = new List<DataSeries>();
                    foreach (DataSeries dtemp in newChar.Series)
                    {
                        oldSeries.Add(dtemp);
                    }
                    newChar.Series.Clear();
                    foreach (DataSeries dtemp in oldSeries)
                    {
                        DataSeries dataSeries = new DataSeries();
                        dataSeries.Name = dtemp.Name;
                        // Set DataSeries properties
                        dataSeries.RenderAs = dtemp.RenderAs;
                        dataSeries.MarkerType = dtemp.MarkerType;
                        dataSeries.SelectionEnabled = dtemp.SelectionEnabled;
                        dataSeries.LineThickness = dtemp.LineThickness;
                        dataSeries.AxisYType = dtemp.AxisYType;
                        dataSeries.AxisXType = dtemp.AxisXType;
                        dataSeries.Padding = dtemp.Padding;

                        //Iterando el distinct de los ciclos ordenados 
                        foreach (String cicloTmp in ciclosOrdered)
                        {
                            DataPoint dpTmp = obtenerPuntoPorLabel(dtemp, cicloTmp);
                            if (dpTmp == null)
                            {
                                DataPoint dataPoint = new DataPoint();
                                dataPoint.AxisXLabel = cicloTmp;
                                dataSeries.DataPoints.Add(dataPoint);
                            }
                            else
                            {
                                dataSeries.DataPoints.Add(dpTmp);
                            }
                        }
                       
                        newChar.Series.Add(dataSeries);

                    }
                }
                #endregion
                newChar.Style = (Style)FindResource("ChartStyle");
                newChar.View3D = true;
                //newChar.DataPointWidth = 4.5;
                CornerRadius cr = new CornerRadius(20);
                newChar.CornerRadius = cr;
                newChar.ShadowEnabled = true;
                newChar.AxesY.Add(axis);
                newChar.ZoomingEnabled = true;
                try
                {
                    newChar.Titles[0].Style = (Style)FindResource("TitleStyle");
                    PlotArea p = new PlotArea();
                    p.Padding = new Thickness(30);
                    newChar.PlotArea = p;


                    p.Background = Brushes.LightGray;

                    if (newChar.AxesY.Count > 0 && newChar.AxesY[0].Grids.Count > 0)
                    {
                        newChar.AxesY[0].Grids[0].InterlacedColor = Brushes.White;
                    }

                    //Asignando una serie dummy para que salga la leyenda cuando se tiene una sola serie.
                    if (newChar.Series.Count == 1 && newChar.Series[0].DataPoints.Count > 0)
                    {
                        DataSeries dataSeries = new DataSeries();
                        // Set DataSeries properties
                        dataSeries.RenderAs = RenderAs.Line;
                        dataSeries.ShowInLegend = false;
                        dataSeries.Name = "HIDDENSERIE";
                        newChar.Series.Add(dataSeries);
                    }
                }
                catch (Exception e)
                {
                    string s = e.Message;
                }

                PopFacturado.Children.Add(newChar);

            }
        }
        private DataPoint obtenerPuntoPorLabel(DataSeries serie, string Xlabel)
        {
            foreach (DataPoint dpTmp in serie.DataPoints)
            {
                if (dpTmp.AxisXLabel == Xlabel)
                {
                    return dpTmp;
                }

            }
            return null;

        }

    }
}
