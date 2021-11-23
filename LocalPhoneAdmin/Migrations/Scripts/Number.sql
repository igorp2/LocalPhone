USE [LocalPhone]
GO
IF OBJECT_ID ('dbo.Number') IS NOT NULL
	DROP TABLE dbo.Number
GO
CREATE TABLE dbo.Number
	(
	[id]          INT IDENTITY NOT NULL,

	[idCustomer]  varchar(20) NOT NULL,
	[phoneNumber] [varchar](20) NOT NULL,

	[idPayment] INT NULL,

	[status] int NULL DEFAULT (((1))),
    [creationDate] datetime NULL,
    [creatorUser] varchar(450) NULL,
    [lastModificationDate] datetime NULL,
    [userThatMadeTheLastModification] varchar(450) NULL

	PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Number]  WITH CHECK ADD FOREIGN KEY([idCustomer])
REFERENCES [dbo].[Customer] ([phoneNumber])
GO
ALTER TABLE [dbo].[Number]  WITH CHECK ADD  CONSTRAINT [FK_NumberCustomer] FOREIGN KEY([idCustomer])
REFERENCES [dbo].[Customer] ([phoneNumber])
GO
ALTER TABLE [dbo].[Number] CHECK CONSTRAINT [FK_NumberCustomer]
GO

ALTER TABLE [dbo].[Number]  WITH CHECK ADD FOREIGN KEY([idPayment])
REFERENCES [dbo].[Payment] ([id])
GO
ALTER TABLE [dbo].[Number]  WITH CHECK ADD  CONSTRAINT [FK_NumberPayment] FOREIGN KEY([idPayment])
REFERENCES [dbo].[Payment] ([id])
GO
ALTER TABLE [dbo].[Number] CHECK CONSTRAINT [FK_NumberPayment]
GO

