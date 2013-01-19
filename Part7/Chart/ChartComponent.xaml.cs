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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ChartComponent : UserControl
    {
        private int count = 0;
        
        
        private string[] gFiltros;
        /// <summary>
        /// Init
        /// </summary>
        public ChartComponent()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// Dibuja el reporte seleccionado con los filtros asociados
        /// </summary>
        /// <param name="idReporte"></param>
        /// <param name="filtros"></param>
        public void draw(string idReporte, params string[] filtros)
        {
           IChart chart = FactoryChart.Instance.getReporte(idReporte);
           if (chart != null)
           {
              LayoutRoot.Children.Clear();
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
                      if (idReporte == "SobreIndexados")
                      {
                          dataSeries.MouseLeftButtonDown += new MouseButtonEventHandler(dataSeries_MouseLeftButtonDown);
                          dataSeries.MouseRightButtonDown += new MouseButtonEventHandler(dataSeries_MouseLeftButtonDown);
                          gFiltros = filtros;
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
                  if (newChar.Series.Count == 1 && newChar.Series[0].DataPoints.Count>0)
                  {
                      DataSeries dataSeries = new DataSeries();                     
                      // Set DataSeries properties
                      dataSeries.RenderAs = RenderAs.Line;
                      dataSeries.ShowInLegend = false;
                      dataSeries.Name = "HIDDENSERIE";
                      newChar.Series.Add(dataSeries);
                  }
              }
               catch(Exception e)
              {
                  string s = e.Message;
              }

                 LayoutRoot.Children.Add(newChar);
             
           }
        }

        /// <summary>
        /// 
        /// </summary>
		/// <param name="newChar"></param>
        /// <returns></returns>
        private bool tieneData(Chart newChar)
        {
            bool data = false;
            foreach (DataSeries dtemp in newChar.Series)
            {
                if(dtemp.DataPoints.Count>0)
                {
                    data = true;
                    break;
                }
            }
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dataSeries_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ParameterizedThreadStart hiloParametro = new ParameterizedThreadStart(ExportExcel);
                Thread hilo = new Thread(hiloParametro);
                string sIndustryLevel = gFiltros[0];
                string[] sFiltros = new string[3];
                sFiltros[0] = sIndustryLevel;
                sFiltros[1] = (sender as DataPoint).Parent.Name;
                sFiltros[2] = (sender as DataPoint).AxisXLabel.ToString();
                hilo.Start(sFiltros);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error en la exportación a Excel - " + ex.Message);
            }
        }
        /// <summary>
        /// Busca dentro de la serie dada el punto con el AxisXLabel = Xlabel
        /// </summary>
        /// <param name="sender">Contiene los filtros enviados</param>
        public static void ExportExcel(object sender)
        {
            string[] sFiltros;
            sFiltros = (string[])sender;

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Excel|*.xls;*.xlsx";

            if (saveDialog.ShowDialog().Value)
            {
                new NMT_ESC_BLL.Excel.ExcelWriter().generarExcelSobreindexados(saveDialog.FileName, sFiltros[0], "", sFiltros[1], sFiltros[2]);
            }
        }
        /// <summary>
        /// Busca dentro de la serie dada el punto con el AxisXLabel = Xlabel
        /// </summary>
        /// <param name="serie"></param>
        /// <param name="Xlabel"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Obtiene las series
        /// </summary>
        /// <returns></returns>
        public List<SeriesColor> obtenerSeries()
        {
            List<SeriesColor> retorno = new List<SeriesColor>();
            if (LayoutRoot.Children.Count > 0)
            {
                Chart chart = (Chart)LayoutRoot.Children[0];
                foreach (DataSeries ser in chart.Series)
                {
                    if (ser.Color != null && ser.Name != "HIDDENSERIE")
                    {
                        Brush convert = ser.Color;
                        SolidColorBrush newBrush = (SolidColorBrush)convert;
                        SeriesColor sercol = new SeriesColor(newBrush.Color, ser.Name);
                        retorno.Add(sercol);
                    }
                }
            }
            return retorno;
        }
        /// <summary>
        /// cambiar la paleta de colores
        /// </summary>
        public void changePalete(List<Color> colores)
        {
            if (LayoutRoot.Children.Count > 0)
            {
                Chart chart = (Chart)LayoutRoot.Children[0];
                ColorSet set = new ColorSet();
                set.Id = "Custom" + count;
                foreach (Color col in colores)
                {
                    Brush b = new SolidColorBrush(col);
                    set.Brushes.Add(b);
                }
                if (colores != null && colores.Count > 0)
                {
                    chart.ColorSets.Clear();
                    chart.ColorSets.Add(set);
                    chart.ColorSet = "Custom" + count;
                    LayoutRoot.Children.Clear();
                    LayoutRoot.Children.Add(chart);
                    count++;
                }
            }
        }
        /// <summary>
        /// Obtiene los filtros del reporte especificado
        /// </summary>
        /// <param name="idReporte"></param>
        /// <returns></returns>
        public List<Filtro> obtenerFiltros(string idReporte)
        {           
            IChart chart = FactoryChart.Instance.getReporte(idReporte);
            if (chart != null)
                return chart.ObtenerFiltros();
            return new List<Filtro>();
        }
        /// <summary>
        /// Obtiene el color
        /// </summary>
        public Color ColorType { get; set; }
    }
}
