using Backend.Data.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data.Repositories;

public interface IUserRepository : IBaseRepository<UserModel>
{
    Task<UserModel?> Find(long id);
}

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;

    public UserRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<UserModel>> GetAll()
    {
        return await _db.Users.ToListAsync();
    }

    public async Task<UserModel?> Find(long userId)
    {
        return await _db.Users.FirstOrDefaultAsync(user => user.UserId == userId);
    }

    public async Task<UserModel?> Find(string userName)
    {
        return await _db.Users.FirstOrDefaultAsync(user => user.UserName == userName);
    }
}
