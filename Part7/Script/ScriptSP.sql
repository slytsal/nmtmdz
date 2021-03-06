CREATE PROCEDURE [dbo].[spTruncateTable]
@Tabla NVARCHAR(100)
AS
BEGIN
	DECLARE @SQLQuery AS NVARCHAR(4000);
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
    SET @SQLQuery = 'IF EXISTS(SELECT name FROM sys.tables WHERE name = '+''''+@Tabla+''+''''+') BEGIN TRUNCATE TABLE '+@Tabla+' END';
	EXECUTE(@SQLQuery);
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_FrecuenciaVisitas]
@IndustryLevel VARCHAR(MAX)
AS
BEGIN
	DECLARE @SQLQuery AS NVARCHAR(MAX);
	DECLARE @In1 AS NVARCHAR(MAX);
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET	@In1= ''''+REPLACE(@IndustryLevel,',',''''+','+'''')+'''';	
    SET @SQLQuery =
    'SELECT
		PositionCode,
		TimeId,
		IndustryLevel,
		FreqVisits,
		LevelFlag
	FROM
		KPI_FrecuenciaVisitas
	WHERE
		IndustryLevel IN ('+ @In1 +')
	ORDER BY 
		IndustryLevel,TimeId'
	EXECUTE(@SQLQuery);
	
END
|

CREATE PROCEDURE [dbo].[spCreateTableKPITemp]
@nombreTabla varchar(100)
AS
BEGIN	
	DECLARE @SQLQuery AS NVARCHAR(4000);
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET @SQLQuery = 'IF EXISTS(SELECT name FROM sys.tables WHERE name = '+''''+@nombreTabla+'_TMP'+''''+') BEGIN DROP TABLE '+@nombreTabla+'_TMP END';
	EXECUTE(@SQLQuery);
	SET @SQLQuery =
    'SELECT * INTO '+@nombreTabla +'_TMP FROM '+@nombreTabla;
	EXECUTE(@SQLQuery);
	SET @SQLQuery =
    'DELETE FROM '+@nombreTabla+'_TMP';
	EXECUTE(@SQLQuery);
	SET @SQLQuery =
    'sp_columns '+@nombreTabla+'_TMP';
	EXECUTE(@SQLQuery);
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_Manejos]
@IndustryLevel varchar(50),
@BrandDescription varchar(MAX)
AS
BEGIN
	DECLARE @SQLQuery AS NVARCHAR(MAX);
	DECLARE @In1 AS NVARCHAR(MAX);
	DECLARE @In2 AS NVARCHAR(MAX);
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SET	@In1= ''''+REPLACE(@BrandDescription,',',''''+','+'''')+'''';	
    SET @SQLQuery =
    'SELECT
		PositionCode,
		TimeId,
		IndustryLevel,
		BrandCode,
		BrandDescription,
		NbrHandling,
		LevelFlag
	FROM
		KPI_Manejos
	WHERE 
		IndustryLevel LIKE '+''''+@IndustryLevel+''''+'
		AND BrandDescription IN ('+ @In1 +')
	ORDER BY
		BrandDescription,TimeId'
	EXECUTE(@SQLQuery);
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_MDZ_EFFECTIVE_DROP_SIZE]
@IndustryLevel varchar(MAX)
AS
BEGIN
	DECLARE @SQLQuery AS NVARCHAR(MAX);
	DECLARE @In1 AS NVARCHAR(MAX);
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET	@In1= ''''+REPLACE(@IndustryLevel,',',''''+','+'''')+'''';	
    SET @SQLQuery =
    'SELECT
		PositionCode,
		TimeId,
		IndustryLevel,		
		EffectiveDropSize,
		LevelFlag
	FROM
		KPI_MDZ_EFFECTIVE_DROP_SIZE
	WHERE
		IndustryLevel IN ('+ @In1 +')
	ORDER BY 
		IndustryLevel,TimeId'
	EXECUTE(@SQLQuery);
	
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_MDZ_GENERAL_DROP_SIZE]
@IndustryLevel varchar(MAX)
AS
BEGIN
	DECLARE @SQLQuery AS NVARCHAR(MAX);
	DECLARE @In1 AS NVARCHAR(MAX);
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET	@In1= ''''+REPLACE(@IndustryLevel,',',''''+','+'''')+'''';	
    SET @SQLQuery =
    'SELECT
		PositionCode,
		TimeId,
		IndustryLevel,		
		GeneralDropSize,
		LevelFlag
	FROM
		KPI_MDZ_GENERAL_DROP_SIZE
	WHERE
		IndustryLevel IN ('+ @In1 +')
	ORDER BY 
		IndustryLevel, TimeId'
	EXECUTE(@SQLQuery);
	
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_MDZ_SKU_Average]
@IndustryLevel varchar(MAX)
AS
BEGIN
	DECLARE @SQLQuery AS NVARCHAR(MAX);
	DECLARE @In1 AS NVARCHAR(MAX);
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET	@In1= ''''+REPLACE(@IndustryLevel,',',''''+','+'''')+'''';	
    SET @SQLQuery =
    'SELECT
		PositionCode,
		TimeId,
		IndustryLevel,		
		SKUAvg,
		LevelFlag
	FROM
		KPI_MDZ_SKU_Average
	WHERE
		IndustryLevel IN ('+ @In1 +')
	ORDER BY 
		IndustryLevel, TimeId'
	EXECUTE(@SQLQuery);
	
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_Share_PMM_SellOut]
@ChainCode varchar(50),
@ManufacturerCode varchar(MAX)
AS
BEGIN
	DECLARE @SQLQuery AS NVARCHAR(MAX);
	DECLARE @In1 AS NVARCHAR(MAX);
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SET	@In1= ''''+REPLACE(@ManufacturerCode,',',''''+','+'''')+'''';	
    SET @SQLQuery =
    'SELECT
		PositionCode,
		TimeId,
		ChainCode,
		ManufacturerCode,
		SOM,
		LevelFlag
	FROM
		KPI_Share_PMM_SellOut
	WHERE 
		ChainCode LIKE '+''''+@ChainCode+''''+'
		AND ManufacturerCode IN ('+ @In1 +')
	ORDER BY
		ManufacturerCode,TimeId'
	EXECUTE(@SQLQuery);
	
