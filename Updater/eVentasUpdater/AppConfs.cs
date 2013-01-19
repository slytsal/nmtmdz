using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace eVentasUpdater
{
    public static class AppConfs
    {
        public static string currentAppPath
        {
            get
            {
                string tmp;
                try
                {
                    tmp = ConfigurationManager.AppSettings["CurrentAppPath"].ToString();
                }
                catch (Exception ex)
                {
                    tmp = "";
                }

                if (String.IsNullOrEmpty(tmp))
                {
                    tmp = System.IO.Path.GetDirectoryName(
                            System.Reflection.Assembly.GetExecutingAssembly().Location
                    );

                    if (tmp[tmp.Length - 1] != '\\')
                        tmp += "\\";
                }

                return tmp;
            }
        }

        public static string permissonExeCSV
        {
            get
            {
                string tmp;
                try
                {
                    tmp = ConfigurationManager.AppSettings["FixExes"].ToString();
                }
                catch (Exception ex)
                {
                    tmp = "";
                }

                if (!String.IsNullOrEmpty(tmp))
                    return tmp;
                else
                    return "eventas_fix_permissons.exe,eventas_fix_permissons.exe.config,no_execute.exe";
            }
            set { }
        }

        public static string permissonServerDir
        {
            get
            {
                string tmp;
                try
                {
                    tmp = ConfigurationManager.AppSettings["FixServerDir"].ToString();
                }
                catch (Exception ex)
                {
                    tmp = "";
                }

                if (!String.IsNullOrEmpty(tmp))
                    return tmp;
                else
                    return @"\\pmintl.net\appdata\pmi-itsc-www-prd\eVentas\FIXeVentas\";

            }
        }

        public static string permissonTmpFolder
        {
            get
            {
                string tmp;
                try
                {
                    tmp = ConfigurationManager.AppSettings["tmpNMT"].ToString();
                }
                catch (Exception ex)
                {
                    tmp = "";
                }

                if (!String.IsNullOrEmpty(tmp))
                    return tmp;
                else
                    return @"C:\tmpNMT_\Uncompressed\";
            }
        }

        public static string permissonMain
        {
            get
            {
                return @"eventas_fix_permisson.exe";
            }
        }

        public static string eventasCheckFolder
        {
            get
            {
                string tmp;
                try
                {
                    tmp = ConfigurationManager.AppSettings["EventasCheckFolder"].ToString();
                }
                catch (Exception ex)
                {
                    tmp = "";
                }

                if (!String.IsNullOrEmpty(tmp))
                    return tmp;
                else
                    return @"c:\Programs\eVentas\Archivos\Contactos\";
            }
        }

        public static string eventasCheckFile
        {
            get
            {
                string tmp;
                try
                {
                    tmp = ConfigurationManager.AppSettings["EventasCheckFile"].ToString();
                }
                catch (Exception ex)
                {
                    tmp = "";
                }

                if (!String.IsNullOrEmpty(tmp))
                    return tmp;
                else
                    return @"copyFolder.txt";
            }
        }

        public static string serverCheckFolder
        {
            get
            {
                string tmp;
                try
                {
                    tmp = ConfigurationManager.AppSettings["ServerCheckFolder"].ToString();
                }
                catch (Exception ex)
                {
                    tmp = "";
                }

                if (!String.IsNullOrEmpty(tmp))
                    return tmp;
                else
                    return @"c:\";
            }
        }//////////////////////////////////////////////////7

        public static string updateFilesFolder
        {
            get
            {
                string tmp;
                try
                {
                    tmp = ConfigurationManager.AppSettings["updateFilesFolder"].ToString();
                }
                catch (Exception ex)
                {
                    tmp = "";
                }

                if (!String.IsNullOrEmpty(tmp))
                    return tmp;
                else
                    return @"\\pmintl.net\appdata\PMI-ITSC-ADW-BORA-PRD\PMMX\NMT\UPDATEFILES\";
            }
        }//////////////////////////////////////////////////7

        public static string userUpdateFolder
        {
            get
            {
                string tmp;
                try
                {
                    tmp = ConfigurationManager.AppSettings["userUpdateFolder"].ToString();
                }
                catch (Exception ex)
                {
                    tmp = "";
                }

                if (!String.IsNullOrEmpty(tmp))
                {
                    string tmp02 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    if (tmp02[tmp02.Length - 1] != '\\')
                        tmp02 += "\\";

                    return tmp02 + tmp;
                }
                else
                {
                    string tmp02 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    if (tmp02[tmp02.Length - 1] != '\\')
                        tmp02 += "\\";
                    return tmp02 + @"eVentas\";
                }

            }
        }//////////////////////////////////////////////////7

        public static Int64 CurFormattedDate
        {
            get
            {
                Int64 cur = 0;
                try
                {
                    string tmp = String.Format("{0:s}", DateTime.Now).Replace("-", "").Replace("T", "").Replace(":", "");
                    cur = Int64.Parse(tmp);
                }
                catch (Exception ex)
                {
                    cur = 19000101000000;
                }

                return cur;
            }
        }//////////////////////

        public static string ZipFilePath
        {
            get
            {
                string tmp;

                try
                {
                    tmp = ConfigurationManager.AppSettings["zipFilePath"].ToString();
                }
                catch (Exception ex)
                {
                    tmp = "";
                }

                if (!String.IsNullOrEmpty(tmp))
                {
                    return tmp;
                }
                else
                {
                    //string tmp02 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                    return /*tmp02 +*/ @"\\pmintl.net\appdata\PMI-ITSC-ADW-BORA-PRD\PMMX\NMT\UPDATEFILES\";
                }
            }
        }/////////////////

        public static string zipFileName
        {
            get
            {
                string tmp;

                try
                {
                    tmp = ConfigurationManager.AppSettings["zipFileName"].ToString();
                }
                catch (Exception ex)
                {
                    tmp = "";
                }

                if (!String.IsNullOrEmpty(tmp))
                {
                    return tmp;
                }
                else
                {
                    //string tmp02 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                    return @"NMT_";
                }
            }
        }/////////////////

        public static string AppFolder
        {
            get
            {
                string tmp;

                try
                {
                    tmp = ConfigurationManager.AppSettings["AppFolder"].ToString();
                }
                catch (Exception ex)
                {
                    tmp = "";
                }

                if (!String.IsNullOrEmpty(tmp))
                {
                    return tmp;
                }
                else
                {
                    return @"C:\Programs\NMT\";
                }
            }
        }/////////////////

        public static string userTmpFolder
        {
            get
            {
                string tmp;

                try
                {
                    tmp = ConfigurationManager.AppSettings["tmpEV_usersFolder"].ToString();
                }
                catch (Exception ex)
                {
                    tmp = "";
                }

                if (!String.IsNullOrEmpty(tmp))
                {
                    return tmp;
                }
                else
                {
                    return @"C:\tmpEv_\users\";
                }
            }
        }////////////////

        public static string finalZipFolder
        {
            get
            {
                string tmp;

                try
                {
                    tmp = ConfigurationManager.AppSettings["finalZipFolder"].ToString();
                }
                catch (Exception ex)
                {
                    tmp = "";
                }

                if (!String.IsNullOrEmpty(tmp))
                {
                    return tmp;
                }
                else
                {
                    return @"C:\tmpNMT_\ZIP\";
                }
            }
        }////////////////

        public static string serverName
        {
            get
            {
                string tmp;

                try
                {
                    tmp = ConfigurationManager.AppSettings["serverName"].ToString();
                }
                catch (Exception ex)
                {
                    tmp = "";
                }

                if (!String.IsNullOrEmpty(tmp))
                {
                    return tmp;
                }
                else
                {
                    return @"pmintl.net";
                }
            }
        }///////////////////////////

        public static string eVentasExeName
        {
            get
            {

                string tmp;

                try
                {
                    tmp = ConfigurationManager.AppSettings["eVentasExeName"].ToString();
                }
                catch (Exception ex)
                {
                    tmp = "";
                }

                if (!String.IsNullOrEmpty(tmp))
                {
                    return tmp;
                }
                else
                {
                    return @"eVentasWpf.exe";
                }
            }
        }//////////7

        public static string DFSZipFolder
        {
            get
            {

                string tmp;

                try
                {
                    tmp = ConfigurationManager.AppSettings["DFSZipFolder"].ToString();
                }
                catch (Exception ex)
                {
                    tmp = "";
                }

                if (!String.IsNullOrEmpty(tmp))
                {
                    return tmp;
                }
                else
                {
                    return @"\\pmintl.net\appdata\PMI-ITSC-ADW-BORA-PRD\PMMX\NMT\UPDATEFILES\";
                }
            }
        }//////////

        public static string ProcessName
        {
            get
            {

                string tmp;

                try
                {
                    tmp = ConfigurationManager.AppSettings["ProcessName"].ToString();
                }
                catch (Exception ex)
                {
                    tmp = "";
                }

                if (!String.IsNullOrEmpty(tmp))
                {
                    return tmp;
                }
                else
                {
                    return @"IMC.Wpf.CoverFlow.NMT.exe";
                }
            }
        }//////////

        public static string MediaFolder
        {
            get
            {

                string tmp;

                try
                {
                    tmp = ConfigurationManager.AppSettings["MediaFolder"].ToString();
                }
                catch (Exception ex)
                {
                    tmp = "";
                }

                if (!String.IsNullOrEmpty(tmp))
                {
                    return tmp;
                }
                else
                {
                    return @"\Media\";
                }
            }
        }//////////@"imgs\"

    }
}
