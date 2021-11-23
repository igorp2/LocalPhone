USE [master]
GO
/****** Object:  Database [LocalPhone]    Script Date: 01/10/2021 13:44:59 ******/
CREATE DATABASE [LocalPhone]
GO
ALTER DATABASE [LocalPhone] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LocalPhone].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LocalPhone] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LocalPhone] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LocalPhone] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LocalPhone] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LocalPhone] SET ARITHABORT OFF 
GO
ALTER DATABASE [LocalPhone] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LocalPhone] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LocalPhone] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LocalPhone] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LocalPhone] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LocalPhone] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LocalPhone] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LocalPhone] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LocalPhone] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LocalPhone] SET  DISABLE_BROKER 
GO
ALTER DATABASE [LocalPhone] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LocalPhone] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LocalPhone] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LocalPhone] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LocalPhone] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LocalPhone] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LocalPhone] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LocalPhone] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [LocalPhone] SET  MULTI_USER 
GO
ALTER DATABASE [LocalPhone] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LocalPhone] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LocalPhone] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LocalPhone] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LocalPhone] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [LocalPhone] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [LocalPhone] SET QUERY_STORE = OFF
GO
USE [LocalPhone]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 01/10/2021 13:44:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Address](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](200) NULL,
	[street] [nvarchar](200) NULL,
	[idCity] [int] NULL,
	[idState] [int] NULL,
	[Zip] [nvarchar](30) NULL,
	[idCountry] [int] NULL,
	[Note] [nvarchar](200) NULL,
	[idCustomer] [varchar](20) NULL,
	[status] [int] NULL,
	[creationDate] [datetime] NULL,
	[creatorUser] [nvarchar](450) NULL,
	[lastModificationDate] [datetime] NULL,
	[userThatMadeTheLastModification] [nvarchar](450) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 09/11/2021 18:02:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 09/11/2021 18:02:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 09/11/2021 18:02:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 09/11/2021 18:02:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 09/11/2021 18:02:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 09/11/2021 18:02:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[Firstname] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 09/11/2021 18:02:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AvailableNumber]    Script Date: 09/11/2021 18:02:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AvailableNumber](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idCountry] [int] NOT NULL,
	[idCity] [int] NOT NULL,
	[phoneNumber] [varchar](20) NOT NULL,
	[status] [int] NULL,
	[creationDate] [datetime] NULL,
	[creatorUser] [nvarchar](450) NULL,
	[lastModificationDate] [datetime] NULL,
	[userThatMadeTheLastModification] [nvarchar](450) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[City]    Script Date: 09/11/2021 18:02:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Phonecode] [int] NOT NULL,
	[Description] [nvarchar](800) NOT NULL,
	[idState] [int] NOT NULL,
	[status] [int] NULL,
	[creationDate] [datetime] NULL,
	[creatorUser] [nvarchar](450) NULL,
	[lastModificationDate] [datetime] NULL,
	[userThatMadeTheLastModification] [nvarchar](450) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 09/11/2021 18:02:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idCustomer] [varchar](20) NOT NULL,
	[status] [int] NULL,
	[creationDate] [datetime] NULL,
	[creatorUser] [nvarchar](450) NULL,
	[lastModificationDate] [datetime] NULL,
	[userThatMadeTheLastModification] [nvarchar](450) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 09/11/2021 18:02:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[iso] [char](2) NOT NULL,
	[name] [varchar](80) NOT NULL,
	[nicename] [varchar](80) NOT NULL,
	[iso3] [char](3) NULL,
	[numcode] [smallint] NULL,
	[phonecode] [int] NOT NULL,
	[status] [int] NULL,
	[creationDate] [datetime] NULL,
	[creatorUser] [nvarchar](450) NULL,
	[lastModificationDate] [datetime] NULL,
	[userThatMadeTheLastModification] [nvarchar](450) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 09/11/2021 18:02:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[phoneNumber] [varchar](20) NOT NULL,
	[idCountry] [int] NULL,
	[operationalSystem] [varchar](100) NULL,
	[verificationCode] [int] NULL,
	[validationCodeDate] [datetime] NULL,
	[verificationCodeDate] [datetime] NULL,
	[publishedAppVersion] [int] NULL,
	[password] [varchar](100) NULL,
	[email] [varchar](200) NULL,
	[dateOfBirth] [date] NULL,
	[firstName] [nvarchar](100) NULL,
	[lastName] [nvarchar](100) NULL,
	[idGender] [int] NULL,
	[avatar] [varchar](max) NULL,
	[status] [int] NOT NULL,
	[creationDate] [datetime] NULL,
	[creatorUser] [nvarchar](450) NULL,
	[lastModificationDate] [datetime] NULL,
	[userThatMadeTheLastModification] [nvarchar](450) NULL,
PRIMARY KEY CLUSTERED 
(
	[phoneNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Gender]    Script Date: 09/11/2021 18:02:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gender](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Gender] [varchar](250) NULL,
	[Abbreviation] [varchar](50) NULL,
	[Description] [nvarchar](500) NULL,
	[idCountry] [int] NULL,
	[status] [int] NULL,
	[creationDate] [datetime] NULL,
	[creatorUser] [nvarchar](450) NULL,
	[lastModificationDate] [datetime] NULL,
	[userThatMadeTheLastModification] [nvarchar](450) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Message]    Script Date: 09/11/2021 18:02:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Message](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idCustomerSending] [varchar](20) NOT NULL,
	[idCustomerReceiving] [varchar](20) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Text] [varchar](max) NULL,
	[Type] [int] NOT NULL,
	[status] [int] NULL,
	[creationDate] [datetime] NULL,
	[creatorUser] [nvarchar](450) NULL,
	[lastModificationDate] [datetime] NULL,
	[userThatMadeTheLastModification] [nvarchar](450) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Number]    Script Date: 09/11/2021 18:02:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Number](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idCustomer] [varchar](20) NOT NULL,
	[phoneNumber] [varchar](20) NOT NULL,
	[idPayment] [int] NOT NULL,
	[status] [int] NULL,
	[creationDate] [datetime] NULL,
	[creatorUser] [varchar](450) NULL,
	[lastModificationDate] [datetime] NULL,
	[userThatMadeTheLastModification] [varchar](450) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 09/11/2021 18:02:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CardNumber] [nvarchar](16) NULL,
	[ExpirationMonth] [int] NULL,
	[ExpirationYear] [int] NULL,
	[CVC] [int] NULL,
	[OrderAmount] [int] NULL,
	[idCustomer] [varchar](20) NULL,
	[Email] [nvarchar](250) NULL,
	[NameOnCard] [nvarchar](250) NULL,
	[ZipCode] [nvarchar](25) NULL,
	[status] [int] NULL,
	[creationDate] [datetime] NULL,
	[creatorUser] [nvarchar](450) NULL,
	[lastModificationDate] [datetime] NULL,
	[userThatMadeTheLastModification] [nvarchar](450) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SmsRelatedInformation]    Script Date: 09/11/2021 18:02:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SmsRelatedInformation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TransactionId] [char](68) NULL,
	[IdCustomer] [varchar](20) NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_SmsRelatedInformation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[State]    Script Date: 09/11/2021 18:02:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[State](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NULL,
	[Abbreviation] [varchar](50) NULL,
	[idCountry] [int] NOT NULL,
	[status] [int] NULL,
	[creationDate] [datetime] NULL,
	[creatorUser] [nvarchar](450) NULL,
	[lastModificationDate] [datetime] NULL,
	[userThatMadeTheLastModification] [nvarchar](450) NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 09/11/2021 18:02:51 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 09/11/2021 18:02:51 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 09/11/2021 18:02:51 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 09/11/2021 18:02:51 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 09/11/2021 18:02:51 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 09/11/2021 18:02:51 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 09/11/2021 18:02:51 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Address] ADD  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[AvailableNumber] ADD  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[City] ADD  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[Contact] ADD  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[Country] ADD  DEFAULT (NULL) FOR [iso3]
GO
ALTER TABLE [dbo].[Country] ADD  DEFAULT (NULL) FOR [numcode]
GO
ALTER TABLE [dbo].[Country] ADD  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[Gender] ADD  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[Message] ADD  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[Number] ADD  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[Payment] ADD  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[SmsRelatedInformation] ADD  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[State] ADD  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_AddressCity] FOREIGN KEY([idCity])
REFERENCES [dbo].[City] ([id])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_AddressCity]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_AddressCountry] FOREIGN KEY([idCountry])
REFERENCES [dbo].[Country] ([id])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_AddressCountry]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_AddressCustomer] FOREIGN KEY([idCustomer])
REFERENCES [dbo].[Customer] ([phoneNumber])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_AddressCustomer]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_AddressState] FOREIGN KEY([idState])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_AddressState]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AvailableNumber]  WITH CHECK ADD  CONSTRAINT [FK_AvailableNumberCity] FOREIGN KEY([idCity])
REFERENCES [dbo].[City] ([id])
GO
ALTER TABLE [dbo].[AvailableNumber] CHECK CONSTRAINT [FK_AvailableNumberCity]
GO
ALTER TABLE [dbo].[AvailableNumber]  WITH CHECK ADD  CONSTRAINT [FK_AvailableNumberCountry] FOREIGN KEY([idCountry])
REFERENCES [dbo].[Country] ([id])
GO
ALTER TABLE [dbo].[AvailableNumber] CHECK CONSTRAINT [FK_AvailableNumberCountry]
GO
ALTER TABLE [dbo].[City]  WITH CHECK ADD  CONSTRAINT [FK_CityState] FOREIGN KEY([idState])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[City] CHECK CONSTRAINT [FK_CityState]
GO
ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_ContactCustomer] FOREIGN KEY([idCustomer])
REFERENCES [dbo].[Customer] ([phoneNumber])
GO
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_ContactCustomer]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCountry] FOREIGN KEY([idCountry])
REFERENCES [dbo].[Country] ([id])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_CustomerCountry]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_CustomerGender] FOREIGN KEY([idGender])
REFERENCES [dbo].[Gender] ([id])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_CustomerGender]
GO
ALTER TABLE [dbo].[Gender]  WITH CHECK ADD  CONSTRAINT [FK_GenderCountry] FOREIGN KEY([idCountry])
REFERENCES [dbo].[Country] ([id])
GO
ALTER TABLE [dbo].[Gender] CHECK CONSTRAINT [FK_GenderCountry]
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [FK_MessageCustomerReceiving] FOREIGN KEY([idCustomerReceiving])
REFERENCES [dbo].[Customer] ([phoneNumber])
GO
ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK_MessageCustomerReceiving]
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [FK_MessageCustomerSending] FOREIGN KEY([idCustomerSending])
REFERENCES [dbo].[Customer] ([phoneNumber])
GO
ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK_MessageCustomerSending]
GO
ALTER TABLE [dbo].[Number]  WITH CHECK ADD  CONSTRAINT [FK_NumberCustomer] FOREIGN KEY([idCustomer])
REFERENCES [dbo].[Customer] ([phoneNumber])
GO
ALTER TABLE [dbo].[Number] CHECK CONSTRAINT [FK_NumberCustomer]
GO
ALTER TABLE [dbo].[Number]  WITH CHECK ADD  CONSTRAINT [FK_NumberPayment] FOREIGN KEY([idPayment])
REFERENCES [dbo].[Payment] ([id])
GO
ALTER TABLE [dbo].[Number] CHECK CONSTRAINT [FK_NumberPayment]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_PaymentCustomer] FOREIGN KEY([idCustomer])
REFERENCES [dbo].[Customer] ([phoneNumber])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_PaymentCustomer]
GO
ALTER TABLE [dbo].[SmsRelatedInformation]  WITH CHECK ADD  CONSTRAINT [FK_SmsCustomer] FOREIGN KEY([IdCustomer])
REFERENCES [dbo].[Customer] ([phoneNumber])
GO
ALTER TABLE [dbo].[SmsRelatedInformation] CHECK CONSTRAINT [FK_SmsCustomer]
GO
ALTER TABLE [dbo].[State]  WITH CHECK ADD  CONSTRAINT [FK_StateCountry] FOREIGN KEY([idCountry])
REFERENCES [dbo].[Country] ([id])
GO
ALTER TABLE [dbo].[State] CHECK CONSTRAINT [FK_StateCountry]
GO
USE [master]
GO
ALTER DATABASE [LocalPhone] SET  READ_WRITE 
GO


