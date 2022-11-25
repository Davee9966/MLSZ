using MLSZ.Entities;

namespace MLSZ.Services.UserService
{
    public interface IUserService
    {
        Task<User[]?> GetAllUsers();
        Task<User?> GetUser(int userId);
        Task<User?> CreateUser(UserDto user);
        Task<User?> EditUser(User user);
        Task DeleteUser(int userId);
        string GetMyEmail();
        Task<User?> FindUserByEmail(string email);
    }
}
