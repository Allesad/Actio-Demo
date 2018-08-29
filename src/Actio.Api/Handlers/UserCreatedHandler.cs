using System.Threading.Tasks;
using Actio.Common.Events;

namespace Actio.Api.Handlers
{
    public class UserCreatedHandler : IEventHandler<UserCreated>
    {
        public Task HandleAsync(UserCreated @event)
        {
            return Task.CompletedTask;
        }
    }
}