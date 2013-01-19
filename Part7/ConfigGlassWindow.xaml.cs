﻿// -------------------------------------------------------------------------------
// 
// This file is part of the FluidKit project: http://www.codeplex.com/fluidkit
// 
// Copyright (c) 2008, The FluidKit community 
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met:
// 
// * Redistributions of source code must retain the above copyright notice, this 
// list of conditions and the following disclaimer.
// 
// * Redistributions in binary form must reproduce the above copyright notice, this 
// list of conditions and the following disclaimer in the documentation and/or 
// other materials provided with the distribution.
// 
// * Neither the name of FluidKit nor the names of its contributors may be used to 
// endorse or promote products derived from this software without specific prior 
// written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE 
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR 
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES 
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; 
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON 
// ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT 
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS 
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. 
// -------------------------------------------------------------------------------
using System.Windows;
using System.Windows.Input;
using System;
using IMC.Wpf.CoverFlow.NMT;
using NMT_ESC_BLL;
using NMT_ESC_DAL;
using System.Data;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Collections.Specialized;
using FluidKit.Controls;
using System.Diagnostics;
using System.Configuration;
using System.IO;
using System.Net.NetworkInformation;
using SevenZSharp;
using SevenZSharp.Engines;
using FolderZipper;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace FluidKit.NMT.GlassWindow
{

    
	    
    /// <summary>
	/// 	Interaction logic for Window1.xaml
	/// </summary>
	public partial class ConfigGlassWindow
	{
        public Window parentWin { get; set; }
        private StringCollection _dataSource;
        private int actualProces = 0;
        private string territorio;
        private string versionCliente;
        private string usuario;
        private LayoutBase[] _layouts = {
		                                	
		                                	new SlideDeck()		                                	
		                                };

        private Random _randomizer = new Random();
        private UpdateProgressBarDelegate updatePbDelegate;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parent"></param>
        public ConfigGlassWindow(Window parent)
		{
			InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;                     
            this.parentWin = parent;
            this.obtenerVersiones();
            Loaded += Window1_Loaded;
		}

        /// <summary>
        /// Obtiene la version de la actializacion
        /// </summary>
        private void obtenerVersiones()
        {
            Consultas consulta = new Consultas();
            territorio = "";
            DataTable version;
            if (parentWin.GetType().FullName == "IMC.Wpf.CoverFlow.NMT.PrincipalWindow")
            {
                territorio = ((PrincipalWindow)parentWin).tbTerrirotio.Text;
                usuario = ((PrincipalWindow)parentWin).tbUsuario.Text;
                versionCliente = ((PrincipalWindow)parentWin).tbVersion.Text;
            }
            else if (parentWin.GetType().FullName == "IMC.Wpf.CoverFlow.NMT.IndicadorWindow")
            {
                territorio = ((IndicadorWindow)parentWin).tbTerrirotio.Text;
                usuario = ((IndicadorWindow)parentWin).tbUsuario.Text;
                versionCliente = ((IndicadorWindow)parentWin).tbVersion.Text;
            }
            lblFecha.Content = "N/A";
            lblVersion.Content = "N/A";
            DataSet vDS =consulta.getVersionBitacora(territorio);
            if (vDS!=null)
            {
                version = vDS.Tables[0];
                foreach (DataRow g in version.Rows)
                {
                    string ver = (String)g["VERSION_CLIENTE"];
                    DateTime fecha = (DateTime)g["FECHA_SINCRONIZACION"];
                    lblFecha.Content = fecha.ToString("dd/MM/yyyy");
                    lblVersion.Content = ver;
                }
            }
            //Configurando informacion de aplicacion
            try
            {
                string strVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                string fechaAp = File.GetCreationTime(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString("dd/MM/yyyy");
                lblFechaAplicacion.Content = fechaAp;
                lblVersionAplicacion.Content = strVersion;
            }
            catch (Exception)
            {
                lblFechaAplicacion.Content = "N/A";
                lblVersionAplicacion.Content = "N/A";
            }
        }

       
        /// <summary>
        /// Evento on load  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            _elementFlow.Layout = _layouts[0];
            //_currentViewText.Text = _elementFlow.Layout.GetType().Name;

            //_selectedIndexSlider.Maximum = _elementFlow.Items.Count - 1;
            _elementFlow.SelectionChanged += EFSelectedIndexChanged;
            _elementFlow.SelectedIndex = 0;


            _dataSource = new StringCollection();
            _dataSource.Add("Media/Imagenes/skin10.jpg");
            _dataSource.Add("Media/Imagenes/skin20.jpg");
            _dataSource.Add("Media/Imagenes/skin30.jpg");
            _dataSource.Add("Media/Imagenes/skin40.jpg");
            _elementFlow.ItemsSource = _dataSource;


            if (parentWin.GetType().FullName == "IMC.Wpf.CoverFlow.NMT.PrincipalWindow")
            {
                _elementFlow.SelectedIndex = Convert.ToInt32(((PrincipalWindow)parentWin).lstyle.Content)-1;
            }
            else if (parentWin.GetType().FullName == "IMC.Wpf.CoverFlow.NMT.IndicadorWindow")
            {
                _elementFlow.SelectedIndex = Convert.ToInt32(((IndicadorWindow)parentWin).lstyle.Content)-1;
            }

        }
        /// <summary>
        /// Evento para el cambio de estilo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EFSelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine((sender as Controls.ElementFlow).SelectedIndex);
            this.changeStyle((((sender as Controls.ElementFlow).SelectedIndex)+1).ToString());
        }
        private delegate void UpdateProgressBarDelegate(
        System.Windows.DependencyProperty dp, Object value);

        /// <summary>
        /// Inicializando propiedades de bar
        /// </summary>
        /// <param name="text"></param>
        private void initBar()
        {
            pBar.Dispatcher.Invoke(
              System.Windows.Threading.DispatcherPriority.Normal,
              new Action(
                delegate()
                {
                    pBar.Minimum = 0;
                    pBar.Value = 0;
                }
            ));
        }

        /// <summary>
        /// Actualizacion (Append) de la propiedad Text
        /// </summary>
        /// <param name="text"></param>
        public void addText(string text)
        {
            tbProceso.Dispatcher.Invoke(
              System.Windows.Threading.DispatcherPriority.Normal,
              new Action(
                delegate()
                {
                    tbProceso.Text += text;
                    tbProceso.ScrollToEnd();
                }
            ));
        }

        /// <summary>
        /// Actualizacion de la propiedad Text
        /// </summary>
        /// <param name="text"></param>
        public string getText()
        {
            string ret = "";
            tbProceso.Dispatcher.Invoke(
              System.Windows.Threading.DispatcherPriority.Normal,
              new Action(
                delegate()
                {
                    ret = tbProceso.Text;                    
                }
            ));
            return ret;
        }

        /// <summary>
        /// Actualizacion de la propiedad Max
        /// </summary>
        /// <param name="text"></param>
        private void setMaxBar(int max)
        {
            pBar.Dispatcher.Invoke(
              System.Windows.Threading.DispatcherPriority.Normal,
              new Action(
                delegate()
                {
                    pBar.Maximum = max;
                }
            ));
        }
        

        /// <summary>
        /// Lanzamiento del update
        /// </summary>
        private void lanzarUpdate()
        {
            try
            {
                actualProces = 0;
                actualProces++;
                // Create a new instance of our ProgressBar Delegate that points
                // to the ProgressBar's SetValue method.
                updatePbDelegate =
                    new UpdateProgressBarDelegate(pBar.SetValue);
                Dispatcher.Invoke(updatePbDelegate,
                System.Windows.Threading.DispatcherPriority.Background, new object[] { ProgressBar.ValueProperty, Convert.ToDouble(actualProces) });
                if (this.validacionBD() )
                if( this.checkSMS() != 0)
                {
                    this.addText("Comprobando la conectividad de la red\n");
                    if (NetworkInterface.GetIsNetworkAvailable())
                   {
                        this.addText("Conectividad de la red comprobada\n");
                        int numIntentos = 1;
                        int numIntentosMax = 1;
                        int tiempoEspera = 1 ;
                        //Obteniendo num maximo de intentos
                        try
                        {
                            numIntentosMax = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["NumReintentos"]);
                        }
                        catch(Exception ex)
                        {
                            this.addText("Formato de número de intentos invalido - " + ex.Message );
                             numIntentos = 1;
                        }
                        //Obteniendo tiempo de espera entre intentos
                        try
                        {
                            tiempoEspera = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SegsTiempoReintento"]);
                        }
                        catch(Exception ex2)
                        {
                            this.addText("Formato de tiempo de espera invalido - " + ex2.Message);
                             numIntentos = 1;
                        }
                        //Intentos de conexion
                        while (numIntentos <= numIntentosMax)
                        {
                            this.addText("Comprobando la conectividad de la carpeta de datos - intento "+numIntentos+" / "+numIntentosMax+"\n");
                            if (this.getStatusConn())
                            {
                                this.addText("Conectividad de la carpeta de datos comprobada\n");
                                this.addText("Iniciando sincronización de KPIs\n");

                                //--------- INI BZG - Elimina Carpeta temporal -------------
                                EliminaArchivosTemporales();
                                //--------- FIN BZG - Elimina Carpeta temporal -------------

                                //Sincronizando
								//--------------   INI BZG ----------------
                                territorio = this.ValidaCambioTerritorio();
                                //--------------   FIN BZG ----------------
                                this.sincroniceKPIs();
                                this.sincroniceCatalogos();
                                DateTime stopTime = DateTime.Now;                                
                                double totalSeconds = (double)stopTime.Ticks / TimeSpan.TicksPerSecond;
                                string dateFormat = stopTime.ToString("yyyyMMddHHmmssffff");
                                long fecha_sin_int = Convert.ToInt64(dateFormat);
                                int duracion = 0;
                                Consultas c = new Consultas();
                                c.InsertVitacora(usuario, WindowsIdentity.GetCurrent().Name, DateTime.Now, versionCliente, duracion, null, territorio, fecha_sin_int, true);

                                if (this.getText().ToLower().Contains("error"))
                                {
                                    MessageBox.Show("El proceso de sincronización terminó con al menos un error. Favor de contactar a Help Desk.");
                                    //MessageBox.Show("El proceso de actualización terminó, pero se registró un problema, verifique el detalle de la ejecución.");
                                }
                                else
                                    MessageBox.Show("El proceso de sincronización de KPIs terminó exitosamente.");
                                    //MessageBox.Show("El proceso de actualización terminó, por favor verifique el log para constatar la ejecución del proceso");
                                string delay = System.Configuration.ConfigurationManager.AppSettings["SegsTiempoComprobacionConexion"];
                                try
                                {
                                    int delayNew = Convert.ToInt32(delay);
                                    delayNew = delayNew + 5;
                                    System.Threading.Thread.Sleep(delayNew * 1000);
                                }
                                catch (Exception)
                                {
                                }
                                this.reinitPrincipal();
                                break;
                            }
                            else
                            {
                                this.addText("No se encontró conexión con la carpeta de datos de aplicación " + System.Configuration.ConfigurationManager.AppSettings["CarpetaCompartida"] + "\n");
                                numIntentos++;
                                System.Threading.Thread.Sleep(tiempoEspera);
                            }                            
                        }
                        //Comprobando maximo de intentos
                        if (numIntentos > numIntentosMax)
                        {
                            this.addText("Los intentos de conectividad superaron el máximo de intentos "+numIntentos+"/"+numIntentosMax+"\n");
                        }
                   }
                    else
                    {
                        MessageBox.Show("No se encontró conexión de red disponible");
                    }
                }
                actualProces++;
                Dispatcher.Invoke(updatePbDelegate,
                System.Windows.Threading.DispatcherPriority.Background, new object[] { ProgressBar.ValueProperty, Convert.ToDouble(actualProces) });
                
            this.btnEjecutarEnabled(true);
            }
            catch (Exception ex3)
            {
                this.btnEjecutarEnabled(true);
                this.addText("Error en la ejecución de la actualización : " + ex3.Message);
                MessageBox.Show("Ocurrió un error en la ejecución de la actualización - " + ex3.Message);
            }
        }
        /// <summary>
        /// Proceso de sincronizacion
        /// </summary>
        private void sincroniceKPIs()
        {
           

            bool local = true;
            this.addText("*****************  INICIANDO PROCESO DE SINCRONIZACIÓN DE KPIS  *****************\n");
            string compartida = System.Configuration.ConfigurationManager.AppSettings["CarpetaCompartida"];
            this.addText("Obteniendo archivos de la carpeta remota \n");
            string[] archivosTerritorio = this.obtenerArchivos(compartida,territorio+"*.zip");
            if (archivosTerritorio.Length == 0)
                this.addText("No se detectaron archivos en la carpeta remota\n");
            setMaxBar(archivosTerritorio.Length);
            Consultas c = new Consultas();
            foreach (string file in archivosTerritorio)
            {
                try
                {
                    FileInfo fkpi = new FileInfo(file);
                    string kpi = fkpi.Name.Substring(fkpi.Name.IndexOf("_") + 1, (fkpi.Name.LastIndexOf("_") - 1 - fkpi.Name.IndexOf("_")));
                    string date = file.Substring(file.LastIndexOf("_")+1, (file.Length-5 - file.LastIndexOf("_")));
                    long dateLong = -1;
                    try
                    {
                        dateLong = Convert.ToInt64(date);
                    }
                    catch { }                    
                    this.addText("Insertando registro en NMT_TMP_FILES para el KPI "+kpi+"\n");
                    c.InsertNMT_TMP_FILES(file,kpi,territorio,dateLong);                                                         
                }
                catch (Exception ex)
                {
                    this.addText("Error insertando registro en NMT_TMP_FILES para el KPI del archivo" + file + "\n" + ex.Message);
                }
                actualProces++;
                Dispatcher.Invoke(updatePbDelegate,
                System.Windows.Threading.DispatcherPriority.Background, new object[] { ProgressBar.ValueProperty, Convert.ToDouble(actualProces) });
            }
            this.addText("Seleccionando archivos a cargar\n");
            c.CompareNMT_TMP_FILES();
            DataTable kpis = new DataTable();            
            kpis = c.SelectNMT_TMP_FILES().Tables[0];
            if (kpis.Rows.Count == 0)
                this.addText("No se detectaron archivos a cargar \n");
            foreach (DataRow g in kpis.Rows)
            {                
                string file = (string)g["FULL_FILENAME"];
                string kpi = (string)g["FILENAME_KPI"];
                long dateLong = (long)g["FILENAME_DATE"];
                this.cargaCurrentFiles(file, kpi, territorio, dateLong);
            }
            this.addText("Seleccionando archivos con bit para carga en CURRENT FILES\n");
            DataTable kpisCurrent = new DataTable();
            kpisCurrent = c.SelectNMT_CURRENT_FILES().Tables[0];
            setMaxBar(kpisCurrent.Rows.Count);
            if (kpisCurrent.Rows.Count == 0)
                this.addText("No se detectaron archivos a cargar con bit en 1\n");
            foreach (DataRow g in kpisCurrent.Rows)
            {
                string file = (string)g["FULL_FILENAME"];
                string kpi = (string)g["FILENAME_KPI"];
                long dateLong = (long)g["FILENAME_DATE"];
                DateTime startTime = DateTime.Now;                
                try
                {
                    this.addText("Realizando la extracción del .zip\n");
                    List<string> files = this.descomprimir(file);
                    this.addText("Homologando "+kpi+" \n");
                    bool exitoLectura = true;
                    try
                    {
                        string tabla = this.homologue(kpi);
                        DataTable datosTabla = new DataTable();
                        datosTabla = c.CreateTableKPITemp(tabla).Tables[0];
                        this.addText("Iniciando lectura de archivo\n");
                        StreamReader reader = null;
                        try
                        {
                            reader = new StreamReader(files[0]);
                            string line = string.Empty;
                            while ((line = reader.ReadLine()) != null)
                            {
                                try
                                {
                                    string[] valores = line.Split('|');
                                    int index = 0;
                                    string columns = "";
                                    string inserts = "";
                                    bool a = false;
                                    foreach (DataRow gdata in datosTabla.Rows)
                                    {
                                        string column = (string)gdata["COLUMN_NAME"];
                                        if (column == "Ciclo")
                                            a = true;
                                        string tipo = (string)gdata["TYPE_NAME"];
                                        columns += column + ",";
                                        string val = "";

                                        // --INI BZG ADECUACION COMILLAS 
                                        if (tipo.ToLower().Contains("char")) val += "{\"}";
                                        // --FIN BZG ADECUACION COMILLAS 
                                        if (index < valores.Length) val += valores[index];
                                        // --INI BZG ADECUACION COMILLAS 
                                        if (tipo.ToLower().Contains("char")) val += "{\"}";
                                        // --FIN BZG ADECUACION COMILLAS 
                                        if (a == true)
                                            val = val.Substring(0, 6);

                                        inserts += val + ",";
                                        index++;
                                        a = false;
                                    }
                                    try
                                    {
                                        c.ExecuteSQLNMT_TMP_FILES(tabla, columns.Remove(columns.Length - 1, 1), inserts.Remove(inserts.Length - 1, 1),local);
                                    }
                                   catch (Exception ex2)
                                    {
                                        this.addText("Error insertando los datos en la tabla temporal\n" + ex2.Message);
                                        this.addText("Registro " + inserts.Remove(inserts.Length - 1, 1) + "\n");
                                        exitoLectura = exitoLectura && false;                                       
                                    }
                                }
                                catch (Exception ex3)
                                {
                                    this.addText("Error mapeando columnas de tabla con columnas del archivo\n" + ex3.Message);
                                    exitoLectura = exitoLectura && false;
                                }
                            }
                            reader.Close();
                        }
                        catch (Exception ex4)
                        {
                            this.addText("Error en la lectura del archivo "+file+"\n" + ex4.Message);
                            if (reader != null)
                                reader.Close();
                            exitoLectura = exitoLectura && false;
                        }
                        if (exitoLectura)
                        {
                            this.addText("Tabla temporal " + tabla + "_TMP creada y cargada\n");
                            this.addText("Truncando la tabla " + tabla + "\n");
                            try
                            {
                                c.Truncate_NMT_Table(tabla, local);
                                this.addText("Tabla " + tabla + " truncada\n");
                                this.addText("Cargando los datos desde la tabla temporal " + tabla + "_TMP\n");
                                try
                                {
                                    c.Merge_NMT_Table(tabla + "_TMP", tabla, local);
                                    this.addText("Carga de datos desde la tabla temporal " + tabla + "_TMP exitosa\n");
                                    //--------- BZG
                                    c.SetLoadedNMT_CURRENT_FILES(kpi);
                                    //---------
                                    try
                                    {
                                        this.addText("Insertando a bitacora\n");
                                        DateTime stopTime = DateTime.Now;
                                        TimeSpan duration = stopTime - startTime;
                                        double totalSeconds = (double)stopTime.Ticks / TimeSpan.TicksPerSecond;
                                        string dateFormat = stopTime.ToString("yyyyMMddHHmmssffff");
                                        long fecha_sin_int = Convert.ToInt64(dateFormat);
                                        int duracion = Convert.ToInt32(duration.TotalSeconds);
                                        c.InsertVitacora(usuario, WindowsIdentity.GetCurrent().Name, DateTime.Now, versionCliente, duracion, kpi, territorio, fecha_sin_int, true);
                                    }
                                    catch (Exception ex5)
                                    {
                                        this.addText("Error en la inserción a bitacora\n" + ex5.Message);
                                    }
                                    this.addText("Eliminando tabla temporal " + tabla + "_TMP \n");
                                    try
                                    {
                                        c.Drop_NMT_Table(tabla + "_TMP", local);
                                    }
                                    catch (Exception ex6)
                                    {
                                        this.addText("Error eliminando la tabla temporal " + tabla + "_TMP\n" + ex6.Message);
                                    }
                                }
                                catch (Exception ex7)
                                {
                                    this.addText("Error al intentar cargar los datos desde la tabla temporal " + tabla + "_TMP\n" + ex7.Message);
                                }
                            }
                            catch (Exception ex8)
                            {
                                this.addText("Error al intentar truncar la tabla\n" + ex8.Message);
                            }
                        }
                        else
                        {
                            try
                            {
                                c.Drop_NMT_Table(tabla + "_TMP", local);
                            }
                            catch (Exception ex9)
                            {
                                this.addText("Error eliminando la tabla temporal " + tabla + "_TMP\n" + ex9.Message);
                            }
                        }
                    }
                    catch (Exception ex10)
                    {
                        this.addText("Ocurrió un error tratando de crear la tabla temporal\n" + ex10.Message);
                    }
                    
                }
                catch(Exception ex11)
                {
                    this.addText("Ocurrió un error en la extracción del .zip\n" + ex11.Message);
                }
                actualProces++;
                Dispatcher.Invoke(updatePbDelegate,
                System.Windows.Threading.DispatcherPriority.Background, new object[] { ProgressBar.ValueProperty, Convert.ToDouble(actualProces) });
            }            
        }

        /// <summary>
        /// Proceso de sincronizacion catalogos
        /// </summary>
        private void sincroniceCatalogos()
        {
            bool local = true;
            this.addText("*****************  INICIANDO PROCESO DE SINCRONIZACIÓN DE CATÁLOGOS  *****************\n");
            string compartida = System.Configuration.ConfigurationManager.AppSettings["CarpetaCompartidaCatalogos"];
            this.addText("Obteniendo archivos de la carpeta remota para los catálogos \n");
            string[] archivosTerritorio = this.obtenerArchivos(compartida, "*.zip");
            if(archivosTerritorio.Length==0)
                this.addText("No se detectaron archivos en la carpeta remota para los catálogos \n");
            setMaxBar(archivosTerritorio.Length);
            Consultas c = new Consultas();
            foreach (string file in archivosTerritorio)
            {
                try
                {
                    FileInfo fkpi = new FileInfo(file);
                    string catalogo = fkpi.Name.Substring(0, (fkpi.Name.IndexOf("_")));
                    string date = file.Substring(file.LastIndexOf("_") + 1, (file.Length - 5 - file.LastIndexOf("_")));
                    long dateLong = -1;
                    try
                    {
                        dateLong = Convert.ToInt64(date);
                    }
                    catch{ }
                    this.addText("Insertando registro en NMT_TMP_CAT_FILES para el catálogo " + catalogo + "\n");
                    c.InsertNMT_TMP_CAT_FILES(file, catalogo, dateLong);
                }
                catch (Exception ex)
                {
                    this.addText("Error insertando registro en NMT_TMP_CAT_FILES para el catálogo en el archivo" + file + "\n" + ex.Message);
                }
                actualProces++;
                Dispatcher.Invoke(updatePbDelegate,
                System.Windows.Threading.DispatcherPriority.Background, new object[] { ProgressBar.ValueProperty, Convert.ToDouble(actualProces) });
            }
            this.addText("Seleccionando archivos a cargar\n");
            c.CompareNMT_TMP_CAT_FILES();
            DataTable cata = new DataTable();            
            cata = c.SelectNMT_TMP_CAT_FILES().Tables[0];
            if(cata.Rows.Count==0)
                this.addText("No se detectaron archivos a cargar \n");
            foreach (DataRow g in cata.Rows)
            {
                string file = (string)g["FULL_FILENAME"];
                string catalog = (string)g["CATALOG_NAME"];
                long dateLong = (long)g["FILENAME_DATE"];
                this.cargaCurrentCatalogos(file, catalog, dateLong);
            }
            this.addText("Seleccionando archivos con bit para carga en CURRENT CAT\n");
            DataTable catCurrent = new DataTable();
            catCurrent = c.SelectNMT_CURRENT_CAT_FILES().Tables[0];
            setMaxBar(catCurrent.Rows.Count);
            if (catCurrent.Rows.Count==0)
                this.addText("No se detectaron archivos a cargar con bit en 1\n");
            foreach (DataRow g in catCurrent.Rows)
            {
                string file = (string)g["FULL_FILENAME"];
                string catalog = (string)g["CATALOG_NAME"];
                long dateLong = (long)g["FILENAME_DATE"];
                try
                {
                    this.addText("Realizando la extracción del .zip\n");
                    List<string> files = this.descomprimir(file);
                    this.addText("Homologando " + catalog + " \n");
                    try
                    {
                        string tabla = this.homologueCatalogo(catalog);
                        DataTable datosTabla = new DataTable();
                        datosTabla = c.CreateTableCatalogoTemp(tabla,local).Tables[0];
                        this.addText("Iniciando lectura de archivo\n");
                        StreamReader reader = null;
                        bool exitoLectura = true;
                        try
                        {
                            reader = new StreamReader(files[0], Encoding.Default);
                            string line = string.Empty;
                            while ((line = reader.ReadLine()) != null)
                            {
                                try
                                {
                                    string[] valores = line.Split('|');
                                    int index = 0;
                                    string columns = "";
                                    string inserts = "";
                                    foreach (DataRow gdata in datosTabla.Rows)
                                    {
                                        string column = (string)gdata["COLUMN_NAME"];
                                        string tipo = (string)gdata["TYPE_NAME"];
                                        columns += column + ",";
                                        string val = "";
                                        if (tipo.ToLower().Contains("char")) val += "{\"}";
                                        if (index < valores.Length) val += valores[index];
                                        if (tipo.ToLower().Contains("char")) val += "{\"}";
                                        inserts += val + ",";
                                        index++;
                                    }
                                    try
                                    {
                                        c.ExecuteSQLNMT_CAT_TMP_FILES(tabla, columns.Remove(columns.Length - 1, 1), inserts.Remove(inserts.Length - 1, 1), local);
                                    }
                                    catch (Exception ex2)
                                    {
                                        exitoLectura = exitoLectura && false;
                                        this.addText("Error insertando los datos en la tabla temporal\n" + ex2.Message);
                                    }
                                }
                                catch (Exception ex3)
                                {
                                    exitoLectura = exitoLectura && false;
                                    this.addText("Error mapeando columnas de tabla con columnas del archivo\n" + ex3.Message);
                                }
                            }
                            reader.Close();
                        }
                        catch (Exception ex4)
                        {
                            this.addText("Error en la lectura del archivo\n" + ex4.Message);
                            if (reader != null)
                                reader.Close();
                            exitoLectura = exitoLectura && false;
                        }
                        if (exitoLectura)
                        {
                            this.addText("Tabla temporal " + tabla + "_TMP creada y cargada\n");
                            this.addText("Truncando la tabla " + tabla + "\n");
                            try
                            {
                                //AQUI TRUNCA LA TABLA DE KPI NORMAL
                                c.Truncate_NMT_Table(tabla, local);
                                //c.Drop_NMT_Table(tabla, local);
                                this.addText("Tabla " + tabla + " truncada\n");
                                this.addText("Cargando los datos desde la tabla temporal " + tabla + "_TMP\n");
                                try
                                {
                                    c.Merge_NMT_Table(tabla + "_TMP", tabla, local);
                                    this.addText("Carga de datos desde la tabla temporal " + tabla + "_TMP exitosa\n");
									// ----- INI - BZG - 20120227 -----------
                                    c.SetLoadedNMT_CURRENT_CAT_FILES(catalog);
                                    // ----- FIN - BZG - 20120227 -----------
                                    this.addText("Eliminando tabla temporal " + tabla + "_TMP \n");
                                    try
                                    {
                                        c.Drop_NMT_Table(tabla + "_TMP", local);
                                    }
                                    catch (Exception ex5)
                                    {
                                        this.addText("Error eliminando la tabla temporal " + tabla + "_TMP\n"+ ex5.Message);
                                    }
                                }
                                catch (Exception ex6)
                                {
                                    this.addText("Error al intentar cargar los datos desde la tabla temporal " + tabla + "_TMP\n"+ex6.Message);
                                }
                            }
                            catch (Exception ex7)
                            {
                                this.addText("Error al intentar truncar la tabla\n" + ex7.Message);
                            }
                        }
                    }
                    catch (Exception ex8)
                    {
                        this.addText("Ocurrió un error tratando de crear la tabla temporal\n" + ex8.Message);
                    }

                }
                catch (Exception ex9)
                {
                    this.addText("Ocurrió un error en la extracción del .zip\n" + ex9.Message);
                }

                this.addText("\nEl proceso ha finalizado.\n");
                actualProces++;
                Dispatcher.Invoke(updatePbDelegate,
                System.Windows.Threading.DispatcherPriority.Background, new object[] { ProgressBar.ValueProperty, Convert.ToDouble(actualProces) });
            }
        }

        /// <summary>
        /// Homologa el nombre del archivo entregado por su correspondiente tabla
        /// </summary>
        /// <param name="nombreTabla"></param>
        /// <returns></returns>
        private string homologue(string nombreTabla)
        {
            Consultas c = new Consultas();            
            string retorno = "";
            DataTable kpisCurrent = new DataTable();
            kpisCurrent = c.SelectHomologacion_FILES(nombreTabla).Tables[0];
            foreach (DataRow g in kpisCurrent.Rows)
            {
                retorno = (string)g["strNombreTabla"];
            }
            return retorno;
        }

        /// <summary>
        /// Homologa el nombre del catalogo entregado por su correspondiente tabla
        /// </summary>
        /// <param name="catalogo"></param>
        /// <returns></returns>
        private string homologueCatalogo(string catalogo)
        {
            Consultas c = new Consultas();
            string retorno = "";
            DataTable catCurrent = new DataTable();
            catCurrent = c.SelectHomologacion_Catalogo(catalogo).Tables[0];
            foreach (DataRow g in catCurrent.Rows)
            {
                retorno = (string)g["strNombreTabla"];
            }
            return retorno;
        }

        /// <summary>
        /// Descomprime el archivo en la misma ubicacion
        /// </summary>
        /// <param name="file"></param>
        private List<string> descomprimir(string file)
        {            
            FileInfo finf = new FileInfo(file);
            return  ZipUtil.UnZipFiles(file, finf.DirectoryName,null,false);
            //CompressionEngine.SetOptions(@"Some other location");
            //CompressionEngine.Current.Decoder.DecodeIntoDirectory
        }

        /// <summary>
        /// Carga la informacion en la tabla NMT_CURRENT_CAT_FILES
        /// </summary>
        /// <param name="file"></param>
        /// <param name="catalogo"></param>
        /// <param name="dateLong"></param>
        private void cargaCurrentCatalogos(string file, string catalogo, long dateLong)
        {
            FileInfo finf = new FileInfo(file);
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string tmpFile = appData + System.Configuration.ConfigurationManager.AppSettings["CarpetaTemporal"] + "\\" + finf.Name;
            if (this.copiarArchivo(file, tmpFile))
            {
                if (comprobacionDescargaArchivo(file, tmpFile))
                {
                    Consultas c = new Consultas();
                    c.InsertNMT_CURRENT_CAT_FILES(tmpFile, catalogo, dateLong);
                    c.DeleteNMT_TMP_CAT_FILES(file);
                }
            }
        }

        /// <summary>
        /// Carga la informacion en la tabla NMT_CURRENT_FILES
        /// </summary>
        /// <param name="file"></param>
        /// <param name="kpi"></param>
        /// <param name="territorio"></param>
        /// <param name="dateLong"></param>
        private void cargaCurrentFiles(string file,string kpi, string territorio, long dateLong)
        {
            FileInfo finf = new FileInfo(file);
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string tmpFile = appData  + System.Configuration.ConfigurationManager.AppSettings["CarpetaTemporal"] + "\\"+finf.Name;
            if (this.copiarArchivo(file, tmpFile))
            {                
                if (comprobacionDescargaArchivo(file,tmpFile))
                {
                    Consultas c = new Consultas();
                    c.InsertNMT_CURRENT_FILES(tmpFile, kpi, territorio, dateLong);
                    c.DeleteNMT_TMP_FILES(file);
                }
            }
        }

        /// <summary>
        /// Comprueba existencia y tamaño
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private bool comprobacionDescargaArchivo(string source, string target)
        {

            if (File.Exists(target))
            {
                FileInfo finfSource = new FileInfo(source);
                FileInfo finfTarget = new FileInfo(target);
                if (finfSource.Length == finfTarget.Length)
                    return true;
                else 
                {
                    this.addText("Se registró una diferencia mientras se comprobaba la descarga entre "+source+" y "+target+"\n");
                    return false;
                }

            }
            return false;


        }

        /// <summary>
        /// Obtiene todos los archivos
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private string[] obtenerArchivos(String dir, string filtro)
        {
            string[] directorios = new string[0];
            try
            {
                directorios = Directory.GetFiles(dir, filtro, SearchOption.AllDirectories);
            }
            catch (Exception)
            {
                directorios = new string[0];
            }
            return directorios;
        }

        //Elimina todos los archivos incluyendo subcarpetas
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dir"></param>
        private void eliminarArchivos(String dir, string filtro)
        {
            string[] filePaths = Directory.GetFiles(dir, filtro, SearchOption.AllDirectories);
            foreach (string filePath in filePaths)
                File.Delete(filePath);
            string[] dirPaths = Directory.GetDirectories(dir);
            foreach (string path in dirPaths)
            {
                if (path != dir)
                    Directory.Delete(path);
            }

        }

        /// <summary>
        /// Copia los archivos de un directorio fuente a un directorio destino
        /// </summary>
        /// <param name="sourceDirName"></param>
        /// <param name="destDirName"></param>
        private bool copiarArchivo(string sourceDirName, string destDirName)
        {
            try
            {
                this.addText("Copiando archivo " + sourceDirName + " a carpeta temporal " + destDirName + "\n");
                FileInfo finf = new FileInfo(destDirName);
                if (!Directory.Exists(finf.DirectoryName))
                    Directory.CreateDirectory(finf.DirectoryName);
                File.Copy(sourceDirName, destDirName,true);
                return true;
            }
            catch(Exception ex) 
            {
                this.addText("Error intentando copiar archivo "+sourceDirName+" a carpeta temporal " + destDirName + "\n" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Copia de archivos
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destinationFile"></param>
        private void copiarArchivos(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the source directory does not exist, throw an exception.
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory does not exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }


            // Get the file contents of the directory to copy.
            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files)
            {
                // Create the path to the new copy of the file.
                string temppath = Path.Combine(destDirName, file.Name);

                // Copy the file.
                file.CopyTo(temppath, false);
            }

            // If copySubDirs is true, copy the subdirectories.
            if (copySubDirs)
            {

                foreach (DirectoryInfo subdir in dirs)
                {
                    // Create the subdirectory.
                    string temppath = Path.Combine(destDirName, subdir.Name);

                    // Copy the subdirectories.
                    copiarArchivos(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        /// <summary>
        /// Validar la conexion
        /// </summary>
        private bool validacionBD()
        {
            bool succes = true;
            try
            {
                this.addText("Comprobando la conectividad con la base de datos\n");
                Consultas consulta = new Consultas();
                consulta.getNMTGroup();
            }
            catch (Exception ex)
            {
                //Nombre de la BD
                string vg_BDConexion = ConfigurationManager.AppSettings["NombreBD"];
                if (ex.Message.Contains("error: 26"))
                {
                    this.addText("Error relacionado con la red o específico de la instancia mientras se establecía una conexión con el servidor SQL Server. No se encontró el servidor o éste no estaba accesible. Compruebe que el nombre de la instancia es correcto y que SQL Server está configurado para admitir conexiones remotas.\n");
                    MessageBox.Show("Error relacionado con la red o específico de la instancia mientras se establecía una conexión con el servidor SQL Server. No se encontró el servidor o éste no estaba accesible. Compruebe que el nombre de la instancia es correcto y que SQL Server está configurado para admitir conexiones remotas - " + ex.Message);
                    Debug.Write(ex.Message);
                    succes = false;
                }
                else if (ex.Message.Contains(vg_BDConexion))
                {
                    try
                    {
                        this.addText("No se detectó la base de datos, procediendo a la creación.\n");
                        ConexionBD cb = new ConexionBD();
                        FileInfo file = new FileInfo("Script\\Script.sql");
                        string script = file.OpenText().ReadToEnd();
                        cb.ejecutarScript("CREATE DATABASE " + vg_BDConexion + "; ");
                        cb.ejecutarScript(script.Replace("[NMTBASE]", vg_BDConexion));
                        System.Threading.Thread.Sleep(20000);
                        file = new FileInfo("Script\\ScriptSP.sql");
                        script = file.OpenText().ReadToEnd();
                        string[] creates = script.Split('|');
                        foreach (string sql in creates)
                        {
                            try
                            {
                                cb.ejecutarScriptBase(sql);
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show("Error tratando de crear el stored procedure " + sql + " - " + e.Message);
                                Debug.Write(e.Message);
                            }
                        }                        
                        this.addText("Creación de base de datos completa.\n");
                        string delay = System.Configuration.ConfigurationManager.AppSettings["SegsTiempoComprobacionConexion"];
                        try
                        {
                            int delayNew = Convert.ToInt32(delay);
                            delayNew = delayNew + 5;
                            System.Threading.Thread.Sleep(delayNew * 1000);
                        }
                        catch (Exception)
                        {
                        }
                        this.reinitPrincipal();
                        if (parentWin.GetType().FullName == "IMC.Wpf.CoverFlow.NMT.PrincipalWindow")
                        {
                            ((PrincipalWindow)parentWin).Dispatcher.Invoke(
                             System.Windows.Threading.DispatcherPriority.Normal,
                             new Action(
                               delegate()
                               {
                                   territorio = ((PrincipalWindow)parentWin).tbTerrirotio.Text;
                               }
                           ));
                            
                            
                        }
                        else if (parentWin.GetType().FullName == "IMC.Wpf.CoverFlow.NMT.IndicadorWindow")
                        {
                            ((IndicadorWindow)parentWin).Dispatcher.Invoke(
                              System.Windows.Threading.DispatcherPriority.Normal,
                              new Action(
                                delegate()
                                {
                                    territorio = ((IndicadorWindow)parentWin).tbTerrirotio.Text;
                                }
                            ));
                           
                        }
                    }
                    catch (FileNotFoundException ex1)
                    {
                        MessageBox.Show("No se encontró el script " + ex1.Message);
                        Debug.Write(ex1.Message);
                        succes = false;
                    }
                    catch (Exception ex2)
                    {
                        MessageBox.Show("Error relacionado con la red o específico de la instancia mientras se establecía una conexión con el servidor SQL Server. No se encontró el servidor o éste no estaba accesible. Compruebe que el nombre de la instancia es correcto y que SQL Server está configurado para admitir conexiones remotas - " + ex2.Message);
                        Debug.Write(ex2.Message);
                        succes = false;
                    }
                }
            }
            return succes;
        }

        /// <summary>
        /// Reinicia la informacion de la ventana desde donde fue invocada
        /// </summary>
        private void reinitPrincipal()
        {
            if (parentWin.GetType().FullName == "IMC.Wpf.CoverFlow.NMT.PrincipalWindow")
            {
                if (((PrincipalWindow)parentWin)!=null)
                ((PrincipalWindow)parentWin).Dispatcher.Invoke(
                             System.Windows.Threading.DispatcherPriority.Normal,
                             new Action(
                               delegate()
                               {
                                   ((PrincipalWindow)parentWin).reinit();
                               }
                           ));
                
            }
            else if (parentWin.GetType().FullName == "IMC.Wpf.CoverFlow.NMT.IndicadorWindow")
            {
                try
                {
                    if (((PrincipalWindow)((IndicadorWindow)parentWin).Parent)!=null)
                    ((PrincipalWindow)((IndicadorWindow)parentWin).Parent).Dispatcher.Invoke(
                                System.Windows.Threading.DispatcherPriority.Normal,
                                new Action(
                                  delegate()
                                  {
                                      ((PrincipalWindow)((IndicadorWindow)parentWin).Parent).reinit();
                                  }
                              ));
                }
                catch(Exception e)
                {
                    e.ToString();
                    throw e;
                }
            }
        }


        /// <summary>
        /// cheuqea el status de la conexion
        /// </summary>
        public bool getStatusConn()
        {
            bool isConnected = false;          
            try
            {
                string sharedFolder = System.Configuration.ConfigurationManager.AppSettings["CarpetaCompartida"];
                System.IO.Directory.EnumerateFiles(sharedFolder);
                isConnected = true;
            }
            catch (Exception)
            {
                isConnected = false;
            }            
            return isConnected;
        }

        /// <summary>
        /// Comprueba la conexión a la basa de datos SMS
        /// </summary>
        private int checkSMS()
        {
            string nombreBD = System.Configuration.ConfigurationManager.AppSettings["NombreBDiSMS"];
            int estado;
            DataTable valores;
            try
            {
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
                    return estado = 1;
                }

            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }


        /// <summary>
        /// Cambia la propiedad enables
        /// </summary>
        /// <param name="text"></param>
        private void btnEjecutarEnabled(bool en)
        {
            btActData.Dispatcher.Invoke(
              System.Windows.Threading.DispatcherPriority.Normal,
              new Action(
                delegate()
                {
                    btActData.IsEnabled = en;
                }
            ));
        }

        /// <summary>
        /// Evento sincronizar Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btActData_Click(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(updateThreadProc), this);
        }

        /// <summary>
        /// Inciia el proceso desde un ente externo
        /// </summary>
        public void initUpdt()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(updateThreadProc), this);
        }

        /// <summary>
        /// Metodo para asignacion de estudios
        /// </summary>
        /// <param name="id"></param>
        public void changeStyle(string id)
        {
            Consultas cons = new Consultas();
            cons.UpdateSkins(Convert.ToInt32(id));
            if (parentWin.GetType().FullName == "IMC.Wpf.CoverFlow.NMT.PrincipalWindow")
            {
                ((PrincipalWindow)parentWin).changeStyle(id);
            }
            else if (parentWin.GetType().FullName == "IMC.Wpf.CoverFlow.NMT.IndicadorWindow")
            {
                ((IndicadorWindow)parentWin).changeStyle(id);
            }
        }

        /// <summary>
        /// Metodo para actualizar la aplicación manualmente.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btActAPP_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea actualizar la aplicación en este momento?", "Actualización automática", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.actualiceAPP();
            }
        }

        /// <summary>
        /// Hilo para la actualizacion.
        /// </summary>
        /// <param name="sender"></param>
        static void updateThreadProc(object sender)
        {
            ConfigGlassWindow win = sender as ConfigGlassWindow;
            win.btnEjecutarEnabled(false);
            win.initBar();
            win.setMaxBar(2);
            win.lanzarUpdate();

        }

        /// <summary>
        /// Inicia la ejecucion del ejecutable de actualizacion de la aplicacion
        /// </summary>
        private void actualiceAPP()
        {
            try
            {
                string instalacion = System.Configuration.ConfigurationManager.AppSettings["CarpetaInstalacion"];
                string proceso = System.Configuration.ConfigurationManager.AppSettings["NombreProcesoActualizacion"];
                if (!instalacion.EndsWith("\\"))
                    instalacion += "\\";
                System.Diagnostics.Process.Start(instalacion + proceso);
                this.Close();
                Application.Current.Shutdown();
                Process.GetCurrentProcess().Kill();
            }
            catch (Exception ex)
            {                
                MessageBox.Show("Error tratando de iniciar el proceso automático de actualización de la aplicación - " + ex.Message);
            }
            
        }

        
        /// <summary>
        /// Autor : Belizario Zapien Garcia
        /// Funcion que elimina los archivos de la carpeta temporal
        /// </summary>
        private void EliminaArchivosTemporales()
        {
            Consultas c = new Consultas();
            DataTable kpis = new DataTable();
            
            kpis = c.SelectNMT_KPI_Temporal().Tables[0];
            //if (kpis.Rows.Count == 0)
            //    this.addText("No se detectaron archivos a borrar \n");
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string tmpFile = appData + System.Configuration.ConfigurationManager.AppSettings["CarpetaTemporal"];


            foreach (DataRow g in kpis.Rows)
            {
                try
                {
                    string kpi = (string)g["FILENAME_KPI"];
                    this.eliminarArchivos(tmpFile, "*" + kpi + "*");
                }
                catch { }
            }

        }//////////////////////////////////////////////////////////////////
		
		private string ValidaCambioTerritorio()
        {
            Consultas cons = new Consultas();
            //cons.DeleteNMT_CURRENT_FILES(territorio);
            string nombreBD = System.Configuration.ConfigurationManager.AppSettings["NombreBDiSMS"];
            string territorio = cons.DeleteNMT_CURRENT_FILES().Tables[0].Rows[0][0].ToString();
            //Establece etiquetas de territorio
            if (parentWin.GetType().FullName == "IMC.Wpf.CoverFlow.NMT.PrincipalWindow")
            {
                ((PrincipalWindow)parentWin).Dispatcher.Invoke(
                    System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(
                      delegate()
                      {
                          ((PrincipalWindow)parentWin).tbTerrirotio.Text = territorio;
                      }
                  ));
            }

            if (parentWin.GetType().FullName == "IMC.Wpf.CoverFlow.NMT.IndicadorWindow")
            {
                ((IndicadorWindow)parentWin).Dispatcher.Invoke(
                    System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(
                      delegate()
                      {
                          ((IndicadorWindow)parentWin).tbTerrirotio.Text = territorio;
                      }
                  ));
            }

            return territorio;
        } 
           
	}
}