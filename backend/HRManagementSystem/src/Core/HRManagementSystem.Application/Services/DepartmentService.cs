using System;
using HRManagementSystem.Application.DTOs.Department;
using HRManagementSystem.Application.DTOs.Employee;

namespace HRManagementSystem.Application.Services;

public class DepartmentService(IUnitOfWork _unitOfWork, IMapper _mapper) : IDepartmentService
{
    public async Task<Response<DepartmentDto>> CreateDepartmentAsync(CreateDepartmentDto createDepartmentDto, CancellationToken cancellationToken = default)
    {
        try
        {
            Department existingDept = await _unitOfWork.Departments.GetByNameAsync(createDepartmentDto.Name, cancellationToken);
            if (existingDept != null)
            {
                return new Response<DepartmentDto>(default!, "Department with the same name already exists.", true);
            }
            Department? department = _mapper.Map<Department>(createDepartmentDto);
            department.CreatedAt = DateTime.UtcNow;
            await _unitOfWork.Departments.AddAsync(department, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            DepartmentDto? departmentDto = _mapper.Map<DepartmentDto>(department);

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
            Department? department = await _unitOfWork.Departments.GetByIdAsync(id, cancellationToken);
            if (department == null)
            {
                return new Response<bool>(false, "Department not found.", true);
            }

            // Check if department has employees
            if (department.Employees != null && department.Employees.Any())
            {
                return new Response<bool>(false, "Cannot delete department with existing employees. Please reassign or remove employees first.", true);
            }

            await _unitOfWork.Departments.DeleteAsync(department.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
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
            IEnumerable<Department> departments = await _unitOfWork.Departments.GetAllAsync(cancellationToken);
            IEnumerable<DepartmentDto> departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
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
            Department? department = await _unitOfWork.Departments.GetByIdAsync(id, cancellationToken);
            if (department == null)
            {
                return new Response<DepartmentDto>(null!, "Department not found.", true);
            }

            DepartmentDto? departmentDto = _mapper.Map<DepartmentDto>(department);

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
            IEnumerable<Department> departments = await _unitOfWork.Departments.GetDepartmentsWithEmployeesAsync(id, cancellationToken);
            Department? department = departments.FirstOrDefault();
            if (department == null)
            {
                return new Response<DepartmentWithEmployeesDto>(default!, "Department not found.", true);
            }

            DepartmentWithEmployeesDto? departmentWithEmployeesDto = _mapper.Map<DepartmentWithEmployeesDto>(department);

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
            Department? department = await _unitOfWork.Departments.GetByIdAsync(id, cancellationToken);
            if (department == null)
            {
                return new Response<bool>(false, "Department not found.", true);
            }

            // Check if name is being changed and if it conflicts with another department
            if (department.Name != updateDepartmentDto.Name)
            {
                Department? existingDept = await _unitOfWork.Departments.GetByNameAsync(updateDepartmentDto.Name, cancellationToken);
                if (existingDept != null && existingDept.Id != id)
                {
                    return new Response<bool>(false, "Another department with this name already exists.", true);
                }
            }

            department.Name = updateDepartmentDto.Name;
            department.Description = updateDepartmentDto.Description;
            department.ManagerId = updateDepartmentDto.ManagerId;
            department.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Departments.UpdateAsync(department, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            // Log the exception (ex) as needed
            return new Response<bool>(false, $"Error occurred while updating the department: {ex.Message}", true);
        }
    }
}
