USE [Seg]
GO

/****** Object:  Table [dbo].[GruposUsuariosFuncionalidade]    Script Date: 14/05/2016 23:06:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GruposUsuariosFuncionalidade](
	[GruposUsuariosCodigo] [int] NOT NULL,
	[ModuloFuncionalidadeCodigo] [int] NOT NULL,
 CONSTRAINT [PK_GruposUsuariosFuncionalidade] PRIMARY KEY CLUSTERED 
(
	[GruposUsuariosCodigo] ASC,
	[ModuloFuncionalidadeCodigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[GruposUsuarios]    Script Date: 14/05/2016 23:06:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[GruposUsuarios](
	[GruposUsuariosCodigo] [int] NOT NULL,
	[GruposUsuariosDescricao] [varchar](50) NOT NULL,
 CONSTRAINT [PK_GruposUsuarios] PRIMARY KEY CLUSTERED 
(
	[GruposUsuariosCodigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Usuario]    Script Date: 14/05/2016 23:06:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Usuario](
	[UsuarioCodigo] [int] NOT NULL,
	[UsuarioLogin] [varchar](50) NULL,
	[UsuarioNome] [varchar](200) NULL,
	[UsuarioSenha] [nvarchar](50) NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[UsuarioCodigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Usuario] UNIQUE NONCLUSTERED 
(
	[UsuarioNome] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[UsuarioFuncionalidade]    Script Date: 14/05/2016 23:06:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UsuarioFuncionalidade](
	[UsuarioCodigo] [int] NOT NULL,
	[ModuloFuncionalidadeCodigo] [int] NOT NULL,
 CONSTRAINT [PK_UsuarioFuncionalidade] PRIMARY KEY CLUSTERED 
(
	[UsuarioCodigo] ASC,
	[ModuloFuncionalidadeCodigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[UsuariosGrupos]    Script Date: 14/05/2016 23:06:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UsuariosGrupos](
	[GruposUsuariosCodigo] [int] NOT NULL,
	[UsuarioCodigo] [int] NOT NULL,
 CONSTRAINT [PK_UsuariosGrupos] PRIMARY KEY CLUSTERED 
(
	[GruposUsuariosCodigo] ASC,
	[UsuarioCodigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[Modulo]    Script Date: 14/05/2016 23:06:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Modulo](
	[ModuloCodigo] [int] IDENTITY(1,1) NOT NULL,
	[ModuloDescricao] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Modulo] PRIMARY KEY CLUSTERED 
(
	[ModuloCodigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[ModuloFuncionalidade]    Script Date: 14/05/2016 23:06:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ModuloFuncionalidade](
	[ModuloFuncionalidadeCodigo] [int] IDENTITY(1,1) NOT NULL,
	[ModuloCodigo] [int] NOT NULL,
	[FuncionalidadeDescricao] [varchar](200) NOT NULL,
	[FormDescricao] [varchar](200) NOT NULL,
	[ControleDescricao] [varchar](200) NULL,
 CONSTRAINT [PK_ModuloFuncionalidade] PRIMARY KEY CLUSTERED 
(
	[ModuloFuncionalidadeCodigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[GruposUsuariosFuncionalidade]  WITH CHECK ADD  CONSTRAINT [FK_GruposUsuariosFuncionalidade_GruposUsuarios] FOREIGN KEY([GruposUsuariosCodigo])
REFERENCES [dbo].[GruposUsuarios] ([GruposUsuariosCodigo])
GO

ALTER TABLE [dbo].[GruposUsuariosFuncionalidade] CHECK CONSTRAINT [FK_GruposUsuariosFuncionalidade_GruposUsuarios]
GO

ALTER TABLE [dbo].[GruposUsuariosFuncionalidade]  WITH CHECK ADD  CONSTRAINT [FK_GruposUsuariosFuncionalidade_ModuloFuncionalidade] FOREIGN KEY([ModuloFuncionalidadeCodigo])
REFERENCES [dbo].[ModuloFuncionalidade] ([ModuloFuncionalidadeCodigo])
GO

ALTER TABLE [dbo].[GruposUsuariosFuncionalidade] CHECK CONSTRAINT [FK_GruposUsuariosFuncionalidade_ModuloFuncionalidade]
GO

ALTER TABLE [dbo].[UsuarioFuncionalidade]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioFuncionalidade_ModuloFuncionalidade] FOREIGN KEY([ModuloFuncionalidadeCodigo])
REFERENCES [dbo].[ModuloFuncionalidade] ([ModuloFuncionalidadeCodigo])
GO

ALTER TABLE [dbo].[UsuarioFuncionalidade] CHECK CONSTRAINT [FK_UsuarioFuncionalidade_ModuloFuncionalidade]
GO

ALTER TABLE [dbo].[UsuarioFuncionalidade]  WITH NOCHECK ADD  CONSTRAINT [FK_UsuarioFuncionalidade_Usuario] FOREIGN KEY([UsuarioCodigo])
REFERENCES [dbo].[Usuario] ([UsuarioCodigo])
GO

ALTER TABLE [dbo].[UsuarioFuncionalidade] CHECK CONSTRAINT [FK_UsuarioFuncionalidade_Usuario]
GO

ALTER TABLE [dbo].[UsuariosGrupos]  WITH CHECK ADD  CONSTRAINT [FK_UsuariosGrupos_GruposUsuarios] FOREIGN KEY([GruposUsuariosCodigo])
REFERENCES [dbo].[GruposUsuarios] ([GruposUsuariosCodigo])
GO

ALTER TABLE [dbo].[UsuariosGrupos] CHECK CONSTRAINT [FK_UsuariosGrupos_GruposUsuarios]
GO

ALTER TABLE [dbo].[UsuariosGrupos]  WITH NOCHECK ADD  CONSTRAINT [FK_UsuariosGrupos_Usuario] FOREIGN KEY([UsuarioCodigo])
REFERENCES [dbo].[Usuario] ([UsuarioCodigo])
GO

ALTER TABLE [dbo].[UsuariosGrupos] CHECK CONSTRAINT [FK_UsuariosGrupos_Usuario]
GO

ALTER TABLE [dbo].[ModuloFuncionalidade]  WITH NOCHECK ADD  CONSTRAINT [FK_ModuloFuncionalidade_Modulo] FOREIGN KEY([ModuloCodigo])
REFERENCES [dbo].[Modulo] ([ModuloCodigo])
GO

ALTER TABLE [dbo].[ModuloFuncionalidade] CHECK CONSTRAINT [FK_ModuloFuncionalidade_Modulo]
GO


