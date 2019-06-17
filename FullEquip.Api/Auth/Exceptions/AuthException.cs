using System;

namespace FullEquip.Api.Auth.Exceptions
{
    public class AuthException : Exception
    {
        public AuthException(string message) : base(message) { }
    }
}
