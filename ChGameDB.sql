USE [master]
GO
/****** Object:  Database [ChGameDB]    Script Date: 10/02/2016 13:18:38 ******/
CREATE DATABASE [ChGameDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GameDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\GameDB.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'GameDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\GameDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ChGameDB] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ChGameDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ChGameDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ChGameDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ChGameDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ChGameDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ChGameDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ChGameDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ChGameDB] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [ChGameDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ChGameDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ChGameDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ChGameDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ChGameDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ChGameDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ChGameDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ChGameDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ChGameDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ChGameDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ChGameDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ChGameDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ChGameDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ChGameDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ChGameDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ChGameDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ChGameDB] SET RECOVERY FULL 
GO
ALTER DATABASE [ChGameDB] SET  MULTI_USER 
GO
ALTER DATABASE [ChGameDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ChGameDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ChGameDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ChGameDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ChGameDB', N'ON'
GO
USE [ChGameDB]
GO
/****** Object:  Table [dbo].[Player]    Script Date: 10/02/2016 13:18:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Player](
	[idPlayer] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[score] [int] NOT NULL,
 CONSTRAINT [PK_Player] PRIMARY KEY CLUSTERED 
(
	[idPlayer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
USE [master]
GO
ALTER DATABASE [ChGameDB] SET  READ_WRITE 
GO
