using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Mwa.Api.Security
{
    public class TokenOptions
    {
        public string Issuer { get; set; }
        public string Subject { get; set; }
        public string Audience { get; set; }
        public DateTime NotBefore { get; set; } = DateTime.UtcNow;
        public DateTime IssueAt { get; set; } = DateTime.UtcNow;
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromDays(30);
        public DateTime Expiration =>IssueAt.Add(ValidFor);

        public Func<Task<string>> JtiGenerator =>
        () => Task.FromResult(Guid.NewGuid().ToString());

        public SigningCredentials SigningCredentials { get; set; }
    }
}