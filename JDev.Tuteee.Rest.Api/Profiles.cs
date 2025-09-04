namespace JDev.Tuteee.Rest.Api;

using AutoMapper;
using DAL.Entities;
using ApiClient.DTOs;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<Client, ClientDto>().ReverseMap();
    }
}

public class InvoiceProfile : Profile
{
    public InvoiceProfile()
    {
        CreateMap<Invoice, InvoiceDto>().ReverseMap();
    }
}

public class LessonProfile : Profile
{
    public LessonProfile()
    {
        CreateMap<Lesson, LessonDto>().ReverseMap();
    }
}

public class RateProfile : Profile
{
    public RateProfile()
    {
        CreateMap<Rate, RateDto>().ReverseMap();
    }
}

public class TuteeProfile : Profile
{
    public TuteeProfile()
    {
        CreateMap<Tutee, TuteeDto>().ReverseMap();
    }
}