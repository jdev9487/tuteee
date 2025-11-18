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
            .ForMember(dto => dto.FirstName, cfg => cfg.MapFrom(cr => cr.Stakeholder.FirstName))
            .ForMember(dto => dto.LastName, cfg => cfg.MapFrom(cr => cr.Stakeholder.LastName))
            .ForMember(dto => dto.EmailAddress, cfg => cfg.MapFrom(cr => cr.Stakeholder.EmailAddress))
            .ForMember(dto => dto.PhoneNumber, cfg => cfg.MapFrom(cr => cr.Stakeholder.PhoneNumber))
            .ForMember(dto => dto.StakeholderId, cfg => cfg.MapFrom(cr => cr.StakeholderId))
            .ForMember(dto => dto.Tutees, cfg => cfg.MapFrom(cr => cr.TuteeRoles))
            .ForMember(dto => dto.Invoices, cfg => cfg.MapFrom(cr => cr.Invoices))
            .ForMember(dto => dto.ClientId, cfg => cfg.MapFrom(cr => cr.ClientRoleId));
        CreateMap<ClientDto, ClientRole>()
            .ForMember(cr => cr.Stakeholder, cfg => cfg.MapFrom(dto => new Stakeholder
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
        CreateMap<Invoice, InvoiceDto>()
            .ForMember(dto => dto.ClientId, cfg => cfg.MapFrom(i => i.ClientRoleId))
            .ForMember(dto => dto.Client, cfg => cfg.MapFrom(i => i.ClientRole));
    }
}

public class LessonProfile : Profile
{
    public LessonProfile()
    {
        CreateMap<Lesson, LessonDto>()
            .ForMember(dto => dto.TuteeId, cfg => cfg.MapFrom(l => l.TuteeRoleId))
            .ForMember(dto => dto.Tutee, cfg => cfg.MapFrom(l => l.TuteeRole));
        CreateMap<LessonDto, Lesson>()
            .ForMember(l => l.TuteeRoleId, cfg => cfg.MapFrom(dto => dto.TuteeId));
    }
}

public class ReservationSlotProfile : Profile
{
    public ReservationSlotProfile()
    {
        CreateMap<ReservationSlot, ReservationSlotDto>()
            .ForMember(dto => dto.TuteeId, cfg => cfg.MapFrom(l => l.TuteeRoleId))
            .ForMember(dto => dto.Tutee, cfg => cfg.MapFrom(l => l.TuteeRole));
        CreateMap<ReservationSlotDto, ReservationSlot>()
            .ForMember(l => l.TuteeRoleId, cfg => cfg.MapFrom(dto => dto.TuteeId));
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
            .ForMember(dto => dto.FirstName, cfg => cfg.MapFrom(tr => tr.Stakeholder.FirstName))
            .ForMember(dto => dto.LastName, cfg => cfg.MapFrom(tr => tr.Stakeholder.LastName))
            .ForMember(dto => dto.EmailAddress, cfg => cfg.MapFrom(tr => tr.Stakeholder.EmailAddress))
            .ForMember(dto => dto.PhoneNumber, cfg => cfg.MapFrom(tr => tr.Stakeholder.PhoneNumber))
            .ForMember(dto => dto.StakeholderId, cfg => cfg.MapFrom(tr => tr.StakeholderId))
            .ForMember(dto => dto.Lessons, cfg => cfg.MapFrom(tr => tr.Lessons))
            .ForMember(dto => dto.Rates, cfg => cfg.MapFrom(tr => tr.Rates))
            .ForMember(dto => dto.TuteeId, cfg => cfg.MapFrom(tr => tr.TuteeRoleId))
            .ForMember(dto => dto.ClientId, cfg => cfg.MapFrom(tr => tr.ClientRoleId))
            .ForMember(dto => dto.Client, cfg => cfg.MapFrom(tr => tr.ClientRole));
        CreateMap<TuteeDto, TuteeRole>()
            .ForMember(tr => tr.ClientRoleId, cfg => cfg.MapFrom(dto => dto.ClientId))
            .ForMember(tr => tr.Rates, cfg => cfg.MapFrom(dto => dto.Rates));
        CreateMap<TuteeDto, Stakeholder>();
    }
}