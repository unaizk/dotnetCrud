using MongoDB.Driver;
using dotnetCrud.Models;
using Microsoft.Extensions.Options;

namespace dotnetCrud.Services;

public class UserService
{
    private readonly IMongoCollection<User> _users;

    public UserService(IOptions<MongoDBSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _users = database.GetCollection<User>(settings.Value.CollectionName);
    }

    public async Task<List<User>> GetAllAsync() => await _users.Find(_ => true).ToListAsync();
    public async Task<User?> GetByIdAsync(string id) => await _users.Find(user => user.Id == id).FirstOrDefaultAsync();
    public async Task CreateAsync(User user) => await _users.InsertOneAsync(user);
    public async Task UpdateAsync(string id, User updated) => await _users.ReplaceOneAsync(user => user.Id == id, updated);
    public async Task DeleteAsync(string id) => await _users.DeleteOneAsync(user => user.Id == id);
}
