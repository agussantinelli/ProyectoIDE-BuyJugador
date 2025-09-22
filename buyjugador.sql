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

INSERT INTO dbo.Localidades (Nombre, IdProvincia) VALUES
-- Ciudad Autónoma de Buenos Aires (20 localidades)
('Palermo', 1), ('Recoleta', 1), ('Belgrano', 1), ('Puerto Madero', 1), ('San Telmo', 1),
('Caballito', 1), ('Almagro', 1), ('Flores', 1), ('Villa Urquiza', 1), ('Villa Devoto', 1),
('Núñez', 1), ('Barracas', 1), ('Boedo', 1), ('Colegiales', 1), ('Parque Patricios', 1),
('Villa Crespo', 1), ('Mataderos', 1), ('Liniers', 1), ('Villa Lugano', 1), ('Parque Chas', 1),

-- Buenos Aires (150 localidades)
('Quilmes', 2), ('Lanús', 2), ('Avellaneda', 2), ('Morón', 2), ('San Isidro', 2),
('Tigre', 2), ('San Fernando', 2), ('Vicente López', 2), ('Tres de Febrero', 2), ('San Martín', 2),
('José C. Paz', 2), ('Malvinas Argentinas', 2), ('Hurlingham', 2), ('Ituzaingó', 2), ('Merlo', 2),
('Moreno', 2), ('Ezeiza', 2), ('Esteban Echeverría', 2), ('Almirante Brown', 2), ('Berazategui', 2),
('Florencio Varela', 2), ('Presidente Perón', 2), ('San Vicente', 2), ('Cañuelas', 2), ('Marcos Paz', 2),
('General Rodríguez', 2), ('Luján', 2), ('Pilar', 2), ('Escobar', 2), ('Campana', 2),
('Zárate', 2), ('Exaltación de la Cruz', 2), ('San Antonio de Areco', 2), ('Baradero', 2), ('Arrecifes', 2),
('Capitán Sarmiento', 2), ('Salto', 2), ('Pergamino', 2), ('Colón', 2), ('Rojas', 2),
('Chacabuco', 2), ('Junín', 2), ('General Viamonte', 2), ('Lincoln', 2), ('General Pinto', 2),
('Leandro N. Alem', 2), ('General Arenales', 2), ('Rivadavia', 2), ('Trenque Lauquen', 2), ('Tres Lomas', 2),
('Carlos Casares', 2), ('Carlos Tejedor', 2), ('9 de Julio', 2), ('Pehuajó', 2), ('Guaminí', 2),
('Salliqueló', 2), ('Daireaux', 2), ('Pellegrini', 2), ('Trenel', 2), ('Quemú Quemú', 2),
('Realicó', 2), ('Eduardo Castex', 2), ('Conhelo', 2), ('Maracó', 2), ('Rancul', 2),
('Telen', 2), ('Catriló', 2), ('Uriburu', 2), ('Atreucó', 2), ('Chapaleufú', 2),
('Quemú Quemú', 2), ('Realicó', 2), ('Eduardo Castex', 2), ('Conhelo', 2), ('Maracó', 2),
('Rancul', 2), ('Telen', 2), ('Catriló', 2), ('Uriburu', 2), ('Atreucó', 2),
('Chapaleufú', 2), ('Trenel', 2), ('Quemú Quemú', 2), ('Realicó', 2), ('Eduardo Castex', 2),
('Conhelo', 2), ('Maracó', 2), ('Rancul', 2), ('Telen', 2), ('Catriló', 2),
('Uriburu', 2), ('Atreucó', 2), ('Chapaleufú', 2), ('Trenel', 2), ('Quemú Quemú', 2),
('Realicó', 2), ('Eduardo Castex', 2), ('Conhelo', 2), ('Maracó', 2), ('Rancul', 2),
('Telen', 2), ('Catriló', 2), ('Uriburu', 2), ('Atreucó', 2), ('Chapaleufú', 2),
('Adolfo Gonzales Chaves', 2), ('Benito Juárez', 2), ('Laprida', 2), ('Coronel Dorrego', 2), ('Coronel Pringles', 2),
('Coronel Suárez', 2), ('Daireaux', 2), ('Guaminí', 2), ('Hipólito Yrigoyen', 2), ('Monte Hermoso', 2),
('Puan', 2), ('Saavedra', 2), ('Tornquist', 2), ('Tres Arroyos', 2), ('Villarino', 2),
('Bahía Blanca', 2), ('Patagones', 2), ('Carmen de Patagones', 2), ('Viedma', 2), ('San Clemente del Tuyú', 2),
('Santa Teresita', 2), ('Mar del Tuyú', 2), ('Costa del Este', 2), ('Aguas Verdes', 2), ('La Lucila del Mar', 2),
('San Bernardo', 2), ('Mar de Ajó', 2), ('Pinar del Sol', 2), ('Costa Azul', 2), ('Marisol', 2),

