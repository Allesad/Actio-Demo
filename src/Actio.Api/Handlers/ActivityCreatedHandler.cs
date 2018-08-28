using System.Threading.Tasks;
using Actio.Common.Events;
using Microsoft.Extensions.Logging;

namespace Actio.Api.Handlers
{
    public class ActivityCreatedHandler : IEventHandler<ActivityCreated>
    {
        private readonly ILogger _logger;

        public ActivityCreatedHandler(ILogger<ActivityCreatedHandler> logger)
        {
            _logger = logger;
        }

        public Task HandleAsync(ActivityCreated @event)
        {
            _logger.LogInformation("Activity created {Activity}", @event.Name);

            return Task.CompletedTask;
        }
    }
}