INSERT INTO [LocalPhone].[dbo].[Country] (iso, name, nicename, iso3, numcode, phonecode) VALUES
('AF', 'AFGHANISTAN', 'Afghanistan', 'AFG', 4, 93),
('AL', 'ALBANIA', 'Albania', 'ALB', 8, 355),
('DZ', 'ALGERIA', 'Algeria', 'DZA', 12, 213),
('AS', 'AMERICAN SAMOA', 'American Samoa', 'ASM', 16, 1684),
('AD', 'ANDORRA', 'Andorra', 'AND', 20, 376),
('AO', 'ANGOLA', 'Angola', 'AGO', 24, 244),
('AI', 'ANGUILLA', 'Anguilla', 'AIA', 660, 1264),
('AQ', 'ANTARCTICA', 'Antarctica', NULL, NULL, 0),
('AG', 'ANTIGUA AND BARBUDA', 'Antigua and Barbuda', 'ATG', 28, 1268),
('AR', 'ARGENTINA', 'Argentina', 'ARG', 32, 54),
('AM', 'ARMENIA', 'Armenia', 'ARM', 51, 374),
('AW', 'ARUBA', 'Aruba', 'ABW', 533, 297),
('AU', 'AUSTRALIA', 'Australia', 'AUS', 36, 61),
('AT', 'AUSTRIA', 'Austria', 'AUT', 40, 43),
('AZ', 'AZERBAIJAN', 'Azerbaijan', 'AZE', 31, 994),
('BS', 'BAHAMAS', 'Bahamas', 'BHS', 44, 1242),
('BH', 'BAHRAIN', 'Bahrain', 'BHR', 48, 973),
('BD', 'BANGLADESH', 'Bangladesh', 'BGD', 50, 880),
('BB', 'BARBADOS', 'Barbados', 'BRB', 52, 1246),
('BY', 'BELARUS', 'Belarus', 'BLR', 112, 375),
('BE', 'BELGIUM', 'Belgium', 'BEL', 56, 32),
('BZ', 'BELIZE', 'Belize', 'BLZ', 84, 501),
('BJ', 'BENIN', 'Benin', 'BEN', 204, 229),
('BM', 'BERMUDA', 'Bermuda', 'BMU', 60, 1441),
('BT', 'BHUTAN', 'Bhutan', 'BTN', 64, 975),
('BO', 'BOLIVIA', 'Bolivia', 'BOL', 68, 591),
('BA', 'BOSNIA AND HERZEGOVINA', 'Bosnia and Herzegovina', 'BIH', 70, 387),
('BW', 'BOTSWANA', 'Botswana', 'BWA', 72, 267),
('BV', 'BOUVET ISLAND', 'Bouvet Island', NULL, NULL, 0),
('BR', 'BRAZIL', 'Brazil', 'BRA', 76, 55),
('IO', 'BRITISH INDIAN OCEAN TERRITORY', 'British Indian Ocean Territory', NULL, NULL, 246),
('BN', 'BRUNEI DARUSSALAM', 'Brunei Darussalam', 'BRN', 96, 673),
('BG', 'BULGARIA', 'Bulgaria', 'BGR', 100, 359),
('BF', 'BURKINA FASO', 'Burkina Faso', 'BFA', 854, 226),
('BI', 'BURUNDI', 'Burundi', 'BDI', 108, 257),
('KH', 'CAMBODIA', 'Cambodia', 'KHM', 116, 855),
('CM', 'CAMEROON', 'Cameroon', 'CMR', 120, 237),
('CA', 'CANADA', 'Canada', 'CAN', 124, 1),
('CV', 'CAPE VERDE', 'Cape Verde', 'CPV', 132, 238),
('KY', 'CAYMAN ISLANDS', 'Cayman Islands', 'CYM', 136, 1345),
('CF', 'CENTRAL AFRICAN REPUBLIC', 'Central African Republic', 'CAF', 140, 236),
('TD', 'CHAD', 'Chad', 'TCD', 148, 235),
('CL', 'CHILE', 'Chile', 'CHL', 152, 56),
('CN', 'CHINA', 'China', 'CHN', 156, 86),
('CX', 'CHRISTMAS ISLAND', 'Christmas Island', NULL, NULL, 61),
('CC', 'COCOS (KEELING) ISLANDS', 'Cocos (Keeling) Islands', NULL, NULL, 672),
('CO', 'COLOMBIA', 'Colombia', 'COL', 170, 57),
('KM', 'COMOROS', 'Comoros', 'COM', 174, 269),
('CG', 'CONGO', 'Congo', 'COG', 178, 242),
('CD', 'CONGO, THE DEMOCRATIC REPUBLIC OF THE', 'Congo, the Democratic Republic of the', 'COD', 180, 242),
('CK', 'COOK ISLANDS', 'Cook Islands', 'COK', 184, 682),
('CR', 'COSTA RICA', 'Costa Rica', 'CRI', 188, 506),
('CI', 'COTE D''IVOIRE', 'Cote D''Ivoire', 'CIV', 384, 225),
('HR', 'CROATIA', 'Croatia', 'HRV', 191, 385),
('CU', 'CUBA', 'Cuba', 'CUB', 192, 53),
('CY', 'CYPRUS', 'Cyprus', 'CYP', 196, 357),
('CZ', 'CZECH REPUBLIC', 'Czech Republic', 'CZE', 203, 420),
('DK', 'DENMARK', 'Denmark', 'DNK', 208, 45),
('DJ', 'DJIBOUTI', 'Djibouti', 'DJI', 262, 253),
('DM', 'DOMINICA', 'Dominica', 'DMA', 212, 1767),
('DO', 'DOMINICAN REPUBLIC', 'Dominican Republic', 'DOM', 214, 1809),
('EC', 'ECUADOR', 'Ecuador', 'ECU', 218, 593),
('EG', 'EGYPT', 'Egypt', 'EGY', 818, 20),
('SV', 'EL SALVADOR', 'El Salvador', 'SLV', 222, 503),
('GQ', 'EQUATORIAL GUINEA', 'Equatorial Guinea', 'GNQ', 226, 240),
('ER', 'ERITREA', 'Eritrea', 'ERI', 232, 291),
('EE', 'ESTONIA', 'Estonia', 'EST', 233, 372),
('ET', 'ETHIOPIA', 'Ethiopia', 'ETH', 231, 251),
('FK', 'FALKLAND ISLANDS (MALVINAS)', 'Falkland Islands (Malvinas)', 'FLK', 238, 500),
('FO', 'FAROE ISLANDS', 'Faroe Islands', 'FRO', 234, 298),
('FJ', 'FIJI', 'Fiji', 'FJI', 242, 679),
('FI', 'FINLAND', 'Finland', 'FIN', 246, 358),
('FR', 'FRANCE', 'France', 'FRA', 250, 33),
('GF', 'FRENCH GUIANA', 'French Guiana', 'GUF', 254, 594),
('PF', 'FRENCH POLYNESIA', 'French Polynesia', 'PYF', 258, 689),
('TF', 'FRENCH SOUTHERN TERRITORIES', 'French Southern Territories', NULL, NULL, 0),
('GA', 'GABON', 'Gabon', 'GAB', 266, 241),
('GM', 'GAMBIA', 'Gambia', 'GMB', 270, 220),
('GE', 'GEORGIA', 'Georgia', 'GEO', 268, 995),
('DE', 'GERMANY', 'Germany', 'DEU', 276, 49),
('GH', 'GHANA', 'Ghana', 'GHA', 288, 233),
('GI', 'GIBRALTAR', 'Gibraltar', 'GIB', 292, 350),
('GR', 'GREECE', 'Greece', 'GRC', 300, 30),
('GL', 'GREENLAND', 'Greenland', 'GRL', 304, 299),
('GD', 'GRENADA', 'Grenada', 'GRD', 308, 1473),
('GP', 'GUADELOUPE', 'Guadeloupe', 'GLP', 312, 590),
('GU', 'GUAM', 'Guam', 'GUM', 316, 1671),
('GT', 'GUATEMALA', 'Guatemala', 'GTM', 320, 502),
('GN', 'GUINEA', 'Guinea', 'GIN', 324, 224),
('GW', 'GUINEA-BISSAU', 'Guinea-Bissau', 'GNB', 624, 245),
('GY', 'GUYANA', 'Guyana', 'GUY', 328, 592),
('HT', 'HAITI', 'Haiti', 'HTI', 332, 509),
('HM', 'HEARD ISLAND AND MCDONALD ISLANDS', 'Heard Island and Mcdonald Islands', NULL, NULL, 0),
('VA', 'HOLY SEE (VATICAN CITY STATE)', 'Holy See (Vatican City State)', 'VAT', 336, 39),
('HN', 'HONDURAS', 'Honduras', 'HND', 340, 504),
('HK', 'HONG KONG', 'Hong Kong', 'HKG', 344, 852),
('HU', 'HUNGARY', 'Hungary', 'HUN', 348, 36),
('IS', 'ICELAND', 'Iceland', 'ISL', 352, 354),
('IN', 'INDIA', 'India', 'IND', 356, 91),
('ID', 'INDONESIA', 'Indonesia', 'IDN', 360, 62),
('IR', 'IRAN, ISLAMIC REPUBLIC OF', 'Iran, Islamic Republic of', 'IRN', 364, 98),
('IQ', 'IRAQ', 'Iraq', 'IRQ', 368, 964),
('IE', 'IRELAND', 'Ireland', 'IRL', 372, 353),
('IL', 'ISRAEL', 'Israel', 'ISR', 376, 972),
('IT', 'ITALY', 'Italy', 'ITA', 380, 39),
('JM', 'JAMAICA', 'Jamaica', 'JAM', 388, 1876),
('JP', 'JAPAN', 'Japan', 'JPN', 392, 81),
('JO', 'JORDAN', 'Jordan', 'JOR', 400, 962),
('KZ', 'KAZAKHSTAN', 'Kazakhstan', 'KAZ', 398, 7),
('KE', 'KENYA', 'Kenya', 'KEN', 404, 254),
('KI', 'KIRIBATI', 'Kiribati', 'KIR', 296, 686),
('KP', 'KOREA, DEMOCRATIC PEOPLE''S REPUBLIC OF', 'Korea, Democratic People''s Republic of', 'PRK', 408, 850),
('KR', 'KOREA, REPUBLIC OF', 'Korea, Republic of', 'KOR', 410, 82),
('KW', 'KUWAIT', 'Kuwait', 'KWT', 414, 965),
('KG', 'KYRGYZSTAN', 'Kyrgyzstan', 'KGZ', 417, 996),
('LA', 'LAO PEOPLE''S DEMOCRATIC REPUBLIC', 'Lao People''s Democratic Republic', 'LAO', 418, 856),
('LV', 'LATVIA', 'Latvia', 'LVA', 428, 371),
('LB', 'LEBANON', 'Lebanon', 'LBN', 422, 961),
('LS', 'LESOTHO', 'Lesotho', 'LSO', 426, 266),
('LR', 'LIBERIA', 'Liberia', 'LBR', 430, 231),
('LY', 'LIBYAN ARAB JAMAHIRIYA', 'Libyan Arab Jamahiriya', 'LBY', 434, 218),
('LI', 'LIECHTENSTEIN', 'Liechtenstein', 'LIE', 438, 423),
('LT', 'LITHUANIA', 'Lithuania', 'LTU', 440, 370),
('LU', 'LUXEMBOURG', 'Luxembourg', 'LUX', 442, 352),
('MO', 'MACAO', 'Macao', 'MAC', 446, 853),
('MK', 'MACEDONIA, THE FORMER YUGOSLAV REPUBLIC OF', 'Macedonia, the Former Yugoslav Republic of', 'MKD', 807, 389),
('MG', 'MADAGASCAR', 'Madagascar', 'MDG', 450, 261),
('MW', 'MALAWI', 'Malawi', 'MWI', 454, 265),
('MY', 'MALAYSIA', 'Malaysia', 'MYS', 458, 60),
('MV', 'MALDIVES', 'Maldives', 'MDV', 462, 960),
('ML', 'MALI', 'Mali', 'MLI', 466, 223),
('MT', 'MALTA', 'Malta', 'MLT', 470, 356),
('MH', 'MARSHALL ISLANDS', 'Marshall Islands', 'MHL', 584, 692),
('MQ', 'MARTINIQUE', 'Martinique', 'MTQ', 474, 596),
('MR', 'MAURITANIA', 'Mauritania', 'MRT', 478, 222),
('MU', 'MAURITIUS', 'Mauritius', 'MUS', 480, 230),
('YT', 'MAYOTTE', 'Mayotte', NULL, NULL, 269),
('MX', 'MEXICO', 'Mexico', 'MEX', 484, 52),
('FM', 'MICRONESIA, FEDERATED STATES OF', 'Micronesia, Federated States of', 'FSM', 583, 691),
('MD', 'MOLDOVA, REPUBLIC OF', 'Moldova, Republic of', 'MDA', 498, 373),
('MC', 'MONACO', 'Monaco', 'MCO', 492, 377),
('MN', 'MONGOLIA', 'Mongolia', 'MNG', 496, 976),
('MS', 'MONTSERRAT', 'Montserrat', 'MSR', 500, 1664),
('MA', 'MOROCCO', 'Morocco', 'MAR', 504, 212),
('MZ', 'MOZAMBIQUE', 'Mozambique', 'MOZ', 508, 258),
('MM', 'MYANMAR', 'Myanmar', 'MMR', 104, 95),
('NA', 'NAMIBIA', 'Namibia', 'NAM', 516, 264),
('NR', 'NAURU', 'Nauru', 'NRU', 520, 674),
('NP', 'NEPAL', 'Nepal', 'NPL', 524, 977),
('NL', 'NETHERLANDS', 'Netherlands', 'NLD', 528, 31),
('AN', 'NETHERLANDS ANTILLES', 'Netherlands Antilles', 'ANT', 530, 599),
('NC', 'NEW CALEDONIA', 'New Caledonia', 'NCL', 540, 687),
('NZ', 'NEW ZEALAND', 'New Zealand', 'NZL', 554, 64),
('NI', 'NICARAGUA', 'Nicaragua', 'NIC', 558, 505),
('NE', 'NIGER', 'Niger', 'NER', 562, 227),
('NG', 'NIGERIA', 'Nigeria', 'NGA', 566, 234),
('NU', 'NIUE', 'Niue', 'NIU', 570, 683),
('NF', 'NORFOLK ISLAND', 'Norfolk Island', 'NFK', 574, 672),
('MP', 'NORTHERN MARIANA ISLANDS', 'Northern Mariana Islands', 'MNP', 580, 1670),
('NO', 'NORWAY', 'Norway', 'NOR', 578, 47),
('OM', 'OMAN', 'Oman', 'OMN', 512, 968),
('PK', 'PAKISTAN', 'Pakistan', 'PAK', 586, 92),
('PW', 'PALAU', 'Palau', 'PLW', 585, 680),
('PS', 'PALESTINIAN TERRITORY, OCCUPIED', 'Palestinian Territory, Occupied', NULL, NULL, 970),
('PA', 'PANAMA', 'Panama', 'PAN', 591, 507),
('PG', 'PAPUA NEW GUINEA', 'Papua New Guinea', 'PNG', 598, 675),
('PY', 'PARAGUAY', 'Paraguay', 'PRY', 600, 595),
('PE', 'PERU', 'Peru', 'PER', 604, 51),
('PH', 'PHILIPPINES', 'Philippines', 'PHL', 608, 63),
('PN', 'PITCAIRN', 'Pitcairn', 'PCN', 612, 0),
('PL', 'POLAND', 'Poland', 'POL', 616, 48),
('PT', 'PORTUGAL', 'Portugal', 'PRT', 620, 351),
('PR', 'PUERTO RICO', 'Puerto Rico', 'PRI', 630, 1787),
('QA', 'QATAR', 'Qatar', 'QAT', 634, 974),
('RE', 'REUNION', 'Reunion', 'REU', 638, 262),
('RO', 'ROMANIA', 'Romania', 'ROM', 642, 40),
('RU', 'RUSSIAN FEDERATION', 'Russian Federation', 'RUS', 643, 70),
('RW', 'RWANDA', 'Rwanda', 'RWA', 646, 250),
('SH', 'SAINT HELENA', 'Saint Helena', 'SHN', 654, 290),
('KN', 'SAINT KITTS AND NEVIS', 'Saint Kitts and Nevis', 'KNA', 659, 1869),
('LC', 'SAINT LUCIA', 'Saint Lucia', 'LCA', 662, 1758),
('PM', 'SAINT PIERRE AND MIQUELON', 'Saint Pierre and Miquelon', 'SPM', 666, 508),
('VC', 'SAINT VINCENT AND THE GRENADINES', 'Saint Vincent and the Grenadines', 'VCT', 670, 1784),
('WS', 'SAMOA', 'Samoa', 'WSM', 882, 684),
('SM', 'SAN MARINO', 'San Marino', 'SMR', 674, 378),
('ST', 'SAO TOME AND PRINCIPE', 'Sao Tome and Principe', 'STP', 678, 239),
('SA', 'SAUDI ARABIA', 'Saudi Arabia', 'SAU', 682, 966),
('SN', 'SENEGAL', 'Senegal', 'SEN', 686, 221),
('CS', 'SERBIA AND MONTENEGRO', 'Serbia and Montenegro', NULL, NULL, 381),
('SC', 'SEYCHELLES', 'Seychelles', 'SYC', 690, 248),
('SL', 'SIERRA LEONE', 'Sierra Leone', 'SLE', 694, 232),
('SG', 'SINGAPORE', 'Singapore', 'SGP', 702, 65),
('SK', 'SLOVAKIA', 'Slovakia', 'SVK', 703, 421),
('SI', 'SLOVENIA', 'Slovenia', 'SVN', 705, 386),
('SB', 'SOLOMON ISLANDS', 'Solomon Islands', 'SLB', 90, 677),
('SO', 'SOMALIA', 'Somalia', 'SOM', 706, 252),
('ZA', 'SOUTH AFRICA', 'South Africa', 'ZAF', 710, 27),
('GS', 'SOUTH GEORGIA AND THE SOUTH SANDWICH ISLANDS', 'South Georgia and the South Sandwich Islands', NULL, NULL, 0),
('ES', 'SPAIN', 'Spain', 'ESP', 724, 34),
('LK', 'SRI LANKA', 'Sri Lanka', 'LKA', 144, 94),
('SD', 'SUDAN', 'Sudan', 'SDN', 736, 249),
('SR', 'SURINAME', 'Suriname', 'SUR', 740, 597),
('SJ', 'SVALBARD AND JAN MAYEN', 'Svalbard and Jan Mayen', 'SJM', 744, 47),
('SZ', 'SWAZILAND', 'Swaziland', 'SWZ', 748, 268),
('SE', 'SWEDEN', 'Sweden', 'SWE', 752, 46),
('CH', 'SWITZERLAND', 'Switzerland', 'CHE', 756, 41),
('SY', 'SYRIAN ARAB REPUBLIC', 'Syrian Arab Republic', 'SYR', 760, 963),
('TW', 'TAIWAN, PROVINCE OF CHINA', 'Taiwan, Province of China', 'TWN', 158, 886),
('TJ', 'TAJIKISTAN', 'Tajikistan', 'TJK', 762, 992),
('TZ', 'TANZANIA, UNITED REPUBLIC OF', 'Tanzania, United Republic of', 'TZA', 834, 255),
('TH', 'THAILAND', 'Thailand', 'THA', 764, 66),
('TL', 'TIMOR-LESTE', 'Timor-Leste', NULL, NULL, 670),
('TG', 'TOGO', 'Togo', 'TGO', 768, 228),
('TK', 'TOKELAU', 'Tokelau', 'TKL', 772, 690),
('TO', 'TONGA', 'Tonga', 'TON', 776, 676),
('TT', 'TRINIDAD AND TOBAGO', 'Trinidad and Tobago', 'TTO', 780, 1868),
('TN', 'TUNISIA', 'Tunisia', 'TUN', 788, 216),
('TR', 'TURKEY', 'Turkey', 'TUR', 792, 90),
('TM', 'TURKMENISTAN', 'Turkmenistan', 'TKM', 795, 7370),
('TC', 'TURKS AND CAICOS ISLANDS', 'Turks and Caicos Islands', 'TCA', 796, 1649),
('TV', 'TUVALU', 'Tuvalu', 'TUV', 798, 688),
('UG', 'UGANDA', 'Uganda', 'UGA', 800, 256),
('UA', 'UKRAINE', 'Ukraine', 'UKR', 804, 380),
('AE', 'UNITED ARAB EMIRATES', 'United Arab Emirates', 'ARE', 784, 971),
('GB', 'UNITED KINGDOM', 'United Kingdom', 'GBR', 826, 44),
('US', 'UNITED STATES', 'United States', 'USA', 840, 1),
('UM', 'UNITED STATES MINOR OUTLYING ISLANDS', 'United States Minor Outlying Islands', NULL, NULL, 1),
('UY', 'URUGUAY', 'Uruguay', 'URY', 858, 598),
('UZ', 'UZBEKISTAN', 'Uzbekistan', 'UZB', 860, 998),
('VU', 'VANUATU', 'Vanuatu', 'VUT', 548, 678),
('VE', 'VENEZUELA', 'Venezuela', 'VEN', 862, 58),
('VN', 'VIET NAM', 'Viet Nam', 'VNM', 704, 84),
('VG', 'VIRGIN ISLANDS, BRITISH', 'Virgin Islands, British', 'VGB', 92, 1284),
('VI', 'VIRGIN ISLANDS, U.S.', 'Virgin Islands, U.s.', 'VIR', 850, 1340),
('WF', 'WALLIS AND FUTUNA', 'Wallis and Futuna', 'WLF', 876, 681),
('EH', 'WESTERN SAHARA', 'Western Sahara', 'ESH', 732, 212),
('YE', 'YEMEN', 'Yemen', 'YEM', 887, 967),
('ZM', 'ZAMBIA', 'Zambia', 'ZMB', 894, 260),
('ZW', 'ZIMBABWE', 'Zimbabwe', 'ZWE', 716, 263);
GO

