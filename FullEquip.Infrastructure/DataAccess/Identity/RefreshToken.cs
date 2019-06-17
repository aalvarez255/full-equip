using FullEquip.Core.Entities;
using System;

namespace FullEquip.Infrastructure.DataAccess.Identity
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public Guid UserId { get; set; }

        public bool Active => DateTime.UtcNow <= Expires;
    }
}
