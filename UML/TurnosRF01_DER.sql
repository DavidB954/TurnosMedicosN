/*
    DDL consolidado - SOLO para reverse-engineering del DER en Sparx Enterprise Architect
    (Database Builder / Import DDL). NO ejecutar contra la base real: la base real ya tiene
    estas tablas creadas por los scripts idempotentes de DB/ (Actualizacion_Turno.sql,
    Actualizacion_MedicoHorario.sql, Actualizacion_BitacoraEventosTurno.sql,
    Actualizacion_ProcesoVencimientoTurnos.sql). Este archivo junta esas mismas definiciones
    en un solo CREATE TABLE por tabla, sin los IF NOT EXISTS/GO de control, porque el parser
    de DDL de EA espera sentencias planas.

    Usuario, Especialidades, Bitacora y Medico_Especialidad no tienen script propio en DB/
    (se crearon antes, a mano); sus columnas de abajo estan reconstruidas a partir de las
    columnas que el codigo (BE_Usuario, BE_Especialidad, BE_Bitacora, DAL_MedicoHorario)
    realmente lee y escribe, no son un export literal del motor. Turno, Medico_Horario y
    BitacoraEventosTurno si son copia exacta de sus scripts en DB/.

    Como importar en EA (el nombre exacto del menu varia segun la version):
      Ribbon "Design" -> "Database Builder" -> "Import" -> "Import DDL from File",
      motor "SQL Server", elegir este archivo, destino: paquete UML > "RF 01 - Turnos" > "DER".
    En versiones mas viejas: menu "Extensions" -> "Data Modeling" -> "Import DDL".
*/

CREATE TABLE dbo.Usuario (
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    Email VARCHAR(150) NOT NULL,
    HashPassword VARCHAR(200) NOT NULL,
    IntentosFallidos INT NOT NULL DEFAULT (0),
    Activo BIT NOT NULL DEFAULT (1),
    DVH VARCHAR(200) NULL
);

CREATE TABLE dbo.Especialidades (
    IdEspecialidad INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL
);

CREATE TABLE dbo.Medico_Especialidad (
    IdMedico INT NOT NULL,
    IdEsp INT NOT NULL,
    CONSTRAINT PK_Medico_Especialidad PRIMARY KEY (IdMedico, IdEsp),
    CONSTRAINT FK_MedicoEspecialidad_Usuario FOREIGN KEY (IdMedico) REFERENCES dbo.Usuario(IdUsuario),
    CONSTRAINT FK_MedicoEspecialidad_Especialidad FOREIGN KEY (IdEsp) REFERENCES dbo.Especialidades(IdEspecialidad)
);

CREATE TABLE dbo.Medico_Horario (
    IdHorario INT IDENTITY(1,1) PRIMARY KEY,
    IdMedico INT NOT NULL,
    DiaSemana TINYINT NOT NULL,
    HoraInicio TIME(0) NOT NULL,
    HoraFin TIME(0) NOT NULL,
    Activo BIT NOT NULL CONSTRAINT DF_MedicoHorario_Activo DEFAULT (1),
    CONSTRAINT FK_MedicoHorario_Usuario FOREIGN KEY (IdMedico) REFERENCES dbo.Usuario(IdUsuario),
    CONSTRAINT UQ_MedicoHorario UNIQUE (IdMedico, DiaSemana, HoraInicio),
    CONSTRAINT CK_MedicoHorario_Dia CHECK (DiaSemana BETWEEN 1 AND 6),
    CONSTRAINT CK_MedicoHorario_Horas CHECK (HoraFin > HoraInicio)
);

CREATE TABLE dbo.Turno (
    IdTurno INT IDENTITY(1,1) PRIMARY KEY,
    IdPaciente INT NOT NULL,
    IdHorario INT NOT NULL,
    Fecha DATE NOT NULL,
    Estado VARCHAR(20) NOT NULL CONSTRAINT DF_Turno_Estado DEFAULT ('Confirmado'),
    FechaCreacion DATETIME NOT NULL CONSTRAINT DF_Turno_FechaCreacion DEFAULT (GETDATE()),
    CONSTRAINT FK_Turno_Paciente FOREIGN KEY (IdPaciente) REFERENCES dbo.Usuario(IdUsuario),
    CONSTRAINT FK_Turno_Horario FOREIGN KEY (IdHorario) REFERENCES dbo.Medico_Horario(IdHorario),
    CONSTRAINT CK_Turno_Estado CHECK (Estado IN ('Confirmado', 'Cancelado', 'Atendido', 'Ausente'))
);

CREATE UNIQUE INDEX UQ_Turno_HorarioFecha_Confirmado
    ON dbo.Turno (IdHorario, Fecha)
    WHERE Estado = 'Confirmado';

CREATE TABLE dbo.BitacoraEventosTurno (
    IdBitacoraEvento INT IDENTITY(1,1) PRIMARY KEY,
    IdTurno INT NOT NULL,
    TipoEvento VARCHAR(20) NOT NULL,
    EstadoAnterior VARCHAR(20) NULL,
    EstadoNuevo VARCHAR(20) NOT NULL,
    FechaHora DATETIME NOT NULL CONSTRAINT DF_BitacoraEventosTurno_FechaHora DEFAULT (GETDATE()),
    UsuarioBD VARCHAR(128) NOT NULL CONSTRAINT DF_BitacoraEventosTurno_UsuarioBD DEFAULT (SUSER_SNAME()),
    HostBD VARCHAR(128) NOT NULL CONSTRAINT DF_BitacoraEventosTurno_HostBD DEFAULT (HOST_NAME()),
    CONSTRAINT FK_BitacoraEventosTurno_Turno FOREIGN KEY (IdTurno) REFERENCES dbo.Turno(IdTurno),
    CONSTRAINT CK_BitacoraEventosTurno_TipoEvento CHECK (TipoEvento IN ('ALTA', 'CAMBIO_ESTADO'))
);

CREATE INDEX IX_BitacoraEventosTurno_IdTurno ON dbo.BitacoraEventosTurno (IdTurno);

CREATE TABLE dbo.Bitacora (
    IdBitacora INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NULL,
    FechaHora DATETIME NOT NULL DEFAULT (GETDATE()),
    Accion VARCHAR(50) NOT NULL,
    Modulo VARCHAR(30) NOT NULL,
    DireccionIP VARCHAR(100) NULL,
    NombreMaquina VARCHAR(100) NULL,
    Descripcion VARCHAR(500) NULL,
    CONSTRAINT FK_Bitacora_Usuario FOREIGN KEY (IdUsuario) REFERENCES dbo.Usuario(IdUsuario)
);
