
USE [practical_test_db]
GO

/****** Object:  Table [dbo].[Category]    Script Date: 10/13/2024 8:21:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


--------------------------------------

/****** Object:  Table [dbo].[UserCategory]    Script Date: 10/13/2024 8:21:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[CategoryId] [int] NULL,
 CONSTRAINT [PK_UserCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-------------------------------------

/****** Object:  Table [dbo].[Users]    Script Date: 10/13/2024 8:21:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[IsAdmin] [bit] NULL,
	[Isdeleted] [bit] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

--------------------------------------

SET IDENTITY_INSERT [dbo].[Category] ON 
INSERT [dbo].[Category] ([Id], [Name]) VALUES (1, N'Category-1')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (2, N'Category-2')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO


-----------------------------------------------------

SET IDENTITY_INSERT [dbo].[SubCategory] ON 
INSERT [dbo].[SubCategory] ([Id], [CategoryId], [Name]) VALUES (1, 1, N'Sub_Category-1')
INSERT [dbo].[SubCategory] ([Id], [CategoryId], [Name]) VALUES (2, 1, N'Sub_Category-2')
INSERT [dbo].[SubCategory] ([Id], [CategoryId], [Name]) VALUES (3, 1, N'Sub_Category-3')
SET IDENTITY_INSERT [dbo].[SubCategory] OFF
GO


-------------------------------------------------------

SET IDENTITY_INSERT [dbo].[UserCategory] ON 
INSERT [dbo].[UserCategory] ([Id], [UserId], [CategoryId]) VALUES (1, 1, 1)
INSERT [dbo].[UserCategory] ([Id], [UserId], [CategoryId]) VALUES (2, 2, 1)
INSERT [dbo].[UserCategory] ([Id], [UserId], [CategoryId]) VALUES (3, 3, 1)
SET IDENTITY_INSERT [dbo].[UserCategory] OFF
GO

---------------------------------------------

SET IDENTITY_INSERT [dbo].[Users] ON 
INSERT [dbo].[Users] ([Id], [Name], [Phone], [Email], [Password], [IsAdmin], [Isdeleted]) VALUES (1, N'Admin', N'9999999999', N'v@gmail.com', N'12345', 1, 0)
INSERT [dbo].[Users] ([Id], [Name], [Phone], [Email], [Password], [IsAdmin], [Isdeleted]) VALUES (2, N'James', N'9999999998', N'j@gmail.com', N'12345', 0, 0)
INSERT [dbo].[Users] ([Id], [Name], [Phone], [Email], [Password], [IsAdmin], [Isdeleted]) VALUES (3, N'Mary', N'8999999999', N'm@gmail.com', N'12345', 0, 0)
INSERT [dbo].[Users] ([Id], [Name], [Phone], [Email], [Password], [IsAdmin], [Isdeleted]) VALUES (4, N'Lisa', N'7999999999', N'lisa@gmail.com', N'12345', 0, 0)
INSERT [dbo].[Users] ([Id], [Name], [Phone], [Email], [Password], [IsAdmin], [Isdeleted]) VALUES (5, N'John', N'8499999999', N'john@gmail.com', N'12345', 0, 0)
INSERT [dbo].[Users] ([Id], [Name], [Phone], [Email], [Password], [IsAdmin], [Isdeleted]) VALUES (6, N'Daniel', N'7999994999', N'daniel@gmail.com', N'12345', 0, 0)
INSERT [dbo].[Users] ([Id], [Name], [Phone], [Email], [Password], [IsAdmin], [Isdeleted]) VALUES (7, N'Anthony', N'8999999993', N'anthony@gmail.com', N'12345', 0, 0)
INSERT [dbo].[Users] ([Id], [Name], [Phone], [Email], [Password], [IsAdmin], [Isdeleted]) VALUES (8, N'Nancy', N'9999949939', N'nancy@gmail.com', N'12345', 0, 0)
INSERT [dbo].[Users] ([Id], [Name], [Phone], [Email], [Password], [IsAdmin], [Isdeleted]) VALUES (9, N'Laura', N'9899949299', N'laura@gmail.com', N'12345', 0, 0)
INSERT [dbo].[Users] ([Id], [Name], [Phone], [Email], [Password], [IsAdmin], [Isdeleted]) VALUES (10, N'Paul', N'9299949959', N'paul@gmail.com', N'12345', 0, 0)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO

-------------------------------------------------

ALTER TABLE [dbo].[SubCategory]  WITH CHECK ADD  CONSTRAINT [FK_SubCategory_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[SubCategory] CHECK CONSTRAINT [FK_SubCategory_Category]
GO


----------------------------------------------------

ALTER TABLE [dbo].[UserCategory]  WITH CHECK ADD  CONSTRAINT [FK_UserCategory_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[UserCategory] CHECK CONSTRAINT [FK_UserCategory_Category]
GO


------------------------------------------------------

ALTER TABLE [dbo].[UserCategory]  WITH CHECK ADD  CONSTRAINT [FK_UserCategory_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserCategory] CHECK CONSTRAINT [FK_UserCategory_Users]
GO
