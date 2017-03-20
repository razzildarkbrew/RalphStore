CREATE TABLE [dbo].[Policies] (
    [Id]            INT NOT NULL,
    [Number]        INT NULL,
    [EffectiveDate] INT NULL,
    [AddressId]     INT NOT NULL,
    CONSTRAINT [Pk_Policies] PRIMARY KEY CLUSTERED ([Id] ASC)
);

