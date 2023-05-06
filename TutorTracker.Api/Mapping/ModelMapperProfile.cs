using TutorTracker.Api.Entities;

namespace TutorTracker.Api.Mapping;

using AutoMapper;
using M = Model;

public class ModelMapperProfile : Profile
{
    public ModelMapperProfile()
    {
        CreateMap<M.Student, Student>();
        CreateMap<M.Customer, Customer>();
        CreateMap<M.Lesson, Lesson>();
    }
}