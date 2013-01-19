using System.Windows;
using System.Windows.Input;
using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using NMT_ESC_DAL;
using NMT_ESC_BLL.Domain;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Text;
using FluidKit.Samples;
using FluidKit.NMT.GlassWindow;
using System.Media;
using IMC.Wpf.CoverFlow.NMT.Media;
using System.Threading;
using System.Net.NetworkInformation;


namespace IMC.Wpf.CoverFlow.NMT
{
    public partial class PrincipalWindow : Window
    {

        public static bool isConnected = true;
        public static int count = 0;
        private static PrincipalWindow instance;
        private GrupoIndicador grupo;
        private IndicadorWindow ind;
        ConfigGlassWindow conf;

        /// <summary>
        /// Patron de disenio solitario
        /// </summary>
        public static PrincipalWindow Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PrincipalWindow();
                }
                return instance;
            }
        }

        /// <summary>
        /// Canparador alfabetico
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
        /// Evento para uso del teclado
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

            this.asignarGrupo();
        }

        /// <summary>
        /// Asigna el grupo
        /// </summary>
        public void asignarGrupo()
        {
            this.grupo.About = this.tbAbout.Text = flow.getActual().About;
            this.grupo.Desc = this.tbDesc.Text = flow.getActual().Desc;
            this.grupo.Image = flow.getActual().Path;
            this.grupo.Id = flow.getActual().ID;
            this.grupo.LAST_INDEX = flow.getActual().LAST_INDEX;
            this.grupo.LAST_FILTER = flow.getActual().LAST_FILTER;
        }

        /// <summary>
        /// Reiniciar last info
        /// </summary>
        public void reinitLastInfo(int index, string filter,string territorio)
        {
            this.grupo.LAST_INDEX = index;
            this.grupo.LAST_FILTER = filter;
            flow.getActual().LAST_INDEX = index;
			this.tbTerrirotio.Text = territorio;
            
        }

        /// <summary>
        /// Evento para capturar el teclado
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
        /// Evento para la carga del coverflow de grupos de indicadores
        /// </summary>
        public void Load()
        {
            if (checkSMS() != 0)
            {
                #region consulta
                DataTable grupos;
                try
                {
                    flow.clear();
                    grupos = new DataTable();
                    Consultas consulta = new Consultas();
                    grupos = consulta.getNMTGroup().Tables[0];
                    consulta = null;
                    foreach (DataRow g in grupos.Rows)
                    {
                        int ID = (Int32)g["ID"];
                        string imagenPath = (String)g["RUTA"];
                        string about = (String)g["ACERCADE"];
                        string desc = (String)g["DESCRIPCION"];
                        Int32 LAST_INDEX = 0;
                        if (g["LAST_INDEX"] != System.DBNull.Value)
                            LAST_INDEX = (Int32)g["LAST_INDEX"];
                        string LAST_FILTER = "";
                        if (g["LAST_FILTER"] != System.DBNull.Value)
                            LAST_FILTER = (string)g["LAST_FILTER"];
                        flow.Dispatcher.Invoke(
                         System.Windows.Threading.DispatcherPriority.Normal,
                         new Action(
                           delegate()
                           {
                               flow.Add(Environment.MachineName, imagenPath, about, desc, ID, LAST_INDEX, LAST_FILTER);
                           }
                       ));

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectarse a la base de datos NMT. Favor de contactar a Help Desk - " + ex.Message);
                    Debug.Write(ex.Message);
                }
                #endregion
            }
            else
            {
                MessageBox.Show("No se ha configurado el usuario y/o territorio en iSMS");
                //MessageBox.Show("No existe un Usuario o Territorio configurado en iSMS");               
                this.Close();
            }
        }

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public PrincipalWindow()
        {
            try
            {
                /* ****************************************
                 * Validación de acceso a BD
                 * 22-02-2012
                 * ****************************************/
                int val = this.validacionProcesoNMT();
                
                if (!IMCEXT.AppSettings.CheckLocalDB())
                {
                    MessageBox.Show("e-Territory no ha podido iniciar debido a alguna de las siguientes causas:"
                                +" \n\t- Su usuario no cuenta con permisos en la Base de Datos de e-Territory \n\t- La Base de Datos de e-Territory ha sido eliminada.");
                    this.Close();
                    Application.Current.Shutdown();
                    Process.GetCurrentProcess().Kill();
                }
                /* ****************************************
                 * FIN Validación de acceso a BD
                 * ****************************************/

                /* ****************************************
                 * Actualización de BD
                 * 23-02-2012
                 * Habilitar sólo para versión de prueba para actualización de BD 
                 * ****************************************/
                /*try 
	            {
                    IMCEXT.AppSettings.UpdateDB();
	            }
	            catch (Exception ex)
	            {
                   
	            }*/
                /* ****************************************
                 * FIN Actualización de BD
                 * ****************************************/

                //Validar permisos
                //int res=IMCEXT.CustomParts.GetDirState(IMCEXT.AppSettings.ZipServerFolder());
                //MessageBox.Show("res: " + res.ToString(), "res");
                //if (res == 2)
                //{
                //    MessageBox.Show("El usuario no cuenta con permisos para la aplicación eTerritory");
                //    this.Close();
                //    Application.Current.Shutdown();
                //    Process.GetCurrentProcess().Kill();
                //}

                if (val == 1 || val == 0)
                {
                    //string sharedFolder = System.Configuration.ConfigurationManager.AppSettings["CarpetaCompartida"];


                    //if (IsDirectoryReadable(sharedFolder) == true)
                    //{
                        InitializeComponent();
                        lstyle.Content = "1";
                        this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        this.WindowState = WindowState.Maximized;
                        flow.parentWin = this;
                        flow.Cache = new ThumbnailManager();
                        Load();
                        init();
                        //System.Threading.Thread newThread;
                        //newThread = new System.Threading.Thread(this.getStatusConn);            
                        //newThread.Start();
                        //System.Threading.Thread.Sleep(1000);
                        //checkConn();
                        //instance = this;
                        ThreadPool.QueueUserWorkItem(new WaitCallback(getStatusConn), this);
                        initIndicador();
                        this.selectSkin();
                        //ThreadPool.QueueUserWorkItem(new WaitCallback(openThreadProc), this);
                        Debug.Write("paso");
                        this.actualizacionAutomatica();
                        conf = new ConfigGlassWindow(this);
                    //}
                    //else
                    //{
                        //MessageBox.Show("El usuario no tiene permisos sobre la carpeta compartida.");
                        //this.Close();
                        //Application.Current.Shutdown();
                        //Process.GetCurrentProcess().Kill();
                    //}
                }
                else
                {
                    MessageBox.Show("e-Territory ya se está ejecutando.");
                    this.Close();
                    Application.Current.Shutdown();
                    Process.GetCurrentProcess().Kill();
                }
            }
            catch (Exception ex1)
            {
                Debug.WriteLine(ex1);
                this.writeEventLog(ex1.Message);
            }
        }

        /// <summary>
        /// Metodo para escribir log
        /// </summary>
        /// <param name="sEvent"></param>
        private void writeEventLog(string sEvent)
        {
            string sSource;
            string sLog;


            sSource = "NMT";
            sLog = "Application";


            //if (!EventLog.SourceExists(sSource))
            //    EventLog.CreateEventSource(sSource, sLog);

            //EventLog.WriteEntry(sSource, sEvent, EventLogEntryType.Error);            
        }

        /// <summary>
        /// Reinicio de los grupos
        /// </summary>
        public void reinit()
        {
            try
            {
                flow.Cache = new ThumbnailManager();
                Load();
                init();
                initIndicador();
                if (ind != null)
                    ind.changeStyle((string)lstyle.Content);
            }
            catch (Exception e)
            {
                e.ToString();
                this.writeEventLog(e.Message);
                throw e;
            }
        }

        /// <summary>
        /// Actualizacion automatica ed aplicacion
        /// </summary>
        private void actualizacionAutomatica()
        {
            try
            {
                string versionactual = this.tbVersion.Text;
                int versionBD = 0;
                Consultas c = new Consultas();
                DataTable ver;
                ver = c.getVersionAPP().Tables[0];
                foreach (DataRow g in ver.Rows)
                {
                    if (g["UPDATE_VERSION"] != DBNull.Value)
                    {
                        versionBD = (int)g["UPDATE_VERSION"];

                    }
                }
                if (versionBD == 0)
                {
                    if (MessageBox.Show("Se detectó una nueva actualización de la aplicación, desea actualizar ahora?", "Actualización automática", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        this.actualiceAPP();
                    }

                }
                else if (obtenerVersionDeTexto(versionactual) < versionBD)
                {
                    if (MessageBox.Show("Se detectó una nueva actualización de la aplicación, desea actualizar ahora?", "Actualización automática", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        this.actualiceAPP();
                    }
                }
            }
            catch (Exception ex2)
            {
                this.writeEventLog(ex2.Message);
            }

        }

        /// <summary>
        /// MEtodo para formatear la version
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        private int obtenerVersionDeTexto(string texto)
        {
            int ret = 0;
            string val = "";
            string[] vars = texto.Split('.');
            int pos = 0;
            foreach (string v in vars)
            {
                if (pos == vars.Length - 1)
                    val += v.Substring(0, 1);
                else
                    val += v;
                pos++;
            }
            ret = Convert.ToInt32(val);
            return ret;
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
            catch (Exception ex2)
            {
                this.writeEventLog(ex2.Message);
                MessageBox.Show("Error tratando de iniciar el proceso automático de actualización de la aplicación - " + ex2.Message);

            }
        }

        /// <summary>
        /// Seleccionar Skin
        /// </summary>
        private void selectSkin()
        {
            try
            {
                DataTable skins;
                Consultas consulta = new Consultas();
                skins = consulta.SelectSkins().Tables[0];
                consulta = null;
                foreach (DataRow g in skins.Rows)
                {
                    int ID = (Int32)g["ID_SKIN"];
                    bool CURRENT = (bool)g["CURRENT_"];
                    if (CURRENT)
                        this.changeStyle(ID.ToString());
                }
            }
            catch (Exception ex2)
            {
                Debug.Write("Error tratando de obtener el skin default");
                this.writeEventLog(ex2.Message);
            }

        }

        /// <summary>
        /// Hilo para inicializar el indicador
        /// </summary>
        /// <param name="sender"></param>
        static void openThreadProc(object sender)
        {
            ((PrincipalWindow)sender).initIndicador();
        }

        /// <summary>
        /// Iniciar la ventana de indicadores
        /// </summary>
        public void initIndicador()
        {
            if (flow.getTamanio() > 0)
            {
                ind = new IndicadorWindow(grupo.Id, grupo.About, grupo.Image, grupo.Desc, this, grupo.LAST_INDEX, grupo.LAST_FILTER);
                ind.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Obteniendo la version de la tabla de bitacora
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

        /// <summary>
        /// Iniciador de los objetos de negocio
        /// </summary>
        private void init()
        {
            if (flow.getTamanio() > 0)
            {
                this.tbAbout.Visibility = Visibility.Visible;
                this.tbDesc.Visibility = Visibility.Visible;
                this.imageBack.Visibility = Visibility.Visible;
                this.imageNext.Visibility = Visibility.Visible;
                this.lbDes.Visibility = Visibility.Visible;
                this.imgDes.Visibility = Visibility.Visible;
                this.tbAbout.Text = flow.getActual().About;
                this.tbDesc.Text = flow.getActual().Desc;

                this.obtenerVersion();

                grupo = new GrupoIndicador();
                this.asignarGrupo();
            }
            else
            {
                this.tbAbout.Visibility = Visibility.Hidden;
                this.tbDesc.Visibility = Visibility.Hidden;
                this.imageBack.Visibility = Visibility.Hidden;
                this.imageNext.Visibility = Visibility.Hidden;
                this.lbDes.Visibility = Visibility.Hidden;
                this.imgDes.Visibility = Visibility.Hidden;
            }
            string strVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.tbVersion.Text = strVersion;
        }

        /// <summary>
        /// Comprueba la conexión a la basa de datos SMS
        /// </summary>
        public int checkSMS()
        {
            int estado = 0;
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
                    tbUsuario.Dispatcher.Invoke(
                     System.Windows.Threading.DispatcherPriority.Normal,
                     new Action(
                       delegate()
                       {
                           tbUsuario.Text = valores.Rows[0][0].ToString();
                       }
                   ));
                    tbTerrirotio.Dispatcher.Invoke(
                     System.Windows.Threading.DispatcherPriority.Normal,
                     new Action(
                       delegate()
                       {
                           tbTerrirotio.Text = valores.Rows[0][1].ToString();
                       }
                   ));
                    return estado = 1;
                }
            }
            catch (Exception Error)
            {
                string nombreBD = System.Configuration.ConfigurationManager.AppSettings["NombreBD"];
                if (!Error.Message.Contains(nombreBD))
                    MessageBox.Show("No se ha encontrado la base de datos de iSMS - " + Error.Message);
                //MessageBox.Show("Error tratando de validar acceso a iSMS " + Error.Message);
                else
                    return estado = 1;
                //throw (new Exception(Error.ToString()));               
            }
            return estado;
        }

        /// <summary>
        /// Evento para el boton minimizar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageMin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/click.wav");
            this.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Evento para el boton back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/click.wav");
            flow.GoToPrevious();
            this.asignarGrupo();
        }

        /// <summary>
        /// Evento para el boton next
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageNext_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/click.wav");
            flow.GoToNext();
            this.asignarGrupo();
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
            PrincipalWindow win = sender as PrincipalWindow;
            while (true)
            {

                try
                {
                    string sharedFolder = System.Configuration.ConfigurationManager.AppSettings["CarpetaCompartida"];
                    System.IO.Directory.EnumerateFiles(sharedFolder);
                    isConnected = true;
                    win.checkConn();
                    try
                    {
                        if (win.conf.Visibility == Visibility.Hidden || win.conf.Visibility == Visibility.Collapsed)
                        {
                            Thread thread = new Thread(win.sincronizacionAutoDatos);
                            thread.SetApartmentState(ApartmentState.STA); //Set the thread to STA 
                            thread.Start();
                            thread.Join();
                        }
                        try
                        {
                            win.sincronizacionBitacora(win);
                        }
                        catch (Exception)
                        {
                            //Expected error
                        }
                    }
                    catch (Exception)
                    {
                        //Expected error
                    }

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
                    //Expected error
                }
            }
        }

        /// <summary>
        /// Verificar si un usurio tiene permisos de lectura sobre un directorio
        /// </summary>
        public static bool IsDirectoryReadable(string path)
        {
            try
            {
                if (NetworkInterface.GetIsNetworkAvailable())
                {
                    System.Security.AccessControl.DirectorySecurity dirSec = System.IO.Directory.GetAccessControl(path);
                    System.Security.Principal.WindowsIdentity self = System.Security.Principal.WindowsIdentity.GetCurrent();
                    System.Security.Principal.WindowsPrincipal selfGroup = new System.Security.Principal.WindowsPrincipal(self);

                    foreach (System.Security.AccessControl.FileSystemAccessRule ar in dirSec.GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier)))
                    {
                        if (selfGroup.IsInRole((System.Security.Principal.SecurityIdentifier)ar.IdentityReference))
                        {

                            if ((ar.FileSystemRights & System.Security.AccessControl.FileSystemRights.Read) == System.Security.AccessControl.FileSystemRights.Read)
                            {
                                if (ar.AccessControlType == System.Security.AccessControl.AccessControlType.Allow)
                                {

                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    return false;
                }
                else return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Sincroniza la bitacora
        /// </summary>
        public void sincronizacionBitacora(PrincipalWindow win)
        {
            DataTable kpi = new DataTable();
            Consultas cons = new Consultas();
            kpi = cons.SelectRegBitacoraToLoad().Tables[0];
            foreach (DataRow g in kpi.Rows)
            {
                try
                {
                    string usuario = (String)g["USUARIO"];
                    string usuarioPC = (String)g["USUARIO_PC"];
                    string fechaSinc = (String)g["FECHA_SINCRONIZACION"];
                    string versionCli = (String)g["VERSION_CLIENTE"];
                    int duracion = (int)g["DURACION"];
                    string KPI = (String)g["KPI"];
                    string territorio = (String)g["TERRITORIO"];
                    long fechaSintint = (long)g["FECHA_SINC_INT"];
                    DateTime fechaSinDate = new DateTime();
                    try
                    {
                        fechaSinDate = Convert.ToDateTime(fechaSinc);
                    }
                    catch (Exception) { }
                    if (KPI!=null && KPI.Trim()!="")
                    {
                        cons.InsertVitacora(usuario, usuarioPC, fechaSinDate, versionCli, duracion, KPI, territorio, fechaSintint, false);
                    }
                    cons.UpdateBitacoraToLoad(fechaSintint);
                }
                catch (Exception) { }
            }
        }

        /// <summary>
        /// Retorna si el proceso de NMT esta corriendo
        /// </summary>
        /// <returns></returns>
        public int validacionProcesoNMT()
        {
            //bool retorno = false;
            Process[] procesos;
            string proceso = System.Configuration.ConfigurationManager.AppSettings["NombreProceso"];
            procesos = Process.GetProcessesByName(proceso);
            return procesos.Length;//retorno = 
            //return retorno;
        }

        /// <summary>
        /// Verifica si debe lanzar la actualizacion de datos y sincroniza la bitacora
        /// </summary>
        public void sincronizacionAutoDatos()
        {
            try
            {
                DataTable bitacoras = new DataTable();
                Consultas cons = new Consultas();
                DateTime hoy = DateTime.Now;
                string dateFormat = hoy.ToString("yyyyMMdd") + "0000000000";
                long fecha_sin_int = Convert.ToInt64(dateFormat);
                bool modoCreacion = false;
                try
                {
                    bitacoras = cons.SelectRegBitacora(fecha_sin_int).Tables[0];
                }
                catch (Exception e)
                {
                    e.ToString();

                    if (e.Message.Contains(System.Configuration.ConfigurationManager.AppSettings["NombreBD"]))
                    {
                        modoCreacion = true;
                    }
                }
                if (bitacoras.Rows.Count == 0 || modoCreacion)
                {
                    conf.Dispatcher.Invoke(
                     System.Windows.Threading.DispatcherPriority.Normal,
                     new Action(
                       delegate()
                       {
                           conf.Show();
                           conf.initUpdt();
                       }
                   ));

                }
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
                e.ToString();
            }
        }

        /// <summary>
        /// Evento para el boton cerrar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/click.wav");
            this.Close();
            Application.Current.Shutdown();
            Process.GetCurrentProcess().Kill();
        }


        /// <summary>
        /// Evento para seleccionar un grupo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void seleccionarGrupo()
        {
            try
            {
                this.Visibility = Visibility.Hidden;
                ind.restart(grupo.Id, grupo.About, grupo.Image, grupo.Desc, grupo.LAST_INDEX, grupo.LAST_FILTER);
                ind.Visibility = Visibility.Visible;
                ind.ShowDialog();
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Evento de configuracion
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
        /// Metodo para asignacion de estudios
        /// </summary>
        /// <param name="id"></param>
        public void changeStyle(string id)
        {
            lstyle.Content = id;
            if (ind != null)
                ind.lstyle.Content = id;

        }

        /// <summary>
        /// Evento de sonido
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageConf_MouseEnter(object sender, MouseEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/over.wav");
        }

        /// <summary>
        /// Evento de sonido 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageHome_MouseEnter(object sender, MouseEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/over.wav");
        }

        /// <summary>
        /// Evento de sonido 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageMin_MouseEnter(object sender, MouseEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/over.wav");
        }

        /// <summary>
        /// Evento de sonido
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageClose_MouseEnter(object sender, MouseEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/over.wav");
        }

        /// <summary>
        /// Evento de sonido
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageNoCon_MouseEnter(object sender, MouseEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/over.wav");
        }

        /// <summary>
        /// Evento de sonido
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageCon_MouseEnter(object sender, MouseEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/over.wav");
        }

        /// <summary>
        /// Evento de sonido
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageHome_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/click.wav");
        }

        /// <summary>
        /// Evento de sonido
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageBack_MouseEnter(object sender, MouseEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/over.wav");
        }

        /// <summary>
        /// Evento de sonido
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
    }
}
