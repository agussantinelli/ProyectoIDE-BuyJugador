
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'BuyJugador')
BEGIN
    CREATE DATABASE BuyJugador;
END
GO

USE BuyJugador;
GO


IF OBJECT_ID('dbo.Precios', 'U') IS NOT NULL DROP TABLE dbo.Precios;
IF OBJECT_ID('dbo.LineaVentas', 'U') IS NOT NULL DROP TABLE dbo.LineaVentas;
IF OBJECT_ID('dbo.LineaPedidos', 'U') IS NOT NULL DROP TABLE dbo.LineaPedidos;
IF OBJECT_ID('dbo.Ventas', 'U') IS NOT NULL DROP TABLE dbo.Ventas;
IF OBJECT_ID('dbo.Pedidos', 'U') IS NOT NULL DROP TABLE dbo.Pedidos;
IF OBJECT_ID('dbo.Productos', 'U') IS NOT NULL DROP TABLE dbo.Productos;
IF OBJECT_ID('dbo.TipoProductos', 'U') IS NOT NULL DROP TABLE dbo.TipoProductos;
IF OBJECT_ID('dbo.Proveedores', 'U') IS NOT NULL DROP TABLE dbo.Proveedores;
IF OBJECT_ID('dbo.Duenios', 'U') IS NOT NULL DROP TABLE dbo.Duenios;
IF OBJECT_ID('dbo.Empleados', 'U') IS NOT NULL DROP TABLE dbo.Empleados;
IF OBJECT_ID('dbo.Localidades', 'U') IS NOT NULL DROP TABLE dbo.Localidades;
IF OBJECT_ID('dbo.Provincias', 'U') IS NOT NULL DROP TABLE dbo.Provincias;
GO


CREATE TABLE dbo.Provincias (
    Id INT PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL
);

CREATE TABLE dbo.Localidades (
    Id INT PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    ProvinciaId INT FOREIGN KEY REFERENCES Provincias(Id)
);

CREATE TABLE dbo.TipoProductos (
    Id INT PRIMARY KEY,
    Descripcion NVARCHAR(255) NOT NULL
);

CREATE TABLE dbo.Proveedores (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Cuit NVARCHAR(20) NOT NULL,
    RazonSocial NVARCHAR(200) NOT NULL,
    Telefono NVARCHAR(50),
    Email NVARCHAR(100)
);

CREATE TABLE dbo.Productos (
    Id INT PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255),
    Stock INT NOT NULL,
    TipoProductoId INT FOREIGN KEY REFERENCES TipoProductos(Id)
);

CREATE TABLE dbo.Precios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Monto DECIMAL(18, 2) NOT NULL,
    FechaDesde DATETIME NOT NULL,
    ProductoId INT FOREIGN KEY REFERENCES Productos(Id)
);

CREATE TABLE dbo.Duenios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    DNI INT NOT NULL UNIQUE,
    NombreCompleto NVARCHAR(200),
    Email NVARCHAR(100),
    Password NVARCHAR(100),
    Telefono NVARCHAR(50)
);

CREATE TABLE dbo.Empleados (
    Id INT PRIMARY KEY IDENTITY(1,1),
    DNI INT NOT NULL UNIQUE,
    NombreCompleto NVARCHAR(200),
    Email NVARCHAR(100),
    Password NVARCHAR(100),
    Telefono NVARCHAR(50),
    FechaAlta DATE
);

INSERT INTO dbo.Provincias (Id, Nombre) VALUES
(1, 'Ciudad Autónoma de Buenos Aires'),
(2, 'Buenos Aires'),
(3, 'Catamarca'),
(4, 'Chaco'),
(5, 'Chubut'),
(6, 'Córdoba'),
(7, 'Corrientes'),
(8, 'Entre Ríos'),
(9, 'Formosa'),
(10, 'Jujuy'),
(11, 'La Pampa'),
(12, 'La Rioja'),
(13, 'Mendoza'),
(14, 'Misiones'),
(15, 'Neuquén'),
(16, 'Río Negro'),
(17, 'Salta'),
(18, 'San Juan'),
(19, 'San Luis'),
(20, 'Santa Fe'),
(21, 'Santa Cruz'),
(22, 'Santiago del Estero'),
(23, 'Tierra del Fuego, Antártida e Islas del Atlántico Sur'),
(24, 'Tucumán');

INSERT INTO dbo.Localidades (Id, Nombre, ProvinciaId) VALUES
(101, 'Rosario', 20),
(102, 'Santa Fe', 20),
(103, 'Funes', 20),
(104, 'Villa Gobernador Galvez', 20),
(201, 'Córdoba Capital', 6),
(202, 'Villa Carlos Paz', 6),
(301, 'La Plata', 2),
(302, 'La Matanza', 2),
(303, 'Florencio Varela', 2),
(304, 'San Nicolas', 2);

INSERT INTO dbo.TipoProductos (Id, Descripcion) VALUES
(1, 'Componentes'),
(2, 'Monitores'),
(3, 'Parlantes');

INSERT INTO dbo.Proveedores (Cuit, RazonSocial, Telefono, Email) VALUES
('30-12345678-9', 'Distrito Digital S.A.', '3416667788', 'compras@distritodigital.com'),
('30-98765432-1', 'Logística Computacional S.R.L.', '116665544', 'ventas@logisticacompsrl.com');

INSERT INTO dbo.Productos (Id, Nombre, Descripcion, Stock, TipoProductoId) VALUES
(100, 'MotherBoard Ryzen 5.0', 'MotherBoard Ryzen 5.0 Mother Asus', 150, 1),
(200, 'Monitor Curvo TLC', 'Monitor Curvo 20°', 200, 2),
(201, 'Monitor Samsung', 'Monitor HD Open-View', 180, 2),
(300, 'Parlante Huge HBL', 'Parlante Sonido Envolvente', 120, 3);

INSERT INTO dbo.Precios (ProductoId, Monto, FechaDesde) VALUES
(100, 1500.00, '2024-01-01'),
(100, 1750.50, '2024-06-15'),
(200, 800.00, '2024-03-01'),
(201, 1200.75, '2024-05-20'),
(300, 950.00, '2024-02-10');

INSERT INTO dbo.Duenios (DNI, NombreCompleto, Email, Password, Telefono) VALUES
(25123456, 'Martin Ratti', 'marto666@empresa.com', 'pass123', '3415550101'),
(30789123, 'Joaquin Peralta', 'rojopasion@empresa.com', 'pass456', '115550202');

INSERT INTO dbo.Empleados (DNI, NombreCompleto, Email, Password, Telefono, FechaAlta) VALUES
(40111222, 'Frank Fabra', 'frank@email.com', 'empleado1', '3415551111', '2023-05-15'),
(42333444, 'Ayrton Costa', 'ayrton@email.com', 'empleado2', '3415552222', '2024-01-20'),
(42553400, 'Luka Doncic', 'doncic@email.com', 'empleado3', '3415882922', '2014-03-06'),
(32393404, 'Stephen Curry', 'curry@email.com', 'empleado4', '3415559202', '2009-01-07');

GO
PRINT '¡Proceso completado! La base de datos BuyJugador está lista con los datos correctos.';
GO