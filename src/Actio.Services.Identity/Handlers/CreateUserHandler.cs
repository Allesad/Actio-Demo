using System.Threading.Tasks;
using Actio.Common.Commands;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace Actio.Services.Identity.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly ILogger _logger;
        private readonly IBusClient _bus;
        //private readonly IUserService _userService;

        public CreateUserHandler(ILogger<CreateUserHandler> logger,
            IBusClient bus)
        {
            _logger = logger;
            _bus = bus;
        }

        public Task HandleAsync(CreateUser command)
        {
            _logger.LogInformation("Create user: {User} with email {Email}", 
                command.Name, command.Email);

            return Task.CompletedTask;
        }
    }
}