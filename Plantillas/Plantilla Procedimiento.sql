SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Agust�n Castillo
-- Create date: 20/02/2018
-- Description:	Realiza el mantenimiento de la empresa
-- =============================================
CREATE PROCEDURE PA_NombreProcedimiento
	--Par�metros
	@TipoOperacion		INT,

	@ESTADO				VARCHAR(2) = '00' OUT,
	@MENSAJE			VARCHAR(Max) = '' OUT
AS
BEGIN
	
	BEGIN TRY

		BEGIN TRANSACTION;
		SET NOCOUNT ON;
		IF @TipoOperacion = 1
		BEGIN
			--Codigo Insertar
		END
		ELSE IF @TipoOperacion = 2
		BEGIN
			--Codigo Actualizar
		END
		ELSE IF @TipoOperacion = 3
		BEGIN
			--Codigo Desactivar
		END
		

	END TRY
	BEGIN CATCH
		SELECT @ESTADO = '99', @MENSAJE = ERROR_MESSAGE();		
		ROLLBACK;
	END CATCH


END