END
|
CREATE PROCEDURE [dbo].[spSelectKPI_Share_Segment]
 @IndustryLevel varchar(MAX)
AS
BEGIN
	DECLARE @SQLQuery AS NVARCHAR(MAX);
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
    SET @SQLQuery =
    'SELECT
		PositionCode,
		TimeId,
		IndustryLevel,
		SegmentPrice,
		SOM,
		LevelFlag
	FROM
		KPI_Share_Segment
	WHERE
		IndustryLevel LIKE '+''''+@IndustryLevel+''''+'		 
	ORDER BY		
		SegmentPrice, TimeId'
	EXECUTE(@SQLQuery);	
END

|

CREATE PROCEDURE [dbo].[spSelectKPI_Share_SKU]
@ProductSubfamilyCode VARCHAR(MAX),
@ProductSubfamilyDescription VARCHAR(MAX),
@SegmentPrice VARCHAR(50)

AS
BEGIN
	DECLARE @SQLQuery AS NVARCHAR(MAX);
	DECLARE @In1 AS NVARCHAR(MAX);
	DECLARE @In2 AS NVARCHAR(MAX);
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET	@In1= ''''+REPLACE(@ProductSubfamilyCode,',',''''+','+'''')+'''';
	SET @In2= ''''+REPLACE(@ProductSubfamilyDescription,',',''''+','+'''')+'''';

   SET @SQLQuery =
     '	SELECT PositionCode,TimeId,ProductSubfamilyCode,ProductSubfamilyDescription,SegmentPrice,SOM,LevelFlag
		FROM
		KPI_Share_SKU
		WHERE
			ProductSubfamilyCode IN ('+ @In1 +')
		AND ProductSubfamilyDescription IN ('+ @In2 +')
		AND SegmentPrice LIKE '+''''+@SegmentPrice+'''' +'
	ORDER BY
		ProductSubfamilyDescription,TimeId';
		EXECUTE(@SQLQuery);
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_Sobreindexados]
@IndustryLevel varchar(50),
@ProductSubfamilyCode varchar(MAX),
@ProductSubfamilyDescription varchar(MAX)	
AS
BEGIN
	DECLARE @SQLQuery AS NVARCHAR(MAX);
	DECLARE @In1 AS NVARCHAR(MAX);
	DECLARE @In2 AS NVARCHAR(MAX);
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET	@In1= ''''+REPLACE(@ProductSubfamilyCode,',',''''+','+'''')+'''';
	SET @In2= ''''+REPLACE(@ProductSubfamilyDescription,',',''''+','+'''')+'''';
    SET @SQLQuery =
    'SELECT
		
		TimeId,
		Count(CustomerCode) as CustomerCode,
		ProductSubfamilyDescription		
	FROM
		KPI_Sobreindexados
	WHERE
		IndustryLevel LIKE '+''''+@IndustryLevel+''''+'
		AND ProductSubfamilyCode IN ('+ @In1 +')
		AND ProductSubfamilyDescription IN ('+ @In2 +')
	GROUP BY TimeId,ProductSubfamilyDescription
	ORDER BY 	ProductSubfamilyDescription,TimeId'
	
	EXECUTE(@SQLQuery);
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_SOM] 
@ManufacturerCode VARCHAR(MAX),
@SegmentPrice VARCHAR(50)
AS
BEGIN
	DECLARE @SQLQuery AS NVARCHAR(MAX);
	DECLARE @In1 AS NVARCHAR(MAX);
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET	@In1= ''''+REPLACE(@ManufacturerCode,',',''''+','+'''')+'''';	
    SET @SQLQuery =
    'SELECT 
		PositionCode,
		TimeId,
		ManufacturerCode,
		SegmentPrice,
		SOM,
		LevelFlag
	FROM 
		KPI_SOM
	WHERE
		ManufacturerCode IN ('+ @In1 +')
		AND SegmentPrice LIKE '+''''+@SegmentPrice+''''+'
	ORDER BY
		ManufacturerCode,TimeId'
	EXECUTE(@SQLQuery);
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_Sobreindexados_Resumen]
@IndustryLevel varchar(50),
@ProductSubfamilyCode nvarchar(MAX),
@ProductSubfamilyDescription nvarchar(MAX),
@TimeId varchar(50)
AS
BEGIN
	DECLARE @SQLQuery AS NVARCHAR(MAX);
	DECLARE @In1 AS NVARCHAR(MAX);
	DECLARE @In2 AS NVARCHAR(MAX);
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SET	@In1= ''''+REPLACE(@ProductSubfamilyCode,',',''''+','+'''')+'''';
	SET @In2= ''''+REPLACE(@ProductSubfamilyDescription,',',''''+','+'''')+'''';
    SET @SQLQuery =
    'SELECT
		PositionCode,
		TimeId,
		CustomerCode,
		CustomerName,
		CustomerAddress,
		IndustryLevel,
		SOMBrandPos,
		SOMBrandPosition,
		ProductSubfamilyCode,
		ProductSubfamilyDescription,
		LevelFlag		
	FROM
		KPI_Sobreindexados
	WHERE
		IndustryLevel LIKE '+''''+@IndustryLevel+''''+'
		
		AND ProductSubfamilyDescription IN ('+ @In2 +')
		AND TimeId = '+ @TimeId+' 
		ORDER BY TimeId'
	
	EXECUTE(@SQLQuery);
