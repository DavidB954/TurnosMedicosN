/*
    Migracion 09 - Bitacora (revision de David, 2026-11-18)

    La pantalla Bitacora fallaba al abrir ("No se puede encontrar el valor solicitado VALOR_HORA_ACTUALIZADO"): un
    registro de la etapa de honorarios por hora tiene una accion que ya no existe. El concepto paso a ser el valor
    del turno (decision D15), asi que la accion se renombra igual que se renombraron los permisos en la migracion 06.

    Repetible. Ejecutar con: sqlcmd -S localhost\SQLEXPRESS -E -C -b -I -f 65001 -d GestionTurnosMedicos -i Migracion_09_Bitacora.sql
*/
SET NOCOUNT ON;
UPDATE dbo.Bitacora SET Accion = 'VALOR_TURNO_ACTUALIZADO' WHERE Accion = 'VALOR_HORA_ACTUALIZADO';
PRINT 'Migracion 09 aplicada.';
