USE [Ejemplo2];
GO

IF OBJECT_ID(N'dbo.Persona', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Persona
    (
        PersonaID VARCHAR(50) NOT NULL
            CONSTRAINT PK_Persona PRIMARY KEY,
        Nombre VARCHAR(50) NOT NULL,
        Tipo TINYINT NOT NULL,
        Gender VARCHAR(10) NOT NULL,
        Password VARCHAR(100) NOT NULL
    );

    CREATE INDEX IX_Persona_Nombre
        ON dbo.Persona (Nombre);
END;
GO
