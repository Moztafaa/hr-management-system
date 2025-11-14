namespace HRManagementSystem.Application.DTOs.Account;

public class RegisterEmployeeDto
{
    // User/Account Information
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    public int DeptId {  get; set; }
    public int DesignationId { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 6)]
    public required string Password { get; set; }

    [Required]
    [Compare("Password")]
    public required string ConfirmPassword { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }

    // Employee Information
    [Required]
    [StringLength(50)]
    public required string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public required string LastName { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    [StringLength(10)]
    public string? Gender { get; set; }

    [Required]
    public DateTime HireDate { get; set; }

    public DateTime? EFF_Start { get; set; }

    public DateTime? EFF_End { get; set; }

    [Phone]
    public string? ContactNumber { get; set; }

    [StringLength(200)]
    public string? Address { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal BasicSalary { get; set; }
    

    // Optional: Role assignment
    public List<string>? Roles { get; set; }
}
