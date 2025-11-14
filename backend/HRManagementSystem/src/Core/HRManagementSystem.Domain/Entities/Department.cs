using System;
using System.ComponentModel.DataAnnotations;

namespace HRManagementSystem.Domain.Entities;

public class Department : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int? ManagerId { get; set; }

    public int? EmployeeCount { get; set; }

    // Navigation property
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();

}
