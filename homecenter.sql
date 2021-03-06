USE [Homecenter]
GO
/****** Object:  Table [dbo].[Asesores]    Script Date: 2018-03-13 05.20.40  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Asesores](
	[AsesorId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[Cedula] [varchar](10) NOT NULL,
	[Codigo] [varchar](50) NOT NULL,
	[Tienda] [varchar](10) NULL,
 CONSTRAINT [PK_Asesor] PRIMARY KEY CLUSTERED 
(
	[AsesorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Garantias]    Script Date: 2018-03-13 05.20.40  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Garantias](
	[GarantiaId] [int] IDENTITY(1,1) NOT NULL,
	[Categoria] [varchar](50) NOT NULL,
	[SKU] [varchar](50) NOT NULL,
	[Descripcion] [varchar](200) NOT NULL,
	[PrecioSinIva] [decimal](18, 2) NOT NULL,
	[PrecioConIva] [decimal](18, 2) NOT NULL,
	[PorcentajeComision] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_MaestroGEXTId] PRIMARY KEY CLUSTERED 
(
	[GarantiaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Perfiles]    Script Date: 2018-03-13 05.20.40  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Perfiles](
	[PerfilId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Perfil] PRIMARY KEY CLUSTERED 
(
	[PerfilId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 2018-03-13 05.20.40  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[UsuarioId] [int] IDENTITY(1,1) NOT NULL,
	[Login] [varchar](50) NOT NULL,
	[Password] [varchar](32) NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[UsuarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UsuariosPerfiles]    Script Date: 2018-03-13 05.20.40  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsuariosPerfiles](
	[UsuarioPerfilId] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioId] [int] NOT NULL,
	[PerfilId] [int] NOT NULL,
 CONSTRAINT [PK_UsuarioPerfil] PRIMARY KEY CLUSTERED 
(
	[UsuarioPerfilId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Ventas]    Script Date: 2018-03-13 05.20.40  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ventas](
	[VentaId] [int] IDENTITY(1,1) NOT NULL,
	[AsesorId] [int] NOT NULL,
	[SKU] [varchar](50) NOT NULL,
	[FechaRegistro] [datetime] NOT NULL,
	[CedulaCliente] [varchar](10) NOT NULL,
	[SKUElectrodomestico] [varchar](50) NOT NULL,
	[ValorComision] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Venta] PRIMARY KEY CLUSTERED 
(
	[VentaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Perfiles] ON 

INSERT [dbo].[Perfiles] ([PerfilId], [Nombre]) VALUES (1, N'Administrador')
INSERT [dbo].[Perfiles] ([PerfilId], [Nombre]) VALUES (2, N'Asesor')
SET IDENTITY_INSERT [dbo].[Perfiles] OFF

SET IDENTITY_INSERT [dbo].[Usuarios] ON 
INSERT [dbo].[Usuarios] ([UsuarioId], [Login], [Password]) VALUES (1, N'admin', N'f9bfa23461a398a8f9687b4a70267e8d')
SET IDENTITY_INSERT [dbo].[Usuarios] OFF

SET IDENTITY_INSERT [dbo].[UsuariosPerfiles] ON 
INSERT [dbo].[UsuariosPerfiles] ([UsuarioPerfilId], [UsuarioId], [PerfilId]) VALUES (1, 1, 1)
SET IDENTITY_INSERT [dbo].[UsuariosPerfiles] OFF

SET ANSI_PADDING ON

GO
/****** Object:  Index [UK_Asesores]    Script Date: 2018-03-13 05.20.40  ******/
ALTER TABLE [dbo].[Asesores] ADD  CONSTRAINT [UK_Asesores] UNIQUE NONCLUSTERED 
(
	[Cedula] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UK_Garantias]    Script Date: 2018-03-13 05.20.40  ******/
ALTER TABLE [dbo].[Garantias] ADD  CONSTRAINT [UK_Garantias] UNIQUE NONCLUSTERED 
(
	[SKU] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UsuariosPerfiles]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioPerfil_Perfil] FOREIGN KEY([PerfilId])
