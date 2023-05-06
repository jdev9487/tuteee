namespace TutorTracker.Api.Mapping;

using AutoMapper;
using M = Model;
using E = Entities;

public class ModelMapperProfile : Profile
{
    public ModelMapperProfile()
    {
        CreateMap<M.CreateStudent, E.Student>();
        CreateMap<M.CreateCustomer, E.Customer>();
        CreateMap<M.Lesson, E.Lesson>();

        CreateMap<E.Customer, M.CustomerResult>()
            .ForMember(dest => dest.StudentIds, opt => opt.MapFrom(src => src.Students.Select(x => x.Id).ToArray()));
        CreateMap<E.Student, M.StudentResult>()
            .ForMember(dest => dest.LessonIds, opt => opt.MapFrom(src => src.Lessons.Select(x => x.Id).ToArray()));
        CreateMap<E.Lesson, M.LessonResult>();
    }
}