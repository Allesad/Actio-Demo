using System;
using System.Threading.Tasks;
using Actio.Common;
using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Services.Identity.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace Actio.Services.Identity.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly ILogger _logger;
        private readonly IBusClient _bus;
        private readonly IUserService _userService;

        public CreateUserHandler(ILogger<CreateUserHandler> logger,
            IBusClient bus,
            IUserService userService)
        {
            _logger = logger;
            _bus = bus;
            _userService = userService;
        }

        public async Task HandleAsync(CreateUser command)
        {
            _logger.LogInformation("Create user: {User} with email {Email}", 
                command.Name, command.Email);

            try
            {
                await _userService.RegisterAsync(command.Email, command.Password, command.Name);
                await _bus.PublishAsync(new UserCreated(command.Email, command.Name));
            }
            catch (ActioException ex)
            {
                _logger.LogError(ex, ex.Message);
                await _bus.PublishAsync(new CreateUserRejected(command.Email, ex.Code, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await _bus.PublishAsync(new CreateUserRejected(command.Email, "error", ex.Message));
            }
        }
    }
}