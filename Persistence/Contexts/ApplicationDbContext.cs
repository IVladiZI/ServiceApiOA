using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Persistence.Contexts
{
    /// <ApplicationDbContext>
    /// The Db context is the representation of the database through the entityframework, that is, it helps us with any method we need from the DB.
    /// </ApplicationDbContext>
    public class ApplicationDbContext : DbContext
    {
        /// <IDateTimeService>
        /// In the dependency injection we must enroll the service in this case 
        /// the IDateTimeService must be enrolled for them must be added in the Shared layer in Services.
        /// </IDateTimeService>
        private readonly IDateTimeService _dateTimeService;

        private readonly IConfiguration _configuration;
        /// <ApplicationDbContext>
        /// It helps us to make a tracking behavior with the entityframwork core controlling all the information 
        /// about an entity instance, where it tracks the changes on an entity using the SaveChange, it will handle 
        /// entityframwork core also the changes that are made in the database.
        /// </ApplicationDbContext>
        /// <param name="options"></param>
        /// <param name="dateTimeService">With this we place the creation and modification dates</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTimeService, IConfiguration configuration) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTimeService = dateTimeService;
            _configuration = configuration;
        }
        public DbSet<Client> Clients { get; set; }
        /// <Task>
        /// Over we write this method, which causes any changes we make to be saved in the database
        /// </Task>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            // Each time we save changes this method will write to the auditable to save the date and time of the change.
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreateUser = _configuration["UsersAuditable:UserCreate"];
                        entry.Entity.ModifyUser = _configuration["UsersAuditable:UserModify"];
                        entry.Entity.CreateDate = _dateTimeService.NowUtc;
                        entry.Entity.ModifyDate = _dateTimeService.NowUtc;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifyUser = _configuration["UsersAuditable:UserModify"];
                        entry.Entity.ModifyDate = _dateTimeService.NowUtc;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        /// <OnModelCreating>
        /// This method calls the configurations of the classes and features that will be used in the database.
        /// </OnModelCreating>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
