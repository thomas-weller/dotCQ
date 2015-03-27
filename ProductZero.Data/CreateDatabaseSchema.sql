USE [master]
GO
/****** Object:  Database [Zero]    Script Date: 25.09.2013 08:19:59 ******/
CREATE DATABASE [Zero]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Zero', FILENAME = N'D:\RDSDBDATA\DATA\Zero.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
 LOG ON 
( NAME = N'Zero_log', FILENAME = N'D:\RDSDBDATA\DATA\Zero_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Zero] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Zero].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Zero] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Zero] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Zero] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Zero] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Zero] SET ARITHABORT OFF 
GO
ALTER DATABASE [Zero] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Zero] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Zero] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Zero] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Zero] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Zero] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Zero] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Zero] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Zero] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Zero] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Zero] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Zero] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Zero] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Zero] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Zero] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Zero] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Zero] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Zero] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Zero] SET RECOVERY FULL 
GO
ALTER DATABASE [Zero] SET  MULTI_USER 
GO
ALTER DATABASE [Zero] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Zero] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Zero] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Zero] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [Zero]
GO
/****** Object:  User [zero_master]    Script Date: 25.09.2013 08:20:00 ******/
CREATE USER [zero_master] FOR LOGIN [zero_master] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [zero_master]
GO
/****** Object:  Table [dbo].[Calculations]    Script Date: 25.09.2013 08:20:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Calculations](
	[Id] [uniqueidentifier] NOT NULL,
	[AddressId] [uniqueidentifier] NOT NULL,
	[Timestamp] [datetime] NOT NULL,
	[Currency] [nchar](3) NOT NULL,
	[ProjectSize] [int] NOT NULL,
	[LifeTime] [int] NOT NULL,
	[Quality] [int] NOT NULL,
	[StaffCostsPerYear] [bigint] NOT NULL,
	[SurplusPercent] [int] NOT NULL,
	[SurplusInvestment] [bigint] NOT NULL,
	[DevCosts] [bigint] NOT NULL,
	[ModDevCosts] [bigint] NOT NULL,
	[MaintCosts] [bigint] NOT NULL,
	[ModMaintCosts] [bigint] NOT NULL,
	[TotalCosts] [bigint] NOT NULL,
	[ModTotalCosts] [bigint] NOT NULL,
	[MaintPercent] [int] NOT NULL,
	[ModMaintPercent] [int] NOT NULL,
	[BreakEven] [float] NOT NULL,
	[Roi] [float] NOT NULL,
	[AvgYearlySaving] [bigint] NOT NULL,
	[MaxYearlySaving] [bigint] NOT NULL,
	[MinYearlySaving] [bigint] NOT NULL,
 CONSTRAINT [PK_Calculations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IpAddresses]    Script Date: 25.09.2013 08:20:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IpAddresses](
	[Id] [uniqueidentifier] NOT NULL,
	[Address] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_IpAddresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NormalizedCalculations]    Script Date: 25.09.2013 08:20:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NormalizedCalculations](
	[CalculationId] [uniqueidentifier] NOT NULL,
	[AddressId] [uniqueidentifier] NULL,
	[Timestamp] [datetime] NOT NULL,
	[SrcCurrency] [nchar](3) NOT NULL,
	[Currency] [nchar](3) NOT NULL,
	[StaffCostsPerYear] [float] NOT NULL,
	[ProjectSize] [int] NOT NULL,
	[LifeTime] [int] NOT NULL,
	[Quality] [int] NOT NULL,
	[SurplusPercent] [int] NOT NULL,
	[SurplusInvestment] [float] NOT NULL,
	[DevCosts] [float] NOT NULL,
	[ModDevCosts] [float] NOT NULL,
	[MaintCosts] [float] NOT NULL,
	[ModMaintCosts] [float] NOT NULL,
	[TotalCosts] [float] NOT NULL,
	[ModTotalCosts] [float] NOT NULL,
	[MaintPercent] [float] NOT NULL,
	[ModMaintPercent] [float] NOT NULL,
	[BreakEven] [float] NOT NULL,
	[Roi] [float] NOT NULL,
	[AvgYearlySaving] [float] NOT NULL,
	[MaxYearlySaving] [float] NOT NULL,
	[MinYearlySaving] [float] NOT NULL,
 CONSTRAINT [PK_NormalizedCalculations_1] PRIMARY KEY CLUSTERED 
(
	[CalculationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OnSiteEngagements]    Script Date: 25.09.2013 08:20:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OnSiteEngagements](
	[Id] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
	[ForumUsers] [int] NULL,
	[NewsLetterSubscriptions] [int] NULL,
	[Surveys] [int] NULL,
	[Comments] [int] NULL,
	[ForumTopics] [int] NULL,
	[CommentUsers] [int] NULL,
 CONSTRAINT [PK_ForumUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_IpAddresses]    Script Date: 25.09.2013 08:20:01 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_IpAddresses] ON [dbo].[IpAddresses]
(
	[Address] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Calculations]  WITH CHECK ADD  CONSTRAINT [FK_Calculations_IpAddresses] FOREIGN KEY([AddressId])
REFERENCES [dbo].[IpAddresses] ([Id])
GO
ALTER TABLE [dbo].[Calculations] CHECK CONSTRAINT [FK_Calculations_IpAddresses]
GO
ALTER TABLE [dbo].[NormalizedCalculations]  WITH CHECK ADD  CONSTRAINT [FK_NormalizedCalculations_Calculations] FOREIGN KEY([CalculationId])
REFERENCES [dbo].[Calculations] ([Id])
GO
ALTER TABLE [dbo].[NormalizedCalculations] CHECK CONSTRAINT [FK_NormalizedCalculations_Calculations]
GO
ALTER TABLE [dbo].[NormalizedCalculations]  WITH CHECK ADD  CONSTRAINT [FK_NormalizedCalculations_IpAddresses] FOREIGN KEY([AddressId])
REFERENCES [dbo].[IpAddresses] ([Id])
GO
ALTER TABLE [dbo].[NormalizedCalculations] CHECK CONSTRAINT [FK_NormalizedCalculations_IpAddresses]
GO
/****** Object:  DdlTrigger [rds_deny_backups_trigger]    Script Date: 25.09.2013 08:20:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [rds_deny_backups_trigger] ON DATABASE WITH EXECUTE AS 'dbo' FOR
 ADD_ROLE_MEMBER, GRANT_DATABASE AS BEGIN
   SET ANSI_PADDING ON;
 
   DECLARE @data XML;
   DECLARE @user SYSNAME;
   DECLARE @role SYSNAME;
   DECLARE @type SYSNAME;
   DECLARE @sql NVARCHAR(MAX);
   DECLARE @permissions TABLE(name SYSNAME PRIMARY KEY);
   
   SELECT @data = EVENTDATA();
   SELECT @type = @data.value('(/EVENT_INSTANCE/EventType)[1]', 'SYSNAME');
    
   IF @type = 'ADD_ROLE_MEMBER' BEGIN
      SELECT @user = @data.value('(/EVENT_INSTANCE/ObjectName)[1]', 'SYSNAME'),
       @role = @data.value('(/EVENT_INSTANCE/RoleName)[1]', 'SYSNAME');

      IF @role IN ('db_owner', 'db_backupoperator') BEGIN
         SELECT @sql = 'DENY BACKUP DATABASE, BACKUP LOG TO ' + QUOTENAME(@user);
         EXEC(@sql);
      END
   END ELSE IF @type = 'GRANT_DATABASE' BEGIN
      INSERT INTO @permissions(name)
      SELECT Permission.value('(text())[1]', 'SYSNAME') FROM
       @data.nodes('/EVENT_INSTANCE/Permissions/Permission')
      AS DatabasePermissions(Permission);
      
      IF EXISTS (SELECT * FROM @permissions WHERE name IN ('BACKUP DATABASE',
       'BACKUP LOG'))
         RAISERROR('Cannot grant backup database or backup log', 15, 1) WITH LOG;       
   END
END

GO
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
ENABLE TRIGGER [rds_deny_backups_trigger] ON DATABASE
GO
USE [master]
GO
ALTER DATABASE [Zero] SET  READ_WRITE 
GO
