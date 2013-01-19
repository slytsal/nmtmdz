using System;
using System.Data;
using System.Data.SqlClient;

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
    /// Clase que ejecuta las consultas básicas
	///	(select, insert, update, delete) a la 
	///	base de datos mediante Procedimientos Almacenados.
	/// </summary>
	public class Consultas
    {

        #region MDZ
        /// <summary>
        /// Selección a KPI_MDZ_VisitasPlaneadas para sacar filtro de MDZDescription
        /// </summary>
        public DataSet SelectKPI_MDZ_VisitasPlaneadas_MDZDescription()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectMDZDescriptionVisitasPlaneadas";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a KPI_MDZ_VisitasPlaneadas para sacar CLO
        /// </summary>
        public DataSet SelectKPI_MDZ_VisitasPlaneadas_CLODescription(string filtro)
        {

            ConexionBD conexion;
            string consulta = "spSelectCloDescriptionVisitasPlaneadas '" + filtro + "'";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a KPI_MDZ_VisitasPlaneadas para sacar RouteCode
        /// </summary>
        public DataSet SelectKPI_MDZ_VisitasPlaneadas_RouteCode(string filtro)
        {

            ConexionBD conexion;
            string consulta = "spSelectRouteCodeVisitasPlaneadas '" + filtro + "'";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a KPI_MDZ_VisitasPlaneadas para sacar filtro de IndustriClasification
        /// </summary>
        public DataSet SelectKPI_MDZ_VisitasPlaneadas_IndustriClasification()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectIndustriClasificationVisitasPlaneadas";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a KPI_MDZ_VisitasPlaneadas para sacar filtro de IndustriClasification
        /// </summary>
        public DataSet SelectKPI_MDZ_VisitasPlaneadas_LocalCSUFlag()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectLocalCSUFlagVisitasPlaneadas";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a KPI_MDZ_VisitasPlaneadas CON TODOS LOS FILTROS
        /// </summary>
        public DataSet SelectKPI_MDZ_VisitasPlaneadas(params string[] filtros)
        {
            
            string MDZDescription =filtros != null && filtros.Length > 0 && filtros[0] != "" ? filtros[0] : "%";
            string CLODescription=filtros != null && filtros.Length > 0 && filtros[1] != "" ? filtros[1] : "%";
            string RouteCode=filtros != null && filtros.Length > 0 && filtros[2] != "" ? filtros[2] : "%";
            string IndustriClasification =filtros != null && filtros.Length > 0 && filtros[3] != "" ? filtros[3] : "%";
            string LocalCSUFlag = filtros != null && filtros.Length > 0 && filtros[4] != "" ? filtros[4] : "%";
            ConexionBD conexion;
            string consulta = "exec spSelectKPI_MDZ_VisitasPlaneadas '" + MDZDescription + "','" + CLODescription + "','" + RouteCode + "','" + IndustriClasification + "','" + LocalCSUFlag + "'";
             
            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        #endregion
        //----------------------------------------------------------------:D
        #region metodos eder
        /// <summary>
        /// Selección a KPI_Volumen_Por_Grupo filtro Segmentacion
        /// </summary>
        public DataSet SelectKPI_DistibucionPDVGrupo_Flt_Segmentacion()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_DistibucionPDVGrupo_Flt_Segmentacion";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a KPI_VolumenPorGrupo
        /// </summary>
        public DataSet SelectKPI_DistibucionPDVGrupo(params string[] filtros)
        {
            string IndustryLevel = filtros != null && filtros.Length > 0 && filtros[0] != "" ? filtros[0] : "%";
            ConexionBD conexion;
            string consulta = "spSelectKPI_DistibucionPDVGrupo '" + IndustryLevel + "'";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a KPI_Volumen_Por_Grupo filtro Segmentacion
        /// </summary>
        public DataSet SelectKPI_VolumenPorGrupo_Flt_Segmentacion()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_VolumenPorGrupo_Flt_Segmentacion";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a KPI_VolumenPorGrupo
        /// </summary>
        public DataSet SelectKPI_VolumenPorGrupo(params string[] filtros)
        {
            string IndustryLevel = filtros != null && filtros.Length > 0 && filtros[0] != "" ? filtros[0] : "%";
            ConexionBD conexion;
            string consulta = "spSelectKPI_VolumenPorGrupo '" + IndustryLevel + "'";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a C900_WS_CPWBUDI_Params
        /// </summary>
        public DataSet SelectParametros()
        {
            
            ConexionBD conexion;
            string consulta = "exec spSelect_C900_WS_CPWBUDI_Params";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a Clientes solo totalbudi y CPW
        /// </summary>
        public DataSet SelectPointsClientes()
        {

            ConexionBD conexion;
            string consulta = "exec spSelectPointsClientes";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a Clientes solo totalbudi y CPW
        /// </summary>
        public DataSet SelectPointsClientesFiltrado(string Filtro)
        {

            ConexionBD conexion;
            string consulta = "exec spSelectPointsClientesFiltrado "+ Filtro;

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a consultaClientes para sacar clientes
        /// </summary>
        public DataSet SelectKPI_ConsultaPorCliente_Flt_Cliente()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_ConsultaPorClientes_Flt_Cliente";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a consultaClientes por cliente
        /// </summary>
        public DataSet SelectConsultaClientesFiltrado(string Filtro)
        {

            ConexionBD conexion;
            string consulta = "exec spSelectConsultaClienteFiltrado '" + Filtro.Substring(0, 11)+"'";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a consultaClientes solo totalbudi y CPW
        /// </summary>
        public DataSet SelectPointConsultaClienteFiltrado(string Filtro)
        {

            ConexionBD conexion;
            string consulta = "exec spSelectPointConsultaClienteFiltrado '" + Filtro.Substring(0, 11)+"'";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a Clientes de total budi
        /// </summary>
        /// 
        public DataSet SelectClientesBudi()
        {

            ConexionBD conexion;
            string consulta = "exec spSelectClientes";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a Clientes de Filtrado
        /// </summary>
        public DataSet SelectClientesBudiFiltrado(string Filtro)
        {

            ConexionBD conexion;
            string consulta = "exec spSelectClientesFiltrado "+Filtro;

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a KPI_Promedio_Marcas_Facturadas filtro Segmentacion
        /// </summary>
        public DataSet SelectKPI_PromedioMarcasFacturadas_Flt_Segmentacion()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_PromedioMarcasFacturadas_Flt_Segmentacion";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a KPI_Promedio_Marcas_Facturadas
        /// </summary>
        public DataSet SelectKPI_PromedioMarcasFacturadas(params string[] filtros)
        {
            string IndustryLevel = filtros != null && filtros.Length > 0 && filtros[0] != "" ? filtros[0] : "%";
            ConexionBD conexion;
            string consulta = "spSelectKPI_PromedioMarcasFacturadas '" + IndustryLevel + "'";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a KPI_Porcentaje_Cliente_Por_Precio_Correcto filtro Segmentacion
        /// </summary>
        public DataSet SelectKPI_PorcentajeClientePorPrecioCorrecto_Flt_Segmentacion()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_PorcentajeClientePorPrecioCorrecto_Flt_Segmentacion";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a KPI_Porcentaje_Cliente_Por_Precio_Correct
        /// </summary>
        public DataSet SelectKPI_PorcentajeClientePorPrecioCorrecto(params string[] filtros)
        {
            string IndustryLevel = filtros != null && filtros.Length > 0 && filtros[0] != "" ? filtros[0] : "%";
            ConexionBD conexion;
            string consulta = "spSelectKPI_PorcentajeClientePorPrecioCorrecto '" + IndustryLevel + "'";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a KPI_Clientes_Por_Visibilidad filtro Segmentacion
        /// </summary>
        public DataSet SelectKPI_ClientesPorVisibilidad_Flt_Segmentacion()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_ClientesPorVisibilidad_Flt_Segmentacion";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a KPI_Clientes_Por_Visibilidad
        /// </summary>
        public DataSet SelectKPI_ClientesPorVisibilidad(params string[] filtros)
        {
            string IndustryLevel = filtros != null && filtros.Length > 0 && filtros[0] != "" ? filtros[0] : "%";
            ConexionBD conexion;
            string consulta = "spSelectKPI_ClientesPorVisibilidad '" + IndustryLevel + "'";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a KPI_Facturado filtro Cliente
        /// </summary>
        public DataSet SelectKPI_Facturado_Flt_Cliente()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_Facturado_Flt_Cliente";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a KPI_Facturado
        /// </summary>
        public DataSet SelectKPI_Facturado(string[] filtros)
        {
            string IndustryLevel = filtros != null && filtros.Length > 0 && filtros[0] != "" ? filtros[0] : "%";
            string IndustryLevel2 = filtros != null && filtros.Length > 0 && filtros[1] != "" ? filtros[1] : "%";
            ConexionBD conexion;
           
            string consulta;
            if (IndustryLevel != "TODOS")
            {
                consulta = "spSelectKPI_Facturado '" + IndustryLevel.Substring(0, 11) + "','" + IndustryLevel2 + "'";
            }
            else
                consulta = "spSelectKPI_Facturado '" + IndustryLevel + "','" + IndustryLevel2 + "'";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a KPI_Clientes y KPI_ConsultaPorCliente JOIN
        /// </summary>
        public DataSet Select_ClientesDetallado( string filtros)
        {
            //string IndustryLevel = filtros != null && filtros.Length > 0 && filtros[0] != "" ? filtros[0] : "%";
            //string ProductSubfamilyCode = filtros != null && filtros.Length > 1 ? filtros[1] : "";
            //string ProductSubfamilyDescription = filtros != null && filtros.Length > 2 ? filtros[2] : "";
            //string TimeId = filtros != null && filtros.Length > 3 ? filtros[3] : "0";

            ConexionBD conexion;
            string consulta = "EXEC sp_SelectClientesDetallado '" +filtros+"'";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
       
        #endregion
        

        //----------------------------------------------------------------:D

        //--------------------------------------------
		#region Autenticación SMS

		/// <summary>
		/// Autenticación en SMS
		/// </summary>
		public DataSet getAutenticacion(string nombreBDISMS)
		{
			ConexionBD conexion;
			string consulta = "EXEC spAutenticacionSMS '"+nombreBDISMS+"'";

			try
			{
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
			}
            catch(Exception Error)
			{
                throw( new Exception(Error.Message));
			}
		}
		
		#endregion
        //--------------------------------------------

        //--------------------------------------------
        #region Consultas a NMT

        /// <summary>
        /// Selección a NMT_GROUP
        /// </summary>
        public DataSet getNMTGroup()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectGroup";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString(),Error));
            }
        }

        /// <summary>
        /// Seleccion de los KPI del grupo
        /// </summary>
        /// <param name="idGrupo"></param>
        /// <returns></returns>
        public DataSet getNMTKPIByGroup(int idGrupo)
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPIByGroup "+idGrupo;

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Seleccion de los KPI del grupo
        /// </summary>
        /// <param name="idGrupo"></param>
        /// <returns></returns>
        public DataSet getNMTKPIByID(int idKPI)
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPIByID " + idKPI;

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Seleccion del ultimo filtro de Clientes 
        /// </summary>
        /// <param name="idGrupo"></param>
        /// <returns></returns>
        public DataSet SelectLastFiltroClientes()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectLastFiltroClientes ";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        #endregion
        //--------------------------------------------

        //--------------------------------------------
        

        #region Consultas de KPI's

        #region Consultas SOM
        /// <summary>
        /// Selección a KIP_SOM
        /// </summary>
        public DataSet SelectKPI_SOM(params string[] filtros)
        {
            string ManufacturerCode = filtros != null && filtros.Length > 0 ? filtros[0] : "";//filtros != null && filtros.Length > 0 && filtros[0] != "" ? filtros[0] : "%";
            string SegmentPrice = filtros != null && filtros.Length > 1 && filtros[1] != "" ? filtros[1] : "%";
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_SOM '"+ManufacturerCode+"' , '"+SegmentPrice+"'";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Selección a KIP_SOM filtro manufact
        /// </summary>
        public DataSet SelectKPI_SOM_Filt_ManufacturerCode()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_SOM_Flt_ManufacturerCode";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Selección a KIP_SOM filtro segment
        /// </summary>
        public DataSet SelectKPI_SOM_Filt_SegmentPrice()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_SOM_Flt_SegmentPrice";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        #endregion 

        #region Share_PMM_SellOut
        /// <summary>
        /// Selección a KPI_Share_PMM_SellOut
        /// </summary>
        public DataSet SelectKPI_Share_PMM_SellOut(params string[] filtros)
        {
            string ChainCode = filtros != null && filtros.Length > 0 && filtros[0] != "" ? filtros[0] : "%";
            string ManufacturerCode = filtros != null && filtros.Length > 1 ? filtros[1] : ""; //filtros != null && filtros.Length > 1 && filtros[1] != "" ? filtros[1] : "%";
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_Share_PMM_SellOut '"+ChainCode+"' , '"+ManufacturerCode+"'";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Selección a KPI_Share_PMM_SellOut_Flt_ChainCode
        /// </summary>
        public DataSet SelectKPI_Share_PMM_SellOut_Flt_ChainCode()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_Share_PMM_SellOut_Flt_ChainCode";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Selección a KPI_Share_PMM_SellOut_Flt_ManufacturerCode
        /// </summary>
        public DataSet SelectKPI_Share_PMM_SellOut_Flt_ManufacturerCode()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_Share_PMM_SellOut_Flt_ManufacturerCode";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        #endregion

        #region Share_SKU
        /// <summary>
        /// Selección a KPI_Share_SKU
        /// </summary>
        public DataSet SelectKPI_Share_SKU(params string[] filtros)
        {
            string ProductSubfamilyCode = filtros != null && filtros.Length > 0 ? filtros[0] : "";
            string ProductSubfamilyDescription = filtros != null && filtros.Length > 1 ? filtros[1] : "";
            string SegmentPrice = filtros != null && filtros.Length > 2 && filtros[2] != "" ? filtros[2] : "%";
            
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_Share_SKU '"+ProductSubfamilyCode+"' , '"+ProductSubfamilyDescription+"' , '"+SegmentPrice+"'";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Selección a KPI_Share_SKU_Flt_ProductSubfamilyCode
        /// </summary>
        public DataSet SelectKPI_Share_SKU_Flt_ProductSubfamilyCode()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_Share_SKU_Flt_ProductSubfamilyCode";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Selección a KPI_Share_SKU_Flt_ProductSubfamilyDescription
        /// </summary>
        public DataSet SelectKPI_Share_SKU_Flt_ProductSubfamilyDescription()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_Share_SKU_Flt_ProductSubfamilyDescription";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Selección a KPI_Share_SKU
        /// </summary>
        public DataSet SelectKPI_Share_SKU_Flt_SegmentPrice()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_Share_SKU_Flt_SegmentPrice";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        #endregion

        #region Share_Segment
        /// <summary>
        /// Selección a KPI_Share_Segment
        /// </summary>
        public DataSet SelectKPI_Share_Segment(params string[] filtros)
        {
            string IndustryLevel = filtros != null && filtros.Length > 0 && filtros[0] != "" ? filtros[0] : "%";
            //filtros != null && filtros.Length > 0 ? filtros[0] : "";
            //string SegmentPrice = filtros != null && filtros.Length > 1 && filtros[1] != "" ? filtros[1] : "%";
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_Share_Segment '" + IndustryLevel + "' ";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Selección a KPI_Share_Segment
        /// </summary>
        public DataSet SelectKPI_Share_Segment_Flt_IndustryLevel()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_Share_Segment_Flt_IndustryLevel";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Selección a KPI_Share_Segment
        /// </summary>
        public DataSet SelectKPI_Share_Segment_Flt_SegmentPrice()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_Share_Segment_Flt_SegmentPrice";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        #endregion

        #region sobreindexados
        /// <summary>
        /// Selección a KPI_Sobreindexados
        /// </summary>
        public DataSet SelectKPI_Sobreindexados(params string[] filtros)
        {
            string IndustryLevel = filtros != null && filtros.Length > 0 && filtros[0] != "" ? filtros[0] : "%";
            string ProductSubfamilyCode = filtros != null && filtros.Length > 1 ? filtros[1] : "";
            string ProductSubfamilyDescription = filtros != null && filtros.Length > 2 ? filtros[2] : "";
            
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_Sobreindexados '"+IndustryLevel+"' , '"+ProductSubfamilyCode+"' , '"+ProductSubfamilyDescription+"'";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Selección a KPI_Sobreindexados
        /// </summary>
        public DataSet SelectKPI_SobreindexadosResumen(params string[] filtros)
        {
            string IndustryLevel = filtros != null && filtros.Length > 0 && filtros[0] != "" ? filtros[0] : "%";
            string ProductSubfamilyCode = filtros != null && filtros.Length > 1 ? filtros[1] : "";
            string ProductSubfamilyDescription = filtros != null && filtros.Length > 2 ? filtros[2] : "";
            string TimeId = filtros != null && filtros.Length > 3 ? filtros[3] : "0";

            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_Sobreindexados_Resumen '" + IndustryLevel + "' , '" + ProductSubfamilyCode + "' , '" + ProductSubfamilyDescription + "', '"+TimeId+"'";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Selección a KPI_Sobreindexados
        /// </summary>
        public DataSet SelectKPI_Sobreindexados_Flt_IndustryLevel()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_Sobreindexados_Flt_IndustryLevel";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a KPI_Sobreindexados
        /// </summary>
        public DataSet SelectKPI_Sobreindexados_Flt_ProductSubfamilyCode()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_Sobreindexados_Flt_ProductSubfamilyCode";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        
        /// <summary>
        /// Selección a KPI_Sobreindexados
        /// </summary>
        public DataSet SelectKPI_Sobreindexados_Flt_ProductSubfamilyDescription()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_Sobreindexados_Flt_ProductSubfamilyDescription";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        #endregion

        #region Clientes_ITO_UX
        /// <summary>
        /// Selección a KPI_Clientes_ITO_UX
        /// </summary>
        public DataSet SelectKPI_Clientes_ITO_UX(params string[] filtros)
        {
            string IndustryLevel = filtros != null && filtros.Length > 0 && filtros[0] != "" ? filtros[0] : "%";
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_Clientes_ITO_UX '"+IndustryLevel+"'";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Selección a KPI_Clientes_ITO_UX
        /// </summary>
        public DataSet SelectKPI_Clientes_ITO_UX_Flt_IndustryLevel()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_Clientes_ITO_UX_Flt_IndustryLevel";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        #endregion

        /// <summary>
        /// Selección a KPI_VisitasPlaneadas_Realizadas
        /// </summary>
        public DataSet SelectKPI_VisitasPlaneadas_Realizadas(params string[] filtros)
        {
            string IndustryLevel = filtros != null && filtros.Length > 0 && filtros[0] != "" ? filtros[0] : "%";
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_VisitasPlaneadas_Realizadas '"+IndustryLevel+"'";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Selección a KPI_VisitasPlaneadas_Realizadas
        /// </summary>
        public DataSet SelectKPI_VisitasPlaneadas_Realizadas_Flt_IndustryLevel()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_VisitasPlaneadas_Realizadas_Flt_IndustryLevel";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        #region FrecuenciaVisitas
        /// <summary>
        /// Selección a KPI_FrecuenciaVisitas
        /// </summary>
        public DataSet SelectKPI_FrecuenciaVisitas(params string[] filtros)
        {
            string IndustryLevel = filtros != null && filtros.Length > 0 ? filtros[0] : "";//filtros != null && filtros.Length > 0 && filtros[0] != "" ? filtros[0] : "%";
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_FrecuenciaVisitas '"+IndustryLevel+"'";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Selección a KPI_FrecuenciaVisitas
        /// </summary>
        public DataSet SelectKPI_FrecuenciaVisitas_Flt_IndustryLevel()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_FrecuenciaVisitas_Flt_IndustryLevel";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }        
        #endregion

        #region SKUAverage
        /// <summary>
        /// Selección a KPI_SKU_Average
        /// </summary>
        public DataSet SelectKPI_SKU_Average(params string[] filtros)
        {
            string SegmentPrice = filtros != null && filtros.Length > 0 && filtros[0] != "" ? filtros[0] : "%";
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_SKU_Average '" + SegmentPrice+"'";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Selección a KPI_SKU_Average
        /// </summary>
        public DataSet SelectKPI_SKU_Average_Flt_SegmentPrice()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_SKU_Average_Flt_SegmentPrice";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        #endregion

        #region SellingEfectivo
        /// <summary>
        /// Selección a KPI_SwitchSellingEfectivo
        /// </summary>
        public DataSet SelectKPI_SwitchSellingEfectivo(params string[] filtros)
        {
            string IndustryLevel = filtros != null && filtros.Length > 0 && filtros[0] != "" ? filtros[0] : "%";
            string Loyalty = filtros != null && filtros.Length > 1 && filtros[1] != "" ? filtros[1] : "%";
            string AgeRange = filtros != null && filtros.Length > 2 && filtros[2] != "" ? filtros[2] : "%";
            string Gender = filtros != null && filtros.Length > 3 && filtros[3] != "" ? filtros[3] : "%";
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_SwitchSellingEfectivo '"+IndustryLevel+"', '"+Loyalty+"', '"+AgeRange+"', '"+Gender+"'";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a KPI_SwitchSellingEfectivo
        /// </summary>
        public DataSet SelectKPI_SwitchSellingEfectivo_Flt_IndustryLevel()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_SwitchSellingEfectivo_Flt_IndustryLevel";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Selección a KPI_SwitchSellingEfectivo
        /// </summary>
        public DataSet SelectKPI_SwitchSellingEfectivo_Flt_Loyalty()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_SwitchSellingEfectivo_Flt_Loyalty";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Selección a KPI_SwitchSellingEfectivo
        /// </summary>
        public DataSet SelectKPI_SwitchSellingEfectivo_Flt_AgeRange()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_SwitchSellingEfectivo_Flt_AgeRange";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Selección a KPI_SwitchSellingEfectivo
        /// </summary>
        public DataSet SelectKPI_SwitchSellingEfectivo_Flt_Gender()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_SwitchSellingEfectivo_Flt_Gender";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        #endregion

        #region SwitchSellingSKU
        /// <summary>
        /// Selección a KPI_SwitchSellingSKU
        /// </summary>
        public DataSet SelectKPI_SwitchSellingSKU(params string[] filtros)
        {
            string OriginalBrandDescription = filtros != null && filtros.Length > 0 && filtros[0] != "" ? filtros[0] : "%";
            string timeId = filtros != null && filtros.Length > 0 && filtros[1] != "" ? filtros[1] : "0";            
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_SwitchSellingSKU '" + OriginalBrandDescription+"' ,"+timeId ;

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Selección a KPI_SwitchSellingSKU Time ID
        /// </summary>
        public DataSet SelectKPI_SwitchSellingSKU_Flt_TimeId()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_SwitchSellingSKU_Flt_TimeId";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Selección a KPI_SwitchSellingSKU
        /// </summary>
        public DataSet SelectKPI_SwitchSellingSKU_Flt_OriginalBrandDescription()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_SwitchSellingSKU_Flt_OriginalBrandDescription";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        #endregion

        #region efectividad MDZ
        /// <summary>
        /// Selección a KPI_EfectividadMDZ
        /// </summary>
        public DataSet SelectKPI_EfectividadMDZ(params string[] filtros)
        {
            string ProductSubfamilyCode = filtros != null && filtros.Length > 0 ? filtros[0] : "";
            string ProductSubfamilyDescription = filtros != null && filtros.Length > 1 ? filtros[1] : "";
            string IndustryLevel = filtros != null && filtros.Length > 2 && filtros[2] != "" ? filtros[2] : "%";
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_EfectividadMDZ '"+ProductSubfamilyCode+"' , '"+ProductSubfamilyDescription+"','"+IndustryLevel+"'";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Selección a KPI_EfectividadMDZ
        /// </summary>
        public DataSet SelectKPI_EfectividadMDZ_Flt_ProductSubfamilyCode()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_EfectividadMDZ_Flt_ProductSubfamilyCode";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Selección a KPI_EfectividadMDZ
        /// </summary>
        public DataSet SelectKPI_EfectividadMDZ_Flt_ProductSubfamilyDescription()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_EfectividadMDZ_Flt_ProductSubfamilyDescription";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a KPI_EfectividadMDZ
        /// </summary>
        public DataSet SelectKPI_EfectividadMDZ_Flt_IndustryLevel()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_EfectividadMDZ_Flt_IndustryLevel";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        #region agotamiento
        /// <summary>
        /// Selección a KPI_Agotamiento
        /// </summary>
        public DataSet SelectKPI_Agotamiento(params string[] filtros)
        {
            string IndustryLevel = filtros != null && filtros.Length > 0 && filtros[0] != "" ? filtros[0] : "%";
            string BrandDescription = filtros != null && filtros.Length > 2 ? filtros[2] : "";
            
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_Agotamiento '"+IndustryLevel+"' , '"+BrandDescription+"'";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a KPI_Agotamiento
        /// </summary>
        public DataSet SelectKPI_Agotamiento_Flt_IndustryLevel()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_Agotamiento_Flt_IndustryLevel";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a KPI_Agotamiento
        /// </summary>
        public DataSet SelectKPI_Agotamiento_Flt_BrandDescription()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_Agotamiento_Flt_BrandDescription";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a KPI_Agotamiento
        /// </summary>
        public DataSet SelectKPI_Agotamiento_Flt_BrandCode()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_Agotamiento_Flt_BrandCode";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        #endregion
        #region Manejos
        /// <summary>
        /// Selección a KPI_Manejos
        /// </summary>
        public DataSet SelectKPI_Manejos(params string[] filtros)
        {
            string IndustryLevel = filtros != null && filtros.Length > 0 && filtros[0] != "" ? filtros[0] : "%";
            string BrandDescription = filtros != null && filtros.Length > 2 ? filtros[2] : "";
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_Manejos '"+IndustryLevel+"' , '"+BrandDescription+"'";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Selección a KPI_Manejos
        /// </summary>
        public DataSet SelectKPI_Manejos_Flt_IndustryLevel()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_Manejos_Flt_IndustryLevel";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Selección a KPI_Manejos
        /// </summary>
        public DataSet SelectKPI_Manejos_Flt_BrandDescription()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_Manejos_Flt_BrandDescription";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Selección a KPI_Manejos
        /// </summary>
        public DataSet SelectKPI_Manejos_Flt_BrandCode()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_Manejos_Flt_BrandCode";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        #endregion
        #endregion

        #region Volumen
        /// <summary>
        /// Selección a KPI_Volumen
        /// </summary>
        public DataSet SelectKPI_Volumen(params string[] filtros)
        {
            string Chain = filtros != null && filtros.Length > 0 && filtros[0] != "" ? filtros[0] : "%";
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_Volumen '"+Chain+"'";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Selección a KPI_Volumen
        /// </summary>
        public DataSet SelectKPI_Volumen_Flt_Chain()
        {
            
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_Volumen_Flt_Chain";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        #endregion

        #region SELL_OUT_KA_SEGMENT_CONTRIBUTION
        /// <summary>
        /// Selección a KPI_Volumen
        /// </summary>
        public DataSet SelectKPI_SELL_OUT_KA_SEGMENT_CONTRIBUTION(params string[] filtros)
        {
            string ChainCode = filtros != null && filtros.Length > 0 && filtros[0] != "" ? filtros[0] : "%";
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_SELL_OUT_KA_SEGMENT_CONTRIBUTION '" + ChainCode + "'";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Selección a KPI_Volumen
        /// </summary>
        public DataSet SelectKPI_SELL_OUT_KA_SEGMENT_CONTRIBUTION_Flt_ChainCode()
        {

            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_SELL_OUT_KA_SEGMENT_CONTRIBUTION_Flt_ChainCode";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        #endregion

        #region Ejecucion De Calidad
        /// <summary>
        /// Selección a KPI_Ejecución_Calidad
        /// </summary>
        public DataSet SelectKPI_Ejecución_Calidad()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_Ejecución_Calidad";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        #endregion

        #region MDZ_SKU_Average
        /// <summary>
        /// Selección a KPI_MDZ_SKU_Average
        /// </summary>
        public DataSet SelectKPI_MDZ_SKU_Average(params string[] filtros)
        {
            string IndustryLevel = filtros != null && filtros.Length > 0 ? filtros[0] : "";//filtros != null && filtros.Length > 0 && filtros[0] != "" ? filtros[0] : "%";
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_MDZ_SKU_Average '" + IndustryLevel + "'";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a KPI_MDZ_SKU_Average
        /// </summary>
        public DataSet SelectKPI_MDZ_SKU_Average_Flt_IndustryLevel()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_MDZ_SKU_Average_Flt_IndustryLevel";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        #endregion


        #region MDZ Coverage
        /// <summary>
        /// Selección a KPI_MDZ_SKU_Average
        /// </summary>
        public DataSet SelectKPI_MDZ_Coverage(params string[] filtros)
        {
            string IndustryLevel = filtros != null && filtros.Length > 0 && filtros[0] != "" ? filtros[0] : "%";
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_MDZ_Coverage '" + IndustryLevel + "'";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a KPI_MDZ_Coverage
        /// </summary>
        public DataSet SelectKPI_MDZ_Coverage_Flt_IndustryLevel()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_MDZ_Coverage_Flt_IndustryLevel";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        #endregion


        #region MDZ_GENERAL_DROP_SIZE
        /// <summary>
        /// Selección a KPI_MDZ_SKU_Average
        /// </summary>
        public DataSet SelectKPI_MDZ_GENERAL_DROP_SIZE(params string[] filtros)
        {
            string IndustryLevel = filtros != null && filtros.Length > 0 ? filtros[0] : ""; //filtros != null && filtros.Length > 0 && filtros[0] != "" ? filtros[0] : "%";
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_MDZ_GENERAL_DROP_SIZE '" + IndustryLevel + "'";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a KPI_MDZ_Coverage
        /// </summary>
        public DataSet SelectKPI_MDZ_GENERAL_DROP_SIZE_Flt_IndustryLevel()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_MDZ_GENERAL_DROP_SIZE_Flt_IndustryLevel";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        #endregion


        #region MDZ_EFFECTIVE_DROP_SIZE
        /// <summary>
        /// Selección a KPI_MDZ_SKU_Average
        /// </summary>
        public DataSet SelectKPI_MDZ_EFFECTIVE_DROP_SIZE(params string[] filtros)
        {
            string IndustryLevel = filtros != null && filtros.Length > 0 ? filtros[0] : ""; //filtros != null && filtros.Length > 0 && filtros[0] != "" ? filtros[0] : "%";
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_MDZ_EFFECTIVE_DROP_SIZE '" + IndustryLevel + "'";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// Selección a KPI_MDZ_Coverage
        /// </summary>
        public DataSet SelectKPI_MDZ_EFFECTIVE_DROP_SIZE_Flt_IndustryLevel()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectKPI_MDZ_EFFECTIVE_DROP_SIZE_Flt_IndustryLevel";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        #endregion


        #endregion


        #region PROCESO ACTUALIZACION
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet UpdateSkins(int ID)
        {
            ConexionBD conexion;
            string consulta = string.Format("EXEC spUpdateSkins {0}", ID);

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet UpdateUltimoIndicador(int ID_GROUP, int LAST_INDEX, string LAST_FILTER)
        {
            ConexionBD conexion;
            string consulta = string.Format("EXEC spUpdateUltimoIndicador {0},{1},'{2}'", ID_GROUP,LAST_INDEX,LAST_FILTER);
            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet UpdateCOLORSIndicador(int ID_KPI, string COLORS)
        {
            ConexionBD conexion;
            string consulta = string.Format("EXEC spUpdateColors {0},'{1}'", ID_KPI, COLORS);
            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet UpdateLastFiltroIndicador(int ID_KPI, string FILTRO)
        {
            ConexionBD conexion;
            string consulta = string.Format("EXEC spUpdateLastFiltro {0},'{1}'", ID_KPI, FILTRO);
            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        public DataSet getImagenes(string[] FILTRO)
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectNMT_IMAGES " + FILTRO[0];

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        public DataSet getImagenesTotal()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectNMT_IMAGES_Total ";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet SelectSkins()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectSkins";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet SelectNMT_TMP_FILES()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectNMT_TMP_FILES";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet SelectNMT_TMP_CAT_FILES()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectNMT_TMP_CAT_FILES";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        

        /// <summary>
        /// Homologacion archivos kpi
        /// </summary>
        /// <returns></returns>
        public DataSet SelectHomologacion_FILES(string nombreArchivo)
        {
            ConexionBD conexion;
            string consulta = string.Format("EXEC spSelectHomologacionKPI '{0}'",nombreArchivo);

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Homologacion archivos catalogos
        /// </summary>
        /// <returns></returns>
        public DataSet SelectHomologacion_Catalogo(string nombreArchivo)
        {
            ConexionBD conexion;
            string consulta = string.Format("EXEC spSelectHomologacionCatalogo '{0}'", nombreArchivo);

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet SelectNMT_CURRENT_FILES()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectNMT_CURRENT_FILES";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet SelectNMT_CURRENT_CAT_FILES()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectNMT_CURRENT_CAT_FILES";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// insert a NMT_TMP_FILES
        /// </summary>
        public void InsertNMT_TMP_FILES(string FULL_FILENAME ,string FILENAME_KPI ,string FILENAME_POSITIONCODE,long FILENAME_DATE)
        {            
            ConexionBD conexion;
            string consulta = String.Format("EXEC spInsertNMT_TMP_FILES '{0}','{1}','{2}',{3}",FULL_FILENAME ,FILENAME_KPI ,FILENAME_POSITIONCODE, FILENAME_DATE);
            try
            {
                conexion = new ConexionBD();
                conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// insert a NMT_TMP_CAT_FILES
        /// </summary>
        public void InsertNMT_TMP_CAT_FILES(string FULL_FILENAME, string CATALOG_NAME, long FILENAME_DATE)
        {
            ConexionBD conexion;
            string consulta = String.Format("EXEC spInsertNMT_TMP_CAT_FILES '{0}','{1}',{2}", FULL_FILENAME, CATALOG_NAME, FILENAME_DATE);
            try
            {
                conexion = new ConexionBD();
                conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        
        /// <summary>
        /// Execute query a NMT_TMP_FILES
        /// </summary>
        public void ExecuteSQLNMT_TMP_FILES(string Tabla, string columnas, string valores,bool local)
        {
            ConexionBD conexion;
            string consulta = String.Format("EXEC spExecuteSQLTableKPITemp '{0}','{1}','{2}'", Tabla, columnas, valores.Replace("'", "''''"));
            try
            {
                conexion = new ConexionBD();
                if (local)
                conexion.getDataSet(consulta);
                else
                conexion.getDataSetBitacora(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Execute query a NMT_TMP_FILES
        /// </summary>
        public void ExecuteSQLNMT_CAT_TMP_FILES(string Tabla, string columnas, string valores, bool local)
        {
            ConexionBD conexion;
            string consulta = String.Format("EXEC spExecuteSQLTableKPITemp '{0}','{1}','{2}'", Tabla, columnas, valores.Replace("'", "''''").Replace("NULL", "").Replace("Null", "").Replace("null", ""));
            try
            {
                conexion = new ConexionBD();
                if (local)
                    conexion.getDataSet(consulta);
                else
                    conexion.getDataSetBitacora(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Habilitar inserción a tabla temporal
        /// </summary>
        public void HabilitarInsertTemp(bool local)
        {
            ConexionBD conexion;
            string consulta = String.Format("EXEC spHabilitarInsertTemp");
            try
            {
                conexion = new ConexionBD();
                if (local)
                    conexion.getDataSet(consulta);
                else
                    conexion.getDataSetBitacora(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Habilitar inserción a tabla temporal
        /// </summary>
        public void NoHabilitarInsertTemp(bool local)
        {
            ConexionBD conexion;
            string consulta = String.Format("EXEC spNoHabilitarInsertTemp");
            try
            {
                conexion = new ConexionBD();
                if (local)
                    conexion.getDataSet(consulta);
                else
                    conexion.getDataSetBitacora(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Execute query a truncate
        /// </summary>
        public void Truncate_NMT_Table(string Tabla,bool local)
        {
            ConexionBD conexion;
            string consulta = String.Format("EXEC spTruncateTable '{0}'", Tabla);
            try
            {
                conexion = new ConexionBD();
                if (local)
                    conexion.getDataSet(consulta);
                else
                    conexion.getDataSetBitacora(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        
        /// <summary>
        /// Execute query a NMT_TMP_FILES
        /// </summary>
        public void Drop_NMT_Table(string tabla,bool local)
        {
            ConexionBD conexion;
            string consulta = String.Format("EXEC spDropTableTMP '{0}'", tabla);
            try
            {
                conexion = new ConexionBD();
                if(local)
                conexion.getDataSet(consulta);
                else
                conexion.getDataSetBitacora(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }



        /// <summary>
        /// Execute query a NMT_TMP_FILES
        /// </summary>
        public void Merge_NMT_Table(string tablaFuente,string TablaDestino, bool local)
        {
            ConexionBD conexion;
            string consulta = String.Format("EXEC spMergeTable '{0}','{1}'", tablaFuente, TablaDestino);
            try
            {
                conexion = new ConexionBD();
                if(local)
                    conexion.getDataSet(consulta);
                else
                    conexion.getDataSetBitacora(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Execute query a NMT_TMP_FILES
        /// </summary>
        public DataSet CreateTableKPITemp(string NombreTabla)
        {
            ConexionBD conexion;
            string consulta = String.Format("EXEC [spCreateTableKPITemp] '{0}'",NombreTabla);
            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// Execute query a NMT_TMP_FILES
        /// </summary>
        public DataSet CreateTableCatalogoTemp(string NombreTabla,bool local)
        {
            ConexionBD conexion;
            string consulta = String.Format("EXEC [spCreateTableKPITemp] '{0}'", NombreTabla);
            try
            {
                conexion = new ConexionBD();
                if(local)
                return conexion.getDataSet(consulta);
                else
                return conexion.getDataSetBitacora(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// delete a NMT_TMP_FILES
        /// </summary>
        public void DeleteNMT_TMP_FILES(string FULL_FILENAME)
        {
            ConexionBD conexion;
            string consulta = String.Format("EXEC spDeleteNMT_TMP_FILES '{0}'", FULL_FILENAME);
            try
            {
                conexion = new ConexionBD();
                conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// delete a NMT_TMP_CAT_FILES
        /// </summary>
        public void DeleteNMT_TMP_CAT_FILES(string FULL_FILENAME)
        {
            ConexionBD conexion;
            string consulta = String.Format("EXEC spDeleteNMT_TMP_CAT_FILES '{0}'", FULL_FILENAME);
            try
            {
                conexion = new ConexionBD();
                conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }


        /// <summary>
        /// insert a NMT_TMP_FILES
        /// </summary>
        public void InsertNMT_CURRENT_FILES(string FULL_FILENAME, string FILENAME_KPI, string FILENAME_POSITIONCODE, long FILENAME_DATE)
        {
            ConexionBD conexion;
            string consulta = String.Format("EXEC spInsertNMT_CURRENT_FILES '{0}','{1}','{2}',{3}", FULL_FILENAME, FILENAME_KPI, FILENAME_POSITIONCODE, FILENAME_DATE);
            try
            {
                conexion = new ConexionBD();
                conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// insert a NMT_TMP_FILES
        /// </summary>
        public void InsertNMT_CURRENT_CAT_FILES(string FULL_FILENAME, string CATALOG_NAME, long FILENAME_DATE)
        {
            ConexionBD conexion;
            string consulta = String.Format("EXEC spInsertNMT_CURRENT_CAT_FILES '{0}','{1}',{2}", FULL_FILENAME, CATALOG_NAME, FILENAME_DATE);
            try
            {
                conexion = new ConexionBD();
                conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// UPDATE a NMT_TMP_FILES
        /// </summary>
        public void SetLoadedNMT_CURRENT_FILES(string FULL_FILENAME)
        {
            ConexionBD conexion;
            string consulta = String.Format("EXEC spSetLoadedNMT_CURRENT_FILES '{0}'", FULL_FILENAME);
            try
            {
                conexion = new ConexionBD();
                conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// UPDATE a NMT_TMP_CAT_FILES
        /// </summary>
        public void SetLoadedNMT_CURRENT_CAT_FILES(string FULL_FILENAME)
        {
            ConexionBD conexion;
            string consulta = String.Format("EXEC spSetLoadedNMT_CURRENT_CAT_FILES '{0}'", FULL_FILENAME);
            try
            {
                conexion = new ConexionBD();
                conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// COMPARE a NMT_TMP_FILES
        /// </summary>
        public void CompareNMT_TMP_FILES()
        {
            ConexionBD conexion;
            string consulta = String.Format("EXEC spCompararNMT_TMP_FILES" );
            try
            {
                conexion = new ConexionBD();
                conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// COMPARE a NMT_TMP_FILES
        /// </summary>
        public void CompareNMT_TMP_CAT_FILES()
        {
            ConexionBD conexion;
            string consulta = String.Format("EXEC spCompararNMT_TMP_CAT_FILES");
            try
            {
                conexion = new ConexionBD();
                conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        // -------------------------   INI - BZG - 20120227 ------------------------------------
        /// <summary>
        /// Execute query a NMT_CURRENT_FILES para validar y limpiar informacion cuando se cambia de territorio.
        /// </summary>
        public DataSet DeleteNMT_CURRENT_FILES()
        {
            ConexionBD conexion;
            string consulta = "EXEC spCompararTerritorioActivo";
            try 
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        // -------------------------   FIN - BZG - 20120227 ------------------------------------
        #endregion


        #region bitacora
        //--------------------------------------------
        /// <summary>
        /// OBTENIENDO LOS REGISTROS DE BITACORA DIARIOS
        /// </summary>
        /// <param name="territorio"></param>
        /// <returns></returns>
        public DataSet SelectRegBitacora(long fecha)
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectBitacora " + fecha + "";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// OBTENIENDO LOS REGISTROS DE BITACORA PARA CARGAR
        /// </summary>
        /// <param name="territorio"></param>
        /// <returns></returns>
        public DataSet SelectRegBitacoraToLoad()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectBitacoraToLoad ";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// ACTUALIZA REGISTROS DE BITACORA COMO CARGADOS
        /// </summary>
        /// <param name="territorio"></param>
        /// <returns></returns>
        public void UpdateBitacoraToLoad(long fecha)
        {
            ConexionBD conexion;
            string consulta = "EXEC spUpdateBitacoraToLoad "+fecha;

            try
            {
                conexion = new ConexionBD();
                conexion.executeBitacora(consulta,true);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }
        
        
        /// <summary>
        /// OBTENIENDO LA VERISON DE LA ULTIMA BITACORA
        /// </summary>
        /// <param name="territorio"></param>
        /// <returns></returns>
        public DataSet getVersionBitacora(string territorio)
        {
            ConexionBD conexion = null;            
            string consulta = "EXEC spSelectVersion '"+territorio+"'";

            try
            {
                //Obteniendo informacion local
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                //Expected Error
                //throw (new Exception(Error.ToString()));
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet getVersionAPP()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectVersionAPP_MDZ ";

            try
            {
                //Obteniendo informacion remota
                conexion = new ConexionBD();
                return conexion.getDataSetBitacora(consulta);
            }
            catch (Exception Error)
            {
                //expected error por falta de conexion.
                throw (new Exception(Error.ToString()));
            }
        }

        /// <summary>
        /// insert a Bitacora
        /// </summary>
        public void InsertVitacora(string USUARIO , string USUARIO_PC , DateTime FECHA_SINCRONIZACION ,string VERSION_CLIENTE, int DURACION, string KPI ,
        string TERRITORIO , long FECHA_SINC_INT, bool local )
        {
            string a = "";
            if(local!=false)
                a="ff";
            ConexionBD conexion;
            string consulta = String.Format("EXEC spInsertBitacora_MDZ '{0}','{1}','{2}','{3}',{4},'{5}','{6}',{7}", USUARIO , USUARIO_PC , FECHA_SINCRONIZACION.ToString("yyyyMMdd HH:mm:ss") ,VERSION_CLIENTE, DURACION, KPI ,
                                            TERRITORIO , FECHA_SINC_INT);
            try
            {
                conexion = new ConexionBD();                
                conexion.executeBitacora(consulta,local);                

            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }

        #endregion


        /// <summary>
        /// Autor : Belizario Zapien Garcia
        /// </summary>
        /// <returns></returns>
        public DataSet SelectNMT_KPI_Temporal()
        {
            ConexionBD conexion;
            string consulta = "EXEC spSelectFileNameKPI";

            try
            {
                conexion = new ConexionBD();
                return conexion.getDataSet(consulta);
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }
        }/////////////////////////////////////////////////////////////


    } // Clase

} // namespace
