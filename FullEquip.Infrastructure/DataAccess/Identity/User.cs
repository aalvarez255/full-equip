using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FullEquip.Infrastructure.DataAccess.Identity
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {
            RefreshTokens = new List<RefreshToken>();
        }

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }

        public bool HasValidRefreshToken(string refreshToken)
        {
            return RefreshTokens.Any(rt => rt.Token == refreshToken && rt.Active);
        }

        public void AddRefreshToken(string token, double daysToExpire = 7)
        {
            RefreshTokens.Add(new RefreshToken()
            {
                Token = token,
                Expires = DateTime.UtcNow.AddDays(daysToExpire),
                UserId = Id,
            });
        }

        public void RemoveRefreshToken(string refreshToken)
        {
            RefreshTokens.Remove(RefreshTokens.First(t => t.Token == refreshToken));
        }
    }
}
