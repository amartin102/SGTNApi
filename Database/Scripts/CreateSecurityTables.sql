-- ========================================
-- Script de creación de tablas de Seguridad
-- Base de datos: WorkShiftDb
-- Esquema: security
-- ========================================

USE [WorkShiftDb]
GO

-- Crear esquema si no existe
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'security')
BEGIN
    EXEC('CREATE SCHEMA [security]')
END
GO

-- ========================================
-- Tabla: tblRol
-- ========================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[security].[tblRol]') AND type in (N'U'))
BEGIN
    CREATE TABLE [security].[tblRol] (
        [idRol] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
        [nombre] NVARCHAR(100) NOT NULL,
        [descripcion] NVARCHAR(255) NULL,
        [estaActivo] BIT NOT NULL DEFAULT 1,
        [usuarioCreacion] NVARCHAR(100) NULL,
        [fechaCreacion] DATETIME NULL DEFAULT GETDATE(),
        [usuarioModificacion] NVARCHAR(100) NULL,
        [fechaModificacion] DATETIME NULL,
        CONSTRAINT [PK_tblRol] PRIMARY KEY CLUSTERED ([idRol] ASC)
    )
END
GO

-- ========================================
-- Tabla: tblPermiso
-- ========================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[security].[tblPermiso]') AND type in (N'U'))
BEGIN
    CREATE TABLE [security].[tblPermiso] (
        [idPermiso] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
        [nombre] NVARCHAR(100) NOT NULL,
        [descripcion] NVARCHAR(255) NULL,
        [codigo] NVARCHAR(50) NOT NULL,
        CONSTRAINT [PK_tblPermiso] PRIMARY KEY CLUSTERED ([idPermiso] ASC),
        CONSTRAINT [UQ_tblPermiso_Codigo] UNIQUE ([codigo])
    )
END
GO

-- ========================================
-- Tabla: tblUsuario
-- ========================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[security].[tblUsuario]') AND type in (N'U'))
BEGIN
    CREATE TABLE [security].[tblUsuario] (
        [idUsuario] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
        [nombreUsuario] NVARCHAR(100) NOT NULL,
        [contrasena] NVARCHAR(255) NOT NULL,
        [idRol] UNIQUEIDENTIFIER NOT NULL,
        [estaActivo] BIT NOT NULL DEFAULT 1,
        [ultimoIngreso] DATETIME NULL,
        [usuarioCreacion] NVARCHAR(100) NULL,
        [fechaCreacion] DATETIME NULL DEFAULT GETDATE(),
        [usuarioModificacion] NVARCHAR(100) NULL,
        [fechaModificacion] DATETIME NULL,
        CONSTRAINT [PK_tblUsuario] PRIMARY KEY CLUSTERED ([idUsuario] ASC),
        CONSTRAINT [UQ_tblUsuario_NombreUsuario] UNIQUE ([nombreUsuario]),
        CONSTRAINT [FK_tblUsuario_tblRol] FOREIGN KEY ([idRol]) 
            REFERENCES [security].[tblRol] ([idRol])
    )
END
GO

-- ========================================
-- Tabla: tblRolPermiso (Relación N:N)
-- ========================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[security].[tblRolPermiso]') AND type in (N'U'))
BEGIN
    CREATE TABLE [security].[tblRolPermiso] (
        [idRol] UNIQUEIDENTIFIER NOT NULL,
        [idPermiso] UNIQUEIDENTIFIER NOT NULL,
        CONSTRAINT [PK_tblRolPermiso] PRIMARY KEY CLUSTERED ([idRol] ASC, [idPermiso] ASC),
        CONSTRAINT [FK_tblRolPermiso_tblRol] FOREIGN KEY ([idRol]) 
            REFERENCES [security].[tblRol] ([idRol]) ON DELETE CASCADE,
        CONSTRAINT [FK_tblRolPermiso_tblPermiso] FOREIGN KEY ([idPermiso]) 
            REFERENCES [security].[tblPermiso] ([idPermiso]) ON DELETE CASCADE
    )
END
GO

