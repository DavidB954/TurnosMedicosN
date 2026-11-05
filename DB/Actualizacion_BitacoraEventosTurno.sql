/*
    Script de actualizacion - Bitacora de Eventos de Turno (orientada a eventos, via SQL Triggers)
    Base: GestionTurnosMedicos

    Esta es una bitacora ADICIONAL a la Bitacora general (BE_Bitacora/AccionBitacora), que hoy
    solo registra lo que la capa BLL decide llamar explicitamente (bll_bitacora.RegistrarEvento).
    Esta nueva bitacora se completa sola, a nivel de motor, mediante triggers AFTER INSERT/UPDATE
    sobre dbo.Turno: registra el Alta de un turno y todo cambio de su columna Estado, sin
    depender de que ninguna capa de la aplicacion se acuerde de llamarla, y sin poder ser
    salteada aunque alguien modifique la tabla Turno directamente desde SSMS.

    Usuario de la aplicacion (T06.B - IdUsuarioCambio): un trigger no conoce al usuario logueado
    (solo el login de SQL, que es siempre el mismo). Por eso la DAL, en la MISMA conexion y
    transaccion del INSERT/UPDATE, ejecuta sp_set_session_context 'IdUsuario' y el trigger lo
    lee con SESSION_CONTEXT(N'IdUsuario'). Queda NULL cuando el cambio lo hizo el proceso
    automatico de vencimiento o alguien desde SSMS (fuera de la aplicacion).

    Es idempotente: se puede ejecutar mas de una vez sin error.
*/

USE GestionTurnosMedicos;
GO

SET QUOTED_IDENTIFIER ON;
GO

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'BitacoraEventosTurno')
BEGIN
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
END
GO

IF COL_LENGTH('dbo.BitacoraEventosTurno', 'IdUsuarioCambio') IS NULL
    ALTER TABLE dbo.BitacoraEventosTurno ADD IdUsuarioCambio INT NULL
        CONSTRAINT FK_BitacoraEventosTurno_Usuario FOREIGN KEY REFERENCES dbo.Usuario(IdUsuario);
GO

-- El indice ayuda a las consultas del filtro por turno (pantalla frm_BitacoraTurno).
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes WHERE name = 'IX_BitacoraEventosTurno_IdTurno' AND object_id = OBJECT_ID('dbo.BitacoraEventosTurno')
)
BEGIN
    CREATE INDEX IX_BitacoraEventosTurno_IdTurno ON dbo.BitacoraEventosTurno (IdTurno);
END
GO

IF OBJECT_ID('dbo.TR_Turno_Insert_BitacoraEventos', 'TR') IS NOT NULL
    DROP TRIGGER dbo.TR_Turno_Insert_BitacoraEventos;
GO

-- Alta de turno: cualquier fila insertada en Turno (por Reservar o por Reprogramar,
-- que internamente crea un turno nuevo) queda registrada como evento ALTA.
CREATE TRIGGER dbo.TR_Turno_Insert_BitacoraEventos
ON dbo.Turno
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.BitacoraEventosTurno (IdTurno, TipoEvento, EstadoAnterior, EstadoNuevo, IdUsuarioCambio)
    SELECT i.IdTurno, 'ALTA', NULL, i.Estado, TRY_CAST(SESSION_CONTEXT(N'IdUsuario') AS INT)
    FROM inserted i;
END
GO

IF OBJECT_ID('dbo.TR_Turno_Update_BitacoraEventos', 'TR') IS NOT NULL
    DROP TRIGGER dbo.TR_Turno_Update_BitacoraEventos;
GO

-- Cambio de estado de turno (Confirmado -> Cancelado, etc.): solo dispara si la columna
-- Estado realmente cambio de valor, para no generar ruido con updates que no la tocan.
CREATE TRIGGER dbo.TR_Turno_Update_BitacoraEventos
ON dbo.Turno
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT UPDATE(Estado)
        RETURN;

    INSERT INTO dbo.BitacoraEventosTurno (IdTurno, TipoEvento, EstadoAnterior, EstadoNuevo, IdUsuarioCambio)
    SELECT i.IdTurno, 'CAMBIO_ESTADO', d.Estado, i.Estado, TRY_CAST(SESSION_CONTEXT(N'IdUsuario') AS INT)
    FROM inserted i
    INNER JOIN deleted d ON d.IdTurno = i.IdTurno
    WHERE ISNULL(d.Estado, '') <> ISNULL(i.Estado, '');
END
GO
