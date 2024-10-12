using Backend.Data.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data.Repositories;

public interface IBpkbRepository : IBaseRepository<BpkbModel>
{
    Task<BpkbModel> Create(BpkbModel model);
}

public class BpkbRepository : IBpkbRepository
{
    private readonly ApplicationDbContext _db;

    public BpkbRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<BpkbModel>> GetAll()
    {
        return await _db.Bpkbs.Include(bpkb => bpkb.StorageLocation).ToListAsync();
    }

    public async Task<BpkbModel?> Find(string agreementNumber)
    {
        return await _db.Bpkbs.Include(bpkb => bpkb.StorageLocation).FirstOrDefaultAsync(bpkb => bpkb.AgreementNumber == agreementNumber);
    }

    public async Task<BpkbModel> Create(BpkbModel bpkb)
    {
        var result = await _db.AddAsync(bpkb);
        await _db.SaveChangesAsync();

        var createdBpkb = await _db.Bpkbs
            .Include(bpkb => bpkb.StorageLocation)
            .FirstOrDefaultAsync(bpkb => bpkb.AgreementNumber == result.Entity.AgreementNumber);

        return createdBpkb;
    }
}
