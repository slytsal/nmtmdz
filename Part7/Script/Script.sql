USE [NMTBASE]
;
/****** Object:  Table [dbo].[KPI_SOM]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[KPI_SOM](
	[PositionCode] [nvarchar](50) NULL,
	[TimeId] [bigint] NULL,
	[ManufacturerCode] [nvarchar](25) NULL,
	[SegmentPrice] [nvarchar](25) NULL,
	[SOM] [decimal](22, 4) NULL,
	[LevelFlag] [int] NULL
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[KPI_Sobreindexados]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[KPI_Sobreindexados](
	[PositionCode] [nvarchar](50) NULL,
	[TimeId] [bigint] NULL,
	[CustomerCode] [nvarchar](20) NULL,
	[CustomerName] [nvarchar](75) NULL,
	[CustomerAddress] [nvarchar](255) NULL,
	[IndustryLevel] [nvarchar](10) NULL,
	[SOMBrandPos] [decimal](22, 4) NULL,
	[SOMBrandPosition] [decimal](22, 4) NULL,
	[ProductSubfamilyCode] [nvarchar](75) NULL,
	[ProductSubfamilyDescription] [nvarchar](75) NULL,
	[LevelFlag] [int] NULL
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[KPI_Clientes_ITO_UX]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[KPI_Clientes_ITO_UX](
	[PositionCode] [nvarchar](50) NULL,
	[TimeId] [bigint] NULL,
	[IndustryLevel] [nvarchar](10) NULL,
	[NbrCustUX] [bigint] NULL,
	[NbrCustITO] [bigint] NULL,
	[LevelFlag] [int] NULL
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[KPI_VisitasPlaneadas_Realizadas]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[KPI_VisitasPlaneadas_Realizadas](
	[PositionCode] [nvarchar](50) NULL,
	[TimeId] [bigint] NULL,
	[IndustryLevel] [nvarchar](10) NULL,
	[NbrPlannedVisits] [bigint] NULL,
	[NbrSuccesfullyVisits] [bigint] NULL,
	[LevelFlag] [int] NULL
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[KPI_SwitchSellingEfectivo]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[KPI_SwitchSellingEfectivo](
	[PositionCode] [nvarchar](50) NULL,
	[TimeId] [bigint] NULL,
	[IndustryLevel] [nvarchar](10) NULL,
	[NbrSwitchSellingEffect] [bigint] NULL,
	[NbrSwitchSelling] [bigint] NULL,
	[LevelFlag] [int] NULL,
	[Loyalty] [nvarchar](50) NULL,
	[AgeRange] [nvarchar](50) NULL,
	[Gender] [nvarchar](50) NULL
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[KPI_SwitchSellingSKU]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[KPI_SwitchSellingSKU](
	[PositionCode] [nvarchar](50) NULL,
	[TimeId] [bigint] NULL,
	[IndustryLevel] [nvarchar](10) NULL,
	[OriginalBrandCode] [nvarchar](75) NULL,
	[OriginalBrandDescription] [nvarchar](75) NULL,
	[ProductSubfamilyCode] [nvarchar](75) NULL,
	[ProductSubfamilyDescription] [nvarchar](75) NULL,
	[NbrSwitchSelling] [bigint] NULL,
	[LevelFlag] [int] NULL
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[KPI_EfectividadMDZ]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[KPI_EfectividadMDZ](
	[PositionCode] [nvarchar](50) NULL,
	[TimeId] [bigint] NULL,
	[ProductSubfamilyCode] [nvarchar](75) NULL,
	[ProductSubfamilyDescription] [nvarchar](75) NULL,
	[IndustryLevel] [nvarchar](50) NULL,
	[EfectivenessByBrand] [decimal](22, 4) NULL,
	[LevelFlag] [int] NULL
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[KPI_Agotamiento]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[KPI_Agotamiento](
	[PositionCode] [nvarchar](50) NULL,
	[TimeId] [bigint] NULL,
	[IndustryLevel] [nvarchar](10) NULL,
	[BrandCode] [nvarchar](75) NULL,
	[BrandDescription] [nvarchar](75) NULL,
	[NbrOOS] [decimal](22, 4) NULL,
	[LevelFlag] [int] NULL
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[KPI_Manejos]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[KPI_Manejos](
	[PositionCode] [nvarchar](50) NULL,
	[TimeId] [bigint] NULL,
	[IndustryLevel] [nvarchar](10) NULL,
	[BrandCode] [nvarchar](75) NULL,
	[BrandDescription] [nvarchar](75) NULL,
	[NbrHandling] [decimal](22, 4) NULL,
	[LevelFlag] [int] NULL
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[KPI_Volumen]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[KPI_Volumen](
	[PositionCode] [nvarchar](50) NULL,
	[TimeId] [bigint] NULL,
	[Chain] [nvarchar](25) NULL,
	[ManufacturerCode] [nvarchar](25) NULL,
	[QtySellOut] [decimal](22, 4) NULL,
	[QtyCPW] [decimal](22, 4) NULL,
	[LevelFlag] [int] NULL
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[KPI_Ejecución_Calidad]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[KPI_Ejecución_Calidad](
	[PositionCode] [nvarchar](50) NULL,
	[PYTD] [decimal](5, 2) NULL,
	[Ciclo_1] [decimal](5, 2) NULL,
	[Ciclo_2] [decimal](5, 2) NULL,
	[Ciclo_3] [decimal](5, 2) NULL,
	[Ciclo_4] [decimal](5, 2) NULL,
	[Ciclo_5] [decimal](5, 2) NULL,
	[Ciclo_6] [decimal](5, 2) NULL,
	[Ciclo_7] [decimal](5, 2) NULL,
	[Ciclo_8] [decimal](5, 2) NULL,
	[Ciclo_9] [decimal](5, 2) NULL,
	[Ciclo_10] [decimal](5, 2) NULL,
	[Ciclo_11] [decimal](5, 2) NULL,
	[Ciclo_12] [decimal](5, 2) NULL,
	[Canal] [nvarchar](10) NULL,
	[LastCycle] [bigint] NULL
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[NMT_CURRENT_FILES]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[NMT_CURRENT_FILES](
	[ID_FILE] [int] IDENTITY(1,1) NOT NULL,
	[FULL_FILENAME] [nvarchar](200) NULL,
	[FILENAME_KPI] [nvarchar](150) NULL,
	[FILENAME_POSITIONCODE] [nvarchar](50) NULL,
	[FILENAME_DATE] [bigint] NULL,
	[DB_LOADED] [bit] NULL,
 CONSTRAINT [PK_NMT_CURRENT_FILES] PRIMARY KEY CLUSTERED 
(
	[ID_FILE] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[NMT_TMP_FILES]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[NMT_TMP_FILES](
	[ID_FILE] [int] IDENTITY(1,1) NOT NULL,
	[FULL_FILENAME] [nvarchar](200) NULL,
	[FILENAME_KPI] [nvarchar](150) NULL,
	[FILENAME_POSITIONCODE] [nvarchar](50) NULL,
	[FILENAME_DATE] [bigint] NULL,
 CONSTRAINT [PK_NMT_TMP_FILES] PRIMARY KEY CLUSTERED 
(
	[ID_FILE] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[NMT_CURRENT_CAT_FILES]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[NMT_CURRENT_CAT_FILES](
	[ID_FILE] [int] IDENTITY(1,1) NOT NULL,
	[FULL_FILENAME] [nvarchar](200) NULL,
	[CATALOG_NAME] [nvarchar](150) NULL,
	[FILENAME_DATE] [bigint] NULL,
	[DB_LOADED] [bit] NULL,
 CONSTRAINT [PK_NMT_CURRENT_CAT_FILES] PRIMARY KEY CLUSTERED 
(
	[ID_FILE] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[NMT_TMP_CAT_FILES]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[NMT_TMP_CAT_FILES](
	[ID_FILE] [int] IDENTITY(1,1) NOT NULL,
	[FULL_FILENAME] [nvarchar](200) NULL,
	[CATALOG_NAME] [nvarchar](150) NULL,
	[FILENAME_DATE] [bigint] NULL,
 CONSTRAINT [PK_NMT_TMP_CAT_FILES] PRIMARY KEY CLUSTERED 
(
	[ID_FILE] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[NMT_SETTINGS]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[NMT_SETTINGS](
	[ID_SETTING] [int]  NOT NULL,
	[SETTING_NAME] [nvarchar](128) NULL,
	[SETTING_VALUE] [nvarchar](2000) NULL,
 CONSTRAINT [PK_NMT_SETTINGS] PRIMARY KEY CLUSTERED 
(
	[ID_SETTING] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[NMT_SKIN]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[NMT_SKIN](
	[ID_SKIN] [int] IDENTITY(1,1) NOT NULL,
	[SKIN_DSC] [nvarchar](128) NULL,
	[SKIN_ABOUT] [nvarchar](500) NULL,
	[RESOURCE_FILE] [nvarchar](250) NULL,
	[CURRENT_] [bit] NULL,
 CONSTRAINT [PK_NMT_SKIN] PRIMARY KEY CLUSTERED 
(
	[ID_SKIN] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;
SET IDENTITY_INSERT [dbo].[NMT_SKIN] ON
INSERT [dbo].[NMT_SKIN] ([ID_SKIN], [SKIN_DSC], [SKIN_ABOUT], [RESOURCE_FILE], [CURRENT_]) VALUES (1, N'BLUE', N'BLUE', NULL, 1)
INSERT [dbo].[NMT_SKIN] ([ID_SKIN], [SKIN_DSC], [SKIN_ABOUT], [RESOURCE_FILE], [CURRENT_]) VALUES (2, N'GREEN', N'GREEN', NULL, 0)
INSERT [dbo].[NMT_SKIN] ([ID_SKIN], [SKIN_DSC], [SKIN_ABOUT], [RESOURCE_FILE], [CURRENT_]) VALUES (3, N'YELLOW', N'YELLOW', NULL, 0)
INSERT [dbo].[NMT_SKIN] ([ID_SKIN], [SKIN_DSC], [SKIN_ABOUT], [RESOURCE_FILE], [CURRENT_]) VALUES (4, N'PURPLE', N'PURPLE', NULL, 0)
SET IDENTITY_INSERT [dbo].[NMT_SKIN] OFF
/****** Object:  Table [dbo].[Internal_Organization]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[Internal_Organization](
	[RegionCode] [nvarchar](75) NULL,
	[RegionDescription] [nvarchar](75) NULL,
	[AreaCode] [nvarchar](75) NULL,
	[AreaDescription] [nvarchar](75) NULL,
	[ZoneCode] [nvarchar](75) NULL,
	[ZoneDescription] [nvarchar](75) NULL,
	[TerritoryCode] [nvarchar](75) NULL,
	[TerritoryDescription] [nvarchar](75) NULL
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[Catalogo_De_Marcas]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[Catalogo_De_Marcas](
	[ProductSubFamilyCode] [nvarchar](75) NULL,
	[ProductSubFamilyDescription] [nvarchar](75) NULL,
	[ManufacturerCode] [nvarchar](75) NULL
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[NMT_LOG]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[NMT_LOG](
	[ID_MSG] [int] NOT NULL,
	[ID_MSG_TYPE] [int] NULL,
	[MSG] [nvarchar](2000) NULL,
	[MSG_DATETIME] [datetime] NULL,
 CONSTRAINT [PK_NMT_LOG] PRIMARY KEY CLUSTERED 
(
	[ID_MSG] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[tblNMTHomologacionCatalogos]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET ANSI_PADDING ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[tblNMTHomologacionCatalogos](
	[strNombreArchivo] [varchar](100) NOT NULL,
	[strNombreTabla] [varchar](100) NOT NULL
) ON [PRIMARY]
;
SET ANSI_PADDING OFF
;
INSERT [dbo].[tblNMTHomologacionCatalogos] ([strNombreArchivo], [strNombreTabla]) VALUES (N'GROUP', N'NMT_GROUP')
INSERT [dbo].[tblNMTHomologacionCatalogos] ([strNombreArchivo], [strNombreTabla]) VALUES (N'KPI', N'NMT_KPI')
INSERT [dbo].[tblNMTHomologacionCatalogos] ([strNombreArchivo], [strNombreTabla]) VALUES (N'GROUPKPI', N'NMT_GROUP_KPI')

/****** Object:  Table [dbo].[KPI_MDZ_SKU_Average]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[KPI_MDZ_SKU_Average](
	[PositionCode] [nvarchar](50) NULL,
	[TimeId] [bigint] NULL,
	[IndustryLevel] [nvarchar](10) NULL,
	[SKUAvg] [decimal](22, 4) NULL,
	[LevelFlag] [int] NULL
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[KPI_MDZ_Coverage]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[KPI_MDZ_Coverage](
	[PositionCode] [nvarchar](50) NULL,
	[TimeId] [bigint] NULL,
	[IndustryLevel] [nvarchar](10) NULL,
	[NbrCustUX] [bigint] NULL,
	[NbrCustITO] [bigint] NULL,
	[LevelFlag] [int] NULL
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[KPI_MDZ_GENERAL_DROP_SIZE]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[KPI_MDZ_GENERAL_DROP_SIZE](
	[PositionCode] [nvarchar](50) NULL,
	[TimeId] [bigint] NULL,
	[IndustryLevel] [nvarchar](10) NULL,
	[GeneralDropSize] [decimal](22, 4) NULL,
	[LevelFlag] [int] NULL
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[KPI_MDZ_EFFECTIVE_DROP_SIZE]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[KPI_MDZ_EFFECTIVE_DROP_SIZE](
	[PositionCode] [nvarchar](50) NULL,
	[TimeId] [bigint] NULL,
	[IndustryLevel] [nvarchar](10) NULL,
	[EffectiveDropSize] [decimal](22, 4) NULL,
	[LevelFlag] [int] NULL
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[KPI_FrecuenciaVisitas]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[KPI_FrecuenciaVisitas](
	[PositionCode] [nvarchar](50) NULL,
	[TimeId] [bigint] NULL,
	[IndustryLevel] [nvarchar](10) NULL,
	[LevelFlag] [int] NULL,
	[FreqVisits] [decimal](22, 4) NULL
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[KPI_SELL_OUT_KA_SEGMENT_CONTRIBUTION]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[KPI_SELL_OUT_KA_SEGMENT_CONTRIBUTION](
	[PositionCode] [nvarchar](50) NULL,
	[TimeId] [bigint] NULL,
	[ChainCode] [nvarchar](35) NULL,
	[SegmentPrice] [nvarchar](25) NULL,
	[SOM] [decimal](22, 4) NULL,
	[LevelFlag] [int] NULL
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[KPI_SKU_Average]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[KPI_SKU_Average](
	[PositionCode] [nvarchar](50) NULL,
	[TimeId] [bigint] NULL,
	[IndustryLevel] [nvarchar](10) NULL,
	[LevelFlag] [int] NULL,
	[SegmentPrice] [nvarchar](25) NULL,
	[SKUAvg] [decimal](22, 4) NULL
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[KPI_Share_Segment]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[KPI_Share_Segment](
	[PositionCode] [nvarchar](50) NULL,
	[TimeId] [bigint] NULL,
	[IndustryLevel] [nvarchar](10) NULL,
	[LevelFlag] [int] NULL,
	[SegmentPrice] [nvarchar](25) NULL,
	[SOM] [decimal](22, 4) NULL
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[KPI_Share_PMM_SellOut]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[KPI_Share_PMM_SellOut](
	[PositionCode] [nvarchar](50) NULL,
	[TimeId] [bigint] NULL,
	[LevelFlag] [int] NULL,
	[ChainCode] [nvarchar](35) NULL,
	[ManufacturerCode] [nvarchar](25) NULL,
	[SOM] [decimal](22, 4) NULL
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[KPI_Share_SKU]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[KPI_Share_SKU](
	[PositionCode] [nvarchar](50) NULL,
	[TimeId] [bigint] NULL,
	[LevelFlag] [int] NULL,
	[ProductSubfamilyCode] [nvarchar](75) NULL,
	[ProductSubfamilyDescription] [nvarchar](75) NULL,
	[SegmentPrice] [nvarchar](25) NULL,
	[SOM] [decimal](22, 4) NULL
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[NMT_GROUP]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[NMT_GROUP](
	[ID_GROUP] [int]  NOT NULL,
	[GROUP_DSC] [nvarchar](150) NULL,
	[GROUP_ABOUT] [nvarchar](500) NULL,
	[IMG_FILE] [nvarchar](250) NULL,
	[LAST_INDEX] [int] NULL,
	[LAST_FILTER] [nvarchar](4000) NULL,
 CONSTRAINT [PK_NMT_GROUP] PRIMARY KEY CLUSTERED 
(
	[ID_GROUP] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;
INSERT [dbo].[NMT_GROUP] ([ID_GROUP], [GROUP_DSC], [GROUP_ABOUT], [IMG_FILE], [LAST_INDEX], [LAST_FILTER]) VALUES (1, N'Indicadores que representan la producción para Volumen', N'Volumen', N'C:\_covers\Volumen.png', 0, NULL)
INSERT [dbo].[NMT_GROUP] ([ID_GROUP], [GROUP_DSC], [GROUP_ABOUT], [IMG_FILE], [LAST_INDEX], [LAST_FILTER]) VALUES (2, N'Indicadores que representan la producción para Mercadeo', N'Mercadeo', N'C:\_covers\Mercadeo.png', 0, NULL)
INSERT [dbo].[NMT_GROUP] ([ID_GROUP], [GROUP_DSC], [GROUP_ABOUT], [IMG_FILE], [LAST_INDEX], [LAST_FILTER]) VALUES (3, N'Indicadores que representan la producción para Ejecución de Calidad', N'Ejecución de Calidad', N'C:\_covers\EjecucionCalidad.png', 0, NULL)
INSERT [dbo].[NMT_GROUP] ([ID_GROUP], [GROUP_DSC], [GROUP_ABOUT], [IMG_FILE], [LAST_INDEX], [LAST_FILTER]) VALUES (4, N'Indicadores que representan la producción para SOM', N'SOM', N'C:\_covers\SOM.png', 0, NULL)
INSERT [dbo].[NMT_GROUP] ([ID_GROUP], [GROUP_DSC], [GROUP_ABOUT], [IMG_FILE], [LAST_INDEX], [LAST_FILTER]) VALUES (5, N'Indicadores que representan la produción para MDZ', N'MDZ', N'C:\_covers\MDZ.png', 0, NULL)


/****** Object:  Table [dbo].[tblNMTHomologacionArchivos]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET ANSI_PADDING ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[tblNMTHomologacionArchivos](
	[strNombreArchivo] [varchar](100) NOT NULL,
	[strNombreTabla] [varchar](100) NOT NULL
) ON [PRIMARY]
;
SET ANSI_PADDING OFF
;
INSERT [dbo].[tblNMTHomologacionArchivos] ([strNombreArchivo], [strNombreTabla]) VALUES (N'AGO', N'KPI_Agotamiento')
INSERT [dbo].[tblNMTHomologacionArchivos] ([strNombreArchivo], [strNombreTabla]) VALUES (N'CLI', N'KPI_Clientes_ITO_UX')
INSERT [dbo].[tblNMTHomologacionArchivos] ([strNombreArchivo], [strNombreTabla]) VALUES (N'EMDZ', N'KPI_EfectividadMDZ')
INSERT [dbo].[tblNMTHomologacionArchivos] ([strNombreArchivo], [strNombreTabla]) VALUES (N'ECLD', N'KPI_Ejecución_Calidad')
INSERT [dbo].[tblNMTHomologacionArchivos] ([strNombreArchivo], [strNombreTabla]) VALUES (N'FVST', N'KPI_FrecuenciaVisitas')
INSERT [dbo].[tblNMTHomologacionArchivos] ([strNombreArchivo], [strNombreTabla]) VALUES (N'MNJ', N'KPI_Manejos')
INSERT [dbo].[tblNMTHomologacionArchivos] ([strNombreArchivo], [strNombreTabla]) VALUES (N'SPS', N'KPI_Share_PMM_SellOut')
INSERT [dbo].[tblNMTHomologacionArchivos] ([strNombreArchivo], [strNombreTabla]) VALUES (N'SGMT', N'KPI_Share_Segment')
INSERT [dbo].[tblNMTHomologacionArchivos] ([strNombreArchivo], [strNombreTabla]) VALUES (N'SSKU', N'KPI_Share_SKU')
INSERT [dbo].[tblNMTHomologacionArchivos] ([strNombreArchivo], [strNombreTabla]) VALUES (N'SAVG', N'KPI_SKU_Average')
INSERT [dbo].[tblNMTHomologacionArchivos] ([strNombreArchivo], [strNombreTabla]) VALUES (N'SIDX', N'KPI_Sobreindexados')
INSERT [dbo].[tblNMTHomologacionArchivos] ([strNombreArchivo], [strNombreTabla]) VALUES (N'SOM', N'KPI_SOM')
INSERT [dbo].[tblNMTHomologacionArchivos] ([strNombreArchivo], [strNombreTabla]) VALUES (N'SSE', N'KPI_SwitchSellingEfectivo')
INSERT [dbo].[tblNMTHomologacionArchivos] ([strNombreArchivo], [strNombreTabla]) VALUES (N'SSS', N'KPI_SwitchSellingSKU')
INSERT [dbo].[tblNMTHomologacionArchivos] ([strNombreArchivo], [strNombreTabla]) VALUES (N'VPR', N'KPI_VisitasPlaneadas_Realizadas')
INSERT [dbo].[tblNMTHomologacionArchivos] ([strNombreArchivo], [strNombreTabla]) VALUES (N'VOL', N'KPI_Volumen')
INSERT [dbo].[tblNMTHomologacionArchivos] ([strNombreArchivo], [strNombreTabla]) VALUES (N'SOKSC',N'KPI_SELL_OUT_KA_SEGMENT_CONTRIBUTION')
INSERT [dbo].[tblNMTHomologacionArchivos] ([strNombreArchivo], [strNombreTabla]) VALUES (N'SAMDZ',N'KPI_MDZ_SKU_Average')
INSERT [dbo].[tblNMTHomologacionArchivos] ([strNombreArchivo], [strNombreTabla]) VALUES (N'CMDZ',N'KPI_MDZ_Coverage')
INSERT [dbo].[tblNMTHomologacionArchivos] ([strNombreArchivo], [strNombreTabla]) VALUES (N'GDSMDZ',N'KPI_MDZ_GENERAL_DROP_SIZE')
INSERT [dbo].[tblNMTHomologacionArchivos] ([strNombreArchivo], [strNombreTabla]) VALUES (N'EDSMDZ',N'KPI_MDZ_EFFECTIVE_DROP_SIZE')
/****** Object:  Table [dbo].[BITACORA_SINC]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[BITACORA_SINC](
	[USUARIO] [nvarchar](250) NULL,
	[USUARIO_PC] [nvarchar](250) NULL,
	[FECHA_SINCRONIZACION] [datetime] NULL,
	[VERSION_CLIENTE] [nvarchar](15) NULL,
	[DURACION] [int] NULL,
	[KPI] [nvarchar](250) NULL,
	[TERRITORIO] [nvarchar](50) NULL,
	[FECHA_SINC_INT] [bigint] NULL,
	[CARGADO] [bit] NULL
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[NMT_KPI]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[NMT_KPI](
	[ID_KPI] [int]  NOT NULL,
	[KPI_DSC] [nvarchar](150) NULL,
	[KPI_ABOUT] [nvarchar](500) NULL,
	[KPI_OWN_TABLE] [nvarchar](128) NULL,
	[IMG_FILE] [nvarchar](250) NULL,
	[KPI_COLORS] [nvarchar](500) NULL,
	[KPI_FILTRO] [nvarchar](4000) NULL,
 CONSTRAINT [PK_NMT_KPI] PRIMARY KEY CLUSTERED 
(
	[ID_KPI] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;

INSERT [dbo].[NMT_KPI] ([ID_KPI], [KPI_DSC], [KPI_ABOUT], [KPI_OWN_TABLE], [IMG_FILE], [KPI_COLORS], [KPI_FILTRO]) VALUES (1, N'Descripcion KEY ACCOUNT', N'KEY ACCOUNT', NULL, N'C:\_covers\KeyAccount.png', NULL, NULL)
INSERT [dbo].[NMT_KPI] ([ID_KPI], [KPI_DSC], [KPI_ABOUT], [KPI_OWN_TABLE], [IMG_FILE], [KPI_COLORS], [KPI_FILTRO]) VALUES (2, N'Descripcion POS OVER INDEXED', N'POS OVER INDEXED', NULL, N'C:\_covers\PosOverIndexed.png', NULL, NULL)
INSERT [dbo].[NMT_KPI] ([ID_KPI], [KPI_DSC], [KPI_ABOUT], [KPI_OWN_TABLE], [IMG_FILE], [KPI_COLORS], [KPI_FILTRO]) VALUES (3, N'Descripcion POS OVER SHARE BY BRAND', N'SHARE BY BRAND', NULL, N'C:\_covers\ShareByBrand.png', NULL, NULL)
INSERT [dbo].[NMT_KPI] ([ID_KPI], [KPI_DSC], [KPI_ABOUT], [KPI_OWN_TABLE], [IMG_FILE], [KPI_COLORS], [KPI_FILTRO]) VALUES (4, N'Descripcion POS OVER SHARE OF MARKET', N'SHARE OF MARKET', NULL, N'C:\_covers\ShareOfMarket.PNG', NULL, NULL)
INSERT [dbo].[NMT_KPI] ([ID_KPI], [KPI_DSC], [KPI_ABOUT], [KPI_OWN_TABLE], [IMG_FILE], [KPI_COLORS], [KPI_FILTRO]) VALUES (5, N'Descripcion POS OVER SEGMENT CONTRIBUTION', N'SEGMENT CONTRIBUTION', NULL, N'C:\_covers\SegmentContribution.png', NUll, NULL)
INSERT [dbo].[NMT_KPI] ([ID_KPI], [KPI_DSC], [KPI_ABOUT], [KPI_OWN_TABLE], [IMG_FILE], [KPI_COLORS], [KPI_FILTRO]) VALUES (6, N'Descripcion POS OVER SELL OUT', N'SELL OUT', NULL, N'C:\_covers\SellOut.png', NULL, NULL)
INSERT [dbo].[NMT_KPI] ([ID_KPI], [KPI_DSC], [KPI_ABOUT], [KPI_OWN_TABLE], [IMG_FILE], [KPI_COLORS], [KPI_FILTRO]) VALUES (7, N'Descripcion POS OVER CUSTOMER PLANNED VS UNIVERSE', N'CUSTOMER PLANNED VS UNIVERSE', NULL, N'C:\_covers\CustomersPlannedVsUniverse.png', NULL,NULL)
INSERT [dbo].[NMT_KPI] ([ID_KPI], [KPI_DSC], [KPI_ABOUT], [KPI_OWN_TABLE], [IMG_FILE], [KPI_COLORS], [KPI_FILTRO]) VALUES (8, N'Descripcion POS OVER PLANNED VISITS VS ACTUAL VISITS', N'PLANNED VISITS VS ACTUAL VISITS', NULL, N'C:\_covers\PlannedVisitsVsActualVisits.png', NULL, NULL)
INSERT [dbo].[NMT_KPI] ([ID_KPI], [KPI_DSC], [KPI_ABOUT], [KPI_OWN_TABLE], [IMG_FILE], [KPI_COLORS], [KPI_FILTRO]) VALUES (9, N'Descripcion POS OVER HANDLING', N'HANDLING', NULL, N'C:\_covers\Handling.png', NULL, NULL)
INSERT [dbo].[NMT_KPI] ([ID_KPI], [KPI_DSC], [KPI_ABOUT], [KPI_OWN_TABLE], [IMG_FILE], [KPI_COLORS], [KPI_FILTRO]) VALUES (10, N'Descripcion POS OVER VISIT FREQUENCY', N'VISIT FREQUENCY', NULL, N'C:\_covers\VisitFrequency.png', NULL, NULL)
INSERT [dbo].[NMT_KPI] ([ID_KPI], [KPI_DSC], [KPI_ABOUT], [KPI_OWN_TABLE], [IMG_FILE], [KPI_COLORS], [KPI_FILTRO]) VALUES (11, N'Descripcion POS OVER SKU AVERAGE', N'SKU AVERAGE', NULL, N'C:\_covers\SKUAverage.png', NULL, NULL)
INSERT [dbo].[NMT_KPI] ([ID_KPI], [KPI_DSC], [KPI_ABOUT], [KPI_OWN_TABLE], [IMG_FILE], [KPI_COLORS], [KPI_FILTRO]) VALUES (12, N'Descripcion POS OVER MDZ EFFECTIVENESS', N'MDZ EFFECTIVENESS', NULL, N'C:\_covers\MDZEffectivenness.png', NULL, NULL)
INSERT [dbo].[NMT_KPI] ([ID_KPI], [KPI_DSC], [KPI_ABOUT], [KPI_OWN_TABLE], [IMG_FILE], [KPI_COLORS], [KPI_FILTRO]) VALUES (13, N'Descripcion POS OVER SWITCH SELLING BY SKU', N'SWITCH SELLING BY SKU', NULL, N'C:\_covers\SwitchSellingBySKU.png', NULL, NULL)
INSERT [dbo].[NMT_KPI] ([ID_KPI], [KPI_DSC], [KPI_ABOUT], [KPI_OWN_TABLE], [IMG_FILE], [KPI_COLORS], [KPI_FILTRO]) VALUES (14, N'Descripcion POS OVER SWITCH SELLING', N'SWITCH SELLING', NULL, N'C:\_covers\SwitchSelling.png', NULL, NULL)
INSERT [dbo].[NMT_KPI] ([ID_KPI], [KPI_DSC], [KPI_ABOUT], [KPI_OWN_TABLE], [IMG_FILE], [KPI_COLORS], [KPI_FILTRO]) VALUES (15, N'Descripcion POS OVER OUT OF STOCKS', N'OUT OF STOCKS', NULL, N'C:\_covers\OutOfStocks.png', NULL, NULL)
INSERT [dbo].[NMT_KPI] ([ID_KPI], [KPI_DSC], [KPI_ABOUT], [KPI_OWN_TABLE], [IMG_FILE], [KPI_COLORS], [KPI_FILTRO]) VALUES (16, N'Descripcion POS OVER EJECUCION DE CALIDAD', N'EJECUCION DE CALIDAD', NULL, N'C:\_covers\EjecucionDeCalidad.png', NULL, NULL)
INSERT [dbo].[NMT_KPI] ([ID_KPI], [KPI_DSC], [KPI_ABOUT], [KPI_OWN_TABLE], [IMG_FILE], [KPI_COLORS], [KPI_FILTRO]) VALUES (18, N'Descripcion SELL OUT KA SEGMENT CONTRIBUTION', N'SELL OUT KA SEGMENT CONTRIBUTION', NULL, N'C:\_covers\GRAF_CONTR_SELL_OUT.png', NULL, NULL)
INSERT [dbo].[NMT_KPI] ([ID_KPI], [KPI_DSC], [KPI_ABOUT], [KPI_OWN_TABLE], [IMG_FILE], [KPI_COLORS], [KPI_FILTRO]) VALUES (19, N'Descripcion MDZ SKU Average', N'MDZ SKU Average', NULL, N'C:\_covers\GRAF_SKUAVERG.png', NULL, NULL)
INSERT [dbo].[NMT_KPI] ([ID_KPI], [KPI_DSC], [KPI_ABOUT], [KPI_OWN_TABLE], [IMG_FILE], [KPI_COLORS], [KPI_FILTRO]) VALUES (20, N'Descripcion MDZ Coverage', N'MDZ Coverage', NULL, N'C:\_covers\GRAF_COB_UX.png', NULL, NULL)
INSERT [dbo].[NMT_KPI] ([ID_KPI], [KPI_DSC], [KPI_ABOUT], [KPI_OWN_TABLE], [IMG_FILE], [KPI_COLORS], [KPI_FILTRO]) VALUES (21, N'Descripcion MDZ GENERAL DROP SIZE', N'MDZ GENERAL DROP SIZE', NULL, N'C:\_covers\GRAF_GRAL_DROPS.png', NULL, NULL)
INSERT [dbo].[NMT_KPI] ([ID_KPI], [KPI_DSC], [KPI_ABOUT], [KPI_OWN_TABLE], [IMG_FILE], [KPI_COLORS], [KPI_FILTRO]) VALUES (22, N'Descripcion MDZ EFFECTIVE DROP SIZE', N'MDZ EFFECTIVE DROP SIZE', NULL, N'C:\_covers\GRAF_EFECT_DROPS.png', NULL, NULL)

/****** Object:  Table [dbo].[NMT_GROUP_KPI]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[NMT_GROUP_KPI](
	[ID_GROUP] [int] NOT NULL,
	[ID_KPI] [int] NOT NULL,
 CONSTRAINT [PK_NMT_GROUP_KPI] PRIMARY KEY CLUSTERED 
(
	[ID_GROUP] ASC,
	[ID_KPI] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;
INSERT [dbo].[NMT_GROUP_KPI] ([ID_GROUP], [ID_KPI]) VALUES (1, 5)
INSERT [dbo].[NMT_GROUP_KPI] ([ID_GROUP], [ID_KPI]) VALUES (1, 6)
INSERT [dbo].[NMT_GROUP_KPI] ([ID_GROUP], [ID_KPI]) VALUES (1, 18)
INSERT [dbo].[NMT_GROUP_KPI] ([ID_GROUP], [ID_KPI]) VALUES (2, 7)
INSERT [dbo].[NMT_GROUP_KPI] ([ID_GROUP], [ID_KPI]) VALUES (2, 8)
INSERT [dbo].[NMT_GROUP_KPI] ([ID_GROUP], [ID_KPI]) VALUES (2, 9)
INSERT [dbo].[NMT_GROUP_KPI] ([ID_GROUP], [ID_KPI]) VALUES (2, 10)
INSERT [dbo].[NMT_GROUP_KPI] ([ID_GROUP], [ID_KPI]) VALUES (2, 11)
INSERT [dbo].[NMT_GROUP_KPI] ([ID_GROUP], [ID_KPI]) VALUES (2, 12)
INSERT [dbo].[NMT_GROUP_KPI] ([ID_GROUP], [ID_KPI]) VALUES (2, 13)
INSERT [dbo].[NMT_GROUP_KPI] ([ID_GROUP], [ID_KPI]) VALUES (2, 14)
INSERT [dbo].[NMT_GROUP_KPI] ([ID_GROUP], [ID_KPI]) VALUES (2, 15)
INSERT [dbo].[NMT_GROUP_KPI] ([ID_GROUP], [ID_KPI]) VALUES (3, 16)
INSERT [dbo].[NMT_GROUP_KPI] ([ID_GROUP], [ID_KPI]) VALUES (4, 1)
INSERT [dbo].[NMT_GROUP_KPI] ([ID_GROUP], [ID_KPI]) VALUES (4, 2)
INSERT [dbo].[NMT_GROUP_KPI] ([ID_GROUP], [ID_KPI]) VALUES (4, 3)
INSERT [dbo].[NMT_GROUP_KPI] ([ID_GROUP], [ID_KPI]) VALUES (4, 4)
INSERT [dbo].[NMT_GROUP_KPI] ([ID_GROUP], [ID_KPI]) VALUES (5, 19)
INSERT [dbo].[NMT_GROUP_KPI] ([ID_GROUP], [ID_KPI]) VALUES (5, 20)
INSERT [dbo].[NMT_GROUP_KPI] ([ID_GROUP], [ID_KPI]) VALUES (5, 21)
INSERT [dbo].[NMT_GROUP_KPI] ([ID_GROUP], [ID_KPI]) VALUES (5, 22)
/****** Object:  Table [dbo].[NMT_OBJECT_DATA]    Script Date: 12/29/2011 10:40:46 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET NOCOUNT ON; CREATE TABLE [dbo].[NMT_OBJECT_DATA](
	[ID_KPI] [int]  NOT NULL,
	[OBJECT_NAME] [nvarchar](128) NULL,
	[JSON_DATA] [nvarchar](max) NULL,
 CONSTRAINT [PK_NMT_OBJECT_DATA] PRIMARY KEY CLUSTERED 
(
	[ID_KPI] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;
/****** Object:  Default [DF_BITACORA_SINC_CARGADO]    Script Date: 12/29/2011 10:40:46 ******/
ALTER TABLE [dbo].[BITACORA_SINC] ADD  CONSTRAINT [DF_BITACORA_SINC_CARGADO]  DEFAULT ((0)) FOR [CARGADO]
;