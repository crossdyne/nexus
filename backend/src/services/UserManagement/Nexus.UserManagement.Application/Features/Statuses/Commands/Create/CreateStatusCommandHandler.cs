using MediatR;
using Crossdyne.Toolkit.Results;
using Nexus.UserManagement.Application.Abstractions.Repositories;
using Nexus.UserManagement.Application.Abstractions.UnitOfWork;
using Nexus.UserManagement.Domain.Models;

namespace Nexus.UserManagement.Application.Features.Statuses.Commands.Create
{
    public sealed class CreateStatusCommandHandler(
        IUnitOfWork unitOfWork,
        IStatusRepository statusRepository) : IRequestHandler<CreateStatusCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateStatusCommand request, CancellationToken cancellationToken)
        {
            Status status = Status.Create(request.Name);

            await statusRepository.AddAsync(status, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return status.Id;
        }
    }
}