# A02 Clubs - Quick Reference Cheat Sheet

## 🔥 Most Important Files

| File | Purpose |
|------|---------|
| `DatabaseScript_Clubs.sql` | Creates database with all tables and sample data |
| `appsettings.json` | Connection string configuration |
| `ClubServices.cs` | All club CRUD and query operations |
| `EmployeeServices.cs` | Employee queries for dropdown |
| `Query.razor` | Search clubs by status component |
| `ClubCRUD.razor` | Create/Update/Deactivate component |

## ⚡ Common Code Snippets

### Get Clubs by Status (BLL)
```csharp
public List<Club> GetClubsByStatus(bool activeStatus)
{
    return _context.Clubs
        .Include(c => c.Employee)
            .ThenInclude(e => e.Position)
        .Where(c => c.Active == activeStatus)
        .OrderBy(c => c.ClubName)
        .ToList();
}
```

### Employee Dropdown Query (BLL)
```csharp
public List<Employee> GetAvailableStaffForClubs()
{
    var validPositions = new[] {
        "Instructor",
        "Office Administrator",
        "Technical Support"
    };

    return _context.Employees
        .Include(e => e.Position)
        .Where(e => validPositions.Contains(e.Position.PositionName))
        .Where(e => e.ReleaseDate == null)
        .OrderBy(e => e.LastName)
        .ToList();
}
```

### Deactivate Club (Soft Delete)
```csharp
public void DeactivateClub(string clubId)
{
    var club = _context.Clubs.Find(clubId);
    if (club == null)
        throw new ArgumentException($"Club '{clubId}' not found.");

    club.Active = false;
    _context.SaveChanges();
}
```

### Pagination Logic
```csharp
// Calculate total pages
totalPages = (int)Math.Ceiling((double)totalClubs / pageSize);

// Get current page items
currentPageClubs = allFilteredClubs
    .Skip((currentPage - 1) * pageSize)
    .Take(pageSize)
    .ToList();
```

## 🎯 Business Rules Quick Reference

| Rule | Validation Location | Error Message |
|------|-------------------|---------------|
| ClubID unique | `ClubServices.AddClub()` | "Club ID '{id}' already exists" |
| ClubName unique | `ClubServices.AddClub/UpdateClub()` | "Club name '{name}' already exists" |
| Fee non-negative | `Club.cs` + `ClubServices` | "Fee cannot be negative" |
| No hard delete | `ClubServices.DeactivateClub()` | Sets Active = false |
| Employee positions | `EmployeeServices.GetAvailableStaffForClubs()` | Filters by position name |

## 📋 Required Queries Checklist

- [x] **Employee Club List** → `GetEmployeesWithClubs()`
- [x] **Club by Status** → `GetClubsByStatus(bool)`
- [x] **Club by ID** → `GetClubById(string)`
- [x] **Available Staff** → `GetAvailableStaffForClubs()`

## 🔧 Troubleshooting Quick Fixes

| Problem | Solution |
|---------|----------|
| Can't connect to database | Update connection string in appsettings.json |
| FullName not working | Add `[NotMapped]` attribute |
| Pagination broken | Ensure `UpdateCurrentPage()` called after search |
| Employee dropdown empty | Check Position table has correct names |
| Service not found | Verify registration in Extensions class |

## 🚀 Run Commands

```bash
# Create database
# Run DatabaseScript_Clubs.sql in SSMS

# Update connection string
# Edit ClubsWebApp/appsettings.json

# Build and run
cd ClubsWebApp
dotnet build
dotnet run
```

## 📊 Database Quick Reference

### Tables
- **Employees** (EmployeeID, FirstName, LastName, DateHired, ReleaseDate, PositionID, ProgramID, LoginID)
- **Clubs** (ClubID, ClubName, Active, EmployeeID, Fee)
- **Positions** (PositionID, PositionName)
- **Programs** (ProgramID, ProgramName)

### Key Relationships
- Clubs.EmployeeID → Employees.EmployeeID (nullable)
- Employees.PositionID → Positions.PositionID
- Employees.ProgramID → Programs.ProgramID

## 🎨 Component Structure

### Query.razor
```
Radio Buttons (Active/Inactive)
    ↓
Search Button → ClubServices.GetClubsByStatus()
    ↓
Display Table (with pagination)
    ↓
Edit Links → Navigate to /crud?clubId=xxx
```

### ClubCRUD.razor
```
Load Club (if clubId in query string)
    ↓
Display Form (Create or Edit mode)
    ↓
Submit → AddClub() or UpdateClub()
    ↓
Deactivate/Reactivate Buttons
```

## 💡 Key Patterns

### Dependency Injection
```csharp
// Extensions class
services.AddDbContext<ClubsContext>(options);
services.AddTransient<ClubServices>(...);

// Component
@inject ClubServices ClubServices
```

### Navigation Properties
```csharp
// Loading related data
.Include(c => c.Employee)
.ThenInclude(e => e.Position)
```

### Service Pattern
```csharp
// Internal constructor
internal ClubServices(ClubsContext context)
{
    _context = context;
}

// Public methods
public List<Club> GetClubsByStatus(bool activeStatus) { ... }
```

## 📝 Assignment Grading Quick Check

- [ ] Query component with radio buttons ✅
- [ ] Tabular results display ✅
- [ ] Pagination (10 items/page) ✅
- [ ] Employee full name or "No staff member" ✅
- [ ] CRUD - Create with validation ✅
- [ ] CRUD - Update with validation ✅
- [ ] CRUD - Deactivate (not delete!) ✅
- [ ] CRUD - Reactivate button ✅
- [ ] Employee dropdown filtered ✅
- [ ] All 4 queries implemented ✅
- [ ] All business rules enforced ✅
- [ ] DbContext internal access ✅
- [ ] Extension class for DI ✅
- [ ] FullName property on Employee ✅

## 🎯 Testing Shortcuts

### Quick Test Sequence
1. `/query` → Search Active → Should see clubs
2. Click Edit → Should load club in CRUD
3. Update club name → Should save
4. Click Deactivate → Should set Active = false
5. `/query` → Search Inactive → Should see deactivated club
6. Click Edit → Click Reactivate → Should set Active = true
7. Try create club with duplicate name → Should show error

### SQL Quick Checks
```sql
-- Verify all clubs
SELECT * FROM Clubs ORDER BY ClubName;

-- Check deactivated clubs still exist
SELECT * FROM Clubs WHERE Active = 0;

-- Verify employee filtering
SELECT e.*, p.PositionName
FROM Employees e
JOIN Positions p ON e.PositionID = p.PositionID
WHERE p.PositionName IN ('Instructor', 'Office Administrator', 'Technical Support');
```

---

**For detailed explanations, see:** `COMPLETE_IMPLEMENTATION_GUIDE.md`