UPDATE [LocalPhone].[dbo].[Country]
     SET [status] = 1
	   , [creationDate] = GETDATE()
GO

INSERT INTO [LocalPhone].[dbo].[Gender] ([Gender]
   , [Abbreviation]
   , [Description]
   , [idCountry]
   , [status]
   , [creationDate]
   , [creatorUser]
   , [lastModificationDate]
   , [userThatMadeTheLastModification])
VALUES
('Masculine', 'M', 'Gender Masculine', 30, 1, getdate(), 'Marcos', '', ''), 
('Feminine', 'F', 'Gender Feminine', 30, 1, getdate(), 'Marcos', '', '');
GO

INSERT INTO LocalPhone.dbo.State (Name, Abbreviation, idCountry) VALUES
    ('Alaska', 'AK', 226),
    ('Arizona', 'AZ', 226),
    ('Arkansas', 'AR', 226),
    ('California', 'CA', 226),
    ('Colorado', 'CO', 226),
    ('Connecticut', 'CT', 226),
    ('Delaware', 'DE', 226),
    ('District of Columbia', 'DC', 226),
    ('Florida', 'FL', 226),
    ('Georgia', 'GA', 226),
    ('Hawaii', 'HI', 226),
    ('Idaho', 'ID', 226),
    ('Illinois', 'IL', 226),
    ('Indiana', 'IN', 226),
    ('Iowa', 'IA', 226),
    ('Kansas', 'KS', 226),
    ('Kentucky', 'KY', 226),
    ('Louisiana', 'LA', 226),
    ('Maine', 'ME', 226),
    ('Maryland', 'MD', 226),
    ('Massachusetts', 'MA', 226),
    ('Michigan', 'MI', 226),
    ('Minnesota', 'MN', 226),
    ('Mississippi', 'MS', 226),
    ('Missouri', 'MO', 226),
    ('Montana', 'MT', 226),
    ('Nebraska', 'NE', 226),
    ('Nevada', 'NV', 226),
    ('New Hampshire', 'NH', 226),
    ('New Jersey', 'NJ', 226),
    ('New Mexico', 'NM', 226),
    ('New York', 'NY', 226),
    ('North Carolina', 'NC', 226),
    ('North Dakota', 'ND', 226),
    ('Ohio', 'OH', 226),
    ('Oklahoma', 'OK', 226),
    ('Oregon', 'OR', 226),
    ('Pennsylvania', 'PA', 226),
    ('Rhode Island', 'RI', 226),
    ('South Carolina', 'SC', 226),
    ('South Dakota', 'SD', 226),
    ('Tennessee', 'TN', 226),
    ('Texas', 'TX', 226),
    ('Utah', 'UT', 226),
    ('Vermont', 'VT', 226),
    ('Virginia', 'VA', 226),
    ('Washington', 'WA', 226),
    ('West Virginia', 'WV', 226),
    ('Wisconsin', 'WI', 226),
    ('Wyoming', 'WY', 226),
    ('Acre', 'AC', 30),
    ('Alagoas', 'AL', 30),
    ('Amap�', 'AP', 30),
    ('Amazonas', 'AM', 30),
    ('Bahia', 'BA', 30),
    ('Cear�', 'CE', 30),
    ('Esp�rito Santo', 'ES', 30),
    ('Goi�s', 'GO', 30),
    ('Maranh�o', 'MA', 30),
    ('Mato Grosso', 'MT', 30),
    ('Mato Grosso do Sul', 'MS', 30),
    ('Minas Gerais', 'MG', 30),
    ('Par�', 'PA', 30),
    ('Para�ba', 'PB', 30),
    ('Paran�', 'PR', 30),
    ('Pernambuco', 'PE', 30),
    ('Piau�', 'PI', 30),
    ('Rio de Janeiro', 'RJ', 30),
    ('Rio Grande do Norte', 'RN', 30),
    ('Rio Grande do Sul', 'RS', 30),
    ('Rond�nia', 'RO', 30),
    ('Roraima', 'RR', 30),
    ('Santa Catarina', 'SC', 30),
    ('S�o Paulo', 'SP', 30),
    ('Sergipe', 'SE', 30),
    ('Tocantins', 'TO', 30),
    ('Distrito Federal', 'DF', 30);
