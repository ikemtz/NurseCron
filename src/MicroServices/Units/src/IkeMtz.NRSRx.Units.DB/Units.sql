CREATE TABLE [Units](
	[Id] [uniqueidentifier] NOT NULL,
	[BuildingId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[RoomCount] [decimal](4, 2) NOT NULL,
	[CreatedBy] [nvarchar](250) NOT NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[DeletedBy] [nvarchar](250) NULL,
	[CreatedOnUtc] [datetimeoffset](7) NOT NULL,
	[UpdatedOnUtc] [datetimeoffset](7) NULL,
	[DeletedOnUtc] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_Units] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Units] ADD  CONSTRAINT [DF_Units_CreatedOnUtc]  DEFAULT (getutcdate()) FOR [CreatedOnUtc]
GO

ALTER TABLE [Units] ADD  CONSTRAINT [FK_Units_Buildings] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([Id])
GO

ALTER TABLE [dbo].[Units] CHECK CONSTRAINT [FK_Units_Buildings]
GO

