CREATE DATABASE cajero;
USE cajero;

CREATE TABLE TipoOperacion (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(255) NOT NULL
);

CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(255) NOT NULL,
    NumeroDeCuenta BIGINT NOT NULL,
    Saldo DECIMAL(18, 2) NOT NULL,
    UltimaExtraccion DATETIME
);

CREATE TABLE Operaciones (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TipoOperacion INT NOT NULL,
    Monto INT NOT NULL,
    FechaOperacion DATETIME NOT NULL,
    IdUsuario INT,
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(Id)
);

CREATE TABLE Tarjetas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NumeroTarjeta BIGINT NOT NULL,
    Pin INT NOT NULL,
    Bloqueada BIT NOT NULL,
    IdUsuario INT,
    IntentosFallidos INT,
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(Id)
);

INSERT INTO Usuarios (Nombre, NumeroDeCuenta, Saldo, UltimaExtraccion)
VALUES ('Julieta', 12341256, 20000.00, '2023-09-08');

INSERT INTO Tarjetas (NumeroTarjeta, Pin, Bloqueada, IdUsuario, IntentosFallidos)
VALUES (1111222233334444, 1234, 0, 1, 0);

INSERT INTO TipoOperacion (Nombre) VALUES ('Depósito');
INSERT INTO TipoOperacion (Nombre) VALUES ('Retiro');

select * from Tarjetas