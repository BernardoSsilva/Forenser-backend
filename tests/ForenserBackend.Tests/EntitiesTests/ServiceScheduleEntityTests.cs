using ForenserBackend.Domain.entities;
using ForenserBackend.Domain.Enums;

namespace ForenserBackend.Tests.EntitiesTests
{
    public class ServiceScheduleEntityTests
    {
        [Fact]
        public void ServiceScheduleEntity_ShouldInitializeWithDefaultValues()
        {
            // Act
            var serviceSchedule = new ServiceScheduleEntity
            {
                UserId = "user123",
                City = "Springfield",
                State = Ufs.SP,
                Type = ServiceType.DocumentAuthentication,
                PoliceUnity = "Police Station 5",
                ContactPhone = "123-456-7890"
            }; ;

            // Assert
            Assert.NotNull(serviceSchedule.Id); // Verifica se o Id é inicializado com um valor gerado automaticamente
            Assert.Equal(string.Empty, serviceSchedule.PostalCode); // Verifica se o código postal é inicializado como uma string vazia
        }

        [Fact]
        public void ServiceScheduleEntity_ShouldRequireMandatoryFields()
        {
            // Arrange
            var serviceSchedule = new ServiceScheduleEntity
            {
                UserId = "user123",
                City = "Springfield",
                State = Ufs.SP,
                Type = ServiceType.DocumentAuthentication,
                PoliceUnity = "Police Station 5",
                ContactPhone = "123-456-7890"
            };

            // Assert
            Assert.Equal("user123", serviceSchedule.UserId);
            Assert.Equal("Springfield", serviceSchedule.City);
            Assert.Equal(Ufs.SP, serviceSchedule.State);
            Assert.Equal(ServiceType.DocumentAuthentication, serviceSchedule.Type);
            Assert.Equal("Police Station 5", serviceSchedule.PoliceUnity);
            Assert.Equal("123-456-7890", serviceSchedule.ContactPhone);
        }

        [Fact]
        public void ServiceScheduleEntity_ShouldAllowOptionalPostalCode()
        {
            // Arrange
            var serviceSchedule = new ServiceScheduleEntity
            {
                UserId = "user123",
                City = "Springfield",
                State = Ufs.RJ,
                Type = ServiceType.PublicService,
                PoliceUnity = "Police Station 3",
                ContactPhone = "098-765-4321",
                PostalCode = "12345-678"
            };

            // Assert
            Assert.Equal("12345-678", serviceSchedule.PostalCode);
        }

        [Fact]
        public void ServiceScheduleEntity_ShouldAllowDefaultPostalCode()
        {
            // Arrange
            var serviceSchedule = new ServiceScheduleEntity
            {
                UserId = "user123",
                City = "Metropolis",
                State = Ufs.MG,
                Type = ServiceType.PublicService,
                PoliceUnity = "Police Station 7",
                ContactPhone = "111-222-3333"
            };

            // Assert
            Assert.Equal(string.Empty, serviceSchedule.PostalCode); // Verifica se o PostalCode permanece como o valor padrão, que é uma string vazia
        }
    }
}