GO

UPDATE [LocalPhone].[dbo].[State]
     SET status = 1
GO

--INSERT INTO LocalPhone.dbo.CustomerStatus (description, abbreviation, value, status) values
--('Active','A',1,1),
--('Inactive','I',0,1),
--('Pending','P',2,1),
--('Verified','V',3,1);
--GO

INSERT INTO [LocalPhone].[dbo].[City]
           ([phonecode]
           ,[Description]
           ,[idState])
     VALUES

(11,	'S�o Paulo e Regi�o Metropolitana, SP', 74),
(12,	'S�o Jos� dos Campos/Taubat�/Vale do Para�ba, SP', 74),
(13,	'Santos/S�o Vicente/Vale do Ribeira, SP', 74),
(14,	'Bauru/Botucatu/Ja�/Mar�lia, SP', 74),
(15,	'Itapetininga/Itapeva/Sorocaba, SP', 74),
(16,	'Araraquara/Franca/Ribeir�o Preto/S�o Carlos, SP', 74),
(17,	'Catanduva/Barretos/S�o Jos� do Rio Preto/Votuporanga, SP', 74),
(18,	'Presidente Prudente/Ara�atuba/Birigui/Assis, SP', 74),
(19,	'Americana/Campinas/Limeira/Piracicaba, SP', 74),
(21,	'Rio de Janeiro e Regi�o Metropolitana/Teres�polis, RJ', 68),
(22,	'Cabo Frio/Campos dos Goytacazes/Maca�/Nova Friburgo, RJ', 68),
(24,	'Angra dos Reis/Petr�polis/Volta Redonda, RJ', 68),
(27,	'Vit�ria e Regi�o Metropolitana/Colatina/Domingos Martins/Linhares, ES', 57),
(28,	'Cachoeiro de Itapemirim/Castelo/Marata�zes, ES', 57),
(31,	'Belo Horizonte e Regi�o Metropolitana/Ipatinga, MG', 62),
(32,	'Juiz de Fora/S�o Jo�o del-Rei/Barbacena, MG', 62),
(33,	'Governador Valadares/Te�filo Otoni/Caratinga/Manhua�u, MG', 62),
(34,	'Araguari/Uberl�ndia/Uberaba, MG', 62),
(35,	'Po�os de Caldas/Pouso Alegre/Varginha, MG', 62),
(37,	'Divin�polis/Ita�na, MG', 62),
(38,	'Diamantina/Montes Claros/Una�, MG', 62),
(41,	'Curitiba e Regi�o Metropolitana, PR', 65),
(42,	'Ponta Grossa/Guarapuava, PR', 65),
(43,	'Apucarana/Londrina, PR', 65),
(44,	'Maring�/Campo Mour�o/Umuarama, PR', 65),
(45,	'Cascavel/Foz do Igua�u, PR', 65),
(46,	'Francisco Beltr�o/Pato Branco, PR', 65),
(47,	'Balne�rio Cambori�/Blumenau/Itaja�/Joinville, SC', 73),
(48,	'Florian�polis e Regi�o Metropolitana/Crici�ma, SC', 73),
(49,	'Ca�ador/Chapec�/Lages, SC', 73),
(51,	'Porto Alegre e Regi�o Metropolitana/Santa Cruz do Sul/Litoral Norte, RS', 70),
(53,	'Pelotas/Rio Grande, RS', 70),
(54,	'Caxias do Sul/Passo Fundo, RS', 70),
(55,	'Santa Maria/Santana do Livramento/Santo �ngelo/Uruguaiana, RS', 70),
(61,	'Distrito Federal/Goi�s,	Abrang�ncia em todo o Distrito Federal e munic�pios da Regi�o Integrada de Desenvolvimento do Distrito Federal e Entorno, DF/GO', 77),
(62,	'Goi�nia e Regi�o Metropolitana/An�polis/Niquel�ndia/Porangatu, GO', 58),
(63,	'Abrang�ncia em todo o estado, TO', 76),
(64,	'Caldas Novas/Catal�o/Itumbiara/Rio Verde, GO', 58),
(65,	'Cuiab� e Regi�o Metropolitana, MT', 60),
(66,	'Rondon�polis/Sinop, MT', 60),
(67,	'Abrang�ncia em todo o estado, MS', 61),
(68,	'Abrang�ncia em todo o estado, AC', 51),
(69,	'Abrang�ncia em todo o estado, RO', 71),
(71,	'Salvador e Regi�o Metropolitana, BA', 55),
(73,	'Ilh�us/Itabuna, BA', 55),
(74,	'Juazeiro/Irec�, BA', 55),
(75,	'Feira de Santana/Alagoinhas, BA', 55),
(77,	'Vit�ria da Conquista/Barreiras, BA', 55),
(79,	'Abrang�ncia em todo o estado, SE', 75),
(81,	'Recife e Regi�o Metropolitana/Caruaru, PE', 66),
(82,	'Abrang�ncia em todo o estado, AL', 52),
(83,	'Abrang�ncia em todo o estado, PB', 64),
(84,	'Abrang�ncia em todo o estado, RN', 69),
(85,	'Fortaleza e Regi�o Metropolitana, CE', 56),
(86,	'Teresina e Regi�o Metropolitana/Parna�ba, PI', 67),
(87,	'Garanhuns/Petrolina/Salgueiro/Serra Talhada, PE', 66),
(88,	'Juazeiro do Norte/Sobral, CE', 56),
(89,	'Picos/Floriano, PI', 67),
(91,	'Bel�m e Regi�o Metropolitana, PA', 63),
(92,	'Manaus e Regi�o Metropolitana/Parintins, AM', 54),
(93,	'Santar�m/Altamira, PA', 63),
(94,	'Marab�, PA', 63),
(95,	'Abrang�ncia em todo o estado, RR', 72),
(96,	'Abrang�ncia em todo o estado, AP', 53),
(97,	'Abrang�ncia no interior do estado, AM', 54),
(98,	'S�o Lu�s e Regi�o Metropolitana, MA', 59),
(99,	'Caxias/Cod�/Imperatriz, MA', 59);
--(201, 'Jersey City, NJ', 30),
--(202, 'District of Columbia', 226),
--(203, 'Bridgeport, CT', 226),
--(204, 'Manitoba', 226),
--(205, 'Birmingham, AL', 226),
--(206, 'Seattle, WA', 226),
--(207, 'Portland, ME', 226),
--(208, 'Idaho', 226),
--(209, 'Stockton, CA', 226),
--(210, 'San Antonio, TX', 226),
--(212, 'New York, NY', 226),
--(213, 'Los Angeles, CA', 226),
--(214, 'Dallas, TX', 226),
--(215, 'Philadelphia, PA', 226),
--(216, 'Cleveland, OH', 226),
--(217, 'Springfield, IL', 226),
--(218, 'Duluth, MN', 226),
--(219, 'Hammond, IN', 226),
--(220, 'Newark, OH', 226),
--(224, 'Elgin, IL', 226),
--(225, 'Baton Rouge, LA', 226),
--(226, 'London, ON', 226),
--(228, 'Gulfport, MS', 226),
--(229, 'Albany, GA', 226),
--(231, 'Muskegon, MI', 226),
--(234, 'Akron, OH', 226),
--(236, 'Vancouver, BC', 226),
--(239, 'Cape Coral, FL', 226),
--(240, 'Germantown, MD', 226),
--(248, 'Troy, MI', 226),
--(249, 'Sudbury, ON', 226),
--(250, 'Kelowna, BC', 226),
--(251, 'Mobile, AL', 226),
--(252, 'Greenville, NC', 226),
--(253, 'Tacoma, WA', 226),
--(254, 'Killeen, TX', 226),
--(256, 'Huntsville, AL', 226),
--(260, 'Fort Wayne, IN', 226),
--(262, 'Kenosha, WI', 226),
--(267, 'Philadelphia, PA', 226),
--(269, 'Kalamazoo, MI', 226),
--(270, 'Bowling Green, KY', 226),
--(272, 'Scranton, PA', 226),
--(276, 'Bristol, VA', 226),
--(281, 'Houston, TX', 226),
--(289, 'Hamilton, ON', 226),
--(661, 'Germantown, MD', 226),
--(662, 'Delaware', 226),
--(663, 'Denver, CO', 226),
--(664, 'West Virginia', 226),
--(665, 'Miami, FL', 226),
--(666, 'Saskatchewan', 226),
--(667, 'Wyoming', 226),
--(668, 'Grand Island, N', 226),
--(669, 'Peoria, IL', 226),
--(310, 'Los Angeles, CA', 226),
--(312, 'Chicago, IL', 226),
--(313, 'Detroit, MI', 226),
--(314, 'St. Louis, MO', 226),
--(315, 'Syracuse, NY', 226),
--(316, 'Wichita, KS', 226),
--(317, 'Indianapolis city (balance), IN', 226),
--(318, 'Shreveport, LA', 226),
--(319, 'Cedar Rapids, IA', 226),
--(320, 'St. Cloud, MN', 226),
--(321, 'Orlando, FL', 226),
--(323, 'Los Angeles, CA', 226),
--(325, 'Abilene, TX', 226),
--(366, 'Akron, OH', 226),
--(331, 'Aurora, IL', 226),
--(332, 'New York, NY', 226),
--(334, 'Montgomery, AL', 226),
--(336, 'Greensboro, NC', 226),
--(337, 'Lafayette, LA', 226),
--(339, 'Boston, MA', 226),
--(340, 'Virgin Islands', 226),
--(343, 'Ottawa, ON', 226),
--(346, 'Houston, TX', 226),
--(347, 'New York, NY', 226),
--(351, 'Lowell, MA', 226),
--(352, 'Gainesville, FL', 226),
--(360, 'Vancouver, WA', 226),
--(361, 'Corpus Christi, TX', 226),
--(364, 'Bowling Green, KY', 226),
--(365, 'Hamilton, ON', 226),
--(380, 'Columbus, OH', 226),
--(385, 'Salt Lake City, UT', 226),
--(386, 'Palm Coast, FL', 226),
--(401, 'Providence, RI', 226),
--(402, 'Omaha, NE', 226),
----(403, 'Calgary, AB', 226),
--(404, 'Atlanta, GA', 226),
--(405, 'Oklahoma City, OK', 226),
--(406, 'Montana', 226),
--(407, 'Orlando, FL', 226),
--(408, 'San Jose, CA', 226),
--(409, 'Beaumont, TX', 226),
--(410, 'Baltimore, MD', 226),
--(412, 'Pittsburgh, PA', 226),
--(413, 'Springfield, MA', 226),
--(414, 'Milwaukee, WI', 226),
--(415, 'San Francisco, CA', 226),
----(416, 'Toronto, ON', 226),
--(417, 'Springfield, MO', 226),
--(418, 'Quebec, QC', 226),
--(419, 'Toledo, OH', 226),
--(423, 'Chattanooga, TN', 226),
--(424, 'Los Angeles, CA', 226),
--(425, 'Bellevue, WA', 226),
--(466, 'Tyler, TX', 226),
--(431, 'Manitoba', 226),
--(432, 'Midland, TX', 226),
--(434, 'Lynchburg, VA', 226),
--(435, 'St. George, UT', 226),
--(437, 'Toronto, ON', 226),
--(438, 'Montreal, QC', 226),
--(440, 'Parma, OH', 226),
--(442, 'Oceanside, CA', 226),
--(443, 'Baltimore, MD', 226),
--(450, 'Granby, QC', 226),
--(458, 'Eugene, OR', 226),
--(463, 'Indianapolis city (balance), IN', 226),
--(469, 'Dallas, TX', 226),
--(470, 'Atlanta, GA', 226),
--(475, 'Bridgeport, CT', 226),
--(478, 'Macon, GA', 226),
--(479, 'Fort Smith, AR', 226),
--(480, 'Mesa, AZ', 226),
--(484, 'Allentown, PA', 226),
--(501, 'Little Rock, AR', 226),
--(502, 'Louisville, KY', 226),
--(503, 'Portland, OR', 226),
--(504, 'New Orleans, LA', 226),
--(505, 'Albuquerque, NM', 226),
--(506, 'New Brunswick', 226),
--(507, 'Rochester, MN', 226),
--(508, 'Worcester, MA', 226),
--(509, 'Spokane, WA', 226),
--(510, 'Oakland, CA', 226),
--(512, 'Austin, TX', 226),
--(513, 'Cincinnati, OH', 226),
----(514, 'Montreal, QC', 226),
--(515, 'Des Moines, IA', 226),
--(516, 'Hempstead, NY', 226),
--(517, 'Lansing, MI', 226),
--(518, 'Albany, NY', 226),
----(519, 'London, ON', 226),
--(520, 'Tucson, AZ', 226),
--(566, 'Redding, CA', 226),
--(531, 'Omaha, NE', 226),
--(534, 'Eau Claire, WI', 226),
--(539, 'Tulsa, OK', 226),
--(540, 'Roanoke, VA', 226),
--(541, 'Eugene, OR', 226),
--(548, 'London, ON', 226),
--(551, 'Jersey City, NJ', 226),
--(559, 'Fresno, CA', 226),
--(561, 'West Palm Beach, FL', 226),
--(562, 'Long Beach, CA', 226),
--(563, 'Davenport, IA', 226),
--(567, 'Toledo, OH', 226),
--(570, 'Scranton, PA', 226),
--(571, 'Arlington, VA', 226),
--(573, 'Columbia, MO', 226),
--(574, 'South Bend, IN', 226),
--(575, 'Las Cruces, NM', 226),
--(579, 'Granby, QC', 226),
--(580, 'Lawton, OK', 226),
--(581, 'Quebec, QC', 226),
--(585, 'Rochester, NY', 226),
--(586, 'Warren, MI', 226),
--(587, 'Calgary, AB', 226),
--(601, 'Jackson, MS', 226),
--(602, 'Phoenix, AZ', 226),
--(603, 'New Hampshire', 226),
----(604, 'Vancouver, BC', 226),
--(605, 'South Dakota', 226),
--(606, 'Ashland, KY', 226),
--(607, 'Binghamton, NY', 226),
--(608, 'Madison, WI', 226),
--(609, 'Trenton, NJ', 226),
--(610, 'Allentown, PA', 226),
--(612, 'Minneapolis, MN', 226),
----(613, 'Ottawa, ON', 226),
--(614, 'Columbus, OH', 226),
--(615, 'Nashville, TN', 226),
--(616, 'Grand Rapids, MI', 226),
--(617, 'Boston, MA', 226),
--(618, 'Belleville, IL', 226),
--(619, 'San Diego, CA', 226),
--(620, 'Hutchinson, KS', 226),
--(623, 'Phoenix, AZ', 226),
--(626, 'Pasadena, CA', 226),
--(628, 'San Francisco, CA', 226),
--(629, 'Nashville, TN', 226),
--(666, 'Aurora, IL', 226),
--(631, 'Brentwood, NY', 226),
--(636, 'O Fallon, MO', 226),
--(639, 'Saskatchewan', 226),
--(641, 'Mason City, IA', 226),
--(646, 'New York, NY', 226),
--(647, 'Toronto, ON', 226),
--(650, 'San Mateo, CA', 226),
--(651, 'St. Paul, MN', 226),
--(657, 'Anaheim, CA', 226),
--(660, 'Sedalia, MO', 226),
--(661, 'Bakersfield, CA', 226),
--(662, 'Southaven, MS', 226),
--(667, 'Baltimore, MD', 226),
--(669, 'San Jose, CA', 226),
--(670, 'Northern Mariana Isl,ands ', 226),
--(671, 'Guam', 226),
--(678, 'Atlanta, GA', 226),
--(680, 'Syracuse, NY', 226),
--(681, 'West Virginia', 226),
--(682, 'Fort Worth, TX', 226),
--(684, 'American Samoa', 226),
--(701, 'North Dakota', 226),
--(702, 'Las Vegas, NV', 226),
--(703, 'Arlington, VA', 226),
--(704, 'Charlotte, NC', 226),
--(705, 'Sudbury, ON', 226),
--(706, 'Augusta, GA', 226),
--(707, 'Santa Rosa, CA', 226),
--(708, 'Cicero, IL', 226),
--(709, 'Newfoundland/Labrador', 226),
--(712, 'Sioux City, IA', 226),
--(713, 'Houston, TX', 226),
--(714, 'Anaheim, CA', 226),
--(715, 'Eau Claire, WI', 226),
--(716, 'Buffalo, NY', 226),
--(717, 'Lancaster, PA', 226),
--(718, 'New York, NY', 226),
--(719, 'Colorado Springs, CO', 226),
--(720, 'Denver, CO', 226),
--(724, 'New Castle, PA', 226),
--(725, 'Las Vegas, NV', 226),
--(727, 'St. Petersburg, FL', 226),
--(731, 'Jackson, TN', 226),
--(732, 'Toms River, NJ', 226),
--(734, 'Ann Arbor, MI', 226),
--(737, 'Austin, TX', 226),
--(740, 'Newark, OH', 226),
--(743, 'Greensboro, NC', 226),
--(747, 'Los Angeles, CA', 226),
--(754, 'Fort Lauderdale, FL', 226),
--(757, 'Virginia Beach, VA', 226),
--(760, 'Oceanside, CA', 226),
--(762, 'Augusta, GA', 226),
--(763, 'Brooklyn Park, MN', 226),
--(765, 'Muncie, IN', 226),
--(769, 'Jackson, MS', 226),
--(770, 'Roswell, GA', 226),
--(772, 'Port St. Lucie, FL', 226),
--(773, 'Chicago, IL', 226),
--(774, 'Worcester, MA', 226),
--(775, 'Reno, NV', 226),
--(778, 'Vancouver, BC', 226),
--(779, 'Rockford, IL', 226),
--(780, 'Edmonton, AB', 226),
--(781, 'Boston, MA', 226),
--(782, 'Nova Scotia/PE Isl,and', 226),
--(785, 'Topeka, KS', 226),
--(786, 'Miami, FL', 226),
--(787, 'Puerto Rico', 226),
--(801, 'Salt Lake City, UT', 226),
--(802, 'Vermont', 226),
--(803, 'Columbia, SC', 226),
--(804, 'Richmond, VA', 226),
--(805, 'Oxnard, CA', 226),
--(806, 'Lubbock, TX', 226),
--(807, 'Kenora, ON', 226),
--(808, 'Hawaii', 226),
--(810, 'Flint, MI', 226),
--(812, 'Evansville, IN', 226),
--(813, 'Tampa, FL', 226),
--(814, 'Erie, PA', 226),
--(815, 'Rockford, IL', 226),
--(816, 'Kansas City, MO', 226),
--(817, 'Fort Worth, TX', 226),
--(818, 'Los Angeles, CA', 226),
--(819, 'Sherbrooke, QC', 226),
--(825, 'Calgary, AB', 226),
--(828, 'Asheville, NC', 226),
--(866, 'New Braunfels, TX', 226),
--(831, 'Salinas, CA', 226),
--(832, 'Houston, TX', 226),
--(843, 'Charleston, SC', 226),
--(845, 'New City, NY', 226),
--(847, 'Elgin, IL', 226),
--(848, 'Toms River, NJ', 226),
--(850, 'Tallahassee, FL', 226),
--(854, 'Charleston, SC', 226),
--(856, 'Camden, NJ', 226),
--(857, 'Boston, MA', 226),
--(858, 'San Diego, CA', 226),
--(859, 'Lexington-Fayette, KY', 226),
--(860, 'Hartford, CT', 226),
--(862, 'Newark, NJ', 226),
--(863, 'Lakeland, FL', 226),
--(864, 'Greenville, SC', 226),
--(865, 'Knoxville, TN', 226),
--(867, 'Northern Canada', 226),
--(870, 'Jonesboro, AR', 226),
--(872, 'Chicago, IL', 226),
--(873, 'Sherbrooke, QC', 226),
--(878, 'Pittsburgh, PA', 226),
--(901, 'Memphis, TN', 226),
--(902, 'Nova Scotia/PE Island ', 226),
--(903, 'Tyler, TX', 226),
--(904, 'Jacksonville, FL', 226),
--(905, 'Hamilton, ON', 226),
--(906, 'Marquette, MI', 226),
--(907, 'Alaska', 226),
--(908, 'Elizabeth, NJ', 226),
--(909, 'San Bernardino, CA', 226),
--(910, 'Fayetteville, NC', 226),
--(912, 'Savannah, GA', 226),
--(913, 'Overland Park, KS', 226),
--(914, 'Yonkers, NY', 226),
--(915, 'El Paso, TX', 226),
--(916, 'Sacramento, CA', 226),
--(917, 'New York, NY', 226),
--(918, 'Tulsa, OK', 226),
--(919, 'Raleigh, NC', 226),
--(920, 'Green Bay, WI', 226),
--(925, 'Concord, CA', 226),
--(928, 'Yuma, AZ', 226),
--(929, 'New York, NY', 226),
--(966, 'Evansville, IN', 226),
--(931, 'Clarksville, TN', 226),
--(934, 'Brentwood, NY', 226),
--(936, 'Conroe, TX', 226),
--(937, 'Dayton, OH', 226),
--(938, 'Huntsville, AL', 226),
--(939, 'Puerto Rico', 226),
--(940, 'Denton, TX', 226),
--(941, 'North Port, FL', 226),
--(947, 'Troy, MI', 226),
--(949, 'Irvine, CA', 226),
--(951, 'Riverside, CA', 226),
--(952, 'Bloomington, MN', 226),
--(954, 'Fort Lauderdale, FL', 226),
--(956, 'Laredo, TX', 226),
--(959, 'Hartford, CT', 226),
--(970, 'Fort Collins, CO', 226),
--(971, 'Portland, OR', 226),
--(972, 'Dallas, TX', 226),
--(973, 'Newark, NJ', 226),
--(978, 'Lowell, MA', 226),
--(979, 'College Station, TX', 226),
--(980, 'Charlotte, NC', 226),
--(984, 'Raleigh, NC', 226),
--(985, 'Houma, LA', 226),
--(989, 'Saginaw, MI', 226),

