namespace HRManagementSystem.Infrastructure.Repositories;

public class EmployeeRepository:Repository<Employee>
{
    public EmployeeRepository(ApplicationDbContext context) : base(context)
    {
    }
}
