using ForenserBackend.Domain.entities;

namespace ForenserBackend.Domain.RepositoriesInterfaces
{
    public interface IServiceScheduleRepository
    {
        public Task RegisterServiceSchedule(ServiceScheduleEntity service);
        public Task UnscheduleService(string scheduleId);

        public void UpdateSchedule(ServiceScheduleEntity serviceNewData);

        public Task<ServiceScheduleEntity> GetServiceScheduleById(string scheduleId);

        public Task<List<ServiceScheduleEntity>> GetAllScheduledServices();
    }
}