-- Catamarca (30 localidades)
('San Fernando del Valle de Catamarca', 3), ('Valle Viejo', 3), ('Fray Mamerto Esquiú', 3), ('Ambato', 3), ('Paclín', 3),
('Santa María', 3), ('Andalgalá', 3), ('Belén', 3), ('Tinogasta', 3), ('Capayán', 3),
('Ancasti', 3), ('El Alto', 3), ('La Paz', 3), ('Santa Rosa', 3), ('Antofagasta de la Sierra', 3),
('Fiambalá', 3), ('Pomán', 3), ('Corral Quemado', 3), ('Hualfín', 3), ('Londres', 3),
('Mutquín', 3), ('Saujil', 3), ('Villa Vil', 3), ('El Rodeo', 3), ('Las Juntas', 3),
('La Merced', 3), ('San Antonio', 3), ('Collagasta', 3), ('Pozo de la Piedra', 3), ('Villa de Balcozna', 3),

-- Chaco (40 localidades)
('Resistencia', 4), ('Barranqueras', 4), ('Fontana', 4), ('Puerto Vilelas', 4), ('Presidencia Roque Sáenz Peña', 4),
('Charata', 4), ('Villa Ángela', 4), ('General San Martín', 4), ('Quitilipi', 4), ('Machagai', 4),
('Las Breñas', 4), ('La Leonesa', 4), ('Puerto Tirol', 4), ('Castelli', 4), ('Corzuela', 4),
('Gancedo', 4), ('General Pinedo', 4), ('Hermoso Campo', 4), ('Juan José Castelli', 4), ('La Clotilde', 4),
('La Escondida', 4), ('La Verde', 4), ('Laguna Blanca', 4), ('Lapachito', 4), ('Las Palmas', 4),
('Napenay', 4), ('Pampa del Infierno', 4), ('Puerto Bermejo', 4), ('Puerto Eva Perón', 4), ('San Bernardo', 4),
('Santa Sylvina', 4), ('Taco Pozo', 4), ('Tres Isletas', 4), ('Venado Grande', 4), ('Villa Berthet', 4),
('Colonia Aborigen', 4), ('Colonia Elisa', 4), ('Colonia Popular', 4), ('Cote Lai', 4), ('El Sauzalito', 4),

-- Chubut (35 localidades)
('Rawson', 5), ('Trelew', 5), ('Puerto Madryn', 5), ('Comodoro Rivadavia', 5), ('Esquel', 5),
('Sarmiento', 5), ('Rada Tilly', 5), ('Gaiman', 5), ('Dolavon', 5), ('28 de Julio', 5),
('Alto Río Senguer', 5), ('Camarones', 5), ('Corcovado', 5), ('El Hoyo', 5), ('El Maitén', 5),
('Epuyén', 5), ('Gobernador Costa', 5), ('Hoyo de Epuyén', 5), ('José de San Martín', 5), ('Lago Puelo', 5),
('Las Plumas', 5), ('Paso de Indios', 5), ('Paso del Sapo', 5), ('Playa Unión', 5), ('Puerto Pirámides', 5),
('Río Mayo', 5), ('Río Pico', 5), ('Tecka', 5), ('Telsen', 5), ('Trevelin', 5),
('Villa Futalaufquen', 5), ('Villa Situación', 5), ('Aldea Epulef', 5), ('Cushamen', 5), ('Gan Gan', 5),

