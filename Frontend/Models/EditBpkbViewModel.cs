namespace Frontend.Models;

public class EditBpkbViewModel : BaseViewModel
{
    public Bpkb Bpkb { get; set; }
    public CreateBpkbFormData FormData { get; set; }
    public List<StorageLocation> StorageLocations { get; set; }
}
