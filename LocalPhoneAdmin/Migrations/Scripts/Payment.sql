USE [LocalPhone]
GO
IF OBJECT_ID ('dbo.Payment') IS NOT NULL
	DROP TABLE dbo.Payment
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[id] [int] IDENTITY(1,1) NOT NULL,	

	[MethodTypes] [nvarchar](100) NULL,
	[CardNumber] [nvarchar](16) NULL,
	[ExpirationMonth] [int] NULL,
	[ExpirationYear] [int] NULL,
	[CVC] [int]  NULL,
	[OrderAmount] [int] NULL,
	[ClientSecret] [nvarchar](100) NULL,
	[PaymentIntentId] [nvarchar](100) NULL,

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

ALTER TABLE [dbo].[Payment]  WITH CHECK ADD FOREIGN KEY([idCustomer])
REFERENCES [dbo].[Customer] ([phoneNumber])
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_PaymentCustomer] FOREIGN KEY([idCustomer])
REFERENCES [dbo].[Customer] ([phoneNumber])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_PaymentCustomer]