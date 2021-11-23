USE [LocalPhone]
GO
IF OBJECT_ID ('dbo.Message') IS NOT NULL
	DROP TABLE dbo.Message
GO
CREATE TABLE dbo.Message
	(
	[id]                  int IDENTITY NOT NULL,

	[idCustomerSending]   varchar(20) NOT NULL,
	[idCustomerReceiving] varchar(20) NOT NULL,
	[Date] datetime NOT NULL,
	[Text] varchar(max) NULL,

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

ALTER TABLE [dbo].[Message]  WITH CHECK ADD FOREIGN KEY([idCustomerSending])
REFERENCES [dbo].[Customer] ([phoneNumber])
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [FK_MessageCustomerSending] FOREIGN KEY([idCustomerSending])
REFERENCES [dbo].[Customer] ([phoneNumber])
GO
ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK_MessageCustomerSending]
GO

ALTER TABLE [dbo].[Message]  WITH CHECK ADD FOREIGN KEY([idCustomerReceiving])
REFERENCES [dbo].[Customer] ([phoneNumber])
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [FK_MessageCustomerReceiving] FOREIGN KEY([idCustomerReceiving])
REFERENCES [dbo].[Customer] ([phoneNumber])
GO
ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK_MessageCustomerReceiving]
GO

