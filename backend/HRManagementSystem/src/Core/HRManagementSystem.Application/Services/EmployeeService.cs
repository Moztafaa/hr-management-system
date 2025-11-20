using HRManagementSystem.Application.Helper;

namespace HRManagementSystem.Application.Services;

public class EmployeeService(IUnitOfWork _unitOfWork, IFaceRecognitionService _faceService, IMapper _mapper) : IEmployeeService
{
    public async Task<Response<EmployeeDto>> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto, CancellationToken cancellationToken = default)
    {
        try
        {
            Employee? employee = _mapper.Map<Employee>(createEmployeeDto);

            await _unitOfWork.Repository<Employee>().AddAsync(employee, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            EmployeeDto? dto = _mapper.Map<EmployeeDto>(employee);

            return new Response<EmployeeDto>(dto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<EmployeeDto>(default!, ex.Message, true);
        }
    }

    public async Task<Response<IEnumerable<EmployeeDto>>> GetAllEmployeesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<Employee> employees = await _unitOfWork.Repository<Employee>().GetAllAsync(cancellationToken);

            IEnumerable<EmployeeDto> dtoList = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return new Response<IEnumerable<EmployeeDto>>(dtoList, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<EmployeeDto>>(null!, ex.Message, true);
        }
    }

    public async Task<Response<EmployeeDto>> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken = default)

    {
        try
        {
            Employee? employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(id, cancellationToken);
            if (employee is null)
                return new Response<EmployeeDto>(null!, "Employee not found", true);

            EmployeeDto? dto = _mapper.Map<EmployeeDto>(employee);

            return new Response<EmployeeDto>(dto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<EmployeeDto>(null!, ex.Message, true);
        }
    }
    public async Task<Response<bool>> UpdateEmployeeAsync(int id, UpdateEmployeeDto updateEmployeeDto, CancellationToken cancellationToken = default)

    {
        try
        {
            Employee? employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(id, cancellationToken);
            if (employee is null)
                return new Response<bool>(false, "Employee not found", true);

            _mapper.Map(updateEmployeeDto, employee);

            await _unitOfWork.Repository<Employee>().UpdateAsync(employee, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, ex.Message, true);
        }
    }
    public async Task<Response<bool>> UpdateEmployeeImageAsync(int employeeId, byte[] image, CancellationToken cancellationToken = default)
    {
        try
        {
            Employee? employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(employeeId, cancellationToken);
            if (employee is null)
                return new Response<bool>(false, "Employee not found", true);

            // Extract embedding for Face Recognition
            double[] embedding= _faceService.ExtractEmbedding(image);
            employee.FaceEmbedding = EmbeddingSerializer.DoubleArrayToBytes(embedding);
            await _unitOfWork.Repository<Employee>().UpdateAsync(employee);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, ex.Message, true);
        }
    }
    public async Task<Response<bool>> DeleteEmployeeAsync(int id, CancellationToken cancellationToken = default)

    {
        try
        {
            Employee? employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(id, cancellationToken);
            if (employee is null)
                return new Response<bool>(false, "Employee not found", true);

            await _unitOfWork.Repository<Employee>().DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, ex.Message, true);
        }
    }

}
