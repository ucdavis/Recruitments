USE [Recruitment]
GO
/****** Object:  Table [dbo].[Ethnicity]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ethnicity](
	[EthnicityID] [int] IDENTITY(1,1) NOT NULL,
	[Ethnicity] [nvarchar](100) NOT NULL,
	[Category] [nvarchar](100) NULL,
 CONSTRAINT [PK_Ethnicity] PRIMARY KEY CLUSTERED 
(
	[EthnicityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PosXStep]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PosXStep](
	[PositionID] [int] NOT NULL,
	[StepID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MessageTracking]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MessageTracking](
	[MessageTrackingID] [int] IDENTITY(1,1) NOT NULL,
	[ToAddress] [varchar](50) NOT NULL,
	[FromAddress] [varchar](50) NOT NULL,
	[SentBy] [varchar](50) NOT NULL,
	[DateSent] [datetime] NOT NULL,
	[Body] [nvarchar](max) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MemberTypes]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MemberTypes](
	[MemberTypeID] [int] IDENTITY(1,1) NOT NULL,
	[MemberType] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MemberTypes] PRIMARY KEY CLUSTERED 
(
	[MemberTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Gender]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gender](
	[GenderID] [int] IDENTITY(1,1) NOT NULL,
	[Gender] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Gender] PRIMARY KEY CLUSTERED 
(
	[GenderID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FileTypes]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileTypes](
	[FileTypeID] [int] IDENTITY(1,1) NOT NULL,
	[FileType] [nvarchar](50) NOT NULL,
	[ApplicationFile] [bit] NOT NULL,
 CONSTRAINT [PK_FileTypes] PRIMARY KEY CLUSTERED 
(
	[FileTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeptXTheme]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DeptXTheme](
	[DepartmentFIS] [char](4) NOT NULL,
	[ThemeName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_DeptXTheme] PRIMARY KEY CLUSTERED 
(
	[DepartmentFIS] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DepartmentMembers]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DepartmentMembers](
	[DepartmentMemberID] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentFIS] [char](4) NOT NULL,
	[OtherDepartmentName] [nvarchar](100) NULL,
	[LoginID] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Inactive] [bit] NULL,
 CONSTRAINT [PK_DepartmentMembers] PRIMARY KEY CLUSTERED 
(
	[DepartmentMemberID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ChangeTypes]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ChangeTypes](
	[ChangeTypeID] [int] IDENTITY(1,1) NOT NULL,
	[ChangeType] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ChangeTypes] PRIMARY KEY CLUSTERED 
(
	[ChangeTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[AccountID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[PasswordFormat] [int] NOT NULL,
	[PasswordSalt] [nvarchar](128) NOT NULL,
	[PasswordQuestion] [nvarchar](256) NULL,
	[PasswordAnswer] [nvarchar](128) NULL,
	[CreatedBy] [int] NULL,
	[isActive] [bit] NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UniqueEmail] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApplicationReferSources]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationReferSources](
	[ApplicationID] [int] NOT NULL,
	[ReferSourceID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sysdiagrams]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sysdiagrams](
	[name] [nvarchar](128) NOT NULL,
	[principal_id] [int] NOT NULL,
	[diagram_id] [int] NOT NULL,
	[version] [int] NULL,
	[definition] [varbinary](max) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProfileFiles]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfileFiles](
	[ProfileFileID] [int] NOT NULL,
	[ProfileID] [int] NOT NULL,
	[FileID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecruitmentSrc]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecruitmentSrc](
	[RecruitmentSrcID] [int] IDENTITY(1,1) NOT NULL,
	[RecruitmentSrc] [nvarchar](50) NOT NULL,
	[AllowSpecify] [bit] NOT NULL,
 CONSTRAINT [PK_RecruitmentSrc] PRIMARY KEY CLUSTERED 
(
	[RecruitmentSrcID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrackingType]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrackingType](
	[TrackingTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TrackingType] [nchar](10) NOT NULL,
 CONSTRAINT [PK_TrackingType] PRIMARY KEY CLUSTERED 
(
	[TrackingTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Themes]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Themes](
	[ThemeID] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentFIS] [char](4) NOT NULL,
	[ThemeName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Themes] PRIMARY KEY CLUSTERED 
(
	[ThemeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TemplateTypes]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TemplateTypes](
	[TemplateTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TemplateType] [varchar](50) NOT NULL,
	[IsEmailTemplate] [bit] NOT NULL,
 CONSTRAINT [PK_TemplateTypes] PRIMARY KEY CLUSTERED 
(
	[TemplateTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Templates]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Templates](
	[TemplateID] [int] IDENTITY(1,1) NOT NULL,
	[TemplateText] [text] NOT NULL,
	[TemplateTypeID] [int] NOT NULL,
 CONSTRAINT [PK_Templates] PRIMARY KEY CLUSTERED 
(
	[TemplateID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Profiles]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profiles](
	[ProfileID] [int] IDENTITY(1,1) NOT NULL,
	[AccountID] [int] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Address1] [nvarchar](100) NOT NULL,
	[Address2] [nvarchar](100) NULL,
	[City] [nvarchar](50) NOT NULL,
	[State] [nvarchar](50) NOT NULL,
	[Zip] [nvarchar](20) NULL,
	[Country] [nvarchar](50) NULL,
	[CountryCode] [nvarchar](5) NULL,
	[Phone] [nvarchar](20) NULL,
	[LastUpdated] [datetime] NULL,
 CONSTRAINT [PK_Profiles] PRIMARY KEY CLUSTERED 
(
	[ProfileID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SurveyXRecruitmentSrc]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SurveyXRecruitmentSrc](
	[SurveyXRecruitmentSrcID] [int] IDENTITY(1,1) NOT NULL,
	[SurveyID] [int] NOT NULL,
	[RecruitmentSrcID] [int] NOT NULL,
	[RecruitmentSrcOther] [nvarchar](50) NULL,
 CONSTRAINT [PK_SurveyXRecruitmentSrc] PRIMARY KEY CLUSTERED 
(
	[SurveyXRecruitmentSrcID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[usp_getDepartments]    Script Date: 01/31/2011 10:46:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[usp_getDepartments]

AS 

	SET NOCOUNT ON;
	
	SELECT     FullName, FIS_Code
	FROM         CATBERT.dbo.Unit
GO
/****** Object:  StoredProcedure [dbo].[usp_getPasswordByUser]    Script Date: 01/31/2011 10:46:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[usp_getPasswordByUser]
	@Email nvarchar(50)
	
AS

SELECT     PasswordFormat, Password, PasswordSalt, isActive
FROM         Accounts
WHERE     (Email = @Email)
GO
/****** Object:  StoredProcedure [dbo].[usp_getUserInfoByEmail]    Script Date: 01/31/2011 10:46:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[usp_getUserInfoByEmail]
	@Email nvarchar(50)

AS

	SELECT     Email, PasswordQuestion, isActive
	FROM         Accounts
	WHERE Email = @Email
GO
/****** Object:  StoredProcedure [dbo].[usp_InsertBlankApplication]    Script Date: 01/31/2011 10:46:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

	Author : Alan Lai
	Date : 12/6/2006
	Description : Creates an application in the db with just hte position id and the profile id
		- it does not fill any information at this point.
		- if the application with this profile and position id have been started then it returns the application id

*/