END
|

CREATE PROCEDURE [dbo].[spAutenticacionSMS] 
@nombreBD varchar(20)
AS
BEGIN
	DECLARE @SQLQuery AS NVARCHAR(MAX);
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SET @SQLQuery ='SELECT 
		LTRIM(RTRIM(UA.UserAccountCode)) UserAccountCode,
		PO.PositionCode,
		UA.UserAccountPassword
	FROM 
		['+@nombreBD+'].[dbo].SiteInfo AS SI
			INNER JOIN ['+@nombreBD+'].[dbo].DDMSitePosition AS DDM 
				ON DDM.DDMSitePosition_DDMSiteId = SI.SiteInfo_DDMSiteId
			INNER JOIN ['+@nombreBD+'].[dbo].Position AS PO 
				ON DDM.DDMSitePosition_PositionId = PO.PositionId
			INNER JOIN ['+@nombreBD+'].[dbo].Employee AS EM
				ON PO.Position_EmployeeId = EM.EmployeeId
			INNER JOIN ['+@nombreBD+'].[dbo].UserAccount AS UA
				ON EM.Employee_UserAccountId = UA.UserAccountId '
	EXECUTE(@SQLQuery);				
END
|

CREATE PROCEDURE [dbo].[spSelectVersionAPP]	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT MAX([UPDATE_VERSION]) AS UPDATE_VERSION
	FROM [dbo].[NMT_UPDATESYS]

END
|

