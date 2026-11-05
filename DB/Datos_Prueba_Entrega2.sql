/*
    DATOS DE PRUEBA - Entrega 2 / Proceso 2 (NO es parte de la estructura de la base)
    Base: GestionTurnosMedicos

    Arma una agenda para HOY para la medica de prueba (Camila Lopez, cl@gmail.com) para poder
    probar Atender, Ausente, Finalizar atencion e Historia Clinica (pruebas P-01 a P-14):
      - Si la medica no atiende el dia de la semana de hoy, le crea 6 franjas de 30 minutos
        (08:00, 09:00, 10:00, 11:00, 15:00 y 18:00).
      - Crea un turno Confirmado HOY en cada una de esas franjas (pacientes: los usuarios que
        no son la medica).
      - Crea un turno Confirmado AYER (si la medica atiende ese dia) para probar el vencimiento
        automatico (P-13: solo los de dias anteriores pasan a Ausente).

    Se puede ejecutar todos los dias: solo agrega lo que falta.
*/

USE GestionTurnosMedicos;
GO

-- Requerido por los indices filtrados de Turno / Medico_Horario
SET QUOTED_IDENTIFIER ON;
GO

SET NOCOUNT ON;
SET DATEFIRST 7;   -- domingo = 1, igual que DayOfWeek de .NET + 1

DECLARE @IdMedico INT = (SELECT IdUsuario FROM dbo.Usuario WHERE Email = 'cl@gmail.com');
IF @IdMedico IS NULL
BEGIN
    RAISERROR('No existe la medica de prueba cl@gmail.com', 16, 1);
    RETURN;
END

DECLARE @Hoy DATE = CAST(GETDATE() AS DATE);
DECLARE @Ayer DATE = DATEADD(DAY, -1, @Hoy);
DECLARE @DiaHoy TINYINT = DATEPART(WEEKDAY, @Hoy) - 1;
DECLARE @DiaAyer TINYINT = DATEPART(WEEKDAY, @Ayer) - 1;

-- Roles de los usuarios de prueba: Facundo Perez (fp@gmail.com) es Paciente, para que al entrar
-- con su usuario vea sus turnos y lo que el medico le indico (indicaciones y receta).
INSERT INTO dbo.Usuario_Rol (IdUsuario, IdRol)
SELECT u.IdUsuario, r.IdRol
FROM dbo.Usuario u
JOIN dbo.Rol r ON r.Nombre = 'Paciente' AND r.Formulario IS NULL
WHERE u.Email IN ('fp@gmail.com', 'JB@gmail.com')
  AND NOT EXISTS (SELECT 1 FROM dbo.Usuario_Rol ur WHERE ur.IdUsuario = u.IdUsuario AND ur.IdRol = r.IdRol);

-- Matricula de prueba de la medica (se puede cambiar en Gestion de Medicos)
IF OBJECT_ID('dbo.Medico') IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.Medico WHERE IdUsuario = @IdMedico)
    INSERT INTO dbo.Medico (IdUsuario, Matricula) VALUES (@IdMedico, 'MN 123456');

-- Franjas de hoy (solo si la medica no atiende hoy)
IF NOT EXISTS (SELECT 1 FROM dbo.Medico_Horario WHERE IdMedico = @IdMedico AND DiaSemana = @DiaHoy AND Activo = 1)
BEGIN
    INSERT INTO dbo.Medico_Horario (IdMedico, DiaSemana, HoraInicio, HoraFin, Activo)
    VALUES (@IdMedico, @DiaHoy, '08:00', '08:30', 1),
           (@IdMedico, @DiaHoy, '09:00', '09:30', 1),
           (@IdMedico, @DiaHoy, '10:00', '10:30', 1),
           (@IdMedico, @DiaHoy, '11:00', '11:30', 1),
           (@IdMedico, @DiaHoy, '15:00', '15:30', 1),
           (@IdMedico, @DiaHoy, '18:00', '18:30', 1);
END

-- Pacientes de prueba: todos los usuarios con Historia Clinica menos la medica
DECLARE @Pacientes TABLE (Orden INT IDENTITY(0,1), IdUsuario INT);
INSERT INTO @Pacientes (IdUsuario)
SELECT p.IdUsuario FROM dbo.Paciente p WHERE p.IdUsuario <> @IdMedico ORDER BY p.IdUsuario;
DECLARE @CantPacientes INT = (SELECT COUNT(*) FROM @Pacientes);

-- Un turno hoy por franja (hasta 6), rotando los pacientes
;WITH Franjas AS (
    SELECT TOP 6 mh.IdHorario, ROW_NUMBER() OVER (ORDER BY mh.HoraInicio) - 1 AS Orden
    FROM dbo.Medico_Horario mh
    WHERE mh.IdMedico = @IdMedico AND mh.DiaSemana = @DiaHoy AND mh.Activo = 1
    ORDER BY mh.HoraInicio
)
INSERT INTO dbo.Turno (IdPaciente, IdHorario, Fecha, Estado)
SELECT pa.IdUsuario, f.IdHorario, @Hoy, 'Confirmado'
FROM Franjas f
JOIN @Pacientes pa ON pa.Orden = f.Orden % @CantPacientes
WHERE NOT EXISTS (SELECT 1 FROM dbo.Turno t WHERE t.IdHorario = f.IdHorario AND t.Fecha = @Hoy);

-- Un turno ayer (para probar el vencimiento automatico)
DECLARE @HorarioAyer INT = (SELECT TOP 1 IdHorario FROM dbo.Medico_Horario
                            WHERE IdMedico = @IdMedico AND DiaSemana = @DiaAyer AND Activo = 1 ORDER BY HoraInicio);
IF @HorarioAyer IS NOT NULL AND @CantPacientes > 0
   AND NOT EXISTS (SELECT 1 FROM dbo.Turno WHERE IdHorario = @HorarioAyer AND Fecha = @Ayer)
    INSERT INTO dbo.Turno (IdPaciente, IdHorario, Fecha, Estado)
    SELECT IdUsuario, @HorarioAyer, @Ayer, 'Confirmado' FROM @Pacientes WHERE Orden = 0;

-- Resultado
SELECT t.IdTurno, t.Fecha, CONVERT(VARCHAR(5), mh.HoraInicio, 108) AS Hora,
       u.Nombre + ' ' + u.Apellido AS Paciente, p.NroHistoriaClinica AS HC, t.Estado
FROM dbo.Turno t
JOIN dbo.Medico_Horario mh ON mh.IdHorario = t.IdHorario
JOIN dbo.Usuario u ON u.IdUsuario = t.IdPaciente
LEFT JOIN dbo.Paciente p ON p.IdUsuario = t.IdPaciente
WHERE mh.IdMedico = @IdMedico AND t.Fecha IN (@Hoy, @Ayer)
ORDER BY t.Fecha, mh.HoraInicio;
GO
