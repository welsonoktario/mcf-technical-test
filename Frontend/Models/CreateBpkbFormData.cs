using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Frontend.Models;

public class CreateBpkbFormData
{
    [JsonPropertyName("agreementNumber")]
    [Required(ErrorMessage = "Agreement Number tidak boleh kosong")]
    [MaxLength(100, ErrorMessage = "Jumlah maksimum Agreement Number adalah 100")]
    public string AgreementNumber { get; set; }

    [JsonPropertyName("branchId")]
    [Required(ErrorMessage = "Branch Id tidak boleh kosong")]
    [MaxLength(10, ErrorMessage = "Jumlah maksimum Branch Id adalah 10")]
    public string BranchId { get; set; }

    [JsonPropertyName("bpkbNo")]
    [Required(ErrorMessage = "No. BPKB tidak boleh kosong")]
    [MaxLength(100, ErrorMessage = "Jumlah maksimum karakter No. BPKB adalah 100")]
    public string BpkbNo { get; set; }

    [JsonPropertyName("bpkbDateIn")]
    [Required(ErrorMessage = "Tanggal BPKB In tidak boleh kosong")]
    public string BpkbDateIn { get; set; }

    [JsonPropertyName("bpkbDate")]
    [Required(ErrorMessage = "Tanggal BPKB tidak boleh kosong")]
    public string BpkbDate { get; set; }

    [JsonPropertyName("fakturNo")]
    [Required(ErrorMessage = "No. Faktur tidak boleh kosong")]
    [MaxLength(100, ErrorMessage = "Jumlah maksimum karakter No. Faktur adalah 100")]
    public string FakturNo { get; set; }

    [JsonPropertyName("fakturDate")]
    [Required(ErrorMessage = "Tanggal Faktur tidak boleh kosong")]
    public string FakturDate { get; set; }

    [JsonPropertyName("policeNo")]
    [Required(ErrorMessage = "Nomor Polisi tidak boleh kosong")]
    [MaxLength(20, ErrorMessage = "Jumlah maksimum karakter Nomor Polisi adalah 20")]
    public string PoliceNo { get; set; }

    [JsonPropertyName("storageLocationId")]
    [Required(ErrorMessage = "Pilih Lokasi Penyimpanan")]
    public string StorageLocationId { get; set; }
}
