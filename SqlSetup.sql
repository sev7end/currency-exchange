USE [master]
GO
/****** Object:  Database [CurrencyExchange]    Script Date: 6/5/2023 2:50:13 AM ******/
CREATE DATABASE [CurrencyExchange]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CurrencyExchange', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\CurrencyExchange.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CurrencyExchange_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\CurrencyExchange_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [CurrencyExchange] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CurrencyExchange].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CurrencyExchange] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CurrencyExchange] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CurrencyExchange] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CurrencyExchange] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CurrencyExchange] SET ARITHABORT OFF 
GO
ALTER DATABASE [CurrencyExchange] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CurrencyExchange] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CurrencyExchange] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CurrencyExchange] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CurrencyExchange] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CurrencyExchange] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CurrencyExchange] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CurrencyExchange] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CurrencyExchange] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CurrencyExchange] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CurrencyExchange] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CurrencyExchange] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CurrencyExchange] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CurrencyExchange] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CurrencyExchange] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CurrencyExchange] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CurrencyExchange] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CurrencyExchange] SET RECOVERY FULL 
GO
ALTER DATABASE [CurrencyExchange] SET  MULTI_USER 
GO
ALTER DATABASE [CurrencyExchange] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CurrencyExchange] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CurrencyExchange] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CurrencyExchange] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CurrencyExchange] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CurrencyExchange] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'CurrencyExchange', N'ON'
GO
ALTER DATABASE [CurrencyExchange] SET QUERY_STORE = OFF
GO
USE [CurrencyExchange]
GO
/****** Object:  Table [dbo].[Currencies]    Script Date: 6/5/2023 2:50:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Currencies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](20) NOT NULL,
	[NameKa] [nvarchar](50) NULL,
 CONSTRAINT [PK_Currencies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CurrencyConversions]    Script Date: 6/5/2023 2:50:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CurrencyConversions](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FromCurrencyId] [int] NOT NULL,
	[ToCurrencyId] [int] NOT NULL,
	[Rate] [decimal](18, 2) NOT NULL,
	[InitialAmount] [decimal](18, 2) NOT NULL,
	[ConvertedAmount] [decimal](18, 2) NOT NULL,
	[PersonId] [bigint] NOT NULL,
	[RecommendatorId] [bigint] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_CurrencyConversions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CurrencyRates]    Script Date: 6/5/2023 2:50:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CurrencyRates](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CurrencyId] [int] NOT NULL,
	[BuyRate] [decimal](18, 2) NOT NULL,
	[SellRate] [decimal](18, 2) NOT NULL,
	[RateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_CurrencyRates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Persons]    Script Date: 6/5/2023 2:50:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persons](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[PersonalNumber] [varchar](50) NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Persons] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CurrencyConversions] ADD  CONSTRAINT [DF_CurrencyConversions_RecommendatorId]  DEFAULT ((1)) FOR [RecommendatorId]
GO
ALTER TABLE [dbo].[CurrencyConversions] ADD  CONSTRAINT [DF_CurrencyConversions_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[CurrencyRates] ADD  CONSTRAINT [DF_CurrencyRates_RateDate]  DEFAULT (getdate()) FOR [RateDate]
GO
ALTER TABLE [dbo].[CurrencyConversions]  WITH CHECK ADD  CONSTRAINT [FK_CurrencyConversions_Currencies] FOREIGN KEY([ToCurrencyId])
REFERENCES [dbo].[Currencies] ([Id])
GO
ALTER TABLE [dbo].[CurrencyConversions] CHECK CONSTRAINT [FK_CurrencyConversions_Currencies]
GO
ALTER TABLE [dbo].[CurrencyConversions]  WITH CHECK ADD  CONSTRAINT [FK_CurrencyConversions_CurrencyConversions] FOREIGN KEY([FromCurrencyId])
REFERENCES [dbo].[Currencies] ([Id])
GO
ALTER TABLE [dbo].[CurrencyConversions] CHECK CONSTRAINT [FK_CurrencyConversions_CurrencyConversions]
GO
ALTER TABLE [dbo].[CurrencyConversions]  WITH CHECK ADD  CONSTRAINT [FK_CurrencyConversions_Persons] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Persons] ([Id])
GO
ALTER TABLE [dbo].[CurrencyConversions] CHECK CONSTRAINT [FK_CurrencyConversions_Persons]
GO
ALTER TABLE [dbo].[CurrencyConversions]  WITH CHECK ADD  CONSTRAINT [FK_CurrencyConversions_Persons1] FOREIGN KEY([RecommendatorId])
REFERENCES [dbo].[Persons] ([Id])
GO
ALTER TABLE [dbo].[CurrencyConversions] CHECK CONSTRAINT [FK_CurrencyConversions_Persons1]
GO
ALTER TABLE [dbo].[CurrencyRates]  WITH CHECK ADD  CONSTRAINT [FK_CurrencyRates_Currencies] FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[Currencies] ([Id])
GO
ALTER TABLE [dbo].[CurrencyRates] CHECK CONSTRAINT [FK_CurrencyRates_Currencies]
GO
/****** Object:  StoredProcedure [dbo].[spAddCurrency]    Script Date: 6/5/2023 2:50:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spAddCurrency]
	@Code VARCHAR(20),
	@NameKa NVARCHAR(50)
AS
BEGIN

	INSERT INTO [dbo].[Currencies]
	           ([Code]
	           ,[NameKa])
	     VALUES
	           (@Code
	           ,@NameKa)

	SELECT SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[spAddCurrencyConversion]    Script Date: 6/5/2023 2:50:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAddCurrencyConversion]
    @FromCurrencyId INT,
    @ToCurrencyId INT,
	@Rate DECIMAL(18,2),
    @InitialAmount DECIMAL(18, 2),
    @ConvertedAmount DECIMAL(18, 2),
    @PersonId BIGINT
AS
BEGIN
    INSERT INTO [dbo].[CurrencyConversions]
    (FromCurrencyId, ToCurrencyId, Rate, InitialAmount, ConvertedAmount, PersonId, CreateDate)
    VALUES (@FromCurrencyId, @ToCurrencyId, @Rate, @InitialAmount, @ConvertedAmount, @PersonId, GETDATE());

	SELECT SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[spAddCurrencyRate]    Script Date: 6/5/2023 2:50:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAddCurrencyRate]
    @CurrencyId INT,
	@BuyRate decimal(18,2),
	@SellRate decimal(18,2)
AS
BEGIN

	INSERT INTO [dbo].[CurrencyRates]
           ([CurrencyId]
           ,[BuyRate]
           ,[SellRate]
           ,[RateDate])
     VALUES
           (@CurrencyId
           ,@BuyRate
           ,@SellRate
           ,GETDATE())

	SELECT SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[spAddPerson]    Script Date: 6/5/2023 2:50:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spAddPerson]
	@PersonalNumber VARCHAR(50),
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50)
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM [dbo].[Persons] WHERE PersonalNumber = @PersonalNumber)
	BEGIN
		INSERT INTO [dbo].[Persons]
			([PersonalNumber], [FirstName], [LastName])
		VALUES
			(@PersonalNumber, @FirstName, @LastName)
		
		SELECT SCOPE_IDENTITY()
	END
	ELSE
	BEGIN
		SELECT [Id] FROM [dbo].[Persons] WHERE PersonalNumber = @PersonalNumber
	END
END
GO
/****** Object:  StoredProcedure [dbo].[spGetAllCurrencies]    Script Date: 6/5/2023 2:50:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spGetAllCurrencies]
AS
BEGIN
	SELECT 
	C.Id,
	C.NameKa as Name,
	c.Code,
	(SELECT TOP 1 BuyRate FROM dbo.CurrencyRates WHERE CurrencyId = C.Id ORDER BY Id DESC) AS BuyRate,
	(SELECT TOP 1 SellRate FROM dbo.CurrencyRates WHERE CurrencyId = C.Id ORDER BY Id DESC) AS SellRate,
	(SELECT TOP 1 RateDate FROM dbo.CurrencyRates WHERE CurrencyId = C.Id ORDER BY Id DESC) AS RateDate
	FROM dbo.Currencies C
	ORDER BY C.Id DESC

END
GO
/****** Object:  StoredProcedure [dbo].[spGetConversionReport]    Script Date: 6/5/2023 2:50:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetConversionReport]
    @StartDate DATETIME = NULL,
    @EndDate DATETIME = NULL,
	@PersonalNumber NVARCHAR(20) = NULL
AS
BEGIN
    SELECT FC.Code AS FromExchangeRate,
	TC.Code as ToExchangeRate,
	CC.Rate as ExchangeRate,
	CC.CreateDate as ExchangeDate,
	CC.InitialAmount,
	CC.ConvertedAmount,
	CONCAT(P.FirstName, ' ', P.LastName) AS FullName,
	P.PersonalNumber
    FROM [dbo].[CurrencyConversions] CC
	INNER JOIN  dbo.Currencies FC ON CC.FromCurrencyId  = FC.Id
	INNER JOIN dbo.Currencies TC ON CC.ToCurrencyId = TC.Id
    LEFT JOIN dbo.Persons P ON P.Id = CC.PersonId
	WHERE  (@StartDate IS NULL OR CC.CreateDate > @StartDate)
		AND (@EndDate IS NULL OR CC.CreateDate < @EndDate)
		AND (@PersonalNumber IS NULL OR P.PersonalNumber = @PersonalNumber)
	ORDER BY CC.CreateDate DESC
END
GO
/****** Object:  StoredProcedure [dbo].[spGetLatestCurrencyRate]    Script Date: 6/5/2023 2:50:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetLatestCurrencyRate]
    @CurrencyId INT
AS
BEGIN
	SELECT TOP 1 * FROM CurrencyRates WHERE CurrencyId = @CurrencyId ORDER BY Id DESC
END
GO
/****** Object:  StoredProcedure [dbo].[spGetPerson]    Script Date: 6/5/2023 2:50:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetPerson]
    @Id INT
AS
BEGIN
    SELECT TOP 1 * FROM [dbo].[Persons] WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[spGetPersonConversionLimit]    Script Date: 6/5/2023 2:50:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetPersonConversionLimit]
	@PersonalNumber NVARCHAR(20),
	@InitialAmount DECIMAL
AS
BEGIN
	DECLARE @Today DATETIME;
    SET @Today = GETDATE();

    DECLARE @Total DECIMAL;
    SELECT @Total = SUM(InitialAmount)
    FROM dbo.CurrencyConversions CC
	INNER JOIN dbo.Persons P on CC.PersonId = P.Id OR CC.RecommendatorId = P.Id
    WHERE P.PersonalNumber = @PersonalNumber AND (FromCurrencyId = 1)
          AND CONVERT(DATE, CC.CreateDate) = CONVERT(DATE, @Today);

    IF (@Total + @InitialAmount > 100000)
    BEGIN
        SELECT 0 AS IsWithinLimit;
    END
    ELSE
    BEGIN
        SELECT 1 AS IsWithinLimit;
    END

END
GO
/****** Object:  StoredProcedure [dbo].[spUpdateCurrencyRate]    Script Date: 6/5/2023 2:50:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spUpdateCurrencyRate]
    @CurrencyId INT,
    @BuyRate DECIMAL(18, 2),
    @SellRate DECIMAL(18, 2),
    @RateDate DATETIME
AS
BEGIN
    IF EXISTS (SELECT 1 FROM [dbo].[CurrencyRates] WHERE CurrencyId = @CurrencyId AND RateDate = @RateDate)
    BEGIN
        UPDATE [dbo].[CurrencyRates]
        SET BuyRate = @BuyRate, SellRate = @SellRate
        WHERE CurrencyId = @CurrencyId AND RateDate = @RateDate;
    END
    ELSE
    BEGIN
        INSERT INTO [dbo].[CurrencyRates]
        (CurrencyId, BuyRate, SellRate, RateDate)
        VALUES (@CurrencyId, @BuyRate, @SellRate, @RateDate);
    END
END
GO
USE [master]
GO
ALTER DATABASE [CurrencyExchange] SET  READ_WRITE 
GO
USE [CurrencyExchange]
GO

INSERT INTO [dbo].[Currencies]
           ([Code]
           ,[NameKa])
     VALUES
           ('GEL'
           ,N'ლარი')
GO

USE [CurrencyExchange]
GO

INSERT INTO [dbo].[Persons]
           ([PersonalNumber]
           ,[FirstName]
           ,[LastName])
     VALUES
           ('0'
           ,N'უცნობი'
           ,N'პირი')
GO



