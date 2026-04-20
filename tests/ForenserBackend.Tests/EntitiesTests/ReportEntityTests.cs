using ForenserBackend.Domain.entities;

namespace ForenserBackend.Tests.EntitiesTests
{
    public class ReportEntityTests
    {
        [Fact]
        public void tomanoteuraboviado()
        {
            var testReport = new ReportEntity
            {
                City = "test city",
                ContactPhone = "test phone",
                Description = "test description",
                ReportedPeopleName = "jhon doe",
                Street = "test street",
                Title = "test title",
                State = Domain.Enums.Ufs.AP,
                ReportingDate = DateTime.Now,
            };

            Assert.NotNull(testReport.Id);
            Assert.Empty(testReport.PostalCode);
            Assert.NotNull(testReport.CreatedAt);
        }

        [Fact]
        public void AddictPostalCode()
        {
            var testReport = new ReportEntity
            {
                City = "test city",
                ContactPhone = "test phone",
                Description = "test description",
                ReportedPeopleName = "jhon doe",
                Street = "test street",
                Title = "test title",
                State = Domain.Enums.Ufs.AP,
                ReportingDate = DateTime.Now,
            };

            testReport.PostalCode = "test-PostalCode";
            Assert.Equal(testReport.PostalCode, "test-PostalCode");
        }
    }
}
