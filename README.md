Historia de Usuario: Prueba - Desarrollador.docx

Arquitectura:
(Frontend) WebAPI + motor Typescript -> (Backend) Llamada RestAPI -> Modelo Inyección de Dependencias -> IRequest -> IRequestHandler (Inicializa EFCore) -> DBContext (Uso de EFCore API)

NetCore 8.0 LTS RestAPI + WebAPI que implementa las siguientes características:
- DDD + CQRS.
- Command Handler NetCore 8.0+ .NET format. (Isolación de capas de seguridad controladas por memoria)
- HttpStatus, RestFul, Inyección de dependencia.
- Información parametrizada desde appsetting. 
- Entity Framework Core (genera bases de datos y tablas dinámicamente desde Clases en RestAPI)
- Api Key que controla la seguridad de los servicios Rest, mediante log-in de Usuario.

Funcionalidad de RestAPI:
- Crea / Lee / Actualiza / Elimina una Región. 
- Crea / Lee / Actualiza / Elimina una Comuna. 

Uso:
- Instalar Visual Studio 2022
- Instalar SQL Express 2017
- Instalar SQM Management Studio
- Instalar Postman
- Abrir SQL Management Studio. Usar inicio de sesión de Windows en SQL Express, agregar la base de datos "Prueba".
- En SQL Explorer, encima de base de datos "Prueba", pulsar botón derecho del mouse, Nueva Query y luego, copiar y pegar todo el contenido dentro de las comillas (sin incluírlas), seleccionar todo y ejecutar script:

"
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
CREATE PROCEDURE ObtenerRegiones_SP 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here
	SELECT [Idregion]
      ,[region]
  FROM [Prueba].[dbo].[Regiones]
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
CREATE PROCEDURE ObtenerComunas_SP 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here
	SELECT [Idcomuna]
      ,[Idregion]
      ,[comuna]
      ,[informacionadicional]
  FROM [Prueba].[dbo].[Comunas]
END
GO

"

- Abrir RestAPI (/RestAPISwagger), ejecutar en segundo plano y no cerrar. 
- Abrir WebAPI (/ClientWebAPI) y ejecutarlo desde el navegador. Realizar operaciones de mantención mediante el navegador. Utilizará tanto la aplicación web como RestAPI para acceso a microservicios.
  Si no hay datos, se van a generar automáticamente.
  
  
Opcional:
   Si se desea, se puede cargar un archivo de Postman collection en Postman para testear los endpoint directamente. (/RestAPISwagger/UnitTestMicroservicios_Postman.json)