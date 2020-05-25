CREATE TABLE [dbo].[StaffingRequirements] (
    [Id]            UNIQUEIDENTIFIER   NOT NULL,
    [UnitId]        UNIQUEIDENTIFIER   NOT NULL,
    [DayOfWeek]     TINYINT            NOT NULL,
    [StartTime]     TIME (7)           NOT NULL,
    [Hours]         DECIMAL (18)       NOT NULL,
    [StaffCount]    INT                NOT NULL,
    [CreatedBy]     NVARCHAR (250)     NOT NULL,
    [UpdatedBy]     NVARCHAR (250)     NULL,
    [DisabledBy]    NVARCHAR (250)     NULL,
    [CreatedOnUtc]  DATETIMEOFFSET (7) CONSTRAINT [DF_StaffingRequirements_CreatedOnUtc] DEFAULT (getutcdate()) NOT NULL,
    [UpdatedOnUtc]  DATETIMEOFFSET (7) NULL,
    [DisabledOnUtc] DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_StaffingRequirements] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StaffingRequirements_Units] FOREIGN KEY ([UnitId]) REFERENCES [dbo].[Units] ([Id])
);

