USE [LocalPhone]
GO
IF OBJECT_ID ('dbo.Address') IS NOT NULL
	DROP TABLE dbo.Address
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
    [idCustomer] varchar(20) NULL,

	[status][int] DEFAULT 1 NULL,
	[creationDate][datetime] NULL,
	[creatorUser][nvarchar](450) NULL,
	[lastModificationDate][datetime] NULL,
	[userThatMadeTheLastModification][nvarchar](450) NULL
	
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Address]  WITH CHECK ADD FOREIGN KEY([idCity])
REFERENCES [dbo].[City] ([id])
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_AddressCity] FOREIGN KEY([idCity])
REFERENCES [dbo].[City] ([id])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_AddressCity]
GO

ALTER TABLE [dbo].[Address]  WITH CHECK ADD FOREIGN KEY([idState])
REFERENCES [dbo].[State] ([id])
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_AddressState] FOREIGN KEY([idState])
REFERENCES [dbo].[State] ([id])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_AddressState]
GO

ALTER TABLE [dbo].[Address]  WITH CHECK ADD FOREIGN KEY([idCountry])
REFERENCES [dbo].[Country] ([id])
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_AddressCountry] FOREIGN KEY([idCountry])
REFERENCES [dbo].[Country] ([id])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_AddressCountry]
GO

ALTER TABLE [dbo].[Address]  WITH CHECK ADD FOREIGN KEY([idCustomer])
REFERENCES [dbo].[Customer] ([phoneNumber])
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_AddressCustomer] FOREIGN KEY([idCustomer])
REFERENCES [dbo].[Customer] ([phoneNumber])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_AddressCustomer]
