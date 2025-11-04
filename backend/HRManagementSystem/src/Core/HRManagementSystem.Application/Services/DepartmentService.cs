using System;
using HRManagementSystem.Application.DTOs.Department;
using HRManagementSystem.Application.DTOs.Employee;

namespace HRManagementSystem.Application.Services;

public class DepartmentService(IDepartmentRepository _departmentRepository) : IDepartmentService
{
    public async Task<Response<DepartmentDto>> CreateDepartmentAsync(CreateDepartmentDto createDepartmentDto, CancellationToken cancellationToken = default)
    {
        try
        {
            Department existingDept = await _departmentRepository.GetByNameAsync(createDepartmentDto.Name, cancellationToken);
            if (existingDept != null)
            {
                return new Response<DepartmentDto>(default!, "Department with the same name already exists.", true);
            }
            var department = new Department
            {
                Name = createDepartmentDto.Name,
                Description = createDepartmentDto.Description,
                ManagerId = createDepartmentDto.ManagerId,
                CreatedAt = DateTime.UtcNow
            };
            await _departmentRepository.AddAsync(department, cancellationToken);
            var departmentDto = new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description,
                ManagerId = department.ManagerId,
                EmployeeCount = 0,
                CreatedAt = department.CreatedAt
            };

            return new Response<DepartmentDto>(departmentDto, string.Empty, false);
        }
        catch (Exception ex)
        {
            // Log the exception (ex) as needed
            return new Response<DepartmentDto>(default!, $"Error occurred while creating the department: {ex.Message}", true);
        }
    }
    public async Task<Response<bool>> DeleteDepartmentAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            Department? department = await _departmentRepository.GetByIdAsync(id, cancellationToken);
            if (department == null)
            {
                return new Response<bool>(false, "Department not found.", true);
            }

            // Check if department has employees
            if (department.Employees != null && department.Employees.Any())
            {
                return new Response<bool>(false, "Cannot delete department with existing employees. Please reassign or remove employees first.", true);
            }

            await _departmentRepository.DeleteAsync(department.Id, cancellationToken);
            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            // Log the exception (ex) as needed
            return new Response<bool>(false, $"Error occurred while deleting the department: {ex.Message}", true);
        }

    }
    public async Task<Response<IEnumerable<DepartmentDto>>> GetAllDepartmentsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<Department> departments = await _departmentRepository.GetAllAsync(cancellationToken);
            IEnumerable<DepartmentDto> departmentDtos = departments.Select(d => new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                ManagerId = d.ManagerId,
                EmployeeCount = d.EmployeeCount ?? 0,
                CreatedAt = d.CreatedAt,
                UpdatedAt = d.UpdatedAt
            });
            return new Response<IEnumerable<DepartmentDto>>(departmentDtos, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<DepartmentDto>>(null!, $"Error occurred while retrieving departments: {ex.Message}", true);

        }
    }
    public async Task<Response<DepartmentDto>> GetDepartmentByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            Department? department = await _departmentRepository.GetByIdAsync(id, cancellationToken);
            if (department == null)
            {
                return new Response<DepartmentDto>(null!, "Department not found.", true);
            }

            var departmentDto = new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description,
                ManagerId = department.ManagerId,
                EmployeeCount = department.EmployeeCount ?? 0,
                CreatedAt = department.CreatedAt,
                UpdatedAt = department.UpdatedAt
            };

            return new Response<DepartmentDto>(departmentDto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<DepartmentDto>(null!, $"Error occurred while retrieving the department: {ex.Message}", true);
        }
    }
    public async Task<Response<DepartmentWithEmployeesDto>> GetDepartmentWithEmployeesAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<Department> departments = await _departmentRepository.GetDepartmentsWithEmployeesAsync(id, cancellationToken);
            Department? department = departments.FirstOrDefault();
            if (department == null)
            {
                return new Response<DepartmentWithEmployeesDto>(default!, "Department not found.", true);
            }

            var departmentWithEmployeesDto = new DepartmentWithEmployeesDto
            {
                Id = department.Id,
                Name = department.Name,
                Employees = [.. department.Employees.Select(e => new EmployeeSummaryDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email
                })]
            };

            return new Response<DepartmentWithEmployeesDto>(departmentWithEmployeesDto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<DepartmentWithEmployeesDto>(null!, $"Error occurred while retrieving the department with employees: {ex.Message}", true);
        }
    }
    public async Task<Response<bool>> UpdateDepartmentAsync(int id, UpdateDepartmentDto updateDepartmentDto, CancellationToken cancellationToken = default)
    {
        try
        {
            Department? department = await _departmentRepository.GetByIdAsync(id, cancellationToken);
            if (department == null)
            {
                return new Response<bool>(false, "Department not found.", true);
            }

            // Check if name is being changed and if it conflicts with another department
            if (department.Name != updateDepartmentDto.Name)
            {
                Department? existingDept = await _departmentRepository.GetByNameAsync(updateDepartmentDto.Name, cancellationToken);
                if (existingDept != null && existingDept.Id != id)
                {
                    return new Response<bool>(false, "Another department with this name already exists.", true);
                }
            }

            department.Name = updateDepartmentDto.Name;
            department.Description = updateDepartmentDto.Description;
            department.ManagerId = updateDepartmentDto.ManagerId;
            department.UpdatedAt = DateTime.UtcNow;

            await _departmentRepository.UpdateAsync(department, cancellationToken);
            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            // Log the exception (ex) as needed
            return new Response<bool>(false, $"Error occurred while updating the department: {ex.Message}", true);
        }
    }
}
