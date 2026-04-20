using ForenserBackend.Domain.entities;

namespace ForenserBackend.Domain.RepositoriesInterfaces
{
    public interface IReportRepository
    {

        public Task CreateNewReport(ReportEntity report);
        public Task DeleteReport(string reportId);
        public void UpdateReport(ReportEntity reportNewData);

        public Task<ReportEntity> GetReportById(string reportId);
        public Task<List<ReportEntity>> GetAllReports();
    }
}
