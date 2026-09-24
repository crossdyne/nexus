using MediatR;
using Nexus.UserManagement.Application.Abstractions.Messaging;
using Shared.Contracts.UserManagement.Responses;

namespace Nexus.UserManagement.Application.Features.Countries.Queries.GetAll
{
    public sealed record GetAllCountriesQuery() : IRequest<List<CountryResponse>>, IQuery;
}