--(250, 'Alert Bay', 38),
--(905, 'Brampton', 38), 
--(905, 'Burlington', 38),
--(403, 'Calgary', 38),
--(418, 'Chicoutimi', 38), 
--(613, 'Cornwall', 38),
--(250, 'Dawson Creek', 38),
--(403, 'Edmonton', 38),
--(905, 'Fort Erie', 38),
--(250, 'Fort Nelson', 38),
--(709, 'Grand Falls', 38),
--(902, 'Halifax', 38),
--(514, 'Joliette', 38),
--(907, 'Juneau', 38),
--(613, 'Kingston', 38),
--(519, 'Kitchener', 38),
--(705, 'Lindsay', 38),
--(519, 'London', 38),
--(905, 'Markham', 38),
--(705, 'Midland', 38),
--(905, 'Mississauga', 38), 
--(514, 'Montreal', 38),
--(250, 'Nelson', 38),
--(902, 'New Glasgow', 38), 
--(604, 'New Westminst', 38),
--(506, 'Newcastle', 38),
--(905, 'Newmarket', 38),
--(905, 'Niagara Falls', 38),
--(705, 'North Bay', 38),
--(416, 'North York', 38),
--(519, 'Orangeville', 38),
--(905, 'Oshawa', 38),
--(613, 'Ottawa', 38),
--(613, 'Pembroke', 38),
--(418, 'Quebec City', 38),
--(666, 'Regina', 38),
--(416, 'Toronto', 38),
--(819, 'Trois Rivier', 38), 
--(604, 'Vancouver', 38),
--(250, 'Victoria', 38),
--(250, 'Victoria Falls', 38),	 
--(604, 'Whistler', 38),
--(519, 'Windsor', 38),
--(204, 'Winnipeg', 38)




