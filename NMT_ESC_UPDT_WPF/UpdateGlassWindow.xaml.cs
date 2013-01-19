// -------------------------------------------------------------------------------
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
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Controls;
using System.Threading;
using System.Windows.Threading;
using System.ComponentModel;
using System.Reflection;
using System.Net.NetworkInformation;

namespace FluidKit.NMT.GlassWindow
{



    /// <summary>
    /// Ventana de actualizacion de aplicación
    /// </summary>
    public partial class UpdateGlassWindow
    {

        public int totalFiles { get; set; }
        public int actualFiles { get; set; }
        public bool estado { get; set; }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parent"></param>
        public UpdateGlassWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            string carpetaInstalacion = System.Configuration.ConfigurationManager.AppSettings["CarpetaInstalacion"];
            string proceso = System.Configuration.ConfigurationManager.AppSettings["NombreExe"];
            try
            {

                AssemblyName asem = AssemblyName.GetAssemblyName(carpetaInstalacion + "\\" + proceso);
                lblVersion.Content = asem.Version.ToString();
                FileInfo fi = new FileInfo(carpetaInstalacion + "\\" + proceso);
                lblFecha.Content = fi.CreationTime;

            }
            catch (Exception)
            {
                lblVersion.Content = "";
                lblFecha.Content = "";
            }
        }

        private delegate void UpdateProgressBarDelegate(
        System.Windows.DependencyProperty dp, Object value);

