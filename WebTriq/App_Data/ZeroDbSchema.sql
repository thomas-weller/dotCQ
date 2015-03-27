USE [Zero]

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

/****** Object:  Table [dbo].[Calculations]    Script Date: 19.09.2013 12:31:55 ******/
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
/****** Object:  Table [dbo].[FeatureProposals]    Script Date: 19.09.2013 12:31:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeatureProposals](
	[Id] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Votes] [int] NOT NULL,
 CONSTRAINT [PK_FeatureProposals] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Forums]    Script Date: 19.09.2013 12:31:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Forums](
	[Id] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Topics] [int] NOT NULL,
 CONSTRAINT [PK_Forums] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IpAddresses]    Script Date: 19.09.2013 12:31:55 ******/
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
/****** Object:  Table [dbo].[NormalizedCalculations]    Script Date: 19.09.2013 12:31:55 ******/
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
/****** Object:  Table [dbo].[OnSiteEngagements]    Script Date: 19.09.2013 12:31:55 ******/
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
 CONSTRAINT [PK_ForumUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_IpAddresses]    Script Date: 19.09.2013 12:31:55 ******/
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
USE [master]
GO
ALTER DATABASE [Zero] SET  READ_WRITE 
GO
