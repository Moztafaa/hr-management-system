

namespace HRManagementSystem.Application.Mappings;

public class TrainingProfile : Profile
{
    public TrainingProfile()
    {
        // TrainingCourse mappings
        CreateMap<TrainingCourse, TrainingCourseDto>().ReverseMap();
        CreateMap<TrainingCourseCreateDto, TrainingCourse>();
        CreateMap<TrainingCourseUpdateDto, TrainingCourse>();

        // EmployeeTraining mappings
        CreateMap<EmployeeTraining, EmployeeTrainingDto>()
            .ForMember(dest => dest.EmployeeName,
                       opt => opt.MapFrom(src => src.Employee!.FirstName + " " + src.Employee.LastName))
            .ForMember(dest => dest.TrainingCourseTitle,
                       opt => opt.MapFrom(src => src.TrainingCourse!.Title))
            .ForMember(dest => dest.Status,
                       opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<EmployeeEnrollDto, EmployeeTraining>();

        // TrainingRequest mappings
        CreateMap<TrainingRequest, TrainingRequestDto>()
            .ForMember(dest => dest.EmployeeName,
                       opt => opt.MapFrom(src => src.Employee!.FirstName + " " + src.Employee.LastName))
            .ForMember(dest => dest.CourseTitle,
                       opt => opt.MapFrom(src => src.TrainingCourse!.Title))
            .ForMember(dest => dest.ManagerName,
                       opt => opt.MapFrom(src => src.Reviewer != null
                                                ? src.Reviewer.FirstName + " " + src.Reviewer.LastName
                                                : null));

        CreateMap<TrainingRequestCreateDto, TrainingRequest>().ReverseMap();
    }
}
