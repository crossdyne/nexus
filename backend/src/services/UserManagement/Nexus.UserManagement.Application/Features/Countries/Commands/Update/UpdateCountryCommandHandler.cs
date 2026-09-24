using MediatR;
using Crossdyne.Toolkit.Results;
using Crossdyne.Toolkit.Primitives;
using Nexus.UserManagement.Application.Abstractions.Repositories;
using Nexus.UserManagement.Application.Abstractions.UnitOfWork;
using Nexus.UserManagement.Domain.Models;
using Nexus.UserManagement.Domain.ValueObjects.Country;

namespace Nexus.UserManagement.Application.Features.Countries.Commands.Update
{
    public sealed class UpdateCountryCommandHandler(
        IUnitOfWork unitOfWork, 
        ICountryRepository countryRepository) : IRequestHandler<UpdateCountryCommand, Result>
    {
        public async Task<Result> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            Maybe<Country> maybeCountry = await countryRepository.GetByAsync(c => c.Id == request.Id, cancellationToken);

            if (maybeCountry.IsNone)
                return Result.Failure(new Error(ErrorCode.Update, "Такой записи не существует."));

            Country country = maybeCountry.Value;

            country.UpdateName(CountryName.Create(request.Name));

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}