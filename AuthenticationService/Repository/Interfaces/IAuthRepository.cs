using AuthenticationService.Model.Entity;

namespace AuthenticationService.Repository.Interfaces
{
    public interface IAuthRepository
    {
        Task InsertUser(User user);
        Task<User?> GetUser(string email);
    }
}
