# RentCar API

A comprehensive car rental management system built with .NET Core 8.0, implementing clean architecture principles and modern security practices.

## Features

- **Authentication & Authorization**
  - JWT-based authentication
  - Role-based access control
  - Secure password hashing using PBKDF2 with SHA256
  - Token-based session management

- **Architecture & Design**
  - Onion (Clean) Architecture implementation
  - SOLID principles
  - Repository & Unit of Work patterns
  - Service Layer pattern
  - Rich domain model

- **Database**
  - Entity Framework Core with SQL Server
  - Code-first migrations
  - Fluent API configurations
  - Efficient query optimization

- **Security**
  - Password hashing with salt
  - JWT token validation
  - Role-based authorization
  - Input validation and sanitization

## Technology Stack

- **Backend**
  - .NET Core 8.0
  - Entity Framework Core
  - Microsoft SQL Server

- **Security & Authentication**
  - JWT Bearer Authentication
  - PBKDF2 Password Hashing
  - Role-based Authorization

- **Documentation**
  - Swagger/OpenAPI

## Project Structure

```
RentCar/
├── Core/
│   ├── RentCar.Domain/
│   │   ├── Constants/
│   │   ├── Entities/
│   │   └── Helpers/
│   └── RentCar.Application/
│       ├── Dtos/
│       ├── Interfaces/
│       ├── Services/
│       └── Validators/
├── Infrastructure/
│   └── RentCar.Persistence/
│       ├── Context/
│       └── Repositories/
└── Presentation/
    └── RentCar.Api/
        └── Controllers/
```

## API Endpoints

### Authentication
```http
POST /api/auth/register
POST /api/auth/login
```

### Users
```http
GET    /api/users
GET    /api/users/{id}
POST   /api/users
PUT    /api/users/{id}
DELETE /api/users/{id}
```

### Cars
```http
GET    /api/cars
GET    /api/cars/{id}
POST   /api/cars
PUT    /api/cars/{id}
DELETE /api/cars/{id}
```

### Rentals
```http
GET    /api/rentedcars
GET    /api/rentedcars/{id}
POST   /api/rentedcars
PUT    /api/rentedcars/{id}
DELETE /api/rentedcars/{id}
```

## Role-Based Access Control

### Admin
- Full system access
- User management
- Car management
- Rental management
- System configuration

### Manager
- User viewing
- Car management
- Rental management
- Reports viewing

### User
- View available cars
- Create rental requests
- Manage own rentals
- View rental history

### Guest
- View available cars
- Register account

## Security Implementation

### Password Requirements
- Minimum 8 characters
- At least one uppercase letter
- At least one lowercase letter
- At least one number
- At least one special character

### JWT Configuration
- Token expiration: 24 hours
- Secure key storage
- Role-based claims
- Token refresh mechanism

## Installation

1. Clone the repository
```bash
git clone https://github.com/erenciceek/RentCar.git
```

2. Update database connection string in `appsettings.json`
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=RentCar;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

3. Apply database migrations
```bash
dotnet ef database update
```

4. Run the application
```bash
dotnet run
```

## API Response Codes

| Status Code | Description |
|-------------|-------------|
| 200 | Success |
| 400 | Bad Request - Invalid input |
| 401 | Unauthorized - Authentication required |
| 403 | Forbidden - Insufficient permissions |
| 404 | Not Found - Resource doesn't exist |
| 500 | Server Error - Internal server error |


## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
