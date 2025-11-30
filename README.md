# FoodOrder

A layered .NET 8.0 food ordering system with real-time notifications, background job processing, and JWT authentication.

## Architecture

The project follows a **layered architecture** pattern with clear separation of concerns:

- **FoodOrder.Entities** - Domain models (`Food`, `Ingredient`, `Order`, `AppUser`) and DTOs
- **FoodOrder.Data** - Data access layer with Entity Framework Core, DbContext, and Repository pattern
- **FoodOrder.Logic** - Business logic layer with validation and AutoMapper for DTO mapping
- **FoodOrder.Endpoint** - ASP.NET Core Web API with controllers, SignalR hub, and middleware
- **FoodOrder.Client** - Console client application for testing API endpoints and SignalR connections

## Technologies & NuGet Packages

### Backend (FoodOrder.Endpoint)
- **Microsoft.EntityFrameworkCore** (8.0.22) - ORM for database operations
- **Microsoft.EntityFrameworkCore.SqlServer** (8.0.22) - SQL Server provider
- **Microsoft.EntityFrameworkCore.Proxies** (8.0.22) - Lazy loading support
- **Microsoft.AspNetCore.Identity.EntityFrameworkCore** (8.0.13) - User authentication and authorization
- **Microsoft.AspNetCore.Authentication.JwtBearer** (8.0.13) - JWT token authentication
- **Microsoft.AspNetCore.SignalR.Common** (10.0.0) - Real-time communication
- **Hangfire** (1.8.22) - Background job processing
- **Hangfire.SqlServer** (1.8.22) - SQL Server storage for Hangfire
- **AutoMapper** (15.1.0) - Object-to-object mapping
- **Swashbuckle.AspNetCore** (6.4.0) - Swagger/OpenAPI documentation
- **Microsoft.AspNetCore.OpenApi** (8.0.8) - OpenAPI support

### Data Layer (FoodOrder.Data)
- **Microsoft.EntityFrameworkCore** (8.0.22)
- **Microsoft.EntityFrameworkCore.SqlServer** (8.0.22)
- **Microsoft.EntityFrameworkCore.Proxies** (8.0.22)
- **Microsoft.AspNetCore.Identity.EntityFrameworkCore** (8.0.13)

### Logic Layer (FoodOrder.Logic)
- **AutoMapper** (15.1.0) - DTO mapping
- **SlugGenerator** (2.0.2) - URL-friendly slug generation

### Client (FoodOrder.Client)
- **Microsoft.AspNetCore.SignalR.Client** (10.0.0) - SignalR client for real-time communication
- **Newtonsoft.Json** (13.0.4) - JSON serialization
- Auto-generated API client (NSwag) - Type-safe HTTP client for API calls

## Features

### Core Functionality
- **Food Management** - CRUD operations for food items with ingredients
- **Ingredient Management** - Manage ingredients with calorie information
- **Order Management** - Create orders with multiple food items
- **User Authentication** - JWT-based authentication with role-based authorization (Admin role)

### Advanced Features
- **Real-time Notifications** - SignalR hub (`FoodHub`) for broadcasting order creation events
- **Background Jobs** - Hangfire integration for delayed order notifications
- **Lazy Loading** - Entity Framework proxies for efficient data loading
- **DTO Mapping** - AutoMapper for clean separation between entities and DTOs
- **API Documentation** - Swagger/OpenAPI with JWT authentication support
- **Validation** - Global validation filters and exception handling
- **Slug Generation** - Automatic URL-friendly slugs for food items

## Console Client

The `FoodOrder.Client` console application:
- Connects to the API via HTTP client (auto-generated with NSwag)
- Establishes SignalR connection to receive real-time notifications
- Creates orders with configurable delay
- Displays elapsed time while waiting for order completion
- Receives notifications when orders are processed via Hangfire background jobs

## Database

- **SQL Server** - Primary database for application data
- **SQL Server** - Separate database for Hangfire job storage
- **Entity Framework Migrations** - Code-first migrations for schema management

## API Endpoints

- `GET /Food` - Get all foods
- `POST /Food` - Create food (requires authentication)
- `DELETE /Food` - Delete food (requires Admin role)
- `GET /Ingredient` - Get ingredients
- `POST /Ingredient` - Create ingredient
- `GET /Order` - Get all orders
- `POST /Order` - Create order with delayed notification
- `DELETE /Order` - Delete order (requires Admin role)
- `POST /Auth` - Authentication endpoints

## SignalR Hub

- **Endpoint**: `/foodHub`
- **Events**: `newOrder` - Broadcasts order ID when order is created (via Hangfire)

## Configuration

Configure connection strings in `appsettings.json`:
- `CustomConnectionStrings:FoodDb` - Main database connection
- `CustomConnectionStrings:HangfireDb` - Hangfire database connection
- `jwt:key` - JWT signing key for token generation


## Target Framework

- **.NET 8.0** across all projects
