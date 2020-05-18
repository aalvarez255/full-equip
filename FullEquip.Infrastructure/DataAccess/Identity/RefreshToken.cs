using System;

namespace FullEquip.Infrastructure.DataAccess.Identity
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public Guid UserId { get; set; }

        public bool Active => DateTime.UtcNow <= Expires;
    }
}
