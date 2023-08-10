DECLARE @IDUSUARIO INT = 2;
SELECT * FROM [dbo].[webpages_Membership]
SELECT * FROM [dbo].[webpages_Roles]
SELECT * FROM [dbo].[webpages_UsersInRoles]

SELECT * FROM dbo.USUARIO;
/*
SP_HELP 'dbo.USUARIO'
--REGISTRAR UN PLAN DE TIENDA
--@I_PLANTIENDA ,@I_PLAN, @V_TIPO, @I_CODIGO_TIENDA, @V_USER NVARCHAR(50)
[dbo].[USP_INS_PLANTIENDA] 0, 2, 'M', 15, 'script'
*/
SELECT * FROM [dbo].[TIENDA];
SELECT * FROM [dbo].[PLANES];
--INSERT INTO PLANES VALUES(34.99, 'Inscripción', null, '1', 'PEN')
/*UPDATE PLANES 
SET MONTO_PLAN = 350.00
WHERE I_PLAN = 3
*/
DELETE FROM [dbo].[PLAN_TIENDA]
select * FROM [dbo].[PLAN_TIENDA]

SELECT * FROM PARAMETROVALOR;
select * from [dbo].[webpages_UsersInRoles] 
--update [dbo].[webpages_UsersInRoles] 
--set roleid = 1
--where  userid = 22


DECLARE @idtienda INT = 17;
select * from dbo.USUARIO where i_tienda=@idtienda;

select * from [dbo].[TIENDA] where i_codigo_tienda = @idtienda
select * from [dbo].[PLAN_TIENDA] where i_codigo_tienda = @idtienda
select * from dbo.producto where i_codigo_tienda = @idtienda
select * from dbo.producto_caracteristica where i_codigo_producto in (select i_codigo_producto from dbo.producto where i_codigo_tienda = @idtienda)
select * from dbo.imagen_producto where i_codigo_producto in (select i_codigo_producto from dbo.producto where i_codigo_tienda = @idtienda)
select * from [dbo].[CARRITO_COMPRA] WHERE I_CODIGO_TIENDA IN (@idtienda)
select * from [dbo].DETALLE_COMPRA WHERE I_CODIGO_PRODUCTO IN (SELECT I_CODIGO_PRODUCTO FROM [dbo].[CARRITO_COMPRA] WHERE I_CODIGO_TIENDA IN (@idtienda))

/*
-- ELIMINAR UNA TIENDA
DECLARE @idtienda INT = 18;
update dbo.USUARIO set i_tienda = null where i_tienda=@idtienda;
DELETE [dbo].[CARRITO_COMPRA] WHERE I_CODIGO_TIENDA IN (@idtienda)
delete from [dbo].[IMAGEN_PRODUCTO] where i_codigo_producto in (select i_codigo_producto from dbo.producto where i_codigo_tienda = @idtienda)
delete from [dbo].[PRODUCTO_CARACTERISTICA] where i_codigo_producto in (select i_codigo_producto from dbo.producto where i_codigo_tienda = @idtienda)
delete from [dbo].[PRODUCTO] where i_codigo_tienda = @idtienda
DELETE FROM [dbo].[PLAN_TIENDA] where i_codigo_tienda = @idtienda
DELETE FROM [dbo].DETALLE_COMPRA WHERE I_CODIGO_PRODUCTO IN (SELECT I_CODIGO_PRODUCTO FROM [dbo].[CARRITO_COMPRA] WHERE I_CODIGO_TIENDA IN (@idtienda))
DELETE [dbo].[CARRITO_COMPRA] WHERE I_CODIGO_TIENDA IN (@idtienda)
delete [dbo].[TIENDA] where i_codigo_tienda = @idtienda

*/


SELECT * FROM [dbo].[CATEGORIA];
	

SELECT * FROM dbo.USUARIO WHERE IdUsuario = @IDUSUARIO;
SELECT * FROM dbo.CARRITO_COMPRA WHERE I_CODIGO_USUARIO = @IDUSUARIO
SELECT * FROM dbo.DETALLE_COMPRA WHERE I_CODIGO_COMPRA IN (SELECT I_CODIGO_COMPRA 
															FROM dbo.CARRITO_COMPRA 
															WHERE I_CODIGO_USUARIO = @IDUSUARIO);

  DECLARE @IDPRODUCTO INT = 20042;
  SELECT * FROM dbo.PRODUCTO WHERE I_CODIGO_PRODUCTO = @IDPRODUCTO
  SELECT * FROM dbo.PRODUCTO_CARACTERISTICA WHERE I_CODIGO_PRODUCTO = @IDPRODUCTO
  SELECT * FROM dbo.IMAGEN_PRODUCTO WHERE I_CODIGO_PRODUCTO = @IDPRODUCTO
--UPDATE dbo.USUARIO SET V_USUARIO = 'ELA' WHERE IdUsuario = 3
/*
INSERT INTO dbo.CATEGORIA
        ( V_CATEGORIA ,
          V_USER_CREATE ,
          D_DATE_CREATE ,
          V_USER_UPDATE ,
          D_DATE_UPDATE ,
          B_ACTIVE
        )
VALUES  ( N'SERVICIO GENERAL' , -- V_CATEGORIA - nvarchar(100)
          N'SISADRI' , -- V_USER_CREATE - nvarchar(100)
          GETDATE() , -- D_DATE_CREATE - datetime
          N'' , -- V_USER_UPDATE - nvarchar(100)
          NULL , -- D_DATE_UPDATE - datetime
          '1'  -- B_ACTIVE - bit
        )
*/


/*
UPDATE dbo.CARRITO_COMPRA SET I_CODIGO_TIENDA = 4
INSERT INTO dbo.TIENDA
        ( V_TIENDA ,
          V_RUC ,
          V_DIRECCION ,
          V_USER_CREATE ,
          D_DATE_CREATE ,
          V_USER_UPDATE ,
          D_DATE_UPDATE ,
          B_ACTIVE ,
          V_WEB
        )
VALUES  ( N'LIBRERIA ELY' , -- V_TIENDA - nvarchar(150)
          N'' , -- V_RUC - nvarchar(15)
          N'CUARTEL GENERAL FAP' , -- V_DIRECCION - nvarchar(200)
          N'SISADRI' , -- V_USER_CREATE - nvarchar(100)
          GETDATE() , -- D_DATE_CREATE - datetime
          N'' , -- V_USER_UPDATE - nvarchar(100)
          GETDATE() , -- D_DATE_UPDATE - datetime
          '1' , -- B_ACTIVE - bit
          N'sisadri-ecommerce'  -- V_WEB - nvarchar(250)
        );
*/
/*

UPDATE TIENDA SET v_web = 'localhost' WHERE I_codigo_tienda = 1;
UPDATE TIENDA SET v_web = 'SISADRI-ECOMMERCE.AZUREWEBSITES.NET' WHERE I_codigo_tienda = 4;
DELETE dbo.USUARIO;
DELETE [dbo].[webpages_Membership];
DELETE [dbo].[webpages_UsersInRoles];
*/
/*
  
  */



  