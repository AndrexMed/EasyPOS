namespace Domain.Primitives
{
    public interface IUnityOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
