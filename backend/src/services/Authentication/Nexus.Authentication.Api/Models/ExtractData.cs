using Microsoft.AspNetCore.Mvc;

namespace Nexus.Authentication.Api.Models
{
   public sealed class ExtractData() 
    {
        public Guid UserId { get; set; }
        public string AccessToken { get; set; } = null!;
        public IActionResult Result { get; set; } = null!;
    }
}