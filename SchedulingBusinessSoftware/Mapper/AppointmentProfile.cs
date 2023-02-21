using AutoMapper;
using SchedulingBusinessSoftware.Entities;
using SchedulingBusinessSoftware.Models;

namespace SchedulingBusinessSoftware.Mapper
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile() 
        {
            CreateMap<Appointment, AppointmentViewModel>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ScheduledAt, opt => opt.MapFrom(src => src.ScheduledAt))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedDate));
        }
    }
}