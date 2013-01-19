using System.Windows;
using System.Windows.Input;
using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using NMT_ESC_DAL;
using NMT_ESC_BLL.Domain;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Documents;
using IMC.Wpf.CoverFlow.NMT.ChartComponent.Domain;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Media;
using Microsoft.Win32;
using FluidKit.NMT.GlassWindow;
using IMC.Wpf.CoverFlow.NMT.Media;
using System.Collections.Specialized;
using FluidKit.Controls;
using IMC.Wpf.CoverFlow.NMT.Chartcomponent.Domain;
using System.Threading;
using System.Globalization;

namespace IMC.Wpf.CoverFlow.NMT
{
    /// <summary>
    /// Ventana de seleccion de indicadores
    /// </summary>
    public partial class IndicadorWindow : Window
    {
        
        public static bool isConnected = true;
        public static int count=0;
        private static IndicadorWindow instance;
        private GrupoIndicador grupo = new GrupoIndicador();
        private Window parent;
        private StringCollection _dataSource;
        private LayoutBase[] _layouts = {
		      new SlideDeck()		                                	
		};
        private Random _randomizer = new Random();
        private String lastfiltros = "";
        /// <summary>
        ///  Patron de disenio solitario
        /// </summary>
        public static IndicadorWindow Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new IndicadorWindow(0,"","","",null,0,"");
                }
                return instance;
            }
        }
        /// <summary>
        /// Comparador
        /// </summary>
        private class FileInfoComparer : IComparer<FileInfo>
        {
            #region IComparer<FileInfo> Membres
            public int Compare(FileInfo x, FileInfo y)
            {
                return string.Compare(x.FullName, y.FullName);
            }
            #endregion
        }
        #region Handlers
        /// <summary>
        /// Evento del teclado
        /// </summary>
        /// <param name="key"></param>
        private void DoKeyDown(Key key)
        {
            switch (key)
            {
                case Key.Right:
                    flow.GoToNext();
                    break;
                case Key.Left:
                    flow.GoToPrevious();
                    break;
                case Key.PageUp:
                    flow.GoToNextPage();
                    break;
                case Key.PageDown:
                    flow.GoToPreviousPage();
                    break;
            }
            
                  
        }
        /// <summary>
        /// Evento del teclado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            DoKeyDown(e.Key);
        }
        
        #endregion

        #region Private stuff

        /// <summary>
        /// Carga de indicadores para el grupo seleccionado
        /// </summary>
        public void Load()
        {
            if (checkSMS() != 0)
            {
                #region consulta
                    DataTable kpis;
                    try
                    {
                        flow.clear();
                        kpis = new DataTable();
                        Consultas consulta = new Consultas();
                        kpis = consulta.getNMTKPIByGroup(grupo.Id).Tables[0];
                        consulta = null;
                        foreach (DataRow g in kpis.Rows)
                        {
                            int ID = (Int32)g["ID"];
                            string imagenPath = (String)g["RUTA"];
                            string about = (String)g["ACERCADE"];
                            string desc = (String)g["DESCRIPCION"];
                            string colors="";
                            if(g["COLORES"]!= System.DBNull.Value)
                                colors = (String)g["COLORES"];
                            flow.Add(Environment.MachineName, imagenPath,about,desc,ID,0,colors);
                        }                        
                    }
                    catch (Exception Error)
                    {
                        MessageBox.Show("Error al conectarse a la base de datos NMT. Favor de contactar a Help Desk - " + Error.Message);
                        Debug.Write(Error.Message);
                    }
                #endregion                
            }
            else
            {
                MessageBox.Show("No existe un Usuario o Territorio configurado en iSMS");
                this.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadKit()
        {
            try
            {                
                _elementFlow.Layout = _layouts[0];
                
                //_elementFlow.SelectedIndex = 0;
                _dataSource = new StringCollection();
                if (checkSMS() != 0)
                {
                    #region consulta
                    DataTable kpis;
                    try
                    {                        
                        kpis = new DataTable();
                        Consultas consulta = new Consultas();
                        kpis = consulta.getNMTKPIByGroup(grupo.Id).Tables[0];
                        consulta = null;
                        foreach (DataRow g in kpis.Rows)
                        {
                            int ID = (Int32)g["ID"];
                            string imagenPath = (String)g["RUTA"];
                            string about = (String)g["ACERCADE"];
                            string desc = (String)g["DESCRIPCION"];
                            //flow.Add(Environment.MachineName, imagenPath, about, desc, ID,0,"");
                            _dataSource.Add(imagenPath);
                        }
                        
                    }
                    catch (Exception Error)
                    {
                        throw (new Exception(Error.ToString()));
                    }
                    #endregion
                }
                else
                {
                    MessageBox.Show("No existe un Usuario o Territorio configurado en iSMS");
                    this.Close();
                }
                _elementFlow.ItemsSource = _dataSource;
                _elementFlow.SelectedIndex = this.grupo.LAST_INDEX;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectarse a la base de datos NMT. Favor de contactar a Help Desk - " + ex.Message);
                Debug.Write(ex.Message);
                ex.ToString();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void obtenerVersion()
        {
            Consultas consulta = new Consultas();
            DataTable version = consulta.getVersionBitacora(tbTerrirotio.Text).Tables[0];
            foreach (DataRow g in version.Rows)
            {
                string ver = (String)g["VERSION_CLIENTE"];
                DateTime fecha = (DateTime)g["FECHA_SINCRONIZACION"];
                this.tbAnio.Text = fecha.ToString("yyyy");
                this.tbMes.Text = fecha.ToString("MMMM");
                this.tbDia.Text = fecha.ToString("dd");
            }
            if (version.Rows.Count == 0)
            {
                this.tbAnio.Visibility = Visibility.Visible;
                this.tbMes.Visibility = Visibility.Visible;
                this.tbDia.Visibility = Visibility.Visible;
            }
        }
        

        #endregion
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="IdGrupo"></param>
        /// <param name="nombreGrupo"></param>
        /// <param name="imagen"></param>
        /// <param name="descripcion"></param>
        /// <param name="parent"></param>
        /// <param name="LAST_INDEX"></param>
        /// <param name="LAST_FILTER"></param>
        public IndicadorWindow(int IdGrupo, String nombreGrupo, String imagen,String descripcion, Window parent,int LAST_INDEX, string LAST_FILTER)
        {
            InitializeComponent();
            //CultureInfo ci = new CultureInfo("en-GB");
            //Thread.CurrentThread.CurrentCulture = ci;
            //Thread.CurrentThread.CurrentUICulture = ci;
            this.parent = parent;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.WindowState = WindowState.Maximized;
            flow.parentWin = this;
            flow.Cache = new ThumbnailManager();
            this.grupo = new GrupoIndicador();
            this.grupo.About = nombreGrupo;
            this.grupo.Image = imagen;
            this.grupo.Id = IdGrupo;
            this.grupo.Desc = descripcion;
            this.grupo.LAST_FILTER = LAST_FILTER;
            this.grupo.LAST_INDEX = LAST_INDEX;
            string strVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.tbVersion.Text = strVersion;
            Load();
            init();
            _elementFlow.SelectedIndex = this.grupo.LAST_INDEX;            
            Loaded += Window1_Loaded;
            this.obtenerVersion();
            //System.Threading.Thread newThread;
            //newThread = new System.Threading.Thread(this.getStatusConn);            
            //newThread.Start();
            //System.Threading.Thread.Sleep(1000);
            //checkConn();
            ThreadPool.QueueUserWorkItem(new WaitCallback(getStatusConn), this);
            initfilters();
            this.draw();
            
        }        

        /// <summary>
        /// Evento on load  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            this.LoadKit();
            _elementFlow.SelectionChanged += EFSelectedIndexChanged;
        }

        /// <summary>
        /// Evento deteccion de cambio de indice en flowcontrol
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EFSelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/click.wav");
            Debug.WriteLine((sender as FluidKit.Controls.ElementFlow).SelectedIndex);
            flow.UpdateIndex((sender as FluidKit.Controls.ElementFlow).SelectedIndex);
            
        }

        /// <summary>
        /// Llena los filtros
        /// </summary>
        public void initfilters()
        {
            int idIndicador = flow.getActual().ID;
            string reporte = System.Configuration.ConfigurationManager.AppSettings["" + idIndicador];
            List<Filtro> filtros;
            
                    filtros = chart.obtenerFiltros(reporte);
               
            ////BZG - INI - Valida excepeciones KPI
            if (idIndicador == 1 || idIndicador == 4 || idIndicador == 10)
                this.drawFiltersAux(filtros,idIndicador);
            else
               this.drawFilters(filtros, idIndicador);
               
            }
            //BZG - FIN - Valida excepeciones KPI   
        

       /// <summary>
       /// Reiniciar la ventana de indicadores
       /// </summary>
       /// <param name="IdGrupo"></param>
       /// <param name="nombreGrupo"></param>
       /// <param name="imagen"></param>
       /// <param name="descripcion"></param>
       /// <param name="LAST_INDEX"></param>
       /// <param name="LAST_FILTER"></param>
        public void restart(int IdGrupo, String nombreGrupo, String imagen, String descripcion, int LAST_INDEX, string LAST_FILTER)
        {
            flow.reinit();
            flow.Cache = new ThumbnailManager();
            this.grupo = new GrupoIndicador();
            this.grupo.About = nombreGrupo;
            this.grupo.Image = imagen;
            this.grupo.Id = IdGrupo;
            this.grupo.Desc = descripcion;
            this.grupo.LAST_FILTER = LAST_FILTER;
            this.grupo.LAST_INDEX = LAST_INDEX;            
            Load();
            LoadKit();
            _elementFlow.SelectedIndex = this.grupo.LAST_INDEX;
            DataTable kpi = new DataTable();
            Consultas cons = new Consultas();            
            kpi = cons.getNMTKPIByID(flow.getActual().ID).Tables[0];
            string filtro = "";
            foreach (DataRow g in kpi.Rows)
            {
                if (g["FILTRO"] != DBNull.Value)
                    filtro = (String)g["FILTRO"];
            }
            this.lastfiltros = filtro;
            flow.UpdateIndex(this.grupo.LAST_INDEX);
            init();
            initfilters();
            this.draw();
        }
       
        /// <summary>
        /// Iniciacion de los objetos de negocio
        /// </summary>
        private void init()
        {
            this.tbAboutGrupo.Text = this.grupo.About;
            this.tbDescripcion.Text = this.grupo.Desc;
            string strUri2 = this.grupo.Image;
            this.imageGrupo.Source = new BitmapImage(new Uri(strUri2));
            if (flow.Count != 0)
            {
                this.tbAboutIndicador.Text = flow.getActual().About;
                this.tbDescripcion.Text = flow.getActual().Desc;
            }
        }

        /// <summary>
        /// Comprueba la conexión a la basa de datos SMS
        /// </summary>
        private int checkSMS()
        {
            int estado;
            DataTable valores;
            try
            {
                string nombreBD = System.Configuration.ConfigurationManager.AppSettings["NombreBDiSMS"];
                estado = 0;
                valores = new DataTable();
                Consultas consulta = new Consultas();
                valores = consulta.getAutenticacion(nombreBD).Tables[0];
                consulta = null;

                if (valores.Rows.Count == 0)
                {
                    return estado;
                }
                else
                {
                    this.tbUsuario.Text = valores.Rows[0][0].ToString();
                    this.tbTerrirotio.Text = valores.Rows[0][1].ToString();
                    return estado = 1;
                }

            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));               
            }
        }

        /// <summary>
        /// Ejecuta el reporte
        /// </summary>
        public void draw()
        {
            Consultas cons = new Consultas();
            List<string> filters = new List<string>();
            lastfiltros = "";
            foreach (UIElement el in spFiltros.Children)
            {
                if (el.GetType().Name == "ComboBox")
                {
                    if (((ComboBoxItem)((ComboBox)el).SelectedValue) != null)
                    {
                        String c = (String)((ComboBoxItem)((ComboBox)el).SelectedValue).Content;
                        c = c.Replace("Seleccione uno(a)", "").Replace("Todo(a)s", "");
                        filters.Add(c);
                        lastfiltros += ((ComboBox)el).Name + "=" + c + "|";
                    }
                }
                else if (el.GetType().Name == "ComboWithCheckboxes")
                {
                    String c = ((Controls.ComboWithCheckboxes)el).Text;
                    filters.Add(c);
                    lastfiltros += ((Controls.ComboWithCheckboxes)el).Name + "=" + c + "|";
                }
            }
            int idIndicador = flow.getActual().ID;
            string reporte = System.Configuration.ConfigurationManager.AppSettings[""+idIndicador];
            
                
                chart.LayoutRoot.Children.Clear();
                chart.draw(reporte, filters.ToArray());
            
            
            DataTable kpi = new DataTable();
            cons.UpdateLastFiltroIndicador(flow.getActual().ID, lastfiltros);
            kpi = cons.getNMTKPIByID(flow.getActual().ID).Tables[0];
            
            string colors = "";
            foreach (DataRow g in kpi.Rows)
            {
                if (g["COLORES"] != DBNull.Value)
                    colors = (String)g["COLORES"];
            }
            if (colors != null && colors != "")
            {
                List<Color> colores = new List<Color>();
                foreach (String el in colors.Split('|'))
                {
                    if (el != "")
                    {
                        Color c = (Color)ColorConverter.ConvertFromString(el);
                        colores.Add(c);
                    }                    
                }
                //colores.Add(Colors.LightSalmon);
                //colores.Add(Colors.HotPink);
                //colores.Add(Colors.Indigo);
                //colores.Add(Colors.Honeydew);
                //colores.Add(Colors.Orange);
                //colores.Add(Colors.PaleGoldenrod);
                //colores.Add(Colors.PaleVioletRed);
                //colores.Add(Colors.Peru);
                //colores.Add(Colors.SkyBlue);
                //colores.Add(Colors.Turquoise);
                //colores.Add(Colors.WhiteSmoke);
                //colores.Add(Colors.YellowGreen);
                //colores.Add(Colors.Tomato);
                //colores.Add(Colors.Gold);
                //colores.Add(Colors.Fuchsia);
                //colores.Add(Colors.DarkTurquoise);
                //colores.Add(Colors.DarkOliveGreen);
                //colores.Add(Colors.Coral);
                //colores.Add(Colors.Aqua);
                //colores.Add(Colors.AliceBlue);
                chagePaleta(colores);
            }
        }
        /// <summary>
        /// Obtiene valor de un name en los filtros dinamicos
        /// </summary>
        /// <returns></returns>
        private string obetenerLastFiltro(string name)
        {
            DataTable kpi = new DataTable();
            Consultas cons = new Consultas();
            kpi = cons.getNMTKPIByID(flow.getActual().ID).Tables[0];
            string filtro = "";
            foreach (DataRow g in kpi.Rows)
            {
                if (g["FILTRO"] != DBNull.Value)
                    filtro = (String)g["FILTRO"];
            }
            this.lastfiltros = filtro;
            string[] lastFiltrosVector = lastfiltros.Split('|');
            foreach (string fil in lastFiltrosVector)
            {

                if (fil != "" && fil.Replace(" ", "").Split('=')[0] == name)
                    return fil.Split('=')[1];
            }
            return null;
        }
        /// <summary>
        /// Deacuerdo a los filtros los agraga dinamicamente
        /// </summary>
        /// <param name="filtros"></param>
        private void drawFilters(List<Filtro> filtros,int id)
        {            
            spFiltros.Children.Clear();            
            foreach (Filtro f in filtros)
            {
                Label newLabel = new Label();
                newLabel.Content = f.label;
                //BZG - INI - Oculta Combos SubfamilyCode
                string a = "";
                if (f.label == "Product Subfamily Code" || f.label == "Brand Code")
                    a = "";
                else
                 spFiltros.Children.Add(newLabel);
                //BZG - FIN - Oculta Combos SubfamilyCode
                ComboBox comb = new ComboBox();
                comb.Name = "comb_" + f.label.Replace(" ","");
                Controls.ComboWithCheckboxes cbN = new Controls.ComboWithCheckboxes();
                cbN.Name = "cbN_" + f.label.Replace(" ", "");
                Controls.ObservableNodeList itemSource = new Controls.ObservableNodeList();
                string busComb = obetenerLastFiltro(comb.Name);
                string buscbN = obetenerLastFiltro(cbN.Name);
                //BZG - INI - Oculta Combos SubfamilyCode
                if (cbN.Name.Contains("SubfamilyCode") || cbN.Name.Contains("BrandCode"))
                    cbN.Visibility = Visibility.Hidden;
                //BZG - FIN - Oculta Combos SubfamilyCode
                //if (cbN.Name.Contains("SubfamilyCode") || cbN.Name.Contains("ProductSubfamilyDescription") || cbN.Name.Contains("BrandCode") || cbN.Name.Contains("BrandDescription"))
                if (cbN.Name.Contains("SubfamilyCode") || cbN.Name.Contains("Marca") || cbN.Name.Contains("BrandCode") || cbN.Name.Contains("Subfamilia"))
                    cbN.GotFocus += new RoutedEventHandler(TextBox_LostFocus);
                foreach (Item item in f.values)
                {                    
                    ComboBoxItem citem = new ComboBoxItem();
                    citem.Content = item.desc;                    
                    comb.Items.Add(citem);
                    Controls.Node i = new Controls.Node(item.desc,item.valorDependencia);
                    i.IsSelected = true;
                    itemSource.Add(i);
                    if (busComb != null && busComb != "" && item.desc==busComb)
                    {
                        comb.SelectedValue = citem;
                    }                                       
                    
                }
                if (f.values.Count>0 && comb.SelectedValue==null)
                    comb.SelectedIndex = 0;   
             
                cbN.ItemsSource = itemSource;
                if (buscbN != null && buscbN != "")
                {
                    cbN.Text = buscbN;
                    
                    foreach (Controls.Node node in  ((Controls.ObservableNodeList)cbN.ItemsSource))
                    {
                        node.IsSelected = false;
                        string[] buscados = buscbN.Split(',');
                        List<string> lista = new List<string>(buscados);
                        if(lista.Contains(node.Title))
                            node.IsSelected = true;                            
                    }                    
                }
                if (!f.multiselection)
                    spFiltros.Children.Add(comb);
                else
                {
                    spFiltros.Children.Add(cbN);
                    IMCEXT.CustomParts.AddSelectAll(id, cbN, spFiltros, this);
                }
            }
            if (filtros == null || filtros.Count == 0)
            {
                filter.Visibility = Visibility.Hidden;
                labelFiltro.Visibility = Visibility.Hidden;
            }
            else
            {
                filter.Visibility = Visibility.Visible;
                labelFiltro.Visibility = Visibility.Visible;
            }                                               
            
        }
        
        private void drawFiltersAux(List<Filtro> filtros,int id)
        {
            spFiltros.Children.Clear();
            foreach (Filtro f in filtros)
            {
                Label newLabel = new Label();

                newLabel.Content = f.label;
                

                //BZG - INI - Oculta Combos SubfamilyCode
                string nada = "";
                //if (f.label == "Manufacturer Code" || f.label == "Industry Level")
                if (f.label == "Manufacturer Code")// || f.label == "Potencial")
                    nada = "";
                else
                    spFiltros.Children.Add(newLabel);
                //BZG - FIN - Oculta Combos SubfamilyCode


                ComboBox comb = new ComboBox();
                comb.Name = "comb_" + f.label.Replace(" ", "");
                Controls.ComboWithCheckboxes cbN = new Controls.ComboWithCheckboxes();
                cbN.Name = "cbN_" + f.label.Replace(" ", "");
                Controls.ObservableNodeList itemSource = new Controls.ObservableNodeList();
                string busComb = obetenerLastFiltro(comb.Name);
                string buscbN = obetenerLastFiltro(cbN.Name);

                //BZG - INI - Oculta Combos SubfamilyCode
                //if (cbN.Name.Contains("cbN_ManufacturerCode") || cbN.Name.Contains("cbN_IndustryLevel"))
                if (cbN.Name.Contains("cbN_ManufacturerCode") )//|| cbN.Name.Contains("cbN_Potencial"))
                    cbN.Visibility = Visibility.Hidden;
                 
                //BZG - FIN - Oculta Combos SubfamilyCode
                //if (cbN.Name.Contains("SubfamilyCode") || cbN.Name.Contains("ProductSubfamilyDescription") || cbN.Name.Contains("BrandCode") || cbN.Name.Contains("BrandDescription"))
                if (cbN.Name.Contains("SubfamilyCode") || cbN.Name.Contains("Marca") || cbN.Name.Contains("BrandCode") || cbN.Name.Contains("Subfamilia"))
                    cbN.GotFocus += new RoutedEventHandler(TextBox_LostFocus);
                foreach (Item item in f.values)
                {


                    ComboBoxItem citem = new ComboBoxItem();
                    citem.Content = item.desc;
                    comb.Items.Add(citem);
                    Controls.Node i = new Controls.Node(item.desc, item.valorDependencia);
                    i.IsSelected = true;
                    itemSource.Add(i);



                    if (busComb != null && busComb != "" && item.desc == busComb)
                    {
                        comb.SelectedValue = citem;
                    }
                }
                if (f.values.Count > 0 && comb.SelectedValue == null)
                    comb.SelectedIndex = 0;
                cbN.ItemsSource = itemSource;
                if (buscbN != null && buscbN != "")
                {
                    cbN.Text = buscbN;

                    foreach (Controls.Node node in ((Controls.ObservableNodeList)cbN.ItemsSource))
                    {
                        node.IsSelected = false;
                        string[] buscados = buscbN.Split(',');
                        List<string> lista = new List<string>(buscados);
                        if (lista.Contains(node.Title))
                            node.IsSelected = true;
                    }
                }

                if (!f.multiselection)
                    spFiltros.Children.Add(comb);
                else
                {
                    spFiltros.Children.Add(cbN);
                    IMCEXT.CustomParts.AddSelectAll(id, cbN, spFiltros, this);
                }
            }


            if (filtros == null || filtros.Count == 0)
            {
                filter.Visibility = Visibility.Hidden;
                labelFiltro.Visibility = Visibility.Hidden;
            }
            else
            {
                filter.Visibility = Visibility.Visible;
                labelFiltro.Visibility = Visibility.Visible;
            }

        }
        /// <summary>
        /// Evento de cambio de texto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void textChange(Object sender,TextCompositionEventArgs e )
        {
            string com = e.ControlText;
            com.ToString();
        }
        /// <summary>
        /// Evento de cambio de foco
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Controls.ComboWithCheckboxes combo = ((Controls.ComboWithCheckboxes)sender);
            string name = combo.Name;
            string text = combo.Text;
            this.changeDataMultiCheck(name);
        }
        /// <summary>
        /// Busca en los MultiCheck el campo para seleccionarle al codigo escogido la descripcion o viseversa
        /// </summary>
        /// <param name="nameSource"></param>
        private void changeDataMultiCheck(string nameSource)
        {
            UIElementCollection colection = spFiltros.Children;
            string textoDependencia = this.obtenerTextoDependiente(nameSource);
            string nameDependiente = "cbN_";
            if (nameSource.Contains("SubfamilyCode"))            
                //nameDependiente += "ProductSubfamilyDescription"; 
                nameDependiente += "Marca"; 
            //else if (nameSource.Contains("ProductSubfamilyDescription"))
            else if (nameSource.Contains("Marca"))
                nameDependiente += "ProductSubfamilyCode";
            else if (nameSource.Contains("BrandCode"))
                //nameDependiente += "BrandDescription";
                nameDependiente += "Subfamilia";
            //else if (nameSource.Contains("BrandDescription"))
            else if (nameSource.Contains("Subfamilia"))
                nameDependiente += "BrandCode";
            this.asignarTextoDependiente(nameDependiente,textoDependencia);
        }
        /// <summary>
        /// Obtiene para cada uno de los codigo del combo sus descripciones o viseversa 
        /// </summary>
        /// <param name="nameSource"></param>
        /// <returns></returns>
        private string obtenerTextoDependiente(string nameSource)
        {
            string textoDependencia = "";
            foreach (UIElement el in spFiltros.Children)
            {
                if (el.GetType().Name == "ComboWithCheckboxes")
                {
                    if (((Controls.ComboWithCheckboxes)el).Name == nameSource)
                    {
                        String textoActual = ((Controls.ComboWithCheckboxes)el).Text;
                        string[] selecciones = textoActual.Split(',');
                        foreach (string sel in selecciones)
                        {
                            ObservableCollection<Controls.Node> items = (ObservableCollection<Controls.Node>)(((Controls.ComboWithCheckboxes)el).ItemsSource);
                            foreach (Controls.Node n in items)
                            {
                                if (n.Title == sel)
                                {
                                    textoDependencia += n.value + ",";
                                }
                            }
                        }
                    }
                }
            }
            if (textoDependencia.Length-1>=0)
                textoDependencia = textoDependencia.Substring(0, textoDependencia.Length - 1);
            return textoDependencia;
        }
        /// <summary>
        /// Cambiar el texto 
        /// </summary>
        /// <param name="nameSource"></param>
        /// <param name="seleccionados"></param>
        public void asignarTextoDependiente(string nameSource, string seleccionados)
        {            
            foreach (UIElement el in spFiltros.Children)
            {
                if (el.GetType().Name == "ComboWithCheckboxes")
                {
                    if (((Controls.ComboWithCheckboxes)el).Name == nameSource)
                    {
                        
                        List<Controls.Node> items = new List<Controls.Node>();
                        ((Controls.ComboWithCheckboxes)el).Text = seleccionados;
                        string[] selecciones = seleccionados.Split(',');
                        System.Console.WriteLine("Iniciando asignacion");
                        foreach (Controls.Node n in ((Controls.ObservableNodeList)((Controls.ComboWithCheckboxes)el).ItemsSource))
                        {                            
                            Controls.Node nod = new Controls.Node(n.Title,n.value);
                            n.IsSelected = false;
                            nod.IsSelected = false;
                            foreach (string sel in selecciones)
                            { 
                                if (n.Title == sel)
                                {
                                    n.IsSelected = true;
                                    nod.IsSelected = true;
                                    Debug.WriteLine(n.Title);
                                    System.Console.WriteLine(n.Title);
                                }                                                                
                            }
                            items.Add(nod);
                        }
                        try
                        {
                            ((Controls.ObservableNodeList)((Controls.ComboWithCheckboxes)el).ItemsSource).Clear();
                            foreach (Controls.Node nTemp in items)
                            {
                                ((Controls.ObservableNodeList)((Controls.ComboWithCheckboxes)el).ItemsSource).Add(nTemp);
                            }
                        }
                        catch (Exception e)
                        {
                            e.ToString();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Evento minimizar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageMin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/click.wav");
            this.WindowState = WindowState.Minimized;
           
        }

        /// <summary>
        /// Evento back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/click.wav");
            flow.GoToPrevious();
            if (_elementFlow.SelectedIndex!=0)
                _elementFlow.SelectedIndex--;
            
        }

        /// <summary>
        /// Evento next
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageNext_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/click.wav");
            flow.GoToNext();
            _elementFlow.SelectedIndex++;
           
        }



        /// <summary>
        /// Comprueba la conectividad y selecciona el icono indicado
        /// </summary>
        public void checkConn()
        {
            try
            {
                if (isConnected)
                {
                    imageCon.Dispatcher.Invoke(
                     System.Windows.Threading.DispatcherPriority.Normal,
                     new Action(
                       delegate()
                       {
                           imageCon.Visibility = Visibility.Visible;
                       }
                   ));
                    //instance.imageCon.Visibility = Visibility.Visible;
                    imageNoCon.Dispatcher.Invoke(
                     System.Windows.Threading.DispatcherPriority.Normal,
                     new Action(
                       delegate()
                       {
                           imageNoCon.Visibility = Visibility.Hidden;
                       }
                   ));

                    //instance.imageNoCon.Visibility = Visibility.Hidden;
                }
                else
                {
                    imageCon.Dispatcher.Invoke(
                     System.Windows.Threading.DispatcherPriority.Normal,
                     new Action(
                       delegate()
                       {
                           imageCon.Visibility = Visibility.Hidden;
                       }
                   ));

                    imageNoCon.Dispatcher.Invoke(
                     System.Windows.Threading.DispatcherPriority.Normal,
                     new Action(
                       delegate()
                       {
                           imageNoCon.Visibility = Visibility.Visible;
                       }
                   ));
                    //instance.imageCon.Visibility = Visibility.Hidden;
                    //instance.imageNoCon.Visibility = Visibility.Visible;
                }
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// Obtiene el status de la conexion
        /// Es un hilo
        /// </summary>
        static void getStatusConn(object sender)
        {
            IndicadorWindow win = sender as IndicadorWindow;
            while (true)
            {

                try
                {
                    string sharedFolder = System.Configuration.ConfigurationManager.AppSettings["CarpetaCompartida"];
                    System.IO.Directory.EnumerateFiles(sharedFolder);
                    isConnected = true;
                    win.checkConn();

                }
                catch (Exception)
                {
                    isConnected = false;
                    win.checkConn();
                }
                try
                {
                    string delay = System.Configuration.ConfigurationManager.AppSettings["SegsTiempoComprobacionConexion"];
                    System.Threading.Thread.Sleep(Convert.ToInt32(delay) * 1000);
                }
                catch (Exception)
                {

                }
            }
        }

        /// <summary>
        /// Evento cerrar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Consultas cons = new Consultas();
            cons.UpdateUltimoIndicador(this.grupo.Id, this.flow.Index, lastfiltros);
            Util.Instance.playSound("Media/Sounds/click.wav");
            this.Close();
            Application.Current.Shutdown();
            Process.GetCurrentProcess().Kill();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageCon_SourceUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ObjectAnimationUsingKeyFrames_Changed(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BitmapImage_Changed(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DiscreteObjectKeyFrame_Changed(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        /// <summary>
        /// Evento Home
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageHome_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/click.wav");
            Consultas cons = new Consultas();
            cons.UpdateUltimoIndicador(this.grupo.Id, this.flow.Index, lastfiltros);            
            this.updateColors();
            this.Visibility = Visibility.Hidden;
            parent.Show();
            ((PrincipalWindow)parent).reinitLastInfo(this.flow.Index, lastfiltros,tbTerrirotio.Text.ToString());
        }

        /// <summary>
        /// Actualizar colores
        /// </summary>
        public void updateColorsFromList(List<Color> coloresParam)
        {
            Consultas cons = new Consultas();
            string s = "";
            foreach (Color colores in coloresParam)
            {
                s += colores.ToString() + "|";
            }
            cons.UpdateCOLORSIndicador(flow.getActual().ID, s);
            flow.getActual().LAST_FILTER = s;
        }

        /// <summary>
        /// Actualizar colores
        /// </summary>
        public void updateColors()
        {
            Consultas cons = new Consultas();
            string s = "";
           
                foreach (SeriesColor colores in chart.obtenerSeries())
                    s += colores.color.ToString() + "|";
           
            cons.UpdateCOLORSIndicador(flow.getActual().ID, s);
            flow.getActual().LAST_FILTER = s;
        }


        /// <summary>
        /// Evento de filtrado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/click.wav");
            this.draw();            
        }

        /// <summary>
        /// Evento para sacar print de la pantalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgSnapShot_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/snap.wav");
            //--------------- fix 
            //RenderTargetBitmap bmp = new RenderTargetBitmap((int)this.Width, (int)this.Height, 96, 96, PixelFormats.Pbgra32);
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)this.ActualWidth, (int)this.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            //------------------
            bmp.Render(this);
            PngBitmapEncoder png = new PngBitmapEncoder();
            png.Frames.Add(BitmapFrame.Create(bmp));            
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Imagenes|*.bmp;*.jpg";
            
            if (saveDialog.ShowDialog().Value)
            {
                using (Stream stm = File.Create(saveDialog.FileName))
                {
                    png.Save(stm);
                }
            }
            
            //RenderTargetBitmap.Render();
        }

        /// <summary>
        /// Metodo para asignacion de estudios
        /// </summary>
        /// <param name="id"></param>
        public void changeStyle(string id)
        {
            lstyle.Content = id;
            if (parent.GetType().FullName == "IMC.Wpf.CoverFlow.NMT.PrincipalWindow")
            {
                ((PrincipalWindow)parent).changeStyle(id);
            }
        }

        /// <summary>
        /// Evento configuracion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageConf_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/click.wav");
            ConfigGlassWindow conf = new ConfigGlassWindow(this);
            conf.ShowDialog();
        }

        /// <summary>
        /// Evento over
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageNoCon_MouseEnter(object sender, MouseEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/over.wav");
        }

        /// <summary>
        /// Evento over
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageConf_MouseEnter(object sender, MouseEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/over.wav");
        }

        /// <summary>
        /// Evento over
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageHome_MouseEnter(object sender, MouseEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/over.wav");
        }

        /// <summary>
        /// Evento over
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageMin_MouseEnter(object sender, MouseEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/over.wav");
        }

        /// <summary>
        /// Evento over
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageClose_MouseEnter(object sender, MouseEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/over.wav");
        }

        /// <summary>
        /// Evento over
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageNoCon_MouseEnter_1(object sender, MouseEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/over.wav");
        }

        /// <summary>
        /// Evento over
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgPaleta_MouseEnter(object sender, MouseEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/over.wav");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgPaleta_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/click.wav");
            ColorsGlassWindow col = new ColorsGlassWindow(this,flow.getActual().ID);
            col.ShowDialog();
        }

        /// <summary>
        /// Cambio de la paleta
        /// </summary>
        public void chagePaleta(List<Color> colores)
        {
            
                chart.changePalete(colores);
            
            try
            {
                updateColorsFromList(colores);
            }
            catch(Exception){
            }
        }

        /// <summary>
        /// Evento over
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgSnapShot_MouseEnter(object sender, MouseEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/over.wav");//C:/_covers/over.wav");
        }

        /// <summary>
        /// Evento over
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageBack_MouseEnter(object sender, MouseEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/over.wav");
        }

        /// <summary>
        /// Evento over
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageNext_MouseEnter(object sender, MouseEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/over.wav");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void image1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            
        
        }
        Border borderAnt = null;
        

        private void checkBox1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }
        
        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
        
            
        }

        private void checkBox1_Unchecked(object sender, RoutedEventArgs e)
        {
            
        }
        
    }
}
