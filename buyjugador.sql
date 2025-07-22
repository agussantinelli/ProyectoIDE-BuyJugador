CREATE DATABASE IF NOT EXISTS buyjugador;
USE buyjugador;

CREATE TABLE Provincia (
    codigoProvincia INT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL
);

CREATE TABLE Localidad (
    codigoLocalidad INT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    codigoProvincia INT NOT NULL,
    FOREIGN KEY (codigoProvincia) REFERENCES Provincia(codigoProvincia)
);

CREATE TABLE TipoProducto (
    idTipoProducto INT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL
);

CREATE TABLE Producto (
    idProducto INT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    stock INT NOT NULL DEFAULT 0,
    idTipoProducto INT NOT NULL,
    FOREIGN KEY (idTipoProducto) REFERENCES TipoProducto(idTipoProducto)
);

CREATE TABLE PrecioProducto (
    idProducto INT,
    fechaDesde DATE,
    monto DECIMAL(10,2) NOT NULL,
    PRIMARY KEY (idProducto, fechaDesde),
    FOREIGN KEY (idProducto) REFERENCES Producto(idProducto)
);

CREATE TABLE Proveedor (
    cuil VARCHAR(20) PRIMARY KEY,
    razonSocial VARCHAR(100) NOT NULL,
    telefono VARCHAR(20),
    mail VARCHAR(100),
    codigoLocalidad INT,
    FOREIGN KEY (codigoLocalidad) REFERENCES Localidad(codigoLocalidad)
);

CREATE TABLE Persona (
    dni VARCHAR(20) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    contraseña VARCHAR(100) NOT NULL,
    telefono VARCHAR(20),
    mail VARCHAR(100)
);

CREATE TABLE Empleado (
    dni VARCHAR(20) PRIMARY KEY,
    fechaIngreso DATE NOT NULL,
    FOREIGN KEY (dni) REFERENCES Persona(dni)
);

CREATE TABLE Dueño (
    dni VARCHAR(20) PRIMARY KEY,
    FOREIGN KEY (dni) REFERENCES Persona(dni)
);

CREATE TABLE Venta (
    idVenta INT PRIMARY KEY,
    fechaVenta DATE NOT NULL,
    estado VARCHAR(50) NOT NULL,
    dniVendedor VARCHAR(20) NOT NULL,
    FOREIGN KEY (dniVendedor) REFERENCES Persona(dni)
);

CREATE TABLE LineaVenta (
    idVenta INT,
    numeroLinea INT,
    cantidad INT NOT NULL,
    idProducto INT NOT NULL,
    PRIMARY KEY (idVenta, numeroLinea),
    FOREIGN KEY (idVenta) REFERENCES Venta(idVenta),
    FOREIGN KEY (idProducto) REFERENCES Producto(idProducto)
);

CREATE TABLE Pedido (
    idPedido INT PRIMARY KEY,
    fecha DATE NOT NULL,
    estado VARCHAR(50) NOT NULL,
    cuilProveedor VARCHAR(20) NOT NULL,
    FOREIGN KEY (cuilProveedor) REFERENCES Proveedor(cuil)
);

CREATE TABLE LineaPedido (
    idPedido INT,
    numeroLinea INT,
    cantidad INT NOT NULL,
    idProducto INT NOT NULL,
    PRIMARY KEY (idPedido, numeroLinea),
    FOREIGN KEY (idPedido) REFERENCES Pedido(idPedido),
    FOREIGN KEY (idProducto) REFERENCES Producto(idProducto)
);
