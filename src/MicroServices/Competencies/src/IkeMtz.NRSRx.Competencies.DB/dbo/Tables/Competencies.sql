CREATE TABLE [dbo].[Competencies] (
    [Id]   UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    [Name] NVARCHAR (250)   NOT NULL,
	[IsEnabled] BIT			NOT NULL DEFAULT 1,
	[CreatedBy] NVARCHAR (250)   NOT NULL ,
	[UpdatedBy] NVARCHAR (250)   NULL,
	[CreatedOnUtc] DATETIMEOFFSET   NOT NULL DEFAULT GETUTCDATE(),
	[UpdatedOnUtc] DATETIMEOFFSET   NULL, 
    CONSTRAINT [PK_Competencies] PRIMARY KEY ([Id])
);


GO

CREATE UNIQUE INDEX [UIX_Competencies_Name] ON [dbo].[Competencies] ([Name])
