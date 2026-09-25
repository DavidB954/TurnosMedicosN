/*
    Script de actualizacion - Especialidad por horario del medico
    Base: GestionTurnosMedicos

    Un medico con varias especialidades (Medico_Especialidad) normalmente no atiende todas
    en cualquier dia y horario. Se agrega Medico_Horario.IdEspecialidad para indicar con
    que especialidad atiende el medico en esa franja (dia + hora).

    IdEspecialidad NULL = franja sin especialidad puntual: vale para cualquiera de las
    especialidades del medico (comportamiento anterior). Al migrar, si el medico tiene
    UNA sola especialidad se completa automaticamente; si tiene varias se deja NULL
    para que el administrativo las reasigne desde frmGestionMedicos.

    Es idempotente: se puede ejecutar mas de una vez sin error.
*/

USE GestionTurnosMedicos;
GO

IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('dbo.Medico_Horario') AND name = 'IdEspecialidad'
)
BEGIN
    ALTER TABLE dbo.Medico_Horario ADD IdEspecialidad INT NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_MedicoHorario_Especialidad')
BEGIN
    ALTER TABLE dbo.Medico_Horario
        ADD CONSTRAINT FK_MedicoHorario_Especialidad
        FOREIGN KEY (IdEspecialidad) REFERENCES dbo.Especialidades(IdEspecialidad);
END
GO

-- Backfill: medicos con una unica especialidad.
UPDATE mh
SET mh.IdEspecialidad = x.IdEsp
FROM dbo.Medico_Horario mh
INNER JOIN (
    SELECT IdMedico, MIN(IdEsp) AS IdEsp
    FROM dbo.Medico_Especialidad
    GROUP BY IdMedico
    HAVING COUNT(*) = 1
) x ON x.IdMedico = mh.IdMedico
WHERE mh.IdEspecialidad IS NULL;
GO
