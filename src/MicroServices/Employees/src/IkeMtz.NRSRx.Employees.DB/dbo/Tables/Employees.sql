CREATE TABLE [dbo].[Employees]
(
    [Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    [LastName] NVARCHAR (250) NOT NULL,
    [FirstName] NVARCHAR (250) NOT NULL,
    [BirthDate] DATE NULL,
    [MobilePhone] VARCHAR (10) NULL,
    [HomePhone] VARCHAR (10) NULL,
    [Photo] VARCHAR(4000) NULL,
    [Email] NVARCHAR (250) NOT NULL,
    [AddressLine1] VARCHAR (250) NULL,
    [AddressLine2] VARCHAR (250) NULL,
    [City] VARCHAR (150) NULL,
    [State] CHAR (2) NULL,
    [Zip] VARCHAR (10) NULL,
    [IsEnabled] BIT NOT NULL DEFAULT 1,
    [HireDate] DATE NOT NULL,
    [FireDate] DATE NULL,
    [TotalHoursOfService] DECIMAL(18,1) NULL,
    [CreatedBy] NVARCHAR (250) NOT NULL ,
    [UpdatedBy] NVARCHAR (250) NULL,
    [CreatedOnUtc] DATETIMEOFFSET NOT NULL DEFAULT GETUTCDATE(),
    [UpdatedOnUtc] DATETIMEOFFSET NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY ([Id])
);


GO

CREATE UNIQUE INDEX [UIX_Employees_Name] ON [dbo].[Employees] ([FirstName], [LastName])
GO
CREATE UNIQUE INDEX [UIX_Employees_Email] ON [dbo].[Employees] ([Email])