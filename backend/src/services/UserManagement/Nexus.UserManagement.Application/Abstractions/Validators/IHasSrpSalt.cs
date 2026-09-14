namespace Nexus.UserManagement.Application.Abstractions.Validators
{
    public interface IHasSrpSalt
    {
        public string SrpSalt { get; }
    }
}