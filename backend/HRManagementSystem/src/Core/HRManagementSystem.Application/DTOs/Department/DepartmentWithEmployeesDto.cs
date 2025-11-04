using System;
using HRManagementSystem.Application.DTOs.Employee;

namespace HRManagementSystem.Application.DTOs.Department;

public class DepartmentWithEmployeesDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? ManagerId { get; set; }
    public List<EmployeeSummaryDto>? Employees { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

}
