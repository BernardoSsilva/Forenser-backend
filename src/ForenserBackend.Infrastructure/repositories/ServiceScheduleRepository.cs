using ForenserBackend.Domain.entities;
using ForenserBackend.Domain.RepositoriesInterfaces;
using ForenserBackend.Exception.HttpErrors;
using Microsoft.EntityFrameworkCore;

namespace ForenserBackend.Infrastructure.repositories
{
    public class ServiceScheduleRepository : IServiceScheduleRepository
    {

        private readonly ForenserDbContext _dbContext;

        public ServiceScheduleRepository(ForenserDbContext context)
        {
            _dbContext = context;
        }
        public async Task<List<ServiceScheduleEntity>> GetAllScheduledServices()
        {
            return await _dbContext.ServiceSchedules.AsNoTracking().ToListAsync();
        }

        public async Task<ServiceScheduleEntity> GetServiceScheduleById(string scheduleId)
        {
            var service = await _dbContext.ServiceSchedules.AsNoTracking().FirstOrDefaultAsync(service => service.Id == scheduleId);
            if(service is null)
            {
                throw new NotFoundException("Service schedule not found");
            }
            return service;
        }

        public async Task RegisterServiceSchedule(ServiceScheduleEntity service)
        {
            await _dbContext.AddAsync(service);
        }

        public async Task UnscheduleService(string scheduleId)
        {
            var scheduleToDelete = await _dbContext.ServiceSchedules.FirstOrDefaultAsync(schedule => schedule.Id == scheduleId);

            if (scheduleToDelete is null)
            {
                throw new NotFoundException("Service schedule not found");
            }
            _dbContext.Remove(scheduleToDelete);
         
        }

        public void UpdateSchedule(ServiceScheduleEntity serviceNewData)
        {
            _dbContext.Update(serviceNewData);
        }
    }
}
