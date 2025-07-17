# 🐟 Módulo de Producción de Piscinas Camaroneras

Este proyecto es un sistema web desarrollado con **ASP.NET Core MVC**, destinado a gestionar el proceso de producción en **piscinas camaroneras**, incluyendo registro de pedidos, control de productos y usuarios autenticados. Forma parte del trabajo de tesis de la carrera de Ingeniería de Software.

## 🚀 Funcionalidades principales

- Registro y autenticación de usuarios (con Identity).
- Creación, edición y anulación de pedidos.
- Selección de piscina y productos por pedido.
- Visualización detallada de cada pedido.
- Filtro de pedidos por fechas.
- Diseño responsive moderno (Bootstrap 5).
- Panel de administración solo visible tras autenticación.
- Despliegue en Microsoft Azure App Service.

## 🧱 Tecnologías utilizadas

- **ASP.NET Core MVC 6/7**
- **C#**
- **Razor Pages**
- **Entity Framework Core**
- **SQL Server / Azure SQL**
- **Bootstrap 5**
- **Azure App Service (publicado)**


```plaintext
ModuloProduccionPiscina/
│
├── Controllers/
├── Models/
│   └── ViewModels/
├── Views/
│   ├── Account/
│   ├── Pedido/
│   └── Shared/
├── wwwroot/
│   ├── css/
│   ├── images/
│   └── js/
├── appsettings.json
└── Program.cs

