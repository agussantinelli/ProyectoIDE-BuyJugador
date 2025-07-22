CREATE DATABASE IF NOT EXISTS buyjugador;
USE buyjugador;

-- Tablas principales
CREATE TABLE Provincia (
    codigo_provincia INT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL
);

CREATE TABLE Localidad (
    codigo_localidad INT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    codigo_provincia INT NOT NULL,
    FOREIGN KEY (codigo_provincia) REFERENCES Provincia(codigo_provincia)
);

CREATE TABLE TipoProducto (
    id_tipo_producto INT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL
);

CREATE TABLE Producto (
    id_producto INT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    stock INT NOT NULL DEFAULT 0,
    id_tipo_producto INT NOT NULL,
    FOREIGN KEY (id_tipo_producto) REFERENCES TipoProducto(id_tipo_producto)
);

CREATE TABLE PrecioProducto (
    id_producto INT,
    fecha_desde DATE,
    monto DECIMAL(10,2) NOT NULL,
    PRIMARY KEY (id_producto, fecha_desde),
    FOREIGN KEY (id_producto) REFERENCES Producto(id_producto)
);

CREATE TABLE Proveedor (
    cuil VARCHAR(20) PRIMARY KEY,
    razon_social VARCHAR(100) NOT NULL,
    telefono VARCHAR(20),
    mail VARCHAR(100),
    codigo_localidad INT,
    FOREIGN KEY (codigo_localidad) REFERENCES Localidad(codigo_localidad)
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
    fecha_ingreso DATE NOT NULL,
    FOREIGN KEY (dni) REFERENCES Persona(dni)
);

CREATE TABLE Dueño (
    dni VARCHAR(20) PRIMARY KEY,
    FOREIGN KEY (dni) REFERENCES Persona(dni)
);

CREATE TABLE Venta (
    id_venta INT PRIMARY KEY,
    fecha_venta DATE NOT NULL,
    estado VARCHAR(50) NOT NULL,
    dni_vendedor VARCHAR(20) NOT NULL,
    FOREIGN KEY (dni_vendedor) REFERENCES Persona(dni)
);

CREATE TABLE LineaVenta (
    id_venta INT,
    numero_linea INT,
    cantidad INT NOT NULL,
    id_producto INT NOT NULL,
    PRIMARY KEY (id_venta, numero_linea),
    FOREIGN KEY (id_venta) REFERENCES Venta(id_venta),
    FOREIGN KEY (id_producto) REFERENCES Producto(id_producto)
);

CREATE TABLE Pedido (
    id_pedido INT PRIMARY KEY,
    fecha DATE NOT NULL,
    estado VARCHAR(50) NOT NULL,
    cuil_proveedor VARCHAR(20) NOT NULL,
    FOREIGN KEY (cuil_proveedor) REFERENCES Proveedor(cuil)
);

CREATE TABLE LineaPedido (
    id_pedido INT,
    numero_linea INT,
    cantidad INT NOT NULL,
    id_producto INT NOT NULL,
    PRIMARY KEY (id_pedido, numero_linea),
    FOREIGN KEY (id_pedido) REFERENCES Pedido(id_pedido),
    FOREIGN KEY (id_producto) REFERENCES Producto(id_producto)
);
