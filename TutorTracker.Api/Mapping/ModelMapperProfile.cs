namespace TutorTracker.Api.Mapping;

using AutoMapper;
using M = Model;
using E = Persistence.Entities;

public class ModelMapperProfile : Profile
{
    public ModelMapperProfile()
    {
        CreateMap<M.Student, E.Student>();
        CreateMap<M.Customer, E.Customer>();
    }
}