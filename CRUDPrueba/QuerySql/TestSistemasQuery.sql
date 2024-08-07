DROP DATABASE IF EXISTs TestSistemas;

CREATE DATABASE TestSistemas

USE TestSistemas

CREATE TABLE Cliente (
    cliente_id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    nombre NVARCHAR(50),
	apellido NVARCHAR(50),
    tipo NVARCHAR(5)
);

SELECT * FROM Cliente

----
CREATE PROCEDURE sp_create_cliente
(
	@nombre NVARCHAR(50),
	@apellido NVARCHAR(50),
	@tipo NVARCHAR(5)
)
AS 
BEGIN
	BEGIN TRY
		BEGIN TRAN
			INSERT INTO Cliente (nombre, apellido, tipo)
				VALUES (@nombre, @apellido, @tipo)
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH
END

----
CREATE PROCEDURE sp_read_cliente
AS 
BEGIN
	SELECT *
	FROM Cliente WITH (NOLOCK)
END

----
CREATE PROCEDURE sp_search_cliente
(
	@cliente_id INT
)
AS 
BEGIN
	SELECT *
	FROM Cliente WITH (NOLOCK)
	WHERE cliente_id = @cliente_id
END

----
CREATE PROCEDURE sp_update_cliente
(
	@cliente_id INT,
	@nombre NVARCHAR(50),
	@apellido NVARCHAR(50),
	@tipo NVARCHAR(5)
)
AS
BEGIN
DECLARE @RowCount INT = 0
	BEGIN TRY
		SET @RowCount = (SELECT COUNT(1) FROM Cliente WITH (NOLOCK) WHERE cliente_id = @cliente_id)
		IF (@RowCount > 0)
			BEGIN
				BEGIN TRAN
					UPDATE Cliente
						SET
							nombre = @nombre,
							apellido = @apellido,
							tipo = @tipo
						WHERE cliente_id = @cliente_id
				COMMIT TRAN
			END
		END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH
END

----
CREATE PROCEDURE sp_delete_cliente
(
	@cliente_id INT
)
AS
BEGIN
DECLARE @RowCount INT = 0
	BEGIN TRY
		SET @RowCount = (SELECT COUNT(1) FROM Cliente WITH (NOLOCK) WHERE cliente_id = @cliente_id)
		IF (@RowCount > 0)
			BEGIN
				BEGIN TRAN
					DELETE Cliente
					WHERE cliente_id = @cliente_id
				COMMIT TRAN
			END
		END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH
ENDDROP DATABASE IF EXISTs TestSistemas;

CREATE DATABASE TestSistemas

USE TestSistemas

CREATE TABLE Cliente (
    cliente_id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    nombre NVARCHAR(50),
	apellido NVARCHAR(50),
    tipo NVARCHAR(5)
);

SELECT * FROM Cliente

----
CREATE PROCEDURE sp_create_cliente
(
	@nombre NVARCHAR(50),
	@apellido NVARCHAR(50),
	@tipo NVARCHAR(5)
)
AS 
BEGIN
	BEGIN TRY
		BEGIN TRAN
			INSERT INTO Cliente (nombre, apellido, tipo)
				VALUES (@nombre, @apellido, @tipo)
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH
END

----
CREATE PROCEDURE sp_read_cliente
AS 
BEGIN
	SELECT *
	FROM Cliente WITH (NOLOCK)
END

----
CREATE PROCEDURE sp_search_cliente
(
	@cliente_id INT
)
AS 
BEGIN
	SELECT *
	FROM Cliente WITH (NOLOCK)
	WHERE cliente_id = @cliente_id
END

----
CREATE PROCEDURE sp_update_cliente
(
	@cliente_id INT,
	@nombre NVARCHAR(50),
	@apellido NVARCHAR(50),
	@tipo NVARCHAR(5)
)
AS
BEGIN
DECLARE @RowCount INT = 0
	BEGIN TRY
		SET @RowCount = (SELECT COUNT(1) FROM Cliente WITH (NOLOCK) WHERE cliente_id = @cliente_id)
		IF (@RowCount > 0)
			BEGIN
				BEGIN TRAN
					UPDATE Cliente
						SET
							nombre = @nombre,
							apellido = @apellido,
							tipo = @tipo
						WHERE cliente_id = @cliente_id
				COMMIT TRAN
			END
		END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH
END

----
CREATE PROCEDURE sp_delete_cliente
(
	@cliente_id INT
)
AS
BEGIN
DECLARE @RowCount INT = 0
	BEGIN TRY
		SET @RowCount = (SELECT COUNT(1) FROM Cliente WITH (NOLOCK) WHERE cliente_id = @cliente_id)
		IF (@RowCount > 0)
			BEGIN
				BEGIN TRAN
					DELETE Cliente
					WHERE cliente_id = @cliente_id
				COMMIT TRAN
			END
		END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH
END