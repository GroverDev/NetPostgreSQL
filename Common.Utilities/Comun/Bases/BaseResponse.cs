namespace Common.Utilities.Comun.Bases;

public class BaseResponse<T>
{
    public bool Ok { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
    public IEnumerable<BaseError>? Errors { get; set; }
}
