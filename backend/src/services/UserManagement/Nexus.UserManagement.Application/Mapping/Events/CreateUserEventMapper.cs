using AutoMapper;
using Nexus.UserManagement.Domain.Events;
using Shared.Contracts.UserManagement.Events;

namespace Nexus.UserManagement.Application.Mapping.Events
{
    public sealed class CreateUserEventMapper : Profile
    {
        public CreateUserEventMapper()
        {
            CreateMap<UserCreatedDomainEvent, UserCreatedIntegrationEvent>()
                .ConstructUsing(src => new UserCreatedIntegrationEvent(
                    IdEvent: src.IdEvent,
                    OccurredOnUtc: src.OccurredOnUtc,
                    UserId: src.UserId,
                    UserName: src.UserName
                ));
        }
    }
}