CREATE Procedure [dbo].[usp_InsertBlankApplication]

	(
		@ProfileID int,
		@PositionID int,
		@ApplicationID int output,
		@Complete bit output
	)


AS

	-- Checks if the application has been started using this profile ID and position ID
	IF EXISTS ( SELECT * FROM Applications WHERE PositionID = @PositionID AND ApplicationID = @ApplicationID )
		BEGIN
			SET @ApplicationID = ( SELECT ApplicationID FROM Applications WHERE PositionID = @PositionID AND ProfileID = @ProfileID )
			SET @Complete = ( SELECT Submitted FROM Applications WHERE PositionID = @PositionID AND ProfileID = @ProfileID )
			RETURN 222
		END



	INSERT INTO Applications ( PositionID, ProfileID )
	VALUES ( @PositionID, @ProfileID )
	
	SET @ApplicationID = ( SELECT ApplicationID FROM Applications WHERE PositionID = @PositionID AND ProfileID = @ProfileID )

	SET @Complete = 0

	-- Insert has occured now we need to enter the tracking information

	DECLARE @Create int
	SET @Create = ( SELECT TrackingTypeID FROM TrackingType WHERE TrackingType = 'Create' )
	
	INSERT INTO ApplicationTracking ( ApplicationID, TrackingTypeID )
	VALUES ( @ApplicationID, @Create)
