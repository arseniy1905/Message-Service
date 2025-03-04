using System;

namespace MessageService.IRepository.Unit
{
    public interface IUnit : IDisposable
    {
        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken=default);

    }
}
