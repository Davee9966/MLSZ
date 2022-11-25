using Microsoft.EntityFrameworkCore;
using MLSZ.Data;
using MLSZ.Entities;
using System.Security.Claims;
using System.Security.Cryptography;

namespace MLSZ.Services.UserService
{

    
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MlszContext _ctx;

        public UserService(IHttpContextAccessor httpContextAccessor, MlszContext ctx)
        {
            _httpContextAccessor = httpContextAccessor;
            _ctx = ctx;
        }

        public async Task<User[]?> GetAllUsers()
        {
            return await _ctx.Users.ToArrayAsync();
        }

        public Task<User?> GetUser(int userId)
        {
            return _ctx.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User?> CreateUser(UserDto user)
        {
            var pwTuple = CreatePasswordHash(user.Password);
            User newUser = new User { 
                Name = user.Name,
                Email = user.Email,
                Org = user.Org,
                Role = user.Role,
                Phone = user.Phone,
                Position = user.Position,
                PwSalt = pwTuple.Item1,
                PwHash = pwTuple.Item2
            };

            await _ctx.Users.AddAsync(newUser);
            await _ctx.SaveChangesAsync();
            return newUser;
        }

        public async Task<User?> EditUser(User user)
        {
            _ctx.Users.Update(user);
            await _ctx.SaveChangesAsync();
            return user;

        }

        public async Task DeleteUser(int userId)
        {
           
            var user = await _ctx.Users.FindAsync(userId);
            if (user != null)
            {
                _ctx.Users.Remove(user);
            }

            await _ctx.SaveChangesAsync();
            
        }

        public async Task<User> FindUserByEmail(string email)
        {
            return await _ctx.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public string GetMyEmail()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            }
            return result;
        }

        /// <summary>
        /// Generates a salt and hash from a string.
        /// </summary>
        /// <param name="password"></param>
        /// <returns>Returns a tuple with passwordSalt and passwordHash</returns>
        public (byte[], byte[]) CreatePasswordHash(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                return (hmac.Key, hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
            }
        }


    }
    
}
