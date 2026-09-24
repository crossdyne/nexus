namespace Nexus.UserManagement.Application.Abstractions.Validators
{
    public interface IHasEncryptedDek
    {
        public string EncryptedVerifierWrapKey { get; }
    }
}