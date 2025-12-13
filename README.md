Biblioteca Universitaria – Backend API

Backend del sistema **Biblioteca Universitaria**, desarrollado como parte del examen del curso **Desarrollo de Servicios Web I**.  
La aplicación expone una API REST que permite la gestión de **libros** y **préstamos**, aplicando reglas de negocio como control de stock y devolución de préstamos.

El proyecto sigue una **arquitectura hexagonal**, separando claramente dominio, aplicación, infraestructura y capa de presentación (API).


## Arquitectura

El proyecto está organizado bajo **arquitectura hexagonal**, con la siguiente estructura:

GestionBiblioteca
│
├── GestionBiblioteca.Domain
│ ├── Entities
│ ├── Exceptions
│ └── Interfaces
│
├── GestionBiblioteca.Application
│ ├── DTOs
│ ├── Interfaces
│ └── Services
│
├── GestionBiblioteca.Infrastructure
│ ├── Persistence
│ ├── Repositories
│ └── Configuration
│
└── GestionBiblioteca.API
├── Controllers
└── Program.cs


## Capas
- **Domain**: Entidades y reglas de negocio.
- **Application**: Casos de uso y lógica de aplicación.
- **Infrastructure**: Acceso a datos y configuración de persistencia.
- **API**: Exposición de endpoints REST.


## Tecnologías Utilizadas

- **.NET 9**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **MySQL**
- **Arquitectura Hexagonal**
- **Inyección de Dependencias**
- **DTOs**
- **Axios (consumo desde frontend)**

---

## Configuración del Proyecto

### Base de Datos
- Motor: **MySQL**
- Base de datos: `biblioteca_db`

Las entidades principales son:
- **Book**
- **Loan**

---

## Endpoints Principales

### Libros (`/api/Book`)

- **GET** `/api/Book` → Listar libros  
- **GET** `/api/Book/{id}` → Obtener libro por ID  
- **POST** `/api/Book` → Registrar libro  
- **PUT** `/api/Book/{id}` → Editar libro  
- **POST** `/api/Book/{id}/baja` → Dar de baja un libro  

---

### Préstamos (`/api/Loan`)

- **GET** `/api/Loan/Active` → Listar préstamos activos  
- **POST** `/api/Loan` → Registrar préstamo  
- **PUT** `/api/Loan/{id}/Return` → Devolver préstamo  

---

## Reglas de Negocio Implementadas

- No se permite registrar un préstamo si el stock del libro es **0**.  
- Al registrar un préstamo, el **stock del libro disminuye** automáticamente.  
- Al devolver un préstamo, el **stock del libro se incrementa**.  
- Solo se listan **préstamos activos**.  
- Manejo de errores mediante **excepciones de dominio**.

---

## Observaciones

- El backend está preparado para ser consumido por un **frontend independiente**.  
- Se implementó **separación de responsabilidades** y **buenas prácticas**.  
- El proyecto cumple con **todos los requisitos solicitados en el examen**.


