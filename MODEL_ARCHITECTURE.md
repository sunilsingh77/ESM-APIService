# Employee Skills Management System - Model Architecture

## Domain Models (EmployeeSkills.Domain/Entities)

### 1. Department
- **Id** (int, Primary Key)
- **Name** (string)
- **Description** (string)
- **CreatedDate** (DateTime)
- **Navigation**: Employees (ICollection<Employee>)

### 2. Employee
- **Id** (int, Primary Key)
- **FirstName** (string)
- **LastName** (string)
- **Email** (string)
- **PhoneNumber** (string)
- **Position** (string)
- **DepartmentId** (int, Foreign Key)
- **HireDate** (DateTime)
- **CreatedDate** (DateTime)
- **UpdatedDate** (DateTime?)
- **Navigation**: Department (one Department), EmployeeSkills (ICollection<EmployeeSkill>)

### 3. Skill
- **Id** (int, Primary Key)
- **Name** (string)
- **Description** (string)
- **Category** (string)
- **CreatedDate** (DateTime)
- **Navigation**: EmployeeSkills (ICollection<EmployeeSkill>)

### 4. EmployeeSkill (Join Entity)
- **Id** (int, Primary Key)
- **EmployeeId** (int, Foreign Key)
- **SkillId** (int, Foreign Key)
- **ProficiencyLevel** (string) - Beginner, Intermediate, Advanced, Expert
- **YearsOfExperience** (int)
- **IsPrimary** (bool)
- **AcquiredDate** (DateTime)
- **LastUpdatedDate** (DateTime?)
- **Navigation**: Employee (one Employee), Skill (one Skill)
- **Unique Index**: (EmployeeId, SkillId)

## Relationships

```
Department (1) ──→ (Many) Employee
  └─ OnDelete: Restrict

Employee (1) ──→ (Many) EmployeeSkill
  └─ OnDelete: Cascade

Skill (1) ──→ (Many) EmployeeSkill
  └─ OnDelete: Restrict
```

## API Endpoints

### Departments
- GET /api/departments - List all departments
- GET /api/departments/{id} - Get department by ID
- POST /api/departments - Create new department
- PUT /api/departments/{id} - Update department
- DELETE /api/departments/{id} - Delete department

### Skills
- GET /api/skills - List all skills
- GET /api/skills/{id} - Get skill by ID
- POST /api/skills - Create new skill
- PUT /api/skills/{id} - Update skill
- DELETE /api/skills/{id} - Delete skill

### Employees
- GET /api/employees - List all employees with skills
- GET /api/employees/{id} - Get employee by ID with skills
- POST /api/employees - Create new employee
- PUT /api/employees/{id} - Update employee
- DELETE /api/employees/{id} - Delete employee

### Employee Skills
- GET /api/employeeskills - List all employee skills
- GET /api/employeeskills/employee/{employeeId} - Get skills for specific employee
- GET /api/employeeskills/{id} - Get specific employee skill
- POST /api/employeeskills - Assign skill to employee
- PUT /api/employeeskills/{id} - Update employee skill proficiency
- DELETE /api/employeeskills/{id} - Remove skill from employee

## Next Steps

1. **Create Database Migration**
   ```
   dotnet ef migrations add InitializeEmployeeSkillsModels
   dotnet ef database update
   ```

2. **Test Endpoints**
   - Create departments first
   - Create skills
   - Create employees (assign to departments)
   - Assign skills to employees

3. **Optional Enhancements**
   - Add filtering by proficiency level
   - Add search by skill name or department
   - Add reporting endpoints (employees by skill, skills by department, etc.)
