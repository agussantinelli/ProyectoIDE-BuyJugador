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
IF OBJECT_ID('dbo.Personas', 'U') IS NOT NULL DROP TABLE dbo.Personas;
IF OBJECT_ID('dbo.TiposProducto', 'U') IS NOT NULL DROP TABLE dbo.TiposProducto;
IF OBJECT_ID('dbo.Proveedores', 'U') IS NOT NULL DROP TABLE dbo.Proveedores;
IF OBJECT_ID('dbo.Localidades', 'U') IS NOT NULL DROP TABLE dbo.Localidades;
IF OBJECT_ID('dbo.Provincias', 'U') IS NOT NULL DROP TABLE dbo.Provincias;
GO

CREATE TABLE dbo.Provincias (
    IdProvincia INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL
);

CREATE TABLE dbo.Localidades (
    IdLocalidad INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    IdProvincia INT FOREIGN KEY REFERENCES Provincias(IdProvincia)
);

CREATE TABLE dbo.TiposProducto (
    IdTipoProducto INT PRIMARY KEY IDENTITY(1,1),
    Descripcion NVARCHAR(255) NOT NULL
);

CREATE TABLE dbo.Proveedores (
    IdProveedor INT PRIMARY KEY IDENTITY(1,1),
    RazonSocial NVARCHAR(200) NOT NULL,
    Cuit NVARCHAR(20) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Telefono NVARCHAR(50) NOT NULL,
    Direccion NVARCHAR(200) NOT NULL,
    IdLocalidad INT FOREIGN KEY REFERENCES Localidades(IdLocalidad)
);

CREATE TABLE dbo.Productos (
    IdProducto INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255) NOT NULL,
    Stock INT NOT NULL,
    IdTipoProducto INT FOREIGN KEY REFERENCES TiposProducto(IdTipoProducto)
);

CREATE TABLE dbo.Precios (
    Monto DECIMAL(18, 2) NOT NULL,
    FechaDesde DATETIME NOT NULL,
    IdProducto INT FOREIGN KEY REFERENCES Productos(IdProducto),
    PRIMARY KEY (IdProducto, FechaDesde)
);

CREATE TABLE dbo.Personas (
    IdPersona INT PRIMARY KEY IDENTITY(1,1),
    NombreCompleto NVARCHAR(200),
    DNI INT NOT NULL UNIQUE,
    Email NVARCHAR(100) NOT NULL,
    Password VARBINARY(64) NOT NULL,
    Telefono NVARCHAR(50) NOT NULL,
    Direccion NVARCHAR(200) NOT NULL,
    IdLocalidad INT FOREIGN KEY REFERENCES Localidades(IdLocalidad),
    FechaIngreso DATE NULL
);

CREATE TABLE dbo.Ventas (
    IdVenta INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATETIME NOT NULL,
    Estado NVARCHAR(50) NOT NULL,
    IdPersona INT FOREIGN KEY REFERENCES Personas(IdPersona)
);

CREATE TABLE dbo.Pedidos (
    IdPedido INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATETIME NOT NULL,
    Estado NVARCHAR(50) NOT NULL,
    IdProveedor INT FOREIGN KEY REFERENCES Proveedores(IdProveedor)
);

CREATE TABLE dbo.LineaPedidos (
    Cantidad INT NOT NULL,
    IdPedido INT NOT NULL FOREIGN KEY REFERENCES Pedidos(IdPedido),
    IdProducto INT FOREIGN KEY REFERENCES Productos(IdProducto),
    NroLineaPedido INT NOT NULL,
    PRIMARY KEY (IdPedido, NroLineaPedido)
);

CREATE TABLE dbo.LineaVentas (
    Cantidad INT NOT NULL,
    IdVenta INT NOT NULL FOREIGN KEY REFERENCES Ventas(IdVenta),
    IdProducto INT FOREIGN KEY REFERENCES Productos(IdProducto),
    NroLineaVenta INT NOT NULL,
    PRIMARY KEY (IdVenta, NroLineaVenta)
);

