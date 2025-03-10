
using Model.Authentication.Entity;

namespace Repository.Interfaces
{
    public interface IAuthRepository
    {
        Task InsertUser(User user);
        Task<User?> GetUser(string email);
    }
}