-- Córdoba (100 localidades)
('Alta Gracia', 6), ('Villa María', 6), ('Río Cuarto', 6), ('San Francisco', 6), ('Villa Carlos Paz', 6),
('Jesús María', 6), ('Cosquín', 6), ('La Falda', 6), ('Capilla del Monte', 6), ('Cruz del Eje', 6),
('Deán Funes', 6), ('Villa Dolores', 6), ('Minas', 6), ('Salsacate', 6), ('Santiago Temple', 6),
('Arroyito', 6), ('Bell Ville', 6), ('Marcos Juárez', 6), ('Monte Maíz', 6), ('Río Tercero', 6),
('Embalse', 6), ('La Calera', 6), ('Malagueño', 6), ('Mendiolaza', 6), ('Río Ceballos', 6),
('Unquillo', 6), ('Agua de Oro', 6), ('Ascochinga', 6), ('Cabalango', 6), ('Casa Grande', 6),
('Charbonier', 6), ('Copacabana', 6), ('Cuesta Blanca', 6), ('Estación General Paz', 6), ('La Cumbre', 6),
('La Granja', 6), ('La Paisanita', 6), ('La Rancherita', 6), ('Los Cocos', 6), ('Mallín', 6),
('Mayu Sumaj', 6), ('Mina Clavero', 6), ('Non', 6), ('Panaholma', 6), ('San Antonio de Arredondo', 6),
('San Esteban', 6), ('San Marcos Sierras', 6), ('Santa Rosa de Calamuchita', 6), ('Tala Cañada', 6), ('Tanti', 6),
('Valle Hermoso', 6), ('Villa Allende', 6), ('Villa Ascasubi', 6), ('Villa Candelaria', 6), ('Villa del Dique', 6),
('Villa del Rosario', 6), ('Villa del Totoral', 6), ('Villa Fontana', 6), ('Villa General Belgrano', 6), ('Villa Giardino', 6),
('Villa Lago Azul', 6), ('Villa María', 6), ('Villa Nueva', 6), ('Villa Parque', 6), ('Villa Quillinzo', 6),
('Villa Reducción', 6), ('Villa Rumipal', 6), ('Villa San Esteban', 6), ('Villa San Isidro', 6), ('Villa Santa Cruz', 6),
('Villa Sarmiento', 6), ('Villa Tulumba', 6), ('Villa Valeria', 6), ('Villa Yacanto', 6), ('Wenceslao', 6),
('Achiras', 6), ('Adelia María', 6), ('Alcira', 6), ('Alejandro Roca', 6), ('Almafuerte', 6),
('Alpa Corral', 6), ('Alto Alegre', 6), ('Arias', 6), ('Assunta', 6), ('Atahona', 6),
('Ausonia', 6), ('Ballesteros', 6), ('Ballesteros Sud', 6), ('Bengolea', 6), ('Benjamín Gould', 6),
('Berrotarán', 6), ('Bialet Massé', 6), ('Bouwer', 6), ('Brinkmann', 6), ('Camilo Aldao', 6),

-- Corrientes (45 localidades)
('Corrientes', 7), ('Goya', 7), ('Mercedes', 7), ('Curuzú Cuatiá', 7), ('Paso de los Libres', 7),
('Monte Caseros', 7), ('Bella Vista', 7), ('Saladas', 7), ('Ituzaingó', 7), ('Empedrado', 7),
('Santo Tomé', 7), ('Gobernador Virasoro', 7), ('San Luis del Palmar', 7), ('Santa Lucía', 7), ('San Roque', 7),
('Concepción', 7), ('Mburucuyá', 7), ('Lavalle', 7), ('Itatí', 7), ('Berón de Astrada', 7),
('Yapeyú', 7), ('La Cruz', 7), ('Alvear', 7), ('Pueblo Libertador', 7), ('Colonia Liebig', 7),
('Garruchos', 7), ('Gobernador Martínez', 7), ('Herlitzka', 7), ('Juan Pujol', 7), ('Lomas de Vallejos', 7),
('Loreto', 7), ('Mariano I. Loza', 7), ('Nuestra Señora del Rosario', 7), ('Palmar Grande', 7), ('Perugorría', 7),
('Rincón', 7), ('San Carlos', 7), ('San Cosme', 7), ('San Miguel', 7), ('Tapebicuá', 7),
('Villa Olivari', 7), ('Wanda', 7), ('Yahapé', 7), ('9 de Julio', 7), ('Chavarría', 7),

-- Entre Ríos (60 localidades)
('Paraná', 8), ('Concordia', 8), ('Gualeguaychú', 8), ('Concepción del Uruguay', 8), ('Gualeguay', 8),
('Victoria', 8), ('Villaguay', 8), ('Nogoyá', 8), ('Diamante', 8), ('Federación', 8),
('Chajarí', 8), ('La Paz', 8), ('San José de Feliciano', 8), ('Colón', 8), ('Ubajay', 8),
('Villa Elisa', 8), ('Hasenkamp', 8), ('General Campos', 8), ('General Galarza', 8), ('General Ramírez', 8),
('Gilbert', 8), ('Gobernador Mansilla', 8), ('Gobernador Solá', 8), ('Hernandarias', 8), ('Hernández', 8),
('Ibicuy', 8), ('Irazusta', 8), ('Jubileo', 8), ('La Clarita', 8), ('Larroque', 8),
('Libertador San Martín', 8), ('Los Charrúas', 8), ('Los Conquistadores', 8), ('Macia', 8), ('Mansilla', 8),
('María Grande', 8), ('Molino Doll', 8), ('Nueva Escocia', 8), ('Oro Verde', 8), ('Piedras Blancas', 8),
('Pronunciamiento', 8), ('Pueblo Brugo', 8), ('Pueblo General Paz', 8), ('Puerto Yeruá', 8), ('Rosario del Tala', 8),
('San Benito', 8), ('San Gustavo', 8), ('San Jaime', 8), ('San Justo', 8), ('San Pedro', 8),
('San Salvador', 8), ('Santa Anita', 8), ('Santa Elena', 8), ('Seguí', 8), ('Tabossi', 8),
('Tezanos Pinto', 8), ('Viale', 8), ('Villa Fontana', 8), ('Villa Paranacito', 8), ('Villa Urquiza', 8),

