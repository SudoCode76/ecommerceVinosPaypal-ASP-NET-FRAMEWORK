
use ecommerceVinos;
GO
CREATE TABLE ADMINISTRATIVO(
    IdAdministrativo INT PRIMARY KEY IDENTITY,
    Nombres VARCHAR(100),
    Apellidos VARCHAR(100),
    Correo VARCHAR(100) UNIQUE,
    Clave VARCHAR(150),
    Activo BIT DEFAULT 1
);

GO
CREATE PROCEDURE sp_RegistrarAdministrativo(
    @Nombres VARCHAR(100),
    @Apellidos VARCHAR(100),
    @Correo VARCHAR(100),
    @Clave VARCHAR(150),
    @Mensaje VARCHAR(500) OUTPUT,
    @Resultado INT OUTPUT
)
AS
BEGIN
    SET @Resultado = 0
    SET @Mensaje = ''

    -- Verificar si el correo ya está en uso
    IF NOT EXISTS (SELECT 1 FROM ADMINISTRATIVO WHERE Correo = @Correo)
    BEGIN
        -- Insertar el nuevo usuario administrativo
        INSERT INTO ADMINISTRATIVO (Nombres, Apellidos, Correo, Clave, Activo)
        VALUES (@Nombres, @Apellidos, @Correo, @Clave, 1)

        -- Obtener el ID del nuevo registro
        SET @Resultado = SCOPE_IDENTITY()
    END
    ELSE
    BEGIN
        -- Si el correo ya existe, retornar un mensaje
        SET @Mensaje = 'El correo ya está en uso por otro usuario administrativo.'
    END
END



GO
CREATE PROCEDURE sp_RegistrarCategoria
    @Descripcion NVARCHAR(250),
    @Resultado INT OUTPUT,
    @Mensaje NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        INSERT INTO Categoria (Descripcion, Activo)
        VALUES (@Descripcion, 1); -- Activo siempre será TRUE

        SET @Resultado = SCOPE_IDENTITY();
        SET @Mensaje = 'Registro exitoso';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;

GO
CREATE PROCEDURE sp_EditarCategoria
    @IdCategoria INT,
    @Descripcion NVARCHAR(250),
    @Activo BIT,
    @Resultado INT OUTPUT,
    @Mensaje NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Intentar actualizar la categoría
        UPDATE Categoria
        SET 
            Descripcion = @Descripcion,
            Activo = @Activo
        WHERE 
            IdCategoria = @IdCategoria;

        IF @@ROWCOUNT > 0
        BEGIN
            SET @Resultado = 1;
            SET @Mensaje = 'Categoría actualizada exitosamente.';
        END
        ELSE
        BEGIN
            SET @Resultado = 0;
            SET @Mensaje = 'No se encontró la categoría especificada.';
        END
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;


GO
CREATE PROCEDURE sp_EliminarCategoria
    @IdCategoria INT,
    @Resultado INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Verificar si la categoría existe
        IF EXISTS (SELECT 1 FROM Categoria WHERE IdCategoria = @IdCategoria)
        BEGIN
            -- Comprobar si la categoría está activa
            DECLARE @Activo INT;
            SELECT @Activo = Activo FROM Categoria WHERE IdCategoria = @IdCategoria;

            IF @Activo = 1
            BEGIN
                -- Si la categoría está activa, eliminarla
                DELETE FROM Categoria WHERE IdCategoria = @IdCategoria;
                SET @Resultado = 1;
                SET @Mensaje = 'Categoría eliminada exitosamente.';
            END
            ELSE
            BEGIN
                -- Si la categoría está inactiva, actualizar el estado a inactivo (si aún está activa)
                UPDATE Categoria
                SET Activo = 0
                WHERE IdCategoria = @IdCategoria;
                
                SET @Resultado = 1;
                SET @Mensaje = 'Categoría marcada como inactiva exitosamente.';
            END
        END
        ELSE
        BEGIN
            -- Si la categoría no existe
            SET @Resultado = 0;
            SET @Mensaje = 'La categoría no existe.';
        END
    END TRY
    BEGIN CATCH
        -- Capturar error en caso de excepción
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END


GO
Create PROCEDURE sp_RegistrarMarca
    @Descripcion NVARCHAR(250),
    @Resultado INT OUTPUT,
    @Mensaje NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        INSERT INTO Marca (Descripcion, Activo)
        VALUES (@Descripcion, 1); -- Activo siempre será TRUE

        SET @Resultado = SCOPE_IDENTITY();
        SET @Mensaje = 'Registro exitoso';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;


GO
CREATE PROCEDURE sp_EditarMarca
    @IdMarca INT,
    @Descripcion NVARCHAR(250),
    @Activo BIT,
    @Resultado INT OUTPUT,
    @Mensaje NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        UPDATE Marca
        SET 
            Descripcion = @Descripcion,
            Activo = @Activo
        WHERE 
            IdMarca = @IdMarca;

        IF @@ROWCOUNT > 0
        BEGIN
            SET @Resultado = 1;
            SET @Mensaje = 'Marca actualizada exitosamente.';
        END
        ELSE
        BEGIN
            SET @Resultado = 0;
            SET @Mensaje = 'No se encontró la marca especificada.';
        END
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;


