using System;
using System.Diagnostics;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace eVentasUpdater
{
    class FixProc
    {
        private MainWindow mw = null;

        public FixProc(MainWindow mw)
        {
            this.mw = mw;
        }

        public void createTmpFolder()
        {
            if (System.IO.Directory.Exists(AppConfs.permissonTmpFolder))
            {
                System.IO.Directory.Delete(AppConfs.permissonTmpFolder, true);
            }
            System.IO.Directory.CreateDirectory(AppConfs.permissonTmpFolder);

        }//////////////////////////////////////////////////////////////////////////////////

        public void copyPermissonFiles()
        {
            string tmpStr;
            //Validar en carpeta de la aplicacion
            for (int i = 0; i < AppConfs.permissonExeCSV.Split(',').Length; i++)
            {
                //obtener primer exe de la lista
                tmpStr = AppConfs.permissonExeCSV.Split(',')[i];
                if (!System.IO.File.Exists(AppConfs.currentAppPath + tmpStr))
                {
                    //Descargar de servidor
                    try
                    {
                        this.downloadPermissonFiles(tmpStr);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
                else
                {
                    //Copiar de carpeta del programa
                    System.IO.File.Copy(AppConfs.currentAppPath + tmpStr, AppConfs.permissonTmpFolder + tmpStr, true);
                }
            }
        }//////////////////////////////////////////////////////////////////////////////////

        public void downloadPermissonFiles(string file)
        {
            try
            {
                if (!String.IsNullOrEmpty(AppConfs.permissonServerDir + file))
                {
                    System.IO.File.Copy(AppConfs.permissonServerDir + file, AppConfs.permissonTmpFolder + file, true);
                }
            }
            catch (Exception ex)
            {
                this.addStatusTextBoxLine("Ocurrio un error al intentar copiar el archivo " + file + " del servidor " + AppConfs.permissonServerDir);
                throw ex;
            }

        }//////////////////////////////////////////////////////////////////////////////////

        public void executeFixPermisson()
        {
            try
            {
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo();
                p.StartInfo.FileName = AppConfs.permissonTmpFolder + AppConfs.permissonMain;

                p.StartInfo.UseShellExecute = true;
                //p.StartInfo.RedirectStandardOutput = true;
                //p.StartInfo.RedirectStandardError = true;
                //StreamReader srSalida = p.StandardOutput;
                //StreamReader srError = p.StandardError;

                p.Start();
                p.WaitForExit();

                //string  salida= srSalida.ReadToEnd();
                //string error = srError.ReadToEnd();

                //this.addStatusTextBoxLine("Salida: " + salida);
                //this.addStatusTextBoxLine("Error: " + error);

                this.addStatusTextBoxLine("Configuracion de permisos Finalizada.");


            }
            catch (Exception ex)
            {
                this.addStatusTextBoxLine("Configuración de permisos terminada con errores: " + ex.Message);
                throw new Exception("Error en la configuración de permisos: " + ex.Message);
            }
        }//////////////////////////////////////////////////////////////////////////////////

        public void validateLocalFixFiles()
        {
            try
            {
                string tmpStr;
                //Validar en carpeta de la aplicacion
                for (int i = 0; i < AppConfs.permissonExeCSV.Split(',').Length; i++)
                {
                    tmpStr = AppConfs.permissonExeCSV.Split(',')[i];
                    if (!System.IO.File.Exists(AppConfs.permissonTmpFolder + tmpStr))
                    {
                        throw new Exception("No existe un archivo necesario para continuar la actualización.");
                    }
                }
            }
            catch (Exception ex)
            {
                this.addStatusTextBoxLine(ex.Message);
                throw ex;
            }
        }//////////////////////////////////////////////////////////////////////////////////

        public bool fixProcess()
        {
            try
            {
                this.addStatusTextBoxLine("- Creando carpeta temporal...");
                // 2. Crear carpeta temporal en C:\
                this.createTmpFolder();

                if (!pingHost(AppConfs.serverName))
                {
                    this.addStatusTextBoxLine("*** ERROR: NO ESTÁ CONECTADO A LA RED DE PMI.\nConéctese a la red de PMI y vuelva a intentar.");
                    return false;
                }

                // obtiene el nombre y ruta del archivo ZIP
                this.addStatusTextBoxLine("- Obteniendo ruta de archivo ZIP...");
                string filePath = "";
                filePath = getZipFilePath();

                if (!(String.IsNullOrEmpty(filePath) || filePath.Length == 0))
                    this.addStatusTextBoxLine("\tRuta obtenida: " + filePath);

                if (!String.IsNullOrEmpty(filePath) && filePath.Length > 0)
                {
                    this.addStatusTextBoxLine("- Buscando en: " + filePath);
                    if (reubicateZipFile(filePath))
                    {
                        this.addStatusTextBoxLine("- Archivo Zip reubicado.");
                        string[] arr = filePath.Split('\\');
                        string finalZipPath = AppConfs.finalZipFolder + arr[arr.Length - 1];

                        this.addStatusTextBoxLine("- Descomprimiendo archivo ZIP...");
                        // 3. Descomprime el contenido del archivo ZIP en la carpeta temporal
                        if (this.UnZipFile(finalZipPath, AppConfs.permissonTmpFolder))
                        {
                            this.addStatusTextBoxLine("- Archivo Zip descomprimido.");

                            //this.addStatusTextBoxLine("- Configurando permisos en carpeta de instalación...");
                            // 4. Configuración de permisos en carpeta C:\Programs\NMT
                            //this.executeFixPermisson();
                            //this.addStatusTextBoxLine("- Permisos configurados.");

                        }
                        else return false;
                    }
                    else
                    {
                        this.addStatusTextBoxLine("*** No se pudo completar el proceso de actualización.");
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.addStatusTextBoxLine("*** No se pudo terminar el proceso de copiado de archivos. " + ex.Message);
                throw new Exception("No se pudo terminar el proceso de copiado de archivos.\n" + ex.Message);
            }
            return true;
        }//////////////////////////////////////////////////////////////////////////////////

        public bool reubicateZipFile(string filePath)
        {
            try
            {
                // Crea carpeta para el archivo Zip
                this.addStatusTextBoxLine("- Reubicando archivo ZIP...");
                if (Directory.Exists(AppConfs.finalZipFolder))
                    Directory.Delete(AppConfs.finalZipFolder, true);

                Directory.CreateDirectory(AppConfs.finalZipFolder);

                string[] arr = filePath.Split('\\');
                string finalZipPath = AppConfs.finalZipFolder + arr[arr.Length - 1];
                string nombre = finalZipPath;

                this.addStatusTextBoxLine("- Copiando de " + filePath + " a " + nombre);
                if (Directory.Exists(AppConfs.finalZipFolder))
                    File.Copy(filePath, nombre);
            }
            catch (Exception ex)
            {
                this.addStatusTextBoxLine("*** No se pudo copiar el archivo Zip a su ubicación final. " + ex.Message);
                return false;
            }
            return true;
        }

        public void addStatusTextBoxLine(string msg)
        {
            mw.Dispatcher.BeginInvoke(new Action(() => mw.addStatusTextBoxLine(msg)));
        }//////////////////////////////////////////////////

        public void checkFolderPermisson()
        {
        }

        public bool UnZipFile(string zipFilePath, string targetFolder)
        {
            bool ret = true;
            try
            {
                if (File.Exists(zipFilePath))
                {
                    string baseDirectory = targetFolder;

                    using (ZipInputStream ZipStream = new ZipInputStream(File.OpenRead(zipFilePath)))
                    {
                        ZipEntry theEntry;
                        while ((theEntry = ZipStream.GetNextEntry()) != null)
                        {
                            if (theEntry.IsFile)
                            {
                                
                                if (theEntry.Name != "")
                                {
                                    string strNewFile = @"" + baseDirectory + @"\" + theEntry.Name;
                                    if (File.Exists(strNewFile))
                                    {
                                        continue;
                                    }

                                    using (FileStream streamWriter = File.Create(strNewFile))
                                    {
                                        
                                        int size = 4096;
                                        byte[] data = new byte[4096];
                                        while (true)
                                        {
                                            size = ZipStream.Read(data, 0, data.Length);
                                            if (size > 0)
                                                streamWriter.Write(data, 0, size);
                                            else
                                                break;
                                        }//while
                                        streamWriter.Close();
                                    }//using
                                }//if
                            }//using
                            else if (theEntry.IsDirectory)
                            {
                                
                                string strNewDirectory = @"" + baseDirectory + @"\" + theEntry.Name;
                                if (!Directory.Exists(strNewDirectory))
                                {
                                    Directory.CreateDirectory(strNewDirectory);
                                }
                            }//else-if
                        }
                        ZipStream.Close();
                    }//using
                }
                else
                {
                    this.addStatusTextBoxLine("*** El archivo Zip no existe en la carpeta Temporal.");
                }
            }
            catch (Exception ex)
            {
                this.addStatusTextBoxLine("*** Error al descomprimir: " + ex.Message);
                ret = false;
            }
            return ret;
        }  ////////////////////////////////////////////////////////

        public string getZipFilePath()
        {

            string zipActual = "";
            string[] arr;
            if (System.IO.Directory.Exists(AppConfs.ZipFilePath))
            {
                arr = System.IO.Directory.GetFiles(AppConfs.ZipFilePath, AppConfs.zipFileName + "*");

                if (arr.Length > 0)
                {
                    try
                    {
                        foreach (string file in arr)
                        {
                            if (file.Contains(AppConfs.zipFileName))
                            {

                                if (zipActual == "")
                                    zipActual = file;
                                else
                                {
                                    string[] zA = zipActual.Split('\\');
                                    string[] zN = file.Split('\\');

                                    string zActual = zA[zA.Length - 1];
                                    string zNew = zN[zN.Length - 1];

                                    if (Int64.Parse(zActual.Split('_')[1].Split('.')[0]) < Int64.Parse(zNew.Split('_')[1].Split('.')[0]))
                                        zipActual = file;
                                }

                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        //try
                        //{
                        //    zipActual = getDFSZipFolder();
                        //}
                        //catch (Exception ex2)
                        //{
                        //    throw new Exception(ex2.Message);
                        //}
                        throw new Exception(ex.Message);
                    }
                }
                else this.addStatusTextBoxLine("***No se encontraron actualizaciones a la aplicación. Usted ya cuenta con la última versión disponible.");

            }
            else this.addStatusTextBoxLine("***No se pudo encontrar la carpeta de sincronización. Verifique su conexión a la red de PMI.");


            //if (zipActual.Contains("_3000.zip"))
            //    zipActual = "";

            return zipActual;
        }///////////////////////////////////////////

        public string getDFSZipFolder()
        {
            string[] arr;
            string zipName = "";
            try
            {
                this.addStatusTextBoxLine("Buscando en " + AppConfs.DFSZipFolder);
                if (Directory.Exists(AppConfs.DFSZipFolder))
                {
                    arr = System.IO.Directory.GetFiles(AppConfs.DFSZipFolder, AppConfs.zipFileName + "*");
                    if (arr.Length > 0)
                    {
                        zipName = arr[0];
                        this.addStatusTextBoxLine(AppConfs.DFSZipFolder + " encontrado.");
                    }
                    else this.addStatusTextBoxLine("***No se encontraron actualizaciones a la aplicación. Usted ya cuenta con la última versión disponible.");
                }
                else this.addStatusTextBoxLine("***No se pudo encontrar la carpeta de sincronización. Verifique su conexión a la red de PMI.");
            }
            catch (Exception ex2)
            {
                throw new Exception("Error al buscar el archivo ZIP: " + ex2.Message);
            }

            return zipName;
        }/////////////////////////////

        public static bool pingHost(string host)
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