-- Formosa (25 localidades)
('Formosa', 9), ('Clorinda', 9), ('Pirané', 9), ('El Colorado', 9), ('Las Lomitas', 9),
('Ibarreta', 9), ('Comandante Fontana', 9), ('Villa General Güemes', 9), ('Laguna Blanca', 9), ('Palo Santo', 9),
('Mayor Vicente Villafañe', 9), ('Estanislao del Campo', 9), ('San Francisco de Laishí', 9), ('Villa Dos Trece', 9), ('General Lucio V. Mansilla', 9),
('Herradura', 9), ('Laguna Naick Neck', 9), ('Laguna Yema', 9), ('Misión Tacaaglé', 9), ('Portón Negro', 9),
('Riacho He-Hé', 9), ('San Hilario', 9), ('Subteniente Perín', 9), ('Tres Lagunas', 9), ('Villa Escolar', 9),

-- Jujuy (40 localidades)
('San Salvador de Jujuy', 10), ('Palpalá', 10), ('La Quiaca', 10), ('San Pedro de Jujuy', 10), ('Libertador General San Martín', 10),
('Perico', 10), ('El Carmen', 10), ('Fraile Pintado', 10), ('Monterrico', 10), ('Humahuaca', 10),
('Abra Pampa', 10), ('Yuto', 10), ('Caimancito', 10), ('Calilegua', 10), ('El Talar', 10),
('Pampa Blanca', 10), ('Santa Clara', 10), ('Susques', 10), ('Tilcara', 10), ('Tumbaya', 10),
('Valle Grande', 10), ('San Antonio', 10), ('Rinconada', 10), ('Catua', 10), ('Cieneguillas', 10),
('Cusi Cusi', 10), ('El Aguilar', 10), ('Hipólito Yrigoyen', 10), ('La Esperanza', 10), ('La Mendieta', 10),
('Maimará', 10), ('Puesto del Marqués', 10), ('Purmamarca', 10), ('Rodeíto', 10), ('San Francisco', 10),
('Santa Catalina', 10), ('Tres Cruces', 10), ('Vinalito', 10), ('Yala', 10), ('Yaví', 10),

-- La Pampa (30 localidades)
('Santa Rosa', 11), ('General Pico', 11), ('Toay', 11), ('Realicó', 11), ('Eduardo Castex', 11),
('Macachín', 11), ('Victorica', 11), ('Quemú Quemú', 11), ('Intendente Alvear', 11), ('Trenel', 11),
('Catriló', 11), ('Alta Italia', 11), ('Bernardo Larroudé', 11), ('Caleufú', 11), ('Carro Quemado', 11),
('Colonia Barón', 11), ('Doblas', 11), ('General Acha', 11), ('General Campos', 11), ('Guatraché', 11),
('Jacinto Arauz', 11), ('La Adela', 11), ('Lonquimay', 11), ('Luan Toro', 11), ('Mauricio Mayer', 11),
('Metileo', 11), ('Miguel Cané', 11), ('Parera', 11), ('Perú', 11), ('Rolón', 11),

-- La Rioja (35 localidades)
('La Rioja', 12), ('Chilecito', 12), ('Aimogasta', 12), ('Chamical', 12), ('Chepes', 12),
('Villa Unión', 12), ('Famatina', 12), ('Ulapes', 12), ('Villa Castelli', 12), ('Arauco', 12),
('Castro Barros', 12), ('Coronel Felipe Varela', 12), ('General Ángel V. Peñaloza', 12), ('General Belgrano', 12), ('General Juan Facundo Quiroga', 12),
('General Lamadrid', 12), ('General Ocampo', 12), ('General San Martín', 12), ('Independencia', 12), ('Rosario Vera Peñaloza', 12),
('Sanagasta', 12), ('Vinchina', 12), ('Capital', 12), ('Chilecito', 12), ('Arauco', 12),
('Castro Barros', 12), ('Coronel Felipe Varela', 12), ('Chamical', 12), ('General Ángel V. Peñaloza', 12), ('General Belgrano', 12),
('General Juan Facundo Quiroga', 12), ('General Lamadrid', 12), ('General Ocampo', 12), ('General San Martín', 12), ('Independencia', 12),

