using System;
using HRManagementSystem.Application.DTOs.Department;

namespace HRManagementSystem.Application.Interfaces;

public interface IDepartmentService
{
    Task<Response<IEnumerable<DepartmentDto>>> GetAllDepartmentsAsync(CancellationToken cancellationToken = default);
    Task<Response<DepartmentDto>> GetDepartmentByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Response<DepartmentDto>> CreateDepartmentAsync(CreateDepartmentDto createDepartmentDto, CancellationToken cancellationToken = default);
    Task<Response<bool>> UpdateDepartmentAsync(int id, UpdateDepartmentDto updateDepartmentDto, CancellationToken cancellationToken = default);
    Task<Response<bool>> DeleteDepartmentAsync(int id, CancellationToken cancellationToken = default);
    Task<Response<DepartmentWithEmployeesDto>> GetDepartmentWithEmployeesAsync(int id, CancellationToken cancellationToken = default);

}
