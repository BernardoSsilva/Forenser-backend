using ForenserBackend.Domain.entities;
using ForenserBackend.Domain.RepositoriesInterfaces;
using ForenserBackend.Exception.HttpErrors;
using Microsoft.EntityFrameworkCore;

namespace ForenserBackend.Infrastructure.repositories
{
    public class UsersRepository : IUserRepository
    {
        private readonly ForenserDbContext _context;


        public UsersRepository(ForenserDbContext context)
        {
            _context = context;
        }
        public async Task DeleteUser(string userId)
        {
            var userToDelete = await _context.Users.FirstOrDefaultAsync(user => user.Id == userId) ;
            if (userToDelete is null)
            {
                throw new NotFoundException("User not found");
            }
            _context.Remove(userToDelete);
        }

        public async Task<List<UserEntity>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<UserEntity> GetUserByCpf(string userCpf)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.CPF == userCpf);
            if (user is null)
            {
                throw new NotFoundException("User not found");
            }
            return user;
        }

        public async Task<UserEntity> GetUserByEmail(string userEmail)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.UserEmail == userEmail);
            if (user is null)
            {
                throw new NotFoundException("User not found");
            }
            return user;
        }

        public async Task<UserEntity> GetUserById(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == userId);
            if(user is null)
            {
                throw new NotFoundException("User not found");
            }
            return user;
        }

        public async Task RegisterNewUser(UserEntity userData)
        {
            var userExists = await _context.Users.FirstOrDefaultAsync(u => u.CPF == userData.CPF || u.UserEmail == userData.UserEmail);
            if(userExists != null)
            {
                throw new ConflictException("User Already Exists");
            }
            await _context.AddAsync(userData);
        }

        public void UpdateUser(UserEntity newUserData)
        {
            _context.Users.Update(newUserData);
        }
    }
}
