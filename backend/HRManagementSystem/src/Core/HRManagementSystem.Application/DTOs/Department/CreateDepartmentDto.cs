using System;

namespace HRManagementSystem.Application.DTOs.Department;

public class CreateDepartmentDto
{
    [StringLength(100, MinimumLength = 2)]
    public required string Name { get; set; }
    [StringLength(500)]
    public string? Description { get; set; }

    public int? ManagerId { get; set; }

}