GO
CREATE PROCEDURE sp_EliminarMarca
    @IdMarca INT,
    @Resultado INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Verificar si la categoría existe
        IF EXISTS (SELECT 1 FROM Marca WHERE IdMarca = @IdMarca)
        BEGIN
            -- Comprobar si la categoría está activa
            DECLARE @Activo INT;
            SELECT @Activo = Activo FROM Marca WHERE IdMarca = @IdMarca;

            IF @Activo = 1
            BEGIN
                -- Si la categoría está activa, eliminarla
                DELETE FROM Marca WHERE IdMarca = @IdMarca;
                SET @Resultado = 1;
                SET @Mensaje = 'Marca eliminada exitosamente.';
            END
            ELSE
            BEGIN
                -- Si la categoría está inactiva, actualizar el estado a inactivo (si aún está activa)
                UPDATE Marca
                SET Activo = 0
                WHERE IdMarca = @IdMarca;
                
                SET @Resultado = 1;
                SET @Mensaje = 'Marca marcada como inactiva exitosamente.';
            END
        END
        ELSE
        BEGIN
            -- Si la categoría no existe
            SET @Resultado = 0;
            SET @Mensaje = 'La marca no existe.';
        END
    END TRY
    BEGIN CATCH
        -- Capturar error en caso de excepción
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END

GO
CREATE PROCEDURE sp_RegistrarProducto
    @Nombre VARCHAR(100),
    @Descripcion VARCHAR(255),
    @IdMarca INT,
    @IdCategoria INT,
    @Precio DECIMAL(18, 2),
    @Stock INT,
    @RutaImagen VARCHAR(255),
	@NombreImagen VARCHAR(255),
    @Resultado INT OUTPUT,
    @Mensaje VARCHAR(250) OUTPUT
AS
BEGIN
    BEGIN TRY
        -- Inserción del producto
        INSERT INTO Producto (Nombre, Descripcion, IdMarca, IdCategoria, Precio, Stock, RutaImagen, NombreImagen, Activo)
        VALUES (@Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio, @Stock, @RutaImagen, @NombreImagen,1);

        SET @Resultado = SCOPE_IDENTITY();  -- Obtiene el último Id insertado
        SET @Mensaje = 'Producto registrado correctamente';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();  -- Obtiene el mensaje de error
    END CATCH
END;

GO
CREATE PROCEDURE sp_EditarProducto
    @IdProducto INT,
    @Nombre NVARCHAR(100),
    @Descripcion NVARCHAR(500),
    @IdMarca INT,
    @IdCategoria INT,
    @Precio DECIMAL(18,2),
    @Stock INT,
    @RutaImagen NVARCHAR(255),
    @NombreImagen NVARCHAR(255),
    @Resultado INT OUTPUT,
    @Mensaje NVARCHAR(250) OUTPUT
AS
BEGIN
    -- Depuración
    PRINT 'IdProducto: ' + CAST(@IdProducto AS NVARCHAR(10));
    PRINT 'Nombre: ' + @Nombre;
    PRINT 'Descripcion: ' + @Descripcion;
    PRINT 'Precio: ' + CAST(@Precio AS NVARCHAR(50));
    PRINT 'Stock: ' + CAST(@Stock AS NVARCHAR(50));

    -- Lógica de actualización
    BEGIN TRY
        -- Comprobar si realmente hay algún cambio en los datos
        IF EXISTS (
            SELECT 1
            FROM Producto
            WHERE IdProducto = @IdProducto
            AND (
                LTRIM(RTRIM(Nombre)) != LTRIM(RTRIM(@Nombre))
                OR LTRIM(RTRIM(Descripcion)) != LTRIM(RTRIM(@Descripcion))
                OR IdMarca != @IdMarca
                OR IdCategoria != @IdCategoria
                OR Precio != @Precio
                OR Stock != @Stock
                OR LTRIM(RTRIM(RutaImagen)) != LTRIM(RTRIM(@RutaImagen))
                OR LTRIM(RTRIM(NombreImagen)) != LTRIM(RTRIM(@NombreImagen))
            )
        )
        BEGIN
            -- Realizar la actualización
            UPDATE Producto
            SET
                Nombre = @Nombre,
                Descripcion = @Descripcion,
                IdMarca = @IdMarca,
                IdCategoria = @IdCategoria,
                Precio = @Precio,
                Stock = @Stock,
                RutaImagen = @RutaImagen,
                NombreImagen = @NombreImagen
            WHERE IdProducto = @IdProducto;

            -- Verificar si la actualización afectó alguna fila
            IF @@ROWCOUNT > 0
            BEGIN
                SET @Resultado = 1;
                SET @Mensaje = 'Producto actualizado correctamente';
            END
            ELSE
            BEGIN
                SET @Resultado = 0;
                SET @Mensaje = 'No se encontró el producto o no se realizó ningún cambio';
            END
        END
        ELSE
        BEGIN
            SET @Resultado = 0;
            SET @Mensaje = 'No se realizaron cambios, los valores son los mismos';
        END
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END





