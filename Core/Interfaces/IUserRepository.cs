using Core.Entities;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAsync(string username);
    }
}
