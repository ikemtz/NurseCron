CREATE TABLE [dbo].[EmployeeCompetencies]
(
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [EmployeeId] UNIQUEIDENTIFIER NOT NULL,
    [CompetencyId] UNIQUEIDENTIFIER NOT NULL,
    [CompetencyName] NVARCHAR(250) NOT NULL,
    [ExpiresOnUtc] DATETIMEOFFSET NULL,
    [IsEnabled] BIT NOT NULL, 
)
GO

ALTER TABLE [dbo].[EmployeeCompetencies] ADD  CONSTRAINT [FK_EmployeeCompetencies_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([Id])
GO 
