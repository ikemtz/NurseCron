CREATE TABLE [dbo].[EmployeeHealthItems]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [EmployeeId] UNIQUEIDENTIFIER NOT NULL,
    [HealthItemId] UNIQUEIDENTIFIER NOT NULL,
    [HealthItemName] NVARCHAR(250) NOT NULL,
    [ExpiresOnUtc] DATETIMEOFFSET NULL,
)
GO

ALTER TABLE [dbo].[EmployeeHealthItems] ADD  CONSTRAINT [FK_EmployeeHealthItems_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([Id])
GO