-- ========================================
-- Datos iniciales: Permisos
-- ========================================
IF NOT EXISTS (SELECT * FROM [security].[tblPermiso])
BEGIN
    INSERT INTO [security].[tblPermiso] ([idPermiso], [nombre], [descripcion], [codigo])
    VALUES 
        (@IdPermiso1, 'Crear Usuario', 'Permite crear nuevos usuarios', 'USUARIO_CREAR'),
        (@IdPermiso2, 'Editar Usuario', 'Permite editar usuarios existentes', 'USUARIO_EDITAR'),
        (@IdPermiso3, 'Eliminar Usuario', 'Permite eliminar usuarios', 'USUARIO_ELIMINAR'),
        (@IdPermiso4, 'Ver Usuario', 'Permite ver listado de usuarios', 'USUARIO_VER'),
        (@IdPermiso5, 'Crear Rol', 'Permite crear nuevos roles', 'ROL_CREAR'),
        (@IdPermiso6, 'Editar Rol', 'Permite editar roles existentes', 'ROL_EDITAR'),
        (@IdPermiso7, 'Eliminar Rol', 'Permite eliminar roles', 'ROL_ELIMINAR'),
        (@IdPermiso8, 'Ver Rol', 'Permite ver listado de roles', 'ROL_VER'),
        (@IdPermiso9, 'Asignar Permisos', 'Permite asignar permisos a roles', 'ROL_ASIGNAR_PERMISOS'),
        (@IdPermiso10, 'Gestionar Nómina', 'Permite gestionar nóminas', 'NOMINA_GESTIONAR'),
        (@IdPermiso11, 'Ver Nómina', 'Permite ver nóminas', 'NOMINA_VER'),
        (@IdPermiso12, 'Gestionar Empleados', 'Permite gestionar empleados', 'EMPLEADO_GESTIONAR'),
        (@IdPermiso13, 'Ver Empleados', 'Permite ver empleados', 'EMPLEADO_VER'),
        (@IdPermiso14, 'Gestionar Clientes', 'Permite gestionar clientes', 'CLIENTE_GESTIONAR'),
        (@IdPermiso15, 'Ver Clientes', 'Permite ver clientes', 'CLIENTE_VER')
END
GO

-- ========================================
-- Datos iniciales: Roles
-- ========================================
IF NOT EXISTS (SELECT * FROM [security].[tblRol])
BEGIN
    DECLARE @IdRolAdmin UNIQUEIDENTIFIER = NEWID()
    DECLARE @IdRolSupervisor UNIQUEIDENTIFIER = NEWID()
    DECLARE @IdRolUsuario UNIQUEIDENTIFIER = NEWID()

    INSERT INTO [security].[tblRol] ([idRol], [nombre], [descripcion], [estaActivo], [usuarioCreacion], [fechaCreacion])
    VALUES 
        (@IdRolAdmin, 'Administrador', 'Acceso total al sistema', 1, 'SYSTEM', GETDATE()),
        (@IdRolSupervisor, 'Supervisor', 'Acceso a gestión de nómina y empleados', 1, 'SYSTEM', GETDATE()),
        (@IdRolUsuario, 'Usuario', 'Acceso de solo lectura', 1, 'SYSTEM', GETDATE())

    -- Asignar todos los permisos al Administrador
    INSERT INTO [security].[tblRolPermiso] ([idRol], [idPermiso])
    SELECT @IdRolAdmin, [idPermiso] FROM [security].[tblPermiso]

    -- Asignar permisos limitados al Supervisor
    INSERT INTO [security].[tblRolPermiso] ([idRol], [idPermiso])
    SELECT @IdRolSupervisor, [idPermiso] FROM [security].[tblPermiso] 
    WHERE [codigo] IN ('NOMINA_GESTIONAR', 'NOMINA_VER', 'EMPLEADO_GESTIONAR', 'EMPLEADO_VER', 'CLIENTE_VER')

    -- Asignar permisos de solo lectura al Usuario
    INSERT INTO [security].[tblRolPermiso] ([idRol], [idPermiso])
    SELECT @IdRolUsuario, [idPermiso] FROM [security].[tblPermiso] 
    WHERE [codigo] IN ('USUARIO_VER', 'ROL_VER', 'NOMINA_VER', 'EMPLEADO_VER', 'CLIENTE_VER')

    -- Crear usuarios por defecto
    INSERT INTO [security].[tblUsuario] ([idUsuario], [nombreUsuario], [contrasena], [idRol], [estaActivo], [usuarioCreacion], [fechaCreacion])
    VALUES 
        (NEWID(), 'admin', 'Admin123!', @IdRolAdmin, 1, 'SYSTEM', GETDATE()),
        (NEWID(), 'supervisor', 'Supervisor123!', @IdRolSupervisor, 1, 'SYSTEM', GETDATE()),
        (NEWID(), 'usuario', 'Usuario123!', @IdRolUsuario, 1, 'SYSTEM', GETDATE())
END
GO

-- ========================================
-- Índices adicionales para mejorar el rendimiento
-- ========================================
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tblUsuario_IdRol')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_tblUsuario_IdRol] ON [security].[tblUsuario] ([idRol])
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tblUsuario_NombreUsuario')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_tblUsuario_NombreUsuario] ON [security].[tblUsuario] ([nombreUsuario])
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tblPermiso_Codigo')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_tblPermiso_Codigo] ON [security].[tblPermiso] ([codigo])
END
GO

PRINT 'Tablas de seguridad creadas exitosamente con UNIQUEIDENTIFIER (Guid)'
GO
