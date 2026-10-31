IF DB_ID('MedicalDB') IS NULL
    CREATE DATABASE MedicalDB;
GO

USE MedicalDB;
GO

IF OBJECT_ID('dbo.Appointments', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Appointments
    (
        AppointmentId INT IDENTITY(1,1) PRIMARY KEY,
        PatientName NVARCHAR(100),
        Age INT,
        Gender VARCHAR(20),
        DoctorName VARCHAR(100),
        AppointmentDate DATE,
        AppointmentType VARCHAR(50),
        Description NVARCHAR(500),
        AICategory VARCHAR(100)
    );
END
GO