--USE [LocalPhone]
--GO
----DROP TABLE [dbo].[City];
----GO
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
--CREATE TABLE [dbo].[City](
--	[id] [int] IDENTITY(1,1) NOT NULL,
--	[phonecode] [int] NOT NULL,
--	[Description] [nvarchar](800) NOT NULL,	
--	[idState] [int] NOT NULL,
--PRIMARY KEY CLUSTERED 
--(
--	[id] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--) ON [PRIMARY]
--GO
--ALTER TABLE [dbo].[City]  WITH CHECK ADD FOREIGN KEY([idState])
--REFERENCES [dbo].[Country] ([id])
--GO
--ALTER TABLE [dbo].[City]  WITH CHECK ADD  CONSTRAINT [FK_CityCountry] FOREIGN KEY([idState])
--REFERENCES [dbo].[Country] ([id])
--GO
--ALTER TABLE [dbo].[City] CHECK CONSTRAINT [FK_CityCountry]
--GO

--INSERT INTO [dbo].[City]
--           ([phonecode]
--           ,[Description]
--           ,[idState])
--     VALUES

--(11,	'S�o Paulo e Regi�o Metropolitana', 66),
--(12,	'S�o Paulo,	S�o Jos� dos Campos/Taubat�/Vale do Para�ba', 66),
--(13,	'S�o Paulo,	Santos/S�o Vicente/Vale do Ribeira', 66),
--(14,	'S�o Paulo,	Bauru/Botucatu/Ja�/Mar�lia', 66),
--(15,	'S�o Paulo,	Itapetininga/Itapeva/Sorocaba', 66),
--(16,	'S�o Paulo,	Araraquara/Franca/Ribeir�o Preto/S�o Carlos', 66),
--(17,	'S�o Paulo,	Catanduva/Barretos/S�o Jos� do Rio Preto/Votuporanga', 66),
--(18,	'S�o Paulo,	Presidente Prudente/Ara�atuba/Birigui/Assis', 66),
--(19,	'S�o Paulo,	Americana/Campinas/Limeira/Piracicaba', 66),
--(21,	'Rio de Janeiro,	Rio de Janeiro e Regi�o Metropolitana/Teres�polis', 66),
--(22,	'Rio de Janeiro,	Cabo Frio/Campos dos Goytacazes/Maca�/Nova Friburgo', 66),
--(24,	'Rio de Janeiro,	Angra dos Reis/Petr�polis/Volta Redonda', 66),
--(27,	'Esp�rito Santo,	Vit�ria e Regi�o Metropolitana/Colatina/Domingos Martins/Linhares', 66),
--(28,	'Esp�rito Santo,	Cachoeiro de Itapemirim/Castelo/Marata�zes', 66),
--(31,	'Minas Gerais,	Belo Horizonte e Regi�o Metropolitana/Ipatinga', 66),
--(32,	'Minas Gerais,	Juiz de Fora/S�o Jo�o del-Rei/Barbacena', 66),
--(33,	'Minas Gerais,	Governador Valadares/Te�filo Otoni/Caratinga/Manhua�u', 66),
--(34,	'Minas Gerais,	Araguari/Uberl�ndia/Uberaba', 66),
--(35,	'Minas Gerais,	Po�os de Caldas/Pouso Alegre/Varginha', 66),
--(37,	'Minas Gerais,	Divin�polis/Ita�na', 66),
--(38,	'Minas Gerais,	Diamantina/Montes Claros/Una�', 66),
--(41,	'Paran�,	Curitiba e Regi�o Metropolitana', 66),
--(42,	'Paran�,	Ponta Grossa/Guarapuava', 66),
--(43,	'Paran�,	Apucarana/Londrina', 66),
--(44,	'Paran�,	Maring�/Campo Mour�o/Umuarama', 66),
--(45,	'Paran�,	Cascavel/Foz do Igua�u', 66),
--(46,	'Paran�,	Francisco Beltr�o/Pato Branco', 66),
--(47,	'Santa Catarina,	Balne�rio Cambori�/Blumenau/Itaja�/Joinville', 66),
--(48,	'Santa Catarina,	Florian�polis e Regi�o Metropolitana/Crici�ma', 66),
--(49,	'Santa Catarina,	Ca�ador/Chapec�/Lages', 66),
--(51,	'Rio Grande do Sul,	Porto Alegre e Regi�o Metropolitana/Santa Cruz do Sul/Litoral Norte', 66),
--(53,	'Rio Grande do Sul,	Pelotas/Rio Grande', 66),
--(54,	'Rio Grande do Sul,	Caxias do Sul/Passo Fundo', 66),
--(55,	'Rio Grande do Sul,	Santa Maria/Santana do Livramento/Santo �ngelo/Uruguaiana', 66),
--(61,	'Distrito Federal/Goi�s,	Abrang�ncia em todo o Distrito Federal e munic�pios da Regi�o Integrada de Desenvolvimento do Distrito Federal e Entorno', 66),
--(62,	'Goi�s,	Goi�nia e Regi�o Metropolitana/An�polis/Niquel�ndia/Porangatu', 66),
--(63,	'Tocantins,	Abrang�ncia em todo o estado', 66),
--(64,	'Goi�s,	Caldas Novas/Catal�o/Itumbiara/Rio Verde', 66),
--(65,	'Mato Grosso,	Cuiab� e Regi�o Metropolitana', 66),
--(66,	'Mato Grosso,	Rondon�polis/Sinop', 66),
--(67,	'Mato Grosso do Sul,	Abrang�ncia em todo o estado', 66),
--(68,	'Acre,	Abrang�ncia em todo o estado', 66),
--(69,	'Rond�nia,	Abrang�ncia em todo o estado', 66),
--(71,	'Bahia,	Salvador e Regi�o Metropolitana', 66),
--(73,	'Bahia,	Ilh�us/Itabuna', 66),
--(74,	'Bahia,	Juazeiro/Irec�', 66),
--(75,	'Bahia,	Feira de Santana/Alagoinhas', 66),
--(77,	'Bahia,	Vit�ria da Conquista/Barreiras', 66),
--(79,	'Sergipe,	Abrang�ncia em todo o estado', 66),
--(81,	'Pernambuco	Recife e Regi�o Metropolitana/Caruaru', 66),
--(82,	'Alagoas,	Abrang�ncia em todo o estado', 66),
--(83,	'Para�ba,	Abrang�ncia em todo o estado', 66),
--(84,	'Rio Grande do Norte,	Abrang�ncia em todo o estado', 66),
--(85,	'Cear�,	Fortaleza e Regi�o Metropolitana', 66),
--(86,	'Piau�,	Teresina e Regi�o Metropolitana/Parna�ba', 66),
--(87,	'Pernambuco	Garanhuns/Petrolina/Salgueiro/Serra Talhada', 66),
--(88,	'Cear�,	Juazeiro do Norte/Sobral', 66),
--(89,	'Piau�,	Picos/Floriano', 66),
--(91,	'Par�,	Bel�m e Regi�o Metropolitana', 66),
--(92,	'Amazonas,	Manaus e Regi�o Metropolitana/Parintins', 66),
--(93,	'Par�,	Santar�m/Altamira', 66),
--(94,	'Par�,	Marab�', 66),
--(95,	'Roraima,	Abrang�ncia em todo o estado', 66),
--(96,	'Amap�,	Abrang�ncia em todo o estado', 66),
--(97,	'Amazonas,	Abrang�ncia no interior do estado', 66),
--(98,	'Maranh�o,	S�o Lu�s e Regi�o Metropolitana', 66),
--(99,	'Maranh�o,	Caxias/Cod�/Imperatriz', 66),

--(201, 'Jersey City, NJ', 226),
--(202, 'District of Columbia', 226),
--(203, 'Bridgeport, CT', 226),
--(204, 'Manitoba', 226),
--(205, 'Birmingham, AL', 226),
--(206, 'Seattle, WA', 226),
--(207, 'Portland, ME', 226),
--(208, 'Idaho', 226),
--(209, 'Stockton, CA', 226),
--(210, 'San Antonio, TX', 226),
--(212, 'New York, NY', 226),
--(213, 'Los Angeles, CA', 226),
--(214, 'Dallas, TX', 226),
--(215, 'Philadelphia, PA', 226),
--(216, 'Cleveland, OH', 226),
--(217, 'Springfield, IL', 226),
--(218, 'Duluth, MN', 226),
--(219, 'Hammond, IN', 226),
--(220, 'Newark, OH', 226),
--(224, 'Elgin, IL', 226),
--(225, 'Baton Rouge, LA', 226),
--(226, 'London, ON', 226),
--(228, 'Gulfport, MS', 226),
--(229, 'Albany, GA', 226),
--(231, 'Muskegon, MI', 226),
--(234, 'Akron, OH', 226),
--(236, 'Vancouver, BC', 226),
--(239, 'Cape Coral, FL', 226),
--(240, 'Germantown, MD', 226),
--(248, 'Troy, MI', 226),
--(249, 'Sudbury, ON', 226),
--(250, 'Kelowna, BC', 226),
--(251, 'Mobile, AL', 226),
--(252, 'Greenville, NC', 226),
--(253, 'Tacoma, WA', 226),
--(254, 'Killeen, TX', 226),
--(256, 'Huntsville, AL', 226),
--(260, 'Fort Wayne, IN', 226),
--(262, 'Kenosha, WI', 226),
--(267, 'Philadelphia, PA', 226),
--(269, 'Kalamazoo, MI', 226),
--(270, 'Bowling Green, KY', 226),
--(272, 'Scranton, PA', 226),
--(276, 'Bristol, VA', 226),
--(281, 'Houston, TX', 226),
--(289, 'Hamilton, ON', 226),
--(661, 'Germantown, MD', 226),
--(662, 'Delaware', 226),
--(663, 'Denver, CO', 226),
--(664, 'West Virginia', 226),
--(665, 'Miami, FL', 226),
--(666, 'Saskatchewan', 226),
--(667, 'Wyoming', 226),
--(668, 'Grand Island, N', 226),
--(669, 'Peoria, IL', 226),
--(310, 'Los Angeles, CA', 226),
--(312, 'Chicago, IL', 226),
--(313, 'Detroit, MI', 226),
--(314, 'St. Louis, MO', 226),
--(315, 'Syracuse, NY', 226),
--(316, 'Wichita, KS', 226),
--(317, 'Indianapolis city (balance), IN', 226),
--(318, 'Shreveport, LA', 226),
--(319, 'Cedar Rapids, IA', 226),
--(320, 'St. Cloud, MN', 226),
--(321, 'Orlando, FL', 226),
--(323, 'Los Angeles, CA', 226),
--(325, 'Abilene, TX', 226),
--(366, 'Akron, OH', 226),
--(331, 'Aurora, IL', 226),
--(332, 'New York, NY', 226),
--(334, 'Montgomery, AL', 226),
--(336, 'Greensboro, NC', 226),
--(337, 'Lafayette, LA', 226),
--(339, 'Boston, MA', 226),
--(340, 'Virgin Islands', 226),
--(343, 'Ottawa, ON', 226),
--(346, 'Houston, TX', 226),
--(347, 'New York, NY', 226),
--(351, 'Lowell, MA', 226),
--(352, 'Gainesville, FL', 226),
--(360, 'Vancouver, WA', 226),
--(361, 'Corpus Christi, TX', 226),
--(364, 'Bowling Green, KY', 226),
--(365, 'Hamilton, ON', 226),
--(380, 'Columbus, OH', 226),
--(385, 'Salt Lake City, UT', 226),
--(386, 'Palm Coast, FL', 226),
--(401, 'Providence, RI', 226),
--(402, 'Omaha, NE', 226),
----(403, 'Calgary, AB', 226),
--(404, 'Atlanta, GA', 226),
--(405, 'Oklahoma City, OK', 226),
--(406, 'Montana', 226),
--(407, 'Orlando, FL', 226),
--(408, 'San Jose, CA', 226),
--(409, 'Beaumont, TX', 226),
--(410, 'Baltimore, MD', 226),
--(412, 'Pittsburgh, PA', 226),
--(413, 'Springfield, MA', 226),
--(414, 'Milwaukee, WI', 226),
--(415, 'San Francisco, CA', 226),
----(416, 'Toronto, ON', 226),
--(417, 'Springfield, MO', 226),
--(418, 'Quebec, QC', 226),
--(419, 'Toledo, OH', 226),
--(423, 'Chattanooga, TN', 226),
--(424, 'Los Angeles, CA', 226),
--(425, 'Bellevue, WA', 226),
--(466, 'Tyler, TX', 226),
--(431, 'Manitoba', 226),
--(432, 'Midland, TX', 226),
--(434, 'Lynchburg, VA', 226),
--(435, 'St. George, UT', 226),
--(437, 'Toronto, ON', 226),
--(438, 'Montreal, QC', 226),
--(440, 'Parma, OH', 226),
--(442, 'Oceanside, CA', 226),
--(443, 'Baltimore, MD', 226),
--(450, 'Granby, QC', 226),
--(458, 'Eugene, OR', 226),
--(463, 'Indianapolis city (balance), IN', 226),
--(469, 'Dallas, TX', 226),
--(470, 'Atlanta, GA', 226),
--(475, 'Bridgeport, CT', 226),
--(478, 'Macon, GA', 226),
--(479, 'Fort Smith, AR', 226),
--(480, 'Mesa, AZ', 226),
--(484, 'Allentown, PA', 226),
--(501, 'Little Rock, AR', 226),
--(502, 'Louisville, KY', 226),
--(503, 'Portland, OR', 226),
--(504, 'New Orleans, LA', 226),
--(505, 'Albuquerque, NM', 226),
--(506, 'New Brunswick', 226),
--(507, 'Rochester, MN', 226),
--(508, 'Worcester, MA', 226),
--(509, 'Spokane, WA', 226),
--(510, 'Oakland, CA', 226),
--(512, 'Austin, TX', 226),
--(513, 'Cincinnati, OH', 226),
----(514, 'Montreal, QC', 226),
--(515, 'Des Moines, IA', 226),
--(516, 'Hempstead, NY', 226),
--(517, 'Lansing, MI', 226),
--(518, 'Albany, NY', 226),
----(519, 'London, ON', 226),
--(520, 'Tucson, AZ', 226),
--(566, 'Redding, CA', 226),
--(531, 'Omaha, NE', 226),
--(534, 'Eau Claire, WI', 226),
--(539, 'Tulsa, OK', 226),
--(540, 'Roanoke, VA', 226),
--(541, 'Eugene, OR', 226),
--(548, 'London, ON', 226),
--(551, 'Jersey City, NJ', 226),
--(559, 'Fresno, CA', 226),
--(561, 'West Palm Beach, FL', 226),
--(562, 'Long Beach, CA', 226),
--(563, 'Davenport, IA', 226),
--(567, 'Toledo, OH', 226),
--(570, 'Scranton, PA', 226),
--(571, 'Arlington, VA', 226),
--(573, 'Columbia, MO', 226),
--(574, 'South Bend, IN', 226),
--(575, 'Las Cruces, NM', 226),
--(579, 'Granby, QC', 226),
--(580, 'Lawton, OK', 226),
--(581, 'Quebec, QC', 226),
--(585, 'Rochester, NY', 226),
--(586, 'Warren, MI', 226),
--(587, 'Calgary, AB', 226),
--(601, 'Jackson, MS', 226),
--(602, 'Phoenix, AZ', 226),
--(603, 'New Hampshire', 226),
----(604, 'Vancouver, BC', 226),
--(605, 'South Dakota', 226),
--(606, 'Ashland, KY', 226),
--(607, 'Binghamton, NY', 226),
--(608, 'Madison, WI', 226),
--(609, 'Trenton, NJ', 226),
--(610, 'Allentown, PA', 226),
--(612, 'Minneapolis, MN', 226),
----(613, 'Ottawa, ON', 226),
--(614, 'Columbus, OH', 226),
--(615, 'Nashville, TN', 226),
--(616, 'Grand Rapids, MI', 226),
--(617, 'Boston, MA', 226),
--(618, 'Belleville, IL', 226),
--(619, 'San Diego, CA', 226),
--(620, 'Hutchinson, KS', 226),
--(623, 'Phoenix, AZ', 226),
--(626, 'Pasadena, CA', 226),
--(628, 'San Francisco, CA', 226),
--(629, 'Nashville, TN', 226),
--(666, 'Aurora, IL', 226),
--(631, 'Brentwood, NY', 226),
--(636, 'O Fallon, MO', 226),
--(639, 'Saskatchewan', 226),
--(641, 'Mason City, IA', 226),
--(646, 'New York, NY', 226),
--(647, 'Toronto, ON', 226),
--(650, 'San Mateo, CA', 226),
--(651, 'St. Paul, MN', 226),
--(657, 'Anaheim, CA', 226),
--(660, 'Sedalia, MO', 226),
--(661, 'Bakersfield, CA', 226),
--(662, 'Southaven, MS', 226),
--(667, 'Baltimore, MD', 226),
--(669, 'San Jose, CA', 226),
--(670, 'Northern Mariana Isl,ands ', 226),
--(671, 'Guam', 226),
--(678, 'Atlanta, GA', 226),
--(680, 'Syracuse, NY', 226),
--(681, 'West Virginia', 226),
--(682, 'Fort Worth, TX', 226),
--(684, 'American Samoa', 226),
--(701, 'North Dakota', 226),
--(702, 'Las Vegas, NV', 226),
--(703, 'Arlington, VA', 226),
--(704, 'Charlotte, NC', 226),
--(705, 'Sudbury, ON', 226),
--(706, 'Augusta, GA', 226),
--(707, 'Santa Rosa, CA', 226),
--(708, 'Cicero, IL', 226),
--(709, 'Newfoundland/Labrador', 226),
--(712, 'Sioux City, IA', 226),
--(713, 'Houston, TX', 226),
--(714, 'Anaheim, CA', 226),
--(715, 'Eau Claire, WI', 226),
--(716, 'Buffalo, NY', 226),
--(717, 'Lancaster, PA', 226),
--(718, 'New York, NY', 226),
--(719, 'Colorado Springs, CO', 226),
--(720, 'Denver, CO', 226),
--(724, 'New Castle, PA', 226),
--(725, 'Las Vegas, NV', 226),
--(727, 'St. Petersburg, FL', 226),
--(731, 'Jackson, TN', 226),
--(732, 'Toms River, NJ', 226),
--(734, 'Ann Arbor, MI', 226),
--(737, 'Austin, TX', 226),
--(740, 'Newark, OH', 226),
--(743, 'Greensboro, NC', 226),
--(747, 'Los Angeles, CA', 226),
--(754, 'Fort Lauderdale, FL', 226),
--(757, 'Virginia Beach, VA', 226),
--(760, 'Oceanside, CA', 226),
--(762, 'Augusta, GA', 226),
--(763, 'Brooklyn Park, MN', 226),
--(765, 'Muncie, IN', 226),
--(769, 'Jackson, MS', 226),
--(770, 'Roswell, GA', 226),
--(772, 'Port St. Lucie, FL', 226),
--(773, 'Chicago, IL', 226),
--(774, 'Worcester, MA', 226),
--(775, 'Reno, NV', 226),
--(778, 'Vancouver, BC', 226),
--(779, 'Rockford, IL', 226),
--(780, 'Edmonton, AB', 226),
--(781, 'Boston, MA', 226),
--(782, 'Nova Scotia/PE Isl,and', 226),
--(785, 'Topeka, KS', 226),
--(786, 'Miami, FL', 226),
--(787, 'Puerto Rico', 226),
--(801, 'Salt Lake City, UT', 226),
--(802, 'Vermont', 226),
--(803, 'Columbia, SC', 226),
--(804, 'Richmond, VA', 226),
--(805, 'Oxnard, CA', 226),
--(806, 'Lubbock, TX', 226),
--(807, 'Kenora, ON', 226),
--(808, 'Hawaii', 226),
--(810, 'Flint, MI', 226),
--(812, 'Evansville, IN', 226),
--(813, 'Tampa, FL', 226),
--(814, 'Erie, PA', 226),
--(815, 'Rockford, IL', 226),
--(816, 'Kansas City, MO', 226),
--(817, 'Fort Worth, TX', 226),
--(818, 'Los Angeles, CA', 226),
--(819, 'Sherbrooke, QC', 226),
--(825, 'Calgary, AB', 226),
--(828, 'Asheville, NC', 226),
--(866, 'New Braunfels, TX', 226),
--(831, 'Salinas, CA', 226),
--(832, 'Houston, TX', 226),
--(843, 'Charleston, SC', 226),
--(845, 'New City, NY', 226),
--(847, 'Elgin, IL', 226),
--(848, 'Toms River, NJ', 226),
--(850, 'Tallahassee, FL', 226),
--(854, 'Charleston, SC', 226),
--(856, 'Camden, NJ', 226),
--(857, 'Boston, MA', 226),
--(858, 'San Diego, CA', 226),
--(859, 'Lexington-Fayette, KY', 226),
--(860, 'Hartford, CT', 226),
--(862, 'Newark, NJ', 226),
--(863, 'Lakeland, FL', 226),
--(864, 'Greenville, SC', 226),
--(865, 'Knoxville, TN', 226),
--(867, 'Northern Canada', 226),
--(870, 'Jonesboro, AR', 226),
--(872, 'Chicago, IL', 226),
--(873, 'Sherbrooke, QC', 226),
--(878, 'Pittsburgh, PA', 226),
--(901, 'Memphis, TN', 226),
--(902, 'Nova Scotia/PE Island ', 226),
--(903, 'Tyler, TX', 226),
--(904, 'Jacksonville, FL', 226),
--(905, 'Hamilton, ON', 226),
--(906, 'Marquette, MI', 226),
--(907, 'Alaska', 226),
--(908, 'Elizabeth, NJ', 226),
--(909, 'San Bernardino, CA', 226),
--(910, 'Fayetteville, NC', 226),
--(912, 'Savannah, GA', 226),
--(913, 'Overland Park, KS', 226),
--(914, 'Yonkers, NY', 226),
--(915, 'El Paso, TX', 226),
--(916, 'Sacramento, CA', 226),
--(917, 'New York, NY', 226),
--(918, 'Tulsa, OK', 226),
--(919, 'Raleigh, NC', 226),
--(920, 'Green Bay, WI', 226),
--(925, 'Concord, CA', 226),
--(928, 'Yuma, AZ', 226),
--(929, 'New York, NY', 226),
--(966, 'Evansville, IN', 226),
--(931, 'Clarksville, TN', 226),
--(934, 'Brentwood, NY', 226),
--(936, 'Conroe, TX', 226),
--(937, 'Dayton, OH', 226),
--(938, 'Huntsville, AL', 226),
--(939, 'Puerto Rico', 226),
--(940, 'Denton, TX', 226),
--(941, 'North Port, FL', 226),
--(947, 'Troy, MI', 226),
--(949, 'Irvine, CA', 226),
--(951, 'Riverside, CA', 226),
--(952, 'Bloomington, MN', 226),
--(954, 'Fort Lauderdale, FL', 226),
--(956, 'Laredo, TX', 226),
--(959, 'Hartford, CT', 226),
--(970, 'Fort Collins, CO', 226),
--(971, 'Portland, OR', 226),
--(972, 'Dallas, TX', 226),
--(973, 'Newark, NJ', 226),
--(978, 'Lowell, MA', 226),
--(979, 'College Station, TX', 226),
--(980, 'Charlotte, NC', 226),
--(984, 'Raleigh, NC', 226),
--(985, 'Houma, LA', 226),
--(989, 'Saginaw, MI', 226),

--(250, 'Alert Bay', 38),
--(905, 'Brampton', 38), 
--(905, 'Burlington', 38),
--(403, 'Calgary', 38),
--(418, 'Chicoutimi', 38), 
--(613, 'Cornwall', 38),
--(250, 'Dawson Creek', 38),
--(403, 'Edmonton', 38),
--(905, 'Fort Erie', 38),
--(250, 'Fort Nelson', 38),
--(709, 'Grand Falls', 38),
--(902, 'Halifax', 38),
--(514, 'Joliette', 38),
--(907, 'Juneau', 38),
--(613, 'Kingston', 38),
--(519, 'Kitchener', 38),
--(705, 'Lindsay', 38),
--(519, 'London', 38),
--(905, 'Markham', 38),
--(705, 'Midland', 38),
--(905, 'Mississauga', 38), 
--(514, 'Montreal', 38),
--(250, 'Nelson', 38),
--(902, 'New Glasgow', 38), 
--(604, 'New Westminst', 38),
--(506, 'Newcastle', 38),
--(905, 'Newmarket', 38),
--(905, 'Niagara Falls', 38),
--(705, 'North Bay', 38),
--(416, 'North York', 38),
--(519, 'Orangeville', 38),
--(905, 'Oshawa', 38),
--(613, 'Ottawa', 38),
--(613, 'Pembroke', 38),
--(418, 'Quebec City', 38),
--(666, 'Regina', 38),
--(416, 'Toronto', 38),
--(819, 'Trois Rivier', 38), 
--(604, 'Vancouver', 38),
--(250, 'Victoria', 38),
--(250, 'Victoria Falls', 38),	 
--(604, 'Whistler', 38),
--(519, 'Windsor', 38),
--(204, 'Winnipeg', 38)
GO

UPDATE [LocalPhone].[dbo].[City]
     SET [status] = 1
	   , [creationDate] = GETDATE()
GO
