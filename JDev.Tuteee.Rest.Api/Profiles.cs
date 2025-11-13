namespace JDev.Tuteee.Rest.Api;

using AutoMapper;
using DAL.Entities;
using ApiClient.DTOs;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<ClientRole, ClientDto>()
            .ForMember(dto => dto.FirstName, cfg => cfg.MapFrom(cr => cr.TuitionStakeholder.FirstName))
            .ForMember(dto => dto.LastName, cfg => cfg.MapFrom(cr => cr.TuitionStakeholder.LastName))
            .ForMember(dto => dto.EmailAddress, cfg => cfg.MapFrom(cr => cr.TuitionStakeholder.EmailAddress))
            .ForMember(dto => dto.PhoneNumber, cfg => cfg.MapFrom(cr => cr.TuitionStakeholder.PhoneNumber))
            .ForMember(dto => dto.Tutees, cfg => cfg.MapFrom(cr => cr.TuteeRoles))
            .ForMember(dto => dto.Invoices, cfg => cfg.MapFrom(cr => cr.Invoices))
            .ForMember(dto => dto.ClientId, cfg => cfg.MapFrom(cr => cr.ClientRoleId));
        CreateMap<ClientDto, ClientRole>()
            .ForMember(cr => cr.TuitionStakeholder, cfg => cfg.MapFrom(dto => new TuitionStakeholder
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                EmailAddress = dto.EmailAddress,
                PhoneNumber = dto.PhoneNumber
            }))
            .ForMember(cr => cr.ClientRoleId, cfg => cfg.MapFrom(dto => dto.ClientId));
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
        CreateMap<TuteeRole, TuteeDto>()
            .ForMember(dto => dto.FirstName, cfg => cfg.MapFrom(tr => tr.TuitionStakeholder.FirstName))
            .ForMember(dto => dto.LastName, cfg => cfg.MapFrom(tr => tr.TuitionStakeholder.LastName))
            .ForMember(dto => dto.EmailAddress, cfg => cfg.MapFrom(tr => tr.TuitionStakeholder.EmailAddress))
            .ForMember(dto => dto.PhoneNumber, cfg => cfg.MapFrom(tr => tr.TuitionStakeholder.PhoneNumber))
            .ForMember(dto => dto.Lessons, cfg => cfg.MapFrom(tr => tr.Lessons))
            .ForMember(dto => dto.Rates, cfg => cfg.MapFrom(tr => tr.Rates));
        CreateMap<TuteeDto, TuteeRole>()
            .ForMember(tr => tr.TuitionStakeholder, cfg => cfg.MapFrom(dto => new TuitionStakeholder
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                EmailAddress = dto.EmailAddress,
                PhoneNumber = dto.PhoneNumber
            }))
            .ForMember(tr => tr.ClientRoleId, cfg => cfg.MapFrom(dto => dto.ClientId))
            .ForMember(tr => tr.Rates, cfg => cfg.MapFrom(dto => dto.Rates));
    }
}