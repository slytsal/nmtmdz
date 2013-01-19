using System;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace eVentasUpdater
{
    class UpdateProc
    {
        private MainWindow mw = null;

        public UpdateProc(MainWindow mw)
        {
            this.mw = mw;
        }

        public void addStatusTextBoxLine(string msg)
        {
            mw.Dispatcher.BeginInvoke(new Action(() => mw.addStatusTextBoxLine(msg)));
        }

        public void setProgressBarMinMax(int min, int max)
        {
            //mw.Dispatcher.BeginInvoke(new Action(() => mw.setProgressBarMinMax(min,max)));
        }//////////////////////////////////////////////////////////

        public void addProgressValue()
        {
            mw.Dispatcher.BeginInvoke(new Action(() => mw.addProgressValue()));
        }//////////////////////////////////////////////////////////

        public void setFileCount(int total, int current)
        {
            mw.Dispatcher.BeginInvoke(new Action(() => mw.setFileCount(total, current)));
        }//////////////////////////////////////////////////////////

        public void downloadServerFiles()
        {
            string[] arr;

            try
            {
                arr = this.getServerFileLists();
                this.createUserFolder();

                if (arr.Length > 0)
                {
                    //this.setProgressBarMinMax(1, arr.Length+2);

                    int totalArchivosServer = arr.Length;

                    for (int i = 0; i < arr.Length; i++)
                    {
                        string tmpStr = "Copiando archivo " + arr[i].Split('\\').Last();
                        int currentI = i + 1;
                        this.addStatusTextBoxLine(tmpStr);

                        System.IO.File.Copy(arr[i], AppConfs.userUpdateFolder + arr[i].Split('\\').Last(), true);
                        this.setFileCount(totalArchivosServer, currentI);
                        this.addStatusTextBoxLine("Archivo copiado a la carpeta temporal");
                        this.addProgressValue();
                    }
                }
                else
                {
                    this.addStatusTextBoxLine("No existen archivos por actualizar");
                    return;
                }
            }
            catch (Exception ex)
            {
                this.addStatusTextBoxLine("No se pudieron descargr los archivos. Compruebe que cuenta con conexión y vuelva a intentar mas tarde");
            }
        }/////////////////////////////////////////////////////

        public string[] getServerFileLists()
        {
            string[] arr;
            try
            {
                arr = System.IO.Directory.GetFiles(AppConfs.updateFilesFolder);
            }
            catch (Exception ex)
            {
                this.addStatusTextBoxLine("No pudo conectar a la carpeta " + AppConfs.updateFilesFolder + ". Valide que tenga permisos para eVentas en el IMDL.");
                throw ex;
            }
            return arr;
        }///////////////////////////////////////////////////////

        public string[] getUserFolderFileList()
        {
            string[] arr;
            try
            {
                arr = System.IO.Directory.GetFiles(AppConfs.userUpdateFolder);
            }
            catch (Exception ex)
            {
                this.addStatusTextBoxLine("No se encontró la carpeta de usuario " + AppConfs.userUpdateFolder + ".");
                throw ex;
            }
            return arr;
        }///////////////////////////////////////////////////////

        public void createUserFolder()
        {
            try
            {
                if (!System.IO.Directory.Exists(AppConfs.userUpdateFolder))
                {
                    System.IO.Directory.CreateDirectory(AppConfs.userUpdateFolder);
                }
            }
            catch (Exception ex)
            {
                this.addStatusTextBoxLine("No pudo crear la carpeta de usuario; valide que esté logueado con el usuario que hace uso de la aplicación. " + ex.Message);
                throw ex;
            }
        }///////////////////////////////////////////////////////

        public void copyToAppFolder()
        {
            string[] arr;

            string[] tmpArr;

            // 5.1 Copia archivos de C:\tmpNMT_\ a C:\Programs\eVentas\ 
            /*try
            {
                arr = this.getTmpFolderFilesList();

                if (arr.Length > 0)
                {
                    //int totalArchivosServer = arr.Length;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        string tmpStr = "- Copiando archivo " + arr[i].Split('\\').Last() + " a " + AppConfs.AppFolder;
                        this.addStatusTextBoxLine(tmpStr);
                        System.IO.File.Copy(arr[i], AppConfs.AppFolder + arr[i].Split('\\').Last(), true);
                        this.addStatusTextBoxLine("\tArchivo copiado.");
                    }
                }
                else
                {
                    this.addStatusTextBoxLine("No existen archivos por copiar a la carpeta de la aplicación.");
                    return;
                }
            }
            catch (Exception ex)
            {
                this.addStatusTextBoxLine("No se pudieron copiar los archivos a la ubicación final : " + ex.Message);
                throw ex;
            }

            // 5.2 Copia la carpeta Media a C:\Programs\NMT\
            try
            {
                copyImgFolder();
            }
            catch (Exception ex)
            {
                this.addStatusTextBoxLine("No se pudo copiar la carpeta Media a la ubicación final : " + ex.Message);
                throw ex;
            }
            */
            if (Directory.Exists(AppConfs.permissonTmpFolder))
            {
                try
                {
                    DirectoryCopy(AppConfs.permissonTmpFolder , AppConfs.AppFolder, true);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                throw new Exception("No se encontró la carpeta " + AppConfs.permissonTmpFolder);
            }
            // 5.3 Copia archivos de C:\tmpeV_\user\ a Roaming 
            //try
            //{

            //    DirectoryCopy(AppConfs.userTmpFolder, AppConfs.userUpdateFolder, true);

            //}
            //catch (Exception ex)
            //{
            //    this.addStatusTextBoxLine("No se pudieron copiar los archivos a la ubicación final : " + ex.Message);
            //    throw ex;
            //}

        }/////////////////////////////////////////////////////

        public void validateDownloadedFiles(string[] arr)
        {
            string tmpStr = "";
            try
            {
                //Validar en carpeta de la aplicacion
                for (int i = 0; i < arr.Length; i++)
                {
                    tmpStr = arr[i];
                    if (!System.IO.File.Exists(AppConfs.userUpdateFolder + tmpStr.Split('\\').Last()))
                    {
                        throw new Exception("No existe un archivo necesario para continuar la actualización : " + AppConfs.userUpdateFolder + tmpStr);
                    }
                }
            }
            catch (Exception ex)
            {
                this.addStatusTextBoxLine(ex.Message);
                throw ex;
            }
        }//////////////////////////////////////////////////////////////////////////////////

        public void updateProcStart()
        {

            try
            {
                if (pingHost(AppConfs.serverName))
                {
                    //this.addStatusTextBoxLine("No se tiene permisos sobre la carpeta de la aplicacion : " + ex.GetType() + " ; " + ex.Message);
                    this.addStatusTextBoxLine("********* INICIANDO PROCESO DE ACTUALIZACIÓN *********");
                    FixProc fp = new FixProc(this.mw);

                    if (!checkFiles())
                    {
                        this.addStatusTextBoxLine("*** ERROR: No se encuentra  el archivo dll en " + AppConfs.AppFolder + " necesario para la descompresión.");
                        return;
                    }

                    if (fp.fixProcess())
                    {
                        try
                        {
                            this.addStatusTextBoxLine("- Terminando proceso de e-Territory...");
                            UpdateProc.EndEVentasProcess();
                            this.addStatusTextBoxLine("\tProceso de e-Territory terminado exitosamente.");
                        }
                        catch (Exception ex0)
                        {
                            this.addStatusTextBoxLine("\te-Territory no se encuentra en ejecución.");
                        }

                        
                        this.addStatusTextBoxLine("- Reemplazando archivos en carpeta de la aplicación...");
                        this.copyToAppFolder();

                        //borrando IsolatedStorage
                        //************************************
                        try
                        {
                            string appdata=  Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                            string pathIso = appdata + "\\IsolatedStorage";
                            if (Directory.Exists(pathIso))
                                Directory.Delete(pathIso, true);
                            this.addStatusTextBoxLine("- IsoletedStorage borrada.");
                        }catch(Exception ex){
                            this.addStatusTextBoxLine("Isolate: "+ex.Message);
                        }
                        //***********************************

                        this.addStatusTextBoxLine("- Borrando carpeta temporal...");
                        this.deleteTmpFolder();

                        this.addStatusTextBoxLine("\n*** PROCESO DE ACTUALIZACIÓN FINALIZADO EXITOSAMENTE ***");
                        
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    this.addStatusTextBoxLine("*** ERROR: NO ESTÁ CONECTADO A LA RED DE PMI.\nConéctese a la red de PMI y vuelva a intentar.");
                }
            }
            catch (Exception ex1)
            {
                this.addStatusTextBoxLine("*** No se pudo completar la actualización de archivos. " + ex1.Message);
                throw new Exception("No se pudo completar la actualización de archivos. " + ex1.Message);
            }
            //}
        }//////////////////////////////////////////////////////////////////////////////////

        

        public static void EndEVentasProcess()
        {
            try
            {
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo();
                p.StartInfo.UseShellExecute = true;
                p.StartInfo.FileName = "taskkill.exe";
                p.StartInfo.Arguments = "/F /IM "+ AppConfs.ProcessName;
                p.Start();
                p.WaitForExit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//////////////////////////////////////////////////////////////////////////////////

        public void deleteTmpFolder()
        {
            try
            {
                string[] arr = System.IO.Directory.GetFiles(AppConfs.permissonTmpFolder);
                for (int i = 0; i < arr.Length; i++)
                {
                    System.IO.File.Delete(arr[i]);
                }

                System.IO.Directory.Delete(AppConfs.permissonTmpFolder, true);
            }
            catch (Exception ex)
            {
                this.addStatusTextBoxLine("*** No se pudo eliminar carpeta temporal.");
            }

        }///////////////////////////////////

        private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs;
            try
            {
                dirs = dir.GetDirectories();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener subdirectorios: " + ex.Message);
            }

            // If the source directory does not exist, throw an exception.
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "El directorio orígen no existe o no puede ser encontrado: "
                    + sourceDirName);
            }

            // If the destination directory does not exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            try
            {
                // Get the file contents of the directory to copy.
                FileInfo[] files = dir.GetFiles();

                foreach (FileInfo file in files)
                {

                    string tmpStr = "Copiando archivo " + file.Name + " a " + destDirName;
                    this.addStatusTextBoxLine(tmpStr);
                    // Create the path to the new copy of the file.
                    string temppath = Path.Combine(destDirName, file.Name);


                    // Copy the file.
                    file.CopyTo(temppath, true);

                    this.addStatusTextBoxLine("Archivo copiado");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al copiar archivo: " + ex.Message);
            }

            // If copySubDirs is true, copy the subdirectories.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string tmpStr = "Copiando carpeta " + subdir.Name + " a " + destDirName;
                    this.addStatusTextBoxLine(tmpStr);
                    // Create the subdirectory.
                    string temppath = Path.Combine(destDirName, subdir.Name);

                    try
                    {
                        // Copy the subdirectories.
                        DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                        this.addStatusTextBoxLine("Carpeta copiada.");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }//////////////////////////////////////////////

        public void copyImgFolder()
        {
            if (Directory.Exists(AppConfs.permissonTmpFolder + AppConfs.MediaFolder))
            {
                try
                {
                    DirectoryCopy(AppConfs.permissonTmpFolder + AppConfs.MediaFolder, AppConfs.AppFolder + AppConfs.MediaFolder, true);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                this.addStatusTextBoxLine("No se encontró la carpeta " + AppConfs.permissonTmpFolder + AppConfs.MediaFolder+@".");
            }
        }//////////////////

        public string[] getTmpFolderFilesList()
        {
            string[] arr;
            try
            {
                arr = System.IO.Directory.GetFiles(AppConfs.permissonTmpFolder);
            }
            catch (Exception ex)
            {
                this.addStatusTextBoxLine("No se encontró la carpeta " + AppConfs.permissonTmpFolder + ".");
                throw ex;
            }
            return arr;
        }

        public string[] getTmpUserFolderFileList()
        {
            string[] arr;
            try
            {
                arr = System.IO.Directory.GetFiles(AppConfs.userTmpFolder);
            }
            catch (Exception ex)
            {
                this.addStatusTextBoxLine("No se encontró la carpeta de archivos de usuario " + AppConfs.userTmpFolder + ".");
                throw ex;
            }
            return arr;
        }///////////////////////////////////////////////////////

        public bool checkFiles()
        {
            try
            {
                //if (System.IO.Directory.Exists(AppConfs.ZipFilePath))
                //{
                //    string[] arr = System.IO.Directory.GetFiles(AppConfs.ZipFilePath, AppConfs.zipFileName + "*.zip");

                    //if (arr.Length > 0)
                    //{
                        //this.addStatusTextBoxLine("Buscando en la ruta: "+System.IO.Directory.GetCurrentDirectory() + "\\" + "ICSharpCode.SharpZipLib.dll");
                        //if (System.IO.File.Exists(System.IO.Directory.GetCurrentDirectory() + "\\" + "ICSharpCode.SharpZipLib.dll"))
                        if (System.IO.File.Exists(AppConfs.ZipFilePath + "ICSharpCode.SharpZipLib.dll"))
                            return true;
                        else if (System.IO.File.Exists(AppConfs.AppFolder + "ICSharpCode.SharpZipLib.dll"))
                            return true;
                        else return false;
                    //}
                   // else return false;
                //}
                //else return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }///////////////////////////

        public bool pingHost(string host)
        {

            if (!String.IsNullOrEmpty(host))
            {

                try
                {

                    System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();

                    System.Net.NetworkInformation.PingReply pingReply = ping.Send(host);



                    return true;

                }

                catch (System.Net.NetworkInformation.PingException e)
                {

                    //MessageBox.Show(e.Message);

                    return false;

                }

            }

            else
            {

                return false;

            }

        }/////////////////////////////////////////

    }
}
