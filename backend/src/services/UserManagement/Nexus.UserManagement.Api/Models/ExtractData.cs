namespace Nexus.UserManagement.Api.Models
{
    public sealed class ExtractData() 
    {
        public Guid UserId { get; set; }
        public string Login { get; set; } = null!;
    }
}