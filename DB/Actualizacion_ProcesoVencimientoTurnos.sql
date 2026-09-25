/*
    Script de actualizacion - Proceso automatico de vencimiento de turnos (Confirmado -> Ausente)
    Base: GestionTurnosMedicos

    dbo.Turno se creo (Actualizacion_Turno.sql) con CK_Turno_Estado limitado a 'Confirmado' y
    'Cancelado'. Este proceso necesita ademas los estados 'Atendido' (lo marca el medico) y
    'Ausente' (lo marca este proceso automatico cuando pasa 1 hora del fin del turno y nadie
    lo atendio), asi que hay que ampliar ese CHECK antes de que BLL_Turno.ProcesarVencimientos
    pueda hacer el UPDATE a 'Ausente'.

    Es idempotente: se puede ejecutar mas de una vez sin error.
*/

USE GestionTurnosMedicos;
GO

IF EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_Turno_Estado')
BEGIN
    ALTER TABLE dbo.Turno DROP CONSTRAINT CK_Turno_Estado;
END
GO

ALTER TABLE dbo.Turno
    ADD CONSTRAINT CK_Turno_Estado CHECK (Estado IN ('Confirmado', 'Cancelado', 'Atendido', 'Ausente'));
GO
