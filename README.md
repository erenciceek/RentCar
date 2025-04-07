# RentCar API

A RESTful API project developed for managing car rental operations. Built with .NET Core 7.0.

## Features

- JWT-based authentication and authorization
- Role-based authorization (Admin, Manager, User, Guest)
- Secure password hashing (PBKDF2 with SHA256)
- Database operations with Entity Framework Core
- SOLID principles compliant architecture
- Repository and Service pattern implementation

## Technologies

- .NET Core 7.0
- Entity Framework Core
- JWT (JSON Web Token)
- SQL Server
- Swagger/OpenAPI

## Architecture Layers

### Core Layer
- **RentCar.Domain**: Entities, constants, and helper classes
- **RentCar.Application**: Business logic, services, DTOs, and interfaces

### Infrastructure Layer
- **RentCar.Persistence**: Database operations, repository implementations

### Presentation Layer
- **RentCar.Api**: API endpoints, controllers

## User Roles

- **Admin**: Full access to all features
- **Manager**: Can manage users and vehicles
- **User**: Can perform car rental operations
- **Guest**: Can only view cars

## API Endpoints

### Authentication Operations
- POST /api/auth/register - Register new user
- POST /api/auth/login - User login

### User Operations
- GET /api/users - List all users (Admin, Manager)
- GET /api/users/{id} - User details (Admin, Manager)
- POST /api/users - Create new user (Admin)
- PUT /api/users/{id} - Update user (Admin)
- DELETE /api/users/{id} - Delete user (Admin)

### Security Requirements

#### Password Requirements:
- Minimum 8 characters
- At least one uppercase letter
- At least one lowercase letter
- At least one number
- At least one special character

## Status Codes

- 200: Success
- 400: Bad Request
- 401: Unauthorized
- 403: Forbidden
- 404: Not Found
- 500: Server Error

## Installation

1. Clone the project
```bash
git clone https://github.com/yourusername/RentCar.git
```

2. Create the database
```bash
dotnet ef database update
```

3. Run the project
```bash
dotnet run
```

## Development

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contact

Project Owner - [Email Address]

Project Link: [https://github.com/yourusername/RentCar](https://github.com/yourusername/RentCar)