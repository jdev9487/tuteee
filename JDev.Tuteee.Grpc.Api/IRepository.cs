namespace JDev.Tuteee.Grpc.Api;

using DAL.Entities;

public interface IRepository
{
    Task<IReadOnlyList<Client>> GetBillableClientsAsync(CancellationToken token);
}