-- Mendoza (80 localidades)
('Mendoza', 13), ('San Rafael', 13), ('Godoy Cruz', 13), ('Guaymallén', 13), ('Las Heras', 13),
('Luján de Cuyo', 13), ('Maipú', 13), ('Rivadavia', 13), ('San Martín', 13), ('Tunuyán', 13),
('Tupungato', 13), ('General Alvear', 13), ('La Paz', 13), ('Santa Rosa', 13), ('Malargüe', 13),
('Junín', 13), ('San Carlos', 13), ('Lavalle', 13), ('Capital', 13), ('Godoy Cruz', 13),
('Guaymallén', 13), ('Las Heras', 13), ('Luján de Cuyo', 13), ('Maipú', 13), ('Rivadavia', 13),
('San Martín', 13), ('Tunuyán', 13), ('Tupungato', 13), ('General Alvear', 13), ('La Paz', 13),
('Santa Rosa', 13), ('Malargüe', 13), ('Junín', 13), ('San Carlos', 13), ('Lavalle', 13),
('Cacheuta', 13), ('Carrodilla', 13), ('Chacras de Coria', 13), ('Coquimbito', 13), ('Cruz de Piedra', 13),
('El Algarrobal', 13), ('El Borbollón', 13), ('El Challao', 13), ('El Plumerillo', 13), ('El Resguardo', 13),
('El Sauce', 13), ('Fray Luis Beltrán', 13), ('General Gutiérrez', 13), ('Gobernador Benegas', 13), ('Goudge', 13),
('La Primavera', 13), ('Las Cañas', 13), ('Las Compuertas', 13), ('Las Vegas', 13), ('Lunlunta', 13),
('Luzuriaga', 13), ('Mayor Drummond', 13), ('Palmira', 13), ('Pedriel', 13), ('Perdriel', 13),
('Piedmont', 13), ('Potrerillos', 13), ('Rodeo del Medio', 13), ('Russell', 13), ('San Francisco del Monte', 13),
('San José', 13), ('San Roque', 13), ('Terrazas del Portezuelo', 13), ('Villa Atuel', 13), ('Villa Bastías', 13),
('Villa Hipódromo', 13), ('Villa Nueva', 13), ('Villa Seca', 13), ('Vistalba', 13), ('Barrio Civit', 13),

-- Misiones (50 localidades)
('Posadas', 14), ('Oberá', 14), ('Eldorado', 14), ('San Vicente', 14), ('Puerto Iguazú', 14),
('Leandro N. Alem', 14), ('Apóstoles', 14), ('Aristóbulo del Valle', 14), ('Cainguás', 14), ('Candelaria', 14),
('Concepción de la Sierra', 14), ('El Soberbio', 14), ('General Manuel Belgrano', 14), ('Guaraní', 14), ('Iguazú', 14),
('Libertador General San Martín', 14), ('Montecarlo', 14), ('Paranay', 14), ('San Ignacio', 14), ('San Javier', 14),
('San Pedro', 14), ('25 de Mayo', 14), ('Alba Posse', 14), ('Azara', 14), ('Bernardo de Irigoyen', 14),
('Bonpland', 14), ('Campo Grande', 14), ('Campo Ramón', 14), ('Campo Viera', 14), ('Capioví', 14),
('Caraguatay', 14), ('Cerro Azul', 14), ('Cerro Corá', 14), ('Colonia Aurora', 14), ('Colonia Polana', 14),
('Comandante Andresito', 14), ('Corpus', 14), ('Dos Arroyos', 14), ('Dos de Mayo', 14), ('El Alcázar', 14),
('Fachinal', 14), ('Garuhapé', 14), ('Garupá', 14), ('General Urquiza', 14), ('Gobernador López', 14),
('Gobernador Roca', 14), ('Hipólito Yrigoyen', 14), ('Jardín América', 14), ('Loreto', 14), ('Mártires', 14),