GO
/****** Object:  View [dbo].[vUserUnit]    Script Date: 01/31/2011 10:46:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vUserUnit]
AS
SELECT     CATBERT.dbo.UserUnit.UnitID, CATBERT.dbo.UserUnit.UserID, CATBERT.dbo.Unit.FIS_Code
FROM         CATBERT.dbo.UserUnit INNER JOIN
                      CATBERT.dbo.Unit ON CATBERT.dbo.UserUnit.UnitID = CATBERT.dbo.Unit.UnitID
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "UserUnit (CATBERT.dbo)"
            Begin Extent = 
               Top = 31
               Left = 69
               Bottom = 116
               Right = 221
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Unit (CATBERT.dbo)"
            Begin Extent = 
               Top = 6
               Left = 259
               Bottom = 268
               Right = 411
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vUserUnit'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vUserUnit'
GO
/****** Object:  View [dbo].[vUsers]    Script Date: 01/31/2011 10:46:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vUsers]
AS
SELECT     UserID, FirstName, LastName, EmployeeID, StudentID, UserImage, SID, Inactive, UserKey
FROM         CATBERT.dbo.Users AS Users
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Users"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 229
               Right = 190
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vUsers'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vUsers'
GO
/****** Object:  View [dbo].[vUnit]    Script Date: 01/31/2011 10:46:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vUnit]
AS
SELECT     UnitID, FullName, ShortName, PPS_Code, FIS_Code, SchoolCode
FROM         CATBERT.dbo.Unit
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Unit (CATBERT.dbo)"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 196
               Right = 190
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vUnit'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vUnit'
GO
/****** Object:  View [dbo].[vLogin]    Script Date: 01/31/2011 10:46:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vLogin]
AS
SELECT     LoginID, UserID
FROM         CATBERT.dbo.Login AS Login
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Login"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 91
               Right = 190
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vLogin'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vLogin'
GO
/****** Object:  StoredProcedure [dbo].[usp_NotifyApplicants]    Script Date: 01/31/2011 10:46:02 ******/
CREATE PROCEDURE [dbo].[usp_NotifyApplicants]
AS
EXTERNAL NAME [Recruitment.DB].[CAESDO.Recruitment.StoredProcedures].[usp_NotifyApplicants]
GO
EXEC sys.sp_addextendedproperty @name=N'AutoDeployed', @value=N'yes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'usp_NotifyApplicants'
GO
EXEC sys.sp_addextendedproperty @name=N'SqlAssemblyFile', @value=N'usp_NotifyApplicants.cs' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'usp_NotifyApplicants'
GO
EXEC sys.sp_addextendedproperty @name=N'SqlAssemblyFileLine', @value=15 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'usp_NotifyApplicants'
GO
/****** Object:  Table [dbo].[AccountTracking]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountTracking](
	[AccountTrackingID] [int] IDENTITY(1,1) NOT NULL,
	[AccountID] [int] NOT NULL,
	[TrackingTypeID] [int] NOT NULL,
	[ActionDate] [datetime] NOT NULL,
	[Comments] [nvarchar](50) NULL,
 CONSTRAINT [PK_AccountTracking] PRIMARY KEY CLUSTERED 
(
	[AccountTrackingID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChangeTracking]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ChangeTracking](
	[TrackingID] [int] IDENTITY(1,1) NOT NULL,
	[TrackingGroupID] [uniqueidentifier] NOT NULL,
	[ObjectChanged] [varchar](50) NOT NULL,
	[ObjectChangedID] [varchar](50) NULL,
	[UserName] [varchar](50) NULL,
	[ChangeDate] [datetime] NOT NULL,
	[ChangeTypeID] [int] NOT NULL,
 CONSTRAINT [PK_ChangeTracking] PRIMARY KEY CLUSTERED 
(
	[TrackingID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Files]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Files](
	[FileID] [int] IDENTITY(1,1) NOT NULL,
	[FileTypeID] [int] NOT NULL,
	[FileName] [nvarchar](100) NOT NULL,
	[Label] [nvarchar](100) NULL,
	[Description] [text] NULL,
 CONSTRAINT [PK_Files] PRIMARY KEY CLUSTERED 
(
	[FileID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Positions]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Positions](
	[PositionID] [int] IDENTITY(1,1) NOT NULL,
	[PositionTitle] [nvarchar](100) NOT NULL,
	[PositionNumber] [nvarchar](20) NULL,
	[ShortDescription] [text] NULL,
	[DescriptionFile] [int] NOT NULL,
	[SearchPlanFile] [int] NULL,
	[FinalRecruitmentReportFile] [int] NULL,
	[DatePosted] [datetime] NOT NULL,
	[Deadline] [datetime] NOT NULL,
	[AllowApps] [bit] NOT NULL,
	[NumReferences] [int] NOT NULL,
	[NumPublications] [int] NOT NULL,
	[HR_Rep] [nvarchar](100) NOT NULL,
	[HR_Phone] [nvarchar](13) NULL,
	[HR_Email] [nvarchar](100) NOT NULL,
	[CommitteeView] [bit] NOT NULL,
	[FacultyView] [bit] NOT NULL,
	[Vote] [bit] NOT NULL,
	[FinalVote] [bit] NOT NULL,
	[Closed] [bit] NOT NULL,
	[AdminAccepted] [bit] NOT NULL,
	[TemplateID] [int] NOT NULL,
	[PrimaryDepartmentFIS] [char](4) NULL,
 CONSTRAINT [PK_Positions] PRIMARY KEY CLUSTERED 
(
	[PositionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ChangedProperties]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ChangedProperties](
	[ChangedPropertyID] [int] IDENTITY(1,1) NOT NULL,
	[TrackingID] [int] NOT NULL,
	[PropertyChanged] [varchar](50) NOT NULL,
	[PropertyChangedValue] [nvarchar](100) NULL,
 CONSTRAINT [PK_ChangedProperties] PRIMARY KEY CLUSTERED 
(
	[ChangedPropertyID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[usp_ResetPassword]    Script Date: 01/31/2011 10:46:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[usp_ResetPassword]
	
	@Email nvarchar(50),
	@NewPassword nvarchar(128),
	@PasswordSalt nvarchar(128),
	@PasswordFormat int,
	@PasswordAnswer nvarchar(128)
	
AS

-- Pull out the correct answer
DECLARE @Answer nvarchar(128)
SET @Answer = NULL
SET @Answer = ( SELECT PasswordAnswer FROM Accounts WHERE  Email = @Email )

-- Check to see if the supplied answer is the same as the correct answer
IF @Answer <> @PasswordAnswer
	RETURN -1
	
-- If the answer is correct, reset the password
UPDATE    Accounts
SET              Password = @NewPassword, PasswordFormat = @PasswordFormat, PasswordSalt = @PasswordSalt
WHERE     (Email = @Email)

-- Add the Correct tracking information ("RESET") to the database
DECLARE @Reset int
SET @Reset = ( SELECT TrackingTypeID FROM TrackingType WHERE TrackingType = 'Reset')

-- Grab the accountID
DECLARE @AccountID int
SET @AccountID = ( SELECT AccountID FROM Accounts WHERE  Email = @Email )

INSERT INTO AccountTracking
	(AccountID, TrackingTypeID, ActionDate)
VALUES (@AccountID,@Reset, GETDATE())
GO
/****** Object:  StoredProcedure [dbo].[usp_InsertAccount]    Script Date: 01/31/2011 10:46:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[usp_InsertAccount]

	@LoginID nvarchar(255),
	@Email nvarchar(50),
	@Password nvarchar(128),
	@PasswordFormat int,
	@PasswordSalt nvarchar(128),
	@PasswordQuestion nvarchar(256),
	@PasswordAnswer nvarchar(128),
	@CreateStatus varchar(16) output

AS

-- Grab the CreatedBy user out of CATBERT: If there is no match, CreatedBy is NULL
DECLARE @CreatedByUserID int
SET @CreatedByUserID = NULL
SET @CreatedByUserID = ( SELECT UserID FROM CATBERT.dbo.Login WHERE LoginID = @LoginID)

-- Make sure the Email is not already in use
DECLARE @EmailCount int
SET @EmailCount = ( SELECT COUNT(Email) FROM  Accounts WHERE Email = @Email )

IF @EmailCount <> 0
BEGIN
	SET @CreateStatus = 'InvalidEmail'
	RETURN -1
END	

-- Now we know we have a unique email, so create the account
INSERT INTO Accounts
                      (Email, Password, PasswordFormat, PasswordSalt, PasswordQuestion, PasswordAnswer, CreatedBy, isActive)
VALUES     (@Email,@Password,@PasswordFormat,@PasswordSalt,@PasswordQuestion,@PasswordAnswer,@CreatedByUserID, 1)

-- Grab the AccountID for the newly created account
DECLARE @AccountID int
SELECT @AccountID = SCOPE_IDENTITY()

-- Get the tracking type for Created Accounts
DECLARE @Create int
SET @Create = ( SELECT TrackingTypeID FROM TrackingType WHERE TrackingType = 'Create')

-- Insert into the account tracking table
INSERT INTO AccountTracking
	(AccountID, TrackingTypeID, ActionDate)
VALUES (@AccountID,@Create, GETDATE())
GO
/****** Object:  StoredProcedure [dbo].[usp_ChangePassword]    Script Date: 01/31/2011 10:46:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[usp_ChangePassword]

	@Email nvarchar(50),
	@NewPassword nvarchar(128),
	@PasswordSalt nvarchar(128),
	@PasswordFormat int


AS

DECLARE @AccountID int
SET @AccountID = NULL
SET @AccountID = (SELECT AccountID FROM Accounts WHERE  Email = @Email)

IF @AccountID IS NULL
	RETURN -1
	
-- Now we know we have an active account, so update it
UPDATE    Accounts
SET              Password = @NewPassword, PasswordFormat = @PasswordFormat, PasswordSalt = @PasswordSalt
WHERE     (AccountID = @AccountID)

-- Add the Correct tracking information ("MODIFY") to the database
DECLARE @Modify int
SET @Modify = ( SELECT TrackingTypeID FROM TrackingType WHERE TrackingType = 'Modify')

INSERT INTO AccountTracking
	(AccountID, TrackingTypeID, ActionDate)
VALUES (@AccountID,@Modify, GETDATE())
GO
/****** Object:  StoredProcedure [dbo].[usp_GetActivePositions]    Script Date: 01/31/2011 10:46:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

	Author : Alan Lai
	Date : 12/1/2006
	Description : Lists all of the active positions (for applicatns).
		- Only shows positions that have not expired and allow applications.

	-- 1/9/06 SRK:  Update to include new Fields, and link with the catbert database to pull out department names
*/

CREATE Procedure [dbo].[usp_GetActivePositions]
/*
	(

	)
*/

