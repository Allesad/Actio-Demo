using System;
using System.Threading.Tasks;
using Actio.Api.Models;
using Actio.Api.Repositories;
using Actio.Common.Events;
using Microsoft.Extensions.Logging;

namespace Actio.Api.Handlers
{
    public class ActivityCreatedHandler : IEventHandler<ActivityCreated>
    {
        private readonly ILogger _logger;
        private readonly IActivityRepository _activityRepository;

        public ActivityCreatedHandler(ILogger<ActivityCreatedHandler> logger,
            IActivityRepository activityRepository)
        {
            _logger = logger;
            _activityRepository = activityRepository;
        }

        public async Task HandleAsync(ActivityCreated @event)
        {
            try
            {
                await _activityRepository.AddAsync(new Activity
                {
                    Id = @event.Id,
                    UserId = @event.UserId,
                    Name = @event.Name,
                    Category = @event.Category,
                    Description = @event.Description,
                    CreatedAt = @event.CreatedAt

                });
                _logger.LogInformation("Activity created {Activity}", @event.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to store activity {Activity}", @event.Name);
            }
        }
    }
}