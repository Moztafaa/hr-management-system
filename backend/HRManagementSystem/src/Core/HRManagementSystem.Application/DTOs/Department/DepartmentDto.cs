using System;

namespace HRManagementSystem.Application.DTOs.Department;

public class DepartmentDto
{

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public int? ManagerId { get; set; }
    public int? EmployeeCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }


}
