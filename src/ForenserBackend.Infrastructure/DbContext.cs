using ForenserBackend.Domain.entities;
using Microsoft.EntityFrameworkCore;

namespace ForenserBackend.Infrastructure
{
    public class ForenserDbContext:DbContext
    {
        
        public ForenserDbContext(DbContextOptions<ForenserDbContext> options) : base(options)
        {

        }

       
        public DbSet<ImageEntity> Images { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ReportEntity> Reports { get; set; }
        public DbSet<VehicleEntity> Vehicles { get; set; }
        public DbSet<OccurrenceEntity> Occurrences { get; set; }
        public DbSet<ServiceScheduleEntity> ServiceSchedules { get; set; }
        public DbSet<PeopleEntity> Peoples { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.Occurrences)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .HasPrincipalKey(e => e.Id).IsRequired();

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.Schedules)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .HasPrincipalKey(e => e.Id).IsRequired();

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.Reports)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .HasPrincipalKey(e => e.Id);

            modelBuilder.Entity<OccurrenceEntity>()
                .HasMany(e => e.Images)
                .WithOne(e => e.Occurrence)
                .HasForeignKey(e => e.OccurenceId)
                .HasPrincipalKey(e => e.Id).IsRequired();

            modelBuilder.Entity<OccurrenceEntity>()
                .HasMany(e => e.Vehicles)
                .WithOne(e => e.Occurrence)
                .HasForeignKey(e => e.OcurrenceId)
                .HasPrincipalKey(e => e.Id).IsRequired();

            modelBuilder.Entity<OccurrenceEntity>()
                .HasMany(e => e.EnvolvedPeople)
                .WithOne(e => e.Occurrence)
                .HasForeignKey(e => e.OccurrenceId)
                .HasPrincipalKey(e => e.Id).IsRequired();
        } 
    }
}