-- Neuquén (40 localidades)
('Neuquén', 15), ('Cutral Có', 15), ('Plottier', 15), ('Centenario', 15), ('San Martín de los Andes', 15),
('Zapala', 15), ('Rincón de los Sauces', 15), ('Villa La Angostura', 15), ('Chos Malal', 15), ('Andacollo', 15),
('Aluminé', 15), ('Junín de los Andes', 15), ('Las Coloradas', 15), ('Las Lajas', 15), ('Loncopué', 15),
('Buta Ranquil', 15), ('Caviahue', 15), ('El Cholar', 15), ('El Huecú', 15), ('Guañacos', 15),
('Huinganco', 15), ('Mariano Moreno', 15), ('Picún Leufú', 15), ('Piedra del Águila', 15), ('Pilo Lil', 15),
('Quili Malal', 15), ('Rahue', 15), ('San Patricio del Chañar', 15), ('Sauzal Bonito', 15), ('Taquimilán', 15),
('Tricao Malal', 15), ('Varvarco', 15), ('Villa Curí Leuvú', 15), ('Villa del Nahueve', 15), ('Villa del Puente Picún Leufú', 15),
('Villa El Chocón', 15), ('Villa Pehuenia', 15), ('Villa Traful', 15), ('Vista Alegre', 15), ('Zapala', 15),

-- Río Negro (45 localidades)
('Viedma', 16), ('San Carlos de Bariloche', 16), ('General Roca', 16), ('Cipolletti', 16), ('Allen', 16),
('Cinco Saltos', 16), ('Catriel', 16), ('Choele Choel', 16), ('Río Colorado', 16), ('Villa Regina', 16),
('El Bolsón', 16), ('Ingeniero Jacobacci', 16), ('Luis Beltrán', 16), ('Chimpay', 16), ('Coronel Belisle', 16),
('Darwin', 16), ('General Conesa', 16), ('Guardia Mitre', 16), ('Lamarque', 16), ('Mainqué', 16),
('Ñorquinco', 16), ('Pilcaniyeu', 16), ('Pomona', 16), ('Sierra Colorada', 16), ('Sierra Grande', 16),
('Valcheta', 16), ('Villa Manzano', 16), ('Aguada Cecilio', 16), ('Comallo', 16), ('Dina Huapi', 16),
('El Cuy', 16), ('Gastre', 16), ('Laguna Blanca', 16), ('Los Menucos', 16), ('Maquinchao', 16),
('Mamuel Choique', 16), ('Río Chico', 16), ('San Antonio Oeste', 16), ('Sierra Pailemán', 16), ('Treneta', 16),
('Villa Llanquín', 16), ('Yaminué', 16), ('Paso Flores', 16), ('Pilquiniyeu', 16), ('Ruca Choroi', 16),

-- Salta (55 localidades)
('Salta', 17), ('San Ramón de la Nueva Orán', 17), ('Tartagal', 17), ('Metán', 17), ('Rosario de la Frontera', 17),
('Cafayate', 17), ('Cerrillos', 17), ('La Caldera', 17), ('Cachi', 17), ('Molinos', 17),
('San Carlos', 17), ('Angastaco', 17), ('Animaná', 17), ('Colonia Santa Rosa', 17), ('El Carril', 17),
('El Galpón', 17), ('El Tala', 17), ('General Güemes', 17), ('Guachipas', 17), ('Joaquín V. González', 17),
('La Candelaria', 17), ('La Merced', 17), ('La Poma', 17), ('La Viña', 17), ('Las Lajitas', 17),
('Los Toldos', 17), ('Nazareno', 17), ('Payogasta', 17), ('Rivadavia', 17), ('Rosario de Lerma', 17),
('San Antonio de los Cobres', 17), ('San Lorenzo', 17), ('Santa Victoria Este', 17), ('Seclantás', 17), ('Tolar Grande', 17),
('Urundel', 17), ('Vaqueros', 17), ('Villa San Lorenzo', 17), ('Aguaray', 17), ('Campo Quijano', 17),
('Coronel Moldes', 17), ('El Bordo', 17), ('El Jardín', 17), ('El Potrero', 17), ('General Ballivián', 17),
('General Mosconi', 17), ('Hickmann', 17), ('Pichanal', 17), ('Profesor Salvador Mazza', 17), ('Río Piedras', 17),
('San Agustín', 17), ('Santa Rosa de Tastil', 17), ('Tobantirenda', 17), ('Yacuy', 17), ('Yariguarenda', 17),

