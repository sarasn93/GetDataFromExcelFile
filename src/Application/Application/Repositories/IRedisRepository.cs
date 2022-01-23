using Domain.Entities;
using System.Threading.Tasks;

namespace Core.Application.Repositories
{
    public interface IRedisRepository
    {
        Task AddUser(User data);
        Task AddOrder(Order data);
    }
}
