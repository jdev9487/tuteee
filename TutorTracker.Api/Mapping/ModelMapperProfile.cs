namespace TutorTracker.Api.Mapping;

using AutoMapper;
using M = Model;
using E = Entities;

public class ModelMapperProfile : Profile
{
    public ModelMapperProfile()
    {
        CreateMap<M.Student, E.Student>();
    }
}