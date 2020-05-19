CREATE TABLE [dbo].[Buildings](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[SiteName] [nvarchar](150) NULL,
	[AddressLine1] [nvarchar](250) NOT NULL,
	[AddressLine2] [nvarchar](250) NOT NULL,
	[CityOrMunicipality] [nvarchar](250) NOT NULL,
	[StateOrProvidence] [nvarchar](50) NOT NULL,
	[PostalCode] [nvarchar](50) NOT NULL,
	[Country] [char](3) NOT NULL,
	[GpsData] [geography] NULL,
	[CreatedBy] [nvarchar](250) NOT NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[DeletedBy] [nvarchar](250) NULL,
	[CreatedOnUtc] [datetimeoffset](7) NOT NULL,
	[UpdatedOnUtc] [datetimeoffset](7) NULL,
	[DeletedOnUtc] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_Buildings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Buildings] ADD  CONSTRAINT [DF_Buildings_CreatedOnUtc]  DEFAULT (getutcdate()) FOR [CreatedOnUtc]
GO
