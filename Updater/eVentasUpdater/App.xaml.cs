using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using System.Diagnostics;

namespace eVentasUpdater
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {

        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // Prevent default unhandled exception processing
            e.Handled = true;

            // Process unhandled exception
            //Mdb.setMsgAppLog(22, "Show win " + e.Exception.Message.ToString(), String.Format("{0:s}", DateTime.Now), 1);

            try
            {
                MessageBox.Show("¡ERROR! No se encuentran todos los archivos del actualizador: " + " | [" + e.Exception.Message.ToString() + "]. La actualización no se puede ejecutar."
                , "eVentas", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                try
                {
                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo();
                    p.StartInfo.UseShellExecute = true;
                    p.StartInfo.FileName = "taskkill.exe";
                    p.StartInfo.Arguments = "/F /IM eVentasUpdater.exe";
                    p.Start();
                    p.WaitForExit();
                }
                catch (Exception ex)
                {
                    App.Current.Shutdown(0);
                }
            }
        }
    }
}