-- San Juan (40 localidades)
('San Juan', 18), ('Rawson', 18), ('Rivadavia', 18), ('Santa Lucía', 18), ('Pocito', 18),
('Caucete', 18), ('Jáchal', 18), ('Albardón', 18), ('Calingasta', 18), ('Iglesia', 18),
('Ullum', 18), ('Zonda', 18), ('Angaco', 18), ('Calingasta', 18), ('Capital', 18),
('Caucete', 18), ('Chimbas', 18), ('Iglesia', 18), ('Jáchal', 18), ('9 de Julio', 18),
('Pocito', 18), ('Rawson', 18), ('Rivadavia', 18), ('San Martín', 18), ('Santa Lucía', 18),
('Sarmiento', 18), ('Ullum', 18), ('Valle Fértil', 18), ('25 de Mayo', 18), ('Zonda', 18),
('Barreal', 18), ('Bella Vista', 18), ('Carpintería', 18), ('Divisadero', 18), ('El Médano', 18),
('El Rincón', 18), ('La Chimbera', 18), ('Las Flores', 18), ('Los Berros', 18), ('Villa Basilio Nievas', 18),

-- San Luis (35 localidades)
('San Luis', 19), ('Villa Mercedes', 19), ('Merlo', 19), ('Juana Koslay', 19), ('La Toma', 19),
('Quines', 19), ('San Francisco del Monte de Oro', 19), ('Nogolí', 19), ('Candelaria', 19), ('Carolina', 19),
('Concarán', 19), ('Cortaderas', 19), ('El Trapiche', 19), ('Estancia Grande', 19), ('Fraga', 19),
('La Punilla', 19), ('Luján', 19), ('Naschel', 19), ('Papagayos', 19), ('Potrero de los Funes', 19),
('Renca', 19), ('Salinas del Bebedero', 19), ('San Gerónimo', 19), ('San Martín', 19), ('Santa Rosa del Conlara', 19),
('Tilisarao', 19), ('Unión', 19), ('Villa de la Quebrada', 19), ('Villa del Carmen', 19), ('Villa General Roca', 19),
('Villa Larca', 19), ('Villa Reynolds', 19), ('Zanjitas', 19), ('Buena Esperanza', 19), ('Lafinur', 19),

-- Santa Fe (100 localidades)
('Rosario', 20), ('Santa Fe', 20), ('Rafaela', 20), ('Reconquista', 20), ('Venado Tuerto', 20),
('San Lorenzo', 20), ('Sunchales', 20), ('Villa Gobernador Gálvez', 20), ('Carcarañá', 20), ('Casilda', 20),
('Cañada de Gómez', 20), ('Esperanza', 20), ('Funes', 20), ('Granadero Baigorria', 20), ('Las Parejas', 20),
('Pérez', 20), ('Roldán', 20), ('San Justo', 20), ('Tostado', 20), ('Villa Constitución', 20),
('Arroyo Seco', 20), ('Avellaneda', 20), ('Calchaquí', 20), ('Ceres', 20), ('Coronda', 20),
('El Trébol', 20), ('Firmat', 20), ('Gálvez', 20), ('Hersilia', 20), ('Humboldt', 20),
('Laguna Paiva', 20), ('Las Rosas', 20), ('Malabrigo', 20), ('Monte Vera', 20), ('Murphy', 20),
('Nelson', 20), ('Palacios', 20), ('Pavón', 20), ('Pueblo Esther', 20), ('Puerto General San Martín', 20),
('San Cristóbal', 20), ('San Javier', 20), ('San Jorge', 20), ('San José del Rincón', 20), ('Santa Clara de Saguier', 20),
('Santo Tomé', 20), ('Sastre', 20), ('Tacuarendí', 20), ('Totoras', 20), ('Villa Cañás', 20),
('Wheelwright', 20), ('Aarón Castellanos', 20), ('Acebal', 20), ('Alcorta', 20), ('Aldao', 20),
('Álvarez', 20), ('Amstrong', 20), ('Arequito', 20), ('Armstrong', 20), ('Arrufó', 20),
('Arteaga', 20), ('Ataliva', 20), ('Aurelia', 20), ('Barrancas', 20), ('Bauer y Sigel', 20),
('Bella Italia', 20), ('Berabevú', 20), ('Bigand', 20), ('Bombal', 20), ('Bouquet', 20),
('Calchines', 20), ('Camilo Aldao', 20), ('Campo Andino', 20), ('Candioti', 20), ('Capivara', 20),
('Cayastá', 20), ('Centeno', 20), ('Chañar Ladeado', 20), ('Chapuy', 20), ('Chovet', 20),
('Clason', 20), ('Colonia Belgrano', 20), ('Colonia Cello', 20), ('Colonia Clara', 20), ('Colonia Durán', 20),
('Colonia Margarita', 20), ('Colonia Raquel', 20), ('Constanza', 20), ('Coronel Arnold', 20), ('Coronel Bogado', 20),
('Correo', 20), ('Crispi', 20), ('Cululú', 20), ('Desvío Arijón', 20), ('Díaz', 20),

