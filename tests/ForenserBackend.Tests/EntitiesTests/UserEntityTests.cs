using ForenserBackend.Domain.entities;
using Xunit;

namespace ForenserBackend.Tests.Entities
    {
        public class UserEntityTests
        {

            [Fact]
            public void UserEntity_ShouldInitializeWithDefaultValues()
            {
                // Act
                var user = new UserEntity
                {
                    UserName = "John Doe",
                    CPF = "123.456.789-00",
                    BornDate = new DateTime(1990, 5, 1),
                    UserEmail = "johndoe@example.com",
                    Password = "StrongPassword123"
                }; ;

                // Assert
                Assert.NotNull(user.Id);
                Assert.Equal(DateTime.UtcNow.Date, user.CreatedAt.Date); // Valida o CreatedAt, considerando a data atual
                Assert.Empty(user.ContactPhonesNumbers); // Lista de números de telefone deve ser vazia
            }

            [Fact]
            public void UserEntity_ShouldRequireMandatoryFields()
            {
                // Arrange
                var user = new UserEntity
                {
                    UserName = "John Doe",
                    CPF = "123.456.789-00",
                    BornDate = new DateTime(1990, 5, 1),
                    UserEmail = "johndoe@example.com",
                    Password = "StrongPassword123"
                };

                // Assert
                Assert.Equal("John Doe", user.UserName);
                Assert.Equal("123.456.789-00", user.CPF);
                Assert.Equal(new DateTime(1990, 5, 1), user.BornDate);
                Assert.Equal("johndoe@example.com", user.UserEmail);
                Assert.Equal("StrongPassword123", user.Password);
            }

            [Fact]
            public void UserEntity_ShouldAllowAddingContactPhoneNumbers()
            {
                // Arrange
                var user = new UserEntity
                {
                    UserName = "John Doe",
                    CPF = "123.456.789-00",
                    BornDate = new DateTime(1990, 5, 1),
                    UserEmail = "johndoe@example.com",
                    Password = "StrongPassword123"
                };

                // Act
                user.ContactPhonesNumbers.Add("123-456-7890");
                user.ContactPhonesNumbers.Add("098-765-4321");

                // Assert
                Assert.Equal(2, user.ContactPhonesNumbers.Count);
                Assert.Contains("123-456-7890", user.ContactPhonesNumbers);
                Assert.Contains("098-765-4321", user.ContactPhonesNumbers);
            }
        }
    }

