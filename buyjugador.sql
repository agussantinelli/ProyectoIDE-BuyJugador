CREATE DATABASE IF NOT EXISTS buyjugador;
USE buyjugador;

CREATE TABLE Provincia (
    codProvincia INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    descripcion TEXT
);

CREATE TABLE TipoProducto (
    codTipoProducto INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL
);

CREATE TABLE Persona (
    legajo INT AUTO_INCREMENT PRIMARY KEY,
    mail VARCHAR(255) NOT NULL UNIQUE,
    contrasenia VARCHAR(255) NOT NULL,
    nombre VARCHAR(150) NOT NULL
);

CREATE TABLE Administrador (
    legajoPersona INT PRIMARY KEY,
    FOREIGN KEY (legajoPersona) REFERENCES Persona(legajo) ON DELETE CASCADE
);

CREATE TABLE Localidad (
    codLocalidad INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    codProvincia INT NOT NULL,
    FOREIGN KEY (codProvincia) REFERENCES Provincia(codProvincia)
);

CREATE TABLE Cliente (
    legajoPersona INT PRIMARY KEY,
    fechaNacimiento DATE,
    telefono VARCHAR(25),
    codLocalidad INT,
	edad INT,
    FOREIGN KEY (legajoPersona) REFERENCES Persona(legajo) ON DELETE CASCADE,
    FOREIGN KEY (codLocalidad) REFERENCES Localidad(codLocalidad)
);

CREATE TABLE Producto (
    idProducto INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(255) NOT NULL,
    caracteristicas TEXT,
    stock INT NOT NULL DEFAULT 0,
    codTipoProducto INT,
    FOREIGN KEY (codTipoProducto) REFERENCES TipoProducto(codTipoProducto)
);

CREATE TABLE Precios (
    idPrecio INT AUTO_INCREMENT PRIMARY KEY,
    idObjeto INT NOT NULL,
    monto DECIMAL(10, 2) NOT NULL,
    fecha DATE NOT NULL,
    FOREIGN KEY (idObjeto) REFERENCES Producto(idObjeto)
);

CREATE TABLE Venta (
    nroVenta INT AUTO_INCREMENT PRIMARY KEY,
    fecha DATETIME NOT NULL,
    estado VARCHAR(50), 
    montoTotal DECIMAL(12, 2),
    legajoCliente INT NOT NULL,
    FOREIGN KEY (legajoCliente) REFERENCES Cliente(legajoPersona)
);

CREATE TABLE Cancelacion (
    nroVenta INT PRIMARY KEY,
    fecha DATETIME NOT NULL,
    motivo TEXT,
    FOREIGN KEY (nroVenta) REFERENCES Venta(nroVenta) ON DELETE CASCADE
);

CREATE TABLE Linea_Venta (
    nroVenta INT NOT NULL,
    idObjeto INT NOT NULL,
    cantidad INT NOT NULL,
    precioUnitario DECIMAL(10, 2) NOT NULL,

    PRIMARY KEY (nroVenta, idObjeto),
    FOREIGN KEY (nroVenta) REFERENCES Venta(nroVenta) ON DELETE CASCADE,
    FOREIGN KEY (idObjeto) REFERENCES Producto(idObjeto)
);
