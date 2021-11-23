USE [LocalPhone]
GO
IF OBJECT_ID ('dbo.Gender') IS NOT NULL
	DROP TABLE dbo.Gender
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gender](
	[id] [int] IDENTITY(1,1) NOT NULL,

	[Gender] [varchar](250) NOT NULL,
	[Abbreviation] [varchar](50) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[idCountry] [int] NULL,

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

ALTER TABLE [dbo].[Gender]  WITH CHECK ADD FOREIGN KEY([idCountry])
REFERENCES [dbo].[Country] ([id])
GO
ALTER TABLE [dbo].[Gender]  WITH CHECK ADD  CONSTRAINT [FK_GenderCountry] FOREIGN KEY([idCountry])
REFERENCES [dbo].[Country] ([id])
GO
ALTER TABLE [dbo].[Gender] CHECK CONSTRAINT [FK_GenderCountry]
GO
INSERT INTO [dbo].[Gender]
           ([Gender]
           ,[Abbreviation]
           ,[Description]
           ,[idCountry]
           ,[status]
           ,[creationDate]
           ,[creatorUser]
           ,[lastModificationDate]
           ,[userThatMadeTheLastModification])
     VALUES
           ('Masculine'
           ,'M'
           ,'Gender Masculine'
           ,30
           ,1
           ,getdate()
           ,'Marcos'
           ,''
           ,'')
GO

INSERT INTO [dbo].[Gender]
           ([Gender]
           ,[Abbreviation]
           ,[Description]
           ,[idCountry]
           ,[status]
           ,[creationDate]
           ,[creatorUser]
           ,[lastModificationDate]
           ,[userThatMadeTheLastModification])
     VALUES
           ('Feminine'
           ,'F'
           ,'Gender Feminine'
           ,30
           ,1
           ,getdate()
           ,'Marcos'
           ,''
           ,'')
GO