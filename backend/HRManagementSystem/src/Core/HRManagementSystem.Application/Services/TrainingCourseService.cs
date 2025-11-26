namespace HRManagementSystem.Application.Services;

public class TrainingCourseService(IUnitOfWork unitOfWork, IMapper mapper) : ITrainingCourseService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Response<TrainingCourseDto>> CreateAsync(TrainingCourseCreateDto dto, CancellationToken cancellationToken = default)
    {
        TrainingCourse entity = _mapper.Map<TrainingCourse>(dto);
        await _unitOfWork.TrainingCourses.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        TrainingCourseDto resultDto = _mapper.Map<TrainingCourseDto>(entity);
        return new Response<TrainingCourseDto>(resultDto, string.Empty, false);
    }

    public async Task<Response<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        TrainingCourse? entity = await _unitOfWork.TrainingCourses.GetByIdAsync(id, cancellationToken);
        if (entity == null)
        {
            return new Response<bool>(false, $"Training course with id {id} not found.", true);
        }

        await _unitOfWork.TrainingCourses.DeleteAsync(id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new Response<bool>(true, string.Empty, false);
    }

    public async Task<Response<IEnumerable<TrainingCourseDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<TrainingCourse> courses = await _unitOfWork.TrainingCourses.GetAllAsync(cancellationToken);
        IEnumerable<TrainingCourseDto> dto = _mapper.Map<IEnumerable<TrainingCourseDto>>(courses);
        return new Response<IEnumerable<TrainingCourseDto>>(dto, string.Empty, false);
    }

    public async Task<Response<TrainingCourseDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        TrainingCourse? entity = await _unitOfWork.TrainingCourses.GetByIdAsync(id, cancellationToken);
        if (entity == null)
        {
            return new Response<TrainingCourseDto>(null!, $"Training course with id {id} not found.", true);
        }

        TrainingCourseDto dto = _mapper.Map<TrainingCourseDto>(entity);
        return new Response<TrainingCourseDto>(dto, string.Empty, false);
    }

    public async Task<Response<TrainingCourseDto>> UpdateAsync(int id, TrainingCourseUpdateDto dto, CancellationToken cancellationToken = default)
    {
        TrainingCourse? entity = await _unitOfWork.TrainingCourses.GetByIdAsync(id, cancellationToken);
        if (entity == null)
            return new Response<TrainingCourseDto>(null!, $"Training course with id {id} not found.", true);

        if (!string.IsNullOrWhiteSpace(dto.Title))
            entity.Title = dto.Title;

        if (dto.Description != null)
            entity.Description = dto.Description;

        if (dto.DurationHours.HasValue)
            entity.DurationHours = dto.DurationHours.Value;

        await _unitOfWork.TrainingCourses.UpdateAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        TrainingCourseDto resultDto = _mapper.Map<TrainingCourseDto>(entity);
        return new Response<TrainingCourseDto>(resultDto, string.Empty, false);
    }
}