-- Santa Cruz (25 localidades)
('Río Gallegos', 21), ('Caleta Olivia', 21), ('Pico Truncado', 21), ('Puerto Deseado', 21), ('Las Heras', 21),
('Río Turbio', 21), ('El Calafate', 21), ('28 de Noviembre', 21), ('Perito Moreno', 21), ('Los Antiguos', 21),
('Puerto San Julián', 21), ('Gobernador Gregores', 21), ('El Chaltén', 21), ('Jaramillo', 21), ('Koluel Kaike', 21),
('La Esperanza', 21), ('Mina 3', 21), ('Paso Roballos', 21), ('Puerto Santa Cruz', 21), ('Tellier', 21),
('Tres Lagos', 21), ('Yacimientos Río Turbio', 21), ('Cañadón Seco', 21), ('Fitz Roy', 21), ('Hipólito Yrigoyen', 21),

-- Santiago del Estero (45 localidades)
('Santiago del Estero', 22), ('La Banda', 22), ('Frías', 22), ('Añatuya', 22), ('Termas de Río Hondo', 22),
('Loreto', 22), ('Quimilí', 22), ('Fernández', 22), ('Monte Quemado', 22), ('Suncho Corral', 22),
('Selva', 22), ('Icaño', 22), ('Clodomira', 22), ('Bandera', 22), ('Sumampa', 22),
('Villa Ojo de Agua', 22), ('Pinto', 22), ('Villa La Punta', 22), ('Villa San Martín', 22), ('Villa Unión', 22),
('Beltrán', 22), ('Colonia Dora', 22), ('Herrera', 22), ('La Cañada', 22), ('Los Juríes', 22),
('Los Telares', 22), ('Nueva Esperanza', 22), ('Pozo Hondo', 22), ('San Pedro', 22), ('Santo Domingo', 22),
('Simbol', 22), ('Tintina', 22), ('Villa Atamisqui', 22), ('Villa Figueroa', 22), ('Villa General Mitre', 22),
('Villa Guasayán', 22), ('Villa Robles', 22), ('Villa Salavina', 22), ('Weisburd', 22), ('Yanda', 22),
('Arraga', 22), ('Casares', 22), ('Chilca Juliana', 22), ('El Bobadal', 22), ('El Charco', 22),

-- Tierra del Fuego (20 localidades)
('Ushuaia', 23), ('Río Grande', 23), ('Tolhuin', 23), ('Lago Escondido', 23), ('Puerto Almanza', 23),
('Estancia Harberton', 23), ('Puerto Williams', 23), ('Cabo San Pablo', 23), ('Estancia Viamonte', 23), ('Estancia María Behety', 23),
('Paso Garibaldi', 23), ('Puesto La Portada', 23), ('Puesto La Unión', 23), ('Puesto Lasifashaj', 23), ('Puesto Policarpo', 23),
('Puesto Río Ewan', 23), ('Puesto San Martín', 23), ('Puesto Sara', 23), ('Puesto Túnel', 23), ('Puesto Yendegaia', 23),

-- Tucumán (50 localidades)
('San Miguel de Tucumán', 24), ('Yerba Buena', 24), ('Tafí Viejo', 24), ('Aguilares', 24), ('Concepción', 24),
('Banda del Río Salí', 24), ('Monteros', 24), ('Famaillá', 24), ('Lules', 24), ('Simoca', 24),
('Graneros', 24), ('La Cocha', 24), ('Trancas', 24), ('Burruyacú', 24), ('Cruz Alta', 24),
('Leales', 24), ('Río Chico', 24), ('Chicligasta', 24), ('Juan B. Alberdi', 24), ('La Madrid', 24),
('Tafí del Valle', 24), ('Alpachiri', 24), ('Amaicha del Valle', 24), ('El Manantial', 24), ('El Mojón', 24),
('El Naranjo', 24), ('El Polear', 24), ('El Tajamar', 24), ('La Angostura', 24), ('La Florida', 24),
('Las Talas', 24), ('Los Nogales', 24), ('Los Pérez', 24), ('Los Ralos', 24), ('Monteagudo', 24),
('Pueblo Viejo', 24), ('Raco', 24), ('San Pedro de Colalao', 24), ('Santa Lucía', 24), ('Villa Belgrano', 24),
('Villa Carmela', 24), ('Villa de Leales', 24), ('Villa Quinteros', 24), ('Villa Recaste', 24), ('Villa de Trancas', 24),
('Yánima', 24), ('Yerba Buena', 24), ('Zárate', 24), ('Atahona', 24), ('Colombres', 24);
GO