AS

	SELECT     PositionID, PositionTitle, PositionNumber, ShortDescription, DescriptionFile, DepartmentFIS, Unit.ShortName, DatePosted, Deadline, AllowApps, NumReferences, 
	                      NumPublications, HR_Rep, HR_AreaCode, HR_Phone, HR_Email, CommitteeView, FacultyView, Vote, FinalVote
	FROM         Positions LEFT JOIN Catbert.dbo.Unit AS Unit ON Positions.DepartmentFIS = Unit.FIS_Code
	WHERE     (Deadline >= CONVERT(datetime, CONVERT(int, GETDATE()-1))) AND (AllowApps = 1)
GO
/****** Object:  Table [dbo].[PosXSteps]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PosXSteps](
	[PositionID] [int] NOT NULL,
	[StepID] [int] NOT NULL,
 CONSTRAINT [PK_PosXSteps] PRIMARY KEY CLUSTERED 
(
	[PositionID] ASC,
	[StepID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetPositionDetails]    Script Date: 01/31/2011 10:46:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[usp_GetPositionDetails]

	(
		@PositionID int
	)


AS

	SELECT     PositionID, PositionTitle, PositionNumber, ShortDescription, DescriptionFile, DepartmentFIS, Unit.FullName AS DepartmentFullName, DatePosted, Deadline, AllowApps, NumReferences, 
	                      NumPublications, HR_Rep, HR_AreaCode, HR_Phone, HR_Email, CommitteeView, FacultyView, Vote, FinalVote
	FROM         Positions LEFT JOIN Catbert.dbo.Unit AS Unit ON Positions.DepartmentFIS = Unit.FIS_Code
	WHERE     (PositionID = @PositionID)
GO
/****** Object:  Table [dbo].[Applications]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Applications](
	[ApplicationID] [int] IDENTITY(1,1) NOT NULL,
	[PositionID] [int] NOT NULL,
	[ProfileID] [int] NOT NULL,
	[PublicationsComplete] [bit] NOT NULL,
	[ReferencesComplete] [bit] NOT NULL,
	[CoverLetterComplete] [bit] NOT NULL,
	[ShortList] [bit] NOT NULL,
	[NoConsideration] [bit] NOT NULL,
	[GetReferences] [bit] NOT NULL,
	[Submitted] [bit] NOT NULL,
	[SubmitDate] [datetime] NULL,
	[LastUpdated] [datetime] NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED 
(
	[ApplicationID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PositionCommittee]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PositionCommittee](
	[PositionCommitteeID] [int] IDENTITY(1,1) NOT NULL,
	[PositionID] [int] NOT NULL,
	[DepartmentMemberID] [int] NOT NULL,
	[MemberTypeID] [int] NOT NULL,
 CONSTRAINT [PK_PositionCommittee] PRIMARY KEY CLUSTERED 
(
	[PositionCommitteeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PosXFileTypes]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PosXFileTypes](
	[PositionID] [int] NOT NULL,
	[FileTypeID] [int] NOT NULL,
 CONSTRAINT [PK_PosXFileTypes] PRIMARY KEY CLUSTERED 
(
	[PositionID] ASC,
	[FileTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PosXDept]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PosXDept](
	[PosXDeptID] [int] IDENTITY(1,1) NOT NULL,
	[PositionID] [int] NOT NULL,
	[DepartmentFIS] [char](4) NOT NULL,
	[PrimaryDept] [bit] NOT NULL,
 CONSTRAINT [PK_PosXDept_1] PRIMARY KEY CLUSTERED 
(
	[PosXDeptID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PositionTracking]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PositionTracking](
	[PositionTrackingID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[TrackingTypeID] [int] NOT NULL,
	[PositionID] [int] NOT NULL,
	[ActionDate] [datetime] NOT NULL,
	[Comments] [text] NULL,
 CONSTRAINT [PK_PositionTracking] PRIMARY KEY CLUSTERED 
(
	[PositionTrackingID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CommitteeMembers]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommitteeMembers](
	[CommitteeMemberID] [int] IDENTITY(1,1) NOT NULL,
	[MemberTypeID] [int] NOT NULL,
	[PositionID] [int] NOT NULL,
	[LoginID] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NULL,
 CONSTRAINT [PK_CommiteeMembers] PRIMARY KEY CLUSTERED 
(
	[CommitteeMemberID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Education]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Education](
	[EducationID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Institution] [nvarchar](100) NOT NULL,
	[Discipline] [nvarchar](50) NOT NULL,
	[ResearchField] [nvarchar](50) NULL,
	[Advisor] [nvarchar](50) NULL,
	[Complete] [bit] NOT NULL,
 CONSTRAINT [PK_Education] PRIMARY KEY CLUSTERED 
(
	[EducationID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CurrentPosition]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CurrentPosition](
	[CurrentPositionID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[Title] [nvarchar](100) NULL,
	[Department] [nvarchar](100) NULL,
	[Institution] [nvarchar](100) NULL,
	[Address1] [nvarchar](50) NULL,
	[Address2] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Zip] [nvarchar](20) NULL,
	[Country] [nvarchar](50) NULL,
	[Complete] [bit] NULL,
 CONSTRAINT [PK_CurrentPosition] PRIMARY KEY CLUSTERED 
(
	[CurrentPositionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApplicationTracking]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationTracking](
	[ApplicationTrackingID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[TrackingTypeID] [int] NOT NULL,
	[ActionDate] [datetime] NOT NULL,
	[Comments] [text] NULL,
 CONSTRAINT [PK_ApplicationTracking] PRIMARY KEY CLUSTERED 
(
	[ApplicationTrackingID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApplicationFiles]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationFiles](
	[ApplicationFileID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[FileID] [int] NOT NULL,
 CONSTRAINT [PK_ApplicationFiles] PRIMARY KEY CLUSTERED 
(
	[ApplicationFileID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[usp_InsertPosition]    Script Date: 01/31/2011 10:46:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

	Author : Alan Lai
	Date : 12/1/2006
	Description : Creates the stored procedure to insert a new position
		-also inserts an entry into the PositionTracking table.

*/

CREATE Procedure [dbo].[usp_InsertPosition]

	(
		@PositionTitle nvarchar(100),
		@PositionNumber nvarchar(20),
		@ShortDescription text,
		@DepartmentFIS char(4),
		@DatePosted datetime,
		@Deadline datetime,
		@NumReferences int,
		@NumPublications int,
		@AllowApps bit,
		@AllowCommittee bit,
		@AllowFaculty bit,
		@AllowVoting bit,
		@HRRep nvarchar(100),
		@HRPhone nvarchar(12),
		@HREmail nvarchar(100),
		@LoginID nvarchar(255) -- Employee ID of the person creating the position
	)


