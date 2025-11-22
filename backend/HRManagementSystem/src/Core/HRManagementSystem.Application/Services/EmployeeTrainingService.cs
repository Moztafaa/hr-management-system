namespace HRManagementSystem.Application.Services;
public class EmployeeTrainingService(IUnitOfWork unitOfWork, IMapper mapper) : IEmployeeTrainingService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Response<EmployeeTrainingDto>> EnrollAsync(EmployeeEnrollDto dto, CancellationToken cancellationToken = default)
    {
        bool alreadyEnrolled = await _unitOfWork.EmployeeTrainings
            .IsEmployeeEnrolledAsync(dto.EmployeeId, dto.TrainingCourseId, cancellationToken);

        if (alreadyEnrolled)
        {
            return new Response<EmployeeTrainingDto>(
                null!,
                "Employee is already enrolled in this course.",
                true
            );
        }

        var entity = new EmployeeTraining
        {
            EmployeeId = dto.EmployeeId,
            TrainingCourseId = dto.TrainingCourseId,
            Status = TrainingStatus.Enrolled
        };

        await _unitOfWork.EmployeeTrainings.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        entity = await _unitOfWork.EmployeeTrainings
            .GetByEmployeeAndCourseAsync(entity.EmployeeId, entity.TrainingCourseId, cancellationToken)
            ?? entity;

        EmployeeTrainingDto dtoResult = _mapper.Map<EmployeeTrainingDto>(entity);

        return new Response<EmployeeTrainingDto>(dtoResult, string.Empty, false);
    }

    public async Task<Response<bool>> CancelAsync(int employeeId, int courseId, CancellationToken cancellationToken = default)
    {
        EmployeeTraining? enrollment = await _unitOfWork.EmployeeTrainings
         .GetByEmployeeAndCourseAsync(employeeId, courseId);

        if (enrollment == null)
        {
            return new Response<bool>(false, string.Empty, true);
        }

        enrollment.Status = TrainingStatus.Cancelled;

        await _unitOfWork.EmployeeTrainings.UpdateAsync(enrollment, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new Response<bool>(true, string.Empty, false);
    }

    public async Task<Response<bool>> CompleteAsync(int employeeId, int courseId, CancellationToken cancellationToken = default)
    {
        EmployeeTraining? enrollment = await _unitOfWork.EmployeeTrainings
            .GetByEmployeeAndCourseAsync(employeeId, courseId);

        if (enrollment == null)
        {
            return new Response<bool>(false, string.Empty, true);
        }

        enrollment.Status = TrainingStatus.Completed;

        await _unitOfWork.EmployeeTrainings.UpdateAsync(enrollment);
        await _unitOfWork.SaveChangesAsync();

        return new Response<bool>(true, string.Empty, false);
    }
    public async Task<IEnumerable<EmployeeTrainingDto>> GetEnrollmentsByEmployeeAsync(int employeeId, CancellationToken cancellationToken = default)
    {
        IEnumerable<EmployeeTraining> enrollments = await _unitOfWork.EmployeeTrainings.GetEnrollmentsByEmployeeAsync(employeeId, cancellationToken);
        return _mapper.Map<IEnumerable<EmployeeTrainingDto>>(enrollments);
    }

    public async Task<IEnumerable<EmployeeTrainingDto>> GetEnrollmentsByCourseAsync(int courseId, CancellationToken cancellationToken = default)
    {
        IEnumerable<EmployeeTraining> enrollments = await _unitOfWork.EmployeeTrainings.GetEnrollmentsByCourseAsync(courseId, cancellationToken);
        return _mapper.Map<IEnumerable<EmployeeTrainingDto>>(enrollments);
    }
}
