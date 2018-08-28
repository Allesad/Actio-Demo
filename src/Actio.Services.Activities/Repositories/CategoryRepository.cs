using System.Collections.Generic;
using System.Threading.Tasks;
using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Actio.Services.Activities.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoDatabase _db;

        public CategoryRepository(IMongoDatabase db)
        {
            _db = db;
        }

        public async Task<Category> GetAsync(string name)
            => await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(category => category.Name == name.ToLowerInvariant());

        public async Task AddAsync(Category category)
            => await Collection.InsertOneAsync(category);

        public async Task<IEnumerable<Category>> BrowseAsync()
            => await Collection
                .AsQueryable()
                .ToListAsync();

        private IMongoCollection<Category> Collection => _db.GetCollection<Category>("Categories");
    }
}