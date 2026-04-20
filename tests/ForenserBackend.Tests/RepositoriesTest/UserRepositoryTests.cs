using ForenserBackend.Domain.entities;
using ForenserBackend.Domain.RepositoriesInterfaces;
using ForenserBackend.Exception.HttpErrors;
using ForenserBackend.Infrastructure;
using ForenserBackend.Infrastructure.repositories;
using Microsoft.EntityFrameworkCore;

namespace ForenserBackend.Tests.RepositoriesTest
{
    public class UserRepositoryTests
    {

        private readonly UsersRepository _userRepository;
        private readonly ForenserDbContext _dbContext;

        public UserRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ForenserDbContext>()
                .UseInMemoryDatabase(databaseName:  Guid.NewGuid().ToString())
                .Options;

            _dbContext = new ForenserDbContext(options);
            _userRepository = new UsersRepository(_dbContext);
           
        }

        [Fact]
        public async Task UserRepository_ShouldBeAbleToCreateANewUser()
        {
           
            var newUser = new UserEntity
            {
                BornDate = DateTime.Now,
                CPF = "000.000.000-00",
                Password = "test password",
                UserEmail = "test email",
                UserName = "test name",

            };

            await _userRepository.RegisterNewUser(newUser);
            await _dbContext.SaveChangesAsync();

            var RegisteredUser = await _userRepository.GetUserByCpf(newUser.CPF);
            Assert.NotNull(RegisteredUser.Id);
            Assert.Equal(RegisteredUser.UserName, newUser.UserName);
        }

        [Fact]
        public async Task UserRepository_ShouldThrowErrorWhenHasConflitantInformation()
        {
            var newUser = new UserEntity
            {
                BornDate = DateTime.Now,
                CPF = "000.000.000-00",
                Password = "test password",
                UserEmail = "test email",
                UserName = "test name",

            };

            await _userRepository.RegisterNewUser(newUser);
            await _dbContext.SaveChangesAsync();


            await Assert.ThrowsAsync<ConflictException>(async () => await _userRepository.RegisterNewUser(newUser));
        }

        [Fact]
        public async Task UserRepository_ShouldFindUserById()
        {
            var newUser = new UserEntity
            {
                BornDate = DateTime.Now,
                CPF = "000.000.000-00",
                Password = "test password",
                UserEmail = "test email",
                UserName = "test name",

            };

            await _userRepository.RegisterNewUser(newUser);
            await _dbContext.SaveChangesAsync();

            var user = await _userRepository.GetUserById(newUser.Id);
            Assert.NotNull(user);
        }
        [Fact]
        public async Task UserRepository_ShouldFindUserByEmail()
        {
            var newUser = new UserEntity
            {
                BornDate = DateTime.Now,
                CPF = "000.000.000-00",
                Password = "test password",
                UserEmail = "test email",
                UserName = "test name",

            };

            await _userRepository.RegisterNewUser(newUser);
            await _dbContext.SaveChangesAsync();

            var user = await _userRepository.GetUserByEmail(newUser.UserEmail);
            Assert.NotNull(user);
        }
        [Fact]
        public async Task UserRepository_ShouldFindUserByCpf()
        {
            var newUser = new UserEntity
            {
                BornDate = DateTime.Now,
                CPF = "000.000.000-00",
                Password = "test password",
                UserEmail = "test email",
                UserName = "test name",

            };

            await _userRepository.RegisterNewUser(newUser);
            await _dbContext.SaveChangesAsync();

            var user = await _userRepository.GetUserByCpf(newUser.CPF);
            Assert.NotNull(user);
        }

        [Fact]
            public async Task UserRepository_ShouldThrowErrorIfNotFindUserById()
            {
             
               
               await Assert.ThrowsAsync<NotFoundException>(async() => await _userRepository.GetUserById("000"));
            }
        [Fact]
        public async Task UserRepository_ShouldThrowErrorIfNotFindUserByEmail()
        {
           

            await Assert.ThrowsAsync<NotFoundException>(async () => await _userRepository.GetUserByEmail("000"));
        }
        [Fact]
        public async Task UserRepository_ShouldThrowErrorIfNotFindUserByCpf()
        {
        

            await Assert.ThrowsAsync<NotFoundException>(async () => await _userRepository.GetUserByCpf("000"));
        }

        [Fact]
        public async Task UserRepository_ShouldDeleteAUSer()
        {

            var newUser = new UserEntity
            {
                BornDate = DateTime.Now,
                CPF = "000.000.000-00",
                Password = "test password",
                UserEmail = "test email",
                UserName = "test name",

            };

            await _userRepository.RegisterNewUser(newUser);
            await _dbContext.SaveChangesAsync();

            await _userRepository.DeleteUser(newUser.Id);

            var users = await _userRepository.GetAllUsers();

            Assert.Equal(1, users.Count);


            await _dbContext.SaveChangesAsync();
            var newUsersSelect = await _userRepository.GetAllUsers();

            Assert.Equal(0, newUsersSelect.Count);


        }

        [Fact]
        public async Task UserRepository_ShouldThrowErrorIfDeleteANonExistentUser()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () => await _userRepository.DeleteUser("testId"));
        }

        [Fact]
        public async Task UserRepository_ShouldUpdateOneUser()
        {

            var newUser = new UserEntity
            {
                BornDate = DateTime.Now,
                CPF = "000.000.000-00",
                Password = "test password",
                UserEmail = "test email",
                UserName = "test name",

            };

            await _userRepository.RegisterNewUser(newUser);
            await _dbContext.SaveChangesAsync();

            var newUserData = newUser;
            newUserData.UserEmail = "testSecondEmail";
            _userRepository.UpdateUser(newUserData);
            await _dbContext.SaveChangesAsync();

            var registeredUser = await _userRepository.GetUserById(newUserData.Id);
            Assert.NotNull(registeredUser);
            Assert.Equal("testSecondEmail", registeredUser.UserEmail);


        }



        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted(); 
            _dbContext.Dispose();
        }
    }
}
