using Actio.Api.Repositories;
using Actio.Common.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Api.Controllers
{
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ActivitiesController : Controller
    {
        private readonly IBusClient _bus;
        private readonly IActivityRepository _repository;

        public ActivitiesController(IBusClient bus, IActivityRepository repository)
        {
            _bus = bus;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var activities = await _repository.BrowseAsync(Guid.Parse(User.Identity.Name));

            return Json(activities.Select(act => new { act.Id, act.Name, act.Category, act.CreatedAt }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var activity = await _repository.GetAsync(id);
            if (activity == null) return NotFound();

            if (activity.UserId != Guid.Parse(User.Identity.Name)) return Unauthorized();

            return Json(activity);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateActivity command)
        {
            command.Id = Guid.NewGuid();
            command.CreatedAt = DateTime.UtcNow;
            command.UserId = Guid.Parse(User.Identity.Name);
            await _bus.PublishAsync(command);

            return Accepted($"activities/{command.Id}");
        }
    }
}