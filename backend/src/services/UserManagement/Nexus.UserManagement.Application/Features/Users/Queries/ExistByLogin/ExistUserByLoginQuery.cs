using Crossdyne.Toolkit.Results;
using MediatR;
using Nexus.UserManagement.Application.Abstractions.Messaging;
using Shared.Abstractions.Validations;

namespace Nexus.UserManagement.Application.Features.Users.Queries.ExistByLogin
{
    public sealed record class ExistUserByLoginQuery(string Login) : IRequest<Result>, IHasLogin, IQuery;
}