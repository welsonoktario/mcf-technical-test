using System.Globalization;
using Backend.Data.Dto;
using Backend.Data.Interfaces;
using Backend.Data.Repositories;
using Backend.Models;
using Backend.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/bpkb")]
public class BpkbController : ControllerBase
{
    private readonly IBpkbRepository _bpkbRepository;
    private readonly IUserRepository _userRepository;
    private readonly IBaseRepository<StorageLocationModel> _storageLocationRepository;
    private readonly ILogger<BpkbController> _logger;

    public BpkbController(IBpkbRepository bpkbRepository, IUserRepository userRepository, IBaseRepository<StorageLocationModel> storageLocationRepository, ILogger<BpkbController> logger)
    {
        _bpkbRepository = bpkbRepository;
        _userRepository = userRepository;
        _storageLocationRepository = storageLocationRepository;
        _logger = logger;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var userName = User.Identity?.Name;
        if (userName == null)
        {
            return ApiResponse.Fail(
                statusCode: StatusCodes.Status401Unauthorized,
                message: "Unauthorized"
            );
        }

        var user = await _userRepository.Find(userName);
        if (user == null)
        {
            return ApiResponse.Fail(
                statusCode: StatusCodes.Status401Unauthorized,
                message: "Unauthorized"
            );
        }

        var bpkbs = await _bpkbRepository.GetAll();

        return ApiResponse.Success(
            data: bpkbs
        );
    }

    [HttpGet("{agreementNumber}")]
    public async Task<IActionResult> Edit(string agreementNumber)
    {
        var userName = User.Identity?.Name;
        if (userName == null)
        {
            return ApiResponse.Fail(
                statusCode: StatusCodes.Status401Unauthorized,
                message: "Unauthorized"
            );
        }

        var user = await _userRepository.Find(userName);
        if (user == null)
        {
            return ApiResponse.Fail(
                statusCode: StatusCodes.Status401Unauthorized,
                message: "Unauthorized"
            );
        }

        var bpkb = await _bpkbRepository.Find(agreementNumber);

        return ApiResponse.Success(
            data: bpkb
        );
    }