CREATE PROCEDURE [dbo].[spExecuteSQLTableKPITemp]
@Tabla NVARCHAR(100),
@Columnas NVARCHAR(MAX),
@Valores NVARCHAR(MAX)
AS
BEGIN
	DECLARE @SQLQuery AS NVARCHAR(MAX);
	DECLARE @SQLValues AS NVARCHAR(MAX);
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET @SQLValues = replace(@Valores,'"','''');
	SET @SQLQuery =
    'INSERT INTO '+@Tabla +'_TMP ('+@Columnas+') VALUES ('+@SQLValues+')';
	EXECUTE(@SQLQuery);
END
|

CREATE PROCEDURE [dbo].[spDropTableTMP]
@nombreTabla NVARCHAR(100)
AS
BEGIN
	DECLARE @SQLQuery AS NVARCHAR(MAX);
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    SET @SQLQuery = 'IF EXISTS(SELECT name FROM sys.tables WHERE name = '+''''+@nombreTabla+''+''''+') BEGIN DROP TABLE '+@nombreTabla+' END';
	EXECUTE(@SQLQuery);

	
END
|

CREATE PROCEDURE [dbo].[spMergeTable]
@TablaOrigen NVARCHAR(100),
@TablaDestino NVARCHAR(100)
AS
BEGIN
	DECLARE @SQLQuery AS NVARCHAR(4000);
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    SET @SQLQuery =
    'INSERT INTO '+@TablaDestino +' SELECT * FROM '+@TablaOrigen ;
	EXECUTE(@SQLQuery);

	
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_Agotamiento]
@IndustryLevel VARCHAR(50),
@BrandDescription VARCHAR(MAX)	
AS
BEGIN
	DECLARE @SQLQuery AS NVARCHAR(MAX);
	DECLARE @In1 AS NVARCHAR(MAX);
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SET	@In1= ''''+REPLACE(@BrandDescription,',',''''+','+'''')+'''';	
    SET @SQLQuery =
    'SELECT
		PositionCode,
		TimeId,
		IndustryLevel,
		BrandCode,
		BrandDescription,
		NbrOOS,
		LevelFlag
	FROM
		KPI_Agotamiento
	WHERE
		IndustryLevel LIKE '+''''+@IndustryLevel+''''+'
		AND BrandDescription IN ('+ @In1 +')
	ORDER BY
		BrandDescription,TimeId'
	EXECUTE(@SQLQuery);	
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_EfectividadMDZ]
@ProductSubfamilyCode VARCHAR(MAX),
@ProductSubfamilyDescription VARCHAR(MAX),
@IndustryLevel VARCHAR(50)
AS
BEGIN
	DECLARE @SQLQuery AS NVARCHAR(MAX);
	DECLARE @In1 AS NVARCHAR(MAX);
	DECLARE @In2 AS NVARCHAR(MAX);
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET	@In1= ''''+REPLACE(@ProductSubfamilyCode,',',''''+','+'''')+'''';
	SET @In2= ''''+REPLACE(@ProductSubfamilyDescription,',',''''+','+'''')+'''';
    SET @SQLQuery =
    'SELECT
		PositionCode,
		TimeId,
		ProductSubfamilyCode,
		ProductSubfamilyDescription,
		EfectivenessByBrand,
		LevelFlag
	FROM
		KPI_EfectividadMDZ
	WHERE
		ProductSubfamilyCode IN ('+ @In1 +')
		AND ProductSubfamilyDescription IN ('+ @In2 +')
		AND IndustryLevel LIKE '+''''+@IndustryLevel+''''+'
	ORDER BY 
		ProductSubfamilyDescription,TimeId'
	
	EXECUTE(@SQLQuery);
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_Clientes_ITO_UX_Flt_IndustryLevel]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT		
		IndustryLevel
	FROM
		KPI_Clientes_ITO_UX
	GROUP BY
		IndustryLevel
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_Clientes_ITO_UX]
@IndustryLevel varchar(50)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    SELECT
		
		TimeId,		
		SUM(NbrCustUX) NbrCustUX,
		SUM(NbrCustITO) NbrCustITO
	FROM
		KPI_Clientes_ITO_UX
	WHERE
		IndustryLevel LIKE @IndustryLevel
	GROUP BY
		TimeId
	ORDER BY TimeId	
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_Agotamiento_Flt_IndustryLevel]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		
		IndustryLevel
	FROM
		KPI_Agotamiento
	GROUP BY
		IndustryLevel
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_Agotamiento_Flt_BrandDescription]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    SELECT
		
		BrandDescription+'&'+BrandCode BrandDescription
	FROM
		KPI_Agotamiento
	GROUP BY
		BrandDescription,BrandCode
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_Agotamiento_Flt_BrandCode]	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    SELECT
		
		BrandCode+'&'+BrandDescription BrandCode
	FROM
		KPI_Agotamiento
	GROUP BY
		BrandCode,BrandDescription
END
|

CREATE PROCEDURE [dbo].[spSelectHomologacionKPI]
@strNombreArchivo VARCHAR(100)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT [strNombreArchivo]
      ,[strNombreTabla]
  FROM [dbo].[tblNMTHomologacionArchivos]
  WHERE [strNombreArchivo] =@strNombreArchivo



	
END
|

CREATE PROCEDURE [dbo].[spSelectHomologacionCatalogo]
@strNombreArchivo VARCHAR(100)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT [strNombreArchivo]
      ,[strNombreTabla]
  FROM [dbo].[tblNMTHomologacionCatalogos]
  WHERE [strNombreArchivo] =@strNombreArchivo



	
END
|

CREATE PROCEDURE [dbo].[spSelectGroup]	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
		ID_GROUP AS ID,
		GROUP_DSC AS DESCRIPCION,
		GROUP_ABOUT AS ACERCADE,
		IMG_FILE AS RUTA,
		LAST_INDEX,
		LAST_FILTER
	
	FROM
		NMT_GROUP
	
END
|

CREATE PROCEDURE [dbo].[spSelectBitacoraToLoad]	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT [USUARIO]
      ,[USUARIO_PC]
      , convert(varchar, [FECHA_SINCRONIZACION], 120) [FECHA_SINCRONIZACION]
      ,[VERSION_CLIENTE]
      ,[DURACION]
      ,[KPI]
      ,[TERRITORIO]
      ,[FECHA_SINC_INT]
	FROM [dbo].[BITACORA_SINC]
	WHERE CARGADO = 0 


	
END
|

CREATE PROCEDURE [dbo].[spSelectBitacora]	
    @FECHA_SINC_INT bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT [USUARIO]
      ,[USUARIO_PC]
      , convert(varchar, [FECHA_SINCRONIZACION], 9) [FECHA_SINCRONIZACION]
      ,[VERSION_CLIENTE]
      ,[DURACION]
      ,[KPI]
      ,[TERRITORIO]
      ,[FECHA_SINC_INT]
	FROM [dbo].[BITACORA_SINC]
	WHERE [FECHA_SINC_INT]>@FECHA_SINC_INT


	
END
|

CREATE PROCEDURE [dbo].[spInsertNMT_TMP_FILES]
@FULL_FILENAME varchar(200),
@FILENAME_KPI varchar(150),
@FILENAME_POSITIONCODE varchar(50),
@FILENAME_DATE bigint
AS
BEGIN
	INSERT INTO [dbo].[NMT_TMP_FILES]
           ([FULL_FILENAME]
           ,[FILENAME_KPI]
           ,[FILENAME_POSITIONCODE]
           ,[FILENAME_DATE])
     VALUES
           (@FULL_FILENAME,
			@FILENAME_KPI,
            @FILENAME_POSITIONCODE,
            @FILENAME_DATE)

END
|

CREATE PROCEDURE [dbo].[spInsertNMT_TMP_CAT_FILES]
@FULL_FILENAME varchar(200),
@CATALOG_NAME varchar(150),
@FILENAME_DATE bigint
AS
BEGIN
	INSERT INTO [dbo].[NMT_TMP_CAT_FILES]
           ([FULL_FILENAME]
           ,[CATALOG_NAME]
           ,[FILENAME_DATE])
     VALUES
           (@FULL_FILENAME,
			@CATALOG_NAME,
            @FILENAME_DATE)
END
|

CREATE PROCEDURE [dbo].[spInsertNMT_CURRENT_FILES]
@FULL_FILENAME varchar(200),
@FILENAME_KPI varchar(150),
@FILENAME_POSITIONCODE varchar(50),
@FILENAME_DATE bigint
AS
BEGIN
	INSERT INTO [dbo].[NMT_CURRENT_FILES]
           ([FULL_FILENAME]
           ,[FILENAME_KPI]
           ,[FILENAME_POSITIONCODE]
           ,[FILENAME_DATE]
           ,[DB_LOADED])
     VALUES
           (@FULL_FILENAME,
			@FILENAME_KPI,
            @FILENAME_POSITIONCODE,
            @FILENAME_DATE,
            0);
     DELETE FROM [dbo].[NMT_TMP_FILES]
     WHERE [FULL_FILENAME]=@FULL_FILENAME;
END
|

CREATE PROCEDURE [dbo].[spInsertNMT_CURRENT_CAT_FILES]
@FULL_FILENAME varchar(200),
@CATALOG_NAME varchar(150),
@FILENAME_DATE bigint
AS
BEGIN
	INSERT INTO [dbo].[NMT_CURRENT_CAT_FILES]
           ([FULL_FILENAME]
           ,[CATALOG_NAME]
           ,[FILENAME_DATE]
           ,[DB_LOADED])
     VALUES
           (@FULL_FILENAME,
			@CATALOG_NAME,
            @FILENAME_DATE,
            0);
     DELETE FROM [dbo].[NMT_TMP_CAT_FILES]
     WHERE [FULL_FILENAME]=@FULL_FILENAME;
END
|

CREATE PROCEDURE [dbo].[spInsertBitacora]
	@USUARIO nvarchar(250),
    @USUARIO_PC nvarchar(250),
    @FECHA_SINCRONIZACION datetime,
    @VERSION_CLIENTE nvarchar(15),
    @DURACION int,
    @KPI nvarchar(250),
    @TERRITORIO nvarchar(50),
    @FECHA_SINC_INT bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO [dbo].[BITACORA_SINC]
           ([USUARIO]
           ,[USUARIO_PC]
           ,[FECHA_SINCRONIZACION]
           ,[VERSION_CLIENTE]
           ,[DURACION]
           ,[KPI]
           ,[TERRITORIO]
           ,[FECHA_SINC_INT])
     VALUES
           (@USUARIO
           ,@USUARIO_PC
           ,@FECHA_SINCRONIZACION
           ,@VERSION_CLIENTE
           ,@DURACION
           ,@KPI
           ,@TERRITORIO
           ,@FECHA_SINC_INT)	
END
|

CREATE PROCEDURE [dbo].[spDeleteNMT_TMP_FILES]
@FULL_FILENAME varchar(200)
AS
BEGIN
	 DELETE FROM [dbo].[NMT_TMP_FILES]
     WHERE [FULL_FILENAME]=@FULL_FILENAME;
END
|

CREATE PROCEDURE [dbo].[spDeleteNMT_TMP_CAT_FILES]
@FULL_FILENAME varchar(200)
AS
BEGIN
	 DELETE FROM [dbo].[NMT_TMP_CAT_FILES]
     WHERE [FULL_FILENAME]=@FULL_FILENAME;
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_Sobreindexados_Flt_ProductSubfamilyDescription]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		
		ProductSubfamilyDescription+'&'+ProductSubfamilyCode ProductSubfamilyDescription
		
	FROM
		KPI_Sobreindexados
	GROUP BY 
		ProductSubfamilyDescription,ProductSubfamilyCode	
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_Sobreindexados_Flt_ProductSubfamilyCode]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		
		ProductSubfamilyCode+'&'+ProductSubfamilyDescription ProductSubfamilyCode
		
	FROM
		KPI_Sobreindexados
	GROUP BY 
		ProductSubfamilyCode,ProductSubfamilyDescription	
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_Sobreindexados_Flt_IndustryLevel]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		
		IndustryLevel
		
	FROM
		KPI_Sobreindexados
	GROUP BY 
		IndustryLevel	
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_SKU_Average_Flt_SegmentPrice]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT		
		SegmentPrice
	FROM
		KPI_SKU_Average
	GROUP BY
		SegmentPrice
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_SKU_Average]
@SegmentPrice VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		PositionCode,
		TimeId,
		IndustryLevel,
		SegmentPrice,
		SKUAvg,
		LevelFlag
	FROM
		KPI_SKU_Average
	WHERE
		SegmentPrice LIKE @SegmentPrice
	ORDER BY 
		IndustryLevel,TimeId
	
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_Share_SKU_Flt_SegmentPrice]	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT		
		SegmentPrice		
	FROM
		KPI_Share_SKU
	GROUP BY
		SegmentPrice
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_Share_SKU_Flt_ProductSubfamilyDescription]	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT		
		ProductSubfamilyDescription+'&'+ProductSubfamilyCode		ProductSubfamilyDescription
	FROM
		KPI_Share_SKU
	GROUP BY
		ProductSubfamilyDescription,ProductSubfamilyCode
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_Share_SKU_Flt_ProductSubfamilyCode]	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT		
		ProductSubfamilyCode+'&'+ProductSubfamilyDescription	ProductSubfamilyCode
	FROM
		KPI_Share_SKU
	GROUP BY
		ProductSubfamilyCode,ProductSubfamilyDescription
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_Share_Segment_Flt_SegmentPrice]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT		
		SegmentPrice
	FROM
		KPI_Share_Segment
	GROUP BY 
		SegmentPrice
	
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_Share_Segment_Flt_IndustryLevel]	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT		
		IndustryLevel
	FROM
		KPI_Share_Segment
	GROUP BY 
		IndustryLevel
	
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_Share_PMM_SellOut_Flt_ManufacturerCode]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT		
		ManufacturerCode
	FROM
		KPI_Share_PMM_SellOut
	GROUP BY 
		ManufacturerCode
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_Share_PMM_SellOut_Flt_ChainCode]	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT		
		ChainCode
	FROM
		KPI_Share_PMM_SellOut
	GROUP BY 
		ChainCode
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_SELL_OUT_KA_SEGMENT_CONTRIBUTION_Flt_ChainCode]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT ChainCode      
	FROM dbo.KPI_SELL_OUT_KA_SEGMENT_CONTRIBUTION
	GROUP BY ChainCode
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_SELL_OUT_KA_SEGMENT_CONTRIBUTION]
@ChainCode nvarchar(35)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT PositionCode
      ,TimeId
      ,ChainCode
      ,SegmentPrice
      ,SOM
      ,LevelFlag
  FROM dbo.KPI_SELL_OUT_KA_SEGMENT_CONTRIBUTION
  WHERE ChainCode LIKE @ChainCode
  ORDER BY SegmentPrice,TimeId
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_MDZ_SKU_Average_Flt_IndustryLevel]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT		
		IndustryLevel		
	FROM
		KPI_MDZ_SKU_Average
	GROUP BY 
		IndustryLevel
	
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_MDZ_GENERAL_DROP_SIZE_Flt_IndustryLevel]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT		
		IndustryLevel
	FROM
		KPI_MDZ_GENERAL_DROP_SIZE
	GROUP BY 
		IndustryLevel
	
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_MDZ_EFFECTIVE_DROP_SIZE_Flt_IndustryLevel]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		IndustryLevel
	FROM
		KPI_MDZ_EFFECTIVE_DROP_SIZE
	GROUP BY 
		IndustryLevel
	
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_MDZ_Coverage_Flt_IndustryLevel]	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    SELECT		
		IndustryLevel
	FROM
		KPI_MDZ_Coverage
	GROUP BY
		IndustryLevel			
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_MDZ_Coverage]
@IndustryLevel varchar(50)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    SELECT		
		TimeId,		
		NbrCustUX,
		NbrCustITO
	FROM
		KPI_MDZ_Coverage
	WHERE
		IndustryLevel LIKE @IndustryLevel	
	ORDER BY TimeId		
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_Manejos_Flt_IndustryLevel]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT		
		IndustryLevel
	FROM
		KPI_Manejos
	GROUP BY
		IndustryLevel
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_Manejos_Flt_BrandDescription]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT		
		BrandDescription+'&'+BrandCode BrandDescription
	FROM
		KPI_Manejos
	GROUP BY
		BrandDescription,BrandCode
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_Manejos_Flt_BrandCode]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT		
		BrandCode+'&'+BrandDescription BrandCode
	FROM
		KPI_Manejos
	GROUP BY
		BrandCode,BrandDescription
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_FrecuenciaVisitas_Flt_IndustryLevel]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT		
		IndustryLevel
	FROM
		KPI_FrecuenciaVisitas
	GROUP BY 
		IndustryLevel
END
|

CREATE PROCEDURE [dbo].[spCompararNMT_TMP_FILES]
AS
BEGIN
	DELETE 	   
	FROM [dbo].[NMT_TMP_FILES] 
	WHERE [FULL_FILENAME]
	IN(
		SELECT 
		   TMP.[FULL_FILENAME]      
		FROM [dbo].[NMT_TMP_FILES] TMP
		INNER JOIN [dbo].[NMT_CURRENT_FILES] CUR
		ON reverse(left(reverse(TMP.FULL_FILENAME), charindex('\', reverse(TMP.FULL_FILENAME)) -1))
		= reverse(left(reverse(CUR.FULL_FILENAME), charindex('\', reverse(CUR.FULL_FILENAME)) -1))
		AND TMP.FILENAME_DATE <= CUR.FILENAME_DATE
	)
END
|

CREATE PROCEDURE [dbo].[spCompararNMT_TMP_CAT_FILES]
AS
BEGIN
	DELETE 	   
	FROM [dbo].[NMT_TMP_CAT_FILES] 
	WHERE [FULL_FILENAME]
	IN(
		SELECT 
		   TMP.[FULL_FILENAME]      
		FROM [dbo].[NMT_TMP_CAT_FILES] TMP
		INNER JOIN [dbo].[NMT_CURRENT_CAT_FILES] CUR
		ON reverse(left(reverse(TMP.FULL_FILENAME), charindex('\', reverse(TMP.FULL_FILENAME)) -1))
		= reverse(left(reverse(CUR.FULL_FILENAME), charindex('\', reverse(CUR.FULL_FILENAME)) -1))
		AND TMP.FILENAME_DATE <= CUR.FILENAME_DATE
	)
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_Ejecución_Calidad]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		PositionCode,
		PYTD,
		Ciclo_1,
		Ciclo_2,
		Ciclo_3,
		Ciclo_4,
		Ciclo_5,
		Ciclo_6,
		Ciclo_7,
		Ciclo_8,
		Ciclo_9,
		Ciclo_10,
		Ciclo_11,
		Ciclo_12,
		Canal,
		LastCycle
	FROM
		KPI_Ejecución_Calidad
	
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_EfectividadMDZ_Flt_ProductSubfamilyDescription]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT		
		ProductSubfamilyDescription+'&'+ProductSubfamilyCode ProductSubfamilyDescription
	FROM
		KPI_EfectividadMDZ
	GROUP BY 
		ProductSubfamilyDescription,ProductSubfamilyCode
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_EfectividadMDZ_Flt_ProductSubfamilyCode]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT		
		ProductSubfamilyCode+'&'+ProductSubfamilyDescription ProductSubfamilyCode
	FROM
		KPI_EfectividadMDZ
	GROUP BY 
		ProductSubfamilyCode,ProductSubfamilyDescription
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_EfectividadMDZ_Flt_IndustryLevel]	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT		
		IndustryLevel
	FROM
		KPI_EfectividadMDZ
	GROUP BY 
		IndustryLevel
END
|

CREATE PROCEDURE [dbo].[spSetLoadedNMT_CURRENT_FILES]
@FULL_FILENAME varchar(200)
AS
BEGIN
	UPDATE [dbo].[NMT_CURRENT_FILES]           
    SET DB_LOADED = 1
    WHERE  FULL_FILENAME = @FULL_FILENAME			     
END
|

CREATE PROCEDURE [dbo].[spSetLoadedNMT_CURRENT_CAT_FILES]
@FULL_FILENAME varchar(200)
AS
BEGIN
	UPDATE [dbo].[NMT_CURRENT_CAT_FILES]           
    SET DB_LOADED = 1
    WHERE  FULL_FILENAME = @FULL_FILENAME			     
END
|

CREATE PROCEDURE [dbo].[spUpdateUltimoIndicador]
@ID_GROUP int,
@LAST_INDEX int,
@LAST_FILTER nvarchar(4000)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    
    UPDATE dbo.NMT_GROUP SET LAST_INDEX=@LAST_INDEX,LAST_FILTER=@LAST_FILTER   WHERE ID_GROUP=@ID_GROUP

	
END
|

CREATE PROCEDURE [dbo].[spUpdateSkins]
@ID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE [dbo].[NMT_SKIN] SET  CURRENT_ = 0;
    UPDATE [dbo].[NMT_SKIN] SET  CURRENT_ = 1 WHERE ID_SKIN=@ID;

	
END
|

CREATE PROCEDURE [dbo].[spUpdateLastFiltro]
@ID int,
@FILTRO VARCHAR(4000)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    UPDATE dbo.NMT_KPI SET  KPI_FILTRO=@FILTRO WHERE ID_KPI=@ID;   	
END
|

CREATE PROCEDURE [dbo].[spUpdateColors]
@ID int,
@COLORS VARCHAR(4000)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    UPDATE dbo.NMT_KPI SET  KPI_COLORS=@COLORS WHERE ID_KPI=@ID;   	
END
|

CREATE PROCEDURE [dbo].[spUpdateBitacoraToLoad]	
@FECHA_SINC_INT bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	UPDATE [dbo].[BITACORA_SINC]
	SET CARGADO = 1
	WHERE FECHA_SINC_INT= @FECHA_SINC_INT


	
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_Volumen_Flt_Chain]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		Chain
	FROM
		KPI_Volumen
	GROUP BY
		Chain
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_Volumen]
@Chain VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		PositionCode,
		TimeId,
		Chain,
		ManufacturerCode,
		QtySellOut,
		QtyCPW,
		LevelFlag
	FROM
		KPI_Volumen
	WHERE
		Chain LIKE @Chain
	ORDER BY
		ManufacturerCode,TimeId
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_VisitasPlaneadas_Realizadas_Flt_IndustryLevel]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT		
		IndustryLevel
	FROM
		KPI_VisitasPlaneadas_Realizadas
	GROUP BY
		IndustryLevel
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_VisitasPlaneadas_Realizadas]
@IndustryLevel varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		TimeId,		
		SUM(NbrPlannedVisits) NbrPlannedVisits,
		SUM(NbrSuccesfullyVisits) NbrSuccesfullyVisits		
	FROM
		KPI_VisitasPlaneadas_Realizadas
	WHERE 
		IndustryLevel LIKE @IndustryLevel
	GROUP BY 
		TimeId
	ORDER BY TimeId
	
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_SwitchSellingSKU_Flt_OriginalBrandDescription]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		
		OriginalBrandDescription
	FROM
		KPI_SwitchSellingSKU
	GROUP BY 
		OriginalBrandDescription
END
|
CREATE PROCEDURE [dbo].[spSelectKPI_SwitchSellingSKU]
@OriginalBrandDescription VARCHAR(100),
@TimeId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT		
		K1.IndustryLevel,		
		K1.ProductSubfamilyDescription,
		K1.NbrSwitchSelling,
		K1.TimeId		
	FROM
		KPI_SwitchSellingSKU AS K1
	WHERE
		K1.OriginalBrandDescription LIKE @OriginalBrandDescription
		AND K1.TimeId = @TimeId --(SELECT MAX(TimeId) FROM KPI_SwitchSellingSKU WHERE OriginalBrandDescription LIKE @OriginalBrandDescription AND IndustryLevel=K1.IndustryLevel AND ProductSubfamilyDescription=K1.ProductSubfamilyDescription)	
	GROUP BY 
		K1.IndustryLevel,		
		K1.ProductSubfamilyDescription,
		K1.NbrSwitchSelling,
		K1.TimeId
	ORDER BY
		K1.IndustryLevel,K1.TimeId
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_SwitchSellingEfectivo_Flt_Loyalty]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		
		Loyalty
	FROM
		KPI_SwitchSellingEfectivo
	GROUP BY
		Loyalty
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_SwitchSellingEfectivo_Flt_IndustryLevel]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		
		IndustryLevel
	FROM
		KPI_SwitchSellingEfectivo
	GROUP BY
		IndustryLevel
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_SwitchSellingEfectivo_Flt_Gender]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		
		Gender
	FROM
		KPI_SwitchSellingEfectivo
	GROUP BY
		Gender
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_SwitchSellingEfectivo_Flt_AgeRange]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		
		AgeRange
	FROM
		KPI_SwitchSellingEfectivo
	GROUP BY
		AgeRange
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_SwitchSellingEfectivo]
@IndustryLevel VARCHAR(50),
@Loyalty VARCHAR(50),
@AgeRange VARCHAR(50),
@Gender VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		PositionCode,
		TimeId,
		IndustryLevel,
		Loyalty,
		AgeRange,
		Gender,
		NbrSwitchSellingEffect,
		NbrSwitchSelling,
		LevelFlag
	FROM
		KPI_SwitchSellingEfectivo
	WHERE
		IndustryLevel LIKE @IndustryLevel AND
		Loyalty LIKE @Loyalty AND
		AgeRange LIKE @AgeRange AND
		Gender LIKE @Gender
	ORDER BY TimeId	
	
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_SOM_Flt_SegmentPrice] 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 		
		SegmentPrice
	FROM 
		KPI_SOM
	GROUP BY 
		SegmentPrice
END
|

CREATE PROCEDURE [dbo].[spSelectKPI_SOM_Flt_ManufacturerCode] 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 		
		ManufacturerCode
	FROM 
		KPI_SOM
	GROUP BY 
		ManufacturerCode
END
|

CREATE PROCEDURE [dbo].[spSelectVersion]
	@IDTerritorio AS VARCHAR(100) = ''
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
		FECHA_SINCRONIZACION ,
		VERSION_CLIENTE 
	FROM
		dbo.BITACORA_SINC

	WHERE 
		TERRITORIO = @IDTerritorio
		AND FECHA_SINCRONIZACION=(SELECT MAX(FECHA_SINCRONIZACION) FROM dbo.BITACORA_SINC WHERE 
		TERRITORIO = @IDTerritorio)
END
|

CREATE PROCEDURE [dbo].[spSelectSkins]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT [ID_SKIN]
      ,[SKIN_DSC]
      ,[SKIN_ABOUT]
      ,[RESOURCE_FILE]
      ,[CURRENT_]
  FROM [dbo].[NMT_SKIN]

	
END
|

CREATE PROCEDURE [dbo].[spSelectNMT_TMP_FILES]
AS
BEGIN
	SELECT [ID_FILE]
      ,[FULL_FILENAME]
      ,[FILENAME_KPI]
      ,[FILENAME_POSITIONCODE]
      ,[FILENAME_DATE]
  FROM [dbo].[NMT_TMP_FILES]
END
|

CREATE PROCEDURE [dbo].[spSelectNMT_TMP_CAT_FILES]
AS
BEGIN
	SELECT [ID_FILE]
      ,[FULL_FILENAME]
      ,[CATALOG_NAME]
      ,[FILENAME_DATE]
  FROM [dbo].[NMT_TMP_CAT_FILES]
END
|

CREATE PROCEDURE [dbo].[spSelectNMT_CURRENT_FILES]
AS
BEGIN
	SELECT [ID_FILE]
      ,[FULL_FILENAME]
      ,[FILENAME_KPI]
      ,[FILENAME_POSITIONCODE]
      ,[FILENAME_DATE]
      ,[DB_LOADED]
  FROM [dbo].[NMT_CURRENT_FILES]
  WHERE [DB_LOADED]=0;

END
|

CREATE PROCEDURE [dbo].[spSelectNMT_CURRENT_CAT_FILES]
AS
BEGIN
	SELECT [ID_FILE]
      ,[FULL_FILENAME]
      ,[CATALOG_NAME]
      ,[FILENAME_DATE]
      ,[DB_LOADED]
  FROM [dbo].[NMT_CURRENT_CAT_FILES]
  WHERE [DB_LOADED]=0;

END
|

CREATE PROCEDURE [dbo].[spSelectKPIByID] 
@IDKPI int	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
		K.ID_KPI AS ID,
		K.KPI_DSC AS DESCRIPCION,
		K.KPI_ABOUT AS ACERCADE,
		K.IMG_FILE AS RUTA,
		K.KPI_COLORS AS COLORES,
		K.KPI_FILTRO AS FILTRO
	FROM
		NMT_KPI AS K
	WHERE K.ID_KPI=@IDKPI
		
END
|

CREATE PROCEDURE [dbo].[spSelectKPIByGroup] 
@IDGrupo int	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
		K.ID_KPI AS ID,
		K.KPI_DSC AS DESCRIPCION,
		K.KPI_ABOUT AS ACERCADE,
		K.IMG_FILE AS RUTA,
		K.KPI_COLORS AS COLORES	
	FROM
		NMT_KPI AS K
	INNER JOIN NMT_GROUP_KPI AS GK
	ON GK.ID_GROUP=@IDGrupo AND GK.ID_KPI=K.ID_KPI
		
END
|
CREATE PROCEDURE [dbo].[spSelectKPI_SwitchSellingSKU_Flt_TimeId]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		
		Cast(TimeId as varchar(6)) TimeId
	FROM
		KPI_SwitchSellingSKU
	GROUP BY 
		TimeId
	ORDER BY TimeId DESC
END
