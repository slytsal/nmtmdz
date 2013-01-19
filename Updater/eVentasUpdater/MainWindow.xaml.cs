using System;
using System.Windows;
using System.Diagnostics;
using System.Threading;

namespace eVentasUpdater
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public string realUserName = "";
        //public string appDataFolder = "";
        public string tmpFolder = "";
        public bool upFail = false;

        public MainWindow()
        {
            InitializeComponent();
        }//////////////////////////////////////////////////////////

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string appVer = this.getAppCurrentVersion();
            lblTitle2.Content = lblTitle2.Content + " v" + appVer;
            //MessageBox.Show(this.permissonServerDir);
            //this.fixProcess();
            this.updateProcess();
        }//////////////////////////////////////////////////////////

        public void setFileCount(int total, int current)
        {
            //this.lblFiles.Content = "Copiando archivo " + current.ToString() + "/" + total.ToString();
        }//////////////////////////////////////////////////////////

        public void addStatusTextBoxLine(string message)
        {
            message += "\n";
            this.txtStatus.Text += message;
            //this.txtStatus.CaretIndex = this.txtStatus.Text.Length;
            this.txtStatus.Focus();
            this.txtStatus.Select(this.txtStatus.Text.Length, 0);
        }//////////////////////////////////////////////////////////

        public void setProgressBarMinMax(int min, int max)
        {
            //this.progressBar1.Minimum = min;
            //this.progressBar1.Maximum = max;
        }//////////////////////////////////////////////////////////

        public void addProgressValue()
        {
            //this.progressBar1.Value = this.progressBar1.Value + 1.0;
        }//////////////////////////////////////////////////////////

        public void enableButton()
        {
            this.btnTerminar.IsEnabled = true;
        }//////////////////////////////////////////////////////////

        public void disableCancelButton()
        {
            this.btnCancelar.IsEnabled = false;
        }//////////////////////////////////////////////////////////

        public void updateProcess()
        {
            //string _serverUpdateFilesFolder = AppConfs.updateFilesFolder;   //ConfigurationManager.AppSettings["UpdateFilesFolder"].ToString();
            //string _finalFolder = AppConfs.eVentasAppFolder;    // ConfigurationManager.AppSettings["AppFolder"].ToString();
            //string appDataFolder = AppConfs.eVentasAppFolder;    // ConfigurationManager.AppSettings["AppFolder"].ToString();
            //string _tmpFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\eVentas\appupp\";


            /*
             * Se mata al proceso eVentasWPF.exe
             */
            try
            {
                this.addStatusTextBoxLine("-Verificando estado el proceso de e-Territory...");
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo();
                p.StartInfo.UseShellExecute = true;
                p.StartInfo.FileName = "taskkill.exe";
                p.StartInfo.Arguments = "/F /IM " + AppConfs.ProcessName;
                p.Start();
                p.WaitForExit();
                this.addStatusTextBoxLine("\tVerificado completo.");
            }
            catch (Exception ex)
            {
                this.addStatusTextBoxLine("\tVerificado completo. " + ex.Message);
            }

            new Thread(delegate()
            {
                MainWindow mW = this;
                string[] arr;
                //string serverUpdateFilesFolder = _serverUpdateFilesFolder;
                //string finalFolder = _finalFolder;
                //string tmpFolder = _tmpFolder;

                try
                {
                    bool errorProcess = false;
                    //UI update
                    mW.Dispatcher.BeginInvoke(new Action(() => mW.addStatusTextBoxLine("- Inicia proceso de actualización...")));
                    mW.Dispatcher.BeginInvoke(new Action(() => mW.addStatusTextBoxLine("\tPor favor espere... ")));

                    UpdateProc up = new UpdateProc(mW);
                    /*
                     * Se inicia el proceso de actualización
                     */
                    string fin = "";
                    try
                    {

                        up.updateProcStart();
                        //fin = "- El proceso de actualización ha finalizado. ";

                    }
                    catch (Exception ex)
                    {
                        mW.Dispatcher.BeginInvoke(new Action(() => mW.addStatusTextBoxLine("***\nNO SE HA PODIDO ACTUALIZAR\nValide:"
                            + "\n1. Conexión estable a la red de PMI"
                            + "\n2. Permisos de su usuario en la carpeta compartida \ny vuelva a intentar.")));
                        upFail = true;
                        return;
                    }

                    mW.Dispatcher.BeginInvoke(new Action(() => mW.addProgressValue()));
                    mW.Dispatcher.BeginInvoke(new Action(() => mW.addStatusTextBoxLine("\n" + fin + "- Haga clic en Terminar para salir de éste actualizador...")));
                    mW.Dispatcher.BeginInvoke(new Action(() => mW.disableCancelButton()));
                    mW.Dispatcher.BeginInvoke(new Action(() => mW.enableButton()));

                }
                catch (Exception ex)
                {
                    mW.Dispatcher.BeginInvoke(new Action(() => mW.addStatusTextBoxLine("*** La actualizacion no ha podido ser concluída. " + ex.Message.ToString())));
                    mW.Dispatcher.BeginInvoke(new Action(() => mW.disableCancelButton()));
                    mW.Dispatcher.BeginInvoke(new Action(() => mW.enableButton()));
                    upFail = true;
                    return;
                }
            }).Start();

        }//////////////////////////////////////////////////////////

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (!upFail)
            {
                MessageBoxResult res = System.Windows.MessageBox.Show(this, "¿Desea interrumpir el proceso de actualización?", "e-Territory Updater", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No, MessageBoxOptions.None);
                if (res == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                }
            }
            else Application.Current.Shutdown();

        }//////////////////////////////////////////////////////////

        private void btnTerminar_Click(object sender, RoutedEventArgs e)
        {
            string ExeName = AppConfs.ProcessName;

            Process p = new Process();
            try
            {
                p.StartInfo = new ProcessStartInfo(System.IO.Directory.GetCurrentDirectory() + "\\" + ExeName);
                p.Start();

            }
            catch (Exception ex)
            {
                this.addStatusTextBoxLine("*** No se pudo iniciar el proceso de e-Territory..." + ex.Message);
            }
            Application.Current.Shutdown();
        }//////////////////////////////////////////////////////////

        public string getAppCurrentVersion()
        {
            string cur = "";
            try
            {
                cur = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener versión del assembly.");
            }

            return cur;
        }//////////////////////////////////////////////////////////////////////////////////
    }
}
