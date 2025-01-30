use ecommerceVinos;


-- Insertar clientes de prueba
INSERT INTO CLIENTE (Nombres, Apellidos, Correo, Clave, Reestablecer, FechaRegistro) 
VALUES ('Juan', 'Pérez', 'juan.perez@email.com', '5ac0852e770506dcd80f1a36d20ba7878bf82244b836d9324593bd14bc56dcb5', 0, GETDATE());

INSERT INTO CLIENTE (Nombres, Apellidos, Correo, Clave, Reestablecer, FechaRegistro) 
VALUES ('Ana', 'Gómez', 'ana.gomez@email.com', 'ca8cc9fa7470c31c495c17f276152d9c9a4b06c20ecbfecffd65129f3de9b24d', 0, GETDATE());

-- Insertar productos de prueba
INSERT INTO PRODUCTO (Nombre, Descripcion, IdMarca, IdCategoria, Precio, Stock, RutaImagen, NombreImagen, Activo, FechaRegistro)
VALUES 
('Vino Malbec', 'Vino tinto Malbec reserva', 1, 1, 35.50, 50, '~/images/vino1.jpg', 'vino1.jpg', 1, GETDATE()),
('Vino Chardonnay', 'Vino blanco Chardonnay premium', 2, 2, 29.99, 30, '~/images/vino2.jpg', 'vino2.jpg', 1, GETDATE());

-- Insertar ventas con pagos procesados por PayPal
INSERT INTO VENTA (IdCliente, TotalProducto, MontoTotal, Contacto, Telefono, Direccion, IdTransaccion, FechaVenta)
VALUES 
(1, 2, 71.00, 'Juan Pérez', '555-1234', 'Calle 123, Ciudad', 'PAYPAL12345', DATEADD(DAY, -10, GETDATE())),
(2, 1, 29.99, 'Ana Gómez', '555-5678', 'Avenida 456, Ciudad', 'PAYPAL67890', DATEADD(DAY, -5, GETDATE()));

-- Insertar detalles de ventas (relacionando productos con ventas)
INSERT INTO DETALLE_VENTA (IdVenta, IdProducto, Cantidad, Total)
VALUES 
(1, 1, 1, 35.50), -- Juan compró 1 Malbec
(1, 2, 1, 35.50), -- Juan compró 1 Chardonnay
(2, 2, 1, 29.99); -- Ana compró 1 Chardonnay

-- Insertar una venta pendiente (sin pago aún)
INSERT INTO VENTA (IdCliente, TotalProducto, MontoTotal, Contacto, Telefono, Direccion, IdTransaccion, FechaVenta)
VALUES 
(1, 3, 105.00, 'Juan Pérez', '555-1234', 'Calle 123, Ciudad', NULL, GETDATE());

-- Insertar detalles de la venta pendiente
INSERT INTO DETALLE_VENTA (IdVenta, IdProducto, Cantidad, Total)
VALUES 
(3, 1, 3, 105.00); -- Juan compró 3 Malbec, pero la transacción aún no está confirmada


SELECT  * FROM VENTA;