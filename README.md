Farmacia CRUD con Arquitectura Hexagonal

Este proyecto implementa un sistema CRUD (Create, Read, Update, Delete) para un laboratorio farmacéutico utilizando C#, .NET Core 8.0, y arquitectura hexagonal (Ports and Adapters). Está dividido en dos aplicaciones:

FarmaciaServicioApi: Una Web API que gestiona operaciones CRUD conectándose a una base de datos MySQL.
FarmaciaHexagonalApp: Una aplicación de consola que consume la API utilizando arquitectura hexagonal.

La entidad principal es ProductoFarmaceutico (ID, Nombre, Fórmula, Fecha de Caducidad, Precio).
Tabla de Contenidos

Requisitos Previos
Estructura del Proyecto
Configuración Inicial
Configurar MySQL
Clonar el Repositorio
Configurar FarmaciaServicioApi
Configurar FarmaciaHexagonalApp


Ejecutar el Proyecto
Solución de Problemas
Extender el Proyecto
Licencia

Requisitos Previos

Visual Studio 2022 con las cargas de trabajo ".NET desktop development" y "ASP.NET and web development".
.NET SDK 8.0 o superior (descargar).
MySQL Server 8.0 o superior (descargar).
MySQL Workbench (opcional, para administrar la base de datos).
Acceso a internet para descargar paquetes NuGet.

Estructura del Proyecto
Farmacia
├── FarmaciaServicioApi
│   ├── Models/ProductoFarmaceutico.cs       # Entidad de dominio
│   ├── Data/FarmaciaDbContext.cs            # Contexto de Entity Framework
│   ├── Controllers/ProductosController.cs   # Endpoints CRUD
│   └── appsettings.json                    # Configuración (e.g., conexión MySQL)
├── FarmaciaHexagonalApp
│   ├── Domain
│   │   ├── Entities/ProductoFarmaceutico.cs # Entidad
│   │   ├── Ports/IRepositorioProducto.cs   # Puerto para repositorio
│   ├── Application/Services/ProductoService.cs # Lógica de negocio
│   ├── Infrastructure/Adapters/ProductoHttpAdapter.cs # Adaptador HTTP
│   └── Program.cs                          # Ejemplo de consola
└── README.md

Configuración Inicial
1. Configurar MySQL

Instala MySQL Server si no lo tienes.
Crea una base de datos llamada FarmaciaDB:CREATE DATABASE FarmaciaDB;


Anota las credenciales de MySQL (e.g., usuario: root, contraseña: tu_password).

2. Clonar el Repositorio
Clona el repositorio desde GitHub:
git clone https://github.com/<tu_usuario>/farmacia.git

O descarga el ZIP y descomprímelo.
Abre la solución Farmacia.sln en Visual Studio 2022.
3. Configurar FarmaciaServicioApi
3.1. Instalar Paquetes NuGet
En Visual Studio:

Haz clic derecho en FarmaciaServicioApi > "Administrar paquetes NuGet".
Instala:
Pomelo.EntityFrameworkCore.MySql (versión 8.0.0 o compatible)
Microsoft.EntityFrameworkCore.Tools
Microsoft.AspNetCore.Mvc.NewtonsoftJson


O usa la terminal:cd FarmaciaServicioApi
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson



3.2. Configurar la Cadena de Conexión

Abre FarmaciaServicioApi/appsettings.json.
Actualiza la sección ConnectionStrings:"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=FarmaciaDB;User=root;Password=tu_password;"
}

Reemplaza tu_password con tu contraseña de MySQL.

3.3. Aplicar Migraciones

Abre la Consola del Administrador de Paquetes (Herramientas > Administrador de Paquetes NuGet > Consola).
Selecciona FarmaciaServicioApi como proyecto predeterminado.
Ejecuta:Add-Migration InitialCreate
Update-Database

Esto crea la tabla ProductosFarmaceuticos en FarmaciaDB.

4. Configurar FarmaciaHexagonalApp
4.1. Instalar Paquetes NuGet

Haz clic derecho en FarmaciaHexagonalApp > "Administrar paquetes NuGet".
Instala:
System.Net.Http.Json
Microsoft.Extensions.DependencyInjection
Microsoft.Extensions.Http


O usa la terminal:cd FarmaciaHexagonalApp
dotnet add package System.Net.Http.Json
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package Microsoft.Extensions.Http



4.2. Verificar la URL de la API

Abre FarmaciaHexagonalApp/Infrastructure/Adapters/ProductoHttpAdapter.cs.
Asegúrate de que BaseUrl coincida con el puerto de FarmaciaServicioApi:private const string BaseUrl = "https://localhost:<puerto>/api/Productos";

Encuentra el puerto en FarmaciaServicioApi > Propiedades > Depuración > URL de la aplicación (e.g., https://localhost:7236).

Ejecutar el Proyecto

En Visual Studio, haz clic derecho en la solución > Propiedades > Proyectos de inicio.
Selecciona "Varios proyectos de inicio":
FarmaciaServicioApi: Acción = "Iniciar".
FarmaciaHexagonalApp: Acción = "Iniciar".
Asegúrate de que FarmaciaServicioApi esté arriba (se inicia primero).


Presiona F5 para ejecutar.
La API estará disponible en https://localhost:<puerto>/swagger para pruebas.
La consola de FarmaciaHexagonalApp ejecutará:
Crear un producto (Aspirina).
Listar productos.
Actualizar un producto.
Eliminar un producto.
Mostrar la lista final.



Solución de Problemas

Error de conexión MySQL: Verifica appsettings.json y que MySQL Server esté activo.
Error de puerto: Confirma que BaseUrl en ProductoHttpAdapter.cs coincide con el puerto de la API.
Certificado HTTPS: Si hay problemas, ejecuta:dotnet dev-certs https --trust


Base de datos vacía: Usa Swagger para crear productos si la consola falla al actualizar/eliminar.

Extender el Proyecto

Agrega autenticación (e.g., JWT) a la API.
Convierte FarmaciaHexagonalApp en una aplicación web (Blazor, MVC).
Implementa pruebas unitarias (ProductoService) y de integración (ProductoHttpAdapter).

Licencia
Este proyecto está bajo la Licencia MIT.
