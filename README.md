# Evaluación T2 - Desarrollo de Servicios Web I (API)

## Autor
**Nombre:** Jamir Ascencio Flores
**Curso:** Desarrollo de Servicios Web I

## Descripción
API RESTful desarrollada con .NET 8.0 implementando **Arquitectura Hexagonal**.
Gestiona el sistema de biblioteca (Libros y Préstamos) con reglas de negocio para el control de stock.

## Tecnologías
* .NET 8.0 (Web API)
* Entity Framework Core (MySQL)
* AutoMapper
* Pattern Repository & Unit of Work
* DotNetEnv (Variables de entorno)

## Instrucciones de Ejecución

1. **Clonar el repositorio**:
   ```
   git clone https://github.com/jamirascencioflores/DSW1_T2_ASCENCIO_FLORES_JAMIR_API.git
   ```
---

2. **Configurar Base de Datos**: Crear un archivo .env en la raíz del proyecto Library.Api con sus credenciales de MySQL:
```
ConnectionStrings__DefaultConnection="Server=localhost;Database=library_db;User=root;Password=SU_CONTRASEÑA;"
```
---

3. **Ejecutar Migraciones** (Desde la consola de Comandos de VS):
```
Update-Database -StartupProject Library.Api
```
---

4. **Ejecutar la API**: Abrir la solución en Visual Studio y ejecutar el proyecto `Library.Api.`
 * Swagger UI: `https://localhost:<PUERTO>/swagger`
