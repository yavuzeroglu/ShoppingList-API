# 🚧 Under Development: ShoppingList - Backend Service

# Shopping List - Backend Service


## 🔍 Project Overview
ShoppingList is a collaborative shopping list management system that allows users to create, share, and manage shopping lists with family members or roommates. The system supports real-time updates and notifications to ensure all users are synchronized.

## 🛠 Tech Stack

### Core Technologies
- **Framework**: .NET 8.0 (Clean Architecture)
- **Database**: PostgreSQL
- **ORM**: Entity Framework Core
- **Authentication**: JWT + Identity Framework
- **API Documentation**: Swagger/OpenAPI

### Design Patterns & Architecture
- **Architecture**: Clean Architecture
- **Patterns**: 
  - Repository Pattern
  - CQRS Pattern
  - Mediator (MediatR)

### Libraries & Tools
- **Mapping**: AutoMapper
- **Validation**: FluentValidation
- **Email Service**: SendGrid
- **Containerization**: Docker
- **Version Control**: Git

## 📁 Project Structure

```
ShoppingList/
├── src/
│   ├── Core/                           # Core business logic and domain models
│   │   ├── ShoppingList.Domain/        # Domain entities, value objects, and business rules
│   │   └── ShoppingList.Application/   # Application services, DTOs, and interfaces
│   │
│   ├── External/                       # External dependencies and infrastructure
│   │   ├── ShoppingList.Persistance/   # Database operations and repositories
│   │   └── ShoppingList.Infrastructure/# External services (email, logging, etc.)
│   │
│   └── ShoppingList.WebAPI/            # API layer and presentation
│       ├── Controllers/                # API endpoints
│       ├── Models/                     # API request/response models
│       ├── Extensions/                 # Extension methods
│       ├── Configurations/            # Application configurations
│       ├── Properties/                # Project properties
│       ├── wwwroot/                   # Static files
│       ├── Program.cs                 # Application entry point
│       └── appsettings.json           # Configuration file
│
├── .github/                           # GitHub configurations
├── .vs/                              # Visual Studio configurations
├── README.md                         # Project documentation
└── ShoppingList.sln                  # Solution file
```

## 📋 Project Status

### Completed Features
- ✅ User Authentication (Register/Login)
- ✅ Shopping List Management
  - Create/Edit/Delete Lists
  - Add/Remove Items
  - Share Lists with Other Users
- ✅ Product Management
  - Categories
  - Brands
  - Basic Product Information

### In Progress
- 🔄 Real-time List Updates
- 🔄 User Notifications
- 🔄 Shopping List Templates

### Upcoming Features
- 📅 Notification System
- 📅 Price Comparison
- 📅 Voice Commands
- 📅 Mobile Application Integration


## 📞 Contact
For questions and support:
- **Email**: [yavuzeroglu15@gmail.com](mailto:yavuzeroglu15@gmail.com)
- **GitHub**: [github.com/yavuzeroglu](https://github.com/yavuzeroglu)

## 📝 License
This project is licensed under the MIT License - see the LICENSE file for details.