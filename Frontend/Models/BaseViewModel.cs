public class BaseViewModel
{
    public bool IsError { get; set; } = false;
    public string Message { get; set; }
    public Dictionary<string, string[]?>? Errors { get; set; }
}