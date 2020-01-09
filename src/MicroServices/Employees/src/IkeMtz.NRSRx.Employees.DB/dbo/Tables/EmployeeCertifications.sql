CREATE TABLE [dbo].[EmployeeCertifications]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [EmployeeId] UNIQUEIDENTIFIER NOT NULL,
    [CertificationId] UNIQUEIDENTIFIER NOT NULL,
    [CertificationName] NVARCHAR(250) NOT NULL,
    [ExpiresOnUtc] DATETIMEOFFSET NULL,
)
GO

ALTER TABLE [dbo].[EmployeeCertifications] ADD  CONSTRAINT [FK_EmployeeCertifications_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[EmployeeCertifications] CHECK CONSTRAINT [FK_EmployeeCertifications_Employees]
GO

