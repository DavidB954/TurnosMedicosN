/*
    Script de actualizacion - Turnos (Reservar / Cancelar / Modificar)
    Base: GestionTurnosMedicos

    Crea la tabla Turno: la reserva de un Paciente sobre un horario puntual (Medico_Horario)
    en una fecha de calendario concreta. Un mismo IdHorario+Fecha no puede tener mas de un
    turno Confirmado a la vez (indice unico filtrado), pero si puede volver a reservarse
    despues de cancelado.

    IMPORTANTE: los horarios (Medico_Horario) referenciados por algun turno NUNCA se borran
    fisicamente (la FK lo impediria, incluso si el turno esta Cancelado, porque el historial
    tiene que poder seguir resolviendose). Por eso frmGestionMedicos, al "Modificar" el
    horario de un medico, desactiva (Activo=0) los horarios anteriores en vez de borrarlos,
    y solo inserta/reactiva los que hacen falta.

    Es idempotente: se puede ejecutar mas de una vez sin error.
*/

USE GestionTurnosMedicos;
GO

-- Requerido por el indice filtrado de mas abajo (falla con "SET options incorrectas"
-- si la sesion que ejecuta el script tiene QUOTED_IDENTIFIER en OFF).
SET QUOTED_IDENTIFIER ON;
GO

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Turno')
BEGIN
    CREATE TABLE dbo.Turno (
        IdTurno INT IDENTITY(1,1) PRIMARY KEY,
        IdPaciente INT NOT NULL,
        IdHorario INT NOT NULL,
        Fecha DATE NOT NULL,
        Estado VARCHAR(20) NOT NULL CONSTRAINT DF_Turno_Estado DEFAULT ('Confirmado'),
        FechaCreacion DATETIME NOT NULL CONSTRAINT DF_Turno_FechaCreacion DEFAULT (GETDATE()),
        CONSTRAINT FK_Turno_Paciente FOREIGN KEY (IdPaciente) REFERENCES dbo.Usuario(IdUsuario),
        CONSTRAINT FK_Turno_Horario FOREIGN KEY (IdHorario) REFERENCES dbo.Medico_Horario(IdHorario),
        CONSTRAINT CK_Turno_Estado CHECK (Estado IN ('Confirmado', 'Cancelado'))
    );
END
GO

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes WHERE name = 'UQ_Turno_HorarioFecha_Confirmado' AND object_id = OBJECT_ID('dbo.Turno')
)
BEGIN
    CREATE UNIQUE INDEX UQ_Turno_HorarioFecha_Confirmado
        ON dbo.Turno (IdHorario, Fecha)
        WHERE Estado = 'Confirmado';
END
GO
