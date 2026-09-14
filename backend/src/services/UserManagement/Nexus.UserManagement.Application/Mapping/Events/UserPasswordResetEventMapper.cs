using AutoMapper;
using Nexus.UserManagement.Domain.Events;
using Shared.Contracts.UserManagement.Events;

namespace Nexus.UserManagement.Application.Mapping.Events
{
    public sealed class UserPasswordResetEventMapper : Profile
    {
        public UserPasswordResetEventMapper()
        {
            CreateMap<UserPasswordResetDomainEvent, UserPasswordResetIntegrationEvent>()
                .ForMember(dest => dest.IdEvent, opt => opt.MapFrom(src => src.IdEvent))
                .ForMember(dest => dest.OccurredOnUtc, opt => opt.MapFrom(src => src.OccurredOnUtc.ToString("O")))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId.Value));
        }
    }
}