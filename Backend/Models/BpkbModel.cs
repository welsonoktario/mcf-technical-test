using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("tr_bpkb")]
public class BpkbModel
{
    [Key]
    [Column("agreement_number", TypeName = "varchar(100)")]
    public string AgreementNumber { get; set; }

    [Column("bpkb_no", TypeName = "varchar(100)")]
    public string BpkbNo { get; set; }

    [Column("branch_id", TypeName = "varchar(10)")]
    public string BranchId { get; set; }

    [Column("bpkb_date", TypeName = "datetime")]
    public DateTime? BpkbDate { get; set; }

    [Column("faktur_no", TypeName = "varchar(100)")]
    public string FakturNo { get; set; }

    [Column("faktur_date", TypeName = "datetime")]
    public DateTime? FakturDate { get; set; }

    [Column("location_id", TypeName = "varchar(10)")]
    public string LocationId { get; set; }

    [Column("police_no", TypeName = "varchar(20)")]
    public string PoliceNo { get; set; }

    [Column("bpkb_date_in", TypeName = "datetime")]
    public DateTime? BpkbDateIn { get; set; }

    [Column("created_by", TypeName = "varchar(20)")]
    public string CreatedBy { get; set; }

    [Column("created_on", TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column("last_updated_by", TypeName = "varchar(20)")]
    public string LastUpdatedBy { get; set; }

    [Column("last_updated_on", TypeName = "datetime")]
    public DateTime? LastUpdatedOn { get; set; }

    public StorageLocationModel StorageLocation { get; set; }
}
