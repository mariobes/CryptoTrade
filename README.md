<h1>CryptoTrade API RESTful</h1>
<hr>
<b>Proyecto de final de curso.</b><br>
CryptoTrade es una API RESTful construida con C# y .NET 6, diseñada para gestionar transacciones de activos como criptomonedas y acciones. Esta API permite una comunicación eficiente entre el cliente y el servidor, procesando solicitudes y garantizando que la lógica de negocio sea robusta y escalable.
<hr style="height: 4px;">
<h2>Stack Tecnológico</h2>
<hr>
<ul>
  <li><b>Backend: </b>Construido con C# y .NET 6, siguiendo una arquitectura por capas para garantizar la separación de responsabilidades y facilitar el mantenimiento a largo plazo.</li>
  <li><b>Frontend: </b>La interfaz de usuario es una aplicación web desarrollada con Vue.js. Para más detalles, visita el repositorio: <a href="https://github.com/mariobes/vue-CryptoTrade">CryptoTrade-Vue</a></li>
  <li><b>Base de datos: </b>SQL Server con Entity Framework Core como ORM, para gestionar de manera eficiente las operaciones de base de datos y asegurar un rendimiento óptimo.</li>
  <li><b>Autenticación: </b>Implementación de JSON Web Tokens (JWT) para proporcionar un sistema de autenticación seguro y manejar el control de acceso.</li>
  <li><b>Docker: </b>El proyecto está dockerizado usando Docker Compose, lo que permite levantar la base de datos, la API y el frontend con un solo comando, facilitando la configuración y el despliegue del entorno de desarrollo y producción.</li>
</ul>
<hr style="height: 4px;">
<h2>Configuración del proyecto</h2>
<hr>
<h3>Clonar el repositorio y entrar en él</h3>

```sh
git clone https://github.com/tu-usuario/CryptoTrade.git
cd CryptoTrade
```
<hr>
<h3>Configurar la base de datos y asegurarte de que la conexión configurada en el archivo appsettings.json sea la correcta.</h3>
<hr style="height: 4px;">
<h2>Cómo arrancar el proyecto</h2>
<hr>
<h3>Opción recomendada: Iniciar con Docker</h3>
<h4>Instrucción para levantar el Docker Compose</h4>

```sh
docker-compose up -d
```
<h4>Instrucción para borrar el Docker Compose</h4>

```sh
docker-compose down
```
<hr style="height: 4px;">
<h3>Otra opción: Iniciar desde tu IDE</h3>
<h4>Si prefieres no utilizar Docker, puedes iniciar el proyecto desde tu IDE, por ejemplo Visual Studio Code.</h4>
<ul>
  <li>Abrir el proyecto en Visual Studio Code.</li>
  <li>Configurar la base de datos y el arranque del proyecto.</li>
  <li>Ejecutar "Run" o presionar F5 y en la pestaña que se abra añadir a la ruta "/swagger".</li>
</ul>