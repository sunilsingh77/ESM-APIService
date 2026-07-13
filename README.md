# Employee Skills Management System - ASP.NET Core Web API

## Overview

The Employee Skills Management System (ESMS) API is a RESTful backend application developed using **ASP.NET Core 9**, **Clean Architecture**, and **Domain-Driven Design (DDD)** principles.

The API provides secure and scalable services for managing employees, departments, skills, employee skill mappings, authentication, and authorization. It is designed as an enterprise-ready application emphasizing maintainability, testability, and separation of concerns.

The API is consumed by the Angular frontend and uses SQL Server as the primary database.

---

# Technology Stack

| Technology | Version |
|------------|----------|
| ASP.NET Core | .NET 9 |
| Entity Framework Core | 9 |
| SQL Server | 2022 |
| ASP.NET Core Identity | ✓ |
| JWT Authentication | ✓ |
| Refresh Token | ✓ |
| FluentValidation | ✓ |
| Serilog | ✓ |
| Swagger / OpenAPI | ✓ |
| Dependency Injection | Built-in |
| C# | Latest |

---

# Architecture

The solution follows **Clean Architecture** with **Domain-Driven Design (DDD)**.

```
Presentation (API)
        │
        ▼
Application Layer
        │
        ▼
Domain Layer
        ▲
        │
Infrastructure Layer
        │
        ▼
SQL Server
```

Dependencies

```
API
 │
 ▼
Application
 │
 ├── Domain
 │
 └── Infrastructure
       │
       └── Domain
```

The Domain layer has no dependency on any other project.

---

# Solution Structure

```
EmployeeSkills.API
│
├── Controllers
├── Middleware
├── Extensions
├── Models
├── Seed
├── Program.cs
└── appsettings.json

EmployeeSkills.Application
│
├── DTOs
├── Interfaces
├── Services
├── Validators
├── Mapping
└── Behaviors

EmployeeSkills.Domain
│
├── Entities
├── Interfaces
├── Enums
├── Common
├── Events
└── ValueObjects

EmployeeSkills.Infrastructure
│
├── Persistence
├── Identity
├── Repositories
├── Services
├── Configurations
└── Migrations
```

---

# Features

## Authentication

- JWT Authentication
- Refresh Token
- ASP.NET Core Identity
- Role Based Authorization
- Login
- Logout
- User Registration
- Token Refresh

---

## Department Management

- Create Department
- Update Department
- Delete Department
- Search Department
- Department Listing

---

## Employee Management

- Create Employee
- Update Employee
- Delete Employee
- Search Employee
- Department Assignment

---

## Skill Management

- Create Skill
- Update Skill
- Delete Skill
- Skill Categories

---

## Employee Skill Management

- Assign Skill
- Update Skill Mapping
- Delete Skill Mapping
- Proficiency Level
- Years of Experience
- Primary Skill

---

## Dashboard

Provides summary information including

- Employee Count
- Department Count
- Skill Count
- Employee Skill Count

---

# Security

The API is secured using

- JWT Bearer Authentication
- Refresh Token
- Role Based Authorization
- Identity Password Policies
- HTTPS
- Authorization Policies

---

# Validation

Validation is implemented using FluentValidation.

Examples

- Department Name Required
- Employee Email Validation
- Skill Name Validation
- Duplicate Checks
- Business Rules

---

# Logging

Logging is implemented using Serilog.

Features

- Console Logging
- File Logging
- Daily Rolling Files
- Request Logging
- Exception Logging
- Structured Logging

Example

```
[INF]

Employee created successfully.

EmployeeId : 12
```

---

# Exception Handling

Global exception handling is implemented using custom middleware.

Features

- Centralized Exception Handling
- Standard Error Response
- TraceId
- Error Code
- HTTP Status Code
- Logging

Example Response

```json
{
  "success": false,
  "statusCode": 500,
  "message": "An unexpected error occurred.",
  "errorCode": "SERVER_ERROR",
  "traceId": "0HNN07PTCKFL0"
}
```

---

# API Documentation

Swagger is enabled.

Launch

```
https://localhost:7093/swagger
```

Features

- JWT Authentication
- Endpoint Documentation
- Request Models
- Response Models

---

# Database

Database Engine

```
SQL Server
```

ORM

```
Entity Framework Core
```

Features

- Code First
- Migrations
- Seed Data
- Identity Tables

---

# Seed Data

Initial data includes

- Roles
- Users
- Departments
- Employees
- Skills
- Employee Skills

---

# Repository Pattern

The application uses the Repository Pattern.

Repositories

- DepartmentRepository
- EmployeeRepository
- SkillRepository
- EmployeeSkillRepository

---

# Unit of Work

The application uses Unit of Work to manage database transactions.

Benefits

- Single transaction
- Consistent commits
- Better testability

---

# Dependency Injection

Service registration is organized into dedicated extension classes.

```
ApplicationServiceRegistration

PersistenceRegistration

RepositoryRegistration

IdentityRegistration

SerilogRegistration
```

---

# Running the API

## Restore Packages

```bash
dotnet restore
```

---

## Apply Database Migration

```bash
Update-Database
```

or

```bash
dotnet ef database update
```

---

## Run

```bash
dotnet run
```

---

# Default URLs

```
https://localhost:7093

http://localhost:5093
```

---

# Configuration

Configuration is stored in

```
appsettings.json

appsettings.Development.json
```

Includes

- SQL Connection String
- JWT Settings
- Serilog
- Allowed Hosts

---

# Design Principles

The project follows

- Clean Architecture
- Domain-Driven Design (DDD)
- SOLID Principles
- Repository Pattern
- Unit of Work
- Dependency Injection
- Separation of Concerns

---

# Code Quality

Implemented using

- Nullable Reference Types
- Async/Await
- Global Exception Middleware
- FluentValidation
- Structured Logging
- Reusable Services

---

# Future Enhancements

- CQRS
- MediatR
- AutoMapper
- Specification Pattern
- Domain Events
- Redis Cache
- Health Checks
- API Versioning
- Docker
- Azure Deployment
- GitHub Actions
- Integration Tests

---

# Testing

Recommended test projects

```
EmployeeSkills.Application.Tests

EmployeeSkills.API.Tests
```

Unit testing framework

- xUnit
- Moq
- FluentAssertions

---

# Developed By

**Sunil Singh**

Employee Skills Management System

2026