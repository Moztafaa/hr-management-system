namespace HRManagementSystem.Application.Interfaces;
public interface ITrainingCourseService
{
    Task<Response<IEnumerable<TrainingCourseDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Response<TrainingCourseDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Response<TrainingCourseDto>> CreateAsync(TrainingCourseCreateDto dto, CancellationToken cancellationToken = default);
    Task<Response<TrainingCourseDto>> UpdateAsync(int id, TrainingCourseUpdateDto dto, CancellationToken cancellationToken = default);
    Task<Response<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
