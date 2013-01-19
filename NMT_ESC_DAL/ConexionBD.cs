using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Diagnostics;
using System.IO;

namespace NMT_ESC_DAL
{
    /// <remarks>
    /// Autor:  César Murcia - ASINE
    /// email:  cmurcia@gmail.com
    /// </remarks>
    /// <remarks>
    /// Fecha:  22-11-2011
    /// </remarks>
    /// <summary>
    /// Clase para apertura, ejecuación y cierre a la base de datos
    /// </summary>
	public class ConexionBD
	{
        /// <summary>
		/// Variable glogal que contiene la 
		/// cadena de conexión a la base de datos NMT
        /// </summary>
		private string vg_CadenaConexion;

        private string vg_BDConexion;
        /// <summary>
        /// Variable glogal que contiene la 
        /// cadena de conexión a la base de datos SMS
        /// </summary>
        private string vg_CadenaConexionSMS;
        /// <summary>
        /// Variable glogal que contiene la 
        /// cadena de conexión a la base de datos SMS
        /// </summary>
        private string vg_CadenaConexionBitacora;

        /// <summary>
		/// Constructor por omisión
        /// </summary>
		public ConexionBD()
		{
			//Nombre de la BD
            this.vg_BDConexion = ConfigurationManager.AppSettings["NombreBD"];
            // Obtiene la cadena de conexión para NMT de una Key del Web.config
            this.vg_CadenaConexion = ConfigurationManager.AppSettings["ConexionBD"] + " Initial Catalog=" + vg_BDConexion;
            // Obtiene la cadena de conexión para SMS de una Key del Web.config
            this.vg_CadenaConexionSMS = ConfigurationManager.AppSettings["ConexionSMS"];
            // Obtiene la cadena de conexión para SMS de una Key del Web.config
            this.vg_CadenaConexionBitacora = ConfigurationManager.AppSettings["ConexionBitacora"];
		}

        //--------------------------------------------
        #region Métodos para SMS

