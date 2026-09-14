using MediatR;
using Crossdyne.Toolkit.Results;
using Shared.Abstractions.Validations;
using Nexus.UserManagement.Application.Abstractions.Messaging;
using Nexus.UserManagement.Application.Abstractions.Validators;

namespace Nexus.UserManagement.Application.Features.Users.Commands.ResetPassword
{
    public sealed record ResetPasswordCommand(
        string Login, 
        // Srp
        string EncryptedVerifier, 
        string SrpSalt, 
        int SrpVersion, 
        int SrpCryptoVersion,
        string EncryptedVerifierWrapKey, 
        int KeyWrapVersion, 
        string AsymmetricKeyId,
        // Dek 
        string EncryptedDek,
        string DekSalt, 
        int CryptoVersion, 
        // RecoveryKeys
        List<RecoveryKeyCommandData> RecoveryKeys) : IRequest<Result>, ICommand,
        IHasLogin,
        IHasEncryptedVerifier, 
        IHasSrpSalt, 
        IHasEncryptedDek;

    public record RecoveryKeyCommandData(string EncryptedValue, int CryptoVersion);
}