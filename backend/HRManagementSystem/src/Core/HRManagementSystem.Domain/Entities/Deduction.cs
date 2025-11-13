namespace HRManagementSystem.Domain.Entities;

public class Deduction: BaseEntity
{
    public string Name { get; set; } = default!;
    public decimal Amount { get; set; }
}
