using MediatR;
using Nexus.UserManagement.Application.Abstractions.Repositories;
using Shared.Contracts.UserManagement.Responses;

namespace Nexus.UserManagement.Application.Features.Countries.Queries.GetAll
{
    public sealed class GetAllCountriesQueryHandler(ICountryReadOnlyRepository countryRepository) : IRequestHandler<GetAllCountriesQuery, List<CountryResponse>>
    {
        public async Task<List<CountryResponse>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
            => [.. await countryRepository.GetAllAsync(cancellationToken)];      
    }
}