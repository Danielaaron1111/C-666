# Clubs Management System - A02 Assignment

## 🎯 Quick Start Guide

This is a complete implementation of the A02: Clubs assignment - a Blazor Server application for managing student clubs with full CRUD operations and query capabilities.

## 📦 What's Included

```
├── DatabaseScript_Clubs.sql          # Complete database setup script
├── ClubsSystem/                      # Class Library (Backend)
│   ├── Entities/                     # Database models
│   ├── DAL/                          # Data Access Layer
│   ├── BLL/                          # Business Logic Layer
│   └── ClubsSystemExtensions.cs      # Dependency Injection setup
├── ClubsWebApp/                      # Blazor Web Application (Frontend)
│   ├── Components/Pages/
│   │   ├── Query.razor               # Search clubs by status
│   │   └── ClubCRUD.razor            # Create/Update/Deactivate clubs
│   └── Program.cs                    # Application configuration
└── COMPLETE_IMPLEMENTATION_GUIDE.md  # Detailed documentation
```

## 🚀 Setup Instructions

### Step 1: Create the Database

1. Open **SQL Server Management Studio (SSMS)**
2. Run the script: `DatabaseScript_Clubs.sql`
3. Verify the database `ClubsDB` was created

### Step 2: Update Connection String

Edit `ClubsWebApp/appsettings.json`:

```json
"ConnectionStrings": {
  "ClubsDB": "Server=YOUR_SERVER\\SQLEXPRESS;Database=ClubsDB;..."
}
```

Replace `YOUR_SERVER` with your SQL Server instance name.

### Step 3: Build and Run (If using Visual Studio)

```bash
# Open the solution in Visual Studio
# Set ClubsWebApp as startup project
# Press F5 to run
```

### Step 4: Build and Run (If using .NET CLI)

```bash
cd ClubsWebApp
dotnet restore
dotnet build
dotnet run
```

Navigate to: `http://localhost:5000`

## ✨ Features Implemented

### Query Component (`/query`)
- ✅ Search clubs by Active/Inactive status
- ✅ Radio button selection
- ✅ Tabular results display
- ✅ Pagination (10 items per page)
- ✅ Shows employee full name or "No staff member"
- ✅ Edit links to CRUD component

### CRUD Component (`/crud`)
- ✅ Create new clubs
- ✅ Update existing clubs
- ✅ Deactivate clubs (soft delete - no hard delete!)
- ✅ Reactivate deactivated clubs
- ✅ Employee dropdown (filtered by position)
- ✅ All validation rules enforced

## 📋 Business Rules

| Rule | Status |
|------|--------|
| No hard delete - clubs are deactivated | ✅ Implemented |
| Reactivate button for inactive clubs | ✅ Implemented |
| Employee dropdown shows only eligible positions | ✅ Implemented |
| ClubID must be unique | ✅ Validated |
| Club names must be unique | ✅ Validated |
| Fees cannot be negative | ✅ Validated |

## 🔍 Required Queries

All 4 assignment queries are implemented:

1. **Employee Club List** - Employees associated with clubs (ordered by last name)
2. **Club by Status** - Clubs filtered by active status (ordered by club name)
3. **Club by ID** - Retrieve a specific club
4. **Available Staff** - Eligible employees for club assignment (Instructors, Office Admins, Technical Support)

## 🧪 Testing

See the **Testing Guide** section in `COMPLETE_IMPLEMENTATION_GUIDE.md` for detailed testing instructions.

Quick smoke test:
1. Navigate to `/query`
2. Search for "Active" clubs
3. Click "Edit" on any club
4. Verify CRUD page loads with club data
5. Try to create a new club
6. Try to deactivate a club
7. Verify it appears in "Inactive" query

## 📚 Documentation

- **COMPLETE_IMPLEMENTATION_GUIDE.md** - Full implementation details, code explanations, and troubleshooting
- **DatabaseScript_Clubs.sql** - Commented SQL script with sample data
- **Code Comments** - All classes and methods are documented with XML comments

## 🏗️ Architecture

This solution follows the **WestWind pattern**:

```
Presentation Layer (Blazor)
    ↓ uses
Business Logic Layer (BLL Services)
    ↓ uses
Data Access Layer (DbContext)
    ↓ accesses
Database (SQL Server)
```

**Key Patterns:**
- Repository pattern (via BLL services)
- Dependency Injection
- Separation of Concerns
- Entity Framework Core for ORM

## 🐛 Known Issues

None currently identified. All features tested and working as expected.

## 📝 Assignment Compliance

| Requirement | Status | Location |
|-------------|--------|----------|
| Reverse engineered entities | ✅ | Entities/ folder |
| DbContext with internal access | ✅ | DAL/ClubsContext.cs |
| Extension class for DI | ✅ | ClubsSystemExtensions.cs |
| BLL service classes | ✅ | BLL/ folder |
| Query component | ✅ | Pages/Query.razor |
| CRUD component | ✅ | Pages/ClubCRUD.razor |
| FullName property on Employee | ✅ | Entities/Employee.cs |
| All 4 queries | ✅ | BLL services |
| All business rules | ✅ | Throughout |

## 🛠️ Technologies Used

- **.NET 9.0** - Framework
- **Blazor Server** - Web UI framework
- **Entity Framework Core 9** - ORM
- **SQL Server** - Database
- **Bootstrap 5** - CSS framework
- **C# 12** - Programming language

## 📧 Support

For detailed explanations of any component, see:
- `COMPLETE_IMPLEMENTATION_GUIDE.md` - Comprehensive guide with code examples
- Code comments in each file
- Assignment requirements document

## 🎓 Learning Outcomes

By studying this implementation, you will learn:
- How to structure a multi-layered .NET application
- Entity Framework Core with Code First approach
- Blazor Server component development
- Dependency Injection in ASP.NET Core
- LINQ queries and data manipulation
- Business rule validation
- Soft delete pattern implementation
- Pagination in web applications

## ✅ Pre-submission Checklist

Before submitting:
- [ ] Database script runs without errors
- [ ] Connection string updated for your environment
- [ ] Application builds without errors
- [ ] Query component displays results
- [ ] CRUD component creates/updates/deactivates clubs
- [ ] All validation rules work
- [ ] Employee dropdown shows only eligible staff
- [ ] Pagination works correctly
- [ ] Code is commented
- [ ] Home page updated with your name

## 🚀 Quick Commands

```bash
# Build the solution
dotnet build

# Run the web app
cd ClubsWebApp
dotnet run

# Clean the solution
dotnet clean

# Restore NuGet packages
dotnet restore
```

## 📖 Additional Resources

- [Complete Implementation Guide](./COMPLETE_IMPLEMENTATION_GUIDE.md)
- [Database Schema Diagram](./DatabaseScript_Clubs.sql) - See comments
- [Assignment Requirements](./Assignment_Requirements.md) - Original requirements

---

**Version:** 1.0
**Author:** A02 Clubs Implementation
**Date:** 2025

**Happy Coding! 🎉**