GO
CREATE PROCEDURE sp_EliminarProducto
    @IdProducto INT,
    @Resultado INT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    BEGIN TRY
        UPDATE PRODUCTO
        SET Activo = 0
        WHERE IdProducto = @IdProducto

        SET @Resultado = 1
        SET @Mensaje = 'Producto desactivado correctamente.'
    END TRY
    BEGIN CATCH
        SET @Resultado = 0
        SET @Mensaje = ERROR_MESSAGE()
    END CATCH
END


SELECT 
    name AS Procedimiento,
    create_date AS FechaCreacion,
    modify_date AS FechaModificacion,
    schema_name(schema_id) AS Esquema
FROM 
    sys.procedures
ORDER BY 
    name;

GO
CREATE PROCEDURE sp_EliminarAdministrativo
    @IdAdministrativo INT,
    @Resultado INT OUTPUT,
    @Mensaje NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Verificar si el administrador existe
        IF EXISTS (SELECT 1 FROM ADMINISTRATIVO WHERE IdAdministrativo = @IdAdministrativo)
        BEGIN
            -- Desactivar el administrador (eliminación lógica)
            UPDATE ADMINISTRATIVO
            SET Activo = 0
            WHERE IdAdministrativo = @IdAdministrativo;

            SET @Resultado = 1;
            SET @Mensaje = 'El administrador ha sido eliminado exitosamente.';
        END
        ELSE
        BEGIN
            SET @Resultado = 0;
            SET @Mensaje = 'El administrador no existe.';
        END
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;


GO
ALTER PROCEDURE sp_EliminarProducto
    @IdProducto INT,
    @Resultado INT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;  -- Evitar mensajes sobre el número de filas afectadas

    BEGIN TRY
        -- Iniciar una transacción
        BEGIN TRANSACTION;

        -- Verificar si el producto existe
        IF EXISTS (SELECT 1 FROM PRODUCTO WHERE IdProducto = @IdProducto)
        BEGIN
            -- Desactivar el producto
            UPDATE PRODUCTO
            SET Activo = 0
            WHERE IdProducto = @IdProducto;

            -- Establecer resultados de éxito
            SET @Resultado = 1;
            SET @Mensaje = 'Producto desactivado correctamente.';

            -- Confirmar la transacción
            COMMIT TRANSACTION;
        END
        ELSE
        BEGIN
            -- Si el producto no existe
            SET @Resultado = 0;
            SET @Mensaje = 'El producto no existe.';
            ROLLBACK TRANSACTION;  -- Revertir la transacción
        END
    END TRY
    BEGIN CATCH
        -- Manejar errores
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();

        -- Revertir la transacción en caso de error
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END
    END CATCH
END;



// desde aca pc mesa


GO
ALTER PROCEDURE sp_EliminarCategoria
    @IdCategoria INT,
    @Resultado INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Iniciar una transacción
        BEGIN TRANSACTION;

        -- Verificar si la categoría existe y está activa en una sola consulta
        IF EXISTS (SELECT 1 FROM Categoria WHERE IdCategoria = @IdCategoria)
        BEGIN
            -- Actualizar el estado de la categoría a inactivo
            UPDATE Categoria
            SET Activo = 0
            WHERE IdCategoria = @IdCategoria;

            -- Confirmar la transacción
            COMMIT TRANSACTION;

            SET @Resultado = 1;
            SET @Mensaje = 'Categoría marcada como inactiva exitosamente.';
        END
        ELSE
        BEGIN
            -- Si la categoría no existe, revertir la transacción
            ROLLBACK TRANSACTION;

            SET @Resultado = 0;
            SET @Mensaje = 'La categoría no existe.';
        END
    END TRY
    BEGIN CATCH
        -- Manejar errores y revertir la transacción
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;


GO
ALTER PROCEDURE sp_EliminarMarca
    @IdMarca INT,
    @Resultado INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Iniciar una transacción
        BEGIN TRANSACTION;

        -- Verificar si la marca existe
        IF EXISTS (SELECT 1 FROM Marca WHERE IdMarca = @IdMarca)
        BEGIN
            -- Actualizar el estado de la marca a inactivo
            UPDATE Marca
            SET Activo = 0
            WHERE IdMarca = @IdMarca;

            -- Confirmar la transacción
            COMMIT TRANSACTION;

            SET @Resultado = 1;
            SET @Mensaje = 'Marca marcada como inactiva exitosamente.';
        END
        ELSE
        BEGIN
            -- Si la marca no existe, revertir la transacción
            ROLLBACK TRANSACTION;

            SET @Resultado = 0;
            SET @Mensaje = 'La marca no existe.';
        END
    END TRY
    BEGIN CATCH
        -- Manejar errores y revertir la transacción
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
