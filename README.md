# Carrito de Compras + Pasarela de Pagos (PayPal)



<h1>Configuración Inicial</h1>


<h4>* Archivo de conexión (Web.config)</h4>

Se realizar los correspondientes cambios a la cadena de conexión, dicho archivo se llama <strong>Web.config</strong> en dónde Data Source= es el nombre de su servidor, Initial Catalog= el nombre de la base de datos, User ID=;Password= nombre de usuario y contraseña de acceso de su usuario configurado en su SGBD SQL SERVER. <strong>Único punto en dónde usted deberá conectar la base de datos a esta aplicación. NO se utilizan más cadenas de conexiones.</strong>

<img width="920" alt="raycast-untitled" src="https://user-images.githubusercontent.com/44457989/184071771-89563455-562a-49f4-bf5f-38c32917e3ac.png">


<h4>* Conectividad API Pasarela de Pagos PayPal (Web.config)</h4>

Dentro del mismo archivo Web.config, se encontrará la configuración general de la conexión hacia la API de PayPal. Para ello sedebe crear una cuenta en PayPal Developer ( https://developer.paypal.com ) y configurar su cuenta personal para utilizar el entorno de pruebas PayPal SandBox


Una vez configurada la cuenta, se debe ubicar los siguientes bloques de código, y sustituir por toda la información que la plataforma antes mencionada le solicita. Dónde ClientId= es su usuario encriptado generado por la plataforma y Secret= su contraseña de acceso generada por la plataforma. <strong>Por favor NO tocar UrlPaypal esa es la URL de comunicación con la API de los servicios de PayPal.</strong>


<img width="920" alt="raycast-untitled (1)" src="https://user-images.githubusercontent.com/44457989/184073047-72c1c6cf-326e-44c0-9b66-c1f29580a9e1.png">



<h4>* URL Redireccionamiento Pagos Procesados / Fallidos - PayPal ( Controllers/TiendaController.cs )</h4>


Ubicar el siguiente metodo <strong>public async Task<JsonResult> ProcesarPago(List<Carrito> oListaCarrito, Venta oVenta)</strong> Dentro de la carpeta Controllers - Archivo llamado TiendaController.cs <strong>Usted debe sustituir únicamente el https inicial el cuál corresponde a su servidor. Todo lo demás NO MODIFICAR</strong> Caso contrario, si modifica de manera incorrecta, o bien no realiza el ajuste pertinente, obtendrá un error 404 al momento de finalizar la compra.


<img width="920" alt="raycast-untitled (2)" src="https://user-images.githubusercontent.com/44457989/184073915-6a1f07dd-fd9e-48fd-bbac-07c037159622.png">



<h4>* Ajustes SMTP .NET - Envío de Correos Automáticos ( CapaNegocio/CN_Recursos.cs )</h4>

Se debe de configurar el SMTP que funciona únicamente para enviar correos automáticos al momento de reestablecer su contraseña si cualquier usuario pierde el acceso a la plataforma. Tomar nota de la imagen citada abajo y realizar los respectivos cambios que los comentarios hacen mención. <strong>Únicamente necesita configurar ya sea su correo personal como receptor de envío de correos automáticos para aplicaciones, o bien un correo institucional real dentro de un servicio de hosting.</strong>

<img width="920" alt="raycast-untitled (3)" src="https://user-images.githubusercontent.com/44457989/184075296-f7baa64d-28db-4300-acd9-bc3e7d83602d.png">



</p>






<p>Este sistema a nivel de código y base de datos se encuentra distribuido de la siguiente manera:<ul><li>Base de Datos:</li><ul><li>10 Tablas + 1 Tabla Virtual.</li><li>6 Procedimientos Almacenados.</li><li>1 Función.</li></ul></ul><ul><li>Sistema:</li><ul><li>Lenguaje de Programación C# ASP.NET - ADO.NET.</li><li>Patrón MVC (Modelo, Vista, Controlador).</li><li>Gestiones AJAX, JQuery.</li><li>Desarrollo basado en capas (Capa de Negocio, Capa de Datos, Capa de Presentación).</li><li>Complementos JQuery, Javascript</li><li>Envío de Correos Automáticos - SMTP .NET.</li><li>Plantilla Bootstrap.</li><li>Mantenimientos y gestiones asíncronos, es decir, todo se realiza en tiempo real sin refrescar la página.</li></ul></ul></p>





<h2> Modificaciones a la base de datos </h2>

```sql
DELETE FROM CARRITO;


-- Eliminar primero los productos, ya que dependen de las marcas
DELETE FROM PRODUCTO;

-- Luego eliminar las marcas
DELETE FROM MARCA;


DBCC CHECKIDENT ('CARRITO', RESEED, 0);
DBCC CHECKIDENT ('PRODUCTO', RESEED, 0);
DBCC CHECKIDENT ('MARCA', RESEED, 0);