        /// <summary>
        /// Método para obtener o cambiar la variable
        /// vg_CadenaConexion
        /// </summary>
        public string CadenaConexionSMS
        {
            get { return vg_CadenaConexionSMS; }
            set { vg_CadenaConexionSMS = value; }
        }

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="script"></param>
        public void ejecutarScript(string script)
        {
            SqlConnection sqlConexion;
            SqlDataAdapter sqlDataAdapter;


            try
            {
                DataSet dataSet= new DataSet();
                sqlConexion = new SqlConnection(this.CadenaConexion.Replace(";Initial Catalog=" + vg_BDConexion,""));
                sqlConexion.Open();
                SqlCommand cmd = sqlConexion.CreateCommand();
                cmd.CommandText = script;
                int o = cmd.ExecuteNonQuery();                
                cmd.Dispose();
                sqlConexion.Close();                
            }
            catch (SqlException)
            {
                throw (new Exception("Error al conectarse con la base de datos"));
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.Message));
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="script"></param>
        public void ejecutarScriptBase(string script)
        {
            SqlConnection sqlConexion;
            SqlDataAdapter sqlDataAdapter;


            try
            {
                DataSet dataSet = new DataSet();
                sqlConexion = new SqlConnection(this.CadenaConexion);
                sqlConexion.Open();
                SqlCommand cmd = sqlConexion.CreateCommand();
                cmd.CommandText = script.Replace("&", "|");
                
                int o = cmd.ExecuteNonQuery();

                cmd.Dispose();
                sqlConexion.Close();
            }
            catch (SqlException)
            {
                if (!script.Contains("Dummy"))
                throw (new Exception("Error al conectarse con la base de datos "+script));
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.Message));
            }

        }     

        /// <summary>
        /// Método para autenticación en la BD SMS
        /// </summary>
        /// <param name="sql">Consulta a ejecutar (nombre SP)</param>
        public DataSet getDataSetSMS(string sql)
        {
            SqlConnection sqlConexion;
            SqlDataAdapter sqlDataAdapter;
            DataSet dataSet;

            try
            {
                sqlConexion = new SqlConnection(this.CadenaConexionSMS);
                sqlConexion.Open();
                sqlDataAdapter = new SqlDataAdapter(sql, sqlConexion);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
                sqlConexion.Close();
                return dataSet;
            }
            catch (SqlException)
            {
                throw (new Exception("Error al conectarse con la base de datos"));
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.Message));
            }
        }


        /// <summary>
        /// Método para autenticación en la BD SMS
        /// </summary>
        /// <param name="sql">Consulta a ejecutar (nombre SP)</param>
        public DataSet getDataSetBitacora(string sql)
        {
            SqlConnection sqlConexion;
            SqlDataAdapter sqlDataAdapter;
            DataSet dataSet;

            try
            {
                sqlConexion = new SqlConnection(this.CadenaConexionBitacora);
                sqlConexion.Open();
                sqlDataAdapter = new SqlDataAdapter(sql, sqlConexion);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
                sqlConexion.Close();
                return dataSet;
            }
            catch (SqlException)
            {
                throw (new Exception("Error al conectarse con la base de datos"));
                
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.Message));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        public void executeBitacora(string sql, bool local)
        {
            SqlConnection sqlConexion;
            SqlDataAdapter sqlDataAdapter;
            DataSet dataSet;

            try
            {
                if(!local)
                    sqlConexion = new SqlConnection(this.CadenaConexionBitacora);
                else
                    sqlConexion = new SqlConnection(this.CadenaConexion);
                sqlConexion.Open();
                SqlCommand cmd = sqlConexion.CreateCommand();
                cmd.CommandText = sql;
                int o = cmd.ExecuteNonQuery();
                sqlConexion.Close();               
            }
            catch (SqlException)
            {
                throw (new Exception("Error al conectarse con la base de datos"));
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.Message));
            }
        }

        #endregion
        //--------------------------------------------

        //--------------------------------------------
		#region Métodos para NMT

        /// <summary>
        /// Método para obtener o cambiar la variable
		/// vg_CadenaConexion
        /// </summary>
		public string CadenaConexion
		{
			get{return vg_CadenaConexion;}
			set{vg_CadenaConexion = value;}
		}

        /// <summary>
        /// Método para obtener o cambiar la variable
        /// vg_CadenaConexion
        /// </summary>
        public string CadenaConexionBitacora
        {
            get { return vg_CadenaConexionBitacora; }
            set { vg_CadenaConexionBitacora = value; }
        }

        /// <summary>
		/// Método que realiza una consulta y retorna un DataSet
        /// </summary>
		public DataSet getDataSet(string sql)
		{
			SqlConnection sqlConexion;
            SqlDataAdapter sqlDataAdapter;
			DataSet dataSet; 

			try
			{
                sqlConexion = new SqlConnection(this.CadenaConexion);
                sqlConexion.Open();
                sqlDataAdapter = new SqlDataAdapter(sql, sqlConexion);
                dataSet = new DataSet();                
                sqlDataAdapter.Fill(dataSet);
                sqlConexion.Close();
                return dataSet;
			}
			catch(Exception Error)
			{
				throw( new Exception(Error.Message));
			}
		}
		
        /// <summary>
		/// Método que realiza una consulta y retorna un string
        /// </summary>
        /// <param name="sql">Consulta a ejecutar (nombre SP)</param>
		public string getString(string sql)
		{
            SqlConnection sqlConexion;
			SqlCommand sqlCommand;
			string valor; 

			try
			{
                sqlConexion = new SqlConnection(this.CadenaConexion);
                sqlConexion.Open();
                sqlCommand = new SqlCommand(sql, sqlConexion);
                valor = sqlCommand.ExecuteScalar().ToString();
                sqlConexion.Close();
                return valor;
			}
			catch(Exception Error)
			{
				throw(new Exception(Error.Message));
			}
		}

        /// <summary>
		/// Método que realiza una consulta
        /// </summary>
        /// <param name="sql">Consulta a ejecutar (nombre SP)</param>
		public void executeSQL(string sql)
		{
            SqlConnection sqlConexion;
            SqlCommand sqlCommand;
			
			try
			{
                sqlConexion = new SqlConnection(this.CadenaConexion);
                sqlConexion.Open();
                sqlCommand = new SqlCommand(sql, sqlConexion);
                sqlCommand.ExecuteNonQuery();
                sqlConexion.Close();
			}
			catch(Exception Error)
			{
				throw( new Exception(Error.Message));
			}
		}

        #endregion
        //--------------------------------------------

	} // Clase

} // namespace
