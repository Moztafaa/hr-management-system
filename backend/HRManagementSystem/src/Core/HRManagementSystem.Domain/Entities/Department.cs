using System;
using System.ComponentModel.DataAnnotations;

namespace HRManagementSystem.Domain.Entities;

public class Department : BaseEntity
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public required string Name { get; set; }
    [StringLength(500)]
    public string? Description { get; set; }
    public int? ManagerId { get; set; }

    public int? EmployeeCount { get; set; }

    // Navigation property
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();

}
