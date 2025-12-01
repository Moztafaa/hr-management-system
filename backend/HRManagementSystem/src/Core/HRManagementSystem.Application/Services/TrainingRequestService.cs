namespace HRManagementSystem.Application.Services;

public class TrainingRequestService(IUnitOfWork unitOfWork, IMapper mapper) : ITrainingRequestService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    public async Task<Response<TrainingRequestDto>> CreateRequestAsync(TrainingRequestCreateDto dto, CancellationToken cancellationToken = default)
    {
        bool duplicate = await _unitOfWork.TrainingRequests.IsDuplicateRequestAsync(dto.EmployeeId, dto.TrainingCourseId, cancellationToken);
        if (duplicate)
            return new Response<TrainingRequestDto>(null!, "Request already exists for this course.", true);

        TrainingRequest entity = _mapper.Map<TrainingRequest>(dto);
        entity.Status = TrainingRequestStatus.Pending;
        entity.RequestDate = DateTime.UtcNow;

        await _unitOfWork.TrainingRequests.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        entity = await _unitOfWork.TrainingRequests.GetByIdWithEmployeeAsync(entity.Id, cancellationToken);

        TrainingRequestDto resultDto = _mapper.Map<TrainingRequestDto>(entity);
        return new Response<TrainingRequestDto>(resultDto, string.Empty, false);
    }
    public async Task<Response<IEnumerable<TrainingRequestDto>>> GetRequestsByEmployeeAsync(int employeeId, CancellationToken cancellationToken = default)
    {
        IEnumerable<TrainingRequest> requests = await _unitOfWork.TrainingRequests.GetByEmployeeAsync(employeeId, cancellationToken);
        IEnumerable<TrainingRequestDto> dtos = _mapper.Map<IEnumerable<TrainingRequestDto>>(requests);
        return new Response<IEnumerable<TrainingRequestDto>>(dtos, string.Empty, false);
    }
    public async Task<Response<IEnumerable<TrainingRequestDto>>> GetRequestsByStatusAsync(TrainingRequestStatus status, CancellationToken cancellationToken = default)
    {
        IEnumerable<TrainingRequest> requests = await _unitOfWork.TrainingRequests.GetByStatusAsync(status, cancellationToken);
        IEnumerable<TrainingRequestDto> dtos = _mapper.Map<IEnumerable<TrainingRequestDto>>(requests);
        return new Response<IEnumerable<TrainingRequestDto>>(dtos, string.Empty, false);
    }
    public async Task<Response<TrainingRequestDto>> ReviewRequestAsync(int requestId, int managerId, bool approve, string? managerNote = null, CancellationToken cancellationToken = default)
    {
        TrainingRequest? request = await _unitOfWork.TrainingRequests.GetByIdWithEmployeeAsync(requestId, cancellationToken);
        if (request == null)
            return new Response<TrainingRequestDto>(null!, "Request not found.", true);

        if (request.Status != TrainingRequestStatus.Pending)
            return new Response<TrainingRequestDto>(null!, "Request has already been reviewed.", true);

        request.Status = approve ? TrainingRequestStatus.Approved : TrainingRequestStatus.Rejected;
        request.ReviewedByManagerId = managerId;
        request.ReviewedAt = DateTime.UtcNow;
        request.ManagerNote = managerNote;

        await _unitOfWork.TrainingRequests.UpdateAsync(request, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        request = await _unitOfWork.TrainingRequests.GetByIdWithEmployeeAsync(request.Id, cancellationToken);

        TrainingRequestDto dto = _mapper.Map<TrainingRequestDto>(request);
        return new Response<TrainingRequestDto>(dto, string.Empty, false);
    }
    public async Task<Response<TrainingRequestDto>> GetRequestByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        TrainingRequest? request = await _unitOfWork.TrainingRequests.GetByIdWithEmployeeAsync(id, cancellationToken);
        if (request == null)
            return new Response<TrainingRequestDto>(null!, "Request not found.", true);

        TrainingRequestDto dto = _mapper.Map<TrainingRequestDto>(request);
        return new Response<TrainingRequestDto>(dto, string.Empty, false);
    }
    public async Task<Response<IEnumerable<TrainingRequestDto>>> GetAllRequestsAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<TrainingRequest> requests = await _unitOfWork.TrainingRequests.GetAllAsync(cancellationToken);
        IEnumerable<TrainingRequestDto> dtos = _mapper.Map<IEnumerable<TrainingRequestDto>>(requests);
        return new Response<IEnumerable<TrainingRequestDto>>(dtos, string.Empty, false);
    }
    public async Task<Response<bool>> DeleteRequestAsync(int id, CancellationToken cancellationToken = default)
    {
        TrainingRequest? request = await _unitOfWork.TrainingRequests.GetByIdAsync(id, cancellationToken);
        if (request == null)
            return new Response<bool>(false, "Request not found.", true);

        if (request.Status != TrainingRequestStatus.Pending)
            return new Response<bool>(false, "Only pending requests can be deleted.", true);

        await _unitOfWork.TrainingRequests.DeleteAsync(id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new Response<bool>(true, string.Empty, false);
    }

}
