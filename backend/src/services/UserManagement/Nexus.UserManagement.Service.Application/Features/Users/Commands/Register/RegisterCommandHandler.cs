using MediatR;
using Nexus.UserManagement.Service.Domain.Models;
using Nexus.UserManagement.Service.Domain.SmartEnums;
using Nexus.UserManagement.Service.Domain.ValueObjects.Role;
using Nexus.UserManagement.Service.Domain.ValueObjects.UserAuthenticator;
using Nexus.UserManagement.Service.Domain.ValueObjects.UserSecurityAsset;
using Crossdyne.Toolkit.Results;
using Nexus.UserManagement.Service.Domain.ValueObjects.User;
using Nexus.UserManagement.Service.Domain.ValueObjects.Deks;
using Nexus.UserManagement.Service.Application.Abstractions.Repositories;
using Nexus.UserManagement.Service.Application.Abstractions.UnitOfWork;
using System.Security.Cryptography;
using Shared.Kernel.Errors;
using Shared.Kernel.Exceptions;

namespace Nexus.UserManagement.Service.Application.Features.Users.Commands.Register
{
    public sealed class RegisterCommandHandler(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository) : IRequestHandler<RegisterCommand, Result>
    {
        private static readonly char[] FriendshipAlphabet = "23456789ABCDEFGHJKMNPQRSTVWXYZ".ToCharArray();

        public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await userRepository.CheckAvailableEmail(Email.Create(request.Email)))
                return Result.Failure(new Error(ErrorCode.Conflict, "Email уже занят."));

            string normalizeLogin = request.Login.ToLowerInvariant();

            var user = User.Create(Login.Create(normalizeLogin), UserName.Create(request.UserName), Email.Create(request.Email), await GenerateFriendshipCodeAsync(cancellationToken), EnumStatus.Active.Id, request.IdGender, request.IdCountry);
            user.AddMainDek(EncryptedValue.Create(request.EncryptedDek), Salt.Create(request.DekSalt), CryptoVersion.Create(request.CryptoVersion));
            user.AddSrpAuthenticator(Login.Create(normalizeLogin), Verificator.Create(request.EncryptedVerifier), Salt.Create(request.SrpSalt), SrpVersion.Create(request.SrpVersion), CredentialBlob.Create(request.EncryptedVerifierWrapKey), CryptoVersion.Create(request.KeyWrapVersion), AsymmetricKeyId.Create(request.AsymmetricKeyId), CryptoVersion.Create(request.SrpCryptoVersion));
            user.AddEmailAuthenticator(Email.Create(request.Email));
            request.RecoveryKeys.ToList().ForEach(x => user.AddRecoveryKey(EncryptedValue.Create(x.EncryptedValue), CryptoVersion.Create(x.CryptoVersion), KeyHint.Create("1")));
            user.AddRole(RoleId.From(EnumRole.User.Id));

            await userRepository.AddAsync(user, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            user.ClearDomainEvents();

            return Result.Success();
        }

        private async Task<FriendshipCode> GenerateFriendshipCodeAsync(CancellationToken cancellationToken)
        {
            const int partLength = 5;
            const int maxRetries = 5;

            for (int attempt = 0; attempt < maxRetries; attempt++)
            {
                string firstPart = new(RandomNumberGenerator.GetItems(FriendshipAlphabet, partLength));
                string secondPart = new(RandomNumberGenerator.GetItems(FriendshipAlphabet, partLength));
                string thirdPart = new(RandomNumberGenerator.GetItems(FriendshipAlphabet, partLength));

                string fullCode = $"{firstPart}-{secondPart}-{thirdPart}";

                var code = FriendshipCode.Create(fullCode);

                if (!await userRepository.ExistFriendshipCode(code, cancellationToken))
                    return code;
            }

            throw new DomainException(new Error(AppErrors.FriendshipCodeExisting, "Не удалось сгенерировать уникальный код друга после нескольких попыток."));
        }
    }
}