REFERENCES [dbo].[Perfiles] ([PerfilId])
GO
ALTER TABLE [dbo].[UsuariosPerfiles] CHECK CONSTRAINT [FK_UsuarioPerfil_Perfil]
GO
ALTER TABLE [dbo].[UsuariosPerfiles]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioPerfil_Usuario] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([UsuarioId])
GO
ALTER TABLE [dbo].[UsuariosPerfiles] CHECK CONSTRAINT [FK_UsuarioPerfil_Usuario]
GO
/****** Object:  StoredProcedure [dbo].[crear_usuario]    Script Date: 2018-03-13 05.20.40  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Leandro Baena
-- Create date: 2018-03-13
-- Description:	Crea un usuario
-- =============================================
CREATE PROCEDURE [dbo].[crear_usuario]
	@login		VARCHAR(50), 
	@password	VARCHAR(50)
AS
BEGIN
	INSERT INTO Usuarios ([Login], [Password]) VALUES (@login, SUBSTRING(master.dbo.fn_varbintohexstr(HashBytes('MD5', @password)), 3, 32))

	SELECT UsuarioId, [Login], [Password] FROM Usuarios WHERE UsuarioId = SCOPE_IDENTITY()
END

GO
/****** Object:  StoredProcedure [dbo].[login_usuario]    Script Date: 2018-03-13 05.20.40  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Leandro Baena
-- Create date: 2018-03-13
-- Description:	Valida el login de un usuario
-- =============================================
CREATE PROCEDURE [dbo].[login_usuario]
	@login		VARCHAR(50), 
	@password	VARCHAR(50)
AS
BEGIN
	SELECT
		UsuarioId, [Login], [Password]
	FROM
		Usuarios
	WHERE
		[LOGIN] = @login
		AND [Password] = SUBSTRING(master.dbo.fn_varbintohexstr(HashBytes('MD5', @password)), 3, 32)
END

GO
/****** Object:  StoredProcedure [dbo].[perfiles_usuario]    Script Date: 2018-03-13 05.20.40  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Leandro Baena
-- Create date: 2018-03-13
-- Description:	Trae los perfiles de un determinado usuario
-- =============================================
CREATE PROCEDURE [dbo].[perfiles_usuario]
	@usuarioId INT
AS
BEGIN
	SELECT
		p.PerfilId, p.Nombre
	FROM
		UsuariosPerfiles up
		INNER JOIN perfiles p ON up.PerfilId = p.PerfilId
	WHERE
		up.UsuarioId = @usuarioId
END

GO
/****** Object:  StoredProcedure [dbo].[reporte_comisiones]    Script Date: 2018-03-13 05.20.40  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Leandro Baena
-- Create date: 2018-03-13
-- Description:	Trae el listado de ventas dentro de un rango de fechas de un determinado asesor
-- =============================================
CREATE PROCEDURE [dbo].[reporte_comisiones]
	@fechaInicio	VARCHAR(50),
	@fechaFin		VARCHAR(50),
	@asesorId		INT
AS
BEGIN
	SELECT
		VentaId, AsesorId, SKU, FechaRegistro, CedulaCliente, SKUElectrodomestico, ValorComision
	FROM
		Ventas
	WHERE
		FechaRegistro BETWEEN CONVERT(DATETIME, @fechaInicio + ' 00:00:00', 120) AND CONVERT(DATETIME, @fechaFin + ' 23:59:59', 120)
		AND AsesorId = @asesorId
END

GO
/****** Object:  StoredProcedure [dbo].[reporte_ventas]    Script Date: 2018-03-13 05.20.40  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Leandro Baena
-- Create date: 2018-03-09
-- Description:	Trae el listado de ventas dentro de un rango de fechas
-- =============================================
CREATE PROCEDURE [dbo].[reporte_ventas]
	@fechaInicio	VARCHAR(50),
	@fechaFin		VARCHAR(50)
AS
BEGIN
	SELECT
		v.VentaId, a.Nombre AS Asesor, a.Cedula, a.Codigo, a.Tienda, v.SKU, v.FechaRegistro, v.CedulaCliente, v.SKUElectrodomestico, v.ValorComision
	FROM
		Ventas v
		INNER JOIN Asesores a ON v.AsesorId = a.AsesorId
	WHERE
		v.FechaRegistro BETWEEN CONVERT(DATETIME, @fechaInicio + ' 00:00:00', 120) AND CONVERT(DATETIME, @fechaFin + ' 23:59:59', 120)
END

GO
USE [master]
GO
ALTER DATABASE [Homecenter] SET  READ_WRITE 
GO