        /// <summary>
        /// Hilo de actualizacion
        /// </summary>
        /// <param name="sender"></param>
        static void updateThreadProc(object sender)
        {
            UpdateGlassWindow win = sender as UpdateGlassWindow;
            win.initBar();
            win.btnActualizarVisibity(Visibility.Hidden);
            win.addText("Iniciando proceso de actualización.....\n");
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string carpetaRemotaInstalacion = System.Configuration.ConfigurationManager.AppSettings["CarpetaRemotaInstalacion"];
                string temporal = appData + "\\" + System.Configuration.ConfigurationManager.AppSettings["CarpetaLocalTemporal"];
                win.addText("Creando respaldo.....\n");
                if (NetworkInterface.GetIsNetworkAvailable())
                {
                    string carpetaInstalacion = System.Configuration.ConfigurationManager.AppSettings["CarpetaInstalacion"];
                    string carpetaRespaldo = appData + "\\" + System.Configuration.ConfigurationManager.AppSettings["CarpetaRespaldo"];
                    bool respaldo = false;
                    try
                    {
                        if (!Directory.Exists(carpetaRespaldo))
                        {
                            Directory.CreateDirectory(carpetaRespaldo);
                        }
                        win.eliminarArchivos(carpetaRespaldo);
                        win.copiarArchivos(carpetaInstalacion, carpetaRespaldo, true);
                        respaldo = true;
                    }
                    catch (Exception) { }
                    if (respaldo)
                    {
                        string[] archivosNuevos = new string[0];
                        win.addText("Obteniendo archivos para actualizar.....\n");
                        if (NetworkInterface.GetIsNetworkAvailable())
                        {
                            try
                            {
                                archivosNuevos = win.obtenerArchivos(carpetaRemotaInstalacion);
                            }
                            catch (Exception) { win.addText("Error obteniendo los archivos a actualizar\n"); }
                            if (archivosNuevos.Length != 0)
                            {
                                win.addText("Eliminando archivos temporales.....\n");
                                if (NetworkInterface.GetIsNetworkAvailable())
                                {
                                    try
                                    {
                                        if (!Directory.Exists(temporal))
                                        {
                                            Directory.CreateDirectory(temporal);
                                        }
                                        win.eliminarArchivos(temporal);
                                    }
                                    catch (Exception ex) { win.addText("Error eliminando archivos temporales\n" + ex.Message); }
                                    win.addText("Copiando archivos temporales.....\n");
                                    Thread.Sleep(5000);
                                    if (NetworkInterface.GetIsNetworkAvailable())
                                    {
                                        win.copiarArchivos(carpetaRemotaInstalacion, temporal, true);
                                        string[] archivosActualizados = win.obtenerArchivos(temporal);
                                        win.addText("Comprobando archivos descargados.....\n");
                                        if (win.comprobacionDescarga(archivosNuevos, archivosActualizados, carpetaRemotaInstalacion, temporal) && archivosNuevos.Length == archivosActualizados.Length)
                                        {
                                            win.addText("Validando proceso NMT.....\n");
                                            if (NetworkInterface.GetIsNetworkAvailable())
                                            {
                                                if (!win.validacionProcesoNMT())
                                                {
                                                    win.estado = true;
                                                    win.totalFiles = archivosActualizados.Length;
                                                    win.setMaxBar(win.totalFiles);
                                                    win.actualFiles = 0;
                                                    win.actualizarLbCuenta();

                                                    win.lblCuentaVisibity(Visibility.Visible);
                                                    win.lblAdv1Visibity(Visibility.Visible);
                                                    win.lblAdv2Visibity(Visibility.Visible);
                                                    win.btnCancelarEnabled(true);
                                                    try
                                                    {
                                                        if (win.estado)
                                                        {
                                                            win.addText("Iniciando actualización.....\n");

                                                            win.inicieCopiado(archivosActualizados, win);
                                                            if (win.estado)
                                                            {
                                                                win.addText("Proceso finalizado con éxito.....\n");
                                                                MessageBox.Show("Actialización terminada");
                                                                win.btnActualizarVisibity(Visibility.Hidden);
                                                                win.btnTerminarVisibity(Visibility.Visible);
                                                                win.btnCancelarEnabled(false);
                                                                win.eliminarArchivos(carpetaRespaldo);
                                                            }
                                                            else
                                                            {
                                                                win.restaurar();
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex2)
                                                    {
                                                        MessageBox.Show("Ocurrió un error en el proceso de copiado, inicie de nuevo el proceso de actualización. Verifique que la aplicación no se encuentre en uso - " + ex2.Message);
                                                        win.restaurar();
                                                    }

                                                }
                                                else
                                                {
                                                    MessageBox.Show("Ocurrió un error en la actualización. Verifique que la aplicación no se encuentre en uso");
                                                    win.restaurar();
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("No se encontró conexión de red disponible. Intente más tarde.");
                                                win.btnTerminarVisibity(Visibility.Visible);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Ocurrió un error en la descarga, por favor inicie de nuevo el proceso de actualización");
                                            win.restaurar();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encontró conexión de red disponible. Intente más tarde.");
                                        win.btnTerminarVisibity(Visibility.Visible);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("No se encontró conexión de red disponible. Intente más tarde.");
                                    win.btnTerminarVisibity(Visibility.Visible);
                                }
                            }
                            else
                            {
                                win.addText("No se encontraron archivos en la carpeta remota, por favor valide la existencia y los permisos de lectura en " + carpetaRemotaInstalacion + "\n");
                            }
                        }
                        else
                        {
                            MessageBox.Show("No se encontró conexión de red disponible. Intente más tarde.");
                            win.btnTerminarVisibity(Visibility.Visible);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ocurrió un error en la creación del respaldo, imposible continuar con el proceso");
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró conexión de red disponible. Intente más tarde.");
                    win.btnTerminarVisibity(Visibility.Visible);
                }
            }
            else
            {
                MessageBox.Show("No se encontró conexión de red disponible. Intente más tarde.");
                win.btnTerminarVisibity(Visibility.Visible);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void restaurar()
        {
            try
            {
                string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string carpetaInstalacion = System.Configuration.ConfigurationManager.AppSettings["CarpetaInstalacion"];
                string carpetaRespaldo = appData + "\\" + System.Configuration.ConfigurationManager.AppSettings["CarpetaRespaldo"];
                this.addText("Proceso cancelado\n");
                this.addText("Iniciando restauración\n");
                this.eliminarArchivos(carpetaInstalacion);
                this.copiarArchivos(carpetaRespaldo, carpetaInstalacion, true);
                this.addText("Restauración completa\n");
                this.btnCancelarEnabled(false);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Evento actualizacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(updateThreadProc), this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        private void addText(string text)
        {
            tBProceso.Dispatcher.Invoke(
              System.Windows.Threading.DispatcherPriority.Normal,
              new Action(
                delegate()
                {
                    tBProceso.Text += text;
                    tBProceso.ScrollToEnd();
                }
            ));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        private void btnCancelarEnabled(bool en)
        {
            btnCancelar.Dispatcher.Invoke(
              System.Windows.Threading.DispatcherPriority.Normal,
              new Action(
                delegate()
                {
                    btnCancelar.IsEnabled = en;
                }
            ));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        private void btnActualizarVisibity(Visibility vis)
        {
            btnActualizar.Dispatcher.Invoke(
              System.Windows.Threading.DispatcherPriority.Normal,
              new Action(
                delegate()
                {
                    btnActualizar.Visibility = vis;
                }
            ));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        private void btnTerminarVisibity(Visibility vis)
        {
            btnTerminar.Dispatcher.Invoke(
              System.Windows.Threading.DispatcherPriority.Normal,
              new Action(
                delegate()
                {
                    btnTerminar.Visibility = vis;
                }
            ));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        private void lblCuentaVisibity(Visibility vis)
        {
            lbCuenta.Dispatcher.Invoke(
              System.Windows.Threading.DispatcherPriority.Normal,
              new Action(
                delegate()
                {
                    lbCuenta.Visibility = vis;
                }
            ));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        private void lblAdv1Visibity(Visibility vis)
        {
            lblAdv1.Dispatcher.Invoke(
              System.Windows.Threading.DispatcherPriority.Normal,
              new Action(
                delegate()
                {
                    lblAdv1.Visibility = vis;
                }
            ));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        private void lblAdv2Visibity(Visibility vis)
        {
            lblAdv2.Dispatcher.Invoke(
              System.Windows.Threading.DispatcherPriority.Normal,
              new Action(
                delegate()
                {
                    lblAdv2.Visibility = vis;
                }
            ));
        }

        /// <summary>
        /// 
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
        /// 
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
        /// actualiza el label de cuenta
        /// </summary>
        private void actualizarLbCuenta()
        {
            lbCuenta.Dispatcher.Invoke(
              System.Windows.Threading.DispatcherPriority.Normal,
              new Action(
                delegate()
                {
                    string[] text = lbCuenta.Content.ToString().Split('-');
                    lbCuenta.Content = text[0].Trim() + " - " + actualFiles + "/" + totalFiles;
                }
            ));



        }

        /// <summary>
        /// Proceso de copiado de archivos temporales a carpeta de instalacion.
        /// </summary>
        private void inicieCopiado(string[] sourceFiles, UpdateGlassWindow win)
        {

            if (win.estado)
            {
                //Create a new instance of our ProgressBar Delegate that points
                // to the ProgressBar's SetValue method.
                UpdateProgressBarDelegate updatePbDelegate =
                    new UpdateProgressBarDelegate(pBar.SetValue);
                string carpetaInstalacion = System.Configuration.ConfigurationManager.AppSettings["CarpetaInstalacion"];
                string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string temporal = appData + "\\" + System.Configuration.ConfigurationManager.AppSettings["CarpetaLocalTemporal"] + "\\";
                foreach (string file in sourceFiles)
                {
                    //string nombreFile = file.Replace(temporal, "");
                    string nombreFile = file.Split('\\')[file.Split('\\').Length - 1];
                    /*String[] subFolds = nombreFile.Split('\\');
                    string acumSubFolder = string.Empty;

                    foreach (string subFold in subFolds)
                    {
                        acumSubFolder += subFold + "\\";

                        if (!Directory.Exists(carpetaInstalacion + "\\" + acumSubFolder.Substring(0, acumSubFolder.Length-2)))
                        {
                            Directory.CreateDirectory(carpetaInstalacion + "\\" + acumSubFolder.Substring(0, acumSubFolder.Length - 2));
                        }
                    }*/

                    this.addText("Copiando archivo: " + file + "\n");
                    try
                    {
                        System.IO.File.Copy(file, carpetaInstalacion + "\\" + nombreFile, true);
                        actualFiles++;
                    }
                    catch (Exception ex)
                    {
                        if (!file.Contains("Fluid"))
                        {
                            win.addText("Ocurrió un error en el copiado de " + file + "\n" + ex.Message);
                            throw (ex);
                        }
                        else if (file.Contains("Fluid"))
                        {
                            actualFiles++;
                            //Expected error
                        }
                    }
                    this.actualizarLbCuenta();
                    try
                    {
                        //System.Threading.Thread.Sleep(500);
                        Dispatcher.Invoke(updatePbDelegate,
                        System.Windows.Threading.DispatcherPriority.Background, new object[] { ProgressBar.ValueProperty, Convert.ToDouble(actualFiles) });
                    }
                    catch (Exception)
                    {
                        //System.Console.Write(e.Message);
                    }
                    if (!win.estado)
                        break;
                }
            }
        }

        /// <summary>
        /// Retorna si el proceso de NMT esta corriendo
        /// </summary>
        /// <returns></returns>
        public bool validacionProcesoNMT()
        {
            bool retorno = false;
            Process[] procesos;
            string proceso = System.Configuration.ConfigurationManager.AppSettings["NombreProceso"];
            procesos = Process.GetProcessesByName(proceso);
            retorno = procesos.Length != 0;
            return retorno;
        }

        /// <summary>
        /// Comprueba que se descargaron los archivos
        /// </summary>
        /// <param name="archivosNuevos"></param>
        /// <param name="archivosActualizados"></param>
        /// <returns></returns>
        public bool comprobacionDescarga(string[] archivosNuevos, string[] archivosActualizados, string rutaNueva, string rutaActualizada)
        {
            bool retorno = true;
            foreach (string temp in archivosNuevos)
            {
                retorno = retorno && containFile(temp, archivosActualizados, rutaNueva, rutaActualizada);
            }
            return retorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="archivosActualizados"></param>
        /// <param name="rutaNueva"></param>
        /// <param name="rutaActualizada"></param>
        /// <returns></returns>
        private bool containFile(string file, string[] archivosActualizados, string rutaNueva, string rutaActualizada)
        {
            bool retorno = false;
            foreach (string fileName in archivosActualizados)
            {
                if (fileName.Replace(rutaActualizada, "") == file.Replace(rutaNueva, ""))
                    return true;
            }
            return retorno;
        }

        /// <summary>
        /// Obtiene todos los archivos
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private string[] obtenerArchivos(String dir)
        {
            string[] directorios = new string[0];
            try
            {
                directorios = Directory.GetFiles(dir, "*", SearchOption.AllDirectories);
            }
            catch (Exception)
            {
                directorios = new string[0];
            }
            return directorios;
        }


        /// <summary>
        /// Elimina todos los archivos incluyendo subcarpetas
        /// </summary>
        /// <param name="dir"></param>
        private void eliminarArchivos(String dir)
        {
            string[] filePaths = Directory.GetFiles(dir, "*", SearchOption.AllDirectories);
            foreach (string filePath in filePaths)
                File.Delete(filePath);
            string[] dirPaths = Directory.GetDirectories(dir);
            foreach (string path in dirPaths)
            {
                if (path != dir)
                    Directory.Delete(path, true);
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
        /// Evento Terminar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTerminar_Click(object sender, RoutedEventArgs e)
        {

            string carpetaInstalacion = System.Configuration.ConfigurationManager.AppSettings["CarpetaInstalacion"];
            string proceso = System.Configuration.ConfigurationManager.AppSettings["NombreExe"];
            System.Diagnostics.Process.Start(carpetaInstalacion + "\\" + proceso);
            this.Close();
            Application.Current.Shutdown();
            Process.GetCurrentProcess().Kill();
        }

        /// <summary>
        /// Evento cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.estado = false;
        }

    }
}