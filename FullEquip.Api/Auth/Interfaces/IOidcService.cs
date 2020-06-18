using System.Threading.Tasks;

namespace FullEquip.Api.Auth.Interfaces
{
    public interface IOidcService
    {
        string GetAzureLoginUrl();
        Task<string> GetAzureUserAsync(string code);
    }
}
