USE [MuniBarvaDB]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 2/8/2023 23:36:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](60) NULL,
	[Email] [varchar](40) NULL,
	[Password] [varchar](64) NULL,
	[Token] [varchar](64) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 2/8/2023 23:36:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](40) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles_Employees]    Script Date: 2/8/2023 23:36:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles_Employees](
	[IdEmployees] [int] NOT NULL,
	[IdRol] [int] NOT NULL,
 CONSTRAINT [PK_RolesEmployees] PRIMARY KEY CLUSTERED 
(
	[IdEmployees] ASC,
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Roles_Employees]  WITH CHECK ADD  CONSTRAINT [FK_RolesEmployess_Employees] FOREIGN KEY([IdEmployees])
REFERENCES [dbo].[Employees] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Roles_Employees] CHECK CONSTRAINT [FK_RolesEmployess_Employees]
GO
ALTER TABLE [dbo].[Roles_Employees]  WITH CHECK ADD  CONSTRAINT [FK_RolesEmployess_Roles] FOREIGN KEY([IdRol])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Roles_Employees] CHECK CONSTRAINT [FK_RolesEmployess_Roles]
GO
/****** Object:  StoredProcedure [dbo].[MUNI_PA_SIGNIN]    Script Date: 2/8/2023 23:36:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto
-- Create date: 02/08/2023
-- Description:	SP PARA OBTENER LOS DATOS DEL USUARIO
-- =============================================
CREATE PROCEDURE [dbo].[MUNI_PA_SIGNIN]
	@Email VARCHAR(40),
	@Password VARCHAR(64)
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	e.Id,
			e.[Name],
			e.Email
	FROM Employees e
	WHERE e.Email = @Email
	AND e.[Password] = @Password
END
GO