AS

		IF EXISTS ( SELECT * FROM Positions WHERE PositionNumber = @PositionNumber )
			RETURN 222

		INSERT INTO Positions
		                      (PositionTitle, PositionNumber, ShortDescription, DepartmentFIS, Deadline, NumReferences, NumPublications, AllowApps, CommitteeView, FacultyView, Vote, HR_Rep, 
		                      HR_Phone, HR_Email, DatePosted)
		VALUES     (@PositionTitle,@PositionNumber,@ShortDescription, @DepartmentFIS, @Deadline,@NumReferences,@NumPublications,@AllowApps,@AllowCommittee,@AllowFaculty,@AllowVoting,@HRRep,@HRPhone,@HREmail,@DatePosted)
					
		DECLARE @PositionID int, @TrackingTypeID int
		SET @PositionID = ( SELECT MAX(PositionID)
							FROM Positions
							WHERE PositionNumber = @PositionNumber )
							
		SET @TrackingTypeID = ( SELECT TrackingTypeID FROM TrackingType WHERE TrackingType = 'Create' )					
		DECLARE @UserID int
		
		SET @UserID = (
					SELECT TOP 1 UserID 
					FROM CATBERT.dbo.Login 
					WHERE LoginID = @LoginID
					)				
							
		INSERT INTO PositionTracking
		                      (UserID, TrackingTypeID, PositionID, ActionDate)
		VALUES     (@UserID,@TrackingTypeID,@PositionID, GETDATE())
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdatePosition]    Script Date: 01/31/2011 10:46:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

	Author : Alan Lai
	Date : 12/1/2006
	Description : Creates the stored procedure to insert a new position
		-also inserts an entry into the PositionTracking table.

*/

CREATE Procedure [dbo].[usp_UpdatePosition]

	(
		@PositionID int,
		@PositionTitle nvarchar(100),
		@PositionNumber nvarchar(20),
		@ShortDescription text,
		@DepartmentFIS char(4),
		@Deadline datetime,
		@NumReferences int,
		@NumPublications int,
		@AllowApps bit,
		@AllowCommittee bit,
		@AllowFaculty bit,
		@AllowVoting bit,
		@HRRep nvarchar(100),
		@HRPhone nvarchar(12),
		@HREmail nvarchar(100),
		@LoginID nvarchar(255) -- LoginID of the person updating the position
	)


AS
		SET NOCOUNT ON;
		
		DECLARE @positionMatch int
		SET @positionMatch = (	
								SELECT     COUNT(PositionID) AS NumPositions
								FROM         Positions
								WHERE      (PositionID = @PositionID)
							)
							
		IF @positionMatch = 0
			RETURN -1

		UPDATE    Positions
		SET              PositionTitle = @PositionTitle, PositionNumber = @PositionNumber, ShortDescription = @ShortDescription, DepartmentFIS = @DepartmentFIS, 
		                      Deadline = @Deadline, NumReferences = @NumReferences, NumPublications = @NumPublications, AllowApps = @AllowApps, 
		                      CommitteeView = @AllowCommittee, FacultyView = @AllowFaculty, Vote = @AllowVoting, HR_Rep = @HRRep, HR_Phone = @HRPhone, 
		                      HR_Email = @HREmail
		WHERE     (PositionID = @PositionID)
					
		DECLARE @TrackingTypeID int		
		DECLARE @UserID int		
		
		SET @TrackingTypeID = ( SELECT TrackingTypeID FROM TrackingType WHERE TrackingType = 'Modify' )					
		
		SET @UserID = ( SELECT TOP 1 UserID FROM CATBERT.dbo.Login WHERE LoginID = @LoginID )				
							
		INSERT INTO PositionTracking
		                      (UserID, TrackingTypeID, PositionID, ActionDate)
		VALUES     (@UserID,@TrackingTypeID,@PositionID, GETDATE())