    [HttpPost("")]
    public async Task<IActionResult> Create([FromBody] CreateBpkbDto payload)
    {
        var userName = User.Identity?.Name;
        if (userName == null)
        {
            return ApiResponse.Fail(
                statusCode: StatusCodes.Status401Unauthorized,
                message: "Unauthorized"
            );
        }

        var user = await _userRepository.Find(userName);
        if (user == null)
        {
            return ApiResponse.Fail(
                statusCode: StatusCodes.Status401Unauthorized,
                message: "Unauthorized"
            );
        }

        if (!DateTime.TryParseExact(payload.BpkbDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime bpkbDate))
        {
            ModelState.AddModelError(nameof(payload.BpkbDate), "Format Tanggal BPKB salah");
        }

        if (!DateTime.TryParseExact(payload.BpkbDateIn, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime bpkbDateIn))
        {
            ModelState.AddModelError(nameof(payload.BpkbDateIn), "Format Tanggal BPKB In salah");
        }

        if (!DateTime.TryParseExact(payload.FakturDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fakturDate))
        {
            ModelState.AddModelError(nameof(payload.FakturDate), "Format Tanggal Faktur salah");
        }

        var storageLocation = _storageLocationRepository.Find(payload.StorageLocationId);

        if (storageLocation == null)
        {
            ModelState.AddModelError(nameof(payload.StorageLocationId), "Pilih Lokasi Penyimpanan");
        }

        var bpkbExists = await _bpkbRepository.Find(payload.AgreementNumber);
        if (bpkbExists != null)
        {
            ModelState.AddModelError(nameof(payload.AgreementNumber), $"Data dengan Agreement Number '{payload.AgreementNumber}' telah ada");
        }

        if (!ModelState.IsValid)
        {
            var errors = ModelStateUtils.GetModelStateErrors(ModelState);

            return ApiResponse.Fail(
                statusCode: StatusCodes.Status400BadRequest,
                message: "Validation error",
                errors: errors
            );
        }

        try
        {
            var bpkbPayload = new BpkbModel
            {
                AgreementNumber = payload.AgreementNumber,
                BpkbDate = bpkbDate,
                BpkbDateIn = bpkbDateIn,
                BranchId = payload.BranchId,
                BpkbNo = payload.BpkbNo,
                FakturDate = fakturDate,
                FakturNo = payload.FakturNo,
                LocationId = payload.StorageLocationId,
                PoliceNo = payload.PoliceNo,
                CreatedBy = user.UserName,
                CreatedOn = DateTime.Now,
                LastUpdatedBy = user.UserName,
                LastUpdatedOn = DateTime.Now,
            };

            var bpkb = await _bpkbRepository.Create(bpkbPayload);

            return ApiResponse.Success(
                statusCode: StatusCodes.Status201Created,
                message: "Data BPKB berhasil disimpan",
                data: bpkb
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[BpkbController][Create] - An error occurred: {msg}", ex.Message);
            return ApiResponse.Fail(
                statusCode: StatusCodes.Status500InternalServerError,
                message: "Unhandled error. Please try again later"    
            );
        }
    }

    [HttpPost("{agreementNumber}")]
    public async Task<IActionResult> Update(string agreementNumber, [FromBody] CreateBpkbDto payload)
    {
        var userName = User.Identity?.Name;
        if (userName == null)
        {
            return ApiResponse.Fail(
                statusCode: StatusCodes.Status401Unauthorized,
                message: "Unauthorized"
            );
        }

        var user = await _userRepository.Find(userName);
        if (user == null)
        {
            return ApiResponse.Fail(
                statusCode: StatusCodes.Status401Unauthorized,
                message: "Unauthorized"
            );
        }

        var bpkb = await _bpkbRepository.Find(agreementNumber);
        if (bpkb == null)
        {
            return ApiResponse.Fail(
                statusCode: StatusCodes.Status400BadRequest,
                message: "Data BPKB tidak ditemukan"
            );
        }

        if (!DateTime.TryParseExact(payload.BpkbDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime bpkbDate))
        {
            ModelState.AddModelError(nameof(payload.BpkbDate), "Format Tanggal BPKB salah");
        }

        if (!DateTime.TryParseExact(payload.BpkbDateIn, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime bpkbDateIn))
        {
            ModelState.AddModelError(nameof(payload.BpkbDateIn), "Format Tanggal BPKB In salah");
        }

        if (!DateTime.TryParseExact(payload.FakturDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fakturDate))
        {
            ModelState.AddModelError(nameof(payload.FakturDate), "Format Tanggal Faktur salah");
        }

        var storageLocation = _storageLocationRepository.Find(payload.StorageLocationId);
        if (storageLocation == null)
        {
            ModelState.AddModelError(nameof(payload.StorageLocationId), "Pilih Lokasi Penyimpanan");
        }

        var bpkbExists = await _bpkbRepository.Find(payload.AgreementNumber, bpkb.AgreementNumber);
        if (bpkbExists != null)
        {
            ModelState.AddModelError(nameof(payload.AgreementNumber), $"Data dengan Agreement Number '{payload.AgreementNumber}' telah ada");
        }

        if (!ModelState.IsValid)
        {
            var errors = ModelStateUtils.GetModelStateErrors(ModelState);

            return ApiResponse.Fail(
                statusCode: StatusCodes.Status400BadRequest,
                message: "Validation error",
                errors: errors
            );
        }

        try
        {
            bpkb.AgreementNumber = agreementNumber;
            bpkb.BpkbDate = bpkbDate;
            bpkb.BpkbDateIn = bpkbDateIn;
            bpkb.BranchId = payload.BranchId;
            bpkb.BpkbNo = payload.BpkbNo;
            bpkb.FakturDate = fakturDate;
            bpkb.FakturNo = payload.FakturNo;
            bpkb.LocationId = payload.StorageLocationId;
            bpkb.PoliceNo = payload.PoliceNo;
            bpkb.LastUpdatedBy = user.UserName;
            bpkb.LastUpdatedOn = DateTime.Now;

            var newBpkb = await _bpkbRepository.Update(bpkb);

            return ApiResponse.Success(
                statusCode: StatusCodes.Status200OK,
                message: "Data BPKB berhasil diubah",
                data: bpkb
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[BpkbController][Create] - An error occurred: {msg}", ex.Message);
            return ApiResponse.Fail(
                statusCode: StatusCodes.Status500InternalServerError,
                message: "Unhandled error. Please try again later"
            );
        }
    }

    [HttpDelete("{agreementNumber}")]
    public async Task<IActionResult> Delete(string agreementNumber)
    {
        var userName = User.Identity?.Name;
        if (userName == null)
        {
            return ApiResponse.Fail(
                statusCode: StatusCodes.Status401Unauthorized,
                message: "Unauthorized"
            );
        }

        var user = await _userRepository.Find(userName);
        if (user == null)
        {
            return ApiResponse.Fail(
                statusCode: StatusCodes.Status401Unauthorized,
                message: "Unauthorized"
            );
        }

        var bpkb = await _bpkbRepository.Find(agreementNumber);
        if (bpkb == null)
        {
            return ApiResponse.Fail(
                statusCode: StatusCodes.Status400BadRequest,
                message: "Data BPKB tidak ditemukan"
            );
        }

        try
        {
            var result = await _bpkbRepository.Delete(bpkb);
            if (!result)
            {
                throw new Exception("Gagal menghapus data BPKB");
            }

            return ApiResponse.Success(
                statusCode: StatusCodes.Status200OK,
                message: "Data BPKB berhasil dihapus",
                data: bpkb
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[BpkbController][Create] - An error occurred: {msg}", ex.Message);
            return ApiResponse.Fail(
                statusCode: StatusCodes.Status500InternalServerError,
                message: ex.Message ?? "Unhandled error. Please try again later"
            );
        }
    }
}
