CREATE TABLE [dbo].[Certifications] (
    [Id]   UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    [Name] NVARCHAR (250)   NOT NULL,
	[IsEnabled] BIT			NOT NULL DEFAULT 1,
    [ExpiresOnUtc] DATETIME NULL,
	[CreatedBy] NVARCHAR (250)   NOT NULL ,
	[UpdatedBy] NVARCHAR (250)   NULL,
	[CreatedOnUtc] DATETIMEOFFSET   NOT NULL DEFAULT GETUTCDATE(),
	[UpdatedOnUtc] DATETIMEOFFSET   NULL, 
    CONSTRAINT [PK_Certifications] PRIMARY KEY ([Id])
);


GO

CREATE UNIQUE INDEX [UIX_Certifications_Name] ON [dbo].[Certifications] ([Name])
