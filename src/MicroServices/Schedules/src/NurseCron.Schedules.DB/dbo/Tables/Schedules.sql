CREATE TABLE [Schedules](
	[Id] [uniqueidentifier] NOT NULL,
	[UnitId] [uniqueidentifier] NOT NULL,
	[UnitName] [nvarchar](250) NOT NULL,
	[EmployeeId] [uniqueidentifier] NOT NULL,
	[EmployeeName] [nvarchar](250) NOT NULL,
	[StaffingRequirementId] [uniqueidentifier] NOT NULL,
	[StartTimeUtc] [datetimeoffset](7) NOT NULL,
  [ScheduledHours] [decimal](18,2) NOT NULL,
  [ApprovedOnUtc] [datetimeoffset](7) NOT NULL,
  [CreatedBy] NVARCHAR (250) NOT NULL ,
  [UpdatedBy] NVARCHAR (250) NULL,
  [CreatedOnUtc] DATETIMEOFFSET NOT NULL DEFAULT GETUTCDATE(),
  [UpdatedOnUtc] DATETIMEOFFSET NULL,
 CONSTRAINT [PK_Schedules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

