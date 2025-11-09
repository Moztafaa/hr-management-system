using System;
using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Domain.Interfaces;

public interface IJobPostingRepository : IRepository<JobPosting>
{
    Task<IEnumerable<JobPosting>> GetActiveJobPostingsAsync();
    Task<IEnumerable<JobPosting>> GetJobPostingsByDepartmentAsync(int departmentId);
    Task<IEnumerable<JobPosting>> GetJobPostingsByDesignationAsync(int designationId);

}
