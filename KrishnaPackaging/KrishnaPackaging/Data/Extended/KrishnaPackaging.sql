USE [master]
GO
/****** Object:  Database [KrishnaPackaging]    Script Date: 11-09-2018 15:32:17 ******/
CREATE DATABASE [KrishnaPackaging]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'KrishnaPackaging', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.DEV1\MSSQL\DATA\KrishnaPackaging.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'KrishnaPackaging_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.DEV1\MSSQL\DATA\KrishnaPackaging_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [KrishnaPackaging] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [KrishnaPackaging].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [KrishnaPackaging] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [KrishnaPackaging] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [KrishnaPackaging] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [KrishnaPackaging] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [KrishnaPackaging] SET ARITHABORT OFF 
GO
ALTER DATABASE [KrishnaPackaging] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [KrishnaPackaging] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [KrishnaPackaging] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [KrishnaPackaging] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [KrishnaPackaging] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [KrishnaPackaging] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [KrishnaPackaging] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [KrishnaPackaging] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [KrishnaPackaging] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [KrishnaPackaging] SET  DISABLE_BROKER 
GO
ALTER DATABASE [KrishnaPackaging] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [KrishnaPackaging] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [KrishnaPackaging] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [KrishnaPackaging] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [KrishnaPackaging] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [KrishnaPackaging] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [KrishnaPackaging] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [KrishnaPackaging] SET RECOVERY FULL 
GO
ALTER DATABASE [KrishnaPackaging] SET  MULTI_USER 
GO
ALTER DATABASE [KrishnaPackaging] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [KrishnaPackaging] SET DB_CHAINING OFF 
GO
ALTER DATABASE [KrishnaPackaging] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [KrishnaPackaging] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [KrishnaPackaging] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'KrishnaPackaging', N'ON'
GO
ALTER DATABASE [KrishnaPackaging] SET QUERY_STORE = OFF
GO
USE [KrishnaPackaging]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [KrishnaPackaging]
GO
/****** Object:  Table [dbo].[CompanyMaster]    Script Date: 11-09-2018 15:32:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyMaster](
	[CompanyId] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyName] [nvarchar](150) NULL,
	[PersonName] [nvarchar](150) NULL,
	[BillingAddress] [nvarchar](1000) NULL,
	[DeliveryAddress] [nvarchar](1000) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[StateCode] [nvarchar](10) NULL,
	[Country] [nvarchar](50) NULL,
	[ContactNo] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Website] [nvarchar](150) NULL,
	[GSTIN] [nvarchar](50) NULL,
	[PANNo] [nvarchar](20) NULL,
	[LogoImg] [image] NULL,
	[ImageFile] [nvarchar](150) NULL,
	[AddDate] [datetime] NULL,
	[EditDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[DeleteDate] [datetime] NULL,
 CONSTRAINT [PK_CompanyMaster] PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConsumptionDetail]    Script Date: 11-09-2018 15:32:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConsumptionDetail](
	[ConDId] [bigint] IDENTITY(1,1) NOT NULL,
	[ConId] [bigint] NULL,
	[IMId] [bigint] NULL,
	[RDId] [bigint] NULL,
	[MMId] [bigint] NULL,
	[Qty] [decimal](18, 4) NULL,
	[Rate] [decimal](18, 2) NULL,
	[Total] [decimal](18, 2) NULL,
	[AddDate] [datetime] NULL,
	[EditDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[DeleteDate] [datetime] NULL,
 CONSTRAINT [PK_ConsumptionDetail] PRIMARY KEY CLUSTERED 
(
	[ConDId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConsumptionMaster]    Script Date: 11-09-2018 15:32:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConsumptionMaster](
	[ConId] [bigint] IDENTITY(1,1) NOT NULL,
	[MachinId] [bigint] NULL,
	[Date] [datetime] NULL,
	[AddDate] [datetime] NULL,
	[EditDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[DeleteDate] [datetime] NULL,
	[CompanyId] [bigint] NULL,
 CONSTRAINT [PK_ConsumptionMaster] PRIMARY KEY CLUSTERED 
(
	[ConId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DispatchDetail]    Script Date: 11-09-2018 15:32:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DispatchDetail](
	[DDId] [bigint] IDENTITY(1,1) NOT NULL,
	[DMId] [bigint] NULL,
	[MachineId] [bigint] NULL,
	[Qty] [decimal](18, 4) NULL,
	[Weight] [decimal](18, 4) NULL,
	[Rate] [decimal](18, 2) NULL,
	[Amount] [decimal](18, 2) NULL,
	[AddDate] [datetime] NULL,
	[EditDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[DeleteDate] [datetime] NULL,
 CONSTRAINT [PK_DispatchDetail] PRIMARY KEY CLUSTERED 
(
	[DDId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DispatchMaster]    Script Date: 11-09-2018 15:32:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DispatchMaster](
	[DMId] [bigint] IDENTITY(1,1) NOT NULL,
	[PartyId] [bigint] NULL,
	[DNo] [nvarchar](50) NULL,
	[InvoiceNo] [nvarchar](50) NULL,
	[Date] [datetime] NULL,
	[VehicleNo] [nvarchar](50) NULL,
	[PartyPoNo] [nvarchar](50) NULL,
	[PartyPODate] [datetime] NULL,
	[CompanyId] [bigint] NULL,
	[AddDate] [datetime] NULL,
	[EditDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[DeleteDate] [datetime] NULL,
 CONSTRAINT [PK_DispatchMaster] PRIMARY KEY CLUSTERED 
(
	[DMId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExpenseDetail]    Script Date: 11-09-2018 15:32:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExpenseDetail](
	[EDId] [bigint] IDENTITY(1,1) NOT NULL,
	[EMId] [bigint] NULL,
	[MachineId] [bigint] NULL,
	[Amount] [decimal](18, 2) NULL,
	[CompanyId] [bigint] NULL,
	[AddDate] [datetime] NULL,
	[EditDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[DeleteDate] [datetime] NULL,
 CONSTRAINT [PK_ExpenseDetail] PRIMARY KEY CLUSTERED 
(
	[EDId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExpenseMaster]    Script Date: 11-09-2018 15:32:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExpenseMaster](
	[EMId] [bigint] IDENTITY(1,1) NOT NULL,
	[ExpenseNo] [nvarchar](50) NULL,
	[Date] [datetime] NULL,
	[Salary] [decimal](18, 2) NULL,
	[Transportation] [decimal](18, 2) NULL,
	[Banking] [decimal](18, 2) NULL,
	[Power] [decimal](18, 2) NULL,
	[Fuel] [decimal](18, 2) NULL,
	[Others] [decimal](18, 2) NULL,
	[Admin] [decimal](18, 2) NULL,
	[CompanyId] [bigint] NULL,
	[AddDate] [datetime] NULL,
	[EditDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[DeleteDate] [datetime] NULL,
 CONSTRAINT [PK_ExpenseMaster] PRIMARY KEY CLUSTERED 
(
	[EMId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FormMaster]    Script Date: 11-09-2018 15:32:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FormMaster](
	[FormId] [bigint] IDENTITY(1,1) NOT NULL,
	[FormName] [nvarchar](150) NULL,
	[MenuName] [nvarchar](50) NULL,
 CONSTRAINT [PK_FormMaster] PRIMARY KEY CLUSTERED 
(
	[FormId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InwardMaster]    Script Date: 11-09-2018 15:32:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InwardMaster](
	[InwId] [bigint] IDENTITY(1,1) NOT NULL,
	[RNMId] [bigint] NULL,
	[IMId] [bigint] NULL,
	[InwNo] [nvarchar](50) NULL,
	[Qty] [decimal](18, 4) NULL,
	[Weight] [decimal](18, 4) NULL,
	[Size] [decimal](18, 4) NULL,
	[SizeUnit] [nvarchar](50) NULL,
	[GSM] [nvarchar](150) NULL,
	[BF] [nvarchar](150) NULL,
	[Plybond] [nvarchar](150) NULL,
	[Shade] [nvarchar](150) NULL,
	[IsRew] [bit] NULL,
	[IsClose] [bit] NULL,
	[CloseDate] [datetime] NULL,
	[AddDate] [datetime] NULL,
	[EditDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[DeleteDate] [datetime] NULL,
	[CompanyId] [bigint] NULL,
 CONSTRAINT [PK_InwardMaster] PRIMARY KEY CLUSTERED 
(
	[InwId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemMaster]    Script Date: 11-09-2018 15:32:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemMaster](
	[IMId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Type] [nvarchar](50) NULL,
	[ProcessType] [nvarchar](50) NULL,
	[InwardType] [nvarchar](50) NULL,
	[Qty] [decimal](18, 2) NULL,
	[UOM] [nvarchar](50) NULL,
	[Rate] [decimal](18, 2) NULL,
	[AddDate] [datetime] NULL,
	[EditDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[DeleteDate] [datetime] NULL,
	[CompanyId] [bigint] NULL,
 CONSTRAINT [PK_ItemMaster] PRIMARY KEY CLUSTERED 
(
	[IMId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MachineMaster]    Script Date: 11-09-2018 15:32:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MachineMaster](
	[MachinId] [bigint] IDENTITY(1,1) NOT NULL,
	[Machine] [nvarchar](150) NULL,
	[Date] [datetime] NULL,
	[LotNo] [nvarchar](50) NULL,
	[DegreeInnerDiameter] [nvarchar](50) NULL,
	[Size] [nvarchar](50) NULL,
	[HeightLength] [nvarchar](50) NULL,
	[CSType] [nvarchar](50) NULL,
	[DesignThickness] [nvarchar](50) NULL,
	[Weight] [nvarchar](50) NULL,
	[IsClose] [bit] NULL,
	[AddDate] [datetime] NULL,
	[EditDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[DeleteDate] [datetime] NULL,
	[CompanyId] [bigint] NULL,
 CONSTRAINT [PK_Machine] PRIMARY KEY CLUSTERED 
(
	[MachinId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MixingMaster]    Script Date: 11-09-2018 15:32:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MixingMaster](
	[MMId] [bigint] IDENTITY(1,1) NOT NULL,
	[InwId] [bigint] NULL,
	[IMId] [bigint] NULL,
	[MixingNo] [nvarchar](50) NULL,
	[IssueQty] [decimal](18, 4) NULL,
	[MixingWater] [decimal](18, 4) NULL,
	[FinisheQty] [decimal](18, 4) NULL,
	[IssueWeight] [decimal](18, 4) NULL,
	[Rate] [decimal](18, 2) NULL,
	[AddDate] [datetime] NULL,
	[EditDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[DeleteDate] [datetime] NULL,
	[CompanyId] [bigint] NULL,
 CONSTRAINT [PK_MixingMaster] PRIMARY KEY CLUSTERED 
(
	[MMId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PartyMaster]    Script Date: 11-09-2018 15:32:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PartyMaster](
	[PartyId] [bigint] IDENTITY(1,1) NOT NULL,
	[PartyName] [nvarchar](150) NULL,
	[BillingName] [nvarchar](150) NULL,
	[BillingAddress] [nvarchar](1000) NULL,
	[DeliveryAddress] [nvarchar](1000) NULL,
	[ContactNo] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NULL,
	[Website] [nvarchar](150) NULL,
	[PinCode] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[StateCode] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[GSTIN] [nvarchar](100) NULL,
	[PANNo] [nvarchar](50) NULL,
	[Type] [nvarchar](50) NULL,
	[AddDate] [datetime] NULL,
	[EditDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[DeleteDate] [datetime] NULL,
	[CompanyId] [bigint] NULL,
 CONSTRAINT [PK_PartyMaster] PRIMARY KEY CLUSTERED 
(
	[PartyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductionDetail]    Script Date: 11-09-2018 15:32:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductionDetail](
	[ProDId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProId] [bigint] NULL,
	[MachineId] [bigint] NULL,
	[Production] [decimal](18, 4) NULL,
	[Unit] [nvarchar](50) NULL,
	[Weight] [decimal](18, 4) NULL,
	[AddDate] [datetime] NULL,
	[EditDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[DeleteDate] [datetime] NULL,
 CONSTRAINT [PK_ProductionDetail] PRIMARY KEY CLUSTERED 
(
	[ProDId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductionMaster]    Script Date: 11-09-2018 15:32:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductionMaster](
	[ProId] [bigint] IDENTITY(1,1) NOT NULL,
	[Machine] [nvarchar](50) NULL,
	[Date] [datetime] NULL,
	[Remarks] [nvarchar](1000) NULL,
	[AddDate] [datetime] NULL,
	[EditDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[DeleteDate] [datetime] NULL,
	[CompanyId] [bigint] NULL,
 CONSTRAINT [PK_ProductionMaster] PRIMARY KEY CLUSTERED 
(
	[ProId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReceiveNoteDetail]    Script Date: 11-09-2018 15:32:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReceiveNoteDetail](
	[RNDId] [bigint] IDENTITY(1,1) NOT NULL,
	[RNMId] [bigint] NULL,
	[IMId] [bigint] NULL,
	[Qty] [decimal](18, 4) NULL,
	[InwardQty] [decimal](18, 4) NULL,
	[Weight] [decimal](18, 4) NULL,
	[Rate] [decimal](18, 2) NULL,
	[Discount] [decimal](18, 2) NULL,
	[NetRate] [decimal](18, 2) NULL,
	[CGST] [decimal](18, 2) NULL,
	[SGST] [decimal](18, 2) NULL,
	[IGST] [decimal](18, 2) NULL,
	[OtherCharges] [decimal](18, 2) NULL,
	[Amount] [decimal](18, 2) NULL,
	[IsInward] [bit] NULL,
	[AddDate] [datetime] NULL,
	[EditDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[DeleteDate] [datetime] NULL,
	[CompanyId] [bigint] NULL,
 CONSTRAINT [PK_ReceiveNoteDetail] PRIMARY KEY CLUSTERED 
(
	[RNDId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReceiveNoteMaster]    Script Date: 11-09-2018 15:32:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReceiveNoteMaster](
	[RNMId] [bigint] IDENTITY(1,1) NOT NULL,
	[PartyId] [bigint] NULL,
	[Date] [datetime] NULL,
	[RNNo] [nvarchar](50) NULL,
	[Amount] [decimal](18, 2) NULL,
	[Remarks] [nvarchar](1000) NULL,
	[AddDate] [datetime] NULL,
	[EditDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[DeleteDate] [datetime] NULL,
	[CompanyId] [bigint] NULL,
 CONSTRAINT [PK_ReceiveNoteMaster] PRIMARY KEY CLUSTERED 
(
	[RNMId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RewinderDetail]    Script Date: 11-09-2018 15:32:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RewinderDetail](
	[RewDId] [bigint] IDENTITY(1,1) NOT NULL,
	[RewId] [bigint] NULL,
	[RewNo] [nvarchar](50) NULL,
	[Size] [decimal](18, 4) NULL,
	[Weight] [decimal](18, 4) NULL,
	[IsUse] [bit] NULL,
	[AddDate] [datetime] NULL,
	[EditDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[DeleteDate] [datetime] NULL,
 CONSTRAINT [PK_RewinderDetail] PRIMARY KEY CLUSTERED 
(
	[RewDId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RewinderMaster]    Script Date: 11-09-2018 15:32:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RewinderMaster](
	[RewId] [bigint] IDENTITY(1,1) NOT NULL,
	[InwId] [bigint] NULL,
	[Size] [decimal](18, 4) NULL,
	[RewindeSize] [decimal](18, 4) NULL,
	[AddDate] [datetime] NULL,
	[EditDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[DeleteDate] [datetime] NULL,
	[CompanyId] [bigint] NULL,
 CONSTRAINT [PK_RewinderMaster] PRIMARY KEY CLUSTERED 
(
	[RewId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserMaster]    Script Date: 11-09-2018 15:32:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserMaster](
	[UserId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](150) NULL,
	[Password] [nvarchar](150) NULL,
	[ContactNo] [nvarchar](50) NULL,
	[Type] [nvarchar](50) NULL,
	[AddDate] [datetime] NULL,
	[EditDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[DeleteDate] [datetime] NULL,
	[CompanyId] [bigint] NULL,
 CONSTRAINT [PK_UserMaster] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPermission]    Script Date: 11-09-2018 15:32:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPermission](
	[UPId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NULL,
	[FormId] [bigint] NULL,
	[AddDate] [datetime] NULL,
	[EditDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[DeleteDate] [datetime] NULL,
 CONSTRAINT [PK_UserPermission] PRIMARY KEY CLUSTERED 
(
	[UPId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WasteMaster]    Script Date: 11-09-2018 15:32:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WasteMaster](
	[WMId] [bigint] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[MachineId] [bigint] NULL,
	[WasteQty] [decimal](18, 4) NULL,
	[Amount] [decimal](18, 4) NULL,
	[CompanyId] [bigint] NULL,
	[AddDate] [datetime] NULL,
	[EditDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[DeleteDate] [datetime] NULL,
 CONSTRAINT [PK_WasteMaster] PRIMARY KEY CLUSTERED 
(
	[WMId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[CompanyMaster] ON 
GO
INSERT [dbo].[CompanyMaster] ([CompanyId], [CompanyName], [PersonName], [BillingAddress], [DeliveryAddress], [City], [State], [StateCode], [Country], [ContactNo], [Email], [Website], [GSTIN], [PANNo], [LogoImg], [ImageFile], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (1, N'Krishna Packaging', N'Mr. Vivko', N'Plot Number 123, Near aastha spintex , Morbi chokdi, Halwad.', N'Plot Number 123, Near aastha polyplast, Morbi chokdi, Halwad.', N'Halwad', N'Gujarat', N'24', N'', N'9876543210', N'krishnapackaging@gmail.com', NULL, N'24asdfg2134b2b2', N'asdfg2134b', NULL, N'CompanyLogo-11092018152001.jpg', CAST(N'2018-08-06T18:09:19.673' AS DateTime), NULL, 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[CompanyMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[ConsumptionDetail] ON 
GO
INSERT [dbo].[ConsumptionDetail] ([ConDId], [ConId], [IMId], [RDId], [MMId], [Qty], [Rate], [Total], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (13, 5, 8, 23, NULL, CAST(21.0000 AS Decimal(18, 4)), CAST(25.00 AS Decimal(18, 2)), NULL, CAST(N'2018-08-24T13:28:19.067' AS DateTime), CAST(N'2018-08-24T14:12:56.667' AS DateTime), 0, NULL)
GO
INSERT [dbo].[ConsumptionDetail] ([ConDId], [ConId], [IMId], [RDId], [MMId], [Qty], [Rate], [Total], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (14, 5, 9, NULL, 4, CAST(200.0000 AS Decimal(18, 4)), CAST(6.00 AS Decimal(18, 2)), NULL, CAST(N'2018-08-24T13:28:19.070' AS DateTime), CAST(N'2018-08-24T14:12:56.673' AS DateTime), 0, NULL)
GO
INSERT [dbo].[ConsumptionDetail] ([ConDId], [ConId], [IMId], [RDId], [MMId], [Qty], [Rate], [Total], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (15, 5, 10, NULL, NULL, CAST(100.0000 AS Decimal(18, 4)), CAST(2.00 AS Decimal(18, 2)), NULL, CAST(N'2018-08-24T14:12:56.677' AS DateTime), NULL, 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[ConsumptionDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[ConsumptionMaster] ON 
GO
INSERT [dbo].[ConsumptionMaster] ([ConId], [MachinId], [Date], [AddDate], [EditDate], [IsDelete], [DeleteDate], [CompanyId]) VALUES (5, 7, CAST(N'2018-08-24T13:24:58.987' AS DateTime), CAST(N'2018-08-24T13:28:19.070' AS DateTime), CAST(N'2018-08-24T14:12:56.680' AS DateTime), 0, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[ConsumptionMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[DispatchDetail] ON 
GO
INSERT [dbo].[DispatchDetail] ([DDId], [DMId], [MachineId], [Qty], [Weight], [Rate], [Amount], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (4, 4, 7, CAST(1000.0000 AS Decimal(18, 4)), NULL, CAST(2.00 AS Decimal(18, 2)), CAST(2000.00 AS Decimal(18, 2)), CAST(N'2018-08-24T14:16:14.993' AS DateTime), NULL, 1, CAST(N'2018-09-10T11:05:47.530' AS DateTime))
GO
INSERT [dbo].[DispatchDetail] ([DDId], [DMId], [MachineId], [Qty], [Weight], [Rate], [Amount], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (5, 5, 7, CAST(500.0000 AS Decimal(18, 4)), NULL, CAST(3.00 AS Decimal(18, 2)), CAST(1500.00 AS Decimal(18, 2)), CAST(N'2018-09-11T15:11:49.423' AS DateTime), CAST(N'2018-09-11T15:12:30.870' AS DateTime), 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[DispatchDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[DispatchMaster] ON 
GO
INSERT [dbo].[DispatchMaster] ([DMId], [PartyId], [DNo], [InvoiceNo], [Date], [VehicleNo], [PartyPoNo], [PartyPODate], [CompanyId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (4, 8, N'DIS001', N'inv001', CAST(N'2018-08-24T14:15:10.383' AS DateTime), N'gj 1 ks 8765', N'po001', CAST(N'2018-08-24T14:15:10.363' AS DateTime), 1, CAST(N'2018-08-24T14:16:14.997' AS DateTime), NULL, 1, CAST(N'2018-09-10T11:05:47.547' AS DateTime))
GO
INSERT [dbo].[DispatchMaster] ([DMId], [PartyId], [DNo], [InvoiceNo], [Date], [VehicleNo], [PartyPoNo], [PartyPODate], [CompanyId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (5, 8, N'DIS001', N'001', CAST(N'2018-09-11T15:11:29.627' AS DateTime), N'1021', N'as10', CAST(N'2018-09-11T15:11:29.603' AS DateTime), 1, CAST(N'2018-09-11T15:11:49.423' AS DateTime), CAST(N'2018-09-11T15:12:30.870' AS DateTime), 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[DispatchMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[ExpenseDetail] ON 
GO
INSERT [dbo].[ExpenseDetail] ([EDId], [EMId], [MachineId], [Amount], [CompanyId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (5, 1, 7, CAST(505.00 AS Decimal(18, 2)), 1, CAST(N'2018-09-10T14:20:57.830' AS DateTime), NULL, 1, NULL)
GO
INSERT [dbo].[ExpenseDetail] ([EDId], [EMId], [MachineId], [Amount], [CompanyId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (6, 1, 8, CAST(505.00 AS Decimal(18, 2)), 1, CAST(N'2018-09-10T14:20:57.833' AS DateTime), NULL, 1, NULL)
GO
INSERT [dbo].[ExpenseDetail] ([EDId], [EMId], [MachineId], [Amount], [CompanyId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (7, 2, 7, CAST(109.50 AS Decimal(18, 2)), 1, CAST(N'2018-09-10T17:26:22.417' AS DateTime), NULL, 0, NULL)
GO
INSERT [dbo].[ExpenseDetail] ([EDId], [EMId], [MachineId], [Amount], [CompanyId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (8, 2, 8, CAST(109.50 AS Decimal(18, 2)), 1, CAST(N'2018-09-10T17:26:22.417' AS DateTime), NULL, 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[ExpenseDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[ExpenseMaster] ON 
GO
INSERT [dbo].[ExpenseMaster] ([EMId], [ExpenseNo], [Date], [Salary], [Transportation], [Banking], [Power], [Fuel], [Others], [Admin], [CompanyId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (1, N'1', CAST(N'2018-09-10T13:23:17.747' AS DateTime), CAST(100.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(200.00 AS Decimal(18, 2)), CAST(300.00 AS Decimal(18, 2)), CAST(400.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), 1, CAST(N'2018-09-10T13:24:05.227' AS DateTime), CAST(N'2018-09-10T14:20:57.833' AS DateTime), 1, CAST(N'2018-09-10T17:22:17.113' AS DateTime))
GO
INSERT [dbo].[ExpenseMaster] ([EMId], [ExpenseNo], [Date], [Salary], [Transportation], [Banking], [Power], [Fuel], [Others], [Admin], [CompanyId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (2, N'1', CAST(N'2018-09-10T17:26:10.200' AS DateTime), CAST(120.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(21.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(21.00 AS Decimal(18, 2)), CAST(21.00 AS Decimal(18, 2)), 1, CAST(N'2018-09-10T17:26:22.417' AS DateTime), NULL, 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[ExpenseMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[FormMaster] ON 
GO
INSERT [dbo].[FormMaster] ([FormId], [FormName], [MenuName]) VALUES (1, N'Company', N'&Company')
GO
INSERT [dbo].[FormMaster] ([FormId], [FormName], [MenuName]) VALUES (2, N'User', N'&User')
GO
INSERT [dbo].[FormMaster] ([FormId], [FormName], [MenuName]) VALUES (3, N'Item', N'&Item')
GO
INSERT [dbo].[FormMaster] ([FormId], [FormName], [MenuName]) VALUES (4, N'Party', N'&Party')
GO
INSERT [dbo].[FormMaster] ([FormId], [FormName], [MenuName]) VALUES (5, N'Master', N'&Master')
GO
INSERT [dbo].[FormMaster] ([FormId], [FormName], [MenuName]) VALUES (6, N'Receive Note', N'&Receive Note')
GO
INSERT [dbo].[FormMaster] ([FormId], [FormName], [MenuName]) VALUES (7, N'Inward', N'&Inward')
GO
INSERT [dbo].[FormMaster] ([FormId], [FormName], [MenuName]) VALUES (8, N'Rewinder', N'Re&winder')
GO
INSERT [dbo].[FormMaster] ([FormId], [FormName], [MenuName]) VALUES (9, N'Mixing', N'Mi&xing')
GO
INSERT [dbo].[FormMaster] ([FormId], [FormName], [MenuName]) VALUES (10, N'Lot No', N'&Lot No')
GO
INSERT [dbo].[FormMaster] ([FormId], [FormName], [MenuName]) VALUES (11, N'Consumption', N'&Consumption')
GO
INSERT [dbo].[FormMaster] ([FormId], [FormName], [MenuName]) VALUES (12, N'Production', N'&Production')
GO
INSERT [dbo].[FormMaster] ([FormId], [FormName], [MenuName]) VALUES (13, N'Stock', N'S&tock')
GO
INSERT [dbo].[FormMaster] ([FormId], [FormName], [MenuName]) VALUES (14, N'Dispatch', N'&Dispatch')
GO
INSERT [dbo].[FormMaster] ([FormId], [FormName], [MenuName]) VALUES (15, N'Expense', N'&Expense')
GO
INSERT [dbo].[FormMaster] ([FormId], [FormName], [MenuName]) VALUES (16, N'Waste', N'&Waste')
GO
INSERT [dbo].[FormMaster] ([FormId], [FormName], [MenuName]) VALUES (17, N'Report', N'R&eport')
GO
INSERT [dbo].[FormMaster] ([FormId], [FormName], [MenuName]) VALUES (18, N'Costing', N'&Costing')
GO
INSERT [dbo].[FormMaster] ([FormId], [FormName], [MenuName]) VALUES (19, N'ReceiveNote', N'&ReceiveNote')
GO
INSERT [dbo].[FormMaster] ([FormId], [FormName], [MenuName]) VALUES (20, N'Rewinder', N'Re&winder')
GO
INSERT [dbo].[FormMaster] ([FormId], [FormName], [MenuName]) VALUES (21, N'Mixing', N'&Mixing')
GO
INSERT [dbo].[FormMaster] ([FormId], [FormName], [MenuName]) VALUES (22, N'Dispatch', N'&Dispatch')
GO
SET IDENTITY_INSERT [dbo].[FormMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[InwardMaster] ON 
GO
INSERT [dbo].[InwardMaster] ([InwId], [RNMId], [IMId], [InwNo], [Qty], [Weight], [Size], [SizeUnit], [GSM], [BF], [Plybond], [Shade], [IsRew], [IsClose], [CloseDate], [AddDate], [EditDate], [IsDelete], [DeleteDate], [CompanyId]) VALUES (27, 6, 8, N'INW001', CAST(1.0000 AS Decimal(18, 4)), CAST(200.0000 AS Decimal(18, 4)), CAST(1000.0000 AS Decimal(18, 4)), N'MM', NULL, NULL, NULL, NULL, 1, 0, NULL, CAST(N'2018-08-24T12:51:28.413' AS DateTime), NULL, 0, NULL, 1)
GO
INSERT [dbo].[InwardMaster] ([InwId], [RNMId], [IMId], [InwNo], [Qty], [Weight], [Size], [SizeUnit], [GSM], [BF], [Plybond], [Shade], [IsRew], [IsClose], [CloseDate], [AddDate], [EditDate], [IsDelete], [DeleteDate], [CompanyId]) VALUES (28, 6, 8, N'INW002', CAST(1.0000 AS Decimal(18, 4)), CAST(100.0000 AS Decimal(18, 4)), CAST(1000.0000 AS Decimal(18, 4)), N'MM', NULL, NULL, NULL, NULL, 0, 0, NULL, CAST(N'2018-08-24T12:51:53.163' AS DateTime), NULL, 0, NULL, 1)
GO
INSERT [dbo].[InwardMaster] ([InwId], [RNMId], [IMId], [InwNo], [Qty], [Weight], [Size], [SizeUnit], [GSM], [BF], [Plybond], [Shade], [IsRew], [IsClose], [CloseDate], [AddDate], [EditDate], [IsDelete], [DeleteDate], [CompanyId]) VALUES (29, 8, 9, N'INW003', CAST(50.0000 AS Decimal(18, 4)), CAST(50.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), N'MM', NULL, NULL, NULL, NULL, 0, 0, NULL, CAST(N'2018-08-24T13:11:09.213' AS DateTime), NULL, 0, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[InwardMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[ItemMaster] ON 
GO
INSERT [dbo].[ItemMaster] ([IMId], [Name], [Type], [ProcessType], [InwardType], [Qty], [UOM], [Rate], [AddDate], [EditDate], [IsDelete], [DeleteDate], [CompanyId]) VALUES (8, N'Paper Reel', N'Raw Material', N'Re-windable', N'Single', CAST(10.00 AS Decimal(18, 2)), N'Kg', CAST(25.00 AS Decimal(18, 2)), CAST(N'2018-08-24T12:17:17.737' AS DateTime), CAST(N'2018-08-24T12:34:16.410' AS DateTime), 0, NULL, 1)
GO
INSERT [dbo].[ItemMaster] ([IMId], [Name], [Type], [ProcessType], [InwardType], [Qty], [UOM], [Rate], [AddDate], [EditDate], [IsDelete], [DeleteDate], [CompanyId]) VALUES (9, N'Hot Gum', N'Raw Material', N'Mixable', N'Multiple', CAST(50.00 AS Decimal(18, 2)), N'Kg', CAST(12.00 AS Decimal(18, 2)), CAST(N'2018-08-24T12:17:43.830' AS DateTime), NULL, 0, NULL, 1)
GO
INSERT [dbo].[ItemMaster] ([IMId], [Name], [Type], [ProcessType], [InwardType], [Qty], [UOM], [Rate], [AddDate], [EditDate], [IsDelete], [DeleteDate], [CompanyId]) VALUES (10, N'Packing Tape', N'Packing', N'No Applicable', N'No Applicable', CAST(0.00 AS Decimal(18, 2)), N'No', CAST(2.00 AS Decimal(18, 2)), CAST(N'2018-08-24T12:18:16.347' AS DateTime), NULL, 0, NULL, 1)
GO
INSERT [dbo].[ItemMaster] ([IMId], [Name], [Type], [ProcessType], [InwardType], [Qty], [UOM], [Rate], [AddDate], [EditDate], [IsDelete], [DeleteDate], [CompanyId]) VALUES (11, N'TEST', N'Raw Material', N'Mixable', N'Single', CAST(10.00 AS Decimal(18, 2)), N'No', CAST(10.00 AS Decimal(18, 2)), CAST(N'2018-08-24T12:42:54.233' AS DateTime), NULL, 0, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[ItemMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[MachineMaster] ON 
GO
INSERT [dbo].[MachineMaster] ([MachinId], [Machine], [Date], [LotNo], [DegreeInnerDiameter], [Size], [HeightLength], [CSType], [DesignThickness], [Weight], [IsClose], [AddDate], [EditDate], [IsDelete], [DeleteDate], [CompanyId]) VALUES (7, N'Core Pipe', CAST(N'2018-08-24T13:23:02.700' AS DateTime), N'CP001', N'2', N'45', N'12', N'12', N'12', N'2', 0, CAST(N'2018-08-24T13:24:43.930' AS DateTime), NULL, 0, NULL, 1)
GO
INSERT [dbo].[MachineMaster] ([MachinId], [Machine], [Date], [LotNo], [DegreeInnerDiameter], [Size], [HeightLength], [CSType], [DesignThickness], [Weight], [IsClose], [AddDate], [EditDate], [IsDelete], [DeleteDate], [CompanyId]) VALUES (8, N'Core Pipe', CAST(N'2018-09-10T12:47:39.860' AS DateTime), N'CP002', N'1', N'21', N'12', N'21', N'12', N'1', 0, CAST(N'2018-09-10T12:47:52.177' AS DateTime), NULL, 0, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[MachineMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[MixingMaster] ON 
GO
INSERT [dbo].[MixingMaster] ([MMId], [InwId], [IMId], [MixingNo], [IssueQty], [MixingWater], [FinisheQty], [IssueWeight], [Rate], [AddDate], [EditDate], [IsDelete], [DeleteDate], [CompanyId]) VALUES (4, 29, 9, N'MIX001', CAST(10.0000 AS Decimal(18, 4)), CAST(200.0000 AS Decimal(18, 4)), CAST(1000.0000 AS Decimal(18, 4)), CAST(200.0000 AS Decimal(18, 4)), CAST(6.00 AS Decimal(18, 2)), CAST(N'2018-08-24T13:13:06.627' AS DateTime), NULL, 0, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[MixingMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[PartyMaster] ON 
GO
INSERT [dbo].[PartyMaster] ([PartyId], [PartyName], [BillingName], [BillingAddress], [DeliveryAddress], [ContactNo], [Email], [Website], [PinCode], [City], [State], [StateCode], [Country], [GSTIN], [PANNo], [Type], [AddDate], [EditDate], [IsDelete], [DeleteDate], [CompanyId]) VALUES (6, N'Mahaveer Enterprise', N'Mahaveer Enterprise', N'10, Pragati Complex, Main Road, Halwad.', N'Plot Number 123, Near aastha Polyplast, Morbi chokdi, Halwad.', N'6549873210', N'mahaveer@gmail.com', NULL, N'382150', N'Halwad', N'Gujarat', N'24', NULL, N'24abkfg2176b1b2', N'abkfg2176b', N'Purchase', CAST(N'2018-08-24T12:22:53.330' AS DateTime), NULL, 0, NULL, 1)
GO
INSERT [dbo].[PartyMaster] ([PartyId], [PartyName], [BillingName], [BillingAddress], [DeliveryAddress], [ContactNo], [Email], [Website], [PinCode], [City], [State], [StateCode], [Country], [GSTIN], [PANNo], [Type], [AddDate], [EditDate], [IsDelete], [DeleteDate], [CompanyId]) VALUES (7, N'Kishan Powder', N'Kishan Powder', N'10, Pragati Complex, Main Road, Halwad.', N'Plot Number 123, Near aastha spintex , Morbi chokdi, Halwad.', N'9874561230', N'', NULL, N'321456', N'Surendranagar', N'Gujarat', N'24', NULL, N'24abhjg2176b1b3', N'abhjg2176b', N'Purchase', CAST(N'2018-08-24T12:24:06.367' AS DateTime), NULL, 0, NULL, 1)
GO
INSERT [dbo].[PartyMaster] ([PartyId], [PartyName], [BillingName], [BillingAddress], [DeliveryAddress], [ContactNo], [Email], [Website], [PinCode], [City], [State], [StateCode], [Country], [GSTIN], [PANNo], [Type], [AddDate], [EditDate], [IsDelete], [DeleteDate], [CompanyId]) VALUES (8, N'KAJARIYA TILES', N'KAJARIYA TILES', N'AHMEDABD', N'AHMEDABD', N'321045697', N'kajariya@gmail.com', NULL, N'380060', N'Ahmedabad', N'Gujarat', N'24', NULL, N'24asdfg2176b1e3', N'asdfg2176b', N'Sales', CAST(N'2018-08-24T14:15:02.800' AS DateTime), NULL, 0, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[PartyMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductionDetail] ON 
GO
INSERT [dbo].[ProductionDetail] ([ProDId], [ProId], [MachineId], [Production], [Unit], [Weight], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (5, 4, 7, CAST(1000.0000 AS Decimal(18, 4)), N'No', CAST(2.0000 AS Decimal(18, 4)), CAST(N'2018-08-24T13:31:12.307' AS DateTime), NULL, 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[ProductionDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductionMaster] ON 
GO
INSERT [dbo].[ProductionMaster] ([ProId], [Machine], [Date], [Remarks], [AddDate], [EditDate], [IsDelete], [DeleteDate], [CompanyId]) VALUES (4, N'Core Pipe', CAST(N'2018-08-24T13:29:41.910' AS DateTime), NULL, CAST(N'2018-08-24T13:31:12.307' AS DateTime), NULL, 0, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[ProductionMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[ReceiveNoteDetail] ON 
GO
INSERT [dbo].[ReceiveNoteDetail] ([RNDId], [RNMId], [IMId], [Qty], [InwardQty], [Weight], [Rate], [Discount], [NetRate], [CGST], [SGST], [IGST], [OtherCharges], [Amount], [IsInward], [AddDate], [EditDate], [IsDelete], [DeleteDate], [CompanyId]) VALUES (8, 6, 8, CAST(10.0000 AS Decimal(18, 4)), CAST(2.0000 AS Decimal(18, 4)), CAST(2000.0000 AS Decimal(18, 4)), CAST(25.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(25.00 AS Decimal(18, 2)), CAST(2.50 AS Decimal(18, 2)), CAST(2.50 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(52500.00 AS Decimal(18, 2)), 0, CAST(N'2018-08-24T12:30:55.673' AS DateTime), CAST(N'2018-08-24T12:47:13.603' AS DateTime), 0, NULL, 1)
GO
INSERT [dbo].[ReceiveNoteDetail] ([RNDId], [RNMId], [IMId], [Qty], [InwardQty], [Weight], [Rate], [Discount], [NetRate], [CGST], [SGST], [IGST], [OtherCharges], [Amount], [IsInward], [AddDate], [EditDate], [IsDelete], [DeleteDate], [CompanyId]) VALUES (9, 7, 11, CAST(10.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(10.0000 AS Decimal(18, 4)), CAST(10.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(2.50 AS Decimal(18, 2)), CAST(2.50 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(105.00 AS Decimal(18, 2)), 0, CAST(N'2018-08-24T12:45:03.863' AS DateTime), CAST(N'2018-08-24T12:47:08.003' AS DateTime), 0, NULL, 1)
GO
INSERT [dbo].[ReceiveNoteDetail] ([RNDId], [RNMId], [IMId], [Qty], [InwardQty], [Weight], [Rate], [Discount], [NetRate], [CGST], [SGST], [IGST], [OtherCharges], [Amount], [IsInward], [AddDate], [EditDate], [IsDelete], [DeleteDate], [CompanyId]) VALUES (10, 8, 9, CAST(50.0000 AS Decimal(18, 4)), CAST(50.0000 AS Decimal(18, 4)), CAST(2500.0000 AS Decimal(18, 4)), CAST(12.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(2.50 AS Decimal(18, 2)), CAST(2.50 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(31500.00 AS Decimal(18, 2)), 1, CAST(N'2018-08-24T13:08:32.160' AS DateTime), CAST(N'2018-08-24T13:09:38.423' AS DateTime), 0, NULL, 1)
GO
INSERT [dbo].[ReceiveNoteDetail] ([RNDId], [RNMId], [IMId], [Qty], [InwardQty], [Weight], [Rate], [Discount], [NetRate], [CGST], [SGST], [IGST], [OtherCharges], [Amount], [IsInward], [AddDate], [EditDate], [IsDelete], [DeleteDate], [CompanyId]) VALUES (11, 9, 10, CAST(100.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(100.0000 AS Decimal(18, 4)), CAST(2.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(2.00 AS Decimal(18, 2)), CAST(2.50 AS Decimal(18, 2)), CAST(2.50 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(210.00 AS Decimal(18, 2)), 0, CAST(N'2018-08-24T14:12:18.003' AS DateTime), NULL, 0, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[ReceiveNoteDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[ReceiveNoteMaster] ON 
GO
INSERT [dbo].[ReceiveNoteMaster] ([RNMId], [PartyId], [Date], [RNNo], [Amount], [Remarks], [AddDate], [EditDate], [IsDelete], [DeleteDate], [CompanyId]) VALUES (6, 6, CAST(N'2018-08-24T12:24:29.373' AS DateTime), N'ME001', CAST(52500.00 AS Decimal(18, 2)), NULL, CAST(N'2018-08-24T12:30:55.673' AS DateTime), CAST(N'2018-08-24T12:47:13.603' AS DateTime), 0, NULL, 1)
GO
INSERT [dbo].[ReceiveNoteMaster] ([RNMId], [PartyId], [Date], [RNNo], [Amount], [Remarks], [AddDate], [EditDate], [IsDelete], [DeleteDate], [CompanyId]) VALUES (7, 6, CAST(N'2018-08-24T12:43:03.223' AS DateTime), N'ME002', CAST(105.00 AS Decimal(18, 2)), NULL, CAST(N'2018-08-24T12:45:06.733' AS DateTime), CAST(N'2018-08-24T12:47:08.010' AS DateTime), 0, NULL, 1)
GO
INSERT [dbo].[ReceiveNoteMaster] ([RNMId], [PartyId], [Date], [RNNo], [Amount], [Remarks], [AddDate], [EditDate], [IsDelete], [DeleteDate], [CompanyId]) VALUES (8, 7, CAST(N'2018-08-24T13:07:09.633' AS DateTime), N'KP001', CAST(31500.00 AS Decimal(18, 2)), NULL, CAST(N'2018-08-24T13:08:32.160' AS DateTime), CAST(N'2018-08-24T13:09:38.423' AS DateTime), 0, NULL, 1)
GO
INSERT [dbo].[ReceiveNoteMaster] ([RNMId], [PartyId], [Date], [RNNo], [Amount], [Remarks], [AddDate], [EditDate], [IsDelete], [DeleteDate], [CompanyId]) VALUES (9, 6, CAST(N'2018-08-24T14:11:36.913' AS DateTime), N'ME003', CAST(210.00 AS Decimal(18, 2)), NULL, CAST(N'2018-08-24T14:12:18.007' AS DateTime), NULL, 0, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[ReceiveNoteMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[RewinderDetail] ON 
GO
INSERT [dbo].[RewinderDetail] ([RewDId], [RewId], [RewNo], [Size], [Weight], [IsUse], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (23, 6, N'INW001-1', CAST(105.0000 AS Decimal(18, 4)), CAST(21.0000 AS Decimal(18, 4)), 1, CAST(N'2018-08-24T12:53:59.850' AS DateTime), CAST(N'2018-08-24T12:54:17.220' AS DateTime), 0, NULL)
GO
INSERT [dbo].[RewinderDetail] ([RewDId], [RewId], [RewNo], [Size], [Weight], [IsUse], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (24, 6, N'INW001-2', CAST(500.0000 AS Decimal(18, 4)), CAST(100.0000 AS Decimal(18, 4)), 0, CAST(N'2018-08-24T12:54:56.423' AS DateTime), NULL, 0, NULL)
GO
INSERT [dbo].[RewinderDetail] ([RewDId], [RewId], [RewNo], [Size], [Weight], [IsUse], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (25, 6, N'INW001-3', CAST(300.0000 AS Decimal(18, 4)), CAST(60.0000 AS Decimal(18, 4)), 0, CAST(N'2018-08-24T12:55:38.147' AS DateTime), NULL, 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[RewinderDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[RewinderMaster] ON 
GO
INSERT [dbo].[RewinderMaster] ([RewId], [InwId], [Size], [RewindeSize], [AddDate], [EditDate], [IsDelete], [DeleteDate], [CompanyId]) VALUES (6, 27, NULL, NULL, CAST(N'2018-08-24T12:56:05.907' AS DateTime), NULL, 0, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[RewinderMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[UserMaster] ON 
GO
INSERT [dbo].[UserMaster] ([UserId], [UserName], [Password], [ContactNo], [Type], [AddDate], [EditDate], [IsDelete], [DeleteDate], [CompanyId]) VALUES (2, N'admin', N'123', NULL, N'admin', CAST(N'2018-08-06T18:09:23.523' AS DateTime), NULL, 0, NULL, 1)
GO
INSERT [dbo].[UserMaster] ([UserId], [UserName], [Password], [ContactNo], [Type], [AddDate], [EditDate], [IsDelete], [DeleteDate], [CompanyId]) VALUES (6, N'sanju', N'123', NULL, N'User', CAST(N'2018-08-24T12:14:02.807' AS DateTime), CAST(N'2018-09-11T15:29:41.057' AS DateTime), 0, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[UserMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[UserPermission] ON 
GO
INSERT [dbo].[UserPermission] ([UPId], [UserId], [FormId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (1, 2, 1, NULL, NULL, 0, NULL)
GO
INSERT [dbo].[UserPermission] ([UPId], [UserId], [FormId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (2, 2, 2, NULL, NULL, 0, NULL)
GO
INSERT [dbo].[UserPermission] ([UPId], [UserId], [FormId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (3, 2, 3, NULL, NULL, 0, NULL)
GO
INSERT [dbo].[UserPermission] ([UPId], [UserId], [FormId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (4, 2, 4, NULL, NULL, 0, NULL)
GO
INSERT [dbo].[UserPermission] ([UPId], [UserId], [FormId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (5, 2, 5, NULL, NULL, 0, NULL)
GO
INSERT [dbo].[UserPermission] ([UPId], [UserId], [FormId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (6, 2, 6, NULL, NULL, 0, NULL)
GO
INSERT [dbo].[UserPermission] ([UPId], [UserId], [FormId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (10, 2, 7, NULL, NULL, 0, NULL)
GO
INSERT [dbo].[UserPermission] ([UPId], [UserId], [FormId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (11, 2, 8, NULL, NULL, 0, NULL)
GO
INSERT [dbo].[UserPermission] ([UPId], [UserId], [FormId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (12, 2, 9, NULL, NULL, 0, NULL)
GO
INSERT [dbo].[UserPermission] ([UPId], [UserId], [FormId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (13, 2, 10, NULL, NULL, 0, NULL)
GO
INSERT [dbo].[UserPermission] ([UPId], [UserId], [FormId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (14, 2, 11, NULL, NULL, 0, NULL)
GO
INSERT [dbo].[UserPermission] ([UPId], [UserId], [FormId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (15, 2, 12, NULL, NULL, 0, NULL)
GO
INSERT [dbo].[UserPermission] ([UPId], [UserId], [FormId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (16, 2, 13, NULL, NULL, 0, NULL)
GO
INSERT [dbo].[UserPermission] ([UPId], [UserId], [FormId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (17, 2, 14, NULL, NULL, 0, NULL)
GO
INSERT [dbo].[UserPermission] ([UPId], [UserId], [FormId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (35, 2, 15, NULL, NULL, 0, NULL)
GO
INSERT [dbo].[UserPermission] ([UPId], [UserId], [FormId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (36, 2, 16, NULL, NULL, 0, NULL)
GO
INSERT [dbo].[UserPermission] ([UPId], [UserId], [FormId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (37, 2, 17, NULL, NULL, 0, NULL)
GO
INSERT [dbo].[UserPermission] ([UPId], [UserId], [FormId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (38, 2, 18, NULL, NULL, 0, NULL)
GO
INSERT [dbo].[UserPermission] ([UPId], [UserId], [FormId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (40, 2, 19, NULL, NULL, 0, NULL)
GO
INSERT [dbo].[UserPermission] ([UPId], [UserId], [FormId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (41, 2, 20, NULL, NULL, 0, NULL)
GO
INSERT [dbo].[UserPermission] ([UPId], [UserId], [FormId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (42, 2, 21, NULL, NULL, 0, NULL)
GO
INSERT [dbo].[UserPermission] ([UPId], [UserId], [FormId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (43, 2, 22, NULL, NULL, 0, NULL)
GO
INSERT [dbo].[UserPermission] ([UPId], [UserId], [FormId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (44, 6, 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[UserPermission] ([UPId], [UserId], [FormId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (45, 6, 12, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[UserPermission] ([UPId], [UserId], [FormId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (46, 6, 13, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[UserPermission] ([UPId], [UserId], [FormId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (47, 6, 14, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[UserPermission] ([UPId], [UserId], [FormId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (48, 6, 17, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[UserPermission] ([UPId], [UserId], [FormId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (49, 6, 18, NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[UserPermission] OFF
GO
SET IDENTITY_INSERT [dbo].[WasteMaster] ON 
GO
INSERT [dbo].[WasteMaster] ([WMId], [Date], [MachineId], [WasteQty], [Amount], [CompanyId], [AddDate], [EditDate], [IsDelete], [DeleteDate]) VALUES (1, CAST(N'2018-09-10T15:03:32.313' AS DateTime), 7, CAST(122.0000 AS Decimal(18, 4)), CAST(120.0000 AS Decimal(18, 4)), 1, CAST(N'2018-09-10T15:03:32.313' AS DateTime), CAST(N'2018-09-10T16:41:30.873' AS DateTime), 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[WasteMaster] OFF
GO
ALTER TABLE [dbo].[CompanyMaster] ADD  CONSTRAINT [DF_CompanyMaster_AddDate]  DEFAULT (getdate()) FOR [AddDate]
GO
ALTER TABLE [dbo].[CompanyMaster] ADD  CONSTRAINT [DF_CompanyMaster_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[ConsumptionDetail] ADD  CONSTRAINT [DF_ConsumptionDetail_AddDate]  DEFAULT (getdate()) FOR [AddDate]
GO
ALTER TABLE [dbo].[ConsumptionDetail] ADD  CONSTRAINT [DF_ConsumptionDetail_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[ConsumptionMaster] ADD  CONSTRAINT [DF_ConsumptionMaster_AddDate]  DEFAULT (getdate()) FOR [AddDate]
GO
ALTER TABLE [dbo].[ConsumptionMaster] ADD  CONSTRAINT [DF_ConsumptionMaster_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[InwardMaster] ADD  CONSTRAINT [DF_InwardMaster_AddDate]  DEFAULT (getdate()) FOR [AddDate]
GO
ALTER TABLE [dbo].[InwardMaster] ADD  CONSTRAINT [DF_InwardMaster_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[ItemMaster] ADD  CONSTRAINT [DF_ItemMaster_AddDate]  DEFAULT (getdate()) FOR [AddDate]
GO
ALTER TABLE [dbo].[ItemMaster] ADD  CONSTRAINT [DF_ItemMaster_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[MachineMaster] ADD  CONSTRAINT [DF_Machine_IsClose]  DEFAULT ((0)) FOR [IsClose]
GO
ALTER TABLE [dbo].[MachineMaster] ADD  CONSTRAINT [DF_Machine_AddDate]  DEFAULT (getdate()) FOR [AddDate]
GO
ALTER TABLE [dbo].[MachineMaster] ADD  CONSTRAINT [DF_Machine_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[MixingMaster] ADD  CONSTRAINT [DF_MixingMaster_AddDate]  DEFAULT (getdate()) FOR [AddDate]
GO
ALTER TABLE [dbo].[MixingMaster] ADD  CONSTRAINT [DF_MixingMaster_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[PartyMaster] ADD  CONSTRAINT [DF_PartyMaster_Contry]  DEFAULT ('India') FOR [Country]
GO
ALTER TABLE [dbo].[PartyMaster] ADD  CONSTRAINT [DF_PartyMaster_Sales]  DEFAULT ((0)) FOR [Type]
GO
ALTER TABLE [dbo].[PartyMaster] ADD  CONSTRAINT [DF_PartyMaster_AddDate]  DEFAULT (getdate()) FOR [AddDate]
GO
ALTER TABLE [dbo].[PartyMaster] ADD  CONSTRAINT [DF_PartyMaster_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[ProductionDetail] ADD  CONSTRAINT [DF_ProductionDetail_AddDate]  DEFAULT (getdate()) FOR [AddDate]
GO
ALTER TABLE [dbo].[ProductionDetail] ADD  CONSTRAINT [DF_ProductionDetail_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[ProductionMaster] ADD  CONSTRAINT [DF_ProductionMaster_AddDate]  DEFAULT (getdate()) FOR [AddDate]
GO
ALTER TABLE [dbo].[ProductionMaster] ADD  CONSTRAINT [DF_ProductionMaster_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[ReceiveNoteDetail] ADD  CONSTRAINT [DF_ReceiveNoteDetail_InwardQty]  DEFAULT ((0)) FOR [InwardQty]
GO
ALTER TABLE [dbo].[ReceiveNoteDetail] ADD  CONSTRAINT [DF_ReceiveNoteDetail_AddDate]  DEFAULT (getdate()) FOR [AddDate]
GO
ALTER TABLE [dbo].[ReceiveNoteDetail] ADD  CONSTRAINT [DF_ReceiveNoteDetail_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[ReceiveNoteMaster] ADD  CONSTRAINT [DF_ReceiveNoteMaster_AddDate]  DEFAULT (getdate()) FOR [AddDate]
GO
ALTER TABLE [dbo].[ReceiveNoteMaster] ADD  CONSTRAINT [DF_ReceiveNoteMaster_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[RewinderDetail] ADD  CONSTRAINT [DF_RewinderDetail_AddDate]  DEFAULT (getdate()) FOR [AddDate]
GO
ALTER TABLE [dbo].[RewinderDetail] ADD  CONSTRAINT [DF_RewinderDetail_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[RewinderMaster] ADD  CONSTRAINT [DF_RewinderMaster_AddDate]  DEFAULT (getdate()) FOR [AddDate]
GO
ALTER TABLE [dbo].[RewinderMaster] ADD  CONSTRAINT [DF_RewinderMaster_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[UserMaster] ADD  CONSTRAINT [DF_UserMaster_AddDate]  DEFAULT (getdate()) FOR [AddDate]
GO
ALTER TABLE [dbo].[UserMaster] ADD  CONSTRAINT [DF_UserMaster_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[ConsumptionDetail]  WITH CHECK ADD  CONSTRAINT [FK_ConsumptionDetail_ConsumptionMaster] FOREIGN KEY([ConId])
REFERENCES [dbo].[ConsumptionMaster] ([ConId])
GO
ALTER TABLE [dbo].[ConsumptionDetail] CHECK CONSTRAINT [FK_ConsumptionDetail_ConsumptionMaster]
GO
ALTER TABLE [dbo].[ConsumptionDetail]  WITH CHECK ADD  CONSTRAINT [FK_ConsumptionDetail_ItemMaster] FOREIGN KEY([IMId])
REFERENCES [dbo].[ItemMaster] ([IMId])
GO
ALTER TABLE [dbo].[ConsumptionDetail] CHECK CONSTRAINT [FK_ConsumptionDetail_ItemMaster]
GO
ALTER TABLE [dbo].[ConsumptionDetail]  WITH CHECK ADD  CONSTRAINT [FK_ConsumptionDetail_MixingMaster] FOREIGN KEY([MMId])
REFERENCES [dbo].[MixingMaster] ([MMId])
GO
ALTER TABLE [dbo].[ConsumptionDetail] CHECK CONSTRAINT [FK_ConsumptionDetail_MixingMaster]
GO
ALTER TABLE [dbo].[ConsumptionDetail]  WITH CHECK ADD  CONSTRAINT [FK_ConsumptionDetail_RewinderDetail] FOREIGN KEY([RDId])
REFERENCES [dbo].[RewinderDetail] ([RewDId])
GO
ALTER TABLE [dbo].[ConsumptionDetail] CHECK CONSTRAINT [FK_ConsumptionDetail_RewinderDetail]
GO
ALTER TABLE [dbo].[ConsumptionMaster]  WITH CHECK ADD  CONSTRAINT [FK_ConsumptionMaster_CompanyMaster] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[CompanyMaster] ([CompanyId])
GO
ALTER TABLE [dbo].[ConsumptionMaster] CHECK CONSTRAINT [FK_ConsumptionMaster_CompanyMaster]
GO
ALTER TABLE [dbo].[ConsumptionMaster]  WITH CHECK ADD  CONSTRAINT [FK_ConsumptionMaster_MachineMaster] FOREIGN KEY([MachinId])
REFERENCES [dbo].[MachineMaster] ([MachinId])
GO
ALTER TABLE [dbo].[ConsumptionMaster] CHECK CONSTRAINT [FK_ConsumptionMaster_MachineMaster]
GO
ALTER TABLE [dbo].[DispatchDetail]  WITH CHECK ADD  CONSTRAINT [FK_DispatchDetail_DispatchMaster] FOREIGN KEY([DMId])
REFERENCES [dbo].[DispatchMaster] ([DMId])
GO
ALTER TABLE [dbo].[DispatchDetail] CHECK CONSTRAINT [FK_DispatchDetail_DispatchMaster]
GO
ALTER TABLE [dbo].[DispatchDetail]  WITH CHECK ADD  CONSTRAINT [FK_DispatchDetail_MachineMaster] FOREIGN KEY([MachineId])
REFERENCES [dbo].[MachineMaster] ([MachinId])
GO
ALTER TABLE [dbo].[DispatchDetail] CHECK CONSTRAINT [FK_DispatchDetail_MachineMaster]
GO
ALTER TABLE [dbo].[DispatchMaster]  WITH CHECK ADD  CONSTRAINT [FK_DispatchMaster_CompanyMaster] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[CompanyMaster] ([CompanyId])
GO
ALTER TABLE [dbo].[DispatchMaster] CHECK CONSTRAINT [FK_DispatchMaster_CompanyMaster]
GO
ALTER TABLE [dbo].[DispatchMaster]  WITH CHECK ADD  CONSTRAINT [FK_DispatchMaster_PartyMaster] FOREIGN KEY([PartyId])
REFERENCES [dbo].[PartyMaster] ([PartyId])
GO
ALTER TABLE [dbo].[DispatchMaster] CHECK CONSTRAINT [FK_DispatchMaster_PartyMaster]
GO
ALTER TABLE [dbo].[ExpenseDetail]  WITH CHECK ADD  CONSTRAINT [FK_ExpenseDetail_CompanyMaster] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[CompanyMaster] ([CompanyId])
GO
ALTER TABLE [dbo].[ExpenseDetail] CHECK CONSTRAINT [FK_ExpenseDetail_CompanyMaster]
GO
ALTER TABLE [dbo].[ExpenseDetail]  WITH CHECK ADD  CONSTRAINT [FK_ExpenseDetail_ExpenseMaster] FOREIGN KEY([EMId])
REFERENCES [dbo].[ExpenseMaster] ([EMId])
GO
ALTER TABLE [dbo].[ExpenseDetail] CHECK CONSTRAINT [FK_ExpenseDetail_ExpenseMaster]
GO
ALTER TABLE [dbo].[ExpenseDetail]  WITH CHECK ADD  CONSTRAINT [FK_ExpenseDetail_MachineMaster] FOREIGN KEY([MachineId])
REFERENCES [dbo].[MachineMaster] ([MachinId])
GO
ALTER TABLE [dbo].[ExpenseDetail] CHECK CONSTRAINT [FK_ExpenseDetail_MachineMaster]
GO
ALTER TABLE [dbo].[ExpenseMaster]  WITH CHECK ADD  CONSTRAINT [FK_ExpenseMaster_CompanyMaster] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[CompanyMaster] ([CompanyId])
GO
ALTER TABLE [dbo].[ExpenseMaster] CHECK CONSTRAINT [FK_ExpenseMaster_CompanyMaster]
GO
ALTER TABLE [dbo].[InwardMaster]  WITH CHECK ADD  CONSTRAINT [FK_InwardMaster_CompanyMaster] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[CompanyMaster] ([CompanyId])
GO
ALTER TABLE [dbo].[InwardMaster] CHECK CONSTRAINT [FK_InwardMaster_CompanyMaster]
GO
ALTER TABLE [dbo].[InwardMaster]  WITH CHECK ADD  CONSTRAINT [FK_InwardMaster_ItemMaster] FOREIGN KEY([IMId])
REFERENCES [dbo].[ItemMaster] ([IMId])
GO
ALTER TABLE [dbo].[InwardMaster] CHECK CONSTRAINT [FK_InwardMaster_ItemMaster]
GO
ALTER TABLE [dbo].[InwardMaster]  WITH CHECK ADD  CONSTRAINT [FK_InwardMaster_ReceiveNoteMaster] FOREIGN KEY([RNMId])
REFERENCES [dbo].[ReceiveNoteMaster] ([RNMId])
GO
ALTER TABLE [dbo].[InwardMaster] CHECK CONSTRAINT [FK_InwardMaster_ReceiveNoteMaster]
GO
ALTER TABLE [dbo].[ItemMaster]  WITH CHECK ADD  CONSTRAINT [FK_ItemMaster_CompanyMaster] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[CompanyMaster] ([CompanyId])
GO
ALTER TABLE [dbo].[ItemMaster] CHECK CONSTRAINT [FK_ItemMaster_CompanyMaster]
GO
ALTER TABLE [dbo].[MachineMaster]  WITH CHECK ADD  CONSTRAINT [FK_MachineMaster_CompanyMaster] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[CompanyMaster] ([CompanyId])
GO
ALTER TABLE [dbo].[MachineMaster] CHECK CONSTRAINT [FK_MachineMaster_CompanyMaster]
GO
ALTER TABLE [dbo].[MixingMaster]  WITH CHECK ADD  CONSTRAINT [FK_MixingMaster_CompanyMaster] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[CompanyMaster] ([CompanyId])
GO
ALTER TABLE [dbo].[MixingMaster] CHECK CONSTRAINT [FK_MixingMaster_CompanyMaster]
GO
ALTER TABLE [dbo].[MixingMaster]  WITH CHECK ADD  CONSTRAINT [FK_MixingMaster_InwardMaster] FOREIGN KEY([InwId])
REFERENCES [dbo].[InwardMaster] ([InwId])
GO
ALTER TABLE [dbo].[MixingMaster] CHECK CONSTRAINT [FK_MixingMaster_InwardMaster]
GO
ALTER TABLE [dbo].[MixingMaster]  WITH CHECK ADD  CONSTRAINT [FK_MixingMaster_ItemMaster] FOREIGN KEY([IMId])
REFERENCES [dbo].[ItemMaster] ([IMId])
GO
ALTER TABLE [dbo].[MixingMaster] CHECK CONSTRAINT [FK_MixingMaster_ItemMaster]
GO
ALTER TABLE [dbo].[PartyMaster]  WITH CHECK ADD  CONSTRAINT [FK_PartyMaster_CompanyMaster] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[CompanyMaster] ([CompanyId])
GO
ALTER TABLE [dbo].[PartyMaster] CHECK CONSTRAINT [FK_PartyMaster_CompanyMaster]
GO
ALTER TABLE [dbo].[ProductionDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProductionDetail_MachineMaster] FOREIGN KEY([MachineId])
REFERENCES [dbo].[MachineMaster] ([MachinId])
GO
ALTER TABLE [dbo].[ProductionDetail] CHECK CONSTRAINT [FK_ProductionDetail_MachineMaster]
GO
ALTER TABLE [dbo].[ProductionDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProductionDetail_ProductionMaster] FOREIGN KEY([ProId])
REFERENCES [dbo].[ProductionMaster] ([ProId])
GO
ALTER TABLE [dbo].[ProductionDetail] CHECK CONSTRAINT [FK_ProductionDetail_ProductionMaster]
GO
ALTER TABLE [dbo].[ProductionMaster]  WITH CHECK ADD  CONSTRAINT [FK_ProductionMaster_CompanyMaster] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[CompanyMaster] ([CompanyId])
GO
ALTER TABLE [dbo].[ProductionMaster] CHECK CONSTRAINT [FK_ProductionMaster_CompanyMaster]
GO
ALTER TABLE [dbo].[ReceiveNoteDetail]  WITH CHECK ADD  CONSTRAINT [FK_ReceiveNoteDetail_ItemMaster] FOREIGN KEY([IMId])
REFERENCES [dbo].[ItemMaster] ([IMId])
GO
ALTER TABLE [dbo].[ReceiveNoteDetail] CHECK CONSTRAINT [FK_ReceiveNoteDetail_ItemMaster]
GO
ALTER TABLE [dbo].[ReceiveNoteDetail]  WITH CHECK ADD  CONSTRAINT [FK_ReceiveNoteDetail_ReceiveNoteDetail] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[CompanyMaster] ([CompanyId])
GO
ALTER TABLE [dbo].[ReceiveNoteDetail] CHECK CONSTRAINT [FK_ReceiveNoteDetail_ReceiveNoteDetail]
GO
ALTER TABLE [dbo].[ReceiveNoteDetail]  WITH CHECK ADD  CONSTRAINT [FK_ReceiveNoteDetail_ReceiveNoteMaster] FOREIGN KEY([RNMId])
REFERENCES [dbo].[ReceiveNoteMaster] ([RNMId])
GO
ALTER TABLE [dbo].[ReceiveNoteDetail] CHECK CONSTRAINT [FK_ReceiveNoteDetail_ReceiveNoteMaster]
GO
ALTER TABLE [dbo].[ReceiveNoteMaster]  WITH CHECK ADD  CONSTRAINT [FK_ReceiveNoteMaster_CompanyMaster] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[CompanyMaster] ([CompanyId])
GO
ALTER TABLE [dbo].[ReceiveNoteMaster] CHECK CONSTRAINT [FK_ReceiveNoteMaster_CompanyMaster]
GO
ALTER TABLE [dbo].[ReceiveNoteMaster]  WITH CHECK ADD  CONSTRAINT [FK_ReceiveNoteMaster_PartyMaster] FOREIGN KEY([PartyId])
REFERENCES [dbo].[PartyMaster] ([PartyId])
GO
ALTER TABLE [dbo].[ReceiveNoteMaster] CHECK CONSTRAINT [FK_ReceiveNoteMaster_PartyMaster]
GO
ALTER TABLE [dbo].[RewinderDetail]  WITH CHECK ADD  CONSTRAINT [FK_RewinderDetail_RewinderMaster] FOREIGN KEY([RewId])
REFERENCES [dbo].[RewinderMaster] ([RewId])
GO
ALTER TABLE [dbo].[RewinderDetail] CHECK CONSTRAINT [FK_RewinderDetail_RewinderMaster]
GO
ALTER TABLE [dbo].[RewinderMaster]  WITH CHECK ADD  CONSTRAINT [FK_RewinderMaster_CompanyMaster] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[CompanyMaster] ([CompanyId])
GO
ALTER TABLE [dbo].[RewinderMaster] CHECK CONSTRAINT [FK_RewinderMaster_CompanyMaster]
GO
ALTER TABLE [dbo].[RewinderMaster]  WITH CHECK ADD  CONSTRAINT [FK_RewinderMaster_InwardMaster] FOREIGN KEY([InwId])
REFERENCES [dbo].[InwardMaster] ([InwId])
GO
ALTER TABLE [dbo].[RewinderMaster] CHECK CONSTRAINT [FK_RewinderMaster_InwardMaster]
GO
ALTER TABLE [dbo].[UserMaster]  WITH CHECK ADD  CONSTRAINT [FK_UserMaster_CompanyMaster] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[CompanyMaster] ([CompanyId])
GO
ALTER TABLE [dbo].[UserMaster] CHECK CONSTRAINT [FK_UserMaster_CompanyMaster]
GO
ALTER TABLE [dbo].[UserPermission]  WITH CHECK ADD  CONSTRAINT [FK_UserPermission_FormMaster] FOREIGN KEY([FormId])
REFERENCES [dbo].[FormMaster] ([FormId])
GO
ALTER TABLE [dbo].[UserPermission] CHECK CONSTRAINT [FK_UserPermission_FormMaster]
GO
ALTER TABLE [dbo].[UserPermission]  WITH CHECK ADD  CONSTRAINT [FK_UserPermission_UserMaster] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserMaster] ([UserId])
GO
ALTER TABLE [dbo].[UserPermission] CHECK CONSTRAINT [FK_UserPermission_UserMaster]
GO
ALTER TABLE [dbo].[WasteMaster]  WITH CHECK ADD  CONSTRAINT [FK_WasteMaster_CompanyMaster] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[CompanyMaster] ([CompanyId])
GO
ALTER TABLE [dbo].[WasteMaster] CHECK CONSTRAINT [FK_WasteMaster_CompanyMaster]
GO
ALTER TABLE [dbo].[WasteMaster]  WITH CHECK ADD  CONSTRAINT [FK_WasteMaster_MachineMaster] FOREIGN KEY([MachineId])
REFERENCES [dbo].[MachineMaster] ([MachinId])
GO
ALTER TABLE [dbo].[WasteMaster] CHECK CONSTRAINT [FK_WasteMaster_MachineMaster]
GO
/****** Object:  StoredProcedure [dbo].[UserSignIn]    Script Date: 11-09-2018 15:32:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[UserSignIn]
	@UserName nvarchar(100),
	@Password nvarchar(100)
AS
BEGIN	
	SET NOCOUNT ON;
	SELECT * FROM UserMaster WHERE UserName COLLATE Latin1_General_CS_AS=@UserName AND Password COLLATE Latin1_General_CS_AS=@Password and IsDelete=0
END
GO
USE [master]
GO
ALTER DATABASE [KrishnaPackaging] SET  READ_WRITE 
GO
