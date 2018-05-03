SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User Mobile](
	[timestamp] [timestamp] NOT NULL,
	[User Security ID] [uniqueidentifier] NOT NULL,
	[User Name] [nvarchar](50) NOT NULL,
	[Full Name] [nvarchar](80) NOT NULL,
	[State] [int] NOT NULL,
	[Expiry Date] [datetime] NOT NULL,
	[Windows Security ID] [nvarchar](119) NOT NULL,
	[Change Password] [tinyint] NOT NULL,
	[License Type] [int] NOT NULL,
	[Authentication Email] [nvarchar](250) NOT NULL,
	[Contact Email] [nvarchar](250) NOT NULL,
	[Exchange Identifier] [nvarchar](250) NOT NULL,
	[Application ID] [uniqueidentifier] NOT NULL,
	[Password] [nvarchar](250) NOT NULL,
 CONSTRAINT [UserMobile$0] PRIMARY KEY CLUSTERED 
(
	[User Security ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