GO
/****** Object:  StoredProcedure [dbo].[usp_GetNotificationList]    Script Date: 01/31/2011 10:46:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Scott Kirkland
-- Create date: 5/23/07
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetNotificationList] 
	@AdjustedDeadline DateTime

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


SELECT     Applications.ProfileID, Applications.Email, Profiles.FirstName, Profiles.MiddleName, Profiles.LastName, Applications.Submitted, Positions.Deadline, 
                      Positions.PositionTitle, Positions.HR_Email AS RecruitmentEmail, vUnit.FullName
FROM         Applications INNER JOIN
                      Profiles ON Applications.ProfileID = Profiles.ProfileID INNER JOIN
                      Positions ON Applications.PositionID = Positions.PositionID INNER JOIN
                      PosXDept ON Positions.PositionID = PosXDept.PositionID INNER JOIN
                      vUnit ON PosXDept.DepartmentFIS = vUnit.FIS_Code
WHERE     (Applications.Submitted = 0) 
				and (PosXDept.PrimaryDept = 1)
				AND (ABS(DATEDIFF(DAY, Positions.Deadline, @AdjustedDeadline)) < 1)

END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetCommitteeMembersByPosition]    Script Date: 01/31/2011 10:46:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Scott Kirkland
-- Create date: 5/9/07
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetCommitteeMembersByPosition] 
	-- Add the parameters for the stored procedure here
	@PositionID int, 
	@MemberTypeID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT     CommitteeMemberID
	FROM         CommitteeMembers
	WHERE     (MemberTypeID = @MemberTypeID) AND (PositionID = @PositionID)

END
GO
/****** Object:  Table [dbo].[References]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[References](
	[ReferenceID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[Title] [nvarchar](50) NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Acad_Title] [nvarchar](50) NULL,
	[Expertise] [nvarchar](50) NOT NULL,
	[Dept] [nvarchar](50) NOT NULL,
	[Institution] [nvarchar](50) NOT NULL,
	[Address1] [nvarchar](50) NOT NULL,
	[Address2] [nvarchar](50) NULL,
	[City] [nvarchar](50) NOT NULL,
	[State] [nvarchar](50) NOT NULL,
	[Zip] [nvarchar](20) NOT NULL,
	[Country] [nvarchar](50) NULL,
	[CountryCode] [nvarchar](5) NULL,
	[AreaCode] [nvarchar](50) NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[SentEmail] [bit] NOT NULL,
	[EmailDate] [datetime] NULL,
	[UnsolicitedReference] [bit] NOT NULL,
	[UnsolicitedEmailDate] [datetime] NULL,
	[UploadID] [nvarchar](50) NULL,
	[Complete] [bit] NULL,
	[ReferenceFileID] [int] NULL,
 CONSTRAINT [PK_References] PRIMARY KEY CLUSTERED 
(
	[ReferenceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Survey]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Survey](
	[SurveyID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[GenderID] [int] NULL,
	[EthnicityID] [int] NULL,
	[TribalAffiliation] [nvarchar](50) NULL,
	[Pub_Advertisement] [nvarchar](50) NULL,
	[Prof_Organization] [nvarchar](50) NULL,
	[Other] [nvarchar](100) NULL,
	[Complete] [bit] NULL,
 CONSTRAINT [PK_Survey] PRIMARY KEY CLUSTERED 
(
	[SurveyID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SurveyTracking]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SurveyTracking](
	[SurveyTrackingID] [int] IDENTITY(1,1) NOT NULL,
	[SurveyID] [int] NOT NULL,
	[TrackingTypeID] [int] NOT NULL,
	[ActionDate] [datetime] NOT NULL,
	[Comments] [text] NULL,
 CONSTRAINT [PK_SurveyTracking] PRIMARY KEY CLUSTERED 
(
	[SurveyTrackingID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RefFiles]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefFiles](
	[RefFileID] [int] IDENTITY(1,1) NOT NULL,
	[ReferenceID] [int] NOT NULL,
	[FileID] [int] NOT NULL,
 CONSTRAINT [PK_RefFiles] PRIMARY KEY CLUSTERED 
(
	[RefFileID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReferenceTracking]    Script Date: 01/31/2011 10:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReferenceTracking](
	[ReferenceTrackingID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[TrackingTypeID] [int] NOT NULL,
	[ReferenceID] [int] NOT NULL,
	[ActionDate] [datetime] NOT NULL,
	[Comments] [text] NULL,
 CONSTRAINT [PK_ReferenceTracking] PRIMARY KEY CLUSTERED 
(
	[ReferenceTrackingID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Default [DF_Applications_CoverLetterComplete]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[Applications] ADD  CONSTRAINT [DF_Applications_CoverLetterComplete]  DEFAULT ((0)) FOR [CoverLetterComplete]
GO
/****** Object:  Default [DF_Applications_ShortList]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[Applications] ADD  CONSTRAINT [DF_Applications_ShortList]  DEFAULT ((0)) FOR [ShortList]
GO
/****** Object:  Default [DF_Applications_NoConsideration]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[Applications] ADD  CONSTRAINT [DF_Applications_NoConsideration]  DEFAULT ((0)) FOR [NoConsideration]
GO
/****** Object:  Default [DF_Applications_GetReferences]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[Applications] ADD  CONSTRAINT [DF_Applications_GetReferences]  DEFAULT ((0)) FOR [GetReferences]
GO
/****** Object:  Default [DF_Positions_Closed]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[Positions] ADD  CONSTRAINT [DF_Positions_Closed]  DEFAULT ((0)) FOR [Closed]
GO
/****** Object:  Default [DF_Positions_AdminAccepted]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[Positions] ADD  CONSTRAINT [DF_Positions_AdminAccepted]  DEFAULT ((0)) FOR [AdminAccepted]
GO
/****** Object:  Default [DF_RecruitmentSrc_AllowSpecify]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[RecruitmentSrc] ADD  CONSTRAINT [DF_RecruitmentSrc_AllowSpecify]  DEFAULT ((0)) FOR [AllowSpecify]
GO
/****** Object:  Default [DF_References_SentEmail]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[References] ADD  CONSTRAINT [DF_References_SentEmail]  DEFAULT ((0)) FOR [SentEmail]
GO
/****** Object:  Default [DF_References_UnsolicitedReference]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[References] ADD  CONSTRAINT [DF_References_UnsolicitedReference]  DEFAULT ((0)) FOR [UnsolicitedReference]
GO
/****** Object:  ForeignKey [FK_AccountTracking_Accounts]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[AccountTracking]  WITH CHECK ADD  CONSTRAINT [FK_AccountTracking_Accounts] FOREIGN KEY([AccountID])
REFERENCES [dbo].[Accounts] ([AccountID])
GO
ALTER TABLE [dbo].[AccountTracking] CHECK CONSTRAINT [FK_AccountTracking_Accounts]
GO
/****** Object:  ForeignKey [FK_AccountTracking_TrackingType]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[AccountTracking]  WITH CHECK ADD  CONSTRAINT [FK_AccountTracking_TrackingType] FOREIGN KEY([TrackingTypeID])
REFERENCES [dbo].[TrackingType] ([TrackingTypeID])
GO
ALTER TABLE [dbo].[AccountTracking] CHECK CONSTRAINT [FK_AccountTracking_TrackingType]
GO
/****** Object:  ForeignKey [FK_ApplicationFiles_Applications]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[ApplicationFiles]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationFiles_Applications] FOREIGN KEY([ApplicationID])
REFERENCES [dbo].[Applications] ([ApplicationID])
GO
ALTER TABLE [dbo].[ApplicationFiles] CHECK CONSTRAINT [FK_ApplicationFiles_Applications]
GO
/****** Object:  ForeignKey [FK_ApplicationFiles_Files]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[ApplicationFiles]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationFiles_Files] FOREIGN KEY([FileID])
REFERENCES [dbo].[Files] ([FileID])
GO
ALTER TABLE [dbo].[ApplicationFiles] CHECK CONSTRAINT [FK_ApplicationFiles_Files]
GO
/****** Object:  ForeignKey [FK_Applications_Positions]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[Applications]  WITH CHECK ADD  CONSTRAINT [FK_Applications_Positions] FOREIGN KEY([PositionID])
REFERENCES [dbo].[Positions] ([PositionID])
GO
ALTER TABLE [dbo].[Applications] CHECK CONSTRAINT [FK_Applications_Positions]
GO
/****** Object:  ForeignKey [FK_Applications_Profiles]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[Applications]  WITH CHECK ADD  CONSTRAINT [FK_Applications_Profiles] FOREIGN KEY([ProfileID])
REFERENCES [dbo].[Profiles] ([ProfileID])
GO
ALTER TABLE [dbo].[Applications] CHECK CONSTRAINT [FK_Applications_Profiles]
GO
/****** Object:  ForeignKey [FK_ApplicationTracking_Applications]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[ApplicationTracking]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationTracking_Applications] FOREIGN KEY([ApplicationID])
REFERENCES [dbo].[Applications] ([ApplicationID])
GO
ALTER TABLE [dbo].[ApplicationTracking] CHECK CONSTRAINT [FK_ApplicationTracking_Applications]
GO
/****** Object:  ForeignKey [FK_ApplicationTracking_TrackingType]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[ApplicationTracking]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationTracking_TrackingType] FOREIGN KEY([TrackingTypeID])
REFERENCES [dbo].[TrackingType] ([TrackingTypeID])
GO
ALTER TABLE [dbo].[ApplicationTracking] CHECK CONSTRAINT [FK_ApplicationTracking_TrackingType]
GO
/****** Object:  ForeignKey [FK_ChangedProperties_ChangeTracking]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[ChangedProperties]  WITH CHECK ADD  CONSTRAINT [FK_ChangedProperties_ChangeTracking] FOREIGN KEY([TrackingID])
REFERENCES [dbo].[ChangeTracking] ([TrackingID])
GO
ALTER TABLE [dbo].[ChangedProperties] CHECK CONSTRAINT [FK_ChangedProperties_ChangeTracking]
GO
/****** Object:  ForeignKey [FK_ChangeTracking_ChangeTypes]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[ChangeTracking]  WITH CHECK ADD  CONSTRAINT [FK_ChangeTracking_ChangeTypes] FOREIGN KEY([ChangeTypeID])
REFERENCES [dbo].[ChangeTypes] ([ChangeTypeID])
GO
ALTER TABLE [dbo].[ChangeTracking] CHECK CONSTRAINT [FK_ChangeTracking_ChangeTypes]
GO
/****** Object:  ForeignKey [FK_CommiteeMembers_MemberTypes]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[CommitteeMembers]  WITH CHECK ADD  CONSTRAINT [FK_CommiteeMembers_MemberTypes] FOREIGN KEY([MemberTypeID])
REFERENCES [dbo].[MemberTypes] ([MemberTypeID])
GO
ALTER TABLE [dbo].[CommitteeMembers] CHECK CONSTRAINT [FK_CommiteeMembers_MemberTypes]
GO
/****** Object:  ForeignKey [FK_CommiteeMembers_Positions]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[CommitteeMembers]  WITH CHECK ADD  CONSTRAINT [FK_CommiteeMembers_Positions] FOREIGN KEY([PositionID])
REFERENCES [dbo].[Positions] ([PositionID])
GO
ALTER TABLE [dbo].[CommitteeMembers] CHECK CONSTRAINT [FK_CommiteeMembers_Positions]
GO
/****** Object:  ForeignKey [FK_CurrentPosition_Applications]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[CurrentPosition]  WITH CHECK ADD  CONSTRAINT [FK_CurrentPosition_Applications] FOREIGN KEY([ApplicationID])
REFERENCES [dbo].[Applications] ([ApplicationID])
GO
ALTER TABLE [dbo].[CurrentPosition] CHECK CONSTRAINT [FK_CurrentPosition_Applications]
GO
/****** Object:  ForeignKey [FK_Education_Applications]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[Education]  WITH CHECK ADD  CONSTRAINT [FK_Education_Applications] FOREIGN KEY([ApplicationID])
REFERENCES [dbo].[Applications] ([ApplicationID])
GO
ALTER TABLE [dbo].[Education] CHECK CONSTRAINT [FK_Education_Applications]
GO
/****** Object:  ForeignKey [FK_Files_FileTypes]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[Files]  WITH CHECK ADD  CONSTRAINT [FK_Files_FileTypes] FOREIGN KEY([FileTypeID])
REFERENCES [dbo].[FileTypes] ([FileTypeID])
GO
ALTER TABLE [dbo].[Files] CHECK CONSTRAINT [FK_Files_FileTypes]
GO
/****** Object:  ForeignKey [FK_DepartmentMembers]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[PositionCommittee]  WITH CHECK ADD  CONSTRAINT [FK_DepartmentMembers] FOREIGN KEY([DepartmentMemberID])
REFERENCES [dbo].[DepartmentMembers] ([DepartmentMemberID])
GO
ALTER TABLE [dbo].[PositionCommittee] CHECK CONSTRAINT [FK_DepartmentMembers]
GO
/****** Object:  ForeignKey [FK_PositionID]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[PositionCommittee]  WITH CHECK ADD  CONSTRAINT [FK_PositionID] FOREIGN KEY([PositionID])
REFERENCES [dbo].[Positions] ([PositionID])
GO
ALTER TABLE [dbo].[PositionCommittee] CHECK CONSTRAINT [FK_PositionID]
GO
/****** Object:  ForeignKey [FK_Positions_Templates]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[Positions]  WITH CHECK ADD  CONSTRAINT [FK_Positions_Templates] FOREIGN KEY([TemplateID])
REFERENCES [dbo].[Templates] ([TemplateID])
GO
ALTER TABLE [dbo].[Positions] CHECK CONSTRAINT [FK_Positions_Templates]
GO
/****** Object:  ForeignKey [FK_PositionTracking_Positions]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[PositionTracking]  WITH CHECK ADD  CONSTRAINT [FK_PositionTracking_Positions] FOREIGN KEY([PositionID])
REFERENCES [dbo].[Positions] ([PositionID])
GO
ALTER TABLE [dbo].[PositionTracking] CHECK CONSTRAINT [FK_PositionTracking_Positions]
GO
/****** Object:  ForeignKey [FK_PositionTracking_TrackingType]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[PositionTracking]  WITH CHECK ADD  CONSTRAINT [FK_PositionTracking_TrackingType] FOREIGN KEY([TrackingTypeID])
REFERENCES [dbo].[TrackingType] ([TrackingTypeID])
GO
ALTER TABLE [dbo].[PositionTracking] CHECK CONSTRAINT [FK_PositionTracking_TrackingType]
GO
/****** Object:  ForeignKey [FK_PosXDept_Positions1]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[PosXDept]  WITH CHECK ADD  CONSTRAINT [FK_PosXDept_Positions1] FOREIGN KEY([PositionID])
REFERENCES [dbo].[Positions] ([PositionID])
GO
ALTER TABLE [dbo].[PosXDept] CHECK CONSTRAINT [FK_PosXDept_Positions1]
GO
/****** Object:  ForeignKey [FK_PosXFileTypes_FileTypes]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[PosXFileTypes]  WITH CHECK ADD  CONSTRAINT [FK_PosXFileTypes_FileTypes] FOREIGN KEY([FileTypeID])
REFERENCES [dbo].[FileTypes] ([FileTypeID])
GO
ALTER TABLE [dbo].[PosXFileTypes] CHECK CONSTRAINT [FK_PosXFileTypes_FileTypes]
GO
/****** Object:  ForeignKey [FK_PosXFileTypes_Positions]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[PosXFileTypes]  WITH CHECK ADD  CONSTRAINT [FK_PosXFileTypes_Positions] FOREIGN KEY([PositionID])
REFERENCES [dbo].[Positions] ([PositionID])
GO
ALTER TABLE [dbo].[PosXFileTypes] CHECK CONSTRAINT [FK_PosXFileTypes_Positions]
GO
/****** Object:  ForeignKey [FK_PosXSteps_Positions]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[PosXSteps]  WITH CHECK ADD  CONSTRAINT [FK_PosXSteps_Positions] FOREIGN KEY([PositionID])
REFERENCES [dbo].[Positions] ([PositionID])
GO
ALTER TABLE [dbo].[PosXSteps] CHECK CONSTRAINT [FK_PosXSteps_Positions]
GO
/****** Object:  ForeignKey [FK_Profiles_Accounts]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[Profiles]  WITH CHECK ADD  CONSTRAINT [FK_Profiles_Accounts] FOREIGN KEY([AccountID])
REFERENCES [dbo].[Accounts] ([AccountID])
GO
ALTER TABLE [dbo].[Profiles] CHECK CONSTRAINT [FK_Profiles_Accounts]
GO
/****** Object:  ForeignKey [FK_References_Applications]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[References]  WITH CHECK ADD  CONSTRAINT [FK_References_Applications] FOREIGN KEY([ApplicationID])
REFERENCES [dbo].[Applications] ([ApplicationID])
GO
ALTER TABLE [dbo].[References] CHECK CONSTRAINT [FK_References_Applications]
GO
/****** Object:  ForeignKey [FK_References_Files]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[References]  WITH CHECK ADD  CONSTRAINT [FK_References_Files] FOREIGN KEY([ReferenceFileID])
REFERENCES [dbo].[Files] ([FileID])
GO
ALTER TABLE [dbo].[References] CHECK CONSTRAINT [FK_References_Files]
GO
/****** Object:  ForeignKey [FK_ReferenceTracking_References]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[ReferenceTracking]  WITH CHECK ADD  CONSTRAINT [FK_ReferenceTracking_References] FOREIGN KEY([ReferenceID])
REFERENCES [dbo].[References] ([ReferenceID])
GO
ALTER TABLE [dbo].[ReferenceTracking] CHECK CONSTRAINT [FK_ReferenceTracking_References]
GO
/****** Object:  ForeignKey [FK_ReferenceTracking_TrackingType]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[ReferenceTracking]  WITH CHECK ADD  CONSTRAINT [FK_ReferenceTracking_TrackingType] FOREIGN KEY([TrackingTypeID])
REFERENCES [dbo].[TrackingType] ([TrackingTypeID])
GO
ALTER TABLE [dbo].[ReferenceTracking] CHECK CONSTRAINT [FK_ReferenceTracking_TrackingType]
GO
/****** Object:  ForeignKey [FK_RefFiles_Files]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[RefFiles]  WITH CHECK ADD  CONSTRAINT [FK_RefFiles_Files] FOREIGN KEY([FileID])
REFERENCES [dbo].[Files] ([FileID])
GO
ALTER TABLE [dbo].[RefFiles] CHECK CONSTRAINT [FK_RefFiles_Files]
GO
/****** Object:  ForeignKey [FK_RefFiles_References]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[RefFiles]  WITH CHECK ADD  CONSTRAINT [FK_RefFiles_References] FOREIGN KEY([ReferenceID])
REFERENCES [dbo].[References] ([ReferenceID])
GO
ALTER TABLE [dbo].[RefFiles] CHECK CONSTRAINT [FK_RefFiles_References]
GO
/****** Object:  ForeignKey [FK_Survey_Applications]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[Survey]  WITH CHECK ADD  CONSTRAINT [FK_Survey_Applications] FOREIGN KEY([ApplicationID])
REFERENCES [dbo].[Applications] ([ApplicationID])
GO
ALTER TABLE [dbo].[Survey] CHECK CONSTRAINT [FK_Survey_Applications]
GO
/****** Object:  ForeignKey [FK_Survey_Ethnicity]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[Survey]  WITH CHECK ADD  CONSTRAINT [FK_Survey_Ethnicity] FOREIGN KEY([EthnicityID])
REFERENCES [dbo].[Ethnicity] ([EthnicityID])
GO
ALTER TABLE [dbo].[Survey] CHECK CONSTRAINT [FK_Survey_Ethnicity]
GO
/****** Object:  ForeignKey [FK_Survey_Gender]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[Survey]  WITH CHECK ADD  CONSTRAINT [FK_Survey_Gender] FOREIGN KEY([GenderID])
REFERENCES [dbo].[Gender] ([GenderID])
GO
ALTER TABLE [dbo].[Survey] CHECK CONSTRAINT [FK_Survey_Gender]
GO
/****** Object:  ForeignKey [FK_SurveyTracking_Survey]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[SurveyTracking]  WITH CHECK ADD  CONSTRAINT [FK_SurveyTracking_Survey] FOREIGN KEY([SurveyID])
REFERENCES [dbo].[Survey] ([SurveyID])
GO
ALTER TABLE [dbo].[SurveyTracking] CHECK CONSTRAINT [FK_SurveyTracking_Survey]
GO
/****** Object:  ForeignKey [FK_SurveyTracking_TrackingType]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[SurveyTracking]  WITH CHECK ADD  CONSTRAINT [FK_SurveyTracking_TrackingType] FOREIGN KEY([TrackingTypeID])
REFERENCES [dbo].[TrackingType] ([TrackingTypeID])
GO
ALTER TABLE [dbo].[SurveyTracking] CHECK CONSTRAINT [FK_SurveyTracking_TrackingType]
GO
/****** Object:  ForeignKey [FK_SurveyXRecruitmentSrc_RecruitmentSrc]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[SurveyXRecruitmentSrc]  WITH CHECK ADD  CONSTRAINT [FK_SurveyXRecruitmentSrc_RecruitmentSrc] FOREIGN KEY([RecruitmentSrcID])
REFERENCES [dbo].[RecruitmentSrc] ([RecruitmentSrcID])
GO
ALTER TABLE [dbo].[SurveyXRecruitmentSrc] CHECK CONSTRAINT [FK_SurveyXRecruitmentSrc_RecruitmentSrc]
GO
/****** Object:  ForeignKey [FK_Templates_TemplateTypes]    Script Date: 01/31/2011 10:46:06 ******/
ALTER TABLE [dbo].[Templates]  WITH CHECK ADD  CONSTRAINT [FK_Templates_TemplateTypes] FOREIGN KEY([TemplateTypeID])
REFERENCES [dbo].[TemplateTypes] ([TemplateTypeID])
GO
ALTER TABLE [dbo].[Templates] CHECK CONSTRAINT [FK_Templates_TemplateTypes]
GO
 