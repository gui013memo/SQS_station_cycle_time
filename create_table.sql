USE [SQS_SCT]
GO

/****** Object:  Table [dbo].[RESULTS]    Script Date: 4/17/2024 9:43:58 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RESULTS](
	[STATION] [varchar](10) NULL,
	[SCREEN] [varchar](10) NULL,
	[PRODUCT_ID] [varchar](20) NULL,
	[ELAPSED_TIME] [time](3) NULL,
	[ET_MAX] [time](3) NULL,
	[ET_MIN] [time](3) NULL,
	[RESULT] [varchar](10) NULL,
	[INSERT_DATE_TIME] DATETIME DEFAULT GETDATE() NULL
) ON [PRIMARY]
GO


