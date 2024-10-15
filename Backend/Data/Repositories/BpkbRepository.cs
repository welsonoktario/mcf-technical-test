using Backend.Data.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data.Repositories;

public interface IBpkbRepository : IBaseRepository<BpkbModel>
{
    Task<BpkbModel?> Find(string agreementNumber, string excludedAgreementNumber);
    Task<BpkbModel> Create(BpkbModel model);
    Task<bool> Update(BpkbModel model);
    Task<bool> Delete(BpkbModel model);
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

    public async Task<BpkbModel?> Find(string agreementNumber, string excludedAgreementNumber)
    {
        return await _db.Bpkbs
            .Include(bpkb => bpkb.StorageLocation)
            .FirstOrDefaultAsync(bpkb => bpkb.AgreementNumber == agreementNumber && bpkb.AgreementNumber != excludedAgreementNumber);
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

    public async Task<bool> Delete(BpkbModel bpkb)
    {
        _db.Bpkbs.Remove(bpkb);
        var result = await _db.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> Update(BpkbModel newBpkb)
    {
        _db.Bpkbs.Update(newBpkb);
        var result = await _db.SaveChangesAsync();

        return result > 0;
    }
}
