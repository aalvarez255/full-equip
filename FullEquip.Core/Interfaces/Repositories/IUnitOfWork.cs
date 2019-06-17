using System;
using System.Threading.Tasks;

namespace FullEquip.Core.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CommitAsync();
    }
}
