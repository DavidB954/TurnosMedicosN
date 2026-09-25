/*
    Script de actualizacion - Gestion de Horarios de Medicos
    Base: GestionTurnosMedicos

    Crea la tabla Medico_Horario, que guarda la franja horaria (dia + hora de inicio/fin)
    en la que cada medico atiende. Se genera automaticamente desde frmGestionMedicos a partir
    de una franja horaria (Desde/Hasta/Turnos cada X min), dejando 1 hora de descanso entre
    las 12:00 y las 13:00. El bit Activo permite desactivar/reactivar un horario puntual sin
    borrar el registro (por ejemplo, si se cancela un turno y ese horario vuelve a quedar
    disponible). Es idempotente: se puede ejecutar mas de una vez sin error.
*/

USE GestionTurnosMedicos;
GO

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Medico_Horario')
BEGIN
    CREATE TABLE dbo.Medico_Horario (
        IdHorario INT IDENTITY(1,1) PRIMARY KEY,
        IdMedico INT NOT NULL,
        DiaSemana TINYINT NOT NULL, -- 1=Lunes, 2=Martes, 3=Miercoles, 4=Jueves, 5=Viernes, 6=Sabado
        HoraInicio TIME(0) NOT NULL,
        HoraFin TIME(0) NOT NULL,
        Activo BIT NOT NULL CONSTRAINT DF_MedicoHorario_Activo DEFAULT (1),
        CONSTRAINT FK_MedicoHorario_Usuario FOREIGN KEY (IdMedico) REFERENCES dbo.Usuario(IdUsuario),
        CONSTRAINT UQ_MedicoHorario UNIQUE (IdMedico, DiaSemana, HoraInicio),
        CONSTRAINT CK_MedicoHorario_Dia CHECK (DiaSemana BETWEEN 1 AND 6),
        CONSTRAINT CK_MedicoHorario_Horas CHECK (HoraFin > HoraInicio)
    );
END
GO
