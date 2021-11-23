USE [LocalPhone]
GO
IF OBJECT_ID ('dbo.Customer') IS NOT NULL
	DROP TABLE dbo.Customer
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[phoneNumber] [varchar](20) PRIMARY KEY,

	[idCountry] [int] NULL,
	[operationalSystem][varchar](100) NULL,
	[verificationCode] [int] NULL,
	[validationCodeDate] [datetime] NULL,
	[verificationCodeDate] [datetime] NULL,
	[publishedAppVersion] [int] NULL,

    [password][varchar](100) NULL,
	[email][varchar](200) NULL,
	[dateOfBirth] date NULL,
	[firstName] [nvarchar](100) NULL,
	[lastName] [nvarchar](100) NULL,	
	[idGender] [int] NULL,	
	[avatar] [varbinary](MAX) NULL,

	[idCustomerStatus] [int] NULL,

	[status][int] DEFAULT 1 NULL,
	[creationDate][datetime] NULL,
	[creatorUser][nvarchar](450) NULL,
	[lastModificationDate][datetime] NULL,
	[userThatMadeTheLastModification][nvarchar](450) NULL

)
GO

--PRIMARY KEY CLUSTERED 
--(
--	[id] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--) ON [PRIMARY]
--GO

ALTER TABLE [dbo].[Customer]  WITH CHECK ADD FOREIGN KEY([idGender])
REFERENCES [dbo].[Gender] ([id])
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_CustomerGender] FOREIGN KEY([idGender])
REFERENCES [dbo].[Gender] ([id])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_CustomerGender]
GO

ALTER TABLE [dbo].[Customer]  WITH CHECK ADD FOREIGN KEY([idCountry])
REFERENCES [dbo].[Country] ([id])
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCountry] FOREIGN KEY([idCountry])
REFERENCES [dbo].[Country] ([id])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_CustomerCountry]
GO

ALTER TABLE [dbo].[Customer]  WITH CHECK ADD FOREIGN KEY([idCustomerStatus])
REFERENCES [dbo].[CustomerStatus] ([id])
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCustomerStatus] FOREIGN KEY([idCustomerStatus])
REFERENCES [dbo].[CustomerStatus] ([id])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_CustomerCustomerStatus]
GO