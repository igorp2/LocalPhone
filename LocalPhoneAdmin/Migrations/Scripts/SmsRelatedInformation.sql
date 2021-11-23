CREATE TABLE [SmsRelatedInformation] (
    [Id] int NOT NULL IDENTITY,
    [TransactionId] char(68) NULL,
    [IdCustomer] varchar(20) NULL,
    [CreationDate] datetime NOT NULL DEFAULT (getdate()),
    CONSTRAINT [PK_SmsRelatedInformation] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SmsCustomer] FOREIGN KEY ([IdCustomer]) REFERENCES [Customer] ([phoneNumber]) ON DELETE NO ACTION
);
GO
