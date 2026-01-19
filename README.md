# DoctorApp

Aplicación web de gestión médica con frontend en Angular 16 y backend en ASP.NET Core.

## 📋 Descripción

**DoctorApp** es una aplicación web completa para consultorios médicos que permite gestionar usuarios y especialidades médicas. El sistema incluye autenticación JWT, gestión de usuarios, y CRUD de especialidades con una arquitectura moderna y escalable.

## 🏗️ Arquitectura

### Backend (ASP.NET Core)
- **Capa de Presentación**: Controladores API
- **Capa de Lógica de Negocio**: Servicios (BLL)
- **Capa de Acceso a Datos**: Repositorios y Entity Framework Core
- **Capa de Modelos**: Entidades y DTOs

### Frontend (Angular 16)
- **Módulos**: usuario, especialidad, compartido
- **Componentes**: Reutilizables con Angular Material
- **Servicios**: Consumo de API REST
- **Rutas**: Lazy loading y guards

## 🧱 Patrones de Diseño

### Backend
- **Repository Pattern**: Abstracción del acceso a datos
- **Unit of Work**: Gestión de transacciones
- **Service Layer**: Lógica de negocio separada
- **DTO Pattern**: Transferencia de datos limpia
- **Dependency Injection**: Inyección de dependencias

### Frontend
- **Module Pattern**: Módulos organizados
- **Component Pattern**: Componentes reutilizables
- **Service Pattern**: Servicios para lógica compartida
- **Observer Pattern**: RxJS para manejo de eventos

## 🚀 Inicialización

### Backend
1. **Abrir el proyecto en Visual Studio**
   ```bash
   cd DoctorAppBackend
   # Abrir DoctorAppBackend.sln en Visual Studio

🔧 Configuración
Backend
JWT Token: Configurar clave secreta en appsettings.json
Base de datos: Configurar conexión en appsettings.Development.json
CORS: Ya configurado para permitir cualquier origen

🔐 Autenticación
JWT Tokens: Generados con HMACSHA512
Duración: 7 días
Endpoints:
POST /api/usuario/registro
POST /api/usuario/login
📚 Endpoints Principales
Usuarios
GET /api/usuario - Listar usuarios (requiere auth)
GET /api/usuario/{id} - Obtener usuario (requiere auth)
POST /api/usuario/registro - Registrar usuario
POST /api/usuario/login - Login
Especialidades
GET /api/especialidad - Listar especialidades
POST /api/especialidad - Crear especialidad
PUT /api/especialidad - Actualizar especialidad
DELETE /api/especialidad/{id} - Eliminar especialidad
🛠️ Tecnologías
Backend
ASP.NET Core 6+
Entity Framework Core
JWT Authentication
AutoMapper
Swagger/OpenAPI
Frontend
Angular 16
Angular Material
RxJS
TypeScript
HTML5/CSS3