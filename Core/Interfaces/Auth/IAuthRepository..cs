
using Core.Entities;

namespace Core.Interfaces

{


   public interface IAuthRepository
{

        //Task<string> LoginAsync(LoginDto loginDto);

        Task<string> LoginAsync(string username, string password);


    }


}