INSERT INTO dbo.Provincias (Nombre) VALUES
('Ciudad Autónoma de Buenos Aires'),
('Buenos Aires'),
('Catamarca'),
('Chaco'),
('Chubut'),
('Córdoba'),
('Corrientes'),
('Entre Ríos'),
('Formosa'),
('Jujuy'),
('La Pampa'),
('La Rioja'),
('Mendoza'),
('Misiones'),
('Neuquén'),
('Río Negro'),
('Salta'),
('San Juan'),
('San Luis'),
('Santa Fe'),
('Santa Cruz'),
('Santiago del Estero'),
('Tierra del Fuego, Antártida e Islas del Atlántico Sur'),
('Tucumán');

INSERT INTO dbo.Localidades (Nombre, IdProvincia) VALUES
('Rosario', (SELECT IdProvincia FROM dbo.Provincias WHERE Nombre = 'Santa Fe')),
('Santa Fe', (SELECT IdProvincia FROM dbo.Provincias WHERE Nombre = 'Santa Fe')),
('Funes', (SELECT IdProvincia FROM dbo.Provincias WHERE Nombre = 'Santa Fe')),
('Villa Gobernador Galvez', (SELECT IdProvincia FROM dbo.Provincias WHERE Nombre = 'Santa Fe')),
('Córdoba Capital', (SELECT IdProvincia FROM dbo.Provincias WHERE Nombre = 'Córdoba')),
('Villa Carlos Paz', (SELECT IdProvincia FROM dbo.Provincias WHERE Nombre = 'Córdoba')),
('La Plata', (SELECT IdProvincia FROM dbo.Provincias WHERE Nombre = 'Buenos Aires')),
('La Matanza', (SELECT IdProvincia FROM dbo.Provincias WHERE Nombre = 'Buenos Aires')),
('Florencio Varela', (SELECT IdProvincia FROM dbo.Provincias WHERE Nombre = 'Buenos Aires')),
('San Nicolas', (SELECT IdProvincia FROM dbo.Provincias WHERE Nombre = 'Buenos Aires'));

INSERT INTO dbo.TiposProducto (Descripcion) VALUES
('Componentes'),
('Monitores'),
('Parlantes');

INSERT INTO dbo.Proveedores (RazonSocial, Cuit, Telefono, Email, Direccion, IdLocalidad) VALUES
('Distrito Digital S.A.', '30-12345678-9', '3416667788', 'compras@distritodigital.com', 'Calle Falsa 123', (SELECT IdLocalidad FROM dbo.Localidades WHERE Nombre = 'Rosario')),
('Logística Computacional S.R.L.', '30-98765432-1', '116665544', 'ventas@logisticacompsrl.com', 'Avenida Siempre Viva 742', (SELECT IdLocalidad FROM dbo.Localidades WHERE Nombre = 'Córdoba Capital'));

INSERT INTO dbo.Productos (Nombre, Descripcion, Stock, IdTipoProducto) VALUES
('MotherBoard Ryzen 5.0', 'MotherBoard Ryzen 5.0 Mother Asus', 150, (SELECT IdTipoProducto FROM dbo.TiposProducto WHERE Descripcion = 'Componentes')),
('Monitor Curvo TLC', 'Monitor Curvo 20°', 200, (SELECT IdTipoProducto FROM dbo.TiposProducto WHERE Descripcion = 'Monitores')),
('Monitor Samsung', 'Monitor HD Open-View', 180, (SELECT IdTipoProducto FROM dbo.TiposProducto WHERE Descripcion = 'Monitores')),
('Parlante Huge HBL', 'Parlante Sonido Envolvente', 120, (SELECT IdTipoProducto FROM dbo.TiposProducto WHERE Descripcion = 'Parlantes'));

