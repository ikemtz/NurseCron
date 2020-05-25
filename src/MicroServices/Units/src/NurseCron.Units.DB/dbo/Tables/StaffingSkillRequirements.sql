CREATE TABLE [dbo].[StaffingSkillRequirements] (
    [Id]                    UNIQUEIDENTIFIER   NOT NULL,
    [StaffingRequirementId] UNIQUEIDENTIFIER   NOT NULL,
    [SkillId]               UNIQUEIDENTIFIER   NOT NULL,
    [SkillName]             NVARCHAR (250)     NOT NULL,
    [SkillTypeId]           TINYINT            NOT NULL,
    [CreatedBy]             NVARCHAR (250)     NOT NULL,
    [UpdatedBy]             NVARCHAR (250)     NULL,
    [DisabledBy]            NVARCHAR (250)     NULL,
    [CreatedOnUtc]          DATETIMEOFFSET (7) CONSTRAINT [DF_StaffingSkillRequirements_CreatedOnUtc] DEFAULT (getutcdate()) NOT NULL,
    [UpdatedOnUtc]          DATETIMEOFFSET (7) NULL,
    [DisabledOnUtc]         DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_StaffingSkillRequirements] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StaffingSkillRequirements_SkillTypes] FOREIGN KEY ([SkillTypeId]) REFERENCES [dbo].[SkillTypes] ([Id]),
    CONSTRAINT [FK_StaffingSkillRequirements_StaffingRequirements] FOREIGN KEY ([StaffingRequirementId]) REFERENCES [dbo].[StaffingRequirements] ([Id])
);



