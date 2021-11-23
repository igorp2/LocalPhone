USE [LocalPhone]
GO
IF OBJECT_ID ('dbo.Contact') IS NOT NULL
	DROP TABLE dbo.Contact
GO
CREATE TABLE dbo.Contact
	(
	[id]         INT IDENTITY NOT NULL,

    [idCustomer] varchar(20) NOT NULL,
	--[idContact]  INT NOT NULL,

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

ALTER TABLE [dbo].[Contact]  WITH CHECK ADD FOREIGN KEY([idCustomer])
REFERENCES [dbo].[Customer] ([phoneNumber])
GO
ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_ContactCustomer] FOREIGN KEY([idCustomer])
REFERENCES [dbo].[Customer] ([phoneNumber])
GO
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_ContactCustomer]
GO

--ALTER TABLE [dbo].[Contact]  WITH CHECK ADD FOREIGN KEY([idContact])
--REFERENCES [dbo].[Customer] ([id])
--GO
--ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_ContactContact] FOREIGN KEY([idContact])
--REFERENCES [dbo].[Customer] ([id])
--GO
--ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_ContactContact]
--GO