using System;
using System.Threading.Tasks;
using Actio.Services.Identity.Domain.Models;
using Actio.Services.Identity.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Actio.Services.Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase _db;

        public UserRepository(IMongoDatabase db)
        {
            _db = db;
        }

        public async Task<User> GetAsync(Guid id)
            => await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(user => user.Id == id);

        public async Task<User> GetAsync(string email)
            => await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(user => user.Email == email.ToLowerInvariant());
        
        public async Task AddAsync(User user)
            => await Collection.InsertOneAsync(user);

        private IMongoCollection<User> Collection
            => _db.GetCollection<User>("Users");
    }
}