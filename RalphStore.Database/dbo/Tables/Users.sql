CREATE TABLE [dbo].[Users] (
    [FirstName] NVARCHAR (100) NULL,
    [LastName]  NVARCHAR (100) NULL,
    [AddressId] INT            NOT NULL,
    [Birthday]  INT            NULL,
    [Id]        INT            NOT NULL,
    [Email]     NVARCHAR (100) NULL,
    [PolicyId]  INT            NOT NULL,
    CONSTRAINT [Pk_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);

