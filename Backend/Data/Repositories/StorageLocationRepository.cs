using Backend.Data.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data.Repositories;

public class StorageLocationRepository : IBaseRepository<StorageLocationModel>
{
    private readonly ApplicationDbContext _db;

    public StorageLocationRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<StorageLocationModel>> GetAll()
    {
        return await _db.StorageLocations.ToListAsync();
    }

    public async Task<StorageLocationModel?> Find(string id)
    {
        return await _db.StorageLocations.FirstOrDefaultAsync(storageLocation => storageLocation.LocationId == id);
    }
}
