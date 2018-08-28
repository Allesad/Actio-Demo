using System.Threading.Tasks;
using Actio.Common;
using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Services.Activities.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace Actio.Services.Activities.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly IBusClient _bus;
        private readonly ILogger<CreateActivityHandler> _logger;
        private readonly IActivityService _activityService;

        public CreateActivityHandler(IBusClient bus, ILogger<CreateActivityHandler> logger, IActivityService activityService)
        {
            this._bus = bus;
            this._logger = logger;
            this._activityService = activityService;
        }

        public async Task HandleAsync(CreateActivity command)
        {
            _logger.LogInformation("Creating activity: {Activity}", command.Name);

            try
            {
                await _activityService.AddAsync(command.Id, command.UserId, command.Category, 
                    command.Name, command.Description, command.CreatedAt);
                await _bus.PublishAsync(new ActivityCreated(command.Id, command.UserId, 
                    command.Category, command.Name));
            }
            catch (ActioException ex)
            {
                await _bus.PublishAsync(
                    new CreateActivityRejected(command.Id, ex.Code, ex.Message));
                _logger.LogError(ex, ex.Message);
            }
            catch (System.Exception ex)
            {
                await _bus.PublishAsync(new CreateActivityRejected(command.Id, "error", ex.Message));
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}