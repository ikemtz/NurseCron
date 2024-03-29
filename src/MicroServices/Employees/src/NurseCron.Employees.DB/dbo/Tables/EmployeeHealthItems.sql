CREATE TABLE [dbo].[EmployeeHealthItems]
(
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [EmployeeId] UNIQUEIDENTIFIER NOT NULL,
    [HealthItemId] UNIQUEIDENTIFIER NOT NULL,
    [HealthItemName] NVARCHAR(250) NOT NULL,
    [ExpiresOnUtc] DATETIMEOFFSET NULL,
    [IsEnabled] BIT NOT NULL, 
)
GO

ALTER TABLE [dbo].[EmployeeHealthItems] ADD  CONSTRAINT [FK_EmployeeHealthItems_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([Id])
GO

