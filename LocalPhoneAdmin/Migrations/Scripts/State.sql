USE [LocalPhone]
GO
IF OBJECT_ID ('dbo.State') IS NOT NULL
	DROP TABLE dbo.State
GO
CREATE TABLE [dbo].[State](
	[Id] [int] IDENTITY(1,1) NOT NULL,

	[Name] [varchar](250) NULL,
	[Abbreviation] [varchar](50) NULL,
	[idCountry] [int] NOT NULL,

	[status][int] DEFAULT 1 NULL,
	[creationDate][datetime] NULL,
	[creatorUser][nvarchar](450) NULL,
	[lastModificationDate][datetime] NULL,
	[userThatMadeTheLastModification][nvarchar](450) NULL

 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[State]  WITH CHECK ADD  CONSTRAINT [FK_StateCountry] FOREIGN KEY([idCountry])
REFERENCES [dbo].[Country] ([id])
GO

ALTER TABLE [dbo].[State] CHECK CONSTRAINT [FK_StateCountry]
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Alaska', 'AK', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Arizona', 'AZ', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Arkansas', 'AR', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('California', 'CA', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Colorado', 'CO', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Connecticut', 'CT', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Delaware', 'DE', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('District of Columbia', 'DC', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Florida', 'FL', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Georgia', 'GA', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Hawaii', 'HI', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Idaho', 'ID', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Illinois', 'IL', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Indiana', 'IN', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Iowa', 'IA', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Kansas', 'KS', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Kentucky', 'KY', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Louisiana', 'LA', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Maine', 'ME', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Maryland', 'MD', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Massachusetts', 'MA', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Michigan', 'MI', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Minnesota', 'MN', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Mississippi', 'MS', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Missouri', 'MO', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Montana', 'MT', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Nebraska', 'NE', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Nevada', 'NV', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('New Hampshire', 'NH', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('New Jersey', 'NJ', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('New Mexico', 'NM', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('New York', 'NY', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('North Carolina', 'NC', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('North Dakota', 'ND', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Ohio', 'OH', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Oklahoma', 'OK', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Oregon', 'OR', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Pennsylvania', 'PA', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Rhode Island', 'RI', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('South Carolina', 'SC', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('South Dakota', 'SD', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Tennessee', 'TN', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Texas', 'TX', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Utah', 'UT', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Vermont', 'VT', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Virginia', 'VA', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Washington', 'WA', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('West Virginia', 'WV', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Wisconsin', 'WI', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Wyoming', 'WY', 226)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Acre', 'AC', 30)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Alagoas', 'AL', 30)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Amapá', 'AP', 30)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Amazonas', 'AM', 30)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Bahia', 'BA', 30)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Ceará', 'CE', 30)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Espírito Santo', 'ES', 30)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Goiás', 'GO', 30)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Maranhão', 'MA', 30)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Mato Grosso', 'MT', 30)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Mato Grosso do Sul', 'MS', 30)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Minas Gerais', 'MG', 30)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Pará', 'PA', 30)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Paraíba', 'PB', 30)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Paraná', 'PR', 30)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Pernambuco', 'PE', 30)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Piauí', 'PI', 30)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Rio de Janeiro', 'RJ', 30)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Rio Grande do Norte', 'RN', 30)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Rio Grande do Sul', 'RS', 30)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Rondônia', 'RO', 30)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Roraima', 'RR', 30)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Santa Catarina', 'SC', 30)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('São Paulo', 'SP', 30)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Sergipe', 'SE', 30)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Tocantins', 'TO', 30)
GO

INSERT INTO dbo.State (Name, Abbreviation, idCountry)
VALUES ('Distrito Federal', 'DF', 30)
GO


  UPDATE [LocalPhone].[dbo].[State]
     SET status = 1


