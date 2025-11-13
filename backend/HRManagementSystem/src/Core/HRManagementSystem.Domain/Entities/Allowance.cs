namespace HRManagementSystem.Domain.Entities;

public class Allowance:BaseEntity
{
    public string Name { get; set; } = default!;
    public decimal Amount { get; set; }
}
