using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NMT_ESC_DAL;
using System.Data.SqlClient;

namespace IMC.Wpf.CoverFlow.NMT.IMCEXT
{
    class AppSettings
    {
        public static string ZipServerFolder()
        {
            return System.Configuration.ConfigurationManager.AppSettings["CarpetaCompartida"].ToString();
        }
        public static string LocalDBConnection()
        {
            ConexionBD tmp = new ConexionBD();

            return tmp.CadenaConexion;
        }
        public static bool CheckLocalDB()
        {
            SqlConnection sqlConexion;
            SqlCommand sqlCommand;
			
			try
			{
                sqlConexion = new SqlConnection(AppSettings.LocalDBConnection());
                sqlConexion.Open();
                sqlCommand = new SqlCommand("select top 1 * from dbo.BITACORA_SINC", sqlConexion);
                sqlCommand.ExecuteNonQuery();
                sqlConexion.Close();
			}
			catch(Exception Error)
			{
                return false;
			}
            return true;
        }
       
        public static void UpdateDB()
        {
            SqlConnection sqlConexion;
            SqlCommand sqlCommand;
            string query = "IF (object_id('tbl_UpdateTest', 'U') is null)"
            + "CREATE TABLE [dbo].[tbl_UpdateTest]("
	        +" [Id] [int] IDENTITY(1,1) NOT NULL,"
	        +" [Desc] [varchar](250) NULL,"
	        +" [Fecha] [int] NULL"
            +")";

            try
            {
                sqlConexion = new SqlConnection(AppSettings.LocalDBConnection());
                sqlConexion.Open();
                sqlCommand = new SqlCommand(query, sqlConexion);
                sqlCommand.ExecuteNonQuery();
                sqlConexion.Close();
            }
            catch (Exception Err)
            {
                throw new Exception("La actualización de base de datos no se pudo realizar. "+Err.Message);
            }
            
        }//
         
    }
}