INSERT INTO dbo.Precios (IdProducto, Monto, FechaDesde) VALUES
((SELECT IdProducto FROM dbo.Productos WHERE Nombre = 'MotherBoard Ryzen 5.0'), 1500.00, '2024-01-01'),
((SELECT IdProducto FROM dbo.Productos WHERE Nombre = 'MotherBoard Ryzen 5.0'), 1750.50, '2024-06-15'),
((SELECT IdProducto FROM dbo.Productos WHERE Nombre = 'Monitor Curvo TLC'), 800.00, '2024-03-01'),
((SELECT IdProducto FROM dbo.Productos WHERE Nombre = 'Monitor Samsung'), 1200.75, '2024-05-20'),
((SELECT IdProducto FROM dbo.Productos WHERE Nombre = 'Parlante Huge HBL'), 950.00, '2024-02-10');

INSERT INTO dbo.Personas (NombreCompleto, DNI, Email, Password, Telefono, Direccion, IdLocalidad, FechaIngreso) VALUES
('Martin Ratti', 25123456, 'marto666@empresa.com', HASHBYTES('SHA2_512', 'pass123'), '3415550101', 'Calle Ficticia 1', (SELECT IdLocalidad FROM dbo.Localidades WHERE Nombre = 'Rosario'), NULL),
('Joaquin Peralta', 30789123, 'rojopasion@empresa.com', HASHBYTES('SHA2_512', 'pass456'), '115550202', 'Avenida Imaginaria 2', (SELECT IdLocalidad FROM dbo.Localidades WHERE Nombre = 'La Plata'), NULL),
('Frank Fabra', 40111222, 'frank@email.com', HASHBYTES('SHA2_512', 'empleado1'), '3415551111', 'Calle Ejemplo 3', (SELECT IdLocalidad FROM dbo.Localidades WHERE Nombre = 'Rosario'), '2023-05-15'),
('Ayrton Costa', 42333444, 'ayrton@email.com', HASHBYTES('SHA2_512', 'empleado2'), '3415552222', 'Calle Demo 4', (SELECT IdLocalidad FROM dbo.Localidades WHERE Nombre = 'Córdoba Capital'), '2024-01-20'),
('Luka Doncic', 42553400, 'doncic@email.com', HASHBYTES('SHA2_512', 'empleado3'), '3415882922', 'Calle Prueba 5', (SELECT IdLocalidad FROM dbo.Localidades WHERE Nombre = 'La Matanza'), '2014-03-06'),
('Stephen Curry', 32393404, 'curry@email.com', HASHBYTES('SHA2_512', 'empleado4'), '3415559202', 'Calle Test 6', (SELECT IdLocalidad FROM dbo.Localidades WHERE Nombre = 'San Nicolas'), '2009-01-07');

INSERT INTO dbo.Ventas (Fecha, Estado, IdPersona) VALUES
('2024-06-20 11:00:00', 'Pagada', (SELECT IdPersona FROM dbo.Personas WHERE DNI = 40111222)), 
('2024-06-21 15:30:00', 'Pagada', (SELECT IdPersona FROM dbo.Personas WHERE DNI = 42333444)), 
('2024-06-21 16:45:00', 'Pendiente', (SELECT IdPersona FROM dbo.Personas WHERE DNI = 42553400)), 
('2024-06-22 09:15:00', 'Pagada', (SELECT IdPersona FROM dbo.Personas WHERE DNI = 32393404)); 

INSERT INTO dbo.Pedidos (Fecha, Estado, IdProveedor) VALUES
('2024-06-25 10:00:00', 'En proceso', (SELECT IdProveedor FROM dbo.Proveedores WHERE RazonSocial = 'Distrito Digital S.A.')),
('2024-06-26 14:30:00', 'Entregado', (SELECT IdProveedor FROM dbo.Proveedores WHERE RazonSocial = 'Logística Computacional S.R.L.'));

GO
