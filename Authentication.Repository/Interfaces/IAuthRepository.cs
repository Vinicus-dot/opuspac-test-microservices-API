using Authentication.Model.Entity;

namespace Authentication.Repository.Interfaces
{
    public interface IAuthRepository
    {
        Task InsertUser(User user);
        Task<User?> GetUser(string email);
    }
}
