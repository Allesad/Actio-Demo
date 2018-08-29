using System.Threading.Tasks;
using Actio.Common.Events;

namespace Actio.Api.Handlers
{
    public class UserAuthenticatedHandler : IEventHandler<UserAuthenticated>
    {
        public Task HandleAsync(UserAuthenticated @event)
        {
            return Task.CompletedTask;
